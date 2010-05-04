using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Took1.Silverlight.Controls
{
    public delegate void WriteOutputEventHandler(Object sender, WriteOutputEventArgs e);

    public class WriteOutputEventArgs : EventArgs
    {
        public string Message { get; set; }
        public WriteOutputLevel Level { get; set; }

        public WriteOutputEventArgs()
        {
            Message = string.Empty;
            Level = WriteOutputLevel.Info;
        }
    }

    public enum WriteOutputLevel
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }
        
}
