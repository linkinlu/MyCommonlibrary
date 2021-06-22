using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;

namespace SCSCommon.Extension
{
    public class AppConfigrationEx
    {

        public static T GetValue<T>(string key, Func<string,T> parseFunc, Func<T> defaultFunc = null)
        {
            try
            {
                var configVal = System.Configuration.ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(configVal))
                {
                    return defaultFunc == null ? default(T) : defaultFunc();
                }
                else
                {
                    return parseFunc(configVal);
                }

            }
            catch
            {
                return defaultFunc !=null ? defaultFunc() : default(T);
            }
          
        }
    }
}
