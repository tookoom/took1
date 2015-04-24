using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Extension
{
    public static class CommonExtensions
    {
        public static string ReplaceIfExists(this string value, string oldValue, string newValue)
        {
            var result = value;
            if (result != null)
            {
                if (result.Contains(oldValue))
                    result = result.Replace(oldValue, newValue);
            }
            return result;
        }

    }
}
