using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mapper.Model;

namespace TK1.Bizz.Broker.Presentation
{
    public class PropertyAdBaseView
    {
        //PRIMARY KEY
        public int AdCode { get; set; }
        public PropertyAdTypes AdType { get; set; }
        public string CustomerCode { get; set; }

        public PropertyAdCategories AdCategory { get; set; }

        public Location Location { get; set; }
        public List<PropertyAdDetailView> Details { get; set; }

        //public string Address { get; set; }
        public string AdTypeName { get; set; }
        public string AreaDescription { get; set; }
        //public string City { get; set; }
        public string CondoDescription { get; set; }
        //public string District { get; set; }
        public string FullDescription { get; set; }
        public string MainPicUrl { get; set; }
        public string ShortDescription { get; set; }
        public string PropertyType { get; set; }
        public string PropertyTypeRoomName { get; set; }
        public string Title { get; set; }

        public PropertyAdBaseView()
        {
            Details = new List<PropertyAdDetailView>();
            Location = new Location();
        }
    }
}
