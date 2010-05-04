using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Diagnostics
{
	/// <summary>
	/// Representa entrada no log
	/// </summary>
    public class EventLogEntry
    {
		/// <summary>
		/// Nome
		/// </summary>
        public string Name { get; set; }
		/// <summary>
		/// Origem do erro ou mensagem
		/// </summary>
        public string Source { get; set; }
		/// <summary>
		/// Tipo do evento
		/// </summary>
        public EventLogEntryType Type { get; set; }
		/// <summary>
		/// Data e hora do evento
		/// </summary>
        public DateTime Timestamp { get; set; }
		/// <summary>
		/// Categoria
		/// </summary>
        public string Category { get; set; }
		/// <summary>
		/// Nome de exibição
		/// </summary>
        public string Caption { get; set; }
		/// <summary>
		/// Dados da entrada a ser logada
		/// </summary>
        public string EventData { get; set; }
       /// <summary>
		/// Construtor
		/// </summary>

        public EventLogEntry()
        {
            Timestamp = DateTime.Now;
            Type = EventLogEntryType.Info;
        }
    }
}
