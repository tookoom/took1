using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Web.Extension;

public partial class Bizz_BizzApp : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelCustomerName.Text = Page.GetSessionStringValue(WebSessionKeys.BizzUserCustomerName) ?? string.Empty;
        labelUserName.Text = Page.GetSessionStringValue(WebSessionKeys.BizzUserName) ?? string.Empty;
   }

    protected void buttonLogout_Click(object sender, EventArgs e)
    {
        Page.SetSessionValue(WebSessionKeys.BizzUserID, null);
        Page.SetSessionValue(WebSessionKeys.BizzUserCustomerName, null);
        Page.SetSessionValue(WebSessionKeys.BizzUserName, null);
        Response.Redirect("~/User");
    }
    protected void buttonSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bizz/User/Settings");
    }
}
