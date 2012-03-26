using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Data.Presentation
{
    public class SiteReleaseAdView : SiteAdView
    {
        public string Name { get; set; }

        public string AreaText { get; set; }
        public string RoomText { get; set; }
        public string ValueText { get; set; }


        public int SiteMinTotalRooms { get; set; }
        public float SiteMinInternalArea { get; set; }

        public float SiteMinTotalArea { get; set; }
        public float MinValue { get; set; }

    }
}
