using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta.Const;
using TK1.Data.Converter;
using TK1.Bizz.Pieta;
using System.IO;

public partial class Pesquisa_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        radioButtonBuy.Checked = true;
        //radioButtonSiteResidence.Checked = true;

        var cities = SiteController.GetCities();
        foreach (var city in cities)
            dropDownCities.Items.Add(new ListItem(city));

        checkBoxListDistricts.Items.Add(new ListItem("Todos", "All") { Selected = true });
        var districts = SiteController.GetDistricts();
        foreach (var district in districts)
            checkBoxListDistricts.Items.Add(new ListItem(district));

        dropDownSiteType.Items.Add(new ListItem("Comercial", SiteAdCategories.Business + "*"));
        var siteComercialTypes = SiteController.GetSiteTypes(SiteAdCategories.Business);
        foreach (var siteType in siteComercialTypes)
            dropDownSiteType.Items.Add(new ListItem("- " + siteType, SiteAdCategories.Business + siteType));

        dropDownSiteType.Items.Add(new ListItem("Residencial", SiteAdCategories.Residence + "*") { Selected = true });
        var siteResidenceTypes = SiteController.GetSiteTypes(SiteAdCategories.Residence);
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

        base.OnPreInit(e);
    }
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        SiteSearchParameters parameters = new SiteSearchParameters();

        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;
        else
            parameters.AdType = SiteAdTypes.Rent;

        //if (radioButtonSiteBusiness.Checked)
        //    parameters.Category = SiteAdCategories.Business;
        //else
        //    parameters.Category = SiteAdCategories.Residence;

        if (dropDownCities.SelectedItem != null)
            parameters.CityName = dropDownCities.SelectedItem.Text;
        if(dropDownPriceFrom.SelectedItem!= null)
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
            if(value.Contains(SiteAdCategories.Business.ToString()))
                parameters.Category = SiteAdCategories.Business;
            else
                parameters.Category = SiteAdCategories.Residence;
            if (value.Contains("*"))
                parameters.SiteType = "*";
            else
            {
                string siteType = dropDownSiteType.SelectedItem.Text;
                if(siteType.Contains("- "))
                    siteType = siteType.Replace("- ","");
                parameters.SiteType = siteType;
            }
        }

        foreach (ListItem item in checkBoxListDistricts.Items)
            if (item.Selected)
                parameters.Districts.Add(item.Text);


        var searchResult = SiteController.SearchSites(parameters);
        foreach (var siteAd in searchResult)
        {
            string imageUrl = "Images/PicNotFound.jpg";
            if (string.IsNullOrEmpty(siteAd.ImageUrl))
            {
                string mainPic = getSiteMainPic(siteAd.AdTypeID, siteAd.SiteAdID);
                if (!string.IsNullOrEmpty(mainPic))
                    imageUrl = mainPic;
            }
            siteAd.ImageUrl = imageUrl;
        }

        listViewSearchResults.DataSourceID = null;
        listViewSearchResults.DataSource = searchResult;
        listViewSearchResults.DataBind();
    }
    private string getSiteMainPic(int siteAdType, int siteAdID)
    {
        string result = string.Empty;

        string baseUrl = string.Empty;
        if (siteAdType == 1)
            baseUrl = string.Format("Imovel/Fotos/Aluguel/{0}/", siteAdID);
        if (siteAdType == 2)
            baseUrl = string.Format("Imovel/Fotos/Venda/{0}/", siteAdID);
        string relUrl = string.Format("~/{0}", baseUrl);
        if (!string.IsNullOrEmpty(baseUrl))
        {
            relUrl = this.ResolveUrl(relUrl);
            string path = Server.MapPath(relUrl);
            if (Directory.Exists(path))
            {
                string items = string.Empty;
                foreach (var file in Directory.GetFiles(path, "*.jpg"))
                {
                    result = baseUrl + Path.GetFileName(file);
                    break;
                }

            }
        }
        return result;
    }

}