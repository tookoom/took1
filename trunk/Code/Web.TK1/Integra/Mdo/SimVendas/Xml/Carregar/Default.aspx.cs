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

public partial class Integra_Mdo_SimVendas_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var queryString = Page.ClientQueryString;
            var dictionary = StringDictionary.LoadFromQueryString(queryString);

            var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;
            var loadFileOnly = dictionary.Get("XmlOnly") != null;

            MdoSiteController controller = new MdoSiteController();
            int mdoCode = controller.GetMdoCode(clientAcronym);

            string sourceDir = getXmlFilePath();
            string picturesPath = getPictureFilesPath(mdoCode);

            string fileFilter = "VisVen*";

            string report = SellingSiteHelper.LoadXmlSiteAd(sourceDir, fileFilter, true);
            literalResponse.Text = report;

            //foreach (var item in FileHelper.GetFiles(sourceDir, fileFilter))
            //{
            //    string report = SellingSiteHelper.LoadXmlSiteAd(item, true);
            //    literalResponse.Text = report;
            //}     

            //if(!loadFileOnly)
            //    SellingSitePicHelper.ResizeSitePics(picturesPath, 500, 70);

            //ImageHelperTestUnit.Resize();
            //

            //if (sourceDirectory == null)
            //    sourceDirectory = string.Empty;
            //string destinationDirectory = @"\Carregados";
            //string errorDirectory = @"\Erro";
            //FileLoader loader = new FileLoader(sourceDirectory, destinationDirectory, errorDirectory);
            //loader.LoadFiles(fileFilter);
            //while (loader.ReadFile())
            //{
            //    SellingSiteHelper.LoadXmlSiteAd(loader.CurrentFile, false);
            //}
        }
        catch (Exception exception)
        {

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
    private string getXmlFilePath()
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Mdo\\SimVendas\\Xml\\";

        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl);

        return result;
    }


}