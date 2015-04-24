using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Presentation;
using TK1.Html.Bot;

namespace TK1.Bizz.Broker.Bot
{
    public delegate void PageLoadedEventHandler(object sender, PropertyAdEventArgs e);

    public interface IPropertyAdBot 
    {
        #region EVENTS
        event PageLoadedEventHandler PageLoaded;
        #endregion
        #region PUBLIC PROPERTIES
        IHtmlPageNavigatorBot Navigator { get; set;  }
        IHtmlListBot  SearchResults { get; set; }
        List<PropertyAdView> Ads { get; set; }
        string CustomerCode { get; set; }
        string Source { get; set; }
        string BaseUrl { get; set; }
        int WaitInterval { get; set; }

        #endregion

        void Initialize();
        PropertyAdView GetAdFromFile(string filePath);
        PropertyAdView GetAdFromPage(string url);


        void Run();

        string StartPage { get; set; }
    }
}
