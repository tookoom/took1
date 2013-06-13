using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Const;
using TK1.Data.Converter;
using TK1.Bizz.Pieta;
using System.IO;
using TK1.Bizz.Pieta.Data.Controller;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Client.Controller;

public partial class Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string customerCode = "pieta";
    private static string searchParametersSessionKey = "PietaQuickSearchParameter";
    private static string searchResultSessionKey = "PietaSearchResult";
    
    #endregion

    protected bool getSearchResultVisibility()
    {
        bool result = true;
        if (Page.Session[searchResultSessionKey] != null)
            result = true;
        return result;
    }
    protected bool getSiteAreaVisibility(PropertyAdCategories siteAdCategory)
    {
        return true;
        bool result = false;
        switch (siteAdCategory)
        {
            case PropertyAdCategories.Comercial:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    private bool getSearchResultAscendingOrder()
    {
        bool result = false;
        var selectedValue = dropDownListResultOrdering.SelectedValue ?? string.Empty;
        if (selectedValue == "DESC")
            result = true;
        return result;
    }
    private string getSiteMainPic(PropertyAdTypes adType, int adCode)
    {
        string result = string.Empty;
        string baseUrl = string.Empty;
        PropertyAdController customerController = new PropertyAdController(customerCode);
        switch (adType)
        {
            case PropertyAdTypes.Rent:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Arquivos/Inetsoft/Fotos/Pieta/{0}/", adCode);
                break;
            case PropertyAdTypes.Sell:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/", adCode);
                break;
        }
        var adPics = customerController.GetPropertyPicViews(adType, adCode);
        foreach (var item in adPics)
        {
            result = baseUrl + item.FileName;
            break;
        }
        return result;
    }
    protected bool getSiteRoomNameVisibility(PropertyAdCategories siteAdCategory)
    {
        bool result = false;
        switch (siteAdCategory)
        {
            case PropertyAdCategories.Residencial:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    private void loadRentSearchParameter()
    {
        PropertyAdController propertyAdController = new PropertyAdController(customerCode);

        var cities = propertyAdController.GetCities(PropertyAdTypes.Rent);
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListRentDistricts.Items.Add(new ListItem("Todos", "*") { Selected = true });
        var districts = propertyAdController.GetDistricts(PropertyAdTypes.Rent);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListRentDistricts.Items.Add(new ListItem(district, district));

        dropDownRentPropertyType.Items.Add(new ListItem("Escolha o tipo de imóvel", "*") { Selected = true });
        var propertyTypes = propertyAdController.GetPropertyTypes(PropertyAdTypes.Rent);
        foreach (var siteType in propertyTypes.OrderBy(o => o))
            dropDownRentPropertyType.Items.Add(new ListItem(siteType, siteType));

        dropDownRentRoomNumber.Items.Add(new ListItem("1 dormitório", "1"));
        dropDownRentRoomNumber.Items.Add(new ListItem("1 dormitório ou mais", "1+") { Selected = true });
        dropDownRentRoomNumber.Items.Add(new ListItem("2 dormitórios", "2"));
        dropDownRentRoomNumber.Items.Add(new ListItem("2 dormitórios ou mais", "2+"));
        dropDownRentRoomNumber.Items.Add(new ListItem("3 dormitórios", "3"));
        dropDownRentRoomNumber.Items.Add(new ListItem("3 dormitórios ou mais", "3+"));
        dropDownRentRoomNumber.Items.Add(new ListItem("4 dormitórios", "4"));
        dropDownRentRoomNumber.Items.Add(new ListItem("4 dormitórios ou mais", "4+"));

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
        PropertyAdController propertyAdController = new PropertyAdController(customerCode);

        var cities = propertyAdController.GetCities(PropertyAdTypes.Sell);
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListSellingDistricts.Items.Add(new ListItem("Todos", "*") { Selected = true });
        var districts = propertyAdController.GetDistricts(PropertyAdTypes.Sell);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListSellingDistricts.Items.Add(new ListItem(district, district));

        dropDownSellingPropertyType.Items.Add(new ListItem("Comercial", PropertyAdCategories.Comercial.ToString() + "*"));
        var comercialPropertyTypes = propertyAdController.GetPropertyTypes(PropertyAdTypes.Sell, PropertyAdCategories.Comercial);
        foreach (var siteType in comercialPropertyTypes.OrderBy(o => o))
            dropDownSellingPropertyType.Items.Add(new ListItem("- " + siteType, PropertyAdCategories.Comercial.ToString() + siteType));

        dropDownSellingPropertyType.Items.Add(new ListItem("Residencial", PropertyAdCategories.Residencial.ToString() + "*") { Selected = true });
        var residencialPropertyTypes = propertyAdController.GetPropertyTypes(PropertyAdTypes.Sell, PropertyAdCategories.Residencial);
        foreach (var siteType in residencialPropertyTypes.OrderBy(o => o))
            dropDownSellingPropertyType.Items.Add(new ListItem("- " + siteType, PropertyAdCategories.Residencial.ToString() + siteType));

        dropDownSellingRoomNumber.Items.Add(new ListItem("1 dormitório", "1"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("1 dormitório ou mais", "1+") { Selected = true });
        dropDownSellingRoomNumber.Items.Add(new ListItem("2 dormitórios", "2"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("2 dormitórios ou mais", "2+"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("3 dormitórios", "3"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("3 dormitórios ou mais", "3+"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("4 dormitórios", "4"));
        dropDownSellingRoomNumber.Items.Add(new ListItem("4 dormitórios ou mais", "4+"));

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
    private void orderSearchResults()
    {
        bool descending = getSearchResultAscendingOrder();
        var selectedValue = dropDownListResultOrderingType.SelectedValue ?? string.Empty;
        switch (selectedValue)
        {
            case "CODE":
                orderSearchResultsByCode(descending);
                break;
            case "PRICE":
                orderSearchResultsByValue(descending);
                break;
            case "AREA":
                orderSearchResultsByArea(descending);
                break;
        }
    }
    private void orderSearchResultsByArea(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<PropertyAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.InternalArea).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.InternalArea).ToList());
        }
    }
    private void orderSearchResultsByCode(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<PropertyAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.AdCode).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.AdCode).ToList());
        }
    }
    private void orderSearchResultsByValue(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<PropertyAdView>;
        if (searchResult != null)
        {
            if(descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.Value).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.Value).ToList());
        }
    }
    private void searchRentSite()
    {
        PropertyAdSearchParameters parameters = new PropertyAdSearchParameters() { CustomerCode = customerCode };

        parameters.AdType = PropertyAdTypes.Rent;

        if (!string.IsNullOrEmpty(textBoxRentSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxRentSiteCode.Text, out siteCode))
                parameters.AdCode = siteCode;
            textBoxRentSiteCode.Text = string.Empty;
        }
        if (dropDownRentCities.SelectedItem != null)
            parameters.CityName = dropDownRentCities.SelectedItem.Text;
        if (dropDownRentPriceFrom.SelectedItem != null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownRentPriceFrom.SelectedItem.Value, 0);
        if (dropDownRentPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownRentPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownRentRoomNumber.SelectedItem != null)
        {
            parameters.RoomsFrom = StringConverter.ToInt(dropDownRentRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownRentRoomNumber.SelectedItem.Value.Contains("+"))
                parameters.RoomsTo = int.MaxValue;
            else
                parameters.RoomsTo = StringConverter.ToInt(dropDownRentRoomNumber.SelectedItem.Value, int.MaxValue);
        }
        if (dropDownRentPropertyType.SelectedItem != null)
        {
            string text = dropDownRentPropertyType.SelectedItem.Text;
            string value = dropDownRentPropertyType.SelectedItem.Value;
            //if (value.Contains(PropertyAdCategories.Residencial.ToString()))
            //    parameters.Category = PropertyAdCategories.Residencial.ToString();
            //else
            //    parameters.Category = PropertyAdCategories.Comercial.ToString();
            if (value.Contains("*"))
                parameters.PropertyType = "*";
            else
            {
                string siteType = dropDownRentPropertyType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                parameters.PropertyType = siteType;
            }
        }

        foreach (ListItem item in checkBoxListRentDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Value);

        searchSite(parameters);
    }
    private void searchSellingSite()
    {
        PropertyAdSearchParameters parameters = new PropertyAdSearchParameters() { CustomerCode = customerCode };

        parameters.AdType = PropertyAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = PropertyAdTypes.Sell;

        if (!string.IsNullOrEmpty(textBoxSellingSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSellingSiteCode.Text, out siteCode))
                parameters.AdCode = siteCode;
            textBoxSellingSiteCode.Text = string.Empty;
        }
        if (dropDownSellingCities.SelectedItem != null)
            parameters.CityName = dropDownSellingCities.SelectedItem.Text;
        if (dropDownSellingPriceFrom.SelectedItem != null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownSellingPriceFrom.SelectedItem.Value, 0);
        if (dropDownSellingPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownSellingPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownSellingRoomNumber.SelectedItem != null)
        {
            parameters.RoomsFrom = StringConverter.ToInt(dropDownSellingRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownSellingRoomNumber.SelectedItem.Value.Contains("+"))
                parameters.RoomsTo = int.MaxValue;
            else
                parameters.RoomsTo = StringConverter.ToInt(dropDownSellingRoomNumber.SelectedItem.Value, int.MaxValue);
        }
        if (dropDownSellingPropertyType.SelectedItem != null)
        {
            string text = dropDownSellingPropertyType.SelectedItem.Text;
            string value = dropDownSellingPropertyType.SelectedItem.Value;
            if (value.Contains(PropertyAdCategories.Residencial.ToString()))
                parameters.Category = PropertyAdCategories.Residencial.ToString();
            else
                parameters.Category = PropertyAdCategories.Comercial.ToString();
            if (value.Contains("*"))
                parameters.PropertyType = "*";
            else
            {
                string siteType = dropDownSellingPropertyType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                parameters.PropertyType = siteType;
            }
        }

        foreach (ListItem item in checkBoxListSellingDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Value);

        searchSite(parameters);
    }
    private void searchSite(PropertyAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            PropertyAdController propertyAdController = new PropertyAdController(customerCode);
            List<PropertyAdView> searchResult = new List<PropertyAdView>();
            searchResult = propertyAdController.SearchPropertyAds(parameters);
            foreach (var propertyAdView in searchResult)
            {
                propertyAdView.IsAreaNameVisible = getSiteAreaVisibility(propertyAdView.AdCategory);
                propertyAdView.IsRoomNameVisible = getSiteRoomNameVisibility(propertyAdView.AdCategory);
                if (propertyAdView.IsRoomNameVisible)
                    propertyAdView.IsRoomNameVisible = !string.IsNullOrEmpty(propertyAdView.PropertyTypeRoomName);
                string imageUrl = "http://www.pietaimoveis.com.br/Images/ImageNotFound.png";
                string mainPic = getSiteMainPic(propertyAdView.AdType, propertyAdView.AdCode);
                if (!string.IsNullOrEmpty(mainPic))
                    imageUrl = mainPic;
                propertyAdView.MainPicUrl = imageUrl;
            }
            setDataBinding(searchResult);
        }
    }
    private void setDataBinding(object dataToBind)
    {
        if (Page.Session[searchResultSessionKey] != null)
            Page.Session.Remove(searchResultSessionKey);
        Page.Session.Add(searchResultSessionKey,dataToBind);

        listViewSearchResults.DataSourceID = null;
        listViewSearchResults.DataSource = dataToBind;
        listViewSearchResults.DataBind();
    }
    private void setSearchParameters(PropertyAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            if (parameters.AdType == PropertyAdTypes.Rent)
                setRentSearchParameters(parameters);
            else
                setSellingSearchParameters(parameters);
        }
    }
    private void setSellingSearchParameters(PropertyAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            radioButtonBuy.Checked = true;

            var item = (from el in dropDownSellingCities.Items.Cast<ListItem>()
                        where el.Text == parameters.CityName
                        select el).FirstOrDefault();
            if (item != null)
                item.Selected = true;

            item = (from el in checkBoxListSellingDistricts.Items.Cast<ListItem>()
                    where el.Text == parameters.Districts.FirstOrDefault()
                    select el).FirstOrDefault();
            if (item != null)
                item.Selected = true;

            item = (from el in dropDownSellingPropertyType.Items.Cast<ListItem>()
                    where el.Text == parameters.PropertyType
                    select el).FirstOrDefault();
            if (item != null)
                item.Selected = true;
        }
    }
    private void setRentSearchParameters(PropertyAdSearchParameters parameters)
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

            item = (from el in checkBoxListRentDistricts.Items.Cast<ListItem>()
                    where el.Text == parameters.Districts.FirstOrDefault()
                    select el).FirstOrDefault();
            if (item != null)
            {
                checkBoxListRentDistricts.SelectedItem.Selected = false;
                item.Selected = true;
            }

            item = (from el in dropDownRentPropertyType.Items.Cast<ListItem>()
                    where el.Text == parameters.PropertyType
                    select el).FirstOrDefault();
            if (item != null)
            {
                dropDownRentPropertyType.SelectedItem.Selected = false;
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

        //if (Page.Session[searchParametersSessionKey] != null)
        {
            var parameters = Page.Session[searchParametersSessionKey] as PropertyAdSearchParameters;
            Page.Session[searchParametersSessionKey] = null;
            setSearchParameters(parameters);
            searchSite(parameters);
        }

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
    }
    protected void dropDownListResultOrdering_SelectedIndexChanged(object sender, EventArgs e)
    {
        orderSearchResults();
    }
    protected void dropDownListResultOrderingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        orderSearchResults();
    }
    protected void linkButtonOrderAreaAsc_Click(object sender, EventArgs e)
    {
        orderSearchResultsByArea(false);
    }
    protected void linkButtonOrdeAreaDesc_Click(object sender, EventArgs e)
    {
        orderSearchResultsByArea(true);
    }
    protected void linkButtonOrderPriceAsc_Click(object sender, EventArgs e)
    {
        orderSearchResultsByValue(false);
    }
    protected void linkButtonOrderPriceDesc_Click(object sender, EventArgs e)
    {
        orderSearchResultsByValue(true);
    }
    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {
        setDataBinding(Page.Session[searchResultSessionKey]);
    }
    #endregion
}