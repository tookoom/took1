using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //objectDataSourceFeaturedSites;
        //objectDataSourceFeaturedSites.DataSourceID = null;
        //objectDataSourceFeaturedSites.DataSource = dataToBind;
        //objectDataSourceFeaturedSites.DataBind();

    }

    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {
        //setDataBinding(Page.Session[searchResultSessionKey]);
    }

    protected void objectDataSourceFeaturedSites_DataBinding(object sender, EventArgs e)
    {
    }
}
