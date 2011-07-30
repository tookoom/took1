using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Classes para geração de HTML.
/// </summary>
namespace TK1.Html
{
    /// <summary>
    /// Classe base para utilização com HTML.
    /// </summary>
    public class HtmlBase
    {
        protected string getIdentation(int identLevel)
        {
            string result = string.Empty;
            for (int i = 0; i < identLevel; i++)
            {
                result += "\t";
            }
            return result;
        }
    }
}
