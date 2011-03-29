using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Html
{
    public class HtmlHead : HtmlBase
    {
        public HtmlHead()
        {
        }

        public string GetHtml()
        {
            return string.Format("<head{0}>", Attributes.ToHtmlAttributeString())
                + Environment.NewLine
                + stringBuilder.ToString()
                + Environment.NewLine
                + "</head>";
        }

        public void Title(string title)
        {
            title = "<title>" + title + "</title>";
            stringBuilder.Append(title);
        }
    }
}
