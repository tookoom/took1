using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;
using TK1.Html.Collection;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Div HTML.
    /// </summary>
    public class HtmlHyperlink : HtmlContainer,IHtmlElement
    {
        public string Url { get; set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlHyperlink()
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlHyperlink(HtmlAttributeCollection attributes)
        {
            this.Attributes = attributes;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="children">Coleção de elementos filhos.</param>
        public HtmlHyperlink(HtmlElementCollection children)
        {
            this.Children = children;
            if (Children == null)
                Children = new HtmlElementCollection();
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="children">Coleção de elementos filhos.</param>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlHyperlink(HtmlElementCollection children, HtmlAttributeCollection attributes)
        {
            this.Attributes = attributes;
            this.Children = children;
            if (Children == null)
                Children = new HtmlElementCollection();
        }

        /// <summary>
        /// Gera elemento HTML.
        /// </summary>
        /// <returns>String com HTML do elemento e elementos filhos, se existirem.</returns>
        public string GetHtml()
        {
            return GetHtml(0);
        }
        /// <summary>
        /// Gera elemento HTML.
        /// </summary>
        /// <param name="identLevel">Nível de identação (tabulação) do elemento dentro do HTML.</param>
        /// <returns>String com HTML do elemento e elementos filhos, se existirem.</returns>
        public string GetHtml(int identLevel)
        {
            return getContainerHtml("a", identLevel);
        }

        public static HtmlHyperlink Parse(string content)
        {
            HtmlHyperlink result = new HtmlHyperlink() { HtmlContent = content };
            if (content.Contains(HyperlinkClosingTag))
            {
                result.HtmlContent = content.Substring(0, content.IndexOf(HyperlinkClosingTag) + HyperlinkClosingTag.Length);
            }
            var href = "href=\"";
            if (result.HtmlContent.Contains(href))
            {
                var url = content.Substring(content.IndexOf(href) + href.Length);
                result.Url = url.Substring(0, url.IndexOf("\""));
            }
            return result;
        }

        public static List<HtmlHyperlink> ParseList(string content)
        {
            List<HtmlHyperlink> result = new List<HtmlHyperlink>();
            while (content.Contains(HyperlinkOpeningTag))
            {
                content = content.Substring(content.IndexOf(HyperlinkOpeningTag));
                var hyperlink = content.Substring(0, content.IndexOf(HyperlinkClosingTag) + HyperlinkClosingTag.Length);
                result.Add(HtmlHyperlink.Parse(hyperlink));
                content = content.Replace(hyperlink, string.Empty);
            }
            return result;
        }
    }
}
