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
    /// Div HTML.
    /// </summary>
    public class HtmlDiv : HtmlContainer,IHtmlElement
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlDiv()
        {
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlDiv(HtmlAttributeCollection attributes)
        {
            this.Attributes = attributes;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="children">Coleção de elementos filhos.</param>
        public HtmlDiv(HtmlElementCollection children)
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
        public HtmlDiv(HtmlElementCollection children, HtmlAttributeCollection attributes)
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
            return getContainerHtml("div", identLevel);
        }

        public static HtmlDiv Parse(string content)
        {
            HtmlDiv result = new HtmlDiv() { HtmlContent = content };
            if (content.Contains(DivClosingTag ))
            {
                result.HtmlContent = content.Substring(0, content.IndexOf(DivClosingTag) + DivClosingTag.Length);
            }
            result.Children.AddRange(HtmlHyperlink.ParseList(result.HtmlContent));
            return result;
        }
    }
}
