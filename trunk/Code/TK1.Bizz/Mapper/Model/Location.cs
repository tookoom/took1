using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Mapper.Model
{
    public class Location
    {
        #region PUBLIC PROPERTIES
        public string AddressLine { get; set; }
        public string FormattedAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }

        public GeoLocation AdminDistrict { get; set; }
        public GeoLocation AdminSubDistrict { get; set; }
        public GeoLocation Country { get; set; }
        public GeoLocation District { get; set; }
        public GeoLocation Locality { get; set; }

        #endregion

        public Location()
        {
            AdminDistrict = new GeoLocation();
            AdminSubDistrict = new GeoLocation();
            Country = new GeoLocation();
            District = new GeoLocation();
            Locality = new GeoLocation();
        }
    }
}
