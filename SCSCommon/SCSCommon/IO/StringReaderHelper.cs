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
        public static List<string> ReadTxtFile(string txtPath)
        {
            var result = new List<string>();
            if (!File.Exists(txtPath)) return result;

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

                        result.Add(lineString);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }

        }
    }
}
