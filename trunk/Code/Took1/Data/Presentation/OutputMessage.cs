using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Presentation
{
	/// <summary>
	/// Mensagem de saída do sistema
	/// </summary>
    public class OutputMessage : UserMessage
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Tipo de mensagem
        /// </summary>
        public OutputMessageTypes OutputMessageType { get; set; }

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public OutputMessage()
        {
            OutputMessageType = OutputMessageTypes.Verbose;
        }

        #region STATIC MEMBERS
        public static OutputMessage CreateMessage(string caption, string data, string source)
        {
            return CreateMessage(caption, data, source, OutputMessageTypes.Info, DateTime.Now);
        }
        public static OutputMessage CreateMessage(string caption, string data, string source, OutputMessageTypes outputMessageType)
        {
            return CreateMessage(caption, data, source, outputMessageType, DateTime.Now);
        }
        public static OutputMessage CreateMessage(string caption, string data, string source, OutputMessageTypes outputMessageType, DateTime timestamp)
        {
            OutputMessage result = new OutputMessage()
            {
                Caption = caption,
                Data = data,
                OutputMessageType = outputMessageType,
                Source = source,
                Timestamp = timestamp
            };
            return result;
        }

        #endregion
    }
}
