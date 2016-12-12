using SCSCommon.Emunerable;
using SCSCommon.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SCSCommon.Configuration;

namespace SCSCommon.Mail
{

   ///相比之下https://github.com/jstedfast/MailKit 当前这个弱爆了!!!! 而且只支持SMTP
    internal class MailHelper
    {

        public event Action<bool, string> AfterSender;

        private static EmailSetting setting = new EmailSetting();
        private static List<EmailTemplate> templates = new List<EmailTemplate>();

        //TODO DB or config or xml  TODO IOC
        static MailHelper()
        {
            IConfigurationFile config = new JsonConfiguration();
            config.GetFile("~/", "emailSetting.json");
            var emailTemplate = config.Resolve<Email>();
            if (emailTemplate != null)
            {
                setting = emailTemplate.Setting;
                templates = emailTemplate.Templates.ToList();
            }
        }

        internal void SendMail(string from, string to ,string content, string title = null,string[] bcc = null , string[] cc = null,string[] attachments = null)
        {
            this.SendMail(from, new[] { to }, title, content, bcc, cc, attachments);
        }

        public void SendMailFromTemplate(string templateName, string @from, string[] to, string[] bcc = null, string[] cc = null, string[] attachments = null)
        {
            var template = templates.FirstOrDefault(t => t.Name == templateName);
            if (template == null)
            {
                AfterSender?.Invoke(false, "没有找到短信模板");
                return;
            }

            this.SendMail(from, to, template.Content,template.Title, bcc, cc, attachments);
        }

        internal void SendMail(string from, string[] to,  string content, string title = null, string[] bcc = null, string[] cc = null, string[] attachments = null)
        {
            this.SendMails(from, to, content, title, bcc, cc, attachments);
        }

        private void SendMails(string from, string[] to, string content, string title = null, string[] bcc = null,
            string[] cc = null, string[] attachments = null)
        {

            try
            {
                if (setting == null || !setting.IsMailEnable) throw new Exception("邮箱没有开启或者设置不正确");
                if (!to.Each<string>(VerifyWithRegex) || !from.IsMailAddress()) throw new Exception("地址格式不正确");
                if (bcc != null && bcc.Any() && !bcc.Each<string>(VerifyWithRegex))
                    throw new Exception("地址格式不正确");
                if (cc != null && bcc.Any() && !cc.Each<string>(VerifyWithRegex))
                    throw new Exception("地址格式不正确");




                SmtpClient client = new SmtpClient(setting.Address, setting.Port)
                {
                    
                    EnableSsl = setting.EnableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = setting.Timeout
                };

                if (!string.IsNullOrEmpty(setting.UserName))
                {
                    client.Credentials = new NetworkCredential(setting.UserName, setting.Password);
                }


                MailMessage message = new MailMessage()
                {
                 
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    Body = content,
                    Subject = title,
                    From = new MailAddress(from)
                };
                
                to.Each(t => message.To.Add(t));
                if (bcc != null && bcc.Any()) bcc.Each(t => message.Bcc.Add(t));
                if (cc != null && bcc.Any()) cc.Each(t => message.CC.Add(t));

                if (attachments != null && attachments.Any())
                    attachments.Each(t => message.Attachments.Add(new Attachment(t)));

                if (setting.IsAsyn)
                {
                    client.SendAsync(message, null);
                }
                else
                {
                    client.Send(message);
                }

                AfterSender?.Invoke(true, string.Empty);
            }
            catch (Exception ex)
            {

                AfterSender?.Invoke(false, ex.Message);
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

    
}
