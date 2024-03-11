using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpBoxes.Validation
{
    /// <summary>
    /// FormatValidationHelper 是一个静态类，提供了一系列的格式验证方法。
    /// 这些方法可以用于验证字符串是否满足特定的格式，例如是否为电子邮件，是否为URL，是否为日期等。
    /// </summary>
    public static class FormatValidationHelper
    {
        /// <summary>
        /// 检查给定的字符串是否为有效的电子邮件地址。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串是有效的电子邮件地址，则为 true；否则为 false。</returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(
                source,
                @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串中是否包含有效的电子邮件地址。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串中包含有效的电子邮件地址，则为 true；否则为 false。</returns>
        public static bool HasEmail(string source)
        {
            return Regex.IsMatch(
                source,
                @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的URL。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串是有效的URL，则为 true；否则为 false。</returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(
                source,
                @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串中是否包含有效的URL。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串中包含有效的URL，则为 true；否则为 false。</returns>
        public static bool HasUrl(string source)
        {
            return Regex.IsMatch(
                source,
                @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的日期时间格式。
        /// 使用 DateTime.TryParse 方法进行验证。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串是有效的日期时间格式，则为 true；否则为 false。</returns>
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的手机号码。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串是有效的手机号码，则为 true；否则为 false。</returns>
        public static bool IsMobile(string source)
        {
            return Regex.IsMatch(source, @"^1[35]\d{9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 检查给定的字符串中是否包含有效的手机号码。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串中包含有效的手机号码，则为 true；否则为 false。</returns>
        public static bool HasMobile(string source)
        {
            return Regex.IsMatch(source, @"1[35]\d{9}", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的IP地址。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串是有效的IP地址，则为 true；否则为 false。</returns>
        public static bool IsIP(string source)
        {
            return Regex.IsMatch(
                source,
                @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串中是否包含有效的IP地址。
        /// 使用正则表达式进行匹配。
        /// </summary>
        /// <param name="source">要检查的字符串。</param>
        /// <returns>如果字符串中包含有效的IP地址，则为 true；否则为 false。</returns>
        public static bool HasIP(string source)
        {
            return Regex.IsMatch(
                source,
                @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])",
                RegexOptions.IgnoreCase
            );
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的IP地址。
        /// 使用字符串分割和转换方法进行验证。
        /// </summary>
        /// <param name="ip">要检查的字符串。</param>
        /// <returns>如果字符串是有效的IP地址，则为 true；否则为 false。</returns>
        public static bool IsIp(string ip)
        {
            bool result = false;
            try
            {
                string[] iparg = ip.Split('.');
                if (string.Empty != ip && ip.Length < 16 && iparg.Length == 4)
                {
                    int intip;
                    for (int i = 0; i < 4; i++)
                    {
                        intip = Convert.ToInt16(iparg[i]);
                        if (intip > 255)
                        {
                            result = false;
                            return result;
                        }
                    }
                    result = true;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的身份证号码。
        /// 使用正则表达式和数学计算进行验证。
        /// </summary>
        /// <param name="Id">要检查的字符串。</param>
        /// <returns>如果字符串是有效的身份证号码，则为 true；否则为 false。</returns>
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查给定的字符串是否为有效的18位身份证号码。
        /// 使用正则表达式和数学计算进行验证。
        /// </summary>
        /// <param name="Id">要检查的字符串。</param>
        /// <returns>如果字符串是有效的18位身份证号码，则为 true；否则为 false。</returns>
        /// <exception cref="System.ArgumentException">如果身份证号码不符合规则，将抛出此异常。</exception>
        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (
                long.TryParse(Id.Remove(17), out n) == false
                || n < Math.Pow(10, 16)
                || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false
            )
            {
                return false; //数字验证
            }
            string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false; //省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false; //校验码验证
            }
            return true; //符合GB11643-1999标准
        }

        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false; //数字验证
            }
            string address =
                "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false; //省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false; //生日验证
            }
            return true; //符合15位身份证标准
        }

        public static bool IsInt(string source)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(source).Success)
            {
                if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool IsLengthStr(string source, int begin, int end)
        {
            int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if ((length <= begin) && (length >= end))
            {
                return false;
            }
            return true;
        }

        public static bool IsTel(string source)
        {
            return Regex.IsMatch(source, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }

        public static bool IsPostCode(string source)
        {
            return Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase);
        }

        public static bool IsChinese(string source)
        {
            return Regex.IsMatch(source, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }

        public static bool hasChinese(string source)
        {
            return Regex.IsMatch(source, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }

        public static bool IsNormalChar(string source)
        {
            return Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }

        public static bool checkUserId(string str)
        {
            Regex regex = new Regex("[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}");
            if (regex.Match(str).Success)
                if (regex.Matches(str)[0].Value.Length == str.Length)
                    return true;
            return false;
        }

        public static bool IsValidDecimal(string strIn)
        {
            return Regex.IsMatch(strIn, @"[0].d{1,2}|[1]");
        }

        public static bool IsValidDate(string strIn)
        {
            return Regex.IsMatch(
                strIn,
                @"^2d{3}-(?:0?[1-9]|1[0-2])-(?:0?[1-9]|[1-2]d|3[0-1])(?:0?[1-9]|1d|2[0-3]):(?:0?[1-9]|[1-5]d):(?:0?[1-9]|[1-5]d)$"
            );
        }

        public static bool IsDate(string str)
        {
            Regex reg = new Regex(
                @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$"
            );
            return reg.IsMatch(str);
        }

        public static bool IsValidPostfix(string strIn)
        {
            return Regex.IsMatch(strIn, @".(?i:gif|jpg)$");
        }

        public static bool IsValidByte(string strIn)
        {
            return Regex.IsMatch(strIn, @"^[a-z]{4,12}$");
        }

        public static bool IsNumber(string str)
        {
            bool result = true;
            foreach (char ar in str)
            {
                if (!char.IsNumber(ar))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static bool IsDecimal(string strNumber)
        {
            return new System.Text.RegularExpressions.Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(
                strNumber
            );
        }

        public static bool IsHanyu(string str)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            if (regex.Match(str).Success)
                return true;
            else
                return false;
        }

        public static bool IsHanyuAll(string str)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]");
            //匹配的内容长度和被验证的内容长度相同时，验证通过
            if (regex.Match(str).Success)
                if (regex.Matches(str).Count == str.Length)
                    return true;
            //其它，未通过
            return false;
        }
    }
}
