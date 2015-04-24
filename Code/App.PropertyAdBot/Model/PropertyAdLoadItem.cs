using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK1.Bizz.Broker.Presentation;

namespace App.PropertyAdBot.Model
{
    public class PropertyAdLoadItem
    {
        public string CustomerCode { get; set; }
        public string Source { get; set; }
        public PropertyAdView Ad { get; set; }

    }
}
