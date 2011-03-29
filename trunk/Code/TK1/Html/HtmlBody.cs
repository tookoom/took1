using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Html
{
    public class HtmlBody : HtmlBase
    {
        public HtmlBody()
        {
        }

        public string GetHtml()
        {
            return string.Format("<body{0}>", Attributes.ToHtmlAttributeString())
                + Environment.NewLine
                + stringBuilder.ToString()
                + Environment.NewLine
                + "</body>";
        }

        public void AppendBlankRow()
        {
            stringBuilder.Append("<br />");
            stringBuilder.Append(Environment.NewLine);
        }
        public void AppendDiv(string content)
        {
            AppendDiv(content, null);
        }
        public void AppendDiv(string content, StringDictionary divAttributes)
        {
            stringBuilder.Append(string.Format("<div{0}>", divAttributes == null ? string.Empty : divAttributes.ToHtmlAttributeString()));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(content ?? string.Empty);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("</div>");
            stringBuilder.Append(Environment.NewLine);
        }
        public void AppendHeaderN(int n, string content)
        {
            AppendHeaderN(n, content, null);
        }
        public void AppendHeaderN(int n, string content, StringDictionary hAttributes)
        {
            stringBuilder.Append(string.Format("<h{0}{1}>", n, hAttributes == null ? string.Empty : hAttributes.ToHtmlAttributeString()));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(content ?? string.Empty);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(string.Format("</h{0}>",n));
            stringBuilder.Append(Environment.NewLine);
        }
        public void AppendLiteral(string content)
        {
            stringBuilder.Append(content ?? string.Empty);
            stringBuilder.Append(Environment.NewLine);
        }
        public void AppendParagraph(string content)
        {
            AppendParagraph(content, null);
        }
        public void AppendParagraph(string content, StringDictionary pAttributes)
        {
            stringBuilder.Append(string.Format("<p{0}>", pAttributes == null ? string.Empty : pAttributes.ToHtmlAttributeString()));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(content ?? string.Empty);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("</p>");
            stringBuilder.Append(Environment.NewLine);
        }





        /// <summary>
        /// Formats to a html row with the text with 4 size text
        /// </summary>
        /// <param name="text">The text.</param>
        public void AppendHeader(string text)
        {
            FormatToRow(text, HeaderTextSize, true);
        }

        /// <summary>
        /// Formats to a html row with the text with 3 size text
        /// </summary>
        /// <param name="text">The text.</param>
        public void AppendSubHeader(string text)
        {
            FormatToRow(text, SubHeaderTextSize, true);
        }

        /// <summary>
        /// Formats to a html row with the text with 2 size text
        /// </summary>
        /// <param name="text">The text.</param>
        public void AppendPara(string text)
        {
            FormatToRow(text, NormalTextSize, false);
        }

        ///// <summary>
        ///// Appends the blank row.
        ///// </summary>
        //public void AppendBlankRow()
        //{
        //    FormatToBlankRow();
        //}

        /// <summary>
        /// Formats a blank HTML row.
        /// </summary>
        public void FormatToBlankRow()
        {
            stringBuilder.Append("<TR> <TD height=20> </TD> </TR>");
        }

        /// <summary>
        /// Formats to row with Normal-Text size.
        /// </summary>
        /// <param name="text">The text.</param>
        public void FormatToRow(string text)
        {
            FormatToRow(text, NormalTextSize, false);
        }

        /// <summary>
        /// Formats to row.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fontSize">Size of the font.</param>
        public void FormatToRow(string text, int fontSize)
        {
            FormatToRow(text, fontSize, false);
        }

        /// <summary>
        /// Formats to row.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="bold">if set to <c>true</c> [bold].</param>
        public void FormatToRow(string text, int fontSize, bool bold)
        {
            string boldMarker = (bold ? "bold" : "normal");
            stringBuilder.Append("<TR> <TD align=left><font size=" + fontSize.ToString() + " face=" + FontFace + " style=\"FONT-WEIGHT: " + boldMarker + "\"  > " + text + "</font></TD> </TR>");
        }

        /// <summary>
        /// Formats the provided key/values by separating them thru a colon, and considers the smallest text size '2'.
        /// </summary>
        /// <param name="strLabel"></param>
        /// <param name="strValue"></param>
        public void FormatToRowInColonSeparatedText(string strLabel, string strValue)
        {
            FormatToRowInColonSeparatedText(strLabel, strValue, false);
        }

        /// <summary>
        /// Formats the provided key/values by separating them thru a colon, and considers the smallest text size '2'.
        /// </summary>
        /// <param name="strLabel"></param>
        /// <param name="strValue"></param>
        /// <param name="bold"></param>
        public void FormatToRowInColonSeparatedText(string strLabel, string strValue, bool bold)
        {
            strLabel = "<b>" + strLabel + " : </b>";
            if (bold) strValue = "<b>" + strValue + "</b>";
            FormatToRow(strLabel + " " + strValue, NormalTextSize, bold);
        }

    }
}
