using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TK1.Html.Elements;
using TK1.Net;

namespace TK1.Html.Bot.Google
{
    public class GoogleTagBot: HtmlBotBase , IHtmlBot 
    {
        #region PUBLIC PROPERTIES
        public string BaseUrl { get; set; }
        public string PushScript { get; set; }
        public int WaitInterval { get; set; }
        #endregion

        public string GetValue(string content, string key)
        {
            string result = string.Empty;
            if (content == null)
                throw new ArgumentNullException("content");
            if (key == null)
                throw new ArgumentNullException("key");
            var openTag = "googletag.pubads().setTargeting(";
            var closeTag = ");";
            List<string> elements = HtmlBotBase.GetElements(content, openTag, closeTag);
            foreach (var item in elements)
            {
                List<string> arguments = HtmlBotBase.GetScriptArguments(item);
                if (arguments.Count == 2)
                {
                    if(arguments[0].Contains(key))
                        result = HtmlBase.RemoveQuotes(arguments[1]);
                }
            }
            return result;
        }

        public void LoadHtml(string htmlContent)
        {
            var openTag = "googletag.cmd.push(function() {";
            var closeTag = "});";
            PushScript = HtmlBotBase.GetElement(htmlContent, openTag, closeTag);

        }
        public void LoadFile(string filePath)
        {
            var htmlContent = File.ReadAllText(filePath);
            LoadHtml(htmlContent);
        }
        public void LoadPage(string url)
        {
            var htmlContent = HttpClient.GetContent(url);
            LoadHtml(htmlContent);
        }

    }
}
