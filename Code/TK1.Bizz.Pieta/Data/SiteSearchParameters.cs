using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Pieta.Const;

namespace TK1.Bizz.Pieta.Data
{
    public class SiteSearchParameters
    {
        #region PUBLIC PROPERTIES
        public SiteAdTypes AdType{ get; set; }
        public float AreaFrom { get; set; }
        public float AreaTo { get; set; }
        public string Category { get; set; }
        public string CityName { get; set; }
        public List<string> Districts { get; set; }
        public string SiteType { get; set; }
        public int RoomsFrom { get; set; }
        public int RoomsTo { get; set; }
        public float PriceTo { get; set; }
        public float PriceFrom { get; set; }
        #endregion

        public SiteSearchParameters()
        {
            AdType = SiteAdTypes.Rent;
            AreaTo = float.MaxValue;
            AreaFrom = float.MinValue;
            Category = SiteAdCategories.Residence;
            CityName = "Porto Alegre";
            Districts = new List<string>();
            SiteType = "Apartamento";
            RoomsFrom = int.MinValue;
            RoomsTo = int.MaxValue;
            PriceTo = float.MaxValue;
            PriceFrom = float.MinValue;
        }
    }
}
