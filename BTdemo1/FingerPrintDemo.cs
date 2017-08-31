#define USEFINGERPRINT
using System;
using System.Collections.Generic;
using System.Text;

namespace Intermec.Printer.Language.Fingerprint
{
    public class Demo
    {
        public static string ESCP_PRODLIST2
        {
            get
            {
                string s = "";
                s+="\x1B@\x1Bw!\x1Bw!\x1Bw!\x1Bw!LOCATION# 111111         ROUTE# 222222\n";
                s+="\x1Bw!REP# 123456   06/26/08 08:52   PAGE  1\n";
                s+="\x1Bw!\n";
                s+="\x1Bw!\x1Bw&     ITEM LIST\n";
                s+="\x1Bw&\x1Bw!\n";
                s+="\x1Bw!     ITEM#  DESCRIPTION\n";
                s+="\x1Bw!\n";
                s+="\x1Bw!      1000  Koala Cola    4/12-6\n";
                s+="\x1Bw!      1001  Dt Koala Cola 4/12-6\n";
                s+="\x1Bw!      1100  Lemon Lime    4/12-6\n";
                s+="\x1Bw!      1101  Orange Soda   4/12-6\n";
                s+="\x1Bw!      1200  Root Beer     4/12-6\n";
                s+="\x1Bw!      2000  Koala Cola   2/12-12\n";
                s+="\x1Bw!      2001  Dt Koala Cola2/12-12\n";
                s+="\x1Bw!      2100  Lemon Lime   2/12-12\n";
                s+="\x1Bw!      2101  Orange Soda  2/12-12\n";
                s+="\x1Bw!      2200  Root Beer    2/12-12\n";
                s+="\x1Bw!      3000  Koala Cola    8/1-2L\n";
                s+="\x1Bw!      3001  Dt Koala Cola 8/1-2L\n";
                s+="\x1Bw!      3100  Lemon Lime    8/1-2L\n";
                s+="\x1Bw!      3101  Orange Soda   8/1-2L\n";
                s+="\x1Bw!      3200  Root Beer     8/1-2L\n";
                s+="\x1Bw!\n";
                s+="\x1Bw!\x1Bw&        MISC\x1Bw!\n";
                s+="\x1Bw!\n";
                s+="\x1Bw!      9000  Empty Cans  (240)\n";
                s+="\x1Bw!      9001  Empty Can Single    \n";
                s+="\x1Bw!      9002  Empty 2L     (80)   \n";
                s+="\x1Bw!\n";
                s += "\x1Bw!\n";
                s += "\x1Bw!\n";
                s += "\x1Bw!\n";
                return s;
            }
        }
        public static string IPL_2_WalmartLabel()
        {
            //IPL
            string s = "";
            s+="<STX><SI>W406<ETX>\r\n";
            s+="<STX><ESC>C<ETX>\r\n";
            s+="<STX><ESC>P<ETX>\r\n";
            s+="<STX>E2;F2;<ETX>\r\n";
            s+="<STX>L1;f1;o809,745;l740;w4<ETX>\r\n";
            s+="<STX>L2;f1;o1012,745;l740;w4<ETX>\r\n";
            s+="<STX>L6;f0;o810,445;l203;w4<ETX>\r\n";
            s+="<STX>L7;f0;o1015,325;l152;w4<ETX>\r\n";
            s+="<STX>H8;f3;o1165,10;c61;b0;h11;w11;d3,SHIP FROM:<ETX>\r\n";
            s+="<STX>H9;f3;o1165,335;c61;b0;h11;w11;d3,SHIP TO:<ETX>\r\n";
            s+="<STX>H10;f3;o1130,10;c61;b0;h14;w14;d3,Intermec Technologies<ETX>\r\n";
            s+="<STX>H11;f3;o1095,10;c61;b0;h14;w14;d3,6001 36th Ave. West<ETX>\r\n";
            s+="<STX>H12;f3;o1060,10;c61;b0;h14;w14;d3,Everett, WA  98203<ETX>\r\n";
            s+="<STX>H13;f3;o1130,335;c61;b0;h17;w17;d3,Wal-Mart Dist Center #04<ETX>\r\n";
            s+="<STX>H14;f3;o1095,335;c61;b0;h17;w17;d3,1901 Hwy 102 East<ETX>\r\n";
            s+="<STX>H15;f3;o1060,335;c61;b0;h17;w17;d3,Bentonville, AR<ETX>\r\n";
            s+="<STX>H16;f3;o1060,580;c61;b0;h17;w17;d3,72712<ETX>\r\n";
            s+="<STX>B17;f3;o925,65;c6,0,0,3;w4;h102;d3,42072712<ETX>\r\n";
            s+="<STX>H18;f3;o980,120;c61;b0;h20;w20;d3,(420) 72712<ETX>\r\n";
            s+="<STX>H19;f3;o1010,10;c61;b0;h11;w11;d3,SHIP TO POSTAL CODE:<ETX>\r\n";
            s+="<STX>H20;f3;o1010,460;c61;b0;h11;w11;d3,CARRIER:<ETX>\r\n";
            s+="<STX>H22;f3;o965,460;c61;b0;h20;w20;d3,PRO #:<ETX>\r\n";
            s+="<STX>H23;f3;o905,460;c61;b0;h20;w20;d3,B/L #:<ETX>\r\n";
            s+="<STX>s2;S2;<ETX>\r\n";
            s+="<STX>Ma,2;q1;O0,1165;R;<ETX>\r\n";
            s+="<STX>D0<ETX>\r\n";
            s+="<STX>R;<ESC>G2<CAN><ETB><ETX>\r\n";

            return s;
        }

        public static string FP_2_WalmartLabel()
        {
            //Fingerprint:
            string s = "";
            s += "CLIP OFF\r\n";
            s += "CLIP BARCODE OFF\r\n";
            s += "XORMODE OFF\r\n";
            s += "AN 7\r\n";
            s += "NASC -2\r\n";
            s += "MAG 1,1:PP 399,750:DIR 2:FT \"Swiss 721 Bold BT\",8,0,104\r\n";
            s += "NI:PT \"SHIP FROM:\"\r\n";
            s += "DIR 4:PP 203,20:PL 770,3\r\n";
            s += "DIR 1:PP 201,407:PL 203,3\r\n";
            s += "PP 3,304:PL 198,3\r\n";
            s += "PP 203,420:PL 150,3\r\n";
            s += "PP 331,750:DIR 2:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"Intermec Technologies\"\r\n";
            s += "PP 299,750:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"6001 36th Ave. West\"\r\n";
            s += "PP 268,750:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"Everett, WA 98203\"\r\n";
            s += "PP 401,399:FT \"Swiss 721 Bold BT\",8,0,104\r\n";
            s += "NI:PT \"SHIP TO:\"\r\n";
            s += "PP 331,393:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"Wal-Mart Dist Center #04\"\r\n";
            s += "PP 300,393:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"1901 Hwy 102 East\"\r\n";
            s += "PP 268,393:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"Bentonville, AR 72712\"\r\n";
            s += "PP 192,292:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"CARRIER:\"\r\n";
            s += "PP 147,292:FT \"Swiss 721 BT\",10,0,100\r\n";
            s += "NI:PT \"PRO #:\"\r\n";
            s += "PP 102,292:FT \"Swiss 721 BT\",10,0,100\r\n";
            s += "NI:PT \"B/L #:\"\r\n";
            s += "PP 191,750:FT \"Swiss 721 BT\",8,0,104\r\n";
            s += "NI:PT \"SHIP TO POSTAL CODE:\"\r\n";
            s += "PP 111,720\r\n";
            s += "BT \"CODE128B\"\r\n";
            s += "BM 2\r\n";
            s += "BH 80\r\n";
            s += "BF OFF\r\n";
            s += "PB \" (420) 72712\"\r\n";
            s += "PP 134,694:FT \"Swiss 721 Bold BT\",7,0,150\r\n";
            s += "NI:PT \" (420) 72712\"\r\n";
            s += "PF\r\n";
            return s;
        }
        public static string smallLabel()
        {
            //Fingerprint:
            string s="";
            s+="10 PRPOS 0,0\r\n";
            s+="20 DIR 4\r\n";
            s+="30 PRPOS 300,30\r\n";
            s+="40 FONT \"Swiss 721 BT\",36\r\n";
            s+="50 PRTXT \"TEXT PRINTING Hello Printer\"\r\n";
            s+="60 PRPOS 30,280\r\n";
            s+="70 PRINTFEED 1\r\n";
            s+="RUN\r\n";
            return s;
        }
        public static string getFPcode(string szText)
        {
            string sText = szText;
            string[] txtAr = sText.Split('|');
#if USEFINGERPRINT
            sText = "";
            sText += "10 SETUP \"MEDIA,MEDIA SIZE,LENGTH,600\"";
            sText += "\r\n";
            sText += "20 ON ERROR GOTO 1000";
            sText += "\r\n";
            sText += "30 CLIP ON";
            sText += "\r\n";
            sText += "40 DIR 3";
            sText += "\r\n";
            sText += "50 FONT \"Swiss 721 BT\",20";
            sText += "\r\n";
            sText += "60 PRPOS 500,100";
            sText += "\r\n";
            sText += "70 PRTXT \"" + txtAr[0] + "\"";
            sText += "\r\n";
            sText += "80 PRPOS 500,150";
            sText += "\r\n";
            sText += "90 PRTXT \"" + txtAr[1] + "\"";
            sText += "\r\n";
            sText += "100 PRPOS 500,200";
            sText += "\r\n";
            sText += "110 PRTXT \"" + txtAr[2] + "\"";
            sText += "\r\n";

            sText += "200 PRPOS 500,350";
            sText += "\r\n";
            sText += "205 DIR 1";
            sText += "\r\n";
            sText += "210 ALIGN 9";
            sText += "\r\n";
            sText += "220 BARSET \"PDF417\",1,1,2,6,5,1,2,0,5,0";
            sText += "\r\n";
            sText += "230 PRBAR \"" + txtAr[0] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[1] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[2] + "\"";
            sText += "\r\n";

            sText += "300 PRINTFEED 1";
            sText += "\r\n";
            sText += "310 FORMFEED";
            sText += "\r\n";
            sText += "320 END";
            sText += "\r\n";
            sText += "1000 PRINT ERR$(ERR)";
            sText += "\r\n";
            sText += "1010 RESUME NEXT";
            sText += "\r\n";
            sText += "RUN";
            sText += "\r\n";
#endif
            return sText;
        }

        public static string getInitPB22()
        {
            string sText = "";
            sText = "VERBOFF\r\n";
            sText += "CLOSE\r\n";
            sText += "LED 0 OFF:LED 1 OFF\r\n";
            sText += "NEW \r\n";
            sText += "OPEN \"tmp:setup.sys\" for output as #2\r\n";
            sText += "PRINT#2, \"MEDIA,MEDIA SIZE,XSTART,0\"\r\n";
            sText += "PRINT#2, \"MEDIA,MEDIA SIZE,WIDTH,384\"\r\n";
            sText += "PRINT #2,\"MEDIA,MEDIA SIZE,LENGTH,889\"\r\n"; //756= length(cm)*72
            sText += "PRINT #2,\"MEDIA,MEDIA TYPE,LABEL (w GAPS)\"\r\n";
            sText += "PRINT #2,\"FEEDADJ,STARTADJ,0\"\r\n"; //-80
            sText += "PRINT #2,\"FEEDADJ,STOPADJ,0\"\r\n"; //80
            sText += "PRINT #2,\"PRINT DEFS,PRINT SPEED,100\"\r\n";
            sText += "PRINT #2,\"MEDIA,CONTRAST,+0%\"\r\n";
            sText += "PRINT #2, \"MEDIA,PAPER TYPE,DIRECT THERMAL\"\r\n";
            sText += "PRINT #2, \"MEDIA,PAPER TYPE,DIRECT THERMAL,LABEL CONSTANT,85\"\r\n";
            sText += "PRINT #2, \"MEDIA,PAPER TYPE,DIRECT THERMAL,LABEL FACTOR,40\"\r\n";
            sText += "PRINT #2, \"RFID,OFF\" \r\n";
            sText += "CLOSE #2\r\n";
            sText += "Setup \"tmp:setup.sys\"\r\n";
            sText += "Kill \"tmp:setup.sys\"\r\n";
            sText += "new\r\n";
            //
            sText += "OPEN \"tmp:CUT\" FOR OUTPUT AS #2";
            sText += "\r\n";
            sText += "PRINT #2,\"0\"";
            sText += "\r\n";
            sText += "CLOSE #2";
            sText += "\r\n";
            sText += "open \"tmp:PAUSE\" for OUTPUT as #3";
            sText += "\r\n";
            sText += "PRINT #3,\"1\"";
            sText += "\r\n";
            sText += "PRINT #3,\"1\"";
            sText += "\r\n";
            sText += "CLOSE #3";
            sText += "\r\n";
            sText += "PRINT KEY OFF";
            sText += "\r\n";
            sText += "CUT OFF";
            sText += "\r\n";
            sText += "new";
            sText += "\r\n";
            sText += "VERBOFF";
            sText += "\r\n";
            sText += "OPTIMIZE \"BATCH\" ON";
            sText += "\r\n";
            sText += "OPTIMIZE \"PRINT\" OFF";
            sText += "\r\n";
            sText += "RIBBON SAVE OFF";
            sText += "\r\n";
            sText += "STORE OFF";
            sText += "\r\n";
            sText += "Close #1:open \"console:\" for output as #1";
            sText += "\r\n";
            sText += "print #1:print #1:Close #1";
            sText += "\r\n";

            return sText;
        }
        public static string getFPCode22(string szText)
        {
            string sText = szText;
            string[] txtAr = szText.Split('|');

            int iLeft = 10;
            int iRowX = 10;
            int iRowDiff = 60;
            sText = "";
            sText += "10 KEY(15) ON:KEY(19) ON";
            sText += "\r\n";
            sText += "20 open \"tmp:PAUSE\" for INPUT as #3";
            sText += "\r\n";
            sText += "30 INPUT #3,P$:PAUSE%=VAL(P$)";
            sText += "\r\n";
            sText += "40 CLOSE #3";
            sText += "\r\n";
            sText += "50 ON KEY(15) GOSUB 777777:ON KEY(19) GOSUB 777776";
            sText += "\r\n";
            sText += "60 CLOSE";
            sText += "\r\n";
            sText += "70 open \"console:\" for output as #1";
            sText += "\r\n";
            sText += "80 print #1, chr$(155) + \"1;H\";:print #1,\"BUSY           \":CLOSE #1";
            sText += "\r\n";
            sText += "90 TROFF";
            sText += "\r\n";
            sText += "100 BARADJUST 1000,1000";
            sText += "\r\n";
            sText += "110 ON ERROR GOTO 150";
            sText += "\r\n";
            sText += "120 goto 600";
            sText += "\r\n";
            sText += "150 open \"console:\" for output as #1";
            sText += "\r\n";
            sText += "155 IF ERR=20 THEN GOTO 175";
            sText += "\r\n";
            sText += "156 IF ERR=1031 THEN GOTO 190";
            sText += "\r\n";
            sText += "157 IF ERR=1005 THEN GOTO 190";
            sText += "\r\n";
            sText += "158 IF ERR=1022 THEN GOTO 190";
            sText += "\r\n";
            sText += "159 IF ERR=1027 THEN GOTO 190";
            sText += "\r\n";
            sText += "160 print #1, chr$(155) + \"2;H\";:print #1,\"ERROR: \";err;";
            sText += "\r\n";
            sText += "165 CLOSE #1:LED 1 ON";
            sText += "\r\n";
            sText += "170 sound 800,20:resume next";
            sText += "\r\n";
            sText += "175 print #1,\"Line > 250 char\";chr$(13);";
            sText += "\r\n";
            sText += "180 CLOSE #1:LED 1 ON";
            sText += "\r\n";
            sText += "185 sound 800,20:resume next";
            sText += "\r\n";
            sText += "190 print #1, chr$(155) + \"2;H\";:print #1,\"ERROR: \";err;";
            sText += "\r\n";
            sText += "195 CLOSE #1 :LED 1 ON";
            sText += "\r\n";
            sText += "200 sound 800,20:BUSY1";
            sText += "\r\n";
            sText += "205 STATUS% = PRSTAT";
            sText += "\r\n";
            sText += "206 IF (STATUS% AND 1) THEN GOTO 205";
            sText += "\r\n";
            sText += "207 IF (STATUS% AND 4) THEN GOTO 205";
            sText += "\r\n";
            sText += "208 IF (STATUS% AND 8) THEN GOTO 205";
            sText += "\r\n";
            sText += "209 LED 1 OFF";
            sText += "\r\n";
            sText += "210 READY1:FF:FF:PF:resume next";
            sText += "\r\n";
            sText += "600 LED 1 OFF";
            sText += "\r\n";
            sText += "1010 GOSUB 10000";
            sText += "\r\n";
            sText += "1020 FOR A%=1 TO 1";
            sText += "\r\n";
            sText += "1030 IF PAUSE% < 0 THEN GOTO 1030";
            sText += "\r\n";
            sText += "1064 PF";
            sText += "\r\n";
            sText += "1090 next A%";
            sText += "\r\n";
            sText += "1100 open \"console:\" for output as #1";
            sText += "\r\n";
            sText += "1110 print #1, VERSION$";
            sText += "\r\n";
            sText += "1120 CLOSE #1";
            sText += "\r\n";
            sText += "1130 END";
            sText += "\r\n";
            sText += "10000 CLIP OFF";
            sText += "\r\n";
            sText += "10010 CLIP BARCODE OFF";
            sText += "\r\n";
            sText += "10020 XORMODE OFF";
            sText += "\r\n";
            sText += "10040 AN 7";
            sText += "\r\n";
            sText += "10050 NASC -2";
            sText += "\r\n";
            //first text line
            sText += "10060 MAG 1,1:PP " + iRowX.ToString() + "," + iLeft.ToString() + ":DIR 4:FT \"Swiss 721 BT\",20,0,101";
            iRowX += iRowDiff;
            sText += "\r\n";
            sText += "10070 NI:PT \"" + txtAr[0] + "\"";
            sText += "\r\n";
            sText += "10090 PP " + iRowX.ToString() + "," + iLeft.ToString() + ":FT \"Swiss 721 BT\",20,0,101";
            sText += "\r\n";
            iRowX += iRowDiff;
            sText += "10100 NI:PT \"" + txtAr[1] + "\"";
            sText += "\r\n";
            sText += "10120 PP " + iRowX.ToString() + "," + iLeft.ToString() + ":FT \"Swiss 721 BT\",20,0,102";
            sText += "\r\n";
            iRowX += iRowDiff;
            sText += "10130 NI:PT \"" + txtAr[2] + "\"";
            sText += "\r\n";
            sText += "10150 PP 200,500";
            sText += "\r\n";
            sText += "10160 BARSET \"PDF417\",1,1,2,6,2,1,2,0,2,0";
            sText += "\r\n";
            sText += "10170 BF OFF";
            sText += "\r\n";
            sText += "10180 PRBAR \"" + txtAr[0] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[1] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[2] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[3] + "\"+CHR$(13)+CHR$(10)+\"" + txtAr[4] + "\"";
            // print barcode with prename, name, time, company, email
            sText += "\r\n";
            sText += "10200 PP " + iRowX.ToString() + "," + iLeft.ToString() + ":FT \"Swiss 721 BT\",18,0,102";
            sText += "\r\n";
            iRowX += iRowDiff;
            sText += "10210 NI:PT \"" + txtAr[3] + "\"";
            sText += "\r\n";
            sText += "10220 RETURN";
            sText += "\r\n";
            sText += "777776 FORMFEED:return";
            sText += "\r\n";
            sText += "777777 PAUSE% = PAUSE% * -1:open \"tmp:PAUSE\" for OUTPUT as #3:PRINT #3,STR$(PAUSE%):CLOSE #3:return";
            sText += "\r\n";
            sText += "Run";
            sText += "\r\n";
            //sText += "LED 0 ON";
            //sText += "\r\n";
            //sText += "VERBON";
            //sText += "\r\n";
            //sText += "PRINT KEY OFF";
            //sText += "\r\n";
            //sText += "CUT OFF";
            //sText += "\r\n";
            return sText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sOldPrice"></param>
        /// <param name="sNewPrice"></param>
        /// <param name="iLang">not used</param>
        /// <returns></returns>
        public static string getPriceMark(string sOldPrice, string sNewPrice, int iLang)
        {
            StringBuilder sb = new StringBuilder();
            string sReducedText = "...reduziert";
            sb.Append("LAYOUT RUN \"c:PRICE_MARKDOWN.LAY\"");
            sb.Append("\r\n");
            sb.Append("INPUT ON");
            sb.Append("\r\n");
            //sb.Append("...stark reduziert");
            sb.Append("" + sReducedText);  //starts with 0x02
            sb.Append("\r\n");
            //sb.Append("9,95");
            sb.Append(sOldPrice);
            sb.Append("\r\n");
            //sb.Append("2,85");
            sb.Append(sNewPrice);
            sb.Append("\r\n");
            sb.Append("76589");
            sb.Append("\r\n");
            //if(iLang==0)
            sb.Append("€");
            sb.Append("\r\n");
            sb.Append("");             //ends with 0x03
            sb.Append("\r\n");
            sb.Append("PF");
            sb.Append("\r\n");
            sb.Append("PRINT KEY OFF");
            sb.Append("\r\n");

            return sb.ToString();
        }
        public static string getLayouPRN()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SYSVAR(18)=0");
            sb.Append("INPUT OFF");
            sb.Append("NEW ");
            sb.Append("OPEN \"tmp:setup.sys\" for output as #2");
            sb.Append("PRINT#2, \"MEDIA,MEDIA SIZE,LENGTH,400\"");
            sb.Append("PRINT#2, \"MEDIA,MEDIA SIZE,XSTART,0\"");
            sb.Append("PRINT#2, \"MEDIA,MEDIA SIZE,WIDTH,420\"");
            sb.Append("PRINT#2, \"MEDIA,MEDIA TYPE,LABEL (w GAPS)\"");
            sb.Append("PRINT#2, \"FEEDADJ,STARTADJ,-40\"");
            sb.Append("PRINT#2, \"FEEDADJ,STOPADJ,0\"");
            sb.Append("PRINT#2, \"PRINT DEFS,PRINT SPEED,75\"");
            sb.Append("PRINT#2, \"MEDIA,PAPER TYPE,DIRECT THERMAL\"");
            sb.Append("PRINT#2, \"MEDIA,PAPER TYPE,DIRECT THERMAL,LABEL CONSTANT,85\"");
            sb.Append("PRINT#2, \"MEDIA,PAPER TYPE,DIRECT THERMAL,LABEL FACTOR,40\"");
            sb.Append("PRINT#2, \"MEDIA,CONTRAST,+0%\"");
            sb.Append("PRINT#2, \"RFID,OFF\"");
            sb.Append("CLOSE #2");
            sb.Append("Setup \"tmp:setup.sys\"");
            sb.Append("Kill \"tmp:setup.sys\"");
            sb.Append("OPTIMIZE \"BATCH\" ON");
            sb.Append("LTS& OFF");
            sb.Append("SETUP KEY OFF");
            sb.Append("SYSVAR(48)=0");
            sb.Append("FORMAT INPUT CHR$(2),CHR$(3),CHR$(13)");
            sb.Append("LAYOUT INPUT \"tmp:LBLSOFT.LAY\"");
            sb.Append("CLIP ON");
            sb.Append("CLIP BARCODE ON");
            sb.Append("XORMODE OFF");
            sb.Append("qFnt1$=\"Swiss 721 BT\"");
            sb.Append("qFnt2$=\"Swiss 721 Bold BT\"");
            sb.Append("NASC 8");
            sb.Append("DIR 1");
            sb.Append("AN 1");
            sb.Append("PP 3,12:PX 256,375,4");
            sb.Append("PP 3,140:PL 180,184");
            sb.Append("AN 5");
            sb.Append("PP 95,130:FT qFnt1$,14,0,100:II");
            sb.Append("PRBOX 140,180,0,VAR1$,2,2,\"\",\"\"");
            sb.Append("AN 2");
            sb.Append("PP 270,210:FT qFnt1$,12,0,100:NI:PT VAR2$");
            sb.Append("PRINT PRSTAT(3),PRSTAT(5),PRSTAT(6)");
            sb.Append("AN 1");
            sb.Append("PP PRSTAT(3)-2,213:PRDIAGONAL PRSTAT(5)+1,PRSTAT(6)-16,2,\"R\"");
            sb.Append("PP PRSTAT(3),212:FT qFnt1$,10,0,100:NI:PT VAR5$");
            sb.Append("AN 2");
            sb.Append("PP 265,140:FT qFnt2$,18,0,100:NI:PT VAR3$");
            sb.Append("PRINT PRSTAT(3)");
            sb.Append("AN 1");
            sb.Append("PP PRSTAT(3)+PRSTAT(5),140:FT qFnt1$,15,0,100:NI:PT VAR5$");
            sb.Append("AN 2");
            sb.Append("PP 185,30:BARSET \"CODE128\",1,1,3,90");
            sb.Append("BARFONT OFF");
            sb.Append("PB VAR4$");
            sb.Append("AN 1");
            sb.Append("DIR 4");
            sb.Append("PP 375,20:FT qFnt1$,10,0,100:NI:PT VAR4$");
            sb.Append("LAYOUT END");
            sb.Append("COPY \"TMP:LBLSOFT.LAY\",\"C:PRICE_MARKDOWN.LAY\"");
            sb.Append("LAYOUT RUN \"C:PRICE_MARKDOWN.LAY\"");
            sb.Append("INPUT ON");
            return sb.ToString();
        }
    }
}
