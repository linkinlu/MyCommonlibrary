using SCSCommon.Emunerable;
using SCSCommon.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Mail
{
    internal class MailHelper
    {
        public Action SendSuccess;
        public Action<string> SendFail;

        private static EmailSetting setting = null;

        //TODO DB or config or xml 
        static MailHelper()
        {

            setting = new EmailSetting() { };
           

        }

        internal void SendMail(string from, string to ,string content, string title = null,string[] bcc = null , string[] cc = null,string[] attachments = null)
        {
            this.SendMail(from, new string[] { to }, title, content);
        }


        internal void SendMail(string from, string[] to,  string content, string title = null, string[] bcc = null, string[] cc = null, string[] attachments = null)
        {
            this.SendMails(from, to, title, content);
        }

        private void SendMails(string from, string[] to, string content, string title = null, string[] bcc = null, string[] cc = null, string[] attachments = null)
        {

            try
            {
                if (setting == null || !setting.IsMailEnable) return;
                if (!to.Each<string>(s => VerifyWithRegex(s)) || !from.IsMailAddress()) throw new Exception("地址格式不正确");
                if (bcc != null && bcc.Any() && !bcc.Each<string>(s => VerifyWithRegex(s))) throw new Exception("地址格式不正确");
                if (cc != null && bcc.Any() && !cc.Each<string>(s => VerifyWithRegex(s))) throw new Exception("地址格式不正确");




                SmtpClient client = new SmtpClient(setting.Address, setting.Port) { Credentials = new NetworkCredential(setting.UserName, setting.Password), EnableSsl = setting.EnableSSL, DeliveryMethod = SmtpDeliveryMethod.Network, Timeout = setting.Timeout };



                MailMessage message = new MailMessage() { SubjectEncoding = Encoding.UTF8, IsBodyHtml = true, BodyEncoding = Encoding.UTF8 };
                to.Each(t => message.To.Add(t));
                if (bcc != null && bcc.Any()) bcc.Each(t => message.Bcc.Add(t));
                if (cc != null && bcc.Any()) cc.Each(t => message.CC.Add(t));

                if (attachments != null && attachments.Any()) attachments.Each(t => message.Attachments.Add(new Attachment(t)));

                if (setting.IsAsyn)
                    {
                        client.SendAsync(message, null);
                    }
                    else
                    {
                        client.Send(message);
                    }
            }
            catch (Exception ex)
            {
                SendFail?.Invoke(ex.Message);
            }
            finally
            {

                if (SendSuccess != null)
                {
                    SendSuccess.Invoke();
                }

            }

        }

       

        private static bool VerifyWithRegex(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);
            return re.IsMatch(emailAddress);
        }







    }

    internal class EmailSetting
    {
        public int Port { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsMailEnable { get; set; }

        public bool IsAsyn { get; set; }

        public bool EnableSSL { get; set; }

        public int Timeout { get; set; }
    }
}
