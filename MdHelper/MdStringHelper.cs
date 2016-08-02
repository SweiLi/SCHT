using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace BDH.MdHelper
{
    public class MdStringHelper
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(string str)
        {
            return (!string.IsNullOrWhiteSpace(str));
        }

        /// <summary>
        /// 检查字符串中是否包含非法字符 ,‘&%+、=！
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool CheckValidity(string s)
        {
            return s.IndexOf("'") <= 0 && s.IndexOf("&") <= 0 && s.IndexOf("%") <= 0 && s.IndexOf("+") <= 0 && s.IndexOf("\"") <= 0 && s.IndexOf("=") <= 0 && s.IndexOf("!") <= 0;
        }
        /// <summary>
        /// 把价格精确至小数点两位 
        /// </summary>
        /// <param name="dPrice"></param>
        /// <returns></returns>
        public static string TransformPrice(double dPrice)
        {
            double num = dPrice;
            NumberFormatInfo provider = new NumberFormatInfo
            {
                NumberNegativePattern = 2
            };
            return num.ToString("N", provider);
        }
        /// <summary>
        /// 检测含有中文字符串的实际长度 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetLength(string str)
        {
            ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] bytes = aSCIIEncoding.GetBytes(str);
            int num = 0;
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63)
                {
                    num++;
                }
                num++;
            }
            return num;
        }
        /// <summary>
        /// 截取长度,num是英文字母的总数，一个中文算两个英文 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="iNum"></param>
        /// <param name="bAddDot"></param>
        /// <returns></returns>
        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            if (str == null || iNum <= 0)
            {
                return "";
            }
            if (str.Length < iNum && str.Length * 2 < iNum)
            {
                return str;
            }
            char[] array = str.ToCharArray(0, (str.Length >= iNum) ? iNum : str.Length);
            int num = 0;
            int num2 = 0;
            char[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                char c = array2[i];
                num2++;
                int num3 = (int)c;
                if (num3 > 127 || num3 < 0)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > iNum)
                {
                    num2--;
                    break;
                }
                if (num == iNum)
                {
                    break;
                }
            }
            string result;
            if (num2 < str.Length && bAddDot)
            {
                result = str.Substring(0, num2 - 3) + "...";
            }
            else
            {
                result = str.Substring(0, num2);
            }
            return result;
        }
        /// <summary>
        /// 获取日期字符串 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime dt)
        {
            return dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
        }
        /// <summary>
        /// 根据指定字符，截取相应字符串 
        /// </summary>
        /// <param name="sOrg"></param>
        /// <param name="sLast"></param>
        /// <returns></returns>

        public static string GetStrByLast(string sOrg, string sLast)
        {
            int num = sOrg.LastIndexOf(sLast);
            if (num > 0)
            {
                return sOrg.Substring(num + 1);
            }
            return sOrg;
        }
        /// <summary>
        /// 根据指定字符，截取相应字符串 
        /// </summary>
        /// <param name="sOrg"></param>
        /// <param name="sLast"></param>
        /// <returns></returns>
        public static string GetPreStrByLast(string sOrg, string sLast)
        {
            int num = sOrg.LastIndexOf(sLast);
            if (num > 0)
            {
                return sOrg.Substring(0, num);
            }
            return sOrg;
        }
        /// <summary>
        /// 根据指定字符，截取相应字符串 
        /// </summary>
        /// <param name="sOrg"></param>
        /// <param name="sEnd"></param>
        /// <returns></returns>
        public static string RemoveEndWith(string sOrg, string sEnd)
        {
            if (sOrg.EndsWith(sEnd))
            {
                sOrg = sOrg.Remove(sOrg.IndexOf(sEnd), sEnd.Length);
            }
            return sOrg;
        }
        /// <summary>
        /// 清除HTML标记  
        /// </summary>
        /// <param name="sHtml"></param>
        /// <returns></returns>
        public static string ClearTag(string sHtml)
        {
            if (sHtml == "")
            {
                return "";
            }
            Regex regex = new Regex("(<[^>\\s]*\\b(\\w)+\\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\\r|\\n|\\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return regex.Replace(sHtml, "");
        }
        /// <summary>
        /// 根据正则清除HTML标记 
        /// </summary>
        /// <param name="sHtml"></param>
        /// <param name="sRegex"></param>
        /// <returns></returns>
        public static string ClearTag(string sHtml, string sRegex)
        {
            Regex regex = new Regex(sRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return regex.Replace(sHtml, "");
        }
        /// <summary>
        /// 转化成JS 
        /// </summary>
        /// <param name="sHtml"></param>
        /// <returns></returns>
        public static string ConvertToJS(string sHtml)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Regex regex = new Regex("\\r\\n", RegexOptions.IgnoreCase);
            string[] array = regex.Split(sHtml);
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text = array2[i];
                stringBuilder.Append("document.writeln(\"" + text.Replace("\"", "\\\"") + "\");\r\n");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 替换空格 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceNbsp(string str)
        {
            string text = str;
            if (text.Length > 0)
            {
                text = text.Replace(" ", "");
                text = text.Replace("&nbsp;", "");
                text = "&nbsp;&nbsp;&nbsp;&nbsp;" + text;
            }
            return text;
        }
        /// <summary>
        /// 字符串转HTML 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {
            string text = str;
            if (text.Length > 0)
            {
                text = text.Replace('\r'.ToString(), "<br>");
                text = text.Replace(" ", "&nbsp;");
                text = text.Replace("\u3000", "&nbsp;&nbsp;");
            }
            return text;
        }
        /// <summary>
        /// 截取长度并转换为HTML 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string AcquireAssignString(string str, int num)
        {
            string letter = MdStringHelper.GetLetter(str, num, false);
            return MdStringHelper.StringToHtml(letter);
        }

        public static string TranslateToHtmlString(string str, int num)
        {
            string letter = MdStringHelper.GetLetter(str, num, false);
            return MdStringHelper.StringToHtml(letter);
        }
        /// <summary>
        /// 删除所有的html标记 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelHtmlString(string str)
        {
            string[] array = new string[]
            {
                "<script[^>]*?>.*?</script>",
                "<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(\\\\[\"'tbnr]|[^\\7])*?\\7|\\w+)|.{0})|\\s)*?(\\/\\s*)?>",
                "([\\r\\n])[\\s]+",
                "&(quot|#34);",
                "&(amp|#38);",
                "&(lt|#60);",
                "&(gt|#62);",
                "&(nbsp|#160);",
                "&(iexcl|#161);",
                "&(cent|#162);",
                "&(pound|#163);",
                "&(copy|#169);",
                "&#(\\d+);",
                "-->",
                "<!--.*\\n"
            };
            string[] array2 = new string[]
            {
                "",
                "",
                "",
                "\"",
                "&",
                "<",
                ">",
                " ",
                "¡",
                "¢",
                "£",
                "©",
                "",
                "\r\n",
                ""
            };
            string text = str;
            for (int i = 0; i < array.Length; i++)
            {
                text = new Regex(array[i], RegexOptions.IgnoreCase | RegexOptions.Multiline).Replace(text, array2[i]);
            }
            return text.Replace("<", "").Replace(">", "").Replace("\r\n", "");
        }
        /// <summary>
        /// 删除字符串中的特定标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tag"></param>
        /// <param name="isContent"></param>
        /// <returns></returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }
            if (isContent)
            {
                return Regex.Replace(str, string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag), "", RegexOptions.IgnoreCase);
            }
            return Regex.Replace(str, string.Format("(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 删除字符串中的一组标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tagA"></param>
        /// <param name="isContent"></param>
        /// <returns></returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {
            string[] source = tagA.Split(new char[]
            {
                ','
            });
            return source.Aggregate(str, (string current, string sr1) => MdStringHelper.DelTag(current, sr1, isContent));
        }
        /// <summary>
        /// 格式化为版本号字符串 
        /// </summary>
        /// <param name="sVersion"></param>
        /// <returns></returns>
        public static string SetVersionFormat(string sVersion)
        {
            if (string.IsNullOrEmpty(sVersion))
            {
                return "";
            }
            int num = 0;
            int num2 = 0;
            while (num < 4 && num2 > -1)
            {
                num2 = sVersion.IndexOf(".", num2 + 1);
                num++;
            }
            return (num2 > 0) ? sVersion.Substring(0, num2) : sVersion;
        }
        /// <summary>
        /// 在前面补0 
        /// </summary>
        /// <param name="sheep"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string AddZero(int sheep, int length)
        {
            return MdStringHelper.AddZero(sheep.ToString(), length);
        }

        public static string AddZero(string sheep, int length)
        {
            StringBuilder stringBuilder = new StringBuilder(sheep);
            for (int i = stringBuilder.Length; i < length; i++)
            {
                stringBuilder.Insert(0, "0");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 简介：获得唯一的字符串 
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString()
        {
            Random random = new Random();
            return ((int)(random.NextDouble() * 10000.0)).ToString() + DateTime.Now.Ticks.ToString();
        }
        /// <summary>
        /// 获得干净,无非法字符的字符串 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetCleanJsString(string str)
        {
            str = str.Replace("\"", "“");
            str = str.Replace("'", "”");
            str = str.Replace("\\", "\\\\");
            Regex regex = new Regex("\\r|\\n|\\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = regex.Replace(str, " ");
            return str;
        }

        public static string GetCleanJsString2(string str)
        {
            str = str.Replace("\"", "\\\"");
            Regex regex = new Regex("\\r|\\n|\\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = regex.Replace(str, " ");
            return str;
        }
        /// <summary>
        /// 将原始字串转换为unicode,格式为\u.\u. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToUnicode(string str)
        {
            string text = "";
            if (!string.IsNullOrEmpty(str))
            {
                text = str.Aggregate(text, delegate (string current, char t)
                {
                    string arg_14_1 = "\\u";
                    int num = (int)t;
                    return current + arg_14_1 + num.ToString("x");
                });
            }
            return text;
        }
        /// <summary>
        /// 将Unicode字串\u.\u.格式字串转换为原始字符串 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToString(string str)
        {
            string text = "";
            str = Regex.Replace(str, "[\r\n]", "", RegexOptions.None);
            if (!string.IsNullOrEmpty(str))
            {
                string[] array = str.Replace("\\u", "㊣").Split(new char[]
                {
                    '㊣'
                });
                try
                {
                    text += array[0];
                    for (int i = 1; i < array.Length; i++)
                    {
                        string text2 = array[i];
                        if (!string.IsNullOrEmpty(text2) && text2.Length >= 4)
                        {
                            text2 = array[i].Substring(0, 4);
                            text += (char)int.Parse(text2, NumberStyles.HexNumber);
                            text += array[i].Substring(4);
                        }
                    }
                }
                catch (FormatException)
                {
                    text += "Erorr";
                }
            }
            return text;
        }
        /// <summary>
        /// GB2312转换成unicode编码 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2Unicode(string str)
        {
            string text = "";
            Encoding encoding = Encoding.GetEncoding("GB2312");
            byte[] bytes = encoding.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                string str2 = "%" + bytes[i].ToString("X");
                text += str2;
            }
            return text;
        }
        /// <summary>
        /// 获取字节
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static ushort GetByte(char ch)
        {
            char c = ch;
            ushort result;
            switch (c)
            {
                case 'A':
                    break;
                case 'B':
                    goto IL_4B;
                case 'C':
                    goto IL_50;
                case 'D':
                    goto IL_55;
                case 'E':
                    goto IL_5A;
                case 'F':
                    goto IL_5F;
                default:
                    switch (c)
                    {
                        case 'a':
                            break;
                        case 'b':
                            goto IL_4B;
                        case 'c':
                            goto IL_50;
                        case 'd':
                            goto IL_55;
                        case 'e':
                            goto IL_5A;
                        case 'f':
                            goto IL_5F;
                        default:
                            result = ushort.Parse(ch.ToString());
                            return result;
                    }
                    break;
            }
            result = 10;
            return result;
        IL_4B:
            result = 11;
            return result;
        IL_50:
            result = 12;
            return result;
        IL_55:
            result = 13;
            return result;
        IL_5A:
            result = 14;
            return result;
        IL_5F:
            result = 15;
            return result;
        }
        /// <summary>
        /// 转换一个字符，输入如"Π"中的"03a0" 
        /// </summary>
        /// <param name="unicodeSingle"></param>
        /// <returns></returns>
        public static string ConvertSingle(string unicodeSingle)
        {
            if (unicodeSingle.Length != 4)
            {
                return null;
            }
            Encoding unicode = Encoding.Unicode;
            byte[] array = new byte[2];
            byte[] array2 = array;
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            byte[] expr_49_cp_0 = array2;
                            int expr_49_cp_1 = 1;
                            expr_49_cp_0[expr_49_cp_1] += (byte)(MdStringHelper.GetByte(unicodeSingle[i]) * 16);
                            break;
                        }
                    case 1:
                        {
                            byte[] expr_6F_cp_0 = array2;
                            int expr_6F_cp_1 = 1;
                            expr_6F_cp_0[expr_6F_cp_1] += (byte)MdStringHelper.GetByte(unicodeSingle[i]);
                            break;
                        }
                    case 2:
                        {
                            byte[] expr_92_cp_0 = array2;
                            int expr_92_cp_1 = 0;
                            expr_92_cp_0[expr_92_cp_1] += (byte)(MdStringHelper.GetByte(unicodeSingle[i]) * 16);
                            break;
                        }
                    case 3:
                        {
                            byte[] expr_B8_cp_0 = array2;
                            int expr_B8_cp_1 = 0;
                            expr_B8_cp_0[expr_B8_cp_1] += (byte)MdStringHelper.GetByte(unicodeSingle[i]);
                            break;
                        }
                }
            }
            char[] array3 = new char[unicode.GetCharCount(array2, 0, array2.Length)];
            unicode.GetChars(array2, 0, array2.Length, array3, 0);
            return new string(array3);
        }
        /// <summary>
        /// unicode编码转换成GB2312汉字 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UtoGB(string str)
        {
            string[] array = str.Replace("\\", "").Split(new char[]
            {
                'u'
            });
            byte[] array2 = new byte[array.Length - 1];
            for (int i = 1; i < array.Length; i++)
            {
                array2[i - 1] = Convert.ToByte(MdStringHelper.Convert2Hex(array[i]));
            }
            char[] chars = Encoding.GetEncoding("GB2312").GetChars(array2);
            string text = "";
            for (int j = 0; j < chars.Length; j++)
            {
                text += chars[j].ToString();
            }
            return text;
        }
        /// <summary>
        /// 转化成16进制
        /// </summary>
        /// <param name="pstr"></param>
        /// <returns></returns>
        private static string Convert2Hex(string pstr)
        {
            if (pstr.Length == 2)
            {
                pstr = pstr.ToUpper();
                return ("0123456789ABCDEF".IndexOf(pstr.Substring(0, 1)) * 16 + "0123456789ABCDEF".IndexOf(pstr.Substring(1, 1))).ToString();
            }
            return "";
        }
    }
}
