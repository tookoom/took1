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
}
