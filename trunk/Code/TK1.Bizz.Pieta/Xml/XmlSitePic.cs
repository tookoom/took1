using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Bizz.Pieta.Xml
{
    public class XmlSitePic
    {
        public int SiteCode { get; set; }
        public int SitePicCode { get; set; }
        public string FileType { get; set; }
        public string FileData { get; set; }
        public bool ExcludeSitePic { get; set; }

    }
}
