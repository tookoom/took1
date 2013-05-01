using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Client.Data.Presentation;

namespace TK1.Bizz.Client.Data.Integration
{
    public class PropertyAdCollection
    {
        public List<PropertyAdView> PropertyAds { get; set; }
        public List<PropertyAdDetailView> PropertyAdDetails { get; set; }
        public List<PropertyAdPicView> PropertyAdPics { get; set; }
    }
}
