using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using SCSCommon.Directory;
using SCSCommon.IO;

namespace SCSCommon.Configuration
{
    public class XmlConfiguration : ConfigurationBase
    {
        private string xml = string.Empty;
        private string url = string.Empty;

        public override T Seriablize<T>()
        {
           
            return (T) (new XmlSerializer(typeof(T)).Deserialize(new XmlTextReader(url)));
        }


        public override void GetFile(string filePath, string fileName)
        {
            var fullName = CommonDirectoryHelper.MapFullFileName(filePath, fileName);
            if (File.Exists(fullName))
            {
                xml = StreamReaderHelper.ReadJson(fullName);
                url = fullName;
            }
        }

        public override ConfigFileExtension FileExtension => ConfigFileExtension.Xml;

        public override string Content => xml;
    }
}
