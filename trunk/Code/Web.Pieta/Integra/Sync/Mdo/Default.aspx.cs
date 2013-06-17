using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Inetsoft.Client;
using TK1.Bizz.Mdo.Client;
using TK1.Collection;
using TK1.Data.Bizz.Client.Controller;

public partial class Integra_Sync_Mdo_Default : System.Web.UI.Page
{
    private static string customerCode = "pieta";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sourceDir = getMdoXmlFilePath();
            string fileFilter = "vendaweb*";

            var adHelper = new ClientPropertyAdHelper("pieta") { SendReportMail = true };
            adHelper.BasePicUrl = @"http://pietaimoveis.com.br/Integra/Arquivos/Mdo/Fotos/4";
            var report = adHelper.LoadFile(sourceDir, fileFilter);
            literalResponse.Text = report;

        }
        catch (Exception exception)
        {
            AppLogClientController.WriteException("Integra_Mdo_SimVendas_Xml_Carregar_Default.Page_Load", exception);
        }

        //try
        //{
        //    string sourceDir = getISoftXmlFilePath();
        //    string fileFilter = "imobiliar*";

        //    var adHelper = new ClientPropertyRentAdHelper("pieta") { SendReportMail = true };
        //    var report = adHelper.LoadFile(sourceDir, fileFilter);
        //    literalResponse.Text = report;


        //}
        //catch (Exception exception)
        //{
        //    AppLogClientController.WriteException("Integra_Inetsoft_Xml_Carregar_Default.Page_Load", exception);
        //}

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
        string relUrl = "~\\Integra\\Arquivos\\Mdo\\Xml\\";

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