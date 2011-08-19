using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Net;
using TK1.Data;

namespace TK1.Bizz
{
    public class AdminHelper
    {
        public static void SendMail(string subject, string body)
        {
            SendMail(subject, body, "andre@tk1.net.br");
        }
        public static void SendMail(string subject, string body, string mailTo)
        {
            try
            {
                string mailFrom = "admin@tk1.net.br";
                //CREDENTIALS
                var domain = string.Empty;
                var password = "P@$$w0rd";
                var userName = mailFrom;
                var credentials = new NetworkCredential() { Domain = domain, Password = password, UserName = userName };

                //HOST
                string smtpServer = "smtp.tk1.net.br";
                int smtpPort = 587;
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
                AppLogController.WriteException("AdminHelper.SendMail", exception);
            }

        }

    }
}
