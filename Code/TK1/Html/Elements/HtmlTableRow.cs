﻿using System;
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
    /// Linha de tabela HTML (td).
    /// </summary>
    public class HtmlTableRow : HtmlContainer, IHtmlElement
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlTableRow()
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlTableRow(HtmlAttributeCollection attributes)
        {
            this.Attributes = attributes;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="children">Coleção de elementos filhos.</param>
        public HtmlTableRow(HtmlElementCollection children)
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
        public HtmlTableRow(HtmlElementCollection children, HtmlAttributeCollection attributes)
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
            return getContainerHtml("tr", identLevel);
        }
    }
}
