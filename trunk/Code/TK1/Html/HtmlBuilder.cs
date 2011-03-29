using System;
using System.Text;
using TK1.Collection;

namespace TK1.Html
{

	/// <summary>
	/// This class is used to provide pre-defined html formats and texts that are required to implement on simple mail and print functionalities.
	/// </summary>
	public class HtmlBuilder
	{
        public HtmlBody Body { get; set; }
        public HtmlHead Head { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HTMLUtils"/> class.
		/// </summary>
        public HtmlBuilder()
		{
            Body = new HtmlBody();
            Head = new HtmlHead();
		}

        public string GetHtmlContent()
        {
            string result = "<html>"
                + Environment.NewLine
                + Head.GetHtml()
                + Environment.NewLine
                + Body.GetHtml()
                + Environment.NewLine
                + "</html>";
            return result;
        }

        public static string Div(string content)
        {
            return Div(content, null);
        }
        public static string Div(string content, StringDictionary divAttributes)
        {
            string div = string.Format("<div{0}>", divAttributes == null ? string.Empty : divAttributes.ToHtmlAttributeString());
            div += Environment.NewLine;
            div += content ?? string.Empty;
            div += Environment.NewLine;
            div += "</div>";
            div += Environment.NewLine;

            return div;
        }

		#region SAMPLE 
		
		/// <summary>
		/// This is the tester class of the current parent class, which contains the sample code snippets to use this class properly in the real-world scenaris.
		/// </summary>
		public class Tester
		{
			public static void UseThis()
			{
                //HtmlBuilder util = new HtmlBuilder();

                //util.AppendHeader("Header");
                //util.AppendSubHeader("Sub Header");
                //util.AppendPara("Hello this is a para");
                //util.AppendPara("Hello this is the second para");
                //util.AppendBlankRow();
                //util.FormatToRowInColonSeparatedText("Label 1","Value 1", false);
                //util.FormatToRowInColonSeparatedText("Label 2","Value 2", false);
                //util.AppendBlankRow();
                //util.AppendPara("Hello this is footer para");
                //util.AppendSubHeader("Sub Footer");
                //util.AppendHeader("Footer");

                //System.Web.HttpContext.Current.Response.Write(util.FormattedHTMLText);
			}
		}

		#endregion
	}
}
