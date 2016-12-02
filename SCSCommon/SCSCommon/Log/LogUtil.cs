using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(Watch = true, ConfigFile = "Configs/Log4net.config")]
namespace SCSCommon.Log
{
    /// <summary>
    /// 目前支持异常的发送 已文本形式及邮件形式
    /// </summary>
    public class LogUtil
    {
        private static readonly ILog exceptionFileLogger = LogManager.GetLogger(Assembly.GetCallingAssembly(), "File");
        private static readonly ILog mailLogger = LogManager.GetLogger(Assembly.GetCallingAssembly(), "Mail");
        //private static readonly ILog infoLogger = LogManager.GetLogger(Assembly.GetCallingAssembly(), "Info");

        public static void LogException(string message, Exception ex)
        {
            exceptionFileLogger.Error(message, ex);
            mailLogger.Error(message, ex);
        }

        //public static void LogInfo(string message)
        //{
        //    infoLogger.Info(message);
        //}


    }
}
