using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCSCommon.Configuration
{
 
    public class EmailTemplate
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Content")]
        public string Content { get; set; }

        [XmlElement("Footer")]
        public string Footer { get; set; }

    }

    public class EmailSetting
    {
        [XmlAttribute]
        public int Port { get; set; }

        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string UserName { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        [XmlAttribute]
        public bool IsMailEnable { get; set; }

        [XmlAttribute]
        public bool IsAsyn { get; set; }

        [XmlAttribute]
        public bool EnableSSL { get; set; }

        [XmlAttribute]
        public int Timeout { get; set; }
    }

    [XmlRoot("MailConfig")]
    public class Email
    {
       [XmlElement("EmailSetting")]
        public EmailSetting Setting { get; set; }

     [XmlArray("Templates"),XmlArrayItem("Template")]
        public EmailTemplate[] Templates { get; set; }
    }
}
