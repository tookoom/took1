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
    private static string customerName = CustomerNames.Pandolfo.ToString();

    private static string searchResultSessionKey = "PandolfoSearchResult";
    private static string debugMessage = string.Empty;
    #endregion

    //private SiteAdSearchParameters getSearchParameters()
    //{
    //    var searchParameterSessionKey = getSearchParameterSessionKey();
    //    var webSessionID = string.Empty;
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;
    //    return getSearchParameters(searchParameterSessionKey, webSessionID);
    //}
    //private static SiteAdSearchParameters getSearchParameters(string searchParameterSessionKey, string webSessionID)
    //{
    //    SiteAdSearchParameters searchParameters = null;
    //    if (!string.IsNullOrEmpty(searchParameterSessionKey) & !string.IsNullOrEmpty(webSessionID))
    //    {
    //        var xml = WebSessionController.Get(webSessionID, searchParameterSessionKey);
    //        if (!string.IsNullOrEmpty(xml))
    //        {
    //            searchParameters = XmlSerializer<SiteAdSearchParameters>.Load(xml);
    //        }
    //    }
    //    return searchParameters;
    //}
    //public string getSearchParameterSessionKey()
    //{
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

    //    return string.Format("MdoSellingSearchParameter_{0}", clientAcronym);
    //}
    private List<SiteAdView> getSearchResult(SiteAdSearchParameters searchParameters)
    {
        List<SiteAdView> result = null;
        if (searchParameters != null)
        {
            SiteAdController siteController = new SiteAdController();
            result = siteController.SearchSites(searchParameters);
            setSiteAdMainPic(siteController, result, searchParameters.CustomerCodename);
            setDataBinding(result);
        }
        if (result == null)
            result = new List<SiteAdView>();
        return result;
    }
    //public string getSearchResultSessionKey()
    //{
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

    //    return string.Format("MdoSellingSearchResult_{0}", clientAcronym);
    //}
    protected bool getSearchResultVisibility()
    {
        bool result = true;
        if (Page.Session[searchResultSessionKey] != null)
            result = true;
        return result;
    }
    private string getSiteMainPic(string customerName, int siteAdID)
    {
        string result = string.Empty;
        string baseUrl = string.Format("~\\Integra\\Mdo\\SimVendas\\Fotos\\{0}\\{1}\\", customerName, siteAdID);
        baseUrl = this.ResolveUrl(baseUrl);
        string path = Server.MapPath(baseUrl);
        if (Directory.Exists(path))
        {
            string items = string.Empty;
            foreach (var file in Directory.GetFiles(path, "*.jpg"))
            {
                result = baseUrl + Path.GetFileName(file);
                break;
            }

        }
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
    private string getSiteMainPic(int siteAdTypeID, int siteAdID)
    {
        string result = string.Empty;
        string baseUrl = string.Empty;
        SiteAdController siteController = new SiteAdController();
        switch (siteAdTypeID)
        {
            case (int)SiteAdTypes.Rent:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Arquivos/Inetsoft/Fotos/Pieta/{0}/", siteAdID);
                break;
            case (int)SiteAdTypes.Sell:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/", siteAdID);
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
    //private void loadSearchParameter()
    //{
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    var customerName = CustomerNames.Pandolfo.ToString();

    //    SiteAdController siteController = new SiteAdController();
    //    //radioButtonResidencial.Checked = true;

    //    //var cities = siteController.GetCities(customerName);
    //    //foreach (var city in cities.OrderBy(o => o))
    //    //    dropDownCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

    //    //checkBoxListDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
    //    //var districts = siteController.GetDistricts(customerName);
    //    //foreach (var district in districts.OrderBy(o => o))
    //    //    checkBoxListDistricts.Items.Add(new ListItem(district));

    //    //dropDownSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Comercial.ToString() + "*"));
    //    //var siteComercialTypes = siteController.GetSiteTypes(customerName, (int)SiteAdCategories.Comercial);
    //    //foreach (var siteType in siteComercialTypes.OrderBy(o => o))
    //    //    dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Comercial.ToString() + siteType));

    //    //dropDownSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residencial.ToString() + "*") { Selected = true });
    //    //var siteResidenceTypes = siteController.GetSiteTypes(customerName, (int)SiteAdCategories.Residencial);
    //    //foreach (var siteType in siteResidenceTypes.OrderBy(o => o))
    //    //    dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Residencial.ToString() + siteType));

    //    dropDownRoomNumber.Items.Add(new ListItem("1 dormitório", "1"));
    //    dropDownRoomNumber.Items.Add(new ListItem("1 dormitório ou mais", "1+") { Selected = true });
    //    dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios", "2"));
    //    dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios ou mais", "2+"));
    //    dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios", "3"));
    //    dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios ou mais", "3+"));
    //    dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios", "4"));
    //    dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios ou mais", "4+"));

    //    //dropDownPriceFrom.Items.Add(new ListItem("R$0,00", "0"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$50.000,00", "50000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$100.000,00", "100000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$150.000,00", "150000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$200.000,00", "200000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$250.000,00", "250000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$300.000,00", "300000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$400.000,00", "400000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$500.000,00", "500000"));
    //    //dropDownPriceFrom.Items.Add(new ListItem("R$1.000.000,00", "1000000"));

    //    //dropDownPriceTo.Items.Add(new ListItem("R$100.000,00", "100000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$150.000,00", "150000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$200.000,00", "200000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$250.000,00", "250000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$300.000,00", "300000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$400.000,00", "400000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$500.000,00", "500000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("R$1.000.000,00", "1000000"));
    //    //dropDownPriceTo.Items.Add(new ListItem("Acima de R$1.000.000,00", "1000000+") { Selected = true });
    //}
    private void loadRentSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities(customerName);
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListRentDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts(customerName);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListRentDistricts.Items.Add(new ListItem(district));

        dropDownRentSiteType.Items.Add(new ListItem("Selecione o tipo de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, 1);
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

        var cities = siteController.GetCities(customerName);
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListSellingDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts(customerName);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListSellingDistricts.Items.Add(new ListItem(district));

        dropDownSellingSiteType.Items.Add(new ListItem("Selecione o tipo de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes(customerName, 1);
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
    //private void searchSite()
    //{
    //    SiteAdController siteController = new SiteAdController();

    //    SiteAdSearchParameters searchParameters = new SiteAdSearchParameters();
    //    searchParameters.AdType = SiteAdTypes.Sell;

    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    searchParameters.CustomerCodename = dictionary.Get("ClienteMDO") ?? string.Empty;

    //    if (!string.IsNullOrEmpty(textBoxSiteCode.Text))
    //    {
    //        int siteCode = 0;
    //        if (int.TryParse(textBoxSiteCode.Text, out siteCode))
    //            searchParameters.Code = siteCode;
    //        textBoxSiteCode.Text = string.Empty;
    //    }

    //    //if (dropDownCities.SelectedItem != null)
    //    //    searchParameters.CityName = dropDownCities.SelectedItem.Text;
    //    //if (dropDownPriceFrom.SelectedItem != null)
    //    //    searchParameters.PriceFrom = StringConverter.ToFloat(dropDownPriceFrom.SelectedItem.Value, 0);
    //    //if (dropDownPriceTo.SelectedItem != null)
    //    //    searchParameters.PriceTo = StringConverter.ToFloat(dropDownPriceTo.SelectedItem.Value, float.MaxValue);
    //    if (dropDownRoomNumber.SelectedItem != null)
    //    {
    //        searchParameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MinValue);
    //        if (dropDownRoomNumber.SelectedItem.Value.Contains("+"))
    //            searchParameters.RoomsTo = int.MaxValue;
    //        else
    //            searchParameters.RoomsTo = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MaxValue);
    //    }
    //    //if (dropDownSiteType.SelectedItem != null)
    //    //{
    //    //    string text = dropDownSiteType.SelectedItem.Text;
    //    //    string value = dropDownSiteType.SelectedItem.Value;
    //    //    if (value.Contains(SiteAdCategories.Comercial.ToString()))
    //    //        searchParameters.Category = SiteAdCategories.Comercial.ToString();
    //    //    else
    //    //        searchParameters.Category = SiteAdCategories.Residencial.ToString();
    //    //    if (value.Contains("*"))
    //    //        searchParameters.SiteType = "*";
    //    //    else
    //    //    {
    //    //        string siteType = dropDownSiteType.SelectedItem.Text;
    //    //        if (siteType.Contains("- "))
    //    //            siteType = siteType.Replace("- ", "");
    //    //        searchParameters.SiteType = siteType;
    //    //    }
    //    //}

    //    foreach (ListItem item in checkBoxListRentDistricts.Items)
    //        if (item.Selected)
    //            searchParameters.Districts.Add(item.Text);

    //    var searchResult = siteController.SearchSites(searchParameters);
    //    setSiteAdMainPic(siteController, searchResult, searchParameters.CustomerCodename);
    //    setSearchParameters(searchParameters);
    //    setDataBinding(searchResult);
    //}
    private void searchSite(SiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            SiteAdController siteController = new SiteAdController();
            List<SiteAdView> searchResult = new List<SiteAdView>();
            searchResult = siteController.SearchSites(parameters);
            foreach (var siteAdView in searchResult)
            {
                siteAdView.IsAreaNameVisible = getSiteAreaVisibility(siteAdView.AdCategory);
                siteAdView.IsRoomNameVisible = getSiteRoomNameVisibility(siteAdView.AdCategory);
                if (siteAdView.IsRoomNameVisible)
                    siteAdView.IsRoomNameVisible = !string.IsNullOrEmpty(siteAdView.SiteTypeRoomName);
                string imageUrl = "http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png";
                string mainPic = getSiteMainPic(siteAdView.AdTypeID, siteAdView.Code);
                if (!string.IsNullOrEmpty(mainPic))
                    imageUrl = mainPic;
                siteAdView.MainPicUrl = imageUrl;
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
                parameters.Districts.Add(item.Text);

        searchSite(parameters);
    }
    private void searchSellingSite()
    {
        SiteAdSearchParameters parameters = new SiteAdSearchParameters() { CustomerCodename = customerName };

        parameters.AdType = SiteAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;

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
                parameters.Districts.Add(item.Text);

        searchSite(parameters);
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
    //private void setDebugContent()
    //{
    //    bool debugMode = false;
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    debugMode = dictionary.Get("DebugMode") != null;
    //    //debugMode = true;
    //    if (debugMode)
    //    {
    //        HtmlDiv div = new HtmlDiv();
    //        div.Children.Add(new HtmlHeading(2, "Dados Debug"));
    //        div.Children.Add(new HtmlParagraph(string.Format("Debug Message: {0}", debugMessage)));

    //        div.Children.Add(new HtmlHeading(2, "Dados Query String"));
    //        div.Children.Add(StringDictionary.LoadFromQueryString(queryString).ToHtmlTable());

    //        div.Children.Add(new HtmlHeading(2, "Dados Sessão"));
    //        div.Children.Add(new HtmlParagraph(string.Format("Session ID: {0}", Session.SessionID))); 
            
    //        var searchKey = getSearchResultSessionKey();
    //        div.Children.Add(new HtmlParagraph(string.Format("Session Search Key: {0}", searchKey)));

    //        //List<SiteAd> searchResult = null;
    //        //searchResult = getSearchResult();

    //        //if (searchResult == null)
    //        //{
    //        //    div.Children.Add(new HtmlParagraph(string.Format("Session Search Result = NULL", searchKey)));
    //        //}
    //        //else
    //        //{
    //        //    div.Children.Add(new HtmlParagraph(string.Format("Session Search Result Count: {0}", searchResult.Count)));
    //        //}


    //        literalDebugResult.Text = div.GetHtml();
    //        literalDebugResult.Visible = true;
    //    }
    //    else
    //    {
    //        literalDebugResult.Visible = false;
    //    }
    //}
    //private void setSearchParameters(SiteAdSearchParameters searchParameters)
    //{
    //    var searchParameterSessionKey = getSearchParameterSessionKey();
    //    var webSessionID = string.Empty;
    //    var queryString = Page.ClientQueryString;
    //    var dictionary = StringDictionary.LoadFromQueryString(queryString);
    //    webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;
    //    if(!string.IsNullOrEmpty(webSessionID))
    //        WebSessionController.Set(webSessionID, searchParameterSessionKey, XmlSerializer<SiteAdSearchParameters>.Save(searchParameters ?? new SiteAdSearchParameters()));
    //}
    private void setSiteAdMainPic(SiteAdController siteController, List<SiteAdView> searchResult, string mdoCodename)
    {
        if (siteController != null & searchResult != null)
        {
            foreach (var siteAdView in searchResult)
            {
                string imageUrl = "http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png";
                //if (string.IsNullOrEmpty(siteAdView.MainPicUrl))
                //{
                var customerName = CustomerNames.Pandolfo.ToString();
                string mainPic = getSiteMainPic(customerName, siteAdView.Code);
                    if (!string.IsNullOrEmpty(mainPic))
                        imageUrl = mainPic;
                //}
                siteAdView.MainPicUrl = imageUrl;
            }
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
        //setDebugContent();
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    return;
    //}

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        if (radioButtonBuy.Checked)
            searchSellingSite();
        else
            searchRentSite();
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


    #region OLD
    //protected void linkButtonOrderAreaAsc_Click(object sender, EventArgs e)
    //{
    //    orderSearchResultsByArea(false);
    //}
    //protected void linkButtonOrdeAreaDesc_Click(object sender, EventArgs e)
    //{
    //    orderSearchResultsByArea(true);
    //}
    //protected void linkButtonOrderPriceAsc_Click(object sender, EventArgs e)
    //{
    //    orderSearchResultsByValue(false);
    //}
    //protected void linkButtonOrderPriceDesc_Click(object sender, EventArgs e)
    //{
    //    orderSearchResultsByValue(true);
    //}

    
    #endregion

}