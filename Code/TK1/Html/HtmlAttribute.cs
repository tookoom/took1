using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

/// <summary>
/// Classes para geração de HTML.
/// </summary>
namespace TK1.Html
{
    /// <summary>
    /// Atributo HTML.
    /// </summary>
    public class HtmlAttribute : StringPair
    {
        /// <summary>
        /// Cria instância de HtmlAttribute com nome e valor indicados.
        /// </summary>
        /// <param name="name">Nome do atributo.</param>
        /// <param name="value">Valor do atributo.</param>
        /// <returns>Instância de HtmlAttribute representando o atributo conforme parâmetros.</returns>
        public static HtmlAttribute CreateAttribute(string name, string value)
        {
            return new HtmlAttribute() { Key = name ?? string.Empty, Value = value ?? string.Empty };
        }
        /// <summary>
        /// Cria instância de HtmlAttribute "class" com valor indicado.
        /// </summary>
        /// <param name="className">Nome da classe.</param>
        /// <returns>Instância de HtmlAttribute representando o atributo conforme parâmetros.</returns>
        public static HtmlAttribute CreateClassAttribute(string className)
        {
            return new HtmlAttribute() { Key = "class", Value = className ?? string.Empty };
        }
        /// <summary>
        /// Cria instância de HtmlAttribute "style" com valor indicado.
        /// </summary>
        /// <param name="style">Definição do estilo.</param>
        /// <returns>Instância de HtmlAttribute representando o atributo conforme parâmetros.</returns>
        public static HtmlAttribute CreateStyleAttribute(string style)
        {
            return new HtmlAttribute() { Key = "style", Value = style ?? string.Empty };
        }
    }
}
