using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Bizz.Mdo.Selling.Xml
{
    public class XmlSite
    {
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AdType { get; set; }
        public string AreaDescription { get; set; }
        public string BuildingName { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string CondDescription { get; set; }
        public string District { get; set; }
        public float ExternalArea { get; set; }
        public float InternalArea { get; set; }
        public string InternetDescription { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsHighlighted { get; set; }
        public int RoomNumber { get; set; }
        public int SiteCode { get; set; }
        public string SiteType { get; set; }
        public string ShortDescription { get; set; }
        public float TotalArea { get; set; }
        public string UF { get; set; }
        public float Value { get; set; }
        public string ZipCode { get; set; }

        public StringDictionary Details { get; set; }
        public List<XmlSitePic> Pictures { get; set; }


        //public bool HasBanner { get; set; }

    }
}
