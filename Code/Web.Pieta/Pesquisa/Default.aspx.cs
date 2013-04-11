using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Pieta.Const;
using TK1.Data.Converter;
using TK1.Bizz.Pieta;
using System.IO;
using TK1.Bizz.Mdo.Data.Controller;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Pieta.Data.Controller;
using TK1.Bizz.Data;
using TK1.Bizz.Data.Controller;

public partial class Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
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
    private bool getSearchResultAscendingOrder()
    {
        bool result = false;
        var selectedValue = dropDownListResultOrdering.SelectedValue ?? string.Empty;
        if (selectedValue == "DESC")
            result = true;
        return result;
    }
    private string getSiteMainPic(int siteAdTypeID, int siteAdID)
    {
        string result = string.Empty;
        string baseUrl = string.Empty;
        PietaSiteAdController siteController = new PietaSiteAdController();
        switch (siteAdTypeID)
        {
            case (int)SiteAdTypes.Rent:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Arquivos/Inetsoft/Fotos/Pieta/{0}/", siteAdID);
                break;
            case (int)SiteAdTypes.Sell:
                baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/", siteAdID);
                break;
        }
        var siteAdPics = siteController.GetSitePics(siteAdTypeID, siteAdID);
        foreach (var item in siteAdPics)
        {
            result = baseUrl + item.FileName;
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
    private void loadRentSearchParameter()
    {
        SiteAdController siteController = new SiteAdController();

        var cities = siteController.GetCities("pieta");
        foreach (var city in cities.OrderBy(o => o))
            dropDownRentCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListRentDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts("pieta");
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListRentDistricts.Items.Add(new ListItem(district));

        dropDownRentSiteType.Items.Add(new ListItem("Escolha o tipo de imóvel", "*") { Selected = true });
        var siteTypes = siteController.GetSiteTypes("pieta", SiteAdTypes.Rent);
        foreach (var siteType in siteTypes.OrderBy(o => o))
            dropDownRentSiteType.Items.Add(new ListItem(siteType, siteType));

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
        MdoSiteAdController siteController = new MdoSiteAdController();

        var cities = siteController.GetCities("pieta");
        foreach (var city in cities.OrderBy(o => o))
            dropDownSellingCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListSellingDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts("pieta");
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListSellingDistricts.Items.Add(new ListItem(district));

        dropDownSellingSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Comercial.ToString() + "*"));
        var siteComercialTypes = siteController.GetSiteTypes(SiteAdCategories.Comercial.ToString());
        foreach (var siteType in siteComercialTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Comercial.ToString() + siteType));

        dropDownSellingSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residencial.ToString() + "*") { Selected = true });
        var siteResidenceTypes = siteController.GetSiteTypes(SiteAdCategories.Residencial.ToString());
        foreach (var siteType in siteResidenceTypes.OrderBy(o => o))
            dropDownSellingSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Residencial.ToString() + siteType));

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
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.SiteTotalArea).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.SiteTotalArea).ToList());
        }
    }
    private void orderSearchResultsByCode(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAdView>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.Code).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.Code).ToList());
        }
    }
    private void orderSearchResultsByValue(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAdView>;
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
        MdoSiteAdSearchParameters parameters = new MdoSiteAdSearchParameters() { CustomerCodename = "pieta", MdoCode = 4 };

        parameters.AdType = SiteAdTypes.Rent;

        if (!string.IsNullOrEmpty(textBoxRentSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxRentSiteCode.Text, out siteCode))
                parameters.Code = siteCode;
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
        MdoSiteAdSearchParameters parameters = new MdoSiteAdSearchParameters() { CustomerCodename = "pieta", MdoCode = 4 };

        parameters.AdType = SiteAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;

        if (!string.IsNullOrEmpty(textBoxSellingSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSellingSiteCode.Text, out siteCode))
                parameters.Code = siteCode;
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
    private void searchSite(MdoSiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            PietaSiteAdController siteController = new PietaSiteAdController();
            List<SiteAdView> searchResult = new List<SiteAdView>();
            searchResult = siteController.SearchSites(parameters);
            foreach (var siteAdView in searchResult)
            {
                siteAdView.IsAreaNameVisible = getSiteAreaVisibility(siteAdView.AdCategory);
                siteAdView.IsRoomNameVisible = getSiteRoomNameVisibility(siteAdView.AdCategory);
                if (siteAdView.IsRoomNameVisible)
                    siteAdView.IsRoomNameVisible = !string.IsNullOrEmpty(siteAdView.SiteTypeRoomName);
                string imageUrl = "http://www.pietaimoveis.com.br/Images/ImageNotFound.png";
                string mainPic = getSiteMainPic(siteAdView.AdTypeID, siteAdView.Code);
                if (!string.IsNullOrEmpty(mainPic))
                    imageUrl = mainPic;
                siteAdView.MainPicUrl = imageUrl;
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
    private void setSearchParameters(MdoSiteAdSearchParameters parameters)
    {
        if (parameters != null)
        {
            if (parameters.AdType == SiteAdTypes.Rent)
                setRentSearchParameters(parameters);
            else
                setSellingSearchParameters(parameters);
        }
    }
    private void setSellingSearchParameters(MdoSiteAdSearchParameters parameters)
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

            item = (from el in dropDownSellingSiteType.Items.Cast<ListItem>()
                    where el.Text == parameters.SiteType
                    select el).FirstOrDefault();
            if (item != null)
                item.Selected = true;
        }
    }
    private void setRentSearchParameters(MdoSiteAdSearchParameters parameters)
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


    #region EVENTS
    protected override void OnInit(EventArgs e)
    {
        radioButtonBuy.Checked = true;
        loadSellingSearchParameter();
        loadRentSearchParameter();

        //if (Page.Session[searchParametersSessionKey] != null)
        {
            var parameters = Page.Session[searchParametersSessionKey] as MdoSiteAdSearchParameters;
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