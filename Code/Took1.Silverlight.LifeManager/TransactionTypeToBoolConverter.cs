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
using System.Windows.Data;

namespace Took1.Silverlight.LifeManager
{
    public class TransactionTypeToBoolConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;

            try
            {
                int val = (int)value;
                int param = int.Parse(parameter as string);
                result = val == param;
            }
            catch { }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        //    int result = 1;

        //    try
        //    {
        //        bool val = (bool)value;
        //        int param = (int)parameter;
        //        if (val & (param == 0)) result = 0;
        //        if (val & (param == 1)) result = 1;
        //        if (!val & (param == 0)) result = 0;
        //        if (!val & (param == 1)) result = 1;
        //    }
        //    catch { }
        //    return result;
        }

        #endregion
    }
}
