using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Html.Bot
{
    public interface IHtmlPageNavigatorBot : IHtmlBot 
    {
        #region PUBLIC PROPERTIES
        HtmlPageCollection Pages { get; set; }

        #endregion

        void LoadPages(List<string> pages);

        void LoadAllPages();
    }
}
