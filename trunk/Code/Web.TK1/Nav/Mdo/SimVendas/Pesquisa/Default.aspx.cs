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

public partial class Nav_Mdo_SimVendas_Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string searchResultSessionKey = "MdoSellingSearchResult";

    #endregion

    protected bool getSearchResultVisibility(object parameter)
    {
        bool result = true;
        if (Page.Session[searchResultSessionKey] != null)
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
    protected bool getSiteRoomNameVisibility(string categoryID)
    {
        bool result = false;
        switch (categoryID)
        {
            case "1":
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
        MdoSiteController siteController = new MdoSiteController();
        radioButtonResidencial.Checked = true;

        var cities = siteController.GetCities();
        foreach (var city in cities)
            dropDownCities.Items.Add(new ListItem(city));

        checkBoxListDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts();
        foreach (var district in districts)
            checkBoxListDistricts.Items.Add(new ListItem(district));

        dropDownSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Business + "*"));
        var siteComercialTypes = siteController.GetSiteTypes(SiteAdCategories.Business);
        foreach (var siteType in siteComercialTypes)
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Business + siteType));

        dropDownSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residence + "*") { Selected = true });
        var siteResidenceTypes = siteController.GetSiteTypes(SiteAdCategories.Residence);
        foreach (var siteType in siteResidenceTypes)
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Residence + siteType));

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
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAd>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.Site.TotalArea).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.Site.TotalArea).ToList());
        }
    }
    private void orderSearchResultsByValue(bool descendingOrder)
    {
        var searchResult = Page.Session[searchResultSessionKey] as List<SiteAd>;
        if (searchResult != null)
        {
            if (descendingOrder)
                setDataBinding(searchResult.OrderByDescending(o => o.Price).ToList());
            else
                setDataBinding(searchResult.OrderBy(o => o.Price).ToList());
        }
    }
    private void searchSite()
    {
        MdoSiteController siteController = new MdoSiteController();

        MdoSiteSearchParameters searchParameters = new MdoSiteSearchParameters();

        var queryString = Page.ClientQueryString;
        var dictionary = StringDictionary.LoadFromQueryString(queryString);
        searchParameters.MdoAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;

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
            if (value.Contains(SiteAdCategories.Business.ToString()))
                searchParameters.Category = SiteAdCategories.Business;
            else
                searchParameters.Category = SiteAdCategories.Residence;
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
        foreach (var siteAd in searchResult)
        {
            string imageUrl = "http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png";
            if (string.IsNullOrEmpty(siteAd.ImageUrl))
            {
                var mdoCode = siteController.GetMdoCode(siteAd.CustomerID);
                string mainPic = getSiteMainPic(mdoCode, siteAd.SiteAdID);
                if (!string.IsNullOrEmpty(mainPic))
                    imageUrl = mainPic;
            }
            siteAd.ImageUrl = imageUrl;
        }
        setDataBinding(searchResult);
    }
    private void setDataBinding(object dataToBind)
    {
        if (Page.Session[searchResultSessionKey] != null)
            Page.Session.Remove(searchResultSessionKey);
        Page.Session.Add(searchResultSessionKey, dataToBind);

        listViewSearchResults.DataSourceID = null;
        listViewSearchResults.DataSource = dataToBind;
        listViewSearchResults.DataBind();

    }


    #region EVENTS
    protected override void OnInit(EventArgs e)
    {
        loadSearchParameter();
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        searchSite();
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    #endregion
}