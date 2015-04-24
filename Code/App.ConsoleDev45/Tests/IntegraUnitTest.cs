using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Sync;

namespace TK1.Dev.UnitTest
{
    public class IntegraUnitTest
    {
        public static void BrokerIntegrationTest()
        {
            try
            {
                var sync = new PropertyAdSync("pieta");
                //sync.UpdateClientBizzPropertyAds();
                sync.UpdateClientMdoPropertyAds();
            }
            catch (Exception exception)
            {
            }
        }
    }
}
