using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta.Const;
using TK1.Data.Converter;

public partial class Pesquisa_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        radioButtonRent.Checked = true;
        radioButtonSiteResidence.Checked = true;

        var cities = SiteController.GetCities();
        foreach (var city in cities)
            dropDownCities.Items.Add(new ListItem(city));

        checkBoxListDistricts.Items.Add(new ListItem("Todos"));
        var districts = SiteController.GetDistricts();
        foreach (var district in districts)
            checkBoxListDistricts.Items.Add(new ListItem(district));

        var siteTypes = SiteController.GetSiteTypes(SiteAdCategories.Residence);
        foreach(var siteType in siteTypes)
            dropDownSiteType.Items.Add(new ListItem(siteType));

        dropDownRoomNumber.Items.Add(new ListItem("1", "1"));
        dropDownRoomNumber.Items.Add(new ListItem("1 ou mais", "1+"));
        dropDownRoomNumber.Items.Add(new ListItem("2", "2"));
        dropDownRoomNumber.Items.Add(new ListItem("2 ou mais", "2+"));
        dropDownRoomNumber.Items.Add(new ListItem("3", "3"));
        dropDownRoomNumber.Items.Add(new ListItem("3 ou mais", "3+"));
        dropDownRoomNumber.Items.Add(new ListItem("4", "4"));
        dropDownRoomNumber.Items.Add(new ListItem("4 ou mais", "4+"));

        dropDownPriceFrom.Items.Add(new ListItem("R$0,00", "0"));
        dropDownPriceFrom.Items.Add(new ListItem("R$500,00", "500"));
        dropDownPriceFrom.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceFrom.Items.Add(new ListItem("R$1000.000,00", "1000000"));

        dropDownPriceTo.Items.Add(new ListItem("R$500,00", "500"));
        dropDownPriceTo.Items.Add(new ListItem("R$1.000,00", "1000"));
        dropDownPriceTo.Items.Add(new ListItem("R$2.000,00", "2000"));
        dropDownPriceTo.Items.Add(new ListItem("R$3.000,00", "3000"));
        dropDownPriceTo.Items.Add(new ListItem("R$4.000,00", "4000"));
        dropDownPriceTo.Items.Add(new ListItem("R$5.000,00", "5000"));
        dropDownPriceTo.Items.Add(new ListItem("R$50.000,00", "50000"));
        dropDownPriceTo.Items.Add(new ListItem("R$100.000,00", "100000"));
        dropDownPriceTo.Items.Add(new ListItem("R$200.000,00", "200000"));
        dropDownPriceTo.Items.Add(new ListItem("R$300.000,00", "300000"));
        dropDownPriceTo.Items.Add(new ListItem("R$400.000,00", "400000"));
        dropDownPriceTo.Items.Add(new ListItem("R$500.000,00", "500000"));
        dropDownPriceTo.Items.Add(new ListItem("R$1000.000,00", "1000000"));
        dropDownPriceTo.Items.Add(new ListItem("Acima de R$1000.000,00", "1000000+"));

        base.OnPreInit(e);
    }
    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        SiteSearchParameters parameters = new SiteSearchParameters();

        if (radioButtonBuy.Checked)
            parameters.AdType = SiteAdTypes.Sell;
        else
            parameters.AdType = SiteAdTypes.Rent;

        if (radioButtonSiteBusiness.Checked)
            parameters.Category = SiteAdCategories.Business;
        else
            parameters.Category = SiteAdCategories.Residence;

        if (dropDownCities.SelectedItem != null)
            parameters.CityName = dropDownCities.SelectedItem.Text;
        if(dropDownPriceFrom.SelectedItem!= null)
            parameters.PriceFrom = StringConverter.ToFloat(dropDownPriceFrom.SelectedItem.Value, 0);
        if (dropDownPriceTo.SelectedItem != null)
            parameters.PriceTo = StringConverter.ToFloat(dropDownPriceTo.SelectedItem.Value, float.MaxValue);
        if(dropDownRoomNumber.SelectedItem!= null)
            parameters.RoomsFrom = StringConverter.ToInt(dropDownRoomNumber.SelectedItem.Value,int.MaxValue);
        if (dropDownSiteType.SelectedItem != null)
            parameters.SiteType = dropDownSiteType.SelectedItem.Text;

        var searchResult = SiteController.SearchSites(parameters);

        listViewSearchResults.DataSource = searchResult;
        listViewSearchResults.DataBind();
    }
}