using SCSCommon.Emunerable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Strings
{
    public static class StringUtils
    {
        public static bool IsMailAddress(this string address)
        {
            return VerifyWithRegex(address);
        }


        public static string ReplaceSensitiveChar(this string content, string[] sensitiveChars,string repleaceChar)
        {
            if (sensitiveChars == null || !sensitiveChars.Any()) return content;

            var result = content;
            sensitiveChars.Each<string>(t =>
            {

#if DEBUG
                Debug.Print(result);
#endif

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
