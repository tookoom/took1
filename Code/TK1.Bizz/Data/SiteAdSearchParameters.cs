using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Data.Presentation;

namespace TK1.Bizz.Data
{
    public class SiteAdSearchParameters
    {
        #region PUBLIC PROPERTIES
        public SiteAdTypes AdType { get; set; }
        public int AdTypeID { get { return (int)AdType; } }
        
        public float AreaFrom { get; set; }
        public float AreaTo { get; set; }
        public string Category { get; set; }
        public string CityName { get; set; }
        public int Code { get; set; }
        public string CustomerCodename { get; set; }
        public List<string> Districts { get; set; }
        public string SiteType { get; set; }
        public SiteAdSearchResultOrders ResultOrdering { get; set; }
        public int RoomsFrom { get; set; }
        public int RoomsTo { get; set; }
        public float PriceTo { get; set; }
        public float PriceFrom { get; set; }
        #endregion

        public SiteAdSearchParameters()
        {
            AreaTo = float.MaxValue;
            AreaFrom = float.MinValue;
            Category = SiteAdCategories.Residencial.ToString();
            CityName = "Porto Alegre";
            Code = 0;
            CustomerCodename = string.Empty;
            Districts = new List<string>();
            SiteType = "Apartamento";
            ResultOrdering = SiteAdSearchResultOrders._Undefined;
            RoomsFrom = int.MinValue;
            RoomsTo = int.MaxValue;
            PriceTo = float.MaxValue;
            PriceFrom = float.MinValue;
        }
    }
}
