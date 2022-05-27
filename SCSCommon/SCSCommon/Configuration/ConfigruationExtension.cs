using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace SCSCommon.Configuration
{
    public static class ConfigruationExtension
    {
        /// <summary>
        /// 根据key 获得app.config 的value 支持简单数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parseFunc"></param>
        /// <param name="defaultValueFunc"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static T GetAppConfigurationSetting<T>(Func<string, T> parseFunc, Func<T> defaultValueFunc, string key)
        {
            try
            {
                string nodeValue =
                    ConfigurationManager.AppSettings[key];
                  

                return string.IsNullOrEmpty(nodeValue) ? defaultValueFunc() : parseFunc(nodeValue);
            }
            catch (Exception ex)
            {
                return defaultValueFunc();
            }
        }

    }
}
