using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz;
using TK1.Data.Bizz.Client.Controller;
using TK1.Data.Bizz.Client.Model;

namespace App.ConsoleDev4.Tests
{
    public class DataTests
    {
        public void ClientAppLogWrite()
        {
            using (TK1ClientBaseEntities entities = new TK1ClientBaseEntities())
            {
                TK1.Data.Bizz.Client.Model.ClientAppLog appLogEntry = new TK1.Data.Bizz.Client.Model.ClientAppLog()
                {
                    LogTimestamp = DateTime.Now,
                    LogType = (int)AppLogLevels.Info,
                    Message = "Unit Test Bizz",
                    Data = DateTime.Now.ToString()
                };
                entities.ClientAppLog.Add(appLogEntry);
                entities.SaveChanges();
            }
        }
        public void ClientPropertyAddSelect()
        {
            var controller = new PropertyAdController("test");
            var ad = controller.Entities.PropertyAd.FirstOrDefault();
        }
    }
}
