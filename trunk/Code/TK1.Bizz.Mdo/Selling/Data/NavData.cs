using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Mdo.Selling.Data
{
    public class NavData
    {
        public string LogoUrl { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhone { get; set; }

        public NavData()
        {
            LogoUrl = string.Empty;
            ContactMail = string.Empty;
            ContactPhone = string.Empty;
        }
    }
}
