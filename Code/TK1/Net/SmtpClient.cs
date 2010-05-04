using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Net
{
    /// <summary>
    /// Client SMTP utilizado para envio de e-mail
    /// </summary>
    public class SmtpClient
    {
        /// <summary>
        /// Habilita SSL
        /// </summary>
        public bool EnableSsl { get; set; }/// <summary>
        /// Nome do servidor (host)
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Porta utilizada para conexão com servidor
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Dados para autenticação de usuário
        /// </summary>
        public NetworkCredential Credential { get; set; }

    }
}
