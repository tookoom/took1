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

public partial class Pesquisa_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string searchResultSessionKey = "PietaSearchResult";
    
    #endregion

    protected bool getSearchResultVisibility()
    {
        bool result = true;
        if (Page.Session[searchResultSessionKey] != null)
            result = true;
        return result;
    }
    //private string getSiteMainPic(SiteAdTypes siteAdType, int siteAdID)
    //{
    //    string result = string.Empty;

    //    string baseUrl = string.Empty;
    //    if (siteAdType == SiteAdTypes.Rent)
    //        baseUrl = string.Format("Imovel/Fotos/Aluguel/{0}/", siteAdID);
    //    if (siteAdType == SiteAdTypes.Sell)
    //        baseUrl = string.Format("Imovel/Fotos/Venda/{0}/", siteAdID);
    //    string relUrl = string.Format("~/{0}", baseUrl);
    //    if (!string.IsNullOrEmpty(baseUrl))
    //    {
    //        relUrl = this.ResolveUrl(relUrl);
    //        string path = Server.MapPath(relUrl);
    //        if (Directory.Exists(path))
    //        {
    //            string items = string.Empty;
    //            foreach (var file in Directory.GetFiles(path, "*.jpg"))
    //            {
    //                result = baseUrl + Path.GetFileName(file);
    //                break;
    //            }

    //        }
    //    }
    //    return result;
    //}
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
    private void loadSearchParameter()
    {
        radioButtonBuy.Checked = true;
        //radioButtonSiteResidence.Checked = true;
        MdoSiteAdController siteController = new MdoSiteAdController();

        var cities = siteController.GetCities();
        foreach (var city in cities)
            dropDownCities.Items.Add(new ListItem(city));

        checkBoxListDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = siteController.GetDistricts();
        foreach (var district in districts)
            checkBoxListDistricts.Items.Add(new ListItem(district));

        dropDownSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Comercial.ToString() + "*"));
        var siteComercialTypes = siteController.GetSiteTypes(SiteAdCategories.Comercial.ToString());
        foreach (var siteType in siteComercialTypes)
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Comercial.ToString() + siteType));

        dropDownSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residencial.ToString() + "*") { Selected = true });
        var siteResidenceTypes = siteController.GetSiteTypes(SiteAdCategories.Residencial.ToString());
        foreach (var siteType in siteResidenceTypes)
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
        dropDownPriceFrom.Items.Add(new ListItem("R$500,00", "500"));
        dropDownPriceFrom.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$10.000,00", "5000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$1.000.000,00", "1000000"));

        dropDownPriceTo.Items.Add(new ListItem("R$500,00", "500"));
        dropDownPriceTo.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownPriceTo.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownPriceTo.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownPriceTo.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownPriceTo.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownPriceTo.Items.Add(new ListItem("R$10.000,00", "5000"));
        dropDownPriceTo.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownPriceTo.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceTo.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceTo.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceTo.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceTo.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceTo.Items.Add(new ListItem("R$1.000.000,00", "1000000"));
        dropDownPriceTo.Items.Add(new ListItem("Acima de R$1.000.000,00", "1000000+") { Selected = true });
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
    private void searchSite()
    {
        MdoSiteAdSearchParameters parameters = new MdoSiteAdSearchParameters() { CustomerCodename = "pieta", MdoCode = 4 };

        parameters.AdType = SiteAdTypes.Rent;
        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;

        if (!string.IsNullOrEmpty(textBoxSiteCode.Text))
        {
            int siteCode = 0;
            if (int.TryParse(textBoxSiteCode.Text, out siteCode))
                parameters.Code = siteCode;
            textBoxSiteCode.Text = string.Empty;
        }
        //if (radioButtonSiteBusiness.Checked)
        //    parameters.Category = SiteAdCategories.Business;
        //else
        //    parameters.Category = SiteAdCategories.Residence;

        if (dropDownCities.SelectedItem != null)
            parameters.CityName = dropDownCities.SelectedItem.Text;
        if (dropDownPriceFrom.SelectedItem != null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownPriceFrom.SelectedItem.Value, 0);
        if (dropDownPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownPriceTo.SelectedItem.Value, float.MaxValue);
        if (dropDownRoomNumber.SelectedItem != null)
        {
            parameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MinValue);
            if (dropDownRoomNumber.SelectedItem.Value.Contains("+"))
                parameters.RoomsTo = int.MaxValue;
            else
                parameters.RoomsTo = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value, int.MaxValue);
        }
        if (dropDownSiteType.SelectedItem != null)
        {
            string text = dropDownSiteType.SelectedItem.Text;
            string value = dropDownSiteType.SelectedItem.Value;
            if (value.Contains(SiteAdCategories.Residencial.ToString()))
                parameters.Category = SiteAdCategories.Residencial.ToString();
            else
                parameters.Category = SiteAdCategories.Comercial.ToString();
            if (value.Contains("*"))
                parameters.SiteType = "*";
            else
            {
                string siteType = dropDownSiteType.SelectedItem.Text;
                if (siteType.Contains("- "))
                    siteType = siteType.Replace("- ", "");
                parameters.SiteType = siteType;
            }
        }

        foreach (ListItem item in checkBoxListDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Text);

        PietaSiteAdController siteController = new PietaSiteAdController();
        List<SiteAdView> searchResult = new List<SiteAdView>();
        searchResult = siteController.SearchSites(parameters);
        foreach (var siteAdView in searchResult)
        {
            siteAdView.IsAreaNameVisible = getSiteAreaVisibility(siteAdView.AdCategory);
            siteAdView.IsRoomNameVisible = getSiteRoomNameVisibility(siteAdView.AdCategory);
            if (string.IsNullOrEmpty(siteAdView.MainPicUrl))
                siteAdView.MainPicUrl = "Images/ImageNotFound.png";
            else
                siteAdView.MainPicUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/{1}", siteAdView.Code, siteAdView.MainPicUrl);
        }
        setDataBinding(searchResult);
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
    
    #endregion
}