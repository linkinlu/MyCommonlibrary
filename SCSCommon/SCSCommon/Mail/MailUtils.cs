using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Mail
{
   public  class MailUtils
    {
        private static readonly MailHelper helper = new MailHelper();
        public  event Action<bool, string> AfterSendHandler;

        /// <summary>自动挂载好像不大好！
        /// Initializes a new instance of the <see cref="MailUtils"/> class.
        /// </summary>
        public MailUtils()
        {
            helper.AfterSender += Helper_AfterSender;
        }

        private  void Helper_AfterSender(bool isSuccess, string errorMsg)
        {
            if (AfterSendHandler != null)
            {
                AfterSendHandler.Invoke(isSuccess, errorMsg);
            }
        }

        public  void SendMail(string from, string[] to, string content, string title = null, string[] bcc = null, string[] cc = null, string[] attachments = null)
        {
            helper.SendMail(from, to, content, title, bcc, cc, attachments);
        }

        public void SendMailFromTemplate(string templateName, string from, string[] to)
        {
            helper.SendMailFromTemplate(templateName, from, to);
        }
    }
}
