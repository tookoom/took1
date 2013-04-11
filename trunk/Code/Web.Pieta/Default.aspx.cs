﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data.Controller;
using System.IO;
using TK1.Bizz.Data.Controller;
using TK1.Bizz.Mdo.Data.Controller;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Mdo.Data;

public partial class _Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string searchParametersSessionKey = "PietaQuickSearchParameter";

    #endregion

    private string getSiteReleaseAdsGallery()
    {
        string result = string.Empty;

        PietaSiteAdController siteController = new PietaSiteAdController();
        var siteReleaseAdViews = siteController.GetSiteReleaseAds();

        string items = string.Empty;
        string baseUrl = string.Empty;

        foreach (var siteReleaseAd in siteReleaseAdViews)
        {
            int siteReleaseAdID = siteReleaseAd.Code;
            baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/L{0}/", siteReleaseAdID);
            string imageSource = string.Empty;
            if(string.IsNullOrEmpty(siteReleaseAd.MainPicUrl))
                imageSource = @"http://www.pietaimoveis.com.br/Images/ImageNotFound.png";
            else
                imageSource = baseUrl + siteReleaseAd.MainPicUrl;

            //baseUrl = this.ResolveUrl(baseUrl);
            //string imageSource = string.Empty;
            //string path = Server.MapPath(baseUrl);
            //if (Directory.Exists(path))
            //{
            //    //int index = 0;
            //    //foreach (var file in Directory.GetFiles(path, "*.jpg"))
            //    //{
            //    //    index++;
            //    //    string fileName = Path.GetFileName(file);
            //    //    imageSource = baseUrl + fileName;
            //    //    break;
            //    //}
            //}
            string li = "<li>"
                        + "<table width=100% border=\"0\" cellpadding=\"0\" cellspacing=\"0\">"
                            + "<tr>"
                                + "<td colspan=2 style=\"vertical-align: top;\">"
                                    + "<div class=\"releaseViewerImage\">"
                                        + "<img src=\"" + imageSource + "\" />"
                                    + "</div>"
                                + "</td>"
                                + "<td>"
                                    + "<div class=\"releaseViewerInfo\">"
                                        + "<div class=\"releaseViewerDescription\">"
                                            + "<h3>" + siteReleaseAd.Name + "</h3>"
                                            + "<p>" + siteReleaseAd.ShortDescription + "</p>"
                                            + "<h4>" + siteReleaseAd.AreaText + "</h4>"
                                            + "<h4>" + siteReleaseAd.RoomText + "</h4>"
                                        + "</div>"
                                        + "<div class=\"releaseViewerDetailButton\">"
                                        + "<a href=\"/Imovel/Lancamentos/?ID=" + siteReleaseAd.Code.ToString() + "\"><b>Conheça este lançamento!</b></a>"
                                        + "</div>"
                                    + "</div>"
                                + "</td>"
                            + "</tr>"
                        + "</table>"
                    + "</li>";
            items += li + Environment.NewLine;


        }
        if (string.IsNullOrEmpty(items))
        {
            result = string.Empty;
        }
        else
        {
            string div = "<div  id=\"slider\" class=\"releaseViewer\"> {0} </div>";
            string ul = "<ul>{0}</ul>";
            ul = string.Format(ul, items);
            div = string.Format(div, ul);
            result = div;
        }
        return result;
    }
    private void loadRentSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities("pieta");
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        dropDownRentDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts("pieta");
        foreach (var district in districts.OrderBy(o => o))
            dropDownRentDistricts.Items.Add(new ListItem(district));

        dropDownRentSiteType.Items.Add(new ListItem("Escolha o tipo de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes("pieta", SiteAdTypes.Rent);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownRentSiteType.Items.Add(new ListItem(siteType, siteType));
    }
    private void loadSellingSearchParameter()
    {
        MdoSiteAdController siteController = new MdoSiteAdController();

        var cities = siteController.GetCities("pieta");
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        dropDownSellingDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts("pieta");
        foreach (var district in districts.OrderBy(o => o))
            dropDownSellingDistricts.Items.Add(new ListItem(district));

        dropDownSellingSiteType.Items.Add(new ListItem("Imóvel Comercial", SiteAdCategories.Comercial.ToString() + "*"));
        var siteComercialTypes = siteController.GetSiteTypes(SiteAdCategories.Comercial.ToString());
        foreach (var siteType in siteComercialTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Comercial.ToString() + siteType));

        dropDownSellingSiteType.Items.Add(new ListItem("Imóvel Residencial", SiteAdCategories.Residencial.ToString() + "*") { Selected = true });
        var siteResidenceTypes = siteController.GetSiteTypes(SiteAdCategories.Residencial.ToString());
        foreach (var siteType in siteResidenceTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Residencial.ToString() + siteType));

    }

    private void searchRentSite()
    {
        MdoSiteAdSearchParameters parameters = new MdoSiteAdSearchParameters() { CustomerCodename = "pieta", MdoCode = 4 };

        parameters.AdType = SiteAdTypes.Rent;

        if (dropDownRentCities.SelectedItem != null)
            parameters.CityName = dropDownRentCities.SelectedItem.Text;
        if (dropDownRentSiteType.SelectedItem != null)
        {
            string text = dropDownRentSiteType.SelectedItem.Text;
            string value = dropDownRentSiteType.SelectedItem.Value;
            //if (value.Contains(SiteAdCategories.Residencial.ToString()))
            //    parameters.Category = SiteAdCategories.Residencial.ToString();
            //else
            //    parameters.Category = SiteAdCategories.Comercial.ToString();
            if (value.Contains("*"))
                parameters.SiteType = "*";
            else
            {
                string siteType = dropDownRentSiteType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                parameters.SiteType = siteType;
            }
        }
        if (dropDownRentDistricts.SelectedItem != null)
            parameters.Districts.Add(dropDownRentDistricts.SelectedItem.Text);

        setSearchSiteParameters(parameters);
    }
    private void searchSellingSite()
    {
        MdoSiteAdSearchParameters parameters = new MdoSiteAdSearchParameters() { CustomerCodename = "pieta", MdoCode = 4 };

        parameters.AdType = SiteAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;

        if (dropDownSellingCities.SelectedItem != null)
            parameters.CityName = dropDownSellingCities.SelectedItem.Text;
        if (dropDownSellingSiteType.SelectedItem != null)
        {
            string text = dropDownSellingSiteType.SelectedItem.Text;
            string value = dropDownSellingSiteType.SelectedItem.Value;
            if (value.Contains(SiteAdCategories.Residencial.ToString()))
                parameters.Category = SiteAdCategories.Residencial.ToString();
            else
                parameters.Category = SiteAdCategories.Comercial.ToString();
            if (value.Contains("*"))
                parameters.SiteType = "*";
            else
            {
                string siteType = dropDownSellingSiteType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                parameters.SiteType = siteType;
            }
        }

        if (dropDownSellingDistricts.SelectedItem != null)
            parameters.Districts.Add(dropDownSellingDistricts.SelectedItem.Text);
        

        setSearchSiteParameters(parameters);
    }
    private void setSearchSiteParameters(MdoSiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            if (Page.Session[searchParametersSessionKey] != null)
                Page.Session.Remove(searchParametersSessionKey);
            Page.Session.Add(searchParametersSessionKey, parameters);

        }
    }

    #region EVENTS
    protected override void OnInit(EventArgs e)
    {
        radioButtonBuy.Checked = true;
        loadSellingSearchParameter();
        loadRentSearchParameter();
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            literalSiteReleaseAds.Text = getSiteReleaseAdsGallery();
        }
    }
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        if (radioButtonBuy.Checked)
            searchSellingSite();
        else
            searchRentSite();
        var searchType = radioButtonBuy.Checked ? "selling" : "rent";
        Response.Redirect("Pesquisa/Default.aspx?quickSearch=" + searchType);
    }
    #endregion

}
