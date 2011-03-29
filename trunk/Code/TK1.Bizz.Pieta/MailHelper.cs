using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Net;
using TK1.Data.Converter;
using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta.Const;

namespace TK1.Bizz.Pieta
{
    public class MailHelper
    {
        //public MailHelper()
        //{
        //}
        public static void SendContactMail(string subject, string body)
        {
            SendMail(subject, body, MailAdresses.Contato, MailAdresses.Contato);
        }
        public static void SendRentMail(string subject, string body)
        {
            SendMail(subject, body, MailAdresses.Contato, MailAdresses.Contato);
        }
        public static void SendSalesMail(string subject, string body)
        {
            SendMail(subject, body, MailAdresses.Contato, MailAdresses.Contato);
        }
        public static void SendWebMasterMail(string subject, string body)
        {
            SendMail(subject, body, MailAdresses.WebMaster, MailAdresses.WebMaster);
        }
        public static void SendMail(string subject, string body, string mailFrom, string mailTo)
        {
            try
            {
                //CREDENTIALS
                var domain = StringConverter.ToString(SiteController.GetParameterValue(SiteMailParameterNames.Domain), "");
                var password = StringConverter.ToString(SiteController.GetParameterValue(SiteMailParameterNames.Password), "P@$$w0rd");
                var userName = StringConverter.ToString(SiteController.GetParameterValue(SiteMailParameterNames.UserName), "webmaster@pietaimoveis.com.br");
                var credentials = new NetworkCredential() { Domain = domain, Password = password, UserName = userName };

                //HOST
                string smtpServer = StringConverter.ToString(SiteController.GetParameterValue(SiteMailParameterNames.SmtpServer), "smtp.pietaimoveis.com.br");
                int smtpPort = StringConverter.ToInt(SiteController.GetParameterValue(SiteMailParameterNames.SmtpPort), 587);
                SmtpClient smtpClient = new SmtpClient()
                {
                    Credential = credentials,
                    Host = smtpServer,
                    Port = smtpPort
                };

                MailMessage.Send(mailFrom, mailTo, subject, body, true, smtpClient);
            }
            catch (Exception exception)
            {
                LogController.WriteException("MailHelper.SendMail", exception);
            }

        }
    }
}
