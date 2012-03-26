using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Inetsoft.Rent;
using TK1.Data;
using TK1.Collection;

public partial class Integra_Inetsoft_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var queryString = Page.ClientQueryString;
            var dictionary = StringDictionary.LoadFromQueryString(queryString);

            var mdoAcronym = dictionary.Get("ClienteISoft") ?? string.Empty;
            var loadFileOnly = dictionary.Get("XmlOnly") != null;

            string sourceDir = getXmlFilePath();

            string fileFilter = "imobiliar*";

            RentSiteHelper RentSiteHelper = new RentSiteHelper();
            RentSiteHelper.InetsoftAcronym = "pieta";
            var report = RentSiteHelper.LoadXmlSiteAd(sourceDir, fileFilter);
            literalResponse.Text = report;


        }
        catch (Exception exception)
        {
            AppLogController.WriteException("Integra_Inetsoft_Xml_Carregar_Default.Page_Load", exception);
        }
    }

    private string getXmlFilePath()
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Arquivos\\Inetsoft\\Xml\\";
        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl);

        return result;
    }

}