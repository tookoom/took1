using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Took1.Silverlight.Xml
{
    public class XmlLoader
    {
        public static string LoadAttributeValue(XElement node, string attributeName)
        {
            string result = null;
            XAttribute xAttribute = null;
            if (node != null)
                xAttribute = node.Attribute(attributeName);
            if(xAttribute!= null)
                result = xAttribute.Value;
            return result;
        }
    }
}
