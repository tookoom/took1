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
    /// Heading HTML ("h1" a "h6").
    /// </summary>
    public class HtmlHeading : HtmlText, IHtmlElement
    {
        #region PRIVATE MEMBERS
        private int headerNumber = 1;

        #endregion


        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="headerNumber">Nível do heading. Valores válidos são de 1 a 6. Sa valor inválido for fornecido, heading manterá nível 1 como padrão.</param>
        /// <param name="value">Texto do heading.</param>
        public HtmlHeading(int headerNumber, string value)
        {
            if(headerNumber > 0 & headerNumber < 7)
                this.headerNumber = headerNumber;
            this.Text = value;
            this.IsBold = false;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="headerNumber">Nível do heading. Valores válidos são de 1 a 6. Sa valor inválido for fornecido, heading manterá nível 1 como padrão.</param>
        /// <param name="value">Texto do heading.</param>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlHeading(int headerNumber, string value, HtmlAttributeCollection attributes)
        {
            if (headerNumber > 0 & headerNumber < 7)
                this.headerNumber = headerNumber;
            this.Attributes = attributes;
            this.Text = value;
            this.IsBold = false;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="headerNumber">Nível do heading. Valores válidos são de 1 a 6. Sa valor inválido for fornecido, heading manterá nível 1 como padrão.</param>
        /// <param name="value">Texto do heading.</param>
        /// <param name="isBold">Indica se texto do heading deve ser exibido em negrito.</param>
        public HtmlHeading(int headerNumber, string value, bool isBold)
        {
            if (headerNumber > 0 & headerNumber < 7)
                this.headerNumber = headerNumber;
            this.Text = value;
            this.IsBold = isBold;
        }
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="headerNumber">Nível do heading. Valores válidos são de 1 a 6. Sa valor inválido for fornecido, heading manterá nível 1 como padrão.</param>
        /// <param name="value">Texto do heading.</param>
        /// <param name="isBold">Indica se texto do heading deve ser exibido em negrito.</param>
        /// <param name="attributes">Coleção de atributos do elemento.</param>
        public HtmlHeading(int headerNumber, string value, bool isBold, HtmlAttributeCollection attributes)
        {
            if (headerNumber > 0 & headerNumber < 7)
                this.headerNumber = headerNumber;
            this.Attributes = attributes;
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
            return getTextItemHtml(string.Format("h{0}", headerNumber), identLevel);
        }

    }
}
