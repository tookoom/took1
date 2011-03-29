using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html;

namespace TK1.Bizz.Pieta.Const
{
    public class HtmlTemplates
    {
        public static string GetContactMailTemplate()
        {
            HtmlBuilder html = new HtmlBuilder();
            html.Head.Title("Mensagem");
            html.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");
            
            html.Body.AppendHeaderN(4, "Mensagem enviada através do site Pietá Imóveis:");
            html.Body.AppendParagraph("Tipo de contato: #TK1_TAG_CONTACT_TYPE#");
            html.Body.AppendParagraph("Data do envio: #TK1_TAG_TIMESTAMP#");
            html.Body.AppendBlankRow();

            html.Body.AppendHeaderN(4, "Informações para contato:");
            html.Body.AppendParagraph("Nome: #TK1_TAG_NAME#");
            html.Body.AppendParagraph("E-mail: #TK1_TAG_MAIL#");
            html.Body.AppendParagraph("Telefone: #TK1_TAG_PHONE#");
            html.Body.AppendParagraph("Forma de contato preferida: #TK1_TAG_CONTACT#");
            html.Body.AppendBlankRow();

            html.Body.AppendHeaderN(4, "Mensagem:");
            html.Body.AppendParagraph("#TK1_TAG_MESSAGE#");

            return html.GetHtmlContent();
        }
//        public static string GetContactMailTemplate()
//        {
//            string result = "<head>    <title></title></head>";
//            result += "<body style='font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif'>";
//            result += @"<h4>        Mensagem enviada através do site Pietá Imóveis:    </h4>
//    
//    <p>
//        Tipo de contato: #TK1_TAG_CONTACT_TYPE#<br />
//        Data do envio: #TK1_TAG_TIMESTAMP#<br />
//    </p>
//
//    <h4>
//        Informações para contato:
//    </h4>
//
//    <p>
//        Nome: #TK1_TAG_NAME#<br />
//        E-mail: #TK1_TAG_MAIL#<br />
//        Telefone: #TK1_TAG_PHONE#<br />
//        Forma de contato preferida: #TK1_TAG_CONTACT#<br />
//    </p>
//
//
//    <h4>
//        Mensagem:<br />
//    </h4>
//
//    <p>
//        #TK1_TAG_MESSAGE#<br />
//    </p>
//
//</body>
//</html>
//";
//            return result;
//        }
    }
}
