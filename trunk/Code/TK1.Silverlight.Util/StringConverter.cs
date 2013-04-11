using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Silverlight
{
    public class StringConverter
    {
        /*private string? value;
        public StringConverter(string value)
        {
            this.value = value;
        }
        */
        public static int ToInt(string value)
        {
            int result = 0;
            if (value != null)
            {
                if (!int.TryParse(value, out result))
                    result = 0;
            }
            return result;
        }
        public static DateTime ToDateTime(string value)
        {
            DateTime result = DateTime.MinValue;
            if (value != null)
            {
                if (!DateTime.TryParse(value, out result))
                    result = DateTime.MinValue;
            }
            return result;
        }
        public static float ToFloat(string value)
        {
            float result = 0;
            if (value != null)
            {
                if (!float.TryParse(value, out result))
                    result = 0;
            }
            return result;
        }
        public static string ToString(string value)
        {
            string result = string.Empty;
            if (value != null)
            {
                result = value;
            }
            return result;
        }
    }
}
