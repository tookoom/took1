using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data;
using System.Net;
using System.Net.Mail;

namespace TK1.Bizz.Pandolfo
{
    public class PandolfoMailHelper
    {
        public PandolfoMailHelper() { }

        public void SendMail(string subject, string body, string mailTo, bool logMailMessage)
        {
            try
            {
                string mailFrom = "suporte@pandolfoimoveis.com.br";
                //CREDENTIALS
                var domain = string.Empty;
                var password = "pandolfopwd";
                var userName = mailFrom;
                var credentials = new NetworkCredential() { Domain = domain, Password = password, UserName = userName };

                //HOST
                string smtpServer = "smtp.pandolfoimoveis.com.br";
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
                    mailMessage.From = new System.Net.Mail.MailAddress(mailFrom, "Contato Web Site Pandolfo Imóveis");
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
                AppLogController.WriteException("Bizz.Pandolfo.PandolfoMailHelper.SendMail", exception);
                logMailMessage = true;
            }
            finally
            {
                if (logMailMessage)
                {
                    var logData = string.Format("Subject: {0}{1}To: {2}{3}Body: {4}", subject, Environment.NewLine, mailTo, Environment.NewLine, body);
                    AppLogController.WriteAppLogEntry("Bizz.Pandolfo.PandolfoMailHelper.SendMail", logData, TK1.Data.AppLogLevels.Error);
                }
            }
        }

    }
}
