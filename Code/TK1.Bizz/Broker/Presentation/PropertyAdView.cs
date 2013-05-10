using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Broker.Presentation
{
    public class PropertyAdView : PropertyAdBaseView
    {
        public bool IsAddressVisible { get; set; }
        public bool IsAreaDescriptionVisible { get { return !string.IsNullOrEmpty(AreaDescription); } }
        public bool IsAreaNameVisible { get; set; }
        public bool IsCityTaxesVisible { get { return CityTaxes != 0; } }
        public bool IsCondoDescriptionVisible { get { return !string.IsNullOrEmpty(CondoDescription); } }
        public bool IsCondoTaxesVisible { get { return CondoTaxes != 0; } }
        public bool IsFeatured { get; set; }
        public bool IsRoomNameVisible { get; set; }
        public bool IsTitleVisible { get { return !string.IsNullOrEmpty(Title); } }


        public float CityTaxes { get; set; }
        public float CondoTaxes { get; set; }
        public float InternalArea { get; set; }
        public float TotalArea { get; set; }
        public int TotalRooms { get; set; }
        public float Value { get; set; }
    }
}
