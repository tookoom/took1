using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.TK1.Mvc.Areas.Map.Models
{
    public class MapItem
    {
        [HiddenInput(DisplayValue = false)]
        public int DonorID { get; set; }
        public string Name { get; set; }
        public string bGroup { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
    public class JsonMapItem
    {
        public int DonorID { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
    }

}