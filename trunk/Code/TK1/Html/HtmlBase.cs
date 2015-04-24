using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Classes para geração de HTML.
/// </summary>
namespace TK1.Html
{
    /// <summary>
    /// Classe base para utilização com HTML.
    /// </summary>
    public class HtmlBase
    {
        public const string DivClosingTag = "</div>";
        public const string DivOpeningTag = "<div";
        public const string HyperlinkClosingTag = "</a>";
        public const string HyperlinkOpeningTag = "<a";
        public const string TableClosingTag = "</table>";
        public const string TableOpeningTag = "<table";

        public string HtmlContent { get; set; }
        protected string getIdentation(int identLevel)
        {
            string result = string.Empty;
            for (int i = 0; i < identLevel; i++)
            {
                result += "\t";
            }
            return result;
        }

        protected static string RemoveQuotes(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (text.Contains("\""))
                text = text.Replace("\"", "");
            if (text.Contains("'"))
                text = text.Replace("'", "");
            return text;
        }
    }
}
