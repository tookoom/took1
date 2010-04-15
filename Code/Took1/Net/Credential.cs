using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Net
{
    /// <summary>
    /// Dados para autenticação de usuário junto ao servidor
    /// </summary>
    public class NetworkCredential
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Senha de autenticação do usuário
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Domínio do servidor
        /// </summary>
        public string Domain { get; set; }
    }
}
