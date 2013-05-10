using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Broker.Presentation
{
    public class PropertyAdSearchParameters
    {
        #region PUBLIC PROPERTIES
        public PropertyAdTypes AdType { get; set; }
        public int AdTypeID { get { return (int)AdType; } }
        
        public float AreaFrom { get; set; }
        public float AreaTo { get; set; }
        public string Category { get; set; }
        public string CityName { get; set; }
        public int Code { get; set; }
        public string CustomerCodename { get; set; }
        public List<string> Districts { get; set; }
        public string PropertyType { get; set; }
        public PropertyAdSearchResultOrders ResultOrdering { get; set; }
        public int RoomsFrom { get; set; }
        public int RoomsTo { get; set; }
        public float PriceTo { get; set; }
        public float PriceFrom { get; set; }
        #endregion

        public PropertyAdSearchParameters()
        {
            AreaTo = float.MaxValue;
            AreaFrom = float.MinValue;
            Category = PropertyAdCategories.Residencial.ToString();
            CityName = "Porto Alegre";
            Code = 0;
            CustomerCodename = string.Empty;
            Districts = new List<string>();
            PropertyType = "Apartamento";
            ResultOrdering = PropertyAdSearchResultOrders._Undefined;
            RoomsFrom = int.MinValue;
            RoomsTo = int.MaxValue;
            PriceTo = float.MaxValue;
            PriceFrom = float.MinValue;
        }
    }
}
