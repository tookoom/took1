using System;
using System.Text;
using TK1.Collection;
using TK1.Html.Elements;

namespace TK1.Html
{

	/// <summary>
	/// Representa documento HTML, com elementos Head e Body
	/// </summary>
    public class HtmlDocument : HtmlContainer
	{
        #region PRIVATE MEMBERS
        private HtmlBody body;
        private HtmlHead head;

        #endregion
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Corpo do documento HTML (body).
        /// </summary>
        public HtmlBody Body { get { return body; } }
        /// <summary>
        /// Cabeçalho do documento HTML (head).
        /// </summary>
        public HtmlHead Head { get { return head; } }

        #endregion

        /// <summary>
        /// Construtor.
        /// </summary>
        public HtmlDocument()
		{
            body = new HtmlBody();
            head = new HtmlHead();

            Children.Add(getStandardStyle());
            Children.Add(head);
            Children.Add(body);
		}

        private HtmlLiteral getStandardStyle()
        {
            //http://www.somacon.com/p141.php

            string style = "<style type=\"text/css\">"
                            + @"table {
	                            border-width: 1px;
	                            border-spacing: 0px;
	                            border-style: none;
	                            border-color: gray;
	                            border-collapse: collapse;
	                            background-color: white;
                            }
                            table th {
	                            border-width: 1px;
	                            padding: 4px;
	                            border-style: inset;
	                            border-color: gray;
	                            background-color: white;
	                            -moz-border-radius: ;
                            }
                            table td {
	                            border-width: 1px;
	                            padding: 4px;
	                            border-style: inset;
	                            border-color: gray;
	                            background-color: white;
	                            -moz-border-radius: ;
                            }
                            </style>";
            return new HtmlLiteral(style);
        }

        /// <summary>
        /// Gera documento HTML.
        /// </summary>
        /// <returns>String com HTML do documento.</returns>
        public string GetHtml()
        {
            return getContainerHtml("html", 0);
        }

	}
}
