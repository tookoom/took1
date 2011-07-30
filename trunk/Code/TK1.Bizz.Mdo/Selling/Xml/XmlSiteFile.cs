using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Xml;

namespace TK1.Bizz.Mdo.Selling.Xml
{
    public class XmlSiteFile
    {
        public XmlHeader Header { get; set; }
        public List<XmlSite> Sites { get; set; }
    }
}
