using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Bizz.Pieta.Xml
{
    public class XmlSite
    {
        public int SiteCode { get; set; }
        public string SiteType { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public string ZipCode { get; set; }
        public string InternetDescription { get; set; }
        public float Value { get; set; }
        public bool IsHighlighted { get; set; }
        public bool ExcludeSite{ get; set; }
        public string AdType { get; set; }
        public int RoomNumber { get; set; }
        public string BuildingName { get; set; }
        public float Area { get; set; }
        public bool HasBanner { get; set; }
        public bool IsExclusive { get; set; }
        public StringDictionary DescriptionCollection { get; set; }

    }
}
