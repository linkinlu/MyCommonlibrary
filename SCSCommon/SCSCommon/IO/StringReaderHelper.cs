using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.IO
{
    public class StringReaderHelper
    {
        /// <summary>
        /// 读取txt文件 不包含空行
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadTxtFile(string txtPath)
        {
            var result = new StringBuilder();
            if (!File.Exists(txtPath)) return string.Empty;

            try
            {
                using (StringReader reader = new StringReader(txtPath))
                {
                    string lineString = string.Empty;
                    while (reader.ReadLine() != null)
                    {
                        lineString = reader.ReadLine();
                        if (string.IsNullOrEmpty(lineString))
                            continue;

                        result.AppendLine(lineString);
                    }
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }

    public class StreamReaderHelper
    {
        public static string ReadJson(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;

            using (StreamReader sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }

        }
    }

}
