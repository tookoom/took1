using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Bizz.Mdo.Xml
{
    public class XmlHeader
    {
        public int Version { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Key { get; set; }
        public int ItemCount { get; set; }
    }
}
