using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Data.Controller;
using TK1.Web.Extension;

public partial class User_Default : BizzPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //var userID = Page.GetSessionIntegerValue(WebSessionKeys.BizzUserID);
        //divLoginFail.Visible = userID > 0;
    }

    #region EVENT HANDLERS
    protected void linkButtonLogin_Click(object sender, EventArgs e)
    {
        //forceLogin();
        string userName = textBoxUserName.Text;
        string userPassword = textBoxUserPassword.Text;
        bool isValidUser = ValidateUser(userName, userPassword);
        if (isValidUser)
        {
            Response.Redirect("~/Bizz/Broker/RealEstate");
        }
        else
        {
            divLoginFail.Visible = true;
        }
    }
    private void forceLogin()
    {
        string userName = "andre";
        string userPassword = "";
        bool isValidUser = ValidateUser(userName, userPassword);
        if (isValidUser)
        {
            Response.Redirect("~/Bizz/Broker/RealEstate");
        }
        else
        {
            divLoginFail.Visible = true;
        }
    }

    #endregion
}