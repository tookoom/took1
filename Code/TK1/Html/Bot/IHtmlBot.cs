using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Html.Bot
{
    public interface IHtmlBot
    {
        #region PUBLIC PROPERTIES
        string BaseUrl { get; set; }
        int WaitInterval { get; set; }

        #endregion

        void LoadFile(string filePath);
        void LoadPage(string url);
    }
}
