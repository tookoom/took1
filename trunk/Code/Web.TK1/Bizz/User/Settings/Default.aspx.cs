using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Web.Extension;

public partial class Bizz_User_Settings_Default : BizzPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUser();
        literalCustomerName.Text = Page.GetSessionStringValue(WebSessionKeys.BizzUserCustomerName) ?? string.Empty;
        literalUserName.Text = Page.GetSessionStringValue(WebSessionKeys.BizzUserName) ?? string.Empty;

    }


    protected void linkButtonSave_Click(object sender, EventArgs e)
    {
        string userLogin = Page.GetSessionStringValue(WebSessionKeys.BizzUserLogin) ?? string.Empty;
        string userPassword = textBoxUserPassword.Text;

        divLoginFail.Visible = false;
        divPasswordNotMatching.Visible = false;

        bool isValidUser = CheckUser(userLogin, userPassword);
        if (isValidUser)
        {
            string newPassword = textBoxUserNewPassword.Text;
            string newPasswordTest = textBoxUserNewPasswordTest.Text;
            bool isPasswordChanged = ChangeUserPassword(userLogin, userPassword, newPassword, newPasswordTest);
            if (isPasswordChanged)
            {
                Response.Redirect("~/Bizz/Broker/RealEstate");
            }
            else
            {
                divPasswordNotMatching.Visible = true;
            }
        }
        else
        {
            divLoginFail.Visible = true;
        }
    }


    protected void linkButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bizz/Broker/RealEstate");
    }
}