///////////////////////////////////////////////////////////
//  EventLog.cs
//  Implementation of the Class EventLog
//  Generated by Enterprise Architect
//  Created on:      06-out-2008 16:19:51
//  Original author: andre
///////////////////////////////////////////////////////////




using TK1.Diagnostics;
using System;
namespace TK1.Diagnostics {
    /// <summary>
    /// Insere entrada no eventLog do sistema, podendo criar source caso n�o exista.
    /// </summary>
    public class EventLog
    {

        /// <summary>
        /// Testa se Fonte de Eventos existe e cria se necess�rio (definido por par�metro)
        /// </summary>
        /// <param name="sourceName">Nome da fonte de eventos do sistema</param>
        /// <param name="createSource">Indica se Log do sistema deve ser gerado caso nao exista</param>
        public static bool TestEventSource(string sourceName, bool createSource)
        {
            bool result = false;
            if (System.Diagnostics.EventLog.SourceExists(sourceName))
            {
                result = true;
            }
            else
            {
                if (createSource)
                {
                    System.Diagnostics.EventLog.CreateEventSource(sourceName, "Application");
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema 
        /// </summary>
        /// <param name="eventLogEntry">Entrada a ser gravada</param>
        public static void WriteEntry(EventLogEntry eventLogEntry)
        {
            WriteEntry(eventLogEntry, true);
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema 
        /// </summary>
        /// <param name="eventLogEntry">Entrada a ser gravada</param>
        /// <param name="createSource">Indica se Log do sistema deve ser gerado caso nao exista</param>
        public static void WriteEntry(EventLogEntry eventLogEntry, bool createSource)
        {
            System.Diagnostics.EventLog eventLog = createEventLog(eventLogEntry.Source, createSource);
            if (eventLog != null)
                eventLog.WriteEntry(eventLogEntry.EventData, convertType(eventLogEntry.Type));
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema com parametros opcionais
        /// padr�o (eventType =Error, createEventSource = true)
        /// </summary>
        /// <param name="sourceName">Nome da fonte de eventos do sistema</param>
        /// <param name="message">Mensagem a ser logada</param>
        public static void WriteEntry(string sourceName, string message)
        {
            WriteEntry(new EventLogEntry { Source = sourceName, EventData = message, Type = EventLogEntryType.Error });
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema com parametros opcionais padr�o
        /// (eventType =Error)
        /// </summary>
        /// <param name="sourceName">Nome da fonte de eventos do sistema</param>
        /// <param name="message">Mensagem a ser logada</param>
        /// <param name="createSource">Indica se Log do sistema deve ser gerado caso nao exista</param>
        public static void WriteEntry(string sourceName, string message, bool createSource)
        {
            WriteEntry(new EventLogEntry { Source = sourceName, EventData = message, Type = EventLogEntryType.Error }, createSource);
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema com parametros opcionais padr�o
        /// (createEventSource = true)
        /// </summary>
        /// <param name="sourceName">Nome da fonte de eventos do sistema</param>
        /// <param name="message">Mensagem a ser logada</param>
        /// <param name="eventLogEntry">Entrada a ser gravada</param>
        public static void WriteEntry(string sourceName, string message, EventLogEntryType eventLogEntryType)
        {
            WriteEntry(new EventLogEntry { Source = sourceName, EventData = message, Type = eventLogEntryType });
        }

        /// <summary>
        /// Insere entrada no eventLog de eventos do sistema com parametros fornecidos
        /// </summary>
        /// <param name="sourceName">Nome da fonte de eventos do sistema</param>
        /// <param name="message">Mensagem a ser logada</param>
        /// <param name="eventLogEntry">Entrada a ser gravada</param>
        /// <param name="createSource">Indica se Log do sistema deve ser gerado caso nao exista</param>
        public static void WriteEntry(string sourceName, string message, EventLogEntryType eventLogEntryType, bool createSource)
        {
            WriteEntry(new EventLogEntry { Source = sourceName, EventData = message, Type = eventLogEntryType }, createSource);
        }

        /// <summary>
        /// Intancia EventLog e cria fonte de eventos do sistema de acordo com parametros fornecidos
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="createSource"></param>
        private static System.Diagnostics.EventLog createEventLog(string sourceName, bool createSource)
        {
            System.Diagnostics.EventLog eventLog = null;
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(sourceName))
                {
                    if (createSource)
                        System.Diagnostics.EventLog.CreateEventSource(sourceName, "Application");
                }
                eventLog = new System.Diagnostics.EventLog();
                eventLog.Source = sourceName;
                eventLog.MachineName = ".";
            }
            catch (Exception exception) { }
            return eventLog;
        }

        /// <summary>
        /// Converte o EventLogEntryType do tipo Numericon para o tipo aceito pelo sistema
        /// </summary>
        /// <param name="eventLogEntryType"></param>
        private static System.Diagnostics.EventLogEntryType convertType(EventLogEntryType eventLogEntryType)
        {
            System.Diagnostics.EventLogEntryType result = System.Diagnostics.EventLogEntryType.Information;
            switch (eventLogEntryType)
            {
                case EventLogEntryType.Error:
                    result = System.Diagnostics.EventLogEntryType.Error;
                    break;
                case EventLogEntryType.Info:
                    result = System.Diagnostics.EventLogEntryType.Information;
                    break;
                case EventLogEntryType.Warning:
                    result = System.Diagnostics.EventLogEntryType.Warning;
                    break;
            }
            return result;
        }

    }//end EventLog

}//end namespace Diagnostics