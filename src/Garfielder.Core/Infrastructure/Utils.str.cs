using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Garfielder.Core.Infrastructure
{
    /// <summary>
    /// String utilities
    /// </summary>
    public static partial class Utils
    {
        #region member variables
        /// <summary>
        /// 52 letters
        /// </summary>
        public static char[] LETTERS =  
      {  
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',  
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'  
      };
        /// <summary>
        /// 10 Arabic numerals
        /// </summary>
        public static char[] DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public static char[] LETTERS_DIGITS = LETTERS.Union(DIGITS).ToArray();
        #endregion

        #region public methods
        /// <summary>
        /// derange email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string DerangeEmail(this string email)
        {
            if (email.Length < 2)
                return email;
            string str = email.Substring(0, 2);
            for (int i = 2; i < email.Length; i++)
            {
                if (email.Substring(i, 1).ToString() == "@")
                {

                    str += email.Substring(i, email.Length - i);
                    break;
                }
                else
                    str += "*";
            }
            email = str;
            return email;
        }
        /// <summary>
        /// Generate a string automatically
        /// </summary>
        /// <returns></returns>
        public static string RandomStr()
        {
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            Random random1 = new Random();
            StringBuilder retVal = new StringBuilder();
            retVal.AppendFormat("{0}{1}{2}", (Environment.TickCount & int.MaxValue).ToString(), random.Next(1000, 9999).ToString(), LETTERS[random1.Next(LETTERS.Length)].ToString());
            return retVal.ToString();
        }
        /// <summary>
        /// 生成数字和字母组成的随机数
        /// </summary>
        /// <param name="size">随机数长度</param>
        /// <param name="plusTimeStamp">是否后面带上时间戳</param>
        /// <returns></returns>
        public static string RandomStr(int size, bool plusTimeStamp = false) { 
            var size0=5;
            size = size < 1 ? size0 : size;
            size = size > LETTERS_DIGITS.Length ? size0 : size;
            var obj = new List<string>();
            var rd=new Random();
            for (var i = 0; i < size; i++) {
                var rnum = (int)Math.Floor(rd.NextDouble() * LETTERS_DIGITS.Length);
                obj.Add(LETTERS_DIGITS[rnum].ToString());
            };
            if (plusTimeStamp) {
                obj.Add(DateTime.Now.Ticks.ToString());
            };
            return String.Join("", obj.ToArray());
        }
        /// <summary> 
        /// 转换金额为人民币大写 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string ToRMBUpper(this double num)
        {
            string strUpperMum = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string strNumUnit = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string strOfNum = "";    //从原num值中取出的值 
            string strNum = "";    //数字的字符串形式 
            string strReturnUpper = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int sumLength;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            strNum = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            sumLength = strNum.Length;      //找出最高位 
            if (sumLength > 15) { return "溢出"; }
            strNumUnit = strNumUnit.Substring(15 - sumLength);   //取出对应位数的strNumUnit的值。如：200.55,sumLength为5所以strNumUnit=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < sumLength; i++)
            {
                strOfNum = strNum.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(strOfNum);      //转换为数字 
                if (i != (sumLength - 3) && i != (sumLength - 7) && i != (sumLength - 11) && i != (sumLength - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (strOfNum == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (strOfNum != "0" && nzero != 0)
                        {
                            ch1 = String.Format("零{0}", strUpperMum.Substring(temp * 1, 1));
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = strUpperMum.Substring(temp * 1, 1);
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (strOfNum != "0" && nzero != 0)
                    {
                        ch1 = String.Format("零{0}", strUpperMum.Substring(temp * 1, 1));
                        ch2 = strNumUnit.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (strOfNum != "0" && nzero == 0)
                        {
                            ch1 = strUpperMum.Substring(temp * 1, 1);
                            ch2 = strNumUnit.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (strOfNum == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (sumLength >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = strNumUnit.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (sumLength - 11) || i == (sumLength - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = strNumUnit.Substring(i, 1);
                }

                if (i == sumLength - 1 && strOfNum == "0")
                    //最后一位（分）为0时，加上“整” 
                    strReturnUpper = String.Format("{0}整", strReturnUpper + ch1 + ch2);
                else
                    strReturnUpper = strReturnUpper + ch1 + ch2;
            }
            if (num == 0)
            {
                strReturnUpper = "零元整";
            }
            return strReturnUpper;
        }// /ToRMBUpper
        public static bool InArray(this string val, string[] targetStrArray)
        {
            if (targetStrArray.Length == 0) return false;
            bool retVal = false;
            foreach (string item in targetStrArray)
            {
                if (item == val)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }
        /// <summary>
        /// strips all illegal characters in a path string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveIllegalPathChars(this string pathText)
        {
            if (string.IsNullOrEmpty(pathText)) return pathText;
            var chars = new List<char>();
            var charsExt = new char[] { '\\', '/', '-', '_', '.' };
            chars.AddRange(LETTERS);
            chars.AddRange(DIGITS);
            chars.AddRange(charsExt);

            var rawChars = pathText.ToCharArray();
            rawChars.ToList().ForEach(x =>
            {
                if (!chars.Contains(x))
                {
                    pathText = pathText.Replace(x.ToString(), string.Empty);
                }
            });
            return pathText;
        }


        private static readonly Regex STRIP_HTML = new Regex("<[^>]*>", RegexOptions.Compiled);
        /// <summary>
        /// Strips all HTML tags from the specified string.
        /// </summary>
        /// <param name="html">The string containing HTML</param>
        /// <returns>A string without HTML tags</returns>
        public static string StripHtml(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return STRIP_HTML.Replace(html, string.Empty);
        }

        private static readonly Regex REGEX_BETWEEN_TAGS = new Regex(@">\s+", RegexOptions.Compiled);
        private static readonly Regex REGEX_LINE_BREAKS = new Regex(@"\n\s+", RegexOptions.Compiled);
        private static readonly Regex REGEX_ALPHANUMBERUNDERLINE = new Regex(@"^[A-Za-z0-9_]+$", RegexOptions.Compiled);

        /// <summary>
        /// Removes the HTML whitespace.
        /// </summary>
        /// <param name="html">The HTML.</param>
        public static string RemoveHtmlWhitespace(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            html = REGEX_BETWEEN_TAGS.Replace(html, "> ");
            html = REGEX_LINE_BREAKS.Replace(html, string.Empty);

            return html.Trim();
        }
        /// <summary>
        /// the string is consisted of alpha\number\underline
        /// </summary>
        /// <returns></returns>
        public static bool IsAlphaNumUnderline(this string str)
        {
            var retVal = REGEX_ALPHANUMBERUNDERLINE.IsMatch(str);
            return retVal;
        }
        /// <summary>
        /// replace back-slash to slash
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string BackslashToSlash(this string str)
        {
            return str.Replace(@"\", "/");
        }
        /// <summary>
        /// replace slash to back-slash
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SlashToBackslash(this string str)
        {
            return str.Replace("/", @"\");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern">Regex pattern</param>
        /// <returns></returns>
        public static bool IsMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// Get a string's length by byte.一个汉字两个字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetLengthByByte(this string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string TailStr(this string p_SrcString, int p_Length, string p_TailString)
        {
            return p_SrcString.TailStr(0, p_Length, p_TailString);
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string TailStr(this string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            //2，非日文或者韩文
            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }//endof TailString
        public static string Pluralize(this string str)
        {
            return Inflector.Pluralize(str);
        }
        public static string Singularize(this string str)
        {
            return Inflector.Singularize(str);
        }
        /// <summary>
        /// Encrypts a string using the SHA256 algorithm.
        /// </summary>
        public static string HashPassword(string plainMessage)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainMessage);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(sha.Hash);
            }
        }
        #endregion


    }
}
