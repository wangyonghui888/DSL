using Dsl.Common;
// CppParser.cs - generated by the SLK parser generator 

namespace Cpp.Parser
{

    static class CppParser
    {

private static short[] Production = {0

,2,105,187 ,4,106,112,188,110 ,2,106,107 ,6,106,1,112,189,190,2 
,3,106,117,3 ,4,106,4,122,2 ,9,107,191,192,5,8,193,9,194,108 
,6,107,10,8,195,9,108 ,2,108,151 ,2,108,2 ,7,109,3,121,8,196,9,198 
,3,110,133,111 ,2,110,2 ,2,111,151 ,4,111,199,200,2 ,2,112,113 
,2,112,115 ,2,112,116 ,2,112,117 ,2,112,114 ,2,112,13 ,2,113,14 
,2,113,15 ,2,113,16 ,2,113,17 ,2,113,18 ,2,114,19 ,2,114,20 
,2,114,21 ,2,115,22 ,2,115,23 ,2,115,24 ,2,115,25 ,2,115,26 
,2,115,27 ,2,115,28 ,2,115,29 ,2,115,30 ,3,115,5,201 
,2,115,118 ,2,115,130 ,2,116,31 ,2,116,32 ,2,117,33 ,2,117,34 
,2,117,35 ,4,118,119,124,204 ,5,118,119,36,205,37 ,4,118,38,123,208 
,2,119,39 ,2,119,40 ,3,120,211,42 ,3,121,212,123 ,3,122,213,5 
,2,123,5 ,3,123,273,42 ,2,124,10 ,3,124,274,42 ,5,125,3,214,121,216 
,4,126,133,217,218 ,3,126,3,183 ,3,127,128,219 ,4,128,133,273,220 
,2,129,115 ,2,129,116 ,5,130,43,36,131,37 ,4,130,43,42,221 
,3,131,132,222 ,4,132,42,275,223 ,3,132,44,224 ,3,133,225,134 
,3,134,120,135 ,5,134,8,133,9,135 ,5,135,45,226,46,135 
,7,135,276,8,137,9,277,135 ,5,135,8,156,9,135 ,4,135,8,9,135 
,2,135,47 ,4,136,41,227,228 ,2,136,48 ,4,137,138,229,230 
,4,138,112,231,232 ,2,139,140 ,3,139,136,233 ,3,140,120,141 
,5,140,8,139,9,141 ,5,140,45,234,46,141 ,5,140,8,235,9,141 
,5,141,45,236,46,141 ,5,141,8,137,9,141 ,5,141,8,156,9,141 
,4,141,8,9,141 ,2,141,47 ,2,142,157 ,5,142,36,143,237,37 
,3,143,142,238 ,4,144,129,239,240 ,3,145,136,241 ,2,145,146 
,5,146,8,145,9,147 ,5,146,45,242,46,147 ,5,146,8,243,9,147 
,5,147,45,244,46,147 ,5,147,8,245,9,147 ,2,147,47 ,2,148,149 
,2,148,150 ,2,148,151 ,2,148,152 ,2,148,154 ,2,148,155 ,2,148,106 
,4,149,42,3,148 ,5,149,50,183,3,148 ,4,149,51,3,148 
,3,150,246,2 ,6,151,276,36,247,37,277 ,7,152,52,8,156,9,148,153 
,6,152,53,8,156,9,148 ,3,153,54,148 ,2,153,47 ,6,154,55,8,156,9,148 
,8,154,56,148,55,8,156,9,2 ,10,154,57,8,248,2,249,2,250,9,148 
,4,155,58,42,2 ,3,155,59,2 ,3,155,60,2 ,4,155,61,251,2 
,3,156,157,252 ,3,157,159,253 ,2,158,12 ,2,158,62 ,2,158,63 
,2,158,64 ,2,158,65 ,2,158,66 ,2,158,67 ,2,158,68 ,2,158,69 
,2,158,70 ,2,158,71 ,3,159,160,254 ,3,160,161,255 ,3,161,162,256 
,3,162,163,257 ,3,163,164,258 ,3,164,165,259 ,3,165,167,260 
,2,166,77 ,2,166,78 ,3,167,169,261 ,2,168,79 ,2,168,80 
,2,168,81 ,2,168,82 ,3,169,171,262 ,2,170,83 ,2,170,84 
,3,171,173,263 ,2,172,85 ,2,172,86 ,3,173,175,264 ,2,174,41 
,2,174,87 ,2,174,88 ,2,175,176 ,5,175,8,144,9,175 ,2,176,178 
,3,176,89,176 ,3,176,90,176 ,3,176,177,175 ,3,176,91,176 
,5,176,91,8,144,9 ,7,176,92,5,265,8,266,9 ,5,176,92,8,144,9 
,3,176,93,175 ,5,176,93,45,46,175 ,2,177,48 ,2,177,41 
,2,177,85 ,2,177,86 ,2,177,7 ,2,177,94 ,3,178,180,179 
,5,179,45,156,46,179 ,5,179,8,267,9,179 ,4,179,95,42,179 
,4,179,96,42,179 ,4,179,97,42,179 ,4,179,98,42,179 ,3,179,89,179 
,3,179,90,179 ,2,179,47 ,2,180,120 ,2,180,182 ,3,180,99,268 
,4,180,8,156,9 ,3,181,157,269 ,2,182,100 ,2,182,101 ,2,182,102 
,2,182,103 ,2,182,44 ,3,183,184,270 ,3,184,185,271 ,2,185,182 
,3,185,186,185 ,4,185,8,183,9 ,3,185,91,185 ,5,185,91,8,144,9 
,2,186,41 ,2,186,85 ,2,186,86 ,2,186,7 ,2,186,94 ,3,187,106,187 
,2,187,272 ,3,188,112,188 ,1,188 ,3,189,112,189 ,1,189 ,2,190,127 
,1,190 ,3,191,5,6 ,1,191 ,2,192,7 ,1,192 ,2,193,137 
,1,193 ,2,194,109 ,1,194 ,2,195,137 ,1,195 ,2,196,156 ,1,196 
,2,197,156 ,1,197 ,7,198,11,121,8,197,9,198 ,1,198 ,3,199,12,142 
,1,199 ,4,200,11,126,200 ,1,200 ,3,201,6,5 ,1,201 ,2,202,125 
,1,202 ,3,203,106,203 ,1,203 ,5,204,202,36,203,37 ,1,204 
,3,205,106,205 ,1,205 ,2,206,125 ,1,206 ,3,207,106,207 ,1,207 
,5,208,206,36,207,37 ,1,208 ,4,209,123,6,209 ,1,209 ,3,210,41,210 
,1,210 ,5,211,123,6,209,210 ,1,211 ,4,212,123,6,212 ,1,212 
,4,213,123,6,213 ,1,213 ,2,214,117 ,1,214 ,2,215,117 ,1,215 
,5,216,11,215,121,216 ,1,216 ,3,217,3,183 ,1,217 ,3,218,12,142 
,1,218 ,4,219,11,128,219 ,1,219 ,3,220,12,142 ,1,220 ,4,221,36,131,37 
,1,221 ,4,222,11,132,222 ,1,222 ,3,223,12,183 ,1,223 ,3,224,12,183 
,1,224 ,2,225,136 ,1,225 ,2,226,183 ,1,226 ,2,227,116 ,1,227 
,2,228,136 ,1,228 ,4,229,11,138,229 ,1,229 ,3,230,11,49 
,1,230 ,3,231,112,231 ,1,231 ,2,232,139 ,1,232 ,2,233,140 
,1,233 ,2,234,183 ,1,234 ,2,235,137 ,1,235 ,2,236,183 ,1,236 
,2,237,11 ,1,237 ,4,238,11,142,238 ,1,238 ,3,239,129,239 
,1,239 ,2,240,145 ,1,240 ,2,241,146 ,1,241 ,2,242,183 ,1,242 
,2,243,137 ,1,243 ,2,244,183 ,1,244 ,2,245,137 ,1,245 ,2,246,156 
,1,246 ,3,247,148,247 ,1,247 ,2,248,156 ,1,248 ,2,249,156 
,1,249 ,2,250,156 ,1,250 ,2,251,156 ,1,251 ,4,252,11,157,252 
,1,252 ,3,253,158,157 ,1,253 ,5,254,72,156,3,159 ,1,254 
,4,255,73,161,255 ,1,255 ,4,256,74,162,256 ,1,256 ,4,257,75,163,257 
,1,257 ,4,258,76,164,258 ,1,258 ,4,259,48,165,259 ,1,259 
,4,260,166,167,260 ,1,260 ,4,261,168,169,261 ,1,261 ,4,262,170,171,262 
,1,262 ,4,263,172,173,263 ,1,263 ,4,264,174,175,264 ,1,264 
,3,265,6,5 ,1,265 ,2,266,181 ,1,266 ,2,267,181 ,1,267 
,3,268,99,268 ,1,268 ,4,269,11,157,269 ,1,269 ,4,270,172,184,270 
,1,270 ,4,271,174,185,271 ,1,271 
,0};

private static int[] Production_row = {0

,1,4,9,12,19,23,28,38,45,48,51,59,63,66,69,74
,77,80,83,86,89,92,95,98,101,104,107,110,113,116,119,122
,125,128,131,134,137,140,143,147,150,153,156,159,162,165,168,173
,179,184,187,190,194,198,202,205,209,212,216,222,227,231,235,240
,243,246,252,257,261,266,270,274,278,284,290,298,304,309,312,317
,320,325,330,333,337,341,347,353,359,365,371,377,382,385,388,394
,398,403,407,410,416,422,428,434,440,443,446,449,452,455,458,461
,464,469,475,480,484,491,499,506,510,513,520,529,540,545,549,553
,558,562,566,569,572,575,578,581,584,587,590,593,596,599,603,607
,611,615,619,623,627,630,633,637,640,643,646,649,653,656,659,663
,666,669,673,676,679,682,685,691,694,698,702,706,710,716,724,730
,734,740,743,746,749,752,755,758,762,768,774,779,784,789,794,798
,802,805,808,811,815,820,824,827,830,833,836,839,843,847,850,854
,859,863,869,872,875,878,881,884,888,891,895,897,901,903,906,908
,912,914,917,919,922,924,927,929,932,934,937,939,942,944,952,954
,958,960,965,967,971,973,976,978,982,984,990,992,996,998,1001,1003
,1007,1009,1015,1017,1022,1024,1028,1030,1036,1038,1043,1045,1050,1052,1055,1057
,1060,1062,1068,1070,1074,1076,1080,1082,1087,1089,1093,1095,1100,1102,1107,1109
,1113,1115,1119,1121,1124,1126,1129,1131,1134,1136,1139,1141,1146,1148,1152,1154
,1158,1160,1163,1165,1168,1170,1173,1175,1178,1180,1183,1185,1188,1190,1195,1197
,1201,1203,1206,1208,1211,1213,1216,1218,1221,1223,1226,1228,1231,1233,1236,1238
,1242,1244,1247,1249,1252,1254,1257,1259,1262,1264,1269,1271,1275,1277,1283,1285
,1290,1292,1297,1299,1304,1306,1311,1313,1318,1320,1325,1327,1332,1334,1339,1341
,1346,1348,1353,1355,1359,1361,1364,1366,1369,1371,1375,1377,1382,1384,1389,1391
,1396
,0};

private static short[] Parse = {

0,0,336,336,11,336,336,59,336,336,85,336,0,394,336,336,336,336,336
,336,336,336,336,336,336,336,336,336,336,336,336,336,336,336,336,336,336,336,337
,336,336,336,336,336,336,336,64,85,10,336,87,336,336,336,336,392,336,336,336
,336,336,336,336,64,64,64,64,64,64,64,64,64,65,65,22,23,24,25,26
,64,64,64,9,7,64,7,336,336,8,286,336,336,336,336,336,336,125,126,127
,128,336,336,336,336,336,112,107,117,112,399,0,398,107,72,112,287,73,112,112
,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112
,112,108,52,112,112,112,107,397,112,107,97,72,271,107,0,106,106,109,109,0
,110,110,110,111,111,111,111,97,97,97,97,97,97,97,97,97,97,97,113,52
,270,270,270,97,97,97,114,115,97,271,107,107,42,43,107,107,107,107,107,107
,152,153,154,155,107,107,107,107,107,251,250,163,251,50,51,251,251,68,251,68
,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251,251
,251,251,251,250,0,251,251,251,251,251,251,53,251,259,258,251,259,164,165,259
,259,0,259,57,259,259,259,259,259,259,259,259,259,259,259,259,259,259,259,259
,259,259,259,259,259,259,259,258,53,259,259,259,259,259,259,58,259,245,239,259
,245,244,0,245,245,316,245,238,245,245,245,245,245,245,245,245,245,245,245,245
,245,245,245,245,245,245,245,245,245,245,245,317,239,245,245,245,245,245,245,285
,245,246,285,245,0,285,285,69,285,70,285,285,285,285,285,285,285,285,285,285
,285,285,285,285,285,285,285,285,285,285,285,285,285,284,247,285,285,285,285,285
,285,131,285,371,371,285,17,27,28,29,371,393,371,371,21,16,16,16,16,16
,20,20,20,17,17,17,17,17,17,17,17,17,18,18,19,19,19,0,371,17
,17,17,370,0,17,118,119,371,74,371,78,132,133,134,135,136,137,138,139,140
,141,231,230,371,371,371,371,371,371,371,371,371,371,371,371,371,371,371,371,371
,371,371,371,371,371,371,371,371,370,370,414,149,150,305,305,231,305,0,304,304
,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304
,304,157,158,304,304,304,305,305,304,241,305,1,15,305,1,1,0,1,241,240
,1,15,15,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
,1,1,1,1,1,1,14,0,1,1,1,216,0,1,216,216,13,216,254,12
,216,99,12,216,216,216,216,216,216,216,216,216,216,216,216,216,216,216,216,216
,216,216,216,216,216,216,160,161,216,216,216,255,0,216,98,12,12,243,99,277
,276,98,12,407,219,225,242,405,277,277,219,44,45,46,1,218,218,218,218,218
,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,279,0
,218,218,218,219,219,218,221,279,278,406,219,227,221,226,217,289,288,220,220,220
,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220
,0,289,220,220,220,221,221,220,324,325,248,281,221,248,248,301,248,413,395,248
,280,104,248,248,248,248,248,248,248,248,248,248,248,248,248,248,248,248,248,248
,248,248,248,248,248,324,249,248,248,248,252,0,248,252,252,89,252,93,103,252
,105,260,252,252,252,252,252,252,252,252,252,252,252,252,252,252,252,252,252,252
,252,252,252,252,252,0,253,252,252,252,256,0,252,256,256,0,256,261,408,256
,274,214,256,256,256,256,256,256,256,256,256,256,256,256,256,256,256,256,256,256
,256,256,256,256,256,275,257,256,256,256,369,369,256,54,228,211,55,369,229,369
,369,0,228,228,228,228,228,228,228,228,228,228,228,228,228,228,228,228,228,228
,228,228,228,228,228,369,0,228,228,228,54,79,228,56,369,121,369,0,80,212
,213,0,120,379,49,391,390,303,215,302,369,369,369,369,369,369,369,369,369,369
,369,369,369,369,369,369,369,369,369,369,369,369,369,368,368,4,367,367,6,386
,379,3,379,367,3,367,367,2,2,2,2,2,2,2,2,2,2,2,2,2
,2,2,2,2,2,2,2,389,388,387,0,367,2,2,2,262,263,2,0,372
,367,373,367,396,379,379,122,123,124,264,379,379,379,379,378,0,367,367,367,367
,367,367,367,367,367,367,367,367,367,367,367,367,367,367,367,367,367,366,366,232
,101,410,0,233,291,290,409,232,232,232,232,232,232,232,232,232,232,232,232,232
,232,232,232,232,232,232,232,232,232,232,291,312,232,232,232,313,415,232,411,312
,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312,312
,312,312,319,328,312,312,312,329,381,312,380,328,328,328,328,328,328,328,328,328
,328,328,328,328,328,328,328,328,328,328,328,328,328,328,0,332,328,328,328,333
,186,328,0,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332,332
,332,332,332,332,332,332,283,203,332,332,332,365,365,332,81,283,282,185,365,193
,365,365,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81
,81,81,81,81,81,0,365,81,81,81,0,0,81,0,0,365,184,365,0,184
,412,191,192,199,200,201,202,187,188,189,190,365,365,365,365,365,365,365,365,365
,365,365,365,365,365,365,365,365,364,364,364,364,82,184,61,184,60,268,0,60
,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82,82
,82,82,82,39,0,82,82,82,363,363,82,0,60,60,0,363,273,363,363,60
,30,31,32,33,34,35,36,37,38,0,184,184,184,184,184,0,40,40,40,0
,0,41,0,363,272,272,272,0,0,0,361,361,363,273,363,0,0,361,0,361
,361,0,0,0,0,0,0,0,363,363,363,363,363,363,363,363,363,363,363,363
,363,363,363,362,362,361,0,0,0,223,359,359,222,0,361,222,360,359,0,359
,359,94,0,94,94,83,0,0,83,0,361,361,361,361,361,361,361,361,361,361
,361,361,361,361,361,359,0,0,222,222,62,0,95,62,359,222,0,94,94,0
,94,84,83,182,94,83,0,0,84,0,359,359,359,359,359,359,359,359,359,359
,359,359,359,359,358,0,62,62,0,96,0,96,96,62,0,0,0,179,322,323
,0,94,94,0,178,94,94,94,94,94,94,0,0,0,0,94,94,94,94,94
,96,0,0,0,0,96,96,194,96,0,197,322,96,0,0,322,357,357,322,0
,0,180,181,357,0,357,357,0,0,116,183,0,116,308,116,116,308,309,0,309
,0,0,0,0,194,0,195,0,0,96,96,357,0,96,96,96,96,96,96,0
,357,0,0,96,96,96,96,96,116,116,308,116,0,308,0,116,357,357,357,357
,357,357,357,357,357,357,357,357,357,356,0,0,0,0,0,234,0,234,234,235
,0,196,195,195,195,195,0,0,0,0,0,63,116,116,63,0,116,116,116,116
,116,116,0,0,0,0,116,116,116,116,116,234,234,0,234,0,0,0,234,0
,0,0,355,355,0,0,0,63,63,355,0,355,355,0,63,0,347,347,236,0
,236,236,237,347,0,346,0,0,0,0,0,0,0,0,0,234,234,355,0,234
,234,234,234,234,234,0,355,0,0,234,234,234,234,234,236,236,0,236,0,0
,347,236,355,355,355,355,355,355,355,355,355,355,355,355,354,0,0,0,0,0
,0,0,0,335,0,0,334,0,334,334,299,0,0,299,299,0,299,0,236,236
,0,0,236,236,236,236,236,236,0,0,0,0,236,236,236,236,236,320,0,0
,321,321,334,334,0,334,298,299,0,334,299,0,0,298,320,320,320,320,320,320
,320,320,320,320,320,0,0,0,0,0,320,320,320,321,0,320,339,321,0,338
,321,338,338,0,0,0,334,334,0,0,334,334,334,334,334,334,71,0,0,71
,334,334,334,334,334,341,293,0,340,293,340,340,0,0,0,338,338,0,338,0
,0,0,338,0,0,0,0,0,0,0,0,0,71,71,0,0,0,0,0,71
,0,0,292,293,340,340,0,340,0,292,0,340,0,0,0,0,0,0,0,338
,338,0,0,338,338,338,338,338,338,0,0,0,0,338,338,338,338,338,342,0
,342,342,343,0,0,0,0,0,340,340,0,0,340,340,340,340,340,340,0,0
,0,0,340,340,340,340,340,345,0,0,344,0,344,344,342,342,0,342,0,0
,0,342,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
,0,374,0,374,374,375,344,344,0,344,0,0,0,344,0,0,0,0,342,342
,0,0,342,342,342,342,342,342,0,0,0,0,342,342,342,342,342,374,374,0
,374,0,204,204,374,0,0,0,0,0,344,344,0,0,344,344,344,344,344,344
,0,0,0,0,344,344,344,344,344,376,0,376,376,377,297,0,204,297,297,204
,297,374,374,0,0,374,374,374,374,374,374,129,0,129,129,374,374,374,374,374
,296,296,0,0,0,376,376,0,376,0,297,297,376,0,297,0,0,297,0,0
,204,204,0,0,0,0,204,129,129,204,129,0,0,0,129,204,204,204,204,0
,0,0,0,0,0,0,0,0,0,376,376,0,0,376,376,376,376,376,376,130
,0,130,130,376,376,376,376,376,0,0,0,129,129,0,0,129,129,129,129,129
,129,142,0,142,142,129,129,129,129,129,0,0,0,0,0,130,130,0,130,0
,0,0,130,0,0,0,383,383,0,0,0,0,0,383,0,383,383,142,142,0
,142,0,0,0,142,0,0,0,0,0,0,0,0,0,0,0,0,0,0,130
,130,383,0,130,130,130,130,130,130,143,383,143,143,130,130,130,130,130,0,0
,0,142,142,0,0,142,142,142,142,142,142,144,0,144,144,142,142,142,142,142
,0,0,0,0,0,143,143,0,143,382,382,0,143,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,144,144,0,144,0,0,0,144,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,143,143,0,0,143,143,143,143,143,143,145
,0,145,145,143,143,143,143,143,0,0,0,144,144,0,0,144,144,144,144,144
,144,146,0,146,146,144,144,144,144,144,0,0,0,0,0,145,145,0,145,0
,0,0,145,0,0,0,0,0,0,0,0,0,0,0,0,0,0,146,146,0
,146,0,0,0,146,0,0,0,0,0,0,0,0,0,0,0,0,0,0,145
,145,0,0,145,145,145,145,145,145,147,0,147,147,145,145,145,145,145,0,0
,0,146,146,0,0,146,146,146,146,146,146,148,0,148,148,146,146,146,146,146
,0,0,0,0,0,147,147,0,147,0,0,0,147,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,148,148,0,148,0,0,0,148,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,147,147,0,0,147,147,147,147,147,147,151
,0,151,151,147,147,147,147,147,0,0,0,148,148,0,0,148,148,148,148,148
,148,156,0,156,156,148,148,148,148,148,0,0,0,0,0,151,151,0,151,0
,0,0,151,0,0,0,0,0,0,0,0,0,0,0,0,0,0,156,156,0
,156,0,0,0,156,0,0,0,0,0,0,0,0,0,0,0,0,0,0,151
,151,0,0,151,151,151,151,151,151,159,0,159,159,151,151,151,151,151,0,0
,0,156,156,0,0,156,156,156,156,156,156,162,0,162,162,156,156,156,156,156
,0,0,0,0,0,159,159,0,159,0,0,0,159,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,162,162,0,162,0,0,0,162,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,159,159,0,0,159,159,159,159,159,159,166
,0,166,400,159,159,159,159,159,0,0,0,162,162,0,0,162,162,162,162,162
,162,168,0,171,168,162,162,162,162,162,0,0,0,0,0,166,166,0,166,0
,0,0,166,0,0,0,0,0,0,0,0,0,0,0,0,0,0,171,168,0
,168,0,0,0,171,0,0,0,0,0,0,0,0,0,0,0,0,0,0,166
,166,0,0,166,166,166,166,166,166,198,0,198,198,166,166,166,166,166,0,0
,0,171,171,0,0,169,170,401,402,403,171,0,0,0,0,168,168,168,168,168
,0,0,0,0,0,198,198,0,198,0,0,0,198,0,0,0,353,353,0,0
,0,0,0,353,0,353,353,0,0,0,0,0,0,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,198,198,353,0,198,198,198,198,198,198,0
,353,351,351,198,198,198,198,198,351,0,351,351,0,0,0,0,353,353,353,353
,353,353,353,353,353,353,353,352,0,0,0,0,0,0,0,0,351,349,349,294
,294,0,0,0,349,351,349,348,0,0,0,0,310,310,0,0,0,0,0,0
,0,351,351,351,351,351,351,351,351,351,351,350,349,294,314,314,294,0,295,306
,0,349,306,307,0,307,310,0,0,310,0,311,0,0,0,0,0,348,348,348
,348,348,348,348,348,348,348,0,314,0,0,314,0,315,0,306,306,294,294,306
,326,326,306,294,0,0,294,0,0,0,310,310,294,294,294,294,310,0,0,310
,330,330,0,0,0,310,310,310,310,0,0,0,314,314,326,0,0,326,314,327
,0,314,0,205,205,0,0,314,314,314,314,0,0,0,330,0,0,330,0,331
,0,0,0,207,208,0,0,0,0,0,0,0,0,0,0,0,0,205,326,326
,205,0,0,0,326,0,0,326,0,0,0,0,0,326,326,326,326,207,330,330
,206,0,0,0,330,0,0,330,0,0,385,385,0,330,330,330,330,385,0,385
,385,205,205,0,0,0,0,205,0,0,205,0,0,0,0,0,205,205,205,205
,0,207,207,0,0,385,0,404,0,384,207,0,0,0,385,0,206,206,206,206
,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,0,0,0,0,385,385,384,384,0,0,0
,0,0,0,0,0,0,0,0,0,0,0,0,0
};

private static int[] Parse_row = {0

,509,893,78,46,1,553,509,380,60,367,1217,160,577,825,173,136
,245,807,810,252,4,1190,1344,1525,41,12,174,304,1730,108,382,809
,1108,1186,1319,5,689,1315,1383,144,552,934,692,104,135,1446,71,372
,807,890,38,1965,2033,368,2055,2123,2145,2213,2235,2303,2325,393,2393,120
,2415,417,2483,500,2505,169,2573,2595,1355,1150,1071,1421,2663,1062,1914,2915
,2935,773,549,601,643,1306,597,645,808,438,973,1509,1572,295,506,594
,294,337,688,206,728,554,768,250,735,893,943,975,1154,146,1230,768
,596,635,688,1103,336,78,643,972,1740,2791,1948,1644,685,857,464,2833
,1447,2804,1005,2826,293,1004,1671,1389,679,2872,1037,2892,1069,1640,1,1713
,1742,1812,1844,1573,2794,2758,2713,1559,1433,1307,1267,1225,1108,893,807,380
,932,1875,1943,854,1038,2083,2987
,0};

private static short[] Conflict = {

0,0,0,2,0,0,2,416,224,417,225,429,0,3,2,2,2,2,2
,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,47,0
,2,2,2,2,2,2,66,269,0,0,2,268,67,107,0,112,0,0,437,0
,0,0,0,419,48,0,86,88,438,0,47,88,88,88,88,88,88,88,88,88
,88,88,88,88,88,88,88,88,88,88,88,88,88,88,219,219,88,88,88,86
,86,88,0,86,221,221,86,2,5,0,2,0,266,2,267,0,0,267,2,2
,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2
,2,0,267,2,2,2,2,2,2,0,0,0,418,2,76,76,77,434,0,112
,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75
,75,75,75,0,439,75,75,75,76,76,75,76,442,107,107,76,0,0,443,0
,0,0,0,0,0,0,444,0,445,0,0,0,0,0,106,0,0,107,0,107
,305,305,0,0,0,0,0,0,76,76,0,0,76,76,86,86,76,76,76,76
,76,76,91,91,166,166,76,76,76,76,76,420,0,91,91,92,107,0,107,90
,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90
,90,90,0,0,90,90,90,91,91,90,91,449,0,451,91,0,0,0,0,0
,107,107,0,0,0,0,107,107,107,107,0,0,0,0,0,0,0,0,209,425
,0,0,0,0,0,0,0,172,172,2,2,91,91,0,0,91,91,91,91,91
,91,0,0,0,0,91,91,91,91,91,102,0,209,100,102,209,0,0,102,102
,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102,102
,102,0,0,102,102,102,100,0,102,421,100,107,107,100,0,0,209,209,0,0
,0,0,209,0,0,209,0,260,0,261,0,209,209,209,209,0,167,435,0,167
,167,0,0,0,0,107,107,0,107,0,0,0,107,167,167,167,167,167,167,167
,167,167,167,167,0,0,261,0,261,167,167,167,167,0,167,0,167,0,0,167
,0,0,0,0,0,0,0,0,0,107,107,0,0,107,107,107,107,107,107,0
,0,0,0,107,107,107,107,107,0,112,261,261,112,422,0,112,261,261,261,261
,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112
,112,112,112,0,0,112,112,112,112,112,112,423,0,166,166,112,0,0,0,0
,0,0,0,0,0,0,0,0,167,167,167,167,167,167,167,167,167,167,167,172
,0,172,424,0,167,167,167,166,166,167,166,0,0,0,166,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,172,172,0,172,0
,0,0,172,0,0,0,0,0,0,0,0,166,166,0,0,166,166,166,166,166
,166,0,0,0,0,166,166,166,166,166,0,0,0,0,0,0,0,0,0,172
,172,0,0,172,172,172,172,172,172,0,0,0,0,172,172,172,172,172,0,174
,218,0,175,218,426,0,218,0,0,0,0,218,218,218,218,218,218,218,218,218
,218,218,218,218,218,218,218,218,218,218,218,218,218,218,0,0,218,218,218,218
,218,218,0,0,220,0,218,220,427,0,220,0,0,0,0,220,220,220,220,220
,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,0,0
,220,220,220,220,220,220,264,0,265,0,220,0,0,0,0,0,0,0,0,176
,0,176,176,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
,0,0,0,0,0,265,0,265,173,446,0,173,173,0,0,176,176,0,176,177
,0,0,176,0,0,173,173,173,173,173,173,173,173,173,173,173,0,0,0,0
,0,173,173,173,173,0,173,0,173,265,265,173,0,0,0,265,265,265,265,176
,176,0,0,176,176,176,176,176,176,0,300,0,0,176,176,176,176,176,300,300
,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300,300
,300,0,0,300,300,300,0,0,300,304,428,0,304,304,301,304,0,304,304,304
,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304
,0,0,304,304,304,304,304,304,318,304,318,318,304,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,318
,319,0,0,0,318,318,0,318,0,0,0,318,0,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
,0,0,0,0,0,0,0,0,318,318,0,0,318,318,318,318,318,318,0,0
,0,0,318,318,318,318,318,430,0,0,2,3,0,0,0,3,3,3,3,3
,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0
,3,3,3,2,2,3,0,0,75,431,2,75,75,0,75,0,75,75,75,75
,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,75,0
,0,75,75,75,75,75,75,0,75,88,432,75,88,88,0,88,0,88,88,88
,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88
,0,0,88,88,88,88,88,88,0,88,90,433,88,90,90,0,90,0,90,90
,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90
,90,0,0,90,90,90,90,90,90,436,90,172,172,90,0,0,0,0,0,0
,0,0,0,0,0,0,173,173,173,173,173,173,173,173,173,173,173,210,0,209
,209,0,173,173,173,172,172,173,172,0,0,0,172,0,210,210,210,210,210,210
,210,210,210,210,210,0,0,0,0,0,210,210,210,209,0,210,209,0,0,0
,0,0,0,0,0,0,0,0,0,172,172,0,0,172,172,172,172,172,172,0
,0,0,0,172,172,172,172,172,0,0,0,0,0,0,0,0,0,209,209,0
,0,0,0,209,0,2,209,0,2,0,0,440,209,209,209,209,2,2,2,2
,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0
,0,2,2,2,2,2,2,0,0,112,0,2,112,107,0,112,0,0,0,0
,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112,112
,112,112,112,0,0,112,112,112,112,112,112,0,0,0,0,112,0,0,0,0
,0,0,0,0,0,0,0,0,3,441,0,3,3,0,3,0,3,3,3,3
,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0
,0,3,3,3,3,3,3,0,3,218,0,3,218,219,0,218,0,0,0,0
,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218,218
,218,218,218,0,0,218,218,218,218,218,218,0,0,220,0,218,220,221,0,220
,0,0,0,0,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220,220
,220,220,220,220,220,220,220,0,0,220,220,220,220,220,220,0,0,304,305,220
,304,304,0,304,0,304,304,304,304,304,304,304,304,304,304,304,304,304,304,304
,304,304,304,304,304,304,304,304,0,0,304,304,304,304,304,304,0,304,447,0
,304,2,3,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3
,3,3,3,3,3,3,3,3,3,0,0,3,3,3,2,2,3,0,0,75
,76,2,75,75,0,75,0,75,75,75,75,75,75,75,75,75,75,75,75,75
,75,75,75,75,75,75,75,75,75,75,0,0,75,75,75,75,75,75,0,75
,0,0,75,0,0,0,0,0,0,0,0,0,0,88,86,0,88,88,448,88
,0,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88,88
,88,88,88,88,0,0,88,88,88,88,88,88,0,88,2,2,88,90,91,0
,90,90,0,90,0,90,90,90,90,90,90,90,90,90,90,90,90,90,90,90
,90,90,90,90,90,90,90,90,0,0,90,90,90,90,90,90,0,90,167,166
,90,167,167,0,0,0,0,0,0,0,0,0,0,0,0,167,167,167,167,167
,167,167,167,167,167,167,0,0,0,0,0,167,167,167,167,0,167,0,167,3
,450,167,3,3,0,3,0,3,3,3,3,3,3,3,3,3,3,3,3,3
,3,3,3,3,3,3,3,3,3,3,0,0,3,3,3,3,3,3,0,3
,173,172,3,173,173,0,0,0,0,0,0,0,0,0,0,0,0,173,173,173
,173,173,173,173,173,173,173,173,0,0,0,0,0,173,173,173,173,0,173,0
,173,0,0,173,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3
,2,0,3,3,0,3,0,3,3,3,3,3,3,3,3,3,3,3,3,3
,3,3,3,3,3,3,3,3,3,3,0,0,3,3,3,3,3,3,0,3
,3,2,3,3,3,0,3,0,3,3,3,3,3,3,3,3,3,3,3,3
,3,3,3,3,3,3,3,3,3,3,3,0,0,3,3,3,3,3,3,0
,3,0,0,3
};

private static int[] Conflict_row = {0

,1,104,104,104,27,27,9,146,57,245,344,210,383,486,525,553
,653,753,310,657,701,2,400,739,105,105,44,844,883,922,6,1021
,1062,1103,1144,46,151,410,1183,1211,52,62,178,1302,1402,186,192,200
,1346,202,782,1446,1490,1531,1572,1672,1613,1667,1711,1752,285,1793,1893,1834
,287,1934
,0};

private static short get_conditional_production ( short symbol ) { return (short) 0; }

private const short   END_OF_SLK_INPUT_ = 104;
private const short   START_SYMBOL = 105;
private const short   START_STATE = 0;
private const short   START_CONFLICT = 386;
private const short   END_CONFLICT = 452;
private const short   START_ACTION = 272;
private const short   END_ACTION = 278;
private const short   TOTAL_CONFLICTS = 66;

public const int   NOT_A_SYMBOL = 0;
public const int   NONTERMINAL_SYMBOL = 1;
public const int   TERMINAL_SYMBOL = 2;
public const int   ACTION_SYMBOL = 3;

public static short[]
GetProductionArray ( short  production_number )
{
   short   index = (short)  Production_row [ production_number ],
           array_length = (short)  Production [ index ],
           new_index = 0;
   short[] productionArray = new short[23];        

   while ( array_length-- >= 0 ) {
       productionArray [ new_index++ ] = Production [ index++ ];
   }
   return  productionArray;
}

public static int GetSymbolType ( short   symbol )
{
   int   symbol_type = NOT_A_SYMBOL;

   if ( symbol >= START_ACTION  &&  symbol < END_ACTION ) {
       symbol_type = ACTION_SYMBOL;
   } else if ( symbol >= START_SYMBOL ) {
       symbol_type = NONTERMINAL_SYMBOL;
   } else if ( symbol > 0 ) {
       symbol_type = TERMINAL_SYMBOL;
   }
   return  symbol_type;
}

public static bool    IsNonterminal ( short   symbol )
{
   return ( symbol >= START_SYMBOL  &&  symbol < START_ACTION );
}

public static bool    IsTerminal ( short   symbol )
{
   return ( symbol > 0  &&  symbol < START_SYMBOL );
}

public static bool    IsAction ( short   symbol )
{
   return ( symbol >= START_ACTION  &&  symbol < END_ACTION );
}

public static short GetTerminalIndex ( short   token ){
 return ( token );
}

public static short
get_production ( short     conflict_number,
                 CppToken  tokens )
{
    short   entry = 0;
    int     index, level;

    if ( conflict_number <= TOTAL_CONFLICTS ) {
        entry = (short) ( conflict_number + (START_CONFLICT - 1) );
        level = 1;
        while ( entry >= START_CONFLICT ) {
            index = Conflict_row [entry - (START_CONFLICT -1)];
            index += tokens.peek ( level );
            entry = Conflict [ index ];
            ++level;
        }
    }

    return  entry;
}

private static short
get_predicted_entry ( CppToken   tokens,
                      short      production_number,
                      short      token,
                      int        scan_level,
                      int        depth )
{
 return  0;
}

        internal unsafe static void
        parse(ref DslAction action,
                ref CppToken tokens,
                ref CppError error,
                short start_symbol)
        {
            short rhs, lhs;
            short production_number, entry, symbol, token, new_token;
            int production_length, top, index, level;
            
            short* stack = stackalloc short[65535];

            top = 65534;
            stack[top] = 0;
            if (start_symbol == 0) {
                start_symbol = START_SYMBOL;
            }
            if (top > 0) {
                stack[--top] = start_symbol;
            } else { error.message("CppParse: stack overflow\n", ref tokens); return; }
            token = tokens.get();
            new_token = token;

            for (symbol = (stack[top] != 0 ? stack[top++] : (short)0); symbol != 0; ) {

                if (symbol >= START_ACTION) {
                    action.execute(symbol - (START_ACTION - 1));

                } else if (symbol >= START_SYMBOL) {
                    entry = 0;
                    level = 1;
                    production_number = get_conditional_production(symbol);
                    if (production_number != 0) {
                        entry = get_predicted_entry(tokens,
                                                      production_number, token,
                                                      level, 1);
                    }
                    if (entry == 0) {
                        index = Parse_row[symbol - (START_SYMBOL - 1)];
                        index += token;
                        entry = Parse[index];
                    }
                    while (entry >= START_CONFLICT) {
                        index = Conflict_row[entry - (START_CONFLICT - 1)];
                        index += tokens.peek(level);
                        entry = Conflict[index];
                        ++level;
                    }
                    if (entry != 0) {
                        index = Production_row[entry];
                        production_length = Production[index] - 1;
                        lhs = Production[++index];
                        if (lhs == symbol) {
                            action.predict(entry);
                            index += production_length;
                            for (; production_length-- > 0; --index) {
                                if (top > 0) {
                                    stack[--top] = Production[index];
                                } else { error.message("CppParse: stack overflow\n", ref tokens); return; }
                            }
                        } else {
                            new_token = error.no_entry(symbol, token, level - 1, ref tokens);
                        }
                    } else {                                       // no table entry
                        new_token = error.no_entry(symbol, token, level - 1, ref tokens);
                    }
                } else if (symbol > 0) {
                    if (symbol == token) {
                        token = tokens.get();
                        new_token = token;
                    } else {
                        new_token = error.mismatch(symbol, token, ref tokens);
                    }
                } else {
                    error.message("\n parser error: symbol value 0\n", ref tokens);
                }
                if (token != new_token) {
                    if (new_token != 0) {
                        token = new_token;
                    }
                    if (token != END_OF_SLK_INPUT_) {
                        continue;
                    }
                }
                symbol = (stack[top] != 0 ? stack[top++] : (short)0);
            }
            if (token != END_OF_SLK_INPUT_) {
                error.input_left(ref tokens);
            }
        }
    };
}
