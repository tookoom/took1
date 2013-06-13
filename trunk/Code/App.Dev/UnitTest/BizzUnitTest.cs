using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Controller;
using TK1.Bizz;
using TK1.Bizz.Net;
using TK1.Data.Bizz.Controller;

namespace TK1.Dev.UnitTest
{
    public class BizzUnitTest
    {
        public static void MailTest()
        {
            new MailHelper().SendMail("teste", "teste", "andre.v.mattos@gmail.com;andre@tk1.net.br;andre.v.mattos@live.com;", true);
        }
        public static void UserTest()
        {
            UserController userController = new UserController();
            int userID = userController.GetUserID("andre", "senha");

            BizzUserController bizzUserController = new BizzUserController();
            var validUser = bizzUserController.CheckUserAccess(userID, AppNames.RealEstateBroker.ToString(), CustomerNames.Pandolfo.ToString(), AppRoles.Admin.ToString());
        }
    }
}
