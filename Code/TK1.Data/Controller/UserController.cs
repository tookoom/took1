using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Controller
{
    public class UserController: BaseController
    {
        public UserController() 
        {
        }

        public int GetUserID(string userName, string userPassword)
        {
            int result = (from o in Entities.Users
                          where o.Name == userName & o.Password == userPassword
                          select o.UserID).FirstOrDefault();
            return result;
        }
        public string GetUserName(int userID)
        {
            string result = null;
            var user = (from o in Entities.Users
                        where o.UserID == userID
                        select o).FirstOrDefault();
            if (user != null)
                result = user.Person.FullName;
            return result;
        }
        public bool SetUserPassword(string userName, string userPassword, string newPassword)
        {
            bool result = false;
            var user = (from o in Entities.Users
                        where o.Name == userName & o.Password == userPassword
                        select o).FirstOrDefault();
            if (user != null)
            {
                user.Password = newPassword;
                Entities.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
