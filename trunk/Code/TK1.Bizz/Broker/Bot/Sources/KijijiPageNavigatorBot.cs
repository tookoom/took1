using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TK1.Bizz.Broker.Presentation;
using TK1.Collection;
using TK1.Data.Bizz.Model;
using TK1.Data.Converter;
using TK1.Html;
using TK1.Html.Bot;
using TK1.Html.Bot.Google;
using TK1.Html.Elements;
using TK1.Net;
using TK1.Net.Services;
using TK1.Bizz.Mapper.Model;
using TK1.Web;
using TK1.Utility;

namespace TK1.Bizz.Broker.Bot.Sources
{
    public class KijijiPageNavigatorBot : IHtmlPageNavigatorBot
    {
        #region PUBLIC PROPERTIES
        public string BaseUrl { get; set; }
        public HtmlPageCollection Pages { get; set; }
        public int WaitInterval { get; set; }

        #endregion

        #region CONSTRUCTORS
        public KijijiPageNavigatorBot()
        {
            Pages = new HtmlPageCollection();
        }

        #endregion
        public void LoadFile(string filePath)
        {
            var htmlContent = File.ReadAllText(filePath);
            loadHtmlContent(htmlContent);
        }
        public void LoadPage(string url)
        {
            var htmlContent = HttpClient.GetContent(url);
            loadHtmlContent(htmlContent);
            if (!url.Contains("rss-srp"))
                Pages.Set(url, htmlContent);
        }
        public void LoadAllPages()
        {
            AppOutput.Write("KijijiPageNavigatorBot.LoadAllPages COUNT: " + Pages.Count.ToString(), true);
            loadAllPages(Pages.Select(o => o.Key).ToList());

        }
        public void LoadPages(List<string> pages)
        {
            foreach (var url in pages)
            {
                LoadPage(url);
                System.Threading.Thread.Sleep(WaitInterval);
            }
        }
        private string getValidUrl(string url)
        {
            string result = string.Empty;
            if (!url.ToLower().Contains("http"))
                result = (BaseUrl ?? string.Empty) + url.ToLower();
            else
                result = url.ToLower();
            return result;
        }
        private void loadAllPages(List<string> pages)
        {
            var currentPages = Pages.ToList();
            LoadPages(pages);
            if (Pages.Count != currentPages.Count)
            {
                var diff = (from o in Pages
                            where !currentPages.Contains(o)
                            select o.Key).ToList();
                loadAllPages(diff);
            }
        }

        private void loadHtmlContent(string htmlContent)
        {
            var navDivTag = "<div class=\"pagination\">";
            if (htmlContent.Contains(navDivTag))
            {
                var navDiv = htmlContent.Substring(htmlContent.IndexOf(navDivTag));
                var div = HtmlDiv.Parse(navDiv);
                var links = div.Children.Where(o => o.GetType() == typeof(HtmlHyperlink)).ToList();
                foreach (HtmlHyperlink item in links)
                {
                    string validUrl = getValidUrl(item.Url);
                    if (HttpClient.ValidateUrl(validUrl) & !Pages.ContainsKey(validUrl) & !validUrl.Contains("rss-srp"))
                        Pages.Set(validUrl, string.Empty);
                }
            }
        }
    }
}

