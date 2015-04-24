using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html.Collection;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Cabeçalho de documento HTML.
    /// </summary>
    public class HtmlHead : HtmlContainer, IHtmlElement
    {
        #region CONST VALUES
        public const string TitleClosingTag = "</title>";
        public const string TitleOpeningTag = "<title>";

        #endregion
        public string Title { get; set; }
        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlHead()
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlHead(HtmlAttributeCollection attributes)
        {
            this.Attributes = attributes;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="children">Coleção de elementos filhos.</param>
        public HtmlHead(HtmlElementCollection children)
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
        public HtmlHead(HtmlElementCollection children, HtmlAttributeCollection attributes)
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
            return getContainerHtml("head", identLevel);
        }

        public static HtmlHead Parse(string htmlContent)
        {
            if (htmlContent == null)
                throw new ArgumentNullException("htmlContent");

            HtmlHead result = new HtmlHead() { HtmlContent = htmlContent };
            //result.HtmlContent = HtmlBase.
            //if (htmlContent.Contains(HeadeClosingTag))
            //{
            //    result.HtmlContent = content.Substring(0, content.IndexOf(DivClosingTag) + DivClosingTag.Length);
            //}
            //result.Children.AddRange(HtmlHyperlink.ParseList(result.HtmlContent));
            return result;
        }

    }
}
