using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TK1.Xml
{
    public class XmlLoader
    {
        /// <summary>
        /// Procura por atributo em elemento XML. Retorna valor do atributo ou nulo caso
        /// atributo não exista no elemento.
        /// </summary>
        /// <param name="node">Elemento a ser pesquisado.</param>
        /// <param name="attributeName">Nome do atributo cujo valor será retornado.</param>
        public static string GetAttributeValue(XElement node, string attributeName)
        {
            string result = null;
            XAttribute xAttribute = null;
            if (node != null)
                xAttribute = node.Attribute(attributeName);
            if (xAttribute != null)
                result = xAttribute.Value;
            return result;
        }
        /// <summary>
        /// Retorna valor do elemento ou nulo caso não exista o elemento.
        /// </summary>
        /// <param name="node">Elemento a ser pesquisado.</param>
        /// <param name="elementName">Nome do atributo cujo valor será retornado.</param>
        public static string GetElementValue(XElement node, string elementName)
        {
            string result = null;
            XElement xElement = null;
            if (node != null)
                xElement = node.Element(elementName);
            if (xElement != null)
                result = xElement.Value;
            return result;
        }

    }
}
