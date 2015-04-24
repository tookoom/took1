using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Interface para elementos HTML.
    /// </summary>
    public interface IHtmlElement
    {
        /// <summary>
        /// Gera elemento HTML.
        /// </summary>
        /// <returns>String com HTML do elemento e elementos filhos, se existirem.</returns>
        string GetHtml();
        /// <summary>
        /// Gera elemento HTML.
        /// </summary>
        /// <param name="identLevel">Nível de identação (tabulação) do elemento dentro do HTML.</param>
        /// <returns>String com HTML do elemento e elementos filhos, se existirem.</returns>
        string GetHtml(int identLevel);


    }
}
