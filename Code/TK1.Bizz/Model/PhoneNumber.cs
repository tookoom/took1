using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Model
{
    public class PhoneNumber
    {
        public PhoneNumberTypes  PhoneType { get; set; }
        public string Number { get; set; }

    }

    public enum PhoneNumberTypes { Main }

}
