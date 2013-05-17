using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Model;

namespace TK1.Bizz.Broker.Presentation
{
    public class BrokerCustomerWebView
    {
        public BrokerCustomer Customer { get; set; }
        public string LogoUrl { get; set; }

        public BrokerCustomerWebView()
        {
            Customer = new BrokerCustomer();
        }


    }

}
