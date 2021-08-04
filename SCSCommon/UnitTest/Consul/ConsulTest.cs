using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Consul;
using NUnit.Framework;
using SCSCommon.Consul;
using SCSCommon.Convertion;
using SCSCommon.QRCodeEx;

namespace UnitTest.ConvertionTest
{
    [TestFixture]
   public class ConsulTest
    {

        [Test]
        public async  Task ToTest()
        {
            //var ss1 = QRCodeGeneratorHelper.DecodeQRCode(File.ReadAllText("E:\\base64 - Copy.txt"));

            var allServices = await ConsulUtils.GetAllRegisterService();

            await ConsulUtils.RegisterService("0", "MyNewService", "http://localhost:80");
            var services = await ConsulUtils.Find("MyNewService");
            await ConsulUtils.RegisterService("0", "MyNewService", "http://localhost:81");
            services = await ConsulUtils.Find("MyNewService");
            await ConsulUtils.RegisterService(new AgentServiceRegistration() { ID = "1", Name = "MyService1", Address = "http://localhost:50000" });
            await ConsulUtils.RegisterService(new AgentServiceRegistration() { ID = "2", Name = "MyService1", Address = "http://localhost:50001" });
            await ConsulUtils.RegisterService(new AgentServiceRegistration() { ID = "3", Name = "MyService1", Address = "http://localhost:50001" });
            var services2 = await ConsulUtils.Find("MyService1");

            var success = await ConsulUtils.DeregisterService("2");
            var services3 = await ConsulUtils.Find("MyService1");
            var services4 = await ConsulUtils.DeregisterServices("MyService1");

            var allServices1 = await ConsulUtils.GetAllRegisterService();
        }
    }

   
}
