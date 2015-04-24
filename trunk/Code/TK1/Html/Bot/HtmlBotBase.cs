using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Html.Bot
{
    public class HtmlBotBase : HtmlBase  
    {
        #region PUBLIC PROPERTIES
        #endregion
        public static string GetElement(string htmlContent, string openTag, string closeTag)
        {
            string result = string.Empty;
            if (htmlContent == null)
                throw new ArgumentNullException("htmlContent");
            if (htmlContent.Contains(openTag) & htmlContent.Contains(closeTag))
            {
                htmlContent = htmlContent.Substring(htmlContent.IndexOf(openTag));
                result = htmlContent.Substring(0, htmlContent.IndexOf(closeTag)) + closeTag;
            }
            return result;
        }
        public static string GetElementContent(string htmlContent, string openTag, string closeTag)
        {
            string result = GetElement(htmlContent, openTag, closeTag);
            if (result != null)
            {
                if (result.Contains(openTag) & result.Contains(closeTag))
                {
                    result = result.Replace(openTag, string.Empty);
                    result = result.Replace(closeTag, string.Empty);
                }
            }
            return result;
        }

        public static List<string> GetElements(string htmlContent, string openTag, string closeTag)
        {
            var result = new List<string>();
            if (htmlContent == null)
                throw new ArgumentNullException("htmlContent");
            if (openTag == null)
                throw new ArgumentNullException("openTag");
            if (closeTag == null)
                throw new ArgumentNullException("closeTag");
            if (htmlContent.Contains(openTag) & htmlContent.Contains(closeTag))
            {
                while (htmlContent.Contains(openTag))
                {
                    htmlContent = htmlContent.Substring(htmlContent.IndexOf(openTag));
                    var html = htmlContent.Substring(0, htmlContent.IndexOf(closeTag)) + closeTag;
                    result.Add(html);
                    htmlContent = htmlContent.Replace(html, string.Empty);
                }

            }
            return result;
        }

        public static List<string> GetScriptArguments(string script)
        {
            var result = new List<string>();
            if (script == null)
                throw new ArgumentNullException("htmlContent");
            var openTag = "(";
            var closeTag = ");";
            if (script.Contains(openTag) & script.Contains(closeTag))
            {
                script = script.Substring(script.LastIndexOf(openTag)+1);
                script = script.Substring(0, script.IndexOf(closeTag));
                if (script.Contains(","))
                {
                    foreach (var item in script.Split(','))
                    {
                        result.Add(HtmlBase.RemoveQuotes(item.Trim()));
                    }
                }
            }
            return result;
        }
    }
}
