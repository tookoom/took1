using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Xml;
using TK1.Bizz.Mdo;
using TK1.Bizz.Mdo.Data;
using System.IO;
using TK1.Bizz.Mdo.Const;
using TK1.Utility;
using TK1.Bizz.Mdo.Selling;
using TK1.Bizz.Mdo.Data.Controller;
using TK1.Collection;
using TK1.Data;
using TK1.Bizz.Inetsoft.Rent;

public partial class Integra_Mdo_SimVendas_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var queryString = Page.ClientQueryString;
            var dictionary = StringDictionary.LoadFromQueryString(queryString);

            var mdoAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;
            var loadFileOnly = dictionary.Get("XmlOnly") != null;

            MdoSiteAdController controller = new MdoSiteAdController();
            int mdoCode = controller.GetMdoCode(mdoAcronym);

            string sourceDir = getMdoXmlFilePath();

            string fileFilter = "vendaweb*";

            SellingSiteHelper sellingSiteHelper = new SellingSiteHelper();
            sellingSiteHelper.MdoAcronym = mdoAcronym;
            var report = sellingSiteHelper.LoadXmlSiteAd(sourceDir, fileFilter);
            literalResponse.Text = report;

        }
        catch (Exception exception)
        {
            AppLogController.WriteException("Integra_Mdo_SimVendas_Xml_Carregar_Default.Page_Load", exception);
        }

        try
        {
            var queryString = Page.ClientQueryString;
            var dictionary = StringDictionary.LoadFromQueryString(queryString);

            var mdoAcronym = dictionary.Get("ClienteISoft") ?? string.Empty;
            var loadFileOnly = dictionary.Get("XmlOnly") != null;

            string sourceDir = getISoftXmlFilePath();

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


    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }
    private string getPictureFilesPath(int mdoCode)
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Mdo\\SimVendas\\Fotos\\";

        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl) + string.Format("{0}\\", mdoCode);
        return result;
    }
    private string getMdoXmlFilePath()
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Mdo\\SimVendas\\Xml\\";

        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl);

        return result;
    }

    private string getISoftXmlFilePath()
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Arquivos\\Inetsoft\\Xml\\";
        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl);

        return result;
    }

}