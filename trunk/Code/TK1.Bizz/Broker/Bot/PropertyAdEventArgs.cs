using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Presentation;

namespace TK1.Bizz.Broker.Bot
{
    public class PropertyAdEventArgs
    {
        public PropertyAdView Ad { get; set; }
        public string CustomerCode { get; set; }
        public string Source { get; set; }
    }
}
