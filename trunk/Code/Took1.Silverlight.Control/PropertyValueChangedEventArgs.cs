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

namespace Numericon.Web.Silverlight.Controls
{
    public delegate void PropertyValueChangedEventHandler(Object sender, PropertyValueChangedEventArgs e);

    public class PropertyValueChangedEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public object Source { get; set; }
        public object NewValue { get; set; }
        public object OldValue { get; set; }

        public PropertyValueChangedEventArgs()
        {
        }
    }

        
}
