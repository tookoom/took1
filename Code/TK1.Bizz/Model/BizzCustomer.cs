using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Model
{
    public class BizzCustomer
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public List<Address> AddressList { get; set; }
        public List<EmailAddress> EmailList { get; set; }
        public List<PhoneNumber> PhoneList { get; set; }

        public BizzCustomer()
        {
            AddressList = new List<Address>();
            EmailList = new List<EmailAddress>();
            PhoneList = new List<PhoneNumber>();
        }
    }
}
