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
        public int ElevatorNumber { get; set; }
        public int GarageNumber { get; set; }
        public int MaxGarageNumber { get; set; }
        public float MaxInternalArea { get; set; }
        public int MaxRoomNumber { get; set; }
        public int MaxSuiteNumber { get; set; }
        public float MaxTotalArea { get; set; }
        public float MaxValue { get; set; }
        public string Name { get; set; }
        public string ShortAdText { get; set; }
        public int SuiteNumber { get; set; }
        public int TotalFloorUnitNumber { get; set; }
        public int TotalUnitNumber { get; set; }
        public int TowerNumber { get; set; }
        public int TowerFloorNumber { get; set; }

        public List<XmlSitePic> Maps { get; set; }
        public List<XmlSitePic> BluePrints { get; set; }


    }
}
