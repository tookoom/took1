using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data.Controller;
using System.IO;
using TK1.Data.Bizz.Client.Controller;
using TK1.Bizz.Broker.Presentation;

public partial class _Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string searchParametersSessionKey = "PietaQuickSearchParameter";
    private static string customerCode = "pieta";

    #endregion

    private string getSiteReleaseAdsGallery()
    {
        string result = string.Empty;

        PropertyAdController propertyAdController = new PropertyAdController(customerCode);
        var siteReleaseAdViews = propertyAdController.GetPropertyReleaseAds();

        string items = string.Empty;
        string baseUrl = string.Empty;

        foreach (var siteReleaseAd in siteReleaseAdViews)
        {
            int siteReleaseAdID = siteReleaseAd.AdCode;
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
                                        + "<a href=\"/Imovel/Lancamentos/?ID=" + siteReleaseAd.AdCode.ToString() + "\"><b>Conheça este lançamento!</b></a>"
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
    //private void loadRentSearchParameter()
    //{
    //    var propertyAdController = new PropertyAdController(customerCode);

    //    var cities = propertyAdController.GetCities();
    //    foreach (var city in cities.OrderBy(o => o))
    //        dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

    //    dropDownRentDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
    //    var districts = propertyAdController.GetDistricts();
    //    foreach (var district in districts.OrderBy(o => o))
    //        dropDownRentDistricts.Items.Add(new ListItem(district));

    //    dropDownRentPropertyType.Items.Add(new ListItem("Escolha o tipo de imóvel", "*") { Selected = true });
    //    var siteTypes = propertyAdController.GetPropertyTypes(PropertyAdTypes.Rent);
    //    foreach (var siteType in siteTypes.OrderBy(o => o))
    //        dropDownRentPropertyType.Items.Add(new ListItem(siteType, siteType));
    //}
    //private void loadSellingSearchParameter()
    //{
    //    var propertyAdController = new PropertyAdController(customerCode);

    //    var cities = propertyAdController.GetCities();
    //    foreach (var city in cities.OrderBy(o => o))
    //        dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

    //    dropDownSellingDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
    //    var districts = propertyAdController.GetDistricts();
    //    foreach (var district in districts.OrderBy(o => o))
    //        dropDownSellingDistricts.Items.Add(new ListItem(district));

    //    dropDownSellingPropertyType.Items.Add(new ListItem("Imóvel Comercial", PropertyAdCategories.Comercial.ToString() + "*"));
    //    var siteComercialTypes = propertyAdController.GetPropertyCategories(PropertyAdCategories.Comercial);
    //    foreach (var siteType in siteComercialTypes.OrderBy(o => o))
    //        dropDownSellingPropertyType.Items.Add(new ListItem("- " + siteType, PropertyAdCategories.Comercial.ToString() + siteType));

    //    dropDownSellingPropertyType.Items.Add(new ListItem("Imóvel Residencial", PropertyAdCategories.Residencial.ToString() + "*") { Selected = true });
    //    var siteResidenceTypes = propertyAdController.GetPropertyCategories(PropertyAdCategories.Residencial);
    //    foreach (var siteType in siteResidenceTypes.OrderBy(o => o))
    //        dropDownSellingPropertyType.Items.Add(new ListItem("- " + siteType, PropertyAdCategories.Residencial.ToString() + siteType));

    //}

    //private void searchRentSite()
    //{
    //    PropertyAdSearchParameters parameters = new PropertyAdSearchParameters() { CustomerCodename = customerCode };

    //    parameters.AdType = PropertyAdTypes.Rent;

    //    if (dropDownRentCities.SelectedItem != null)
    //        parameters.CityName = dropDownRentCities.SelectedItem.Text;
    //    if (dropDownRentPropertyType.SelectedItem != null)
    //    {
    //        string text = dropDownRentPropertyType.SelectedItem.Text;
    //        string value = dropDownRentPropertyType.SelectedItem.Value;
    //        //if (value.Contains(PropertyAdCategories.Residencial.ToString()))
    //        //    parameters.Category = PropertyAdCategories.Residencial.ToString();
    //        //else
    //        //    parameters.Category = PropertyAdCategories.Comercial.ToString();
    //        if (value.Contains("*"))
    //            parameters.PropertyType = "*";
    //        else
    //        {
    //            string siteType = dropDownRentPropertyType.SelectedItem.Text;
    //            if (siteType.Contains("- "))
    //                siteType = siteType.Replace("- ", "");
    //            parameters.PropertyType = siteType;
    //        }
    //    }
    //    if (dropDownRentDistricts.SelectedItem != null)
    //        parameters.Districts.Add(dropDownRentDistricts.SelectedItem.Text);

    //    setSearchSiteParameters(parameters);
    //}
    //private void searchSellingSite()
    //{
    //    PropertyAdSearchParameters parameters = new PropertyAdSearchParameters() { CustomerCodename = customerCode };

    //    parameters.AdType = PropertyAdTypes.Rent;
    //    if (radioButtonBuy.Checked)
    //        parameters.AdType = PropertyAdTypes.Sell;

    //    if (dropDownSellingCities.SelectedItem != null)
    //        parameters.CityName = dropDownSellingCities.SelectedItem.Text;
    //    if (dropDownSellingPropertyType.SelectedItem != null)
    //    {
    //        string text = dropDownSellingPropertyType.SelectedItem.Text;
    //        string value = dropDownSellingPropertyType.SelectedItem.Value;
    //        if (value.Contains(PropertyAdCategories.Residencial.ToString()))
    //            parameters.Category = PropertyAdCategories.Residencial.ToString();
    //        else
    //            parameters.Category = PropertyAdCategories.Comercial.ToString();
    //        if (value.Contains("*"))
    //            parameters.PropertyType = "*";
    //        else
    //        {
    //            string siteType = dropDownSellingPropertyType.SelectedItem.Text;
    //            if (siteType.Contains("- "))
    //                siteType = siteType.Replace("- ", "");
    //            parameters.PropertyType = siteType;
    //        }
    //    }

    //    if (dropDownSellingDistricts.SelectedItem != null)
    //        parameters.Districts.Add(dropDownSellingDistricts.SelectedItem.Text);
        

    //    setSearchSiteParameters(parameters);
    //}
    private void setSearchSiteParameters(PropertyAdSearchParameters parameters)
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
        //radioButtonBuy.Checked = true;
        //loadSellingSearchParameter();
        //loadRentSearchParameter();
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
        //if (radioButtonBuy.Checked)
        //    searchSellingSite();
        //else
        //    searchRentSite();
        //var searchType = radioButtonBuy.Checked ? "selling" : "rent";
        //Response.Redirect("Pesquisa/Default.aspx?quickSearch=" + searchType);
    }
    #endregion

}
