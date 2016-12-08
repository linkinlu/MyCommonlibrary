using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using SCSCommon.Configuration;
using SCSCommon.IO;

namespace UnitTest.IOTest
{
    [TestFixture]
    public class XmlSectionWriterTest
    {
        [Test]
        public void Test()
        {
            var email = new Email()
            {
                Setting = new EmailSetting() {Address = "127.0.0.1", IsAsyn = false},
                Templates = new EmailTemplate[]
                {
                    new EmailTemplate()
                    {
                        Title = "test",
                        Content = "test agin"

                    }
                }
            };
            var item = new XmlSerializer(typeof(Email));
            var memory = new MemoryStream();
            var xmlwriter = new XmlTextWriter(memory, Encoding.UTF8);
             item.Serialize(xmlwriter, email);

            Debug.Print(System.Text.Encoding.UTF8.GetString(memory.GetBuffer()));

        }
    }
}
