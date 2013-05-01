using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Collection;

namespace TK1.Bizz.Mdo.Selling.Xml
{
    public class XmlSiteRelease : XmlSite
    {
        public string AdText { get; set; }
        public string AddressComplement { get; set; }
        public string ConstructorName { get; set; }
        public string DeliveryPrevision { get; set; }
        public int TotalElevators { get; set; }
        public int MinParkingLots { get; set; }
        public int MaxParkingLots { get; set; }
        public float MaxInternalArea { get; set; }
        public int MaxTotalRooms { get; set; }
        public int MaxSuites { get; set; }
        public float MaxTotalArea { get; set; }
        public float MaxValue { get; set; }
        public string Name { get; set; }
        public string ShortAdText { get; set; }
        public int MinSuites { get; set; }
        public int TotalFloorUnits { get; set; }
        public int TotalUnits { get; set; }
        public int TotalTowers { get; set; }
        public int TotalTowerFloors { get; set; }

        public List<XmlSitePic> Maps { get; set; }
        public List<XmlSitePic> BluePrints { get; set; }


    }
}
