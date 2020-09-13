﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dsl
{
    public delegate void DslLogDelegation(string msg);
    internal enum DslBinaryCode
    {
        BeginStatement = 1,
        EndStatement,
        BeginFunction,
        EndFunction,
        BeginValue,
        EndValue,
        ValueTypeBegin,
        ValueTypeEnd = ValueTypeBegin + ValueData.MAX_TYPE,
        ParamOrExternClassBegin
    }

    internal class SyntaxComponentCommentsInfo
    {
        internal List<string> mFirstComments = null;
        internal bool mFirstCommentOnNewLine = false;
        internal List<string> mLastComments = null;
        internal bool mLastCommentOnNewLine = false;
    }
    internal class FunctionCommentsInfo : SyntaxComponentCommentsInfo
    {
        internal List<string> m_Comments = null;
    }
    /// <summary>
    /// 基于函数样式的脚本化数据解析工具。可以用作DSL元语言。
    /// </summary>
    /// <remarks>
    /// 混淆的当前实现要求脚本里不能出现`字符。另外，测试代码的加解密表的设计要求脚本里不能出现以形如“_数字_”的标识符。
    /// </remarks>
    public interface ISyntaxComponent
    {
        bool IsValid();
        string GetId();
        int GetIdType();
        int GetLine();
        string ToScriptString(bool includeComment);

        string CalcFirstComment();
        string CalcLastComment();
        void CopyComments(ISyntaxComponent other);
        void CopyFirstComments(ISyntaxComponent other);
        void CopyLastComments(ISyntaxComponent other);
        List<string> FirstComments { get; }
        bool FirstCommentOnNewLine { get; set; }
        List<string> LastComments { get; }
        bool LastCommentOnNewLine { get; set; }
    }

    public abstract class AbstractSyntaxComponent : ISyntaxComponent
    {
        public const int ID_TOKEN = 0;
        public const int NUM_TOKEN = 1;
        public const int STRING_TOKEN = 2;
        public const int MAX_TYPE = 2;

        public abstract bool IsValid();
        public abstract string GetId();
        public abstract int GetIdType();
        public abstract int GetLine();
        public abstract string ToScriptString(bool includeComment);

        public string CalcFirstComment()
        {
            if (DslFile.DontLoadComments)
                return string.Empty;
            var cmtsInfo = GetCommentsInfo();
            if (null == cmtsInfo || null == cmtsInfo.mFirstComments) {
                return string.Empty;
            }
            else {
                return string.Join(string.Empty, cmtsInfo.mFirstComments.ToArray());
            }
        }
        public string CalcLastComment()
        {
            if (DslFile.DontLoadComments)
                return string.Empty;
            var cmtsInfo = GetCommentsInfo();
            if (null == cmtsInfo || null == cmtsInfo.mLastComments) {
                return string.Empty;
            }
            else {
                return string.Join(string.Empty, cmtsInfo.mLastComments.ToArray());
            }
        }
        public void CopyComments(ISyntaxComponent other)
        {
            CopyFirstComments(other);
            CopyLastComments(other);
        }
        public void CopyFirstComments(ISyntaxComponent other)
        {
            if (DslFile.DontLoadComments)
                return;
            if (other.FirstComments.Count > 0) {
                FirstComments.AddRange(other.FirstComments);
                FirstCommentOnNewLine = other.FirstCommentOnNewLine;
            }
        }
        public void CopyLastComments(ISyntaxComponent other)
        {
            if (DslFile.DontLoadComments)
                return;
            if (other.LastComments.Count > 0) {
                LastComments.AddRange(other.LastComments);
                LastCommentOnNewLine = other.LastCommentOnNewLine;
            }
        }

        public List<string> FirstComments
        {
            get {
                if (DslFile.DontLoadComments)
                    return NullSyntax.EmptyStringList;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return NullSyntax.EmptyStringList;
                if (null == cmtsInfo.mFirstComments) {
                    cmtsInfo.mFirstComments = new List<string>();
                }
                return cmtsInfo.mFirstComments;
            }
        }
        public bool FirstCommentOnNewLine
        {
            get {
                if (DslFile.DontLoadComments)
                    return false;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return false;
                return cmtsInfo.mFirstCommentOnNewLine;
            }
            set {
                if (DslFile.DontLoadComments)
                    return;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return;
                cmtsInfo.mFirstCommentOnNewLine = value;
            }
        }
        public List<string> LastComments
        {
            get {
                if (DslFile.DontLoadComments)
                    return NullSyntax.EmptyStringList;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return NullSyntax.EmptyStringList;
                if (null == cmtsInfo.mLastComments) {
                    cmtsInfo.mLastComments = new List<string>();
                }
                return cmtsInfo.mLastComments;
            }
        }
        public bool LastCommentOnNewLine
        {
            get {
                if (DslFile.DontLoadComments)
                    return false;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return false;
                return cmtsInfo.mLastCommentOnNewLine;
            }
            set {
                if (DslFile.DontLoadComments)
                    return;
                var cmtsInfo = GetCommentsInfo();
                if (null == cmtsInfo)
                    return;
                cmtsInfo.mLastCommentOnNewLine = value;
            }
        }

        internal virtual SyntaxComponentCommentsInfo GetCommentsInfo()
        {
            return null;
        }
    }
    /// <summary>
    /// 空语法单件
    /// </summary>
    public class NullSyntax : AbstractSyntaxComponent
    {
        public override bool IsValid()
        {
            return false;
        }
        public override string GetId()
        {
            return string.Empty;
        }
        public override int GetIdType()
        {
            return ID_TOKEN;
        }
        public override int GetLine()
        {
            return -1;
        }
        public override string ToScriptString(bool includeComment)
        {
            return ToString();
        }

        public static NullSyntax Instance
        {
            get {
                return s_Instance;
            }
        }
        public static List<string> EmptyStringList
        {
            get {
                s_StringList.Clear();
                return s_StringList;
            }
        }
        private static NullSyntax s_Instance = new NullSyntax();
        private static List<string> s_StringList = new List<string>();
    }
    /// <summary>
    /// 用于描述变量、常量与无参命令语句。可能会出现在函数调用参数表与函数语句列表中。
    /// </summary>
    public class ValueData : AbstractSyntaxComponent
    {
        public override bool IsValid()
        {
            return HaveId();
        }
        public override string GetId()
        {
            return m_Id;
        }
        public override int GetIdType()
        {
            return m_Type;
        }
        public override int GetLine()
        {
            return m_Line;
        }
        public override string ToScriptString(bool includeComment)
        {
#if FULL_VERSION
            if (includeComment) {
                return CalcFirstComment() + Utility.quoteString(m_Id, m_Type) + CalcLastComment();
            }
            else {
                return Utility.quoteString(m_Id, m_Type);
            }
#else
      return ToString();
#endif
        }

        public bool HaveId()
        {
            return !string.IsNullOrEmpty(m_Id) || m_Type == STRING_TOKEN;
        }
        public void SetId(string id)
        {
            m_Id = id;
        }
        public void SetType(int _type)
        {
            m_Type = _type;
        }
        public void SetLine(int line)
        {
            m_Line = line;
        }
        public bool IsId()
        {
            return ID_TOKEN == m_Type;
        }
        public bool IsNumber()
        {
            return NUM_TOKEN == m_Type;
        }
        public bool IsString()
        {
            return STRING_TOKEN == m_Type;
        }
        public void Clear()
        {
            m_Type = ID_TOKEN;
            m_Id = string.Empty;
            m_Line = -1;
        }
        public void CopyFrom(ValueData other)
        {
            m_Type = other.m_Type;
            m_Id = other.m_Id;
            m_Line = other.m_Line;

            if (!DslFile.DontLoadComments) {
                CopyComments(other);
            }
        }

        public ValueData()
        { }
        public ValueData(string val)
        {
            if (Utility.needQuote(val))
                m_Type = STRING_TOKEN;
            else if (val.Length > 0 && (val[0] >= '0' && val[0] <= '9' || val[0] == '.' || val[0] == '-'))
                m_Type = NUM_TOKEN;
            else
                m_Type = ID_TOKEN;

            m_Id = val;
            m_Line = -1;
        }
        public ValueData(string val, int _type)
        {
            m_Type = _type;
            m_Id = val;
            m_Line = -1;
        }

        private int m_Type = ID_TOKEN;
        private string m_Id = string.Empty;
        private int m_Line = -1;

        public static ValueData NullValue
        {
            get {
                s_Instance.Clear();
                return s_Instance;
            }
        }
        private static ValueData s_Instance = new ValueData();
    }
    /// <summary>
    /// 函数数据，可能出现在函数头、参数表中。
    /// </summary>
    public class FunctionData : AbstractSyntaxComponent
    {
        public enum ParamClassEnum
        {
            PARAM_CLASS_MIN = 0,
            PARAM_CLASS_NOTHING = PARAM_CLASS_MIN,
            PARAM_CLASS_PARENTHESIS,
            PARAM_CLASS_BRACKET,
            PARAM_CLASS_PERIOD,
            PARAM_CLASS_PERIOD_PARENTHESIS,
            PARAM_CLASS_PERIOD_BRACKET,
            PARAM_CLASS_PERIOD_BRACE,
            PARAM_CLASS_QUESTION_PERIOD,
            PARAM_CLASS_QUESTION_PARENTHESIS,
            PARAM_CLASS_QUESTION_BRACKET,
            PARAM_CLASS_QUESTION_BRACE,
            PARAM_CLASS_POINTER,
            PARAM_CLASS_STATEMENT,
            PARAM_CLASS_EXTERN_SCRIPT,
            PARAM_CLASS_PARENTHESIS_ATTR,
            PARAM_CLASS_BRACKET_ATTR,
            PARAM_CLASS_PERIOD_STAR,
            PARAM_CLASS_QUESTION_PERIOD_STAR,
            PARAM_CLASS_POINTER_STAR,
            PARAM_CLASS_OPERATOR,
            PARAM_CLASS_TERNARY_OPERATOR,
            PARAM_CLASS_MAX,
            PARAM_CLASS_WRAP_INFIX_CALL_MASK = 0x20,
            PARAM_CLASS_UNMASK = 0x1F,
        }
        public override bool IsValid()
        {
            return HaveId() || HaveParamOrStatement();
        }
        public override string GetId()
        {
            if (null != m_Name)
                return m_Name.GetId();
            else if (null != m_LowerOrderFunction)
                return m_LowerOrderFunction.GetId();
            else
                return string.Empty;
        }
        public override int GetIdType()
        {
            if (null != m_Name)
                return m_Name.GetIdType();
            else if (null != m_LowerOrderFunction)
                return m_LowerOrderFunction.GetIdType();
            else
                return ID_TOKEN;
        }
        public override int GetLine()
        {
            if (null != m_Name)
                return m_Name.GetLine();
            else if (null != m_LowerOrderFunction)
                return m_LowerOrderFunction.GetLine();
            else
                return -1;
        }
        public override string ToScriptString(bool includeComment)
        {
#if FULL_VERSION
            if (includeComment) {
                return CalcFirstComment() + Utility.getFunctionString(this, true) + CalcComment() + CalcLastComment();
            }
            else {
                return Utility.getFunctionString(this, true);
            }
#else
      return ToString();
#endif
        }

        public string CalcComment()
        {
            if (DslFile.DontLoadComments)
                return string.Empty;
            var cmtsInfo = GetFunctionCommentsInfo();
            if (null == cmtsInfo || null == cmtsInfo.m_Comments)
                return string.Empty;
            string cmt = string.Join(string.Empty, cmtsInfo.m_Comments.ToArray());
            if (null != m_LowerOrderFunction && !HaveStatement() && !HaveExternScript()) {
                cmt = m_LowerOrderFunction.CalcComment() + cmt;
            }
            return cmt;
        }
        public List<string> Comments
        {
            get {
                if (DslFile.DontLoadComments)
                    return NullSyntax.EmptyStringList;
                var cmtsInfo = GetFunctionCommentsInfo();
                if (null == cmtsInfo || null == cmtsInfo.m_Comments) {
                    cmtsInfo.m_Comments = new List<string>();
                }
                return cmtsInfo.m_Comments;
            }
        }

        public List<ISyntaxComponent> Params
        {
            get {
                PrepareParams();
                return m_Params;
            }
            set {
                m_Params = value;
                if (null == m_Params) {
                    m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_NOTHING;
                }
                else if (m_Params.Count > 0) {
                    if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                        m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
                    }
                }
            }
        }
        public bool IsHighOrder
        {
            get { return m_IsHighOrder; }
        }
        public ValueData Name
        {
            get {
                if (null != m_Name)
                    return m_Name;
                else
                    return ValueData.NullValue;
            }
            set {
                m_Name = value;
                m_LowerOrderFunction = null;
                m_IsHighOrder = false;
            }
        }
        public FunctionData LowerOrderFunction
        {
            get {
                if (null != m_LowerOrderFunction)
                    return m_LowerOrderFunction;
                else
                    return FunctionData.NullFunction;
            }
            set {
                m_Name = null;
                m_LowerOrderFunction = value;
                m_IsHighOrder = true;
            }
        }
        public FunctionData ThisOrLowerOrderCall
        {
            get {
                if (HaveParam())
                    return this;
                else if (HaveLowerOrderParam())
                    return m_LowerOrderFunction;
                else 
                    return FunctionData.NullFunction;
            }
        }
        public FunctionData ThisOrLowerOrderBody
        {
            get {
                if (HaveStatement())
                    return this;
                else if (HaveLowerOrderStatement())
                    return m_LowerOrderFunction;
                else 
                    return FunctionData.NullFunction;
            }
        }
        public FunctionData ThisOrLowerOrderScript
        {
            get {
                if (HaveExternScript())
                    return this;
                else if (HaveLowerOrderExternScript())
                    return m_LowerOrderFunction;
                else 
                    return FunctionData.NullFunction;
            }
        }
        public bool HaveLowerOrderParam()
        {
            if (null != m_LowerOrderFunction && m_LowerOrderFunction.HaveParam())
                return true;
            else
                return false;
        }
        public bool HaveLowerOrderStatement()
        {
            if (null != m_LowerOrderFunction && m_LowerOrderFunction.HaveStatement())
                return true;
            else
                return false;
        }
        public bool HaveLowerOrderExternScript()
        {
            if (null != m_LowerOrderFunction && m_LowerOrderFunction.HaveExternScript())
                return true;
            else
                return false;
        }
        public bool HaveId()
        {
            if (null != m_Name)
                return m_Name.HaveId();
            else if (null != m_LowerOrderFunction)
                return m_LowerOrderFunction.HaveId();
            else
                return false;
        }
        public void SetParamClass(int type)
        {
            int innerType = type & (int)ParamClassEnum.PARAM_CLASS_UNMASK;
            if (innerType >= (int)ParamClassEnum.PARAM_CLASS_MIN && innerType < (int)ParamClassEnum.PARAM_CLASS_MAX) {
                m_ParamClass = type;
            }
        }
        public int GetParamClass()
        {
            return m_ParamClass;
        }
        public bool HaveParamOrStatement()
        {
            return (int)ParamClassEnum.PARAM_CLASS_NOTHING != m_ParamClass;
        }
        public bool HaveParam()
        {
            return HaveParamOrStatement() && !HaveStatement() && !HaveExternScript();
        }
        public bool HaveStatement()
        {
            return (int)ParamClassEnum.PARAM_CLASS_STATEMENT == m_ParamClass;
        }
        public bool HaveExternScript()
        {
            return (int)ParamClassEnum.PARAM_CLASS_EXTERN_SCRIPT == m_ParamClass;
        }
        public int GetParamNum()
        {
            if (null == m_Params)
                return 0;
            return m_Params.Count;
        }
        public void SetParam(int index, ISyntaxComponent data)
        {
            if (null == m_Params)
                return;
            if (index < 0 || index >= m_Params.Count)
                return;
            m_Params[index] = data;
        }
        public ISyntaxComponent GetParam(int index)
        {
            if (null == m_Params)
                return NullSyntax.Instance;
            if (index < 0 || index >= (int)m_Params.Count)
                return NullSyntax.Instance;
            return m_Params[index];
        }
        public string GetParamId(int index)
        {
            if (null == m_Params)
                return string.Empty;
            if (index < 0 || index >= (int)m_Params.Count)
                return string.Empty;
            return m_Params[index].GetId();
        }
        public void ClearParams()
        {
            PrepareParams();
            m_Params.Clear();
        }
        public void AddParam(string id)
        {
            PrepareParams();
            m_Params.Add(new ValueData(id));
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void AddParam(string id, int type)
        {
            PrepareParams();
            m_Params.Add(new ValueData(id, type));
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void AddParam(ValueData param)
        {
            PrepareParams();
            m_Params.Add(param);
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void AddParam(FunctionData param)
        {
            PrepareParams();
            m_Params.Add(param);
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void AddParam(StatementData param)
        {
            PrepareParams();
            m_Params.Add(param);
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void AddParam(ISyntaxComponent param)
        {
            PrepareParams();
            m_Params.Add(param);
            if ((int)ParamClassEnum.PARAM_CLASS_NOTHING == m_ParamClass) {
                m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_PARENTHESIS;
            }
        }
        public void Clear()
        {
            m_Name = null;
            m_LowerOrderFunction = null;
            m_IsHighOrder = false;
            m_Params = null;
            m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_NOTHING;

            m_CommentsInfo = null;
        }
        public void CopyFrom(FunctionData other)
        {
            m_IsHighOrder = other.m_IsHighOrder;
            m_Name = other.m_Name;
            m_LowerOrderFunction = other.m_LowerOrderFunction;
            m_Params = other.m_Params;
            m_ParamClass = other.m_ParamClass;

            if (!DslFile.DontLoadComments) {
                if (null != other.m_CommentsInfo && null != other.m_CommentsInfo.m_Comments) {
                    Comments.AddRange(other.m_CommentsInfo.m_Comments);
                }
                CopyComments(other);
            }
        }
        private void PrepareParams()
        {
            if (null == m_Params) {
                m_Params = new List<ISyntaxComponent>();
            }
        }
        internal override SyntaxComponentCommentsInfo GetCommentsInfo()
        {
            return GetFunctionCommentsInfo();
        }
        private FunctionCommentsInfo GetFunctionCommentsInfo()
        {
            if (DslFile.DontLoadComments)
                return null;
            if (null == m_CommentsInfo) {
                m_CommentsInfo = new FunctionCommentsInfo();
            }
            return m_CommentsInfo;
        }

        private bool m_IsHighOrder = false;
        private ValueData m_Name = null;
        private FunctionData m_LowerOrderFunction = null;
        private List<ISyntaxComponent> m_Params = null;
        private int m_ParamClass = (int)ParamClassEnum.PARAM_CLASS_NOTHING;

        private FunctionCommentsInfo m_CommentsInfo = null;

        public static FunctionData NullFunction
        {
            get {
                s_Instance.Clear();
                return s_Instance;
            }
        }
        private static FunctionData s_Instance = new FunctionData();
    }
    /// <summary>
    /// 语句数据，由多个函数项连接而成。
    /// </summary>
    /// <remarks>
    /// 备忘：为什么StatementData的成员不使用ISyntaxComponent[]而是FunctionData[]
    /// 1、虽然语法上这里的FunctionData可以退化为ValueData，但不可以是StatementData，这样在概念上不能与ISyntaxComponent等同
    /// 2、在设计上，FunctionData应该考虑到退化情形，尽量在退化情形不占用额外空间
    /// </remarks>
    public class StatementData : AbstractSyntaxComponent
    {
        public override bool IsValid()
        {
            bool ret = true;
            if (m_Functions.Count <= 0) {
                ret = false;
            }
            else {
                for (int i = 0; i < m_Functions.Count; ++i) {
                    ret = ret && m_Functions[i].IsValid();
                }
            }
            return ret;
        }
        public override string GetId()
        {
            if (m_Functions.Count <= 0)
                return string.Empty;
            else
                return m_Functions[0].GetId();
        }
        public override int GetIdType()
        {
            if (m_Functions.Count <= 0)
                return ID_TOKEN;
            else
                return m_Functions[0].GetIdType();
        }
        public override int GetLine()
        {
            if (m_Functions.Count <= 0)
                return -1;
            else
                return m_Functions[0].GetLine();
        }
        public override string ToScriptString(bool includeComment)
        {
#if FULL_VERSION
            //与write方法不同，这里输出无缩进单行表示
            FunctionData tempData = First;
            FunctionData callData = null;
            callData = tempData.LowerOrderFunction;
            if (null != callData && tempData.GetParamClass() == (int)FunctionData.ParamClassEnum.PARAM_CLASS_TERNARY_OPERATOR) {
                //三目表达式表示为op1(cond)(true_val)op2(false_val)
                if (callData.HaveId() && callData.HaveParamOrStatement()) {
                    string line = "/*[?:]*/";
                    if (Functions.Count == 2) {
                        FunctionData funcData = Functions[1];
                        if (funcData.HaveId() && funcData.HaveParamOrStatement()) {
                            line = string.Format("{0} {1} {2}", callData.GetParam(0).ToScriptString(includeComment), callData.GetId(), tempData.GetParam(0).ToScriptString(includeComment));
                            line = string.Format("{0} {1} {2}", line, funcData.GetId(), funcData.GetParam(0).ToScriptString(includeComment));
                        }
                    }
                    if (includeComment) {
                        return CalcFirstComment() + line + CalcLastComment();
                    }
                    else {
                        return line;
                    }
                }
                else {
                    if (includeComment) {
                        return CalcFirstComment() + CalcLastComment();
                    }
                    else {
                        return string.Empty;
                    }
                }
            }
            else {
                StringBuilder stream = new StringBuilder();
                if (includeComment) {
                    stream.Append(CalcFirstComment());
                }
                int ct = Functions.Count;
                for (int i = 0; i < ct; ++i) {
                    FunctionData funcData = Functions[i];
                    stream.Append(funcData.ToScriptString(includeComment));
                }
                if (includeComment) {
                    stream.Append(CalcLastComment());
                }
                return stream.ToString();
            }
#else
      return ToString();
#endif
        }

        public int GetFunctionNum()
        {
            return m_Functions.Count;
        }
        public void SetFunction(int index, FunctionData funcData)
        {
            if (index < 0 || index >= m_Functions.Count)
                return;
            m_Functions[index] = funcData;
        }
        public FunctionData GetFunction(int index)
        {
            if (index < 0 || index >= m_Functions.Count)
                return FunctionData.NullFunction;
            return m_Functions[index];
        }
        public string GetFunctionId(int index)
        {
            if (index < 0 || index >= m_Functions.Count)
                return string.Empty;
            return m_Functions[index].GetId();
        }
        public void AddFunction(FunctionData funcData)
        {
            m_Functions.Add(funcData);
        }
        public List<FunctionData> Functions
        {
            get { return m_Functions; }
        }
        public FunctionData First
        {
            get {
                if (m_Functions.Count > 0)
                    return m_Functions[0];
                else
                    return FunctionData.NullFunction;
            }
        }
        public FunctionData Second
        {
            get {
                if (m_Functions.Count > 1)
                    return m_Functions[1];
                else
                    return FunctionData.NullFunction;
            }
        }
        public FunctionData Third
        {
            get {
                if (m_Functions.Count > 2)
                    return m_Functions[2];
                else
                    return FunctionData.NullFunction;
            }
        }
        public FunctionData Last
        {
            get {
                if (m_Functions.Count > 0)
                    return m_Functions[m_Functions.Count - 1];
                else
                    return FunctionData.NullFunction;
            }
        }
        public void Clear()
        {
            m_Functions = new List<FunctionData>();

            m_CommentsInfo = null;
        }
        public void CopyFrom(StatementData other)
        {
            m_Functions = other.m_Functions;

            if (!DslFile.DontLoadComments) {
                CopyComments(other);
            }
        }
        internal override SyntaxComponentCommentsInfo GetCommentsInfo()
        {
            if (DslFile.DontLoadComments)
                return null;
            if (null == m_CommentsInfo) {
                m_CommentsInfo = new SyntaxComponentCommentsInfo();
            }
            return m_CommentsInfo;
        }

        private List<FunctionData> m_Functions = new List<FunctionData>();
        private SyntaxComponentCommentsInfo m_CommentsInfo = null;

        public static StatementData NullStatementData
        {
            get {
                s_NullStatementData.Clear();
                return s_NullStatementData;
            }
        }
        private static StatementData s_NullStatementData = new StatementData();
    }

    public class DslFile
    {
        public List<ISyntaxComponent> DslInfos
        {
            get { return mDslInfos; }
        }
        public void AddDslInfo(ISyntaxComponent data)
        {
            mDslInfos.Add(data);
        }

        public bool Load(string file, DslLogDelegation logCallback)
        {
            string content = File.ReadAllText(file);
            //logCallback(string.Format("DslFile.Load {0}:\n{1}", file, content));
            return LoadFromString(content, file, logCallback);
        }
        public bool LoadFromString(string content, string resourceName, DslLogDelegation logCallback)
        {
            mDslInfos.Clear();
            Common.DslLog log = new Common.DslLog();
            log.OnLog += logCallback;
            Parser.DslToken tokens = new Parser.DslToken(log, content);
            Parser.DslError error = new Parser.DslError(log);
            Common.DslAction action = new Common.DslAction(log, mDslInfos);
            action.onGetLastToken = () => { return tokens.getLastToken(); };
            action.onGetLastLineNumber = () => { return tokens.getLastLineNumber(); };
            action.onGetComment = (out bool commentOnNewLine) => { commentOnNewLine = tokens.IsCommentOnNewLine(); List<string> ret = new List<string>(); ret.AddRange(tokens.GetComments()); tokens.ResetComments(); return ret; };
            action.onSetStringDelimiter = (string begin, string end) => { tokens.setStringDelimiter(begin, end); };
            action.onSetScriptDelimiter = (string begin, string end) => { tokens.setScriptDelimiter(begin, end); };

            Parser.DslParser.parse(ref action, ref tokens, ref error, 0);
            if (error.HasError) {
                for (int i = 0; i < mDslInfos.Count; i++) {
                    mDslInfos.Clear();
                }
            }
            return !error.HasError;
        }
        public void Save(string file)
        {
#if FULL_VERSION
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mDslInfos.Count; i++) {
                Utility.writeSyntaxComponent(sb, mDslInfos[i], 0, true, true);
            }
            File.WriteAllText(file, sb.ToString());
#endif
        }
        public string ToScriptString()
        {
#if FULL_VERSION
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < mDslInfos.Count; i++) {
                Utility.writeSyntaxComponent(sb, mDslInfos[i], 0, true, false);
            }
            return sb.ToString();
#else
            return ToString();
#endif
        }

        public void LoadBinaryFile(string file)
        {
            var code = File.ReadAllBytes(file);
            LoadBinaryCode(code);
        }
        public void LoadBinaryCode(byte[] binaryCode)
        {
            mDslInfos.Clear();
            if (null == binaryCode)
                return;
            int pos = c_BinaryIdentity.Length;
            int bytesLen = ReadInt(binaryCode, pos);
            pos += 4;
            int bytes2Len = ReadInt(binaryCode, pos);
            pos += 4;
            int keyCount = ReadInt(binaryCode, pos);
            pos += 4;
            int bytesStart = pos;
            int bytes2Start = bytesStart + bytesLen;
            int keyStart = bytes2Start + bytes2Len;
            List<string> keys = new List<string>();
            pos = keyStart;
            for (int i = 0; i < keyCount; ++i) {
                int byteCount;
                int len = Read7BitEncodedInt(binaryCode, pos, out byteCount);
                if (len >= 0) {
                    pos += byteCount;
                    var key = Encoding.UTF8.GetString(binaryCode, pos, len);
                    keys.Add(key);
                    pos += len;
                }
                else {
                    break;
                }
            }
            List<string> identifiers = new List<string>();
            for (int i = bytes2Start; i < bytes2Start + bytes2Len && i < binaryCode.Length; ++i) {
                int ix;
                byte first = binaryCode[i];
                if ((first & 0x80) == 0x80) {
                    ++i;
                    byte second = binaryCode[i];
                    ix = (int)(((int)first & 0x0000007f) | ((int)second << 7));
                }
                else {
                    ix = first;
                }
                if (ix >= 0 && ix < keys.Count) {
                    identifiers.Add(keys[ix]);
                }
                else {
                    identifiers.Add(string.Empty);
                }
            }
            List<ISyntaxComponent> infos = Utility.readBinary(binaryCode, bytesStart, bytesLen, identifiers);
            mDslInfos.AddRange(infos);
        }
        public void SaveBinaryFile(string file)
        {
#if FULL_VERSION
            MemoryStream stream = new MemoryStream();
            List<string> identifiers = new List<string>();
            foreach (ISyntaxComponent info in DslInfos) {
                Utility.writeBinary(stream, identifiers, info);
            }

            if (null == mStringComparer) {
                mStringComparer = new MyStringComparer();
            }
            byte[] bytes = stream.ToArray();
            SortedDictionary<string, int> dict = new SortedDictionary<string, int>(mStringComparer);
            int ct = identifiers.Count;
            if (ct > 0x00004000) {
                System.Diagnostics.Debug.Assert(false);
                //Console.WriteLine("Identifiers count {0} too large than 0x04000", ct);
                return;
            }
            for (int i = 0; i < ct; ++i) {
                string key = identifiers[i];
                if (!dict.ContainsKey(key)) {
                    dict.Add(key, 0);
                }
            }
            List<string> keys = new List<string>(dict.Keys);
            byte[] bytes2;
            using (MemoryStream ms = new MemoryStream()) {
                for (int i = 0; i < ct; ++i) {
                    string key = identifiers[i];
                    int ix = keys.BinarySearch(key, mStringComparer);
                    if (ix < 0x80) {
                        ms.WriteByte((byte)ix);
                    }
                    else {
                        ms.WriteByte((byte)((ix & 0x0000007f) | 0x00000080));
                        ms.WriteByte((byte)(ix >> 7));
                    }
                }
                bytes2 = ms.ToArray();
            }
            using (MemoryStream bdsl = new MemoryStream()) {
                bdsl.Write(BinaryIdentity, 0, c_BinaryIdentity.Length);
                WriteInt(bdsl, bytes.Length);
                WriteInt(bdsl, bytes2.Length);
                WriteInt(bdsl, keys.Count);
                bdsl.Write(bytes, 0, bytes.Length);
                bdsl.Write(bytes2, 0, bytes2.Length);
                foreach (var str in keys) {
                    var bstr = Encoding.UTF8.GetBytes(str);
                    Write7BitEncodedInt(bdsl, bstr.Length);
                    bdsl.Write(bstr, 0, bstr.Length);
                }
                using (FileStream fs = new FileStream(file, FileMode.Create)) {
                    fs.Write(bdsl.GetBuffer(), 0, (int)bdsl.Length);
                    fs.Close();
                }
            }
#endif
        }

        public bool LoadLua(string file, DslLogDelegation logCallback)
        {
            string content = File.ReadAllText(file);
            //logCallback(string.Format("DslFile.Load {0}:\n{1}", file, content));
            return LoadLuaFromString(content, file, logCallback);
        }
        public bool LoadLuaFromString(string content, string resourceName, DslLogDelegation logCallback)
        {
            mDslInfos.Clear();
            Common.DslLog log = new Common.DslLog();
            log.OnLog += logCallback;
            Lua.Parser.LuaToken tokens = new Lua.Parser.LuaToken(log, content);
            Lua.Parser.LuaError error = new Lua.Parser.LuaError(log);
            Common.DslAction action = new Common.DslAction(log, mDslInfos);
            action.Type = Common.DslActionType.Lua;
            action.onGetLastToken = () => { return tokens.getLastToken(); };
            action.onGetLastLineNumber = () => { return tokens.getLastLineNumber(); };
            action.onGetComment = (out bool commentOnNewLine) => { commentOnNewLine = tokens.IsCommentOnNewLine(); List<string> ret = new List<string>(); ret.AddRange(tokens.GetComments()); tokens.ResetComments(); return ret; };
            
            Lua.Parser.LuaParser.parse(ref action, ref tokens, ref error, 0);
            if (error.HasError) {
                for (int i = 0; i < mDslInfos.Count; i++) {
                    mDslInfos.Clear();
                }
            }
            return !error.HasError;
        }

        public bool LoadCpp(string file, DslLogDelegation logCallback)
        {
            string content = File.ReadAllText(file);
            //logCallback(string.Format("DslFile.Load {0}:\n{1}", file, content));
            return LoadCppFromString(content, file, logCallback);
        }
        public bool LoadCppFromString(string content, string resourceName, DslLogDelegation logCallback)
        {
            mDslInfos.Clear();
            Common.DslLog log = new Common.DslLog();
            log.OnLog += logCallback;
            Cpp.Parser.CppToken tokens = new Cpp.Parser.CppToken(log, content);
            Cpp.Parser.CppError error = new Cpp.Parser.CppError(log);
            Common.DslAction action = new Common.DslAction(log, mDslInfos);
            action.Type = Common.DslActionType.Cpp;
            action.onGetLastToken = () => { return tokens.getLastToken(); };
            action.onGetLastLineNumber = () => { return tokens.getLastLineNumber(); };
            action.onGetComment = (out bool commentOnNewLine) => { commentOnNewLine = tokens.IsCommentOnNewLine(); List<string> ret = new List<string>(); ret.AddRange(tokens.GetComments()); tokens.ResetComments(); return ret; };

            Cpp.Parser.CppParser.parse(ref action, ref tokens, ref error, 0);
            if (error.HasError) {
                for (int i = 0; i < mDslInfos.Count; i++) {
                    mDslInfos.Clear();
                }
            }
            return !error.HasError;
        }

        private void WriteInt(Stream s, int val)
        {
            if (null == mBuffer) {
                mBuffer = new byte[4];
            }
            mBuffer[0] = (byte)val;
            mBuffer[1] = (byte)(val >> 8);
            mBuffer[2] = (byte)(val >> 16);
            mBuffer[3] = (byte)(val >> 24);
            s.Write(mBuffer, 0, 4);
        }
        private void Write7BitEncodedInt(Stream s, int val)
        {
            uint num;
            for (num = (uint)val; num >= 128; num >>= 7) {
                s.WriteByte((byte)(num | 0x80));
            }
            s.WriteByte((byte)num);
        }
        private int ReadInt(byte[] bytes, int pos)
        {
            if (null != bytes && pos >= 0 && pos + 3 < bytes.Length) {
                return bytes[pos] | (bytes[pos + 1] << 8) | (bytes[pos + 2] << 16) | (bytes[pos + 3] << 24);
            }
            else {
                return -1;
            }
        }
        private int Read7BitEncodedInt(byte[] bytes, int pos, out int byteCount)
        {
            int num = -1;
            byteCount = 0;
            if (null != bytes && pos < bytes.Length) {
                int bitCount = 0;
                byte b;
                num = 0;
                do {
                    if (bitCount == 35) {
                        num = -1;
                        break;
                    }
                    b = bytes[pos++];
                    num |= (b & 0x7F) << bitCount;
                    bitCount += 7;
                }
                while (pos < bytes.Length && (b & 0x80) != 0);
                byteCount = bitCount / 7;
            }
            return num;
        }

        private class MyStringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (object.ReferenceEquals(x, y)) {
                    return 0;
                }
                if (x == null) {
                    return -1;
                }
                if (y == null) {
                    return 1;
                }

                if (x.Length < y.Length)
                    return -1;
                else if (x.Length > y.Length)
                    return 1;
                else
                    return string.CompareOrdinal(x, y);
            }
        }

        private byte[] mBuffer = null;
        private MyStringComparer mStringComparer = null;
        private List<ISyntaxComponent> mDslInfos = new List<ISyntaxComponent>();

        public static byte[] BinaryIdentity
        {
            get {
                if (null == sBinaryIdentity) {
                    sBinaryIdentity = Encoding.ASCII.GetBytes(c_BinaryIdentity);
                }
                return sBinaryIdentity;
            }
        }
        public static bool DontLoadComments
        {
            get {
                return sDontLoadComments;
            }
            set {
                sDontLoadComments = value;
            }
        }
        public static bool IsBinaryDsl(byte[] data, int start)
        {
            if (null == data || data.Length < c_BinaryIdentity.Length)
                return false;
            bool r = true;
            for (int i = 0; i < BinaryIdentity.Length && start + i < data.Length; ++i) {
                if (BinaryIdentity[i] != data[start + i]) {
                    r = false;
                    break;
                }
            }
            return r;
        }

        public const string c_BinaryIdentity = "BDSL";

        private static byte[] sBinaryIdentity = null;
        private static bool sDontLoadComments = false;
    };

    public sealed class Utility
    {
        public static void writeSyntaxComponent(StringBuilder stream, ISyntaxComponent data, int indent, bool firstLineNoIndent, bool isLastOfStatement)
        {
#if FULL_VERSION
            ValueData val = data as ValueData;
            if (null != val) {
                writeValueData(stream, val, indent, firstLineNoIndent, isLastOfStatement);
            }
            else {
                FunctionData call = data as FunctionData;
                if (null != call) {
                    writeFunctionData(stream, call, indent, firstLineNoIndent, isLastOfStatement);
                }
                else {
                    StatementData statement = data as StatementData;
                    writeStatementData(stream, statement, indent, firstLineNoIndent, isLastOfStatement);
                }
            }
#endif
        }

        public static void writeValueData(StringBuilder stream, ValueData data, int indent, bool firstLineNoIndent, bool isLastOfStatement)
        {
#if FULL_VERSION
            writeFirstComments(stream, data, indent, firstLineNoIndent);
            writeText(stream, data.ToScriptString(false), firstLineNoIndent ? 0 : indent);
            if (isLastOfStatement)
                stream.Append(';');
            writeLastComments(stream, data, indent, isLastOfStatement);
#endif
        }

        public static void writeFunctionData(StringBuilder stream, FunctionData data, int indent, bool firstLineNoIndent, bool isLastOfStatement)
        {
#if FULL_VERSION
            //string lineNo = string.Format("/* {0} */", data.GetLine());
            //writeLine(stream, lineNo, indent);
            writeFirstComments(stream, data, indent, firstLineNoIndent);
            if (data.HaveParamOrStatement()) {
                int paramClass = (data.GetParamClass() & (int)FunctionData.ParamClassEnum.PARAM_CLASS_UNMASK);
                if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_OPERATOR == paramClass) {
                    int infix = (data.GetParamClass() & (int)FunctionData.ParamClassEnum.PARAM_CLASS_WRAP_INFIX_CALL_MASK);
                    int paramNum = data.GetParamNum();
                    if (paramNum == 1) {
                        writeText(stream, " ", 0);
                        if (data.IsHighOrder) {
                            writeFunctionData(stream, data.LowerOrderFunction, indent, paramNum > 0 ? true : firstLineNoIndent, false);
                        }
                        else if (data.HaveId()) {
                            string op = data.GetId();
                            if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_WRAP_INFIX_CALL_MASK == infix)
                                op = "`" + op;
                            string line = quoteString(op, data.GetIdType());
                            writeText(stream, line, paramNum > 0 ? 0 : (firstLineNoIndent ? 0 : indent));
                        }
                        writeText(stream, " ", 0);
                        writeSyntaxComponent(stream, data.GetParam(0), indent, firstLineNoIndent, false);
                    }
                    else {
                        if (paramNum > 0) {
                            writeSyntaxComponent(stream, data.GetParam(0), indent, firstLineNoIndent, false);
                            writeText(stream, " ", 0);
                        }
                        if (data.IsHighOrder) {
                            writeFunctionData(stream, data.LowerOrderFunction, indent, paramNum > 0 ? true : firstLineNoIndent, false);
                        }
                        else if (data.HaveId()) {
                            string op = data.GetId();
                            if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_WRAP_INFIX_CALL_MASK == infix)
                                op = "`" + op;
                            string line = quoteString(op, data.GetIdType());
                            writeText(stream, line, paramNum > 0 ? 0 : (firstLineNoIndent ? 0 : indent));
                        }
                        if (paramNum > 1) {
                            writeText(stream, " ", 0);
                            writeSyntaxComponent(stream, data.GetParam(1), indent, true, false);
                        }
                    }
                }
                else {
                    if (data.IsHighOrder) {
                        writeFunctionData(stream, data.LowerOrderFunction, indent, firstLineNoIndent, false);
                    }
                    else if (data.HaveId()) {
                        string line = quoteString(data.GetId(), data.GetIdType());
                        writeText(stream, line, firstLineNoIndent ? 0 : indent);
                    }
                    else {
                        writeText(stream, string.Empty, firstLineNoIndent ? 0 : indent);
                    }
                    if (data.HaveStatement() || data.HaveExternScript()) {
                        if (data.IsHighOrder) {
                            writeLastComments(stream, data.LowerOrderFunction, indent, isLastOfStatement);
                        }
                        else if (data.HaveId()) {
                            writeLastComments(stream, data.Name, indent, isLastOfStatement);
                        }
                        writeStatementsOrExternScript(stream, data, indent);
                    }
                    else {
                        string lbracket = string.Empty;
                        string rbracket = string.Empty;
                        switch (paramClass) {
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS:
                                lbracket = "(";
                                rbracket = ")";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET:
                                lbracket = "[";
                                rbracket = "]";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD:
                                lbracket = ".";
                                rbracket = string.Empty;
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_PARENTHESIS:
                                lbracket = ".(";
                                rbracket = ")";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACKET:
                                lbracket = ".[";
                                rbracket = "]";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACE:
                                lbracket = ".{";
                                rbracket = "}";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD:
                                lbracket = "?.";
                                rbracket = string.Empty;
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PARENTHESIS:
                                lbracket = "?(";
                                rbracket = ")";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_BRACKET:
                                lbracket = "?[";
                                rbracket = "]";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_BRACE:
                                lbracket = "?{";
                                rbracket = "}";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER:
                                lbracket = "->";
                                rbracket = string.Empty;
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET_ATTR:
                                lbracket = "[:";
                                rbracket = ":]";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS_ATTR:
                                lbracket = "(:";
                                rbracket = ":)";
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_STAR:
                                lbracket = ".*";
                                rbracket = string.Empty;
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD_STAR:
                                lbracket = "?.*";
                                rbracket = string.Empty;
                                break;
                            case (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER_STAR:
                                lbracket = "->*";
                                rbracket = string.Empty;
                                break;
                        }
                        stream.Append(lbracket);
                        int ct = data.GetParamNum();
                        for (int i = 0; i < ct; ++i) {
                            if (i > 0)
                                stream.Append(",");
                            ISyntaxComponent param = data.GetParam(i);
                            if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_STAR == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD_STAR == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER_STAR == paramClass)
                                stream.Append(param.ToScriptString(true));
                            else
                                writeSyntaxComponent(stream, param, indent, true, false);
                        }
                        stream.Append(rbracket);
                    }
                }
            }
            else {
                if (data.IsHighOrder) {
                    writeFunctionData(stream, data.LowerOrderFunction, indent, firstLineNoIndent, false);
                }
                else if (data.HaveId()) {
                    string line = quoteString(data.GetId(), data.GetIdType());
                    writeText(stream, line, firstLineNoIndent ? 0 : indent);
                }
            }
            if (isLastOfStatement)
                stream.Append(';');
            foreach (string cmt in data.Comments) {
                writeText(stream, cmt, 1);
            }
            writeLastComments(stream, data, indent, isLastOfStatement);
#endif
        }

        public static void writeStatementData(StringBuilder stream, StatementData data, int indent, bool firstLineNoIndent, bool isLastOfStatement)
        {
#if FULL_VERSION
            writeFirstComments(stream, data, indent, firstLineNoIndent);
            FunctionData tempData = data.First;
            FunctionData callData = tempData.LowerOrderFunction;
            if (null != callData && tempData.GetParamClass() == (int)FunctionData.ParamClassEnum.PARAM_CLASS_TERNARY_OPERATOR) {
                //三目运算符表示为op1(cond)(true_val)op2(false_val)
                if (callData.HaveId() && callData.HaveParamOrStatement()) {
                    string line = "/*[?:]*/";                    
                    if (data.Functions.Count == 2) {
                        FunctionData funcData = data.Functions[1];
                        if (funcData.HaveId() && funcData.HaveParamOrStatement()) {
                            line = string.Format("{0} {1} {2}", callData.GetParam(0).ToScriptString(true), callData.GetId(), tempData.GetParam(0).ToScriptString(true));
                            line = string.Format("{0} {1} {2}", line, funcData.GetId(), funcData.GetParam(0).ToScriptString(true));
                        }
                    }
                    writeText(stream, line, firstLineNoIndent ? 0 : indent);
                }
            }
            else {
                int ct = data.Functions.Count;
                bool lastFuncNoParam = false;
                bool lastFuncNoStatement = false;
                for (int i = 0; i < ct; ++i) {
                    FunctionData func = data.Functions[i];
                    bool noIndent = false;
                    bool funcNoParam = !func.IsHighOrder && !func.HaveParam();
                    bool funcNoStatement = !func.HaveStatement() && !func.HaveExternScript();
                    if (i > 0) {
                        if (lastFuncNoParam && lastFuncNoStatement) {
                            writeText(stream, " ", 0);
                            noIndent = true;
                        }
                        else if (lastFuncNoStatement && funcNoStatement) {
                            noIndent = true;
                        }
                        else {
                            writeLine(stream, string.Empty, 0);
                            noIndent = false;
                        }
                    }
                    writeFunctionData(stream, func, indent, firstLineNoIndent && i == 0 || noIndent, false);
                    lastFuncNoParam = funcNoParam;
                    lastFuncNoStatement = funcNoStatement;
                }
            }
            if (isLastOfStatement)
                stream.Append(';');
            writeLastComments(stream, data, indent, isLastOfStatement);
#endif
        }
        
        internal static bool needQuote(string str)
        {
            const string escapeChars = " \t\r\n{}()[],;~`!%^&*-+=|:<>?/#\\'\"";
            if (str.Length == 0) {
                return true;
            }
            bool haveDot = false;
            bool notNum = false;
            for (int i = 0; i < str.Length; ++i) {
                char c = str[i];
                if (escapeChars.IndexOf(c) >= 0) {
                    return true;
                }
                if (c == '.') {
                    haveDot = true;
                }
                else if (!notNum && !char.IsDigit(c)) {
                    notNum = true;
                }
                if (haveDot && notNum) {
                    return true;
                }
            }
            return false;
        }

        internal static string quoteString(string str, int _Type)
        {
            switch (_Type) {
                case AbstractSyntaxComponent.STRING_TOKEN: {
                        if (str.Contains("\\"))
                            str = str.Replace("\\", "\\\\");
                        if (str.Contains("\""))
                            str = str.Replace("\"", "\\\"");
                        if (str.Contains("\0"))
                            str = str.Replace("\0", "\\0");
                        return "\"" + str + "\"";
                    }
                case AbstractSyntaxComponent.NUM_TOKEN:
                case AbstractSyntaxComponent.ID_TOKEN:
                    return str;
                default:
                    return str;
            }
        }

        internal static string getFunctionString(FunctionData data, bool includeComment)
        {
#if FULL_VERSION
            string lineNo = string.Empty;// string.Format("/* {0} */", data.GetLine());
            string line = string.Empty;
            if (data.IsHighOrder) {
                line = getFunctionString(data.LowerOrderFunction, includeComment);
            }
            else if (data.HaveId()) {
                int infix = (data.GetParamClass() & (int)FunctionData.ParamClassEnum.PARAM_CLASS_WRAP_INFIX_CALL_MASK);
                string op = data.GetId();
                if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_WRAP_INFIX_CALL_MASK == infix)
                    op = "`" + op;
                line = quoteString(op, data.GetIdType());
            }
            if (data.HaveParamOrStatement()) {
                int paramClass = (data.GetParamClass() & (int)FunctionData.ParamClassEnum.PARAM_CLASS_UNMASK);
                if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_OPERATOR == paramClass) {
                    switch (data.GetParamNum()) {
                        case 1:
                            return string.Format("{0} {1}", line, data.GetParam(0).ToScriptString(includeComment));
                        case 2:
                            return string.Format("{0} {1} {2}", data.GetParam(0).ToScriptString(includeComment), line, data.GetParam(1).ToScriptString(includeComment));
                        default:
                            return line;
                    }
                }
                else {
                    string lbracket = string.Empty;
                    string rbracket = string.Empty;
                    switch (paramClass) {
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS:
                            lbracket = "(";
                            rbracket = ")";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET:
                            lbracket = "[";
                            rbracket = "]";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD:
                            lbracket = ".";
                            rbracket = string.Empty;
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_PARENTHESIS:
                            lbracket = ".(";
                            rbracket = ")";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACKET:
                            lbracket = ".[";
                            rbracket = "]";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_BRACE:
                            lbracket = ".{";
                            rbracket = "}";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD:
                            lbracket = "?.";
                            rbracket = string.Empty;
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PARENTHESIS:
                            lbracket = "?(";
                            rbracket = ")";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_BRACKET:
                            lbracket = "?[";
                            rbracket = "]";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_BRACE:
                            lbracket = "?{";
                            rbracket = "}";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER:
                            lbracket = "->";
                            rbracket = string.Empty;
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_STATEMENT:
                            lbracket = "{";
                            rbracket = "}";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_EXTERN_SCRIPT:
                            lbracket = "{:";
                            rbracket = ":}";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_BRACKET_ATTR:
                            lbracket = "[:";
                            rbracket = ":]";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PARENTHESIS_ATTR:
                            lbracket = "(:";
                            rbracket = ":)";
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_STAR:
                            lbracket = ".*";
                            rbracket = string.Empty;
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD_STAR:
                            lbracket = "?.*";
                            rbracket = string.Empty;
                            break;
                        case (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER_STAR:
                            lbracket = "->*";
                            rbracket = string.Empty;
                            break;
                    }
                    StringBuilder stream = new StringBuilder();
                    stream.Append(lbracket);
                    if (paramClass == (int)FunctionData.ParamClassEnum.PARAM_CLASS_EXTERN_SCRIPT) {
                        stream.Append(data.GetParamId(0));
                    }
                    else {
                        int ct = data.GetParamNum();
                        for (int i = 0; i < ct; ++i) {
                            if (data.HaveParam() && i > 0) {
                                stream.Append(",");
                            }
                            ISyntaxComponent param = data.GetParam(i);
                            if ((int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_PERIOD_STAR == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_QUESTION_PERIOD_STAR == paramClass
                                 || (int)FunctionData.ParamClassEnum.PARAM_CLASS_POINTER_STAR == paramClass)
                                stream.Append(param.ToScriptString(includeComment));
                            else
                                stream.Append(param.ToScriptString(includeComment));
                            if (data.HaveStatement()) {
                                stream.Append(";");
                            }
                        }
                    }
                    stream.Append(rbracket);
                    return string.Format("{0}{1}{2}", lineNo, line, stream.ToString());
                }
            }
            else {
                return string.Format("{0}{1}", lineNo, line);
            }
#else
      return string.Empty;
#endif
        }
        
        private static void writeStatementsOrExternScript(StringBuilder stream, FunctionData data, int indent)
        {
#if FULL_VERSION
            if (data.HaveStatement()) {
                writeLine(stream, string.Empty, 0);
                writeLine(stream, "{", indent);
                ++indent;

                int ct = data.GetParamNum();
                for (int i = 0; i < ct; ++i) {
                    ISyntaxComponent tempData = data.GetParam(i);
                    writeSyntaxComponent(stream, tempData, indent, false, true);
                }

                --indent;
                writeText(stream, "}", indent);
            }
            else if (data.HaveExternScript()) {
                writeLine(stream, string.Empty, 0);
                string script = data.GetParamId(0);
                if (script.IndexOf('\n') >= 0) {
                    writeLine(stream, "{:", indent);
                }
                else {
                    writeText(stream, "{:", indent);
                }
                stream.Append(script);
                if (script.Length > 0 && script[script.Length - 1] == '\n') {
                    writeText(stream, ":}", indent);
                }
                else {
                    stream.Append(":}");
                }
            }
#endif
        }
        
        private static void writeFirstComments(StringBuilder stream, ISyntaxComponent data, int indent, bool firstLineNoIndent)
        {
#if FULL_VERSION
            AbstractSyntaxComponent syntaxComp = data as AbstractSyntaxComponent;
            bool isFirst = true;
            bool haveComments = false;
            bool newLine = false;
            foreach (string cmt in syntaxComp.FirstComments) {
                if (isFirst && !syntaxComp.FirstCommentOnNewLine) {
                    writeText(stream, cmt, firstLineNoIndent ? 0 : indent);
                }
                else {
                    writeLine(stream, cmt, isFirst && firstLineNoIndent ? 0 : indent);
                    newLine = true;
                }
                isFirst = false;
                haveComments = true;
            }
            if (haveComments && !newLine) {
                //行首注释必须要换行，否则可能会把代码注释掉
                writeLine(stream, string.Empty, 0);
            }
#endif
        }

        private static void writeLastComments(StringBuilder stream, ISyntaxComponent data, int indent, bool isLastOfStatement)
        {
#if FULL_VERSION
            AbstractSyntaxComponent syntaxComp = data as AbstractSyntaxComponent;
            bool isFirst = true;
            if (syntaxComp.LastComments.Count > 0) {
                if (syntaxComp.LastCommentOnNewLine) {
                    writeLine(stream, string.Empty, 0);
                }
                isFirst = true;
                foreach (string cmt in syntaxComp.LastComments) {
                    if (isFirst && !syntaxComp.LastCommentOnNewLine) {
                        writeText(stream, cmt, 1);
                    }
                    else {
                        writeText(stream, cmt, indent);
                    }
                    if (isLastOfStatement) {
                        writeLine(stream, string.Empty, 0);
                    }
                    isFirst = false;
                }
            }
            else if (isLastOfStatement) {
                writeLine(stream, string.Empty, 0);
            }
#endif
        }

        private static void writeText(StringBuilder stream, string line, int indent)
        {
#if FULL_VERSION
            for (int i = 0; i < indent; ++i) {
                stream.Append('\t');
            }
            stream.Append(line);
#endif
        }

        private static void writeLine(StringBuilder stream, string line, int indent)
        {
#if FULL_VERSION
            writeText(stream, line, indent);
            stream.Append("\r\n");
#endif
        }

        private static byte readByte(byte[] bytes, int curCodeIndex)
        {
            if (curCodeIndex < bytes.Length)
                return bytes[curCodeIndex];
            else
                return 0;
        }
        private static string readIdentifier(List<string> identifiers, int curIdIndex)
        {
            if (curIdIndex < identifiers.Count)
                return identifiers[curIdIndex];
            else
                return string.Empty;
        }
        internal static List<ISyntaxComponent> readBinary(byte[] bytes, int start, int count, List<string> identifiers)
        {
            List<ISyntaxComponent> infos = new List<ISyntaxComponent>();
            int curCodeIndex = 0;
            int curIdIndex = 0;
            while (curCodeIndex < count) {
                while (curCodeIndex < count) {
                    byte b = bytes[start + curCodeIndex];
                    if (b == (byte)DslBinaryCode.BeginStatement || b == (byte)DslBinaryCode.BeginFunction || b == (byte)DslBinaryCode.BeginValue)
                        break;
                    ++curCodeIndex;
                }
                if (curCodeIndex < count) {
                    ISyntaxComponent info = readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex);
                    if (null != info && info.IsValid()) {
                        infos.Add(info);
                    }
                }
            }
            return infos;
        }
        internal static ISyntaxComponent readBinary(byte[] bytes, int start, ref int curCodeIndex, List<string> identifiers, ref int curIdIndex)
        {
            ISyntaxComponent ret = null;
            byte code = readByte(bytes, start + curCodeIndex);
            if (code == (byte)DslBinaryCode.BeginValue) {
                ValueData data = new ValueData();
                readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, data);
                ret = data;
            }
            else if (code == (byte)DslBinaryCode.BeginFunction) {
                FunctionData data = new FunctionData();
                readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, data);
                ret = data;
            }
            else if (code == (byte)DslBinaryCode.BeginStatement) {
                StatementData data = new StatementData();
                readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, data);
                ret = data;
            }
            return ret;
        }
        internal static void readBinary(byte[] bytes, int start, ref int curCodeIndex, List<string> identifiers, ref int curIdIndex, ValueData data)
        {
            byte code = readByte(bytes, start + curCodeIndex++);
            if (code == (byte)DslBinaryCode.BeginValue) {
                code = readByte(bytes, start + curCodeIndex);
                if (code >= (byte)DslBinaryCode.ValueTypeBegin) {
                    ++curCodeIndex;
                    data.SetType(code - (byte)DslBinaryCode.ValueTypeBegin);
                    data.SetId(readIdentifier(identifiers, curIdIndex++));
                }
                code = readByte(bytes, start + curCodeIndex);
                if (code == (byte)DslBinaryCode.EndValue) {
                    ++curCodeIndex;
                }
            }
        }
        internal static void readBinary(byte[] bytes, int start, ref int curCodeIndex, List<string> identifiers, ref int curIdIndex, FunctionData data)
        {
            byte code = readByte(bytes, start + curCodeIndex++);
            if (code == (byte)DslBinaryCode.BeginFunction) {
                code = readByte(bytes, start + curCodeIndex);
                if (code >= (byte)DslBinaryCode.ParamOrExternClassBegin) {
                    ++curCodeIndex;
                    data.SetParamClass(code - (byte)DslBinaryCode.ParamOrExternClassBegin);
                }
                code = readByte(bytes, start + curCodeIndex);
                if (code == (byte)DslBinaryCode.BeginValue) {
                    ValueData valueData = new ValueData();
                    readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, valueData);
                    data.Name = valueData;
                }
                else if (code == (byte)DslBinaryCode.BeginFunction) {
                    FunctionData callData = new FunctionData();
                    readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, callData);
                    data.LowerOrderFunction = callData;
                }
                for (; ; ) {
                    code = readByte(bytes, start + curCodeIndex);
                    if (code == (byte)DslBinaryCode.EndFunction) {
                        ++curCodeIndex;
                        break;
                    }
                    else {
                        ISyntaxComponent syntaxData = readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex);
                        if (null != syntaxData) {
                            data.Params.Add(syntaxData);
                        }
                        else {
                            break;
                        }
                    }
                }
            }
        }
        internal static void readBinary(byte[] bytes, int start, ref int curCodeIndex, List<string> identifiers, ref int curIdIndex, StatementData data)
        {
            byte code = readByte(bytes, start + curCodeIndex++);
            if (code == (byte)DslBinaryCode.BeginStatement) {
                for (; ; ) {
                    code = readByte(bytes, start + curCodeIndex);
                    if (code == (byte)DslBinaryCode.BeginFunction) {
                        FunctionData funcData = new FunctionData();
                        readBinary(bytes, start, ref curCodeIndex, identifiers, ref curIdIndex, funcData);
                        data.Functions.Add(funcData);
                    }
                    else if (code == (byte)DslBinaryCode.EndStatement) {
                        ++curCodeIndex;
                        break;
                    }
                    else {
                        break;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
#if FULL_VERSION
        internal static void writeBinary(MemoryStream stream, List<string> identifiers, ISyntaxComponent data)
        {
            ValueData val = data as ValueData;
            if (null != val) {
                writeBinary(stream, identifiers, val);
            }
            else {
                FunctionData call = data as FunctionData;
                if (null != call) {
                    writeBinary(stream, identifiers, call);
                }
                else {
                    StatementData statement = data as StatementData;
                    writeBinary(stream, identifiers, statement);
                }
            }
        }
        internal static void writeBinary(MemoryStream stream, List<string> identifiers, ValueData data)
        {
            stream.WriteByte((byte)DslBinaryCode.BeginValue);
            if (null != data) {
                stream.WriteByte((byte)((int)DslBinaryCode.ValueTypeBegin + data.GetIdType()));
                identifiers.Add(data.GetId());
            }
            stream.WriteByte((byte)DslBinaryCode.EndValue);
        }
        internal static void writeBinary(MemoryStream stream, List<string> identifiers, FunctionData data)
        {
            stream.WriteByte((byte)DslBinaryCode.BeginFunction);
            if (null != data) {
                stream.WriteByte((byte)((int)DslBinaryCode.ParamOrExternClassBegin + data.GetParamClass()));
                if (data.IsHighOrder) {
                    writeBinary(stream, identifiers, data.LowerOrderFunction);
                }
                else {
                    writeBinary(stream, identifiers, data.Name);
                }
                foreach (ISyntaxComponent syntaxData in data.Params) {
                    writeBinary(stream, identifiers, syntaxData);
                }
            }
            stream.WriteByte((byte)DslBinaryCode.EndFunction);
        }
        internal static void writeBinary(MemoryStream stream, List<string> identifiers, StatementData data)
        {
            stream.WriteByte((byte)DslBinaryCode.BeginStatement);
            foreach (FunctionData funcData in data.Functions) {
                writeBinary(stream, identifiers, funcData);
            }
            stream.WriteByte((byte)DslBinaryCode.EndStatement);
        }
#endif
    }
}
