using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Model
{
    public class Address
    {
        public AddressTypes AddressType { get; set; }
        public string AddressInfo { get; set; }

    }

    public enum AddressTypes { Main }

}
