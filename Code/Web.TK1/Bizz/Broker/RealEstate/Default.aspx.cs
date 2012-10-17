using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bizz_Broker_RealEstate_Default : BizzPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUser();
    }
}