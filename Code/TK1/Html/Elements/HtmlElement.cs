using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;
using TK1.Html.Elements;
using TK1.Html.Collection;

/// <summary>
/// Elementos HTML.
/// </summary>
namespace TK1.Html.Elements
{
    /// <summary>
    /// Base para outros elementos HTML. Não pode ser instanciado.
    /// </summary>
    public class HtmlElement : HtmlBase
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Coleção de atributos do elemento.
        /// </summary>
        public HtmlAttributeCollection Attributes { get; set; }
        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        protected HtmlElement()
		{
            Attributes = new HtmlAttributeCollection();
		}

    }
}
