using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Const;

namespace TK1.Bizz.Mdo.Data
{
    public class MdoSiteSearchParameters
    {
        #region PUBLIC PROPERTIES
        public float AreaFrom { get; set; }
        public float AreaTo { get; set; }
        public string Category { get; set; }
        public string CityName { get; set; }
        public int Code { get; set; }
        public List<string> Districts { get; set; }
        public string MdoAcronym { get; set; }
        public int MdoCode { get; set; }
        public string SiteType { get; set; }
        public MdoSiteSearchResultOrder ResultOrdering { get; set; }
        public int RoomsFrom { get; set; }
        public int RoomsTo { get; set; }
        public float PriceTo { get; set; }
        public float PriceFrom { get; set; }
        #endregion

        public MdoSiteSearchParameters()
        {
            AreaTo = float.MaxValue;
            AreaFrom = float.MinValue;
            Category = SiteAdCategories.Residence;
            CityName = "Porto Alegre";
            Code = 0;
            Districts = new List<string>();
            MdoAcronym = string.Empty;
            MdoCode = 0;
            SiteType = "Apartamento";
            ResultOrdering = MdoSiteSearchResultOrder._Undefined;
            RoomsFrom = int.MinValue;
            RoomsTo = int.MaxValue;
            PriceTo = float.MaxValue;
            PriceFrom = float.MinValue;
        }
    }
}
