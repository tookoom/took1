using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Bizz.Controller
{
    public class BizzUserController : BizzBaseController
    {
        public BizzUserController()
        {
        }

        public bool CheckUserAccess(int userID, string appName, string customerName, string role)
        {
            bool result = false;
            try
            {
                if (userID > 0)
                {
                    var customerApp = (from o in Entities.CustomerApps
                                       where o.App.Name == appName & o.Customer.Name == customerName
                                       select o).FirstOrDefault();
                    if (customerApp != null)
                    {
                        var userCustomerApp = (from o in Entities.UserCustomerApps
                                               where o.CustomerAppID == customerApp.CustomerAppID & o.AppRole.Name == role
                                               select o).FirstOrDefault();
                        result = userCustomerApp != null;
                    }
                }

            }
            catch (Exception exception)
            {
                
                
            } 
            return result;
        }
        public string GetUserCustomerName(int userID)
        {
            string result = string.Empty;
            var apps = (from o in Entities.UserCustomerApps
                        where o.UserID == userID
                        select o.CustomerApp.Customer.FullName).Distinct().ToList();
            result = apps.FirstOrDefault();
            return result;
        }
    }
}
