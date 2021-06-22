using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SCSCommon.Serialization
{
    public static class XmlHelper
    {
        static readonly UTF8Encoding utf8NoBom = new UTF8Encoding(false);

        static XmlHelper()
        {
            emptyNamespace = new XmlSerializerNamespaces();
            emptyNamespace.Add("", "");
        }

        static readonly XmlSerializerNamespaces emptyNamespace;

        public static IEnumerable<string> GetNodeValues(string xml, string nodeName)
        {
            using (var sr = new StringReader(xml))
            {
                using (var reader = XmlReader.Create(sr))
                {
                    while (reader.ReadToDescendant(nodeName))
                    {
                        reader.ReadAsync().GetAwaiter().GetResult();
                        yield return reader.Value;
                    }
                }
            }
        }

        public static string GetNodeValue(string xml, string nodeName)
        {
            return GetNodeValues(xml, nodeName).FirstOrDefault();
        }

        public static string ValidateViaXsd(string xml, string xsdRootFolder, string version, string xsdName)
        {
            var xsdFile = !string.IsNullOrWhiteSpace(version)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xsdRootFolder, version, xsdName)
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xsdRootFolder, xsdName);

            string error = null;
            using (var reader = new XmlTextReader(xsdFile))
            {
                var schema = XmlSchema.Read(reader, (o, e) => error = e.Message);
                if (!string.IsNullOrWhiteSpace(error))
                    return error;

                var doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Schemas.Add(schema);

                doc.Validate((o, e) => error = e.Message);
                return error;
            }
        }

        static XmlAttributeOverrides RootName<T>(string rootName = null)
        {
            XmlAttributeOverrides xao = null;
            if (!string.IsNullOrWhiteSpace(rootName))
            {
                xao = new XmlAttributeOverrides();
                xao.Add(typeof(T), new XmlAttributes { XmlRoot = new XmlRootAttribute(rootName) });
            }

            return xao;
        }

        public static string Serialize<T>(T data, string rootName = null, bool requireHeader = false)
        {
            if (data == null)
                return null;


            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            //using (StringWriter textWriter = new StringWriter())
            //{
            //    xmlSerializer.Serialize(textWriter, data);
            //    return textWriter.ToString();
            //}

            using (var ms = new MemoryStream())
            using (var xw = XmlWriter.Create(ms, new XmlWriterSettings
            {
                OmitXmlDeclaration = !requireHeader,
                Encoding = utf8NoBom
            }))
            {
                new XmlSerializer(typeof(T), RootName<T>(rootName)).Serialize(xw, data, emptyNamespace);
                return utf8NoBom.GetString(ms.ToArray());
            }
        }

        public static T Deserialize<T>(string xml, string rootName = null)
        {
            if (xml == null)
                return default(T);

            using (var ms = new MemoryStream(utf8NoBom.GetBytes(xml)))
            using (var xr = XmlReader.Create(ms))
            {
                return (T)new XmlSerializer(typeof(T), RootName<T>(rootName)).Deserialize(xr);
            }
        }


    }
}
