using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Controller;
using TK1.Bizz;
using TK1.Dev.TK1IntegraService;

namespace TK1.Dev.UnitTest
{
    public class TK1ServicesUnitTest
    {
        public static void Test()
        {
            IntegraClient client = new IntegraClient();

            client.DoWork();

            
        }
    }
}
