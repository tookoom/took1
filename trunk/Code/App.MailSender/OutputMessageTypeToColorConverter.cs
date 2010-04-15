using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Took1.Data;
using Took1.Data.Presentation;

namespace Took1.MailSender
{
    public class OutputMessageTypeToColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush result = new SolidColorBrush(Colors.DarkGray);
            if (value != null)
            {
                try
                {
                    OutputMessageTypes outputType = (OutputMessageTypes)value;
                    switch (outputType)
                    {
                        case OutputMessageTypes.Error:
                            result = new SolidColorBrush(Colors.Red);//  Brushes.Red;
                            break;
                        case OutputMessageTypes.Info:
                            result = new SolidColorBrush(Colors.Blue);//  Brushes.Blue;
                            break;
                        case OutputMessageTypes.Verbose:
                            result = new SolidColorBrush(Colors.Gray);// Brushes.Gray;
                            break;
                        case OutputMessageTypes.Warning:
                            result = new SolidColorBrush(Colors.Orange);//  Brushes.Tan;
                            break;
                        case OutputMessageTypes.Event:
                            result = new SolidColorBrush(Colors.Green);//  Brushes.Orange;
                            break;
                        default:
                            break;
                    }
                }
                catch { }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "UNKNOWN";
        }

        #endregion
    }
}
