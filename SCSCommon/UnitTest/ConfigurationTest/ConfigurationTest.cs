using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SCSCommon.Configuration;

namespace UnitTest.ConfigurationTest
{

    [TestFixture]
    class ConfigurationTest
    {
        [Test]
        public void Test()
        {

            IConfigurationFile file = new XmlConfiguration();
            file.GetFile("~/", "emailSetting.xml");

            Assert.IsNotNull(file.Resolve<Email>());


            IConfigurationFile file1 = new JsonConfiguration();
            file.GetFile("~/", "emailSetting.json");

            Assert.IsNotNull(file1.Resolve<Email>());
        }
    }
}
