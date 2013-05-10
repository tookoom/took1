using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Broker.Presentation
{
    public class PropertyReleaseAdView : PropertyAdBaseView
    {
        //VISIBILITY 
        public bool IsAreaDescriptionVisible { get { return !string.IsNullOrEmpty(AreaDescription); } }
        public bool IsCondoDescriptionVisible { get { return !string.IsNullOrEmpty(CondoDescription); } }

        // DISPLAY TEXTS
        public string AreaText { get; set; }
        public string RoomText { get; set; }
        public string ValueText { get; set; }

        // PROPERTY RELEASE AD INFO
        public string AddressComplement { get; set; }
        public int AddressNumber { get; set; }
        public string ConstructorName { get; set; }

        public int MaxTotalRooms { get; set; }
        public float MaxExternalArea { get; set; }
        public float MaxInternalArea { get; set; }
        public int MaxParkingLots { get; set; }
        public int MaxSuites { get; set; }
        public float MaxTotalArea { get; set; }
        public float MaxValue { get; set; }

        public int MinTotalRooms { get; set; }
        public float MinExternalArea { get; set; }
        public float MinInternalArea { get; set; }
        public int MinParkingLots { get; set; }
        public int MinSuites { get; set; }
        public float MinTotalArea { get; set; }
        public float MinValue { get; set; }

        public string Name { get; set; }

        public int TotalElevators { get; set; }
        public int TotalFloorUnits { get; set; }
        public int TotalTowerFloors { get; set; }
        public int TotalTowers { get; set; }
        public int TotalUnits { get; set; }
    }
}
