using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Dev.Data;

namespace TK1.Dev.UnitTest
{
    public class MySqlUnitTest
    {
        public static void Test()
        {
            try
            {
                itk1Entities entities = new itk1Entities();
                var list = entities.AppLogs.ToList();

            }
            catch (Exception exception)
            {
            }
        }
    }
}
