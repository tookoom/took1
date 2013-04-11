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
using System.Windows.Browser;

namespace Took1.Silverlight
{
    public class QueryStringManager
    {
        public static string GetValue(string name)
        {
            string result = null;
            if (HtmlPage.Document.QueryString.ContainsKey(name))
                result = HtmlPage.Document.QueryString[name].ToString();
            return result;
        }

    }
}
