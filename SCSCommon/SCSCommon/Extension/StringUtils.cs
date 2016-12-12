using SCSCommon.Emunerable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Extension
{
    public static class StringUtils
    {
        
        public static bool IsMailAddress(this string address)
        {
            return VerifyWithRegex(address);
        }

        public static bool IsNumber(this string number)
        {
            if (string.IsNullOrEmpty(number)) return false;

            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(number);
        }

        public static bool IsVaildFilePath(this string fileName)
        {
            Regex containsABadCharacter = new Regex("^([a-zA-Z]:)?(\\\\[^<>:\"/\\\\|?*]+)+\\\\?$");
            return !containsABadCharacter.IsMatch(fileName);
        }

        public static bool IsMobileNumber(this string mobiles)
        {
            if (string.IsNullOrEmpty(mobiles))
            {
                return false;
            }

            var array = mobiles.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return array.All(input => Regex.IsMatch(input, @"^1[3|4|5|7|8]\d{9}$"));
        }

        public static string ReplaceSensitiveChar(this string content, string[] sensitiveChars,string repleaceChar)
        {
            if (sensitiveChars == null || !sensitiveChars.Any()) return content;

            var result = content;
            sensitiveChars.Each<string>(t =>
            { 
                result = result.Replace(t, repleaceChar);
            }
            );
            return result;
        }

        public static bool ContainCharaters(this string source, string charaters)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(charaters))
            {
                return false;
            }


           return charaters.Each(source.Contains);
        }


       




        #region private
        private static bool VerifyWithRegex(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            return re.IsMatch(emailAddress);
        }
        #endregion


    }



}
