using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK1.Net;

namespace TK1.Dev.UnitTest
{
    public class NetUnitTests
    {
        public static void HttpGet()
        {
            HttpClient.GetContent("http://www.kijiji.ca/v-appartement-condo-3-1-2/ville-de-quebec/a/583645307");
            HttpClient.GetContent("http://www.kijiji.ca/v-appartement-condo-3-1-2/ville-de-quebec/a/583645308");
        }
    }
}
