using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Extension;
using TK1.Utility;

namespace TK1.Web
{
    public class WebHelper
    {
        public static string GetUriValue(string value)
        {
            var result = string.Empty;
            if (value != null)
            {
                result = System.Uri.EscapeUriString(value);
                result = result.ReplaceIfExists("?", string.Empty);
            }
            return result;
        }
    }
}
