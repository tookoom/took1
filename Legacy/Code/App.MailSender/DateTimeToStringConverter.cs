using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TK1.MailSender
{
    public class DateTimeToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "UNABLE TO CONVERT";

            try
            {
                DateTime dateTime = (DateTime)value;
                result = string.Format("[{0}.{1}]", dateTime.ToLongTimeString(), dateTime.Millisecond.ToString());
            }
            catch { }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.MinValue;
        }

        #endregion
    }

}
