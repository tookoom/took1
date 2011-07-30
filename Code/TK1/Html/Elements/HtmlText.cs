using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html.Elements;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Base para outros elementos HTML que possuem texto. Não pode ser instanciado.
    /// </summary>
    public class HtmlText : HtmlElement
    {
        #region PRIVATE MEMBERS
        private string text;

        #endregion
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Indica se texto será renderizado em negrito.
        /// </summary>
        public bool IsBold { get; set; }
        /// <summary>
        /// Texto a ser exibido.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsBold)
                    return string.Format("<b>{0}</b>", text);
                else
                    return text;
            }
            set { text = value ?? string.Empty; }
        }

        #endregion

        protected string getTextItemHtml(string itemName, int identLevel)
        {
            string result = string.Empty;
            result = string.Format("{0}<{1}{2}>{3}</{1}>", getIdentation(identLevel), itemName, Attributes == null ? string.Empty : Attributes.ToHtmlAttributeString(), text);
            return result;
        }

    }
}
