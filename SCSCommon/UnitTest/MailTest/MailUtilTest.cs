using NUnit.Framework;
using SCSCommon.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCSCommon.Configuration;
using SCSCommon.Serialization;

namespace UnitTest.MailTest
{
    [TestFixture]
    class MailUtilTest
    {
        [Test]
        public void Test()
        {
            var email = new Email()
            {
                Setting = new EmailSetting() { Address = "127.0.0.1", IsAsyn = false },
                Templates = new EmailTemplate[]
              {
                    new EmailTemplate()
                    {
                        Title = "p",
                        Content = @"<div id='qm_con_body'><div id='mailContentContainer' class='qmbox qm_con_body_content qqmail_webmail_only'><style></style><div><font color='#000080'>亲爱的网易用户，您好：</font></div><div><font color='#000080'></font>&nbsp;</ div >< div > &nbsp;</ div >< div >< font color = '#000080' > 请您点击下面链接来修复网易邮箱帐号登录密码:< br >< a href = 'http://reg.163.com/getpasswd/SetPassword.jsp?mailType=qq&amp;sid=bAr*vD%3DUddKyPxqimyM' target = '_blank' > http://reg.163.com/getpasswd/SetPassword.jsp?mailType=qq&amp;sid=bAr*vD%3DUddKyPxqimyM</a></font></div>< div > &nbsp;</ div >< div > &nbsp;</ div >< div >< font color = '#000080' > 为了确保您的帐号安全，该链接仅 < font color = '#ff0000' > 3天内 </ font > 访问有效。</ font ></ div >< div >< font color = '#000080' >< br > 如果该链接已经失效，请您点击 < a href = 'http://reg.163.com/getpasswd/Pickup.jsp?retakeMethod=qq' target = '_blank' > 这里 </ a > 重新获取修复密码邮件。</ font ></ div >< div > &nbsp;</ div >< div > &nbsp;</ div >< div >< font color = '#000080' > 如果点击链接不工作...</ font ></ div >< div >< font color = '#000080' >< br > 请您选择并复制整个链接，打开浏览器窗口并将其粘贴到地址栏中。然后单击'转到'按钮或按键盘上的 Enter键。</ font ></ div >< div > &nbsp;</ div >< font color = '#000080' >< div >< br >< font color = '#ff0000' > 请勿直接回复该邮件 </ font >，有关 < a href = 'http://reg.163.com' target = '_blank' > 网易邮箱帐号 </ a > 的更多帮助信息，请访问：< a href = 'http://reg.163.com/help/help.shtml' target = '_blank' > http://reg.163.com/help/help.shtml</a></div><div>&nbsp;</div>< div >< p style = 'font-size:12px;padding:10px 0px;margin-top:0px;margin-bottom:0px;margin-left:0px;margin-right:0px;' >< a href = 'http://u.163.com/gift?from=urs2' target = '_blank' >< img src = 'http://reg.163.com/images2/yxds.png' ></ a ></ p ></ div >< div > &nbsp;</ div >< div > &nbsp;</ div >< div ></ div > &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;网易用户中心 </ font >< div ></ div >< div >< font color = '#000080' size = '2' ></ font > &nbsp;</ div >< style type = 'text/css' >.qmbox style, .qmbox script, .qmbox head, .qmbox link, .qmbox meta { display: none !important; }</ style ></ div ></ div >"
                    }
              }
            };

            var items = JavaScriptSerializerHelper.GetJsonString<Email>(email);
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
