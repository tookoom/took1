using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html.Elements;
using TK1.Html.Collection;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Base para outros elementos HTML que possuem filhos. Não pode ser instanciado.
    /// </summary>
    public class HtmlContainer : HtmlElement
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Coleção de elementos filhos.
        /// </summary>
        public HtmlElementCollection Children { get; set; }

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        protected HtmlContainer()
        {
            Children = new HtmlElementCollection();
        }

        protected string getContainerHtml(string containerName, int identLevel)
        {
            string result = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("{0}<{1}{2}>", getIdentation(identLevel), containerName, Attributes == null ? string.Empty : Attributes.ToHtmlAttributeString()));
            stringBuilder.Append(Environment.NewLine);
            foreach (IHtmlElement item in Children)
            {
                stringBuilder.Append(item.GetHtml(identLevel + 1));
                stringBuilder.Append(Environment.NewLine);
            }
            stringBuilder.Append(string.Format("{0}</{1}>", getIdentation(identLevel), containerName));
            result = stringBuilder.ToString();
            return result;
        }
    }
}
