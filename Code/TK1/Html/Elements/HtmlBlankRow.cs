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
    /// Quebra de linha HTML.
    /// </summary>
    public class HtmlBlankRow : HtmlBase, IHtmlElement
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlBlankRow()
        {
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
            string result = string.Format("{0}</br>",getIdentation(identLevel));
            return result;
        }

    }
}
