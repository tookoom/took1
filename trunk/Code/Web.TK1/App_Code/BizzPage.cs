using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TK1.Web.Extension;
using TK1.Data.Controller;
using TK1.Bizz.Data.Controller;

/// <summary>
/// Summary description for BizzPage
/// </summary>
public class BizzPage : System.Web.UI.Page
{
    protected void CheckUser()
    {
        var userID = Page.GetSessionIntegerValue(WebSessionKeys.BizzUserID);
        if (userID <= 0)
            Response.Redirect("~/User");
    }
    protected bool ValidateUser(string userName, string userPassword)
    {
        var userController = new UserController();
        var bizzUserController = new BizzUserController();
        var userID = userController.GetUserID(userName, userPassword);
        Page.SetSessionValue(WebSessionKeys.BizzUserID, userID);
        Page.SetSessionValue(WebSessionKeys.BizzUserCustomerName, bizzUserController.GetUserCustomerName(userID));
        Page.SetSessionValue(WebSessionKeys.BizzUserName, userController.GetUserName(userID));
        return userID > 0;
    }

}