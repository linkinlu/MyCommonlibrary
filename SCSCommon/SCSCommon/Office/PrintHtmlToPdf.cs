using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Office
{
   public class PrintHtmlToPdf
    {
        // So cool function  but not support execute js function      https://www.puppeteersharp.com/index.html is more awesome
        public void Print(string outputDir, string fileName , string chromeFilePath )
        {
            
            var url = "https://stackoverflow.com/questions/564650/convert-html-to-pdf-in-net";
            var chromePath = string.IsNullOrEmpty(chromeFilePath)
                ? chromeFilePath
                : @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            var output = Path.Combine(outputDir,
                fileName.LastIndexOf(".pdf", StringComparison.Ordinal) > -1 ? fileName : fileName + ".pdf");
            using (var p = new Process())
            {
                p.StartInfo.FileName = chromePath;
                p.StartInfo.Arguments = $"--headless --disable-gpu --print-to-pdf={output} {url}";
                p.Start();
                p.WaitForExit();
            }
        }


    }
}
