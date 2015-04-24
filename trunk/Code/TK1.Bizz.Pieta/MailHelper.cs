using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data;
using System.Net;
using System.Net.Mail;
using TK1.Data.Bizz.Client.Controller;

namespace TK1.Bizz.Pieta
{
    public class MailHelper
    {
        public MailHelper() { }

        public void SendMail(string subject, string body)
        {
            SendMail(subject, body, "pieta@pietaimoveis.com.br");
        }
        public void SendMail(string subject, string body, string mailTo)
        {
            try
            {
                var logData = string.Format("Subject: {0}{1}To: {2}{3}Body: {4}", subject, Environment.NewLine, mailTo, Environment.NewLine, body);
                AppLogClientController.WriteAppLogEntry("PietaMailHelper.SendMail", logData, AppLogLevels.Info);

                string mailFrom = "contato@pietaimoveis.com.br";
                //CREDENTIALS
                var domain = string.Empty;
                var password = "93879999";
                var userName = mailFrom;
                var credentials = new NetworkCredential() { Domain = domain, Password = password, UserName = userName };

                //HOST
                string smtpServer = "smtp.pietaimoveis.com.br";
                int smtpPort = 587;
                SmtpClient smtpClient = new SmtpClient()
                {
                    Credentials = credentials,
                    Host = smtpServer,
                    Port = smtpPort
                };

                if (!string.IsNullOrEmpty(mailTo))
                {
                    MailMessage mailMessage = new System.Net.Mail.MailMessage();
                    mailMessage.From = new System.Net.Mail.MailAddress(mailFrom, "Contato Pietá");
                    if (mailTo.Contains(";"))
                    {
                        foreach (var item in mailTo.Split(';'))
                        {
                            if (!string.IsNullOrEmpty(item))
                                mailMessage.To.Add(item);
                        }
                    }
                    else
                    {
                        mailMessage.To.Add(mailTo);
                    }
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PietaMailHelper.SendMail", exception);
                var logData = string.Format("Subject: {0}{1}To: {2}{3}Body: {4}", subject, Environment.NewLine, mailTo, Environment.NewLine, body);
                AppLogClientController.WriteAppLogEntry("PietaMailHelper.SendMail", logData, AppLogLevels.Error);

            }

        }

    }
}
