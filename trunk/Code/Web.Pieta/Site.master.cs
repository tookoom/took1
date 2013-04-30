using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    private void selectActiveMenuItem()
    {
        string url = Request.AppRelativeCurrentExecutionFilePath;
        if (url != null)
        {
            string itemStyle = "menuItem";
            string itemSelectedStyle = "menuItemSelected";


            linkButtonAbout.CssClass = itemStyle;
            linkButtonContact.CssClass = itemStyle;
            linkButtonLocation.CssClass = itemStyle;
            linkButtonHome.CssClass = itemStyle;
            linkButtonSearch.CssClass = itemStyle;
            linkButtonSiteRegistry.CssClass = itemStyle;
            linkButtonUtil.CssClass = itemStyle;

            if(url.Contains("Pesquisa"))
                linkButtonSearch.CssClass = itemSelectedStyle;
            else if (url.Contains("Aguarde"))
                linkButtonSearch.CssClass = itemSelectedStyle;
            else if (url.Contains("Cadastre"))
                linkButtonSiteRegistry.CssClass = itemSelectedStyle;
            else if (url.Contains("Utilidades"))
                linkButtonUtil.CssClass = itemSelectedStyle;
            else if (url.Contains("QuemSomos"))
                linkButtonAbout.CssClass = itemSelectedStyle;
            else if (url.Contains("OndeEstamos"))
                linkButtonLocation.CssClass = itemSelectedStyle;
            else if (url.Contains("FaleConosco"))
                linkButtonContact.CssClass = itemSelectedStyle;
            else
                linkButtonHome.CssClass = itemSelectedStyle;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack & !this.Page.IsCallback)
            selectActiveMenuItem();
    }


    //protected void dxNavigationMain_Load(object sender, EventArgs e)
    //{
    //    if (!this.Page.IsPostBack & !this.Page.IsCallback)
    //    {
    //        //DxMenuHelper.LoadItems(dxMenuMain, SessionManager.GetString(SessionKeys.NCMenuItems));
    //        //selectActiveMenuItem();
    //    }
    //}

    //protected void linkButtonHome_Click(object sender, EventArgs e)
    //{
    //    //linkButtonHome.CssClass = "menuItemSelected";
    //    //linkButtonSearch.CssClass = "menuItem";
    //}
    //protected void linkButtonSearch_Click(object sender, EventArgs e)
    //{
    //    //linkButtonHome.CssClass = "menuItem";
    //    //linkButtonSearch.CssClass = "menuItemSelected";
    //}
}
