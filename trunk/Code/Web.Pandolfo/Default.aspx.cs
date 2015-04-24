using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using TK1.Bizz.Data;
//using TK1.Bizz.Data.Controller;
//using TK1.Bizz.Data.Presentation;
using TK1.Bizz;
using TK1.Data.Bizz.Controller;
using TK1.Data.Bizz.Presentation;

public partial class _Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private string searchParametersSessionKey = "PandolfoSearchParameter";
    private string customerName = CustomerNames.Pandolfo.ToString();

    #endregion

    private void loadRentSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities(customerName, SiteAdTypes.Rent);
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        dropDownRentDistricts.Items.Add(new ListItem("Todos os bairros", "*") { Selected = true });
        var districts = siteController.GetDistricts(customerName, SiteAdTypes.Rent);
        foreach (var district in districts.OrderBy(o => o))
            dropDownRentDistricts.Items.Add(new ListItem(district, district));

        dropDownRentSiteType.Items.Add(new ListItem("Todos os tipos de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, SiteAdTypes.Rent);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownRentSiteType.Items.Add(new ListItem(siteType, siteType));
    }
    private void loadSellingSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities(customerName, SiteAdTypes.Sell);
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        dropDownSellingDistricts.Items.Add(new ListItem("Todos os bairros", "*") { Selected = true });
        var districts = siteController.GetDistricts(customerName, SiteAdTypes.Sell);
        foreach (var district in districts.OrderBy(o => o))
            dropDownSellingDistricts.Items.Add(new ListItem(district, district));

        dropDownSellingSiteType.Items.Add(new ListItem("Todos os tipos de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, SiteAdTypes.Sell);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem(siteType, siteType));

    }

    private void searchRentSite()
    {
        SiteAdSearchParameters parameters = new SiteAdSearchParameters() { CustomerCodename = customerName };

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
            parameters.Districts.Add(dropDownRentDistricts.SelectedItem.Value);

        setSearchSiteParameters(parameters);
    }
    private void searchSellingSite()
    {
        SiteAdSearchParameters parameters = new SiteAdSearchParameters() { CustomerCodename = customerName };

        parameters.AdType = SiteAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;

        if (dropDownSellingCities.SelectedItem != null)
            parameters.CityName = dropDownSellingCities.SelectedItem.Text;
        if (dropDownSellingSiteType.SelectedItem != null)
        {
            string text = dropDownSellingSiteType.SelectedItem.Text;
            string value = dropDownSellingSiteType.SelectedItem.Value;
            //if (value.Contains(SiteAdCategories.Residencial.ToString()))
            //    parameters.Category = SiteAdCategories.Residencial.ToString();
            //else
            //    parameters.Category = SiteAdCategories.Comercial.ToString();
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
            parameters.Districts.Add(dropDownSellingDistricts.SelectedItem.Value);


        setSearchSiteParameters(parameters);
    }
    private void setSearchSiteParameters(SiteAdSearchParameters parameters)
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
    }
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        if (radioButtonBuy.Checked)
            searchSellingSite();
        else
            searchRentSite();
        var searchType = radioButtonBuy.Checked ? "selling" : "rent";
        Response.Redirect("Pesquisa/Default.aspx?searchType=" + searchType);
    }
    public void objectDataSourceFeaturedRentSites_OnSelected(object source, ObjectDataSourceStatusEventArgs e)
    {
        if (e.ReturnValue != null)
        {
            var items = e.ReturnValue as List<SiteAdView>;
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.IsRoomNameVisible = item.AdCategory == SiteAdCategories.Residencial;
                    item.IsAreaNameVisible = item.AdCategory == SiteAdCategories.Comercial;
                }
            }
        }
    }
    #endregion
}
