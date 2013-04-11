using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Client.Data.Controller;
using TK1.Bizz.Client.Data;

namespace TK1.Dev.UnitTest
{
    public class BizzClientUnitTest
    {
        public static void AppLog()
        {
            AppLogClientController.WriteAppLogEntry("Testando Bizz Cliente", DateTime.Now.ToString());
        }
        public static void PropertyAds()
        {
            //var list = PropertyAd
        }
    }
}
