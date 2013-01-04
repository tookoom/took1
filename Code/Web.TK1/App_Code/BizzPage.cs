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
    protected bool CheckUser(string userLogin, string userPassword)
    {
        var userController = new UserController();
        var userID = userController.GetUserID(userLogin, userPassword);
        return userID > 0;
    }
    protected bool ValidateUser(string userLogin, string userPassword)
    {
        var userController = new UserController();
        var bizzUserController = new BizzUserController();
        var userID = userController.GetUserID(userLogin, userPassword);
        if (userID > 0)
        {
            Page.SetSessionValue(WebSessionKeys.BizzUserID, userID);
            Page.SetSessionValue(WebSessionKeys.BizzUserLogin, userLogin);
            Page.SetSessionValue(WebSessionKeys.BizzUserCustomerName, bizzUserController.GetUserCustomerName(userID));
            Page.SetSessionValue(WebSessionKeys.BizzUserName, userController.GetUserName(userID));
        }
        else
        {
            Page.SetSessionValue(WebSessionKeys.BizzUserID, 0);
            Page.SetSessionValue(WebSessionKeys.BizzUserLogin, string.Empty);
            Page.SetSessionValue(WebSessionKeys.BizzUserCustomerName, string.Empty);
            Page.SetSessionValue(WebSessionKeys.BizzUserName, string.Empty);
        }
        return userID > 0;
    }
    protected bool ChangeUserPassword(string userLogin, string userPassword, string newPassword, string newPasswordTest)
    {
        bool result = false;
        var userController = new UserController();
        if (!string.IsNullOrEmpty(userLogin) & !string.IsNullOrEmpty(newPassword))
        {
            if (!string.IsNullOrWhiteSpace(userLogin) & !string.IsNullOrWhiteSpace(newPassword))
            {
                if(newPassword == newPasswordTest)
                    result = userController.SetUserPassword(userLogin, userPassword, newPassword);
            }
        }
        return result;
    }

}