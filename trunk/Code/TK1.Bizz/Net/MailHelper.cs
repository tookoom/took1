using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data;
using System.Net;
using System.Net.Mail;

namespace TK1.Bizz.Net
{
    //public class MailHelper
    //{
    //    public MailHelper(){}

    //    //public void SendMail(string subject, string body, string mailTo, bool logMailMessage)
    //    //{

    //    //    string mailFrom = "suporte@tk1.net.br";
    //    //    //CREDENTIALS
    //    //    var domain = string.Empty;
    //    //    var password = "Tk1pwd!!";
    //    //    var userName = mailFrom;
    //    //    var credentials = new NetworkCredential() { Domain = domain, Password = password, UserName = userName };

    //    //    //HOST
    //    //    string smtpServer = "smtp.live.com";
    //    //    int smtpPort = 587;
    //    //    SmtpClient smtpClient = new SmtpClient()
    //    //    {
    //    //        Credentials = credentials,
    //    //        Host = smtpServer,
    //    //        Port = smtpPort, EnableSsl = true
    //    //    };

    //    //    if (!string.IsNullOrEmpty(mailTo))
    //    //    {
    //    //        MailMessage mailMessage = new System.Net.Mail.MailMessage();
    //    //        mailMessage.From = new System.Net.Mail.MailAddress(mailFrom, "Suporte TK1");
    //    //        if (mailTo.Contains(";"))
    //    //        {
    //    //            foreach (var item in mailTo.Split(';'))
    //    //            {
    //    //                if (!string.IsNullOrEmpty(item))
    //    //                    mailMessage.To.Add(item.Trim());
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            mailMessage.To.Add(mailTo.Trim());
    //    //        }
    //    //        mailMessage.Subject = subject;
    //    //        mailMessage.Body = body;
    //    //        mailMessage.IsBodyHtml = true;
    //    //        smtpClient.Send(mailMessage);
    //    //    }
    //    //}

    //}
}
