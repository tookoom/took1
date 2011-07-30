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
    /// HTML customizado, inserido no documento sem qualquer manipulação. 
    /// </summary>
    public class HtmlLiteral : HtmlBase, IHtmlElement
    {
        #region PRIVATE MEMBERS
        private string value;

        #endregion

        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlLiteral(string value)
        {
            this.value = value;
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
            string result = string.Format("{0}{1}", getIdentation(identLevel), value ?? string.Empty);
            return result;
        }

    }
}
