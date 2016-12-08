using NUnit.Framework;
using SCSCommon.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.MailTest
{
    [TestFixture]
    class MailUtilTest
    {
        [Test]
        public void Test()
        {
            MailUtils utils = new MailUtils();
            utils.AfterSendHandler += MailUtils_AfterSendHandler;
            utils.SendMail("bambooljm14@126.com", new string[] { "bambooljm14@sohu.com" }, "11");
            utils.SendMailFromTemplate("Test", "bambooljm14@126.com",
                new[] { "bambooljm14@sohu.com", "bambooljm14@sina.com" });
        }

        private void MailUtils_AfterSendHandler(bool arg1, string arg2)
        {
          
        }
    }
}
