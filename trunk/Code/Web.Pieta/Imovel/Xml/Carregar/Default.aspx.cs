using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Xml;
using TK1.Bizz.Pieta;
using TK1.Bizz.Pieta.Data;
using System.IO;
using TK1.Bizz.Pieta.Const;
using TK1.Utility;
using TK1IntegraService;

public partial class Imovel_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sourceDir = getXmlFileDirectory();
        //string report = SiteHelper.LoadXmlSiteAd(xmlFilePath, false);
        //literalResponse.Text = report;
        string fileFilter = "VisVen*";

        foreach (var item in FileHelper.GetFiles(sourceDir, fileFilter))
        {
            exportFile(item);
            string report = SiteHelper.LoadXmlSiteAd(item, true);
            literalResponse.Text = report;
        }
        //if (!loadFileOnly)
        //    SellingSitePicHelper.ResizeSitePics(picturesPath, 500, 70);


        //try
        //{

        //    loadXml(xmlFilePath);
        //    literal += string.Format("<p>Início do redimensionamento de fotos Aluguel: {0}</p>", DateTime.Now.ToLongTimeString());
        //    SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/")), 500, 70);
        //    literal += string.Format("<p>Início do redimensionamento de fotos Vendas: {0}</p>", DateTime.Now.ToLongTimeString());
        //    SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Venda/")), 500, 70);
        //    literal += string.Format("<p>Limpeza da base de dados: {0}</p>", DateTime.Now.ToLongTimeString());
        //    SiteController.CleanUpDatabase();
        //    literal += string.Format("<p>Término do processo: {0}</p>", DateTime.Now.ToLongTimeString());
        //}
        //catch (Exception exception)
        //{
        //    string exceptionMessage = string.Format("{0}", exception);
        //    createMessageAlert(this, exceptionMessage, "");
        //    if (!string.IsNullOrEmpty(exceptionMessage))
        //        literal += string.Format("<p>EXCEPTION: {0}</p>", exceptionMessage);

        //}
        //finally
        //{
        //    string message = success ? "CARGA REALIZADA COM SUCESSO. " : "FALHA NA CARGA DE CADASTROS. ";
        //    //message += Environment.NewLine;
        //    message += "Um e-mail foi enviado para vendas@pietaimoveis.com.br com maiores informações.";
        //    createMessageAlert(this, message, "");

        //    literalResponse.Text = report;

        //}
    }

    private void exportFile(string filePath)
    {
        try
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                IntegraClient client = new IntegraClient();
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileContent = File.ReadAllText(filePath);
                client.ImportXmlFile("1234", fileName, fileContent);
            }

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
    private string getXmlFileDirectory()
    {
        string result = string.Empty;
        string relUrl = "~\\Imovel\\Xml\\";

        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl);

        return result;

        //string result = string.Empty;
        //string relUrl = "~/Imovel/Xml/";

        //relUrl = this.ResolveUrl(relUrl);
        //string path = Server.MapPath(relUrl);
        //if (Directory.Exists(path))
        //    result = path + fileName;

        //return result;
    }
    //private void loadXml(string path)
    //{
    //    XmlLoadLog log = null;
    //    bool loadResult = false;
    //    try
    //    {
    //        log = LogController.WriteXmlLoadLogEntry();
    //        LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos", "", LogLevels.Info);

    //        var sites = XmlSiteHelper.LoadSiteFromFile(path);
    //        LogController.WriteXmlLoadMessageLogEntry(log, string.Format("{0} imóveis carregados",sites.Count), "", LogLevels.Info);
    //        if (sites.Count > 0)
    //        {
    //            SiteController siteController = new SiteController();
    //            siteController.AddSalesSiteAds(sites);
    //            loadResult = true;
    //        }
    //        LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Concluído", LogLevels.Info);

    //    }
    //    catch (Exception exception)
    //    {
    //        if (log != null)
    //            LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Falha", LogLevels.Info);
    //        LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
    //    }
    //    finally
    //    {
    //        sendMail(log, loadResult);
    //    }
    //}

    /* INTEGRA
     * 
     *     
                var queryString = Page.ClientQueryString;
            var dictionary = StringDictionary.LoadFromQueryString(queryString);

            var clientAcronym = dictionary.Get("ClienteMDO") ?? string.Empty;
            var loadFileOnly = dictionary.Get("XmlOnly") != null;

            MdoSiteController controller = new MdoSiteController();
            int mdoCode = controller.GetMdoCode(clientAcronym);

            string sourceDir = getXmlFilePath();
            string picturesPath = getPictureFilesPath(mdoCode);

            string fileFilter = "VisVen*";

            foreach (var item in FileHelper.GetFiles(sourceDir, fileFilter))
            {
                string report = SellingSiteHelper.LoadXmlSiteAd(item, true);
                literalResponse.Text = report;
            }     
            if(!loadFileOnly)
                SellingSitePicHelper.ResizeSitePics(picturesPath, 500, 70);

     * 
     * 
     * 
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
    */

}