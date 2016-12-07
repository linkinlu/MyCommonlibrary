using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SCSCommon.IO
{
    public class XmlTextWriterHelper
    {
        public static string ExportAsXml<T>(T entity)
        {
            if (entity == null)
                return string.Empty;
            var stringWriter = new StringWriter();
            var writer = new XmlTextWriter(stringWriter);
            writer.WriteStartDocument();

            




            writer.WriteEndDocument();





            return stringWriter.ToString();
        }


    }
}
