using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html;
using TK1.Html.Elements;

namespace TK1.Bizz.Pandolfo
{
    public class HtmlTemplates
    {
        public static string GetContactMailTemplate()
        {
            HtmlDocument html = new HtmlDocument();
            html.Head.Title = "Mensagem";
            html.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");
            
            html.Body.Children.Add(new HtmlHeading(4,"Mensagem enviada através do site Pandolfo Imóveis:"));
            //html.Body.Children.Add(new HtmlParagraph("Tipo de contato: #TK1_TAG_CONTACT_TYPE#"));
            html.Body.Children.Add(new HtmlParagraph("Data do envio: #TK1_TAG_TIMESTAMP#"));
            html.Body.Children.Add(new HtmlBlankRow());

            html.Body.Children.Add(new HtmlHeading(4,"Informações para contato:"));
            html.Body.Children.Add(new HtmlParagraph("Nome: #TK1_TAG_NAME#"));
            html.Body.Children.Add(new HtmlParagraph("E-mail: #TK1_TAG_MAIL#"));
            html.Body.Children.Add(new HtmlParagraph("Telefone: #TK1_TAG_PHONE#"));
            //html.Body.Children.Add(new HtmlParagraph("Forma de contato preferida: #TK1_TAG_CONTACT#"));
            html.Body.Children.Add(new HtmlBlankRow());

            html.Body.Children.Add(new HtmlHeading(4, "Mensagem:"));
            html.Body.Children.Add(new HtmlParagraph("#TK1_TAG_MESSAGE#"));

            return html.GetHtml();
        }
        public static string GetXmlSalesFileLoadTemplate()
        {
            HtmlDocument html = new HtmlDocument();
            html.Head.Title = "Carga de Arquivos";
            html.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");

            html.Body.Children.Add(new HtmlHeading(4, "Carga de arquivos do cadastro de imóveis a venda"));
            html.Body.Children.Add(new HtmlParagraph("Data da carga: " + MailTemplateTags.General.Timestamp));
            html.Body.Children.Add(new HtmlParagraph("Resultado: " + MailTemplateTags.General.Result));
            html.Body.Children.Add(new HtmlBlankRow());
            html.Body.Children.Add(new HtmlHeading(4, "Mensagens geradas:"));
            html.Body.Children.Add(new HtmlLiteral(MailTemplateTags.General.Message));

            return html.GetHtml();
        }

    }
}
