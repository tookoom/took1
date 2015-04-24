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
    public class KijijiAdListingBot : IHtmlListBot
    {
        #region PUBLIC PROPERTIES
        public string BaseUrl { get; set; }
        public List<string> Items { get; set; }
        public int WaitInterval { get; set; }

        #endregion

        #region CONSTRUCTORS
        public KijijiAdListingBot()
        {
            Items = new List<string>();
        }

        #endregion
        public void LoadFile(string filePath)
        {
            var htmlContent = File.ReadAllText(filePath);
            loadHtmlContent(htmlContent);
        }
        public void LoadPage(StringPair page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            if (string.IsNullOrEmpty(page.Value))
                page.Value = HttpClient.GetContent(page.Key);

            loadHtmlContent(page.Value);
        }
        public void LoadPage(string url)
        {
            var htmlContent = HttpClient.GetContent(url);
            loadHtmlContent(htmlContent);
        }
        public void LoadPages(HtmlPageCollection pages)
        {
            AppOutput.Write("KijijiAdListingBot.LoadPages COUNT: " + pages.Count.ToString(), true);
            var count = 0;
            var div = (int)pages.Count / 10;
            foreach (var page in pages)
            {
                count++;
                LoadPage(page);
                System.Threading.Thread.Sleep(50);
                if (count % (div == 0 ? 1 : div) == 0)
                    AppOutput.Write("KijijiAdListingBot.LoadPages LOADING: " + ((count * 100)/ pages.Count).ToString() + "%", true);
            }
            AppOutput.Write("KijijiAdListingBot.LoadPages LOADING: 100%", true);
        }
        private void loadHtmlContent(string htmlContent)
        {
            findUrl(htmlContent, HtmlBase.TableOpeningTag + " class=\"top-feature  js-hover\"");
            findUrl(htmlContent, HtmlBase.TableOpeningTag + " class=\" regular-ad js-hover\"");
        }

        private string findUrl(string htmlContent, string tableBeginTag)
        {
            while (htmlContent.Contains(tableBeginTag))
            {
                htmlContent = htmlContent.Substring(htmlContent.IndexOf(tableBeginTag));
                var table = htmlContent.Substring(0, htmlContent.IndexOf(HtmlBase.TableClosingTag)) + HtmlBase.TableClosingTag;
                var urlTag = "data-vip-url=\"";
                var url = table.Substring(table.IndexOf(urlTag) + urlTag.Length);
                url = url.Substring(0, url.IndexOf("\""));
                var validUrl = getValidUrl(url);
                if (!Items.Contains(validUrl))
                {
                    if (HttpClient.ValidateUrl(validUrl) & !Items.Contains(validUrl))
                    {
                        Items.Add(validUrl);
                        //onPageLoaded(validUrl);
                    }
                }
                htmlContent = htmlContent.Replace(table, string.Empty);
            }
            return htmlContent;
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
    }
}

