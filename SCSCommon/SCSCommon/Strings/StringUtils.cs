using SCSCommon.Emunerable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Strings
{
    public static class StringUtils
    {
        /// <summary>
        /// Determines whether [is mail address].
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>
        ///   <c>true</c> if [is mail address] [the specified address]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMailAddress(this string address)
        {
            return VerifyWithRegex(address);
        }

        /// <summary>
        /// Determines whether [is vaild file path].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <c>true</c> if [is vaild file path] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
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

        /// <summary>
        /// Replaces the sensitive character.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="sensitiveChars">The sensitive chars.</param>
        /// <param name="repleaceChar">The repleace character.</param>
        /// <returns></returns>
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


      






        private static bool VerifyWithRegex(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            return re.IsMatch(emailAddress);
        }      
            
        
    }



}
