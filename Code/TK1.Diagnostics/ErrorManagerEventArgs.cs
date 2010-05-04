///////////////////////////////////////////////////////////
//  ErrorManagerEventArgs.cs
//  Implementation of the Class ErrorManagerEventArgs
//  Generated by Enterprise Architect
//  Created on:      06-out-2008 17:10:16
//  Original author: andre
///////////////////////////////////////////////////////////



using System;
namespace TK1.Diagnostics {
    /// <summary>
    /// Argumentos para eventos de ErrorManager
    /// </summary>
    public class ErrorManagerEventArgs : EventArgs
    {
        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Fonte do erro
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Tipo de evento
        /// </summary>
        public EventLogEntryType EntryType { get; set; }
    }//end ErrorManagerEventArgs

}//end namespace Diagnostics