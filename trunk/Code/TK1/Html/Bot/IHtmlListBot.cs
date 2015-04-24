using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Html.Bot
{
    public interface IHtmlListBot : IHtmlBot 
    {
        List<string> Items { get; set; }

        void LoadPage(TK1.Collection.StringPair page);

        void LoadPages(HtmlPageCollection htmlPageCollection);
    }
}
