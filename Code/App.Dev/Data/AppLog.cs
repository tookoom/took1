//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TK1.Dev.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppLog
    {
        public int AppLogID { get; set; }
        public System.DateTime LogTimestamp { get; set; }
        public int LogType { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}
