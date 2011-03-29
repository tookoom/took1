using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Html
{
    public class HtmlBase
    {
        #region PRIVATE MEMBERS
        const string tableStartText = "<Table cellSpacing=0 cellPadding=3 width=100% border=0>";
        const string tableEndText = "</Table>";

        int headerTextSize = 4;
        int subHeaderTextSize = 3;
        int normalTextSize = 2;

        string fontFace = "verdana";

        #endregion
        #region PROTECTED MEMBERS

        protected StringBuilder stringBuilder;

        #endregion
        #region PUBLIC PROPERTIES

        public StringDictionary Attributes { get; set; }

        /// <summary>
        /// Gets or sets the size of the header text.
        /// </summary>
        /// <value>The size of the header text.</value>
        public int HeaderTextSize
        {
            get
            {
                return headerTextSize;
            }

            set
            {
                headerTextSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the sub header text.
        /// </summary>
        /// <value>The size of the sub header text.</value>
        public int SubHeaderTextSize
        {
            get
            {
                return subHeaderTextSize;
            }

            set
            {
                subHeaderTextSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the normal text.
        /// </summary>
        /// <value>The size of the normal text.</value>
        public int NormalTextSize
        {
            get
            {
                return normalTextSize;
            }

            set
            {
                normalTextSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the font face.
        /// </summary>
        /// <value>The font face.</value>
        public string FontFace
        {
            get
            {
                return fontFace;
            }

            set
            {
                fontFace = value;
            }

        }

        #endregion

        public HtmlBase()
		{
			stringBuilder = new StringBuilder();
            Attributes = new StringDictionary();
		}

        //protected string getAttributes()
        //{
        //    string result = string.Empty;
        //    if (Attributes != null)
        //    {
        //        foreach (var attribute in Attributes)
        //        {
        //            result += string.Format(" {0}=\"{1}\"", attribute.Key, attribute.Value);
        //        }
        //    }
        //    return result;
        //}


    }
}
