using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Data.Presentation
{
    public class SiteAdView
    {
        public SiteAdCategories AdCategory { get; set; }
        public SiteAdTypes AdType { get; set; }
        public int AdTypeID { get { return (int)AdType; } }

        public bool IsAddressVisible { get; set; }
        public bool IsAreaDescriptionVisible { get { return !string.IsNullOrEmpty(AreaDescription); } }
        public bool IsAreaNameVisible { get; set; }
        public bool IsCondoDescriptionVisible { get { return !string.IsNullOrEmpty(CondoDescription); } }
        public bool IsRoomNameVisible { get; set; }
        public bool IsTaxVisible { get; set; }

        public int Code { get; set; }
        public int CustomerID { get; set; }
        public int SiteTotalRooms { get; set; }

        public float CityTaxes { get; set; }
        public float CondoTaxes { get; set; }
        public float SiteTotalArea { get; set; }
        public float Value { get; set; }

        public string Address { get; set; }
        public string AdTypeName { get; set; }
        public string AreaDescription { get; set; }
        public string City { get; set; }
        public string CondoDescription { get; set; }
        public string District { get; set; }
        public string FullDescription { get; set; }
        public string MainPicUrl { get; set; }
        public string ShortDescription { get; set; }
        public string SiteType { get; set; }
        public string SiteTypeRoomName { get; set; }
        public string Title { get; set; }


    }
}
