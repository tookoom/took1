using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Took1.Net
{
    	/// <summary>
	/// Mensagem de e-mail
	/// </summary>
    public class MailMessage
    {
        public static void Send(string from, string to, string subject, string body, bool isBodyHtml)
        {
            //System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(from, to);
            //mailMessage.Subject = subject;
            //mailMessage.Body = body;
            //mailMessage.IsBodyHtml = isBodyHtml;
            //System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            ////smtpClient.Host = "mailrs.numericon.com.br";
            //smtpClient.Host = "smtp.poa.terra.com.br";
            //smtpClient.Port = 110;
            //smtpClient.EnableSsl = false;
            //smtpClient.Credentials = new System.Net.NetworkCredential("andre.mattos", "asenha", "terra.com.br");
            //smtpClient.Send(mailMessage);
            
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            //smtpClient.Host = "mailrs.numericon.com.br";
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            
            smtpClient.Credentials = new System.Net.NetworkCredential("andre.v.mattos@gmail.com", "14351623", "");
            smtpClient.Send(mailMessage);
        }
        /// <summary>
		/// Envia mensagem de e-mail com conteúdo e destinatário indicados nos parâmetros
		/// </summary>
		/// <param name="from">Rementende do e-mail</param>
		/// <param name="to">Destinatário</param>
		/// <param name="subject">Assunto do e-mail</param>
		/// <param name="body">Conteúdo do e-mail</param>
		/// <param name="isBodyHtml">indica se conteúdo do e-mail é html</param>
		/// <param name="smtpClient">Client SMTP para envio de e-mails</param>
        public static void Send(string from, string to, string subject, string body, bool isBodyHtml, SmtpClient smtpClient)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isBodyHtml;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient()
            {
                Host = smtpClient.Host,
                Port = smtpClient.Port,
                EnableSsl = smtpClient.EnableSsl,
                Credentials = new System.Net.NetworkCredential(smtpClient.Credential.UserName, smtpClient.Credential.Password, smtpClient.Credential.Domain)
            };
            client.Send(mailMessage);
        }

    }
}
