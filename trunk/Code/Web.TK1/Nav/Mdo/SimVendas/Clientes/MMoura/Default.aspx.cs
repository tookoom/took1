using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Collection;

public partial class Nav_Mdo_SimVendas_Clientes_MMoura_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session.IsNewSession)
        //{
            setNavigation("mmoura");
        //}
    }

    private void setNavigation(string customerAcronym)
    {
        var src = navigationFrame.Attributes["src"];
        if (!string.IsNullOrEmpty(src))
        {
            if (!src.Contains(customerAcronym))
            {
                StringDictionary queryString = new StringDictionary();
                queryString.Set("ClienteMDO", customerAcronym);
                queryString.Set("WebSessionID", Session.SessionID);
                //queryString.Set("DebugMode", true.ToString());
                string relUrl = "~\\Nav\\Mdo\\SimVendas\\Pesquisa\\" + queryString.ToQueryString();

                relUrl = this.ResolveUrl(relUrl);
                navigationFrame.Attributes["src"] = relUrl;
            }
        }

    }
}