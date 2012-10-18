using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace TK1.Web.Extension
{
    public static class PageExtension
    {
        public static int GetSessionIntegerValue(this Page page, string key)
        {
            int result = -1;
            var value = page.Session[key] == null ? string.Empty : page.Session[key].ToString();
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out result);
            }
            return result; 
        }
        public static string GetSessionStringValue(this Page page, string key)
        {
            return page.Session[key] == null ? null : page.Session[key].ToString();
        }
        public static object GetSessionValue(this Page page, string key)
        {
            return page.Session[key];
        }
        public static void SetSessionValue(this Page page, string key, object value)
        {
            page.Session[key] = value;
  
        }
    }
}
