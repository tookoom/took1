using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Model.Presentation
{
    public class PersonView
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<EmailAddress> EmailList { get; set; }
        public List<TelephoneNumber> PhoneList { get; set; }

        public PersonView()
        {
            EmailList = new List<EmailAddress>();
            PhoneList = new List<TelephoneNumber>();
        }

    }

    public class EmailAddress
    {
        public string Address { get; set; }
    }
    public class TelephoneNumber
    {
        public string Code { get; set; }
        public string Number { get; set; }
    }
}
