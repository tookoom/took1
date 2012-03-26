using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Mdo.Const;
using TK1.Data.Converter;
using TK1.Bizz.Mdo;
using System.IO;
using TK1.Bizz.Mdo.Data.Controller;
using TK1.Collection;
using TK1.Html;
using TK1.Html.Elements;
using TK1.Data.Controller;
using TK1.Xml;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Data;

public partial class Nav_Mdo_SimVendas_Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    //private static string searchResultSessionKey = "MdoSellingSearchResult";
    private static string debugMessage = string.Empty;
    #endregion

    private MdoSiteAdSearchParameters getSearchParameters()
    {
        var searchParameterSessionKey = getSearchParameterSessionKey();
        var webSessionID = string.Empty;
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;
        return getSearchParameters(searchParameterSessionKey, webSessionID);
    }
    private static MdoSiteAdSearchParameters getSearchParameters(string searchParameterSessionKey, string webSessionID)
    {
        MdoSiteAdSearchParameters searchParameters = null;
        if (!string.IsNullOrEmpty(searchParameterSessionKey) & !string.IsNullOrEmpty(webSessionID))
        {
            var xml = WebSessionController.Get(webSessionID, searchParameterSessionKey);
            if (!string.IsNullOrEmpty(xml))
            {
                searchParameters = XmlSerializer<MdoSiteAdSearchParameters>.Load(xml);
            }
        }
        return searchParameters;
    }
    public string getSearchParameterSessionKey()
    {
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

        return string.Format("MdoSellingSearchParameter_{0}", clientAcronym);
    }
    private List<SiteAdView> getSearchResult()
    {
        List<SiteAdView> result = null;

        var searchResultSessionKey = getSearchResultSessionKey();
        var searchParameterSessionKey = getSearchParameterSessionKey();

        var webSessionID = string.Empty;
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;

        if (string.IsNullOrEmpty(webSessionID))
        {
            result = Page.Session[searchResultSessionKey] as List<SiteAdView>;
        }
        else
        {
            MdoSiteAdSearchParameters searchParameters = getSearchParameters(searchParameterSessionKey, webSessionID);
            if (searchParameters != null)
            {
                MdoSiteAdController siteController = new MdoSiteAdController();
                result = siteController.SearchSites(searchParameters);
                setSiteAdMainPic(siteController, result, searchParameters.CustomerCodename);
                setDataBinding(result);
            }

        }
        if (result == null)
            result = new List<SiteAdView>();
        return result;
    }
    public string getSearchResultSessionKey()
    {
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

        return string.Format("MdoSellingSearchResult_{0}", clientAcronym);
    }
    protected bool getSearchResultVisibility(object parameter)
    {
        bool result = true;
        if (getSearchResult() != null)
            result = true;
        return result;
    }
    private string getSiteMainPic(int mdoCode, int siteAdID)
    {
        string result = string.Empty;
        string baseUrl = string.Format("~\\Integra\\Mdo\\SimVendas\\Fotos\\{0}\\{1}\\", mdoCode, siteAdID);
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
    private void loadSearchParameter()
    {
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

        MdoSiteAdController siteController = new MdoSiteAdController();
        radioButtonResidencial.Checked = true;

        var cities = siteController.GetCities(clientAcronym);
        foreach (var city in cities.OrderBy(o => o))
            dropDownCities.Items.Add(new ListItem(city) { Selected = city == "Porto Alegre" });

        checkBoxListDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts(clientAcronym);
        foreach (var district in districts.OrderBy(o => o))
            checkBoxListDistricts.Items.Add(new ListItem(district));

        dropDownSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Comercial.ToString() + "*"));
        var siteComercialTypes = siteController.GetSiteTypes(SiteAdCategories.Comercial.ToString());
        foreach (var siteType in siteComercialTypes.OrderBy(o => o))
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Comercial.ToString() + siteType));

        dropDownSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residencial.ToString() + "*") { Selected = true });
        var siteResidenceTypes = siteController.GetSiteTypes(SiteAdCategories.Residencial.ToString());
        foreach (var siteType in siteResidenceTypes.OrderBy(o => o))
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Residencial.ToString() + siteType));

        dropDownRoomNumber.Items.Add(new ListItem("1 dormitório", "1"));
        dropDownRoomNumber.Items.Add(new ListItem("1 dormitório ou mais", "1+") { Selected = true });
        dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios", "2"));
        dropDownRoomNumber.Items.Add(new ListItem("2 dormitórios ou mais", "2+"));
        dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios", "3"));
        dropDownRoomNumber.Items.Add(new ListItem("3 dormitórios ou mais", "3+"));
        dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios", "4"));
        dropDownRoomNumber.Items.Add(new ListItem("4 dormitórios ou mais", "4+"));

        dropDownPriceFrom.Items.Add(new ListItem("R$0,00", "0"));
        dropDownPriceFrom.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$150.000,00", "150000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$250.000,00", "250000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$1.000.000,00", "1000000"));

        dropDownPriceTo.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceTo.Items.Add(new ListItem("R$150.000,00", "150000"));
        dropDownPriceTo.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceTo.Items.Add(new ListItem("R$250.000,00", "250000"));
        dropDownPriceTo.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceTo.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceTo.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceTo.Items.Add(new ListItem("R$1.000.000,00", "1000000"));
        dropDownPriceTo.Items.Add(new ListItem("Acima de R$1.000.000,00", "1000000+") { Selected = true });
    }
    private void orderSearchResultsByArea(bool descendingOrder)
    {
        var searchResult = getSearchResult();
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(MdoSiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.AreaDescending));
            else
                setDataBinding(MdoSiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.AreaAscending));
        }
    }
    private void orderSearchResultsByValue(bool descendingOrder)
    {
        var searchResult = getSearchResult();
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(MdoSiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.PriceDescending));
            else
                setDataBinding(MdoSiteAdController.OrderResults(searchResult, SiteAdSearchResultOrders.PriceAscending));
        }
    }
    private void searchSite()
    {
        MdoSiteAdController siteController = new MdoSiteAdController();

        MdoSiteAdSearchParameters searchParameters = new MdoSiteAdSearchParameters();
        searchParameters.AdType = SiteAdTypes.Sell;

        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        searchParameters.CustomerCodename = dictionary.Get("ClienteMDO") ?? string.Empty;

        if (!string.IsNullOrEmpty(textBoxSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSiteCode.Text, out siteCode))
                searchParameters.Code = siteCode;
            textBoxSiteCode.Text = string.Empty;
        }

        if (dropDownCities.SelectedItem != null)
            searchParameters.CityName = dropDownCities.SelectedItem.Text;
        if (dropDownPriceFrom.SelectedItem != null)
            searchParameters.PriceFrom = StringConverter.ToFloat(dropDownPriceFrom.SelectedItem.Value, 0);
        if (dropDownPriceTo.SelectedItem != null)
            searchParameters.PriceTo = StringConverter.ToFloat(dropDownPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownRoomNumber.SelectedItem != null)
        {
            searchParameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownRoomNumber.SelectedItem.Value.Contains("+"))
                searchParameters.RoomsTo = int.MaxValue;
            else
                searchParameters.RoomsTo = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MaxValue);
        }
        if (dropDownSiteType.SelectedItem != null)
        {
            string text = dropDownSiteType.SelectedItem.Text;
            string value = dropDownSiteType.SelectedItem.Value;
            if (value.Contains(SiteAdCategories.Comercial.ToString()))
                searchParameters.Category = SiteAdCategories.Comercial.ToString();
            else
                searchParameters.Category = SiteAdCategories.Residencial.ToString();
            if (value.Contains("*"))
                searchParameters.SiteType = "*";
            else
            {
                string siteType = dropDownSiteType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                searchParameters.SiteType = siteType;
            }
        }

        foreach (ListItem item in checkBoxListDistricts.Items)
            if (item.Selected)
                searchParameters.Districts.Add(item.Text);

        var searchResult = siteController.SearchSites(searchParameters);
        setSiteAdMainPic(siteController, searchResult, searchParameters.CustomerCodename);
        setSearchParameters(searchParameters);
        setDataBinding(searchResult);
    }
    private void setDataBinding(List<SiteAdView> dataToBind)
    {
        var searchSessionKey = getSearchResultSessionKey();

        var webSessionID = string.Empty;
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;

        if (string.IsNullOrEmpty(webSessionID))
        {
            if (Page.Session[searchSessionKey] != null)
                Page.Session.Remove(searchSessionKey);
            Page.Session.Add(searchSessionKey, dataToBind);
        }
        //else
        //{
        //    WebSessionController.Set(webSessionID, searchSessionKey, XmlSerializer<List<SiteAd>>.Save(dataToBind ?? new List<SiteAd>()));
        //}
        listViewSearchResults.DataSourceID = null;
        listViewSearchResults.DataSource = dataToBind;
        listViewSearchResults.DataBind();
        debugMessage = string.Format("Binding NULL data: {0}", (dataToBind == null).ToString());
        setDebugContent();

    }
    private void setDebugContent()
    {
        bool debugMode = false;
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        debugMode = dictionary.Get("DebugMode") != null;
        //debugMode = true;
        if (debugMode)
        {
            HtmlDiv div = new HtmlDiv();
            div.Children.Add(new HtmlHeading(2, "Dados Debug"));
            div.Children.Add(new HtmlParagraph(string.Format("Debug Message: {0}", debugMessage)));

            div.Children.Add(new HtmlHeading(2, "Dados Query String"));
            div.Children.Add(StringDictionary.LoadFromQueryString(queryString).ToHtmlTable());

            div.Children.Add(new HtmlHeading(2, "Dados Sessão"));
            div.Children.Add(new HtmlParagraph(string.Format("Session ID: {0}", Session.SessionID))); 
            
            var searchKey = getSearchResultSessionKey();
            div.Children.Add(new HtmlParagraph(string.Format("Session Search Key: {0}", searchKey)));

            //List<SiteAd> searchResult = null;
            //searchResult = getSearchResult();

            //if (searchResult == null)
            //{
            //    div.Children.Add(new HtmlParagraph(string.Format("Session Search Result = NULL", searchKey)));
            //}
            //else
            //{
            //    div.Children.Add(new HtmlParagraph(string.Format("Session Search Result Count: {0}", searchResult.Count)));
            //}


            literalDebugResult.Text = div.GetHtml();
            literalDebugResult.Visible = true;
        }
        else
        {
            literalDebugResult.Visible = false;
        }
    }
    private void setSearchParameters(MdoSiteAdSearchParameters searchParameters)
    {
        var searchParameterSessionKey = getSearchParameterSessionKey();
        var webSessionID = string.Empty;
        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        webSessionID = dictionary.Get("WebSessionID") ?? string.Empty;
        if(!string.IsNullOrEmpty(webSessionID))
            WebSessionController.Set(webSessionID, searchParameterSessionKey, XmlSerializer<MdoSiteAdSearchParameters>.Save(searchParameters ?? new MdoSiteAdSearchParameters()));
    }
    private void setSiteAdMainPic(MdoSiteAdController siteController, List<SiteAdView> searchResult, string mdoCodename)
    {
        if (siteController != null & searchResult != null)
        {
            foreach (var siteAdView in searchResult)
            {
                string imageUrl = "http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png";
                //if (string.IsNullOrEmpty(siteAdView.MainPicUrl))
                //{
                    var mdoCode = siteController.GetMdoCode(mdoCodename);
                    string mainPic = getSiteMainPic(mdoCode, siteAdView.Code);
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
        loadSearchParameter();
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        setDebugContent();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        searchSite();
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
        var searchParameters = getSearchParameters();
        if (searchParameters != null)
        {
            searchParameters.ResultOrdering = resultOrder;
            setSearchParameters(searchParameters);
        }
    }
    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {

        setDataBinding(getSearchResult());
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