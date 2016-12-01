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
            utils.SendMail(string.Empty, new string[] { string.Empty }, "11");

        }

        private void MailUtils_AfterSendHandler(bool arg1, string arg2)
        {
          
        }

     
    }
}
