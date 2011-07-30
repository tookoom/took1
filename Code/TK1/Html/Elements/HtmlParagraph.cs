using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Parágrafo HTML (p).
    /// </summary>
    public class HtmlParagraph: HtmlText,IHtmlElement
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="value">Texto do parágrafo.</param>
        public HtmlParagraph(string value)
        {
            this.Text = value;
            this.IsBold = false;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="value">Texto do parágrafo.</param>
        /// <param name="isBold">Indica se texto do heading deve ser exibido em negrito.</param>
        public HtmlParagraph(string value, bool isBold)
        {
            this.Text = value;
            this.IsBold = isBold;
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
            return getTextItemHtml("p", identLevel);
        }

    }
}
