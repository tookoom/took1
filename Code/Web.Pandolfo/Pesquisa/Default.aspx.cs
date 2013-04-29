using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Data.Converter;
using System.IO;
using TK1.Collection;
using TK1.Html;
using TK1.Html.Elements;
using TK1.Data.Controller;
using TK1.Xml;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Data;
using TK1.Bizz.Data.Controller;
using TK1.Bizz;

public partial class Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private string customerName = CustomerNames.Pandolfo.ToString();
    private string searchParametersSessionKey = "PandolfoSearchParameter";
    private string searchResultSessionKey = "PandolfoSearchResult";
    private string debugMessage = string.Empty;
    #endregion


    //private List<SiteAdView> getSearchResult(SiteAdSearchParameters searchParameters)
    //{
    //    List<SiteAdView> result = null;
    //    if (searchParameters != null)
    //    {
    //        SiteAdController siteController = new SiteAdController();
    //        result = siteController.SearchSites(searchParameters);
    //        setSiteAdMainPic(siteController, result, searchParameters.CustomerCodename);
    //        setDataBinding(result);
    //    }
    //    if (result == null)
    //        result = new List<SiteAdView>();
    //    return result;
    //}
    #region PAGE METHODS
    protected bool getSearchResultVisibility()
    {
        bool result = true;
        if (Page.Session[searchResultSessionKey] != null)
            result = true;
        return result;
    }
    protected bool getSiteRoomNameVisibility(string roomName)
    {
        return !string.IsNullOrEmpty(roomName);
        bool result = false;
        switch (roomName)
        {
            case "Residencial":
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    protected bool getSiteAreaVisibility(SiteAdCategories siteAdCategory)
    {
        return true;
        bool result = false;
        switch (siteAdCategory)
        {
            case SiteAdCategories.Comercial:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    protected bool getSiteRoomNameVisibility(SiteAdCategories siteAdCategory)
    {
        bool result = false;
        switch (siteAdCategory)
        {
            case SiteAdCategories.Residencial:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    #endregion

    //private string getSiteMainPic(string customerName, int siteAdID)
    //{
    //    string result = string.Empty;
    //    string baseUrl = string.Format("~\\Integra\\Mdo\\SimVendas\\Fotos\\{0}\\{1}\\", customerName, siteAdID);
    //    baseUrl = this.ResolveUrl(baseUrl);
    //    string path = Server.MapPath(baseUrl);
    //    if (Directory.Exists(path))
    //    {
    //        string items = string.Empty;
    //        foreach (var file in Directory.GetFiles(path, "*.jpg"))
    //        {
    //            result = baseUrl + Path.GetFileName(file);
    //            break;
    //        }

    //    }
    //    return result;
    //}
    private string getSiteMainPic(int siteAdTypeID, int siteAdID)
    {
        string result = string.Empty;
        string baseUrl = string.Empty;
        SiteAdController siteController = new SiteAdController();
        switch (siteAdTypeID)
        {
            case (int)SiteAdTypes.Rent:
                baseUrl = string.Format(@"http://tk1br.azurewebsites.net/Integra/Arquivos/Inetsoft/Fotos/Pieta/{0}/", siteAdID);
                break;
            case (int)SiteAdTypes.Sell:
                baseUrl = string.Format(@"http://tk1br.azurewebsites.net/Integra/Mdo/SimVendas/Fotos/4/{0}/", siteAdID);
                break;
        }
        //var siteAdPics = siteController.GetSitePics(siteAdTypeID, siteAdID);
        //foreach (var item in siteAdPics)
        //{
        //    result = baseUrl + item.FileName;
        //    break;
        //}
        return result;
    }
    private void loadRentSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities(customerName, SiteAdTypes.Rent);
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city));
        //if (dropDownRentCities.SelectedItem == null)
        //{
        //    foreach (ListItem item in dropDownRentCities.Items)
        //    {
        //        if (item.Text == "Porto Alegre")
        //            item.Selected = true;
        //    }
        //}

        checkBoxListRentDistricts.Items.Add(new ListItem("Todos os bairros", "*") { Selected = true });
        var districts = siteController.GetDistricts(customerName, SiteAdTypes.Rent);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListRentDistricts.Items.Add(new ListItem(district, district));

        dropDownRentSiteType.Items.Add(new ListItem("Todos os tipos de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, SiteAdTypes.Rent);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownRentSiteType.Items.Add(new ListItem(siteType, siteType));

        dropDownRoomNumber.Items.Add(new ListItem("1 dormitório", "1"));
        dropDownRoomNumber.Items.Add(new ListItem("1 dormitório ou mais", "1+") { Selected = true });
        dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios", "2"));
        dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios ou mais", "2+"));
        dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios", "3"));
        dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios ou mais", "3+"));
        dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios", "4"));
        dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios ou mais", "4+"));

        dropDownRentPriceFrom.Items.Add(new ListItem("R$0,00", "0"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$250,00", "250"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$500,00", "500"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$750,00", "750"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownRentPriceFrom.Items.Add(new ListItem("R$10.000,00", "10000"));

        dropDownRentPriceTo.Items.Add(new ListItem("R$250,00", "250"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$500,00", "500"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$750,00", "750"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownRentPriceTo.Items.Add(new ListItem("R$10.000,00", "10000"));
        dropDownRentPriceTo.Items.Add(new ListItem("Acima de R$10.000,00", "1000000+") { Selected = true });
    }
    private void loadSellingSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities(customerName, SiteAdTypes.Sell);
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) );
        //if (dropDownSellingCities.SelectedItem == null)
        //{
        //    foreach (ListItem item in dropDownSellingCities.Items)
        //    {
        //        if (item.Text == "Porto Alegre")
        //            item.Selected = true;
        //    }
        //}

        checkBoxListSellingDistricts.Items.Add(new ListItem("Todos os bairros", "*") { Selected = true });
        var districts = siteController.GetDistricts(customerName, SiteAdTypes.Sell);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListSellingDistricts.Items.Add(new ListItem(district, district));

        dropDownSellingSiteType.Items.Add(new ListItem("Todos os tipos de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, SiteAdTypes.Sell);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem(siteType, siteType));

        dropDownSellingPriceFrom.Items.Add(new ListItem("R$0,00", "0"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$500,00", "500"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$10.000,00", "10000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownSellingPriceFrom.Items.Add(new ListItem("R$1.000.000,00", "1000000"));

        dropDownSellingPriceTo.Items.Add(new ListItem("R$500,00", "500"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$10.000,00", "10000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("R$1.000.000,00", "1000000"));
        dropDownSellingPriceTo.Items.Add(new ListItem("Acima de R$1.000.000,00", "1000000+") { Selected = true });

    }
    private void orderSearchResultsByArea(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(SiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.AreaDescending));
            else
                setDataBinding(SiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.AreaAscending));
        }
    }
    private void orderSearchResultsByValue(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(SiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.PriceDescending));
            else
                setDataBinding(SiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.PriceAscending));
        }
    }
    private void searchSite(SiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            SiteAdController siteController = new SiteAdController();
            List<SiteAdView> searchResult = new List<SiteAdView>();
            searchResult = siteController.SearchSites(parameters);
            foreach (var siteAdView in searchResult)
            {
                siteAdView.IsRoomNameVisible = siteAdView.AdCategory == SiteAdCategories.Residencial;
                siteAdView.IsAreaNameVisible = siteAdView.SiteTotalArea > 0;

                //siteAdView.IsAreaNameVisible = getSiteAreaVisibility(siteAdView.AdCategory);
                //siteAdView.IsRoomNameVisible = getSiteRoomNameVisibility(siteAdView.AdCategory);
                //if (siteAdView.IsRoomNameVisible)
                //    siteAdView.IsRoomNameVisible = !string.IsNullOrEmpty(siteAdView.SiteTypeRoomName);
                //string imageUrl = "http://tk1br.azurewebsites.net/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png";
            }
            setDataBinding(searchResult);
        }
    }
    private void searchRentSite()
    {
        SiteAdSearchParameters parameters = new SiteAdSearchParameters() { CustomerCodename = customerName };

        parameters.AdType = SiteAdTypes.Rent;
        if (!string.IsNullOrEmpty(textBoxSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSiteCode.Text, out siteCode))
                parameters.Code = siteCode;
            textBoxSiteCode.Text = string.Empty;
        }
        if (dropDownRentCities.SelectedItem != null)
            parameters.CityName = dropDownRentCities.SelectedItem.Text;
        if (dropDownRentPriceFrom.SelectedItem != null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownRentPriceFrom.SelectedItem.Value, 0);
        if (dropDownRentPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownRentPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownRoomNumber.SelectedItem != null)
        {
            parameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownRoomNumber.SelectedItem.Value.Contains("+"))
                parameters.RoomsTo = int.MaxValue;
            else
                parameters.RoomsTo = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MaxValue);
        }
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
        foreach (ListItem item in checkBoxListRentDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Value);

        setSearchSiteParameters(parameters);
        //searchSite(parameters);
    }
    private void searchSellingSite()
    {
        SiteAdSearchParameters parameters = new SiteAdSearchParameters() { CustomerCodename = customerName };

        parameters.AdType = SiteAdTypes.Sell;
        if (!string.IsNullOrEmpty(textBoxSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSiteCode.Text, out siteCode))
                parameters.Code = siteCode;
            textBoxSiteCode.Text = string.Empty;
        }
        if (dropDownSellingCities.SelectedItem != null)
            parameters.CityName = dropDownSellingCities.SelectedItem.Text;
        if (dropDownSellingPriceFrom.SelectedItem != null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownSellingPriceFrom.SelectedItem.Value, 0);
        if (dropDownSellingPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownSellingPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownRoomNumber.SelectedItem != null)
        {
            parameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownRoomNumber.SelectedItem.Value.Contains("+"))
                parameters.RoomsTo = int.MaxValue;
            else
                parameters.RoomsTo = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MaxValue);
        }
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

        foreach (ListItem item in checkBoxListSellingDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Value);
        setSearchSiteParameters(parameters);
        //searchSite(parameters);
    }
    private void setDataBinding(List<SiteAdView> dataToBind)
    {
        if (Page.Session[searchResultSessionKey] != null)
            Page.Session.Remove(searchResultSessionKey);
        Page.Session.Add(searchResultSessionKey, dataToBind);

        listViewSearchResults.DataSourceID = null;
        listViewSearchResults.DataSource = dataToBind;
        listViewSearchResults.DataBind();

    }
    private void setRentSearchParameters(SiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            radioButtonRent.Checked = true;

            var item = (from el in dropDownRentCities.Items.Cast<ListItem>()
                        where el.Text == parameters.CityName
                        select el).FirstOrDefault();
            if (item != null)
            {
                dropDownRentCities.SelectedItem.Selected = false;
                item.Selected = true;
            }

            var districtItems = checkBoxListRentDistricts.Items.Cast<ListItem>();
            if (districtItems != null)
            {
                checkBoxListRentDistricts.SelectedItem.Selected = false;
                foreach (var district in parameters.Districts)
                {
                    item = (from el in districtItems
                            where el.Text == district
                            select el).FirstOrDefault();
                    if (item != null)
                        item.Selected = true;
                }
                if (checkBoxListRentDistricts.SelectedItem == null)
                {
                    item = (from el in districtItems
                            where el.Value == "*"
                            select el).FirstOrDefault();
                    if (item != null)
                        item.Selected = true;
                }
            }

            item = (from el in dropDownRentSiteType.Items.Cast<ListItem>()
                    where el.Text == parameters.SiteType
                    select el).FirstOrDefault();
            if (item != null)
            {
                dropDownRentSiteType.SelectedItem.Selected = false;
                item.Selected = true;
            }
        }
    }
    private void setSearchParameters(SiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            if (parameters.AdType == SiteAdTypes.Rent)
                setRentSearchParameters(parameters);
            else
                setSellingSearchParameters(parameters);
        }
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
    private void setSellingSearchParameters(SiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            radioButtonBuy.Checked = true;

            var item = (from el in dropDownSellingCities.Items.Cast<ListItem>()
                        where el.Text == parameters.CityName
                        select el).FirstOrDefault();
            if (item != null)
                item.Selected = true;

            var districtItems = checkBoxListSellingDistricts.Items.Cast<ListItem>();
            if (districtItems != null)
            {
                checkBoxListSellingDistricts.SelectedItem.Selected = false;
                foreach (var district in parameters.Districts)
                {
                    item = (from el in districtItems
                            where el.Text == district
                            select el).FirstOrDefault();
                    if (item != null)
                        item.Selected = true;
                }
                if (checkBoxListSellingDistricts.SelectedItem == null)
                {
                    item = (from el in districtItems
                            where el.Value == "*"
                            select el).FirstOrDefault();
                    if (item != null)
                        item.Selected = true;
                }
            }

            item = (from el in dropDownSellingSiteType.Items.Cast<ListItem>()
                    where el.Text == parameters.SiteType
                    select el).FirstOrDefault();
            if (item != null)
            {
                dropDownSellingSiteType.SelectedItem.Selected = false;
                item.Selected = true;
            }
        }
    }

    #region EVENTS
    protected override void OnInit(EventArgs e)
    {
        radioButtonBuy.Checked = true;
        loadSellingSearchParameter();
        loadRentSearchParameter();

        var parameters = Page.Session[searchParametersSessionKey] as SiteAdSearchParameters;
        Page.Session[searchParametersSessionKey] = null;
        setSearchParameters(parameters);
        searchSite(parameters);

        base.OnPreInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //setDebugContent();
    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        if (radioButtonBuy.Checked)
            searchSellingSite();
        else
            searchRentSite();
        var searchType = radioButtonBuy.Checked ? "selling" : "rent";
        Response.Redirect("Default.aspx?searchType=" + searchType);
    }
    protected void dropDownListResultOrdering_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedValue = dropDownListResultOrdering.SelectedValue ?? string.Empty;
        SiteAdSearchResultOrders resultOrder = SiteAdSearchResultOrders._Undefined;
        switch (selectedValue)
        {
            case "PRICE_ASC":
                resultOrder = SiteAdSearchResultOrders.PriceAscending;
                orderSearchResultsByValue(false);
                break;
            case "PRICE_DESC":
                resultOrder = SiteAdSearchResultOrders.PriceDescending;
                orderSearchResultsByValue(true);
                break;
            case "AREA_ASC":
                resultOrder = SiteAdSearchResultOrders.AreaAscending;
                orderSearchResultsByArea(false);
                break;
            case "AREA_DESC":
                resultOrder = SiteAdSearchResultOrders.AreaDescending;
                orderSearchResultsByArea(true);
                break;
        }
        //var searchParameters = getSearchParameters();
        //if (searchParameters != null)
        //{
        //    searchParameters.ResultOrdering = resultOrder;
        //    setSearchParameters(searchParameters);
        //}
    }
    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {
        setDataBinding(Page.Session[searchResultSessionKey] as List<SiteAdView>);
    }



    #endregion

}