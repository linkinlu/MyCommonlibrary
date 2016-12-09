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
            var writer = new StreamWriter(memory);
            var xmlwriter = new NoNameSpaceXmlTextWriter(writer);

            item.Serialize(xmlwriter, email);

            Assert.IsNotNull(System.Text.Encoding.UTF8.GetString(memory.GetBuffer()));
            
            memory.Position = 0;

            Assert.IsNotNull((Email) item.Deserialize(memory));


        }
    }
}
