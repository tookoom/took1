using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Presentation
{
    public class UserMessage
    {
        /// <summary>
        /// Legenda da mensagem
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Dados da mensagem
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Origem da mensagem
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Data e hora da mensagem
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
