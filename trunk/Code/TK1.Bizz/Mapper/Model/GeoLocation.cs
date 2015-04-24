using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Mapper.Model
{
    public class GeoLocation
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Name { get; set; }
        public string FormattedName { get; set; }

    }
}
