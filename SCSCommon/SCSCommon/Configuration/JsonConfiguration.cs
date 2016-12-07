using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Directory;
using System.Web.Script.Serialization;
using SCSCommon.IO;

namespace SCSCommon.Configuration
{
    public class JsonConfiguration : ConfigurationBase
    {
        private string readString = string.Empty;
        public override T Seriablize<T>()
        {
            return new JavaScriptSerializer().Deserialize<T>(Content);
        }

        public override void GetFile(string filePath, string fileName)
        {
            var fullName = CommonDirectoryHelper.MapFullFileName(filePath, fileName);
            if (File.Exists(fullName))
            {
                readString = StreamReaderHelper.ReadJson(fullName);
            }
        }

        public override string Content => readString;



        public override ConfigFileExtension FileExtension => ConfigFileExtension.Json;
    }
}
