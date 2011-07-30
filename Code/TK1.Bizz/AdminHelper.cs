using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Net;

namespace TK1.Bizz
{
    public class AdminHelper
    {
        public static void SendMail(string subject, string body)
        {
            try
            {
                string mailFrom = "admin@tk1.net.br";
                string mailTo = "andre@tk1.net.br";
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
                //LogController.WriteException("MailHelper.SendMail", exception);
            }

        }

    }
}
