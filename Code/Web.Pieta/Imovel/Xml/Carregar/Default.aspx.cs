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

public partial class Imovel_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = getXmlFilePath("VisVen.xml");
        loadXml(path);
    }
    private void loadXml(string path)
    {
        XmlLoadLog log = null;

        try
        {
            log = LogController.WriteXmlLoadLogEntry();
            LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos", "", LogLevels.Info);

            var sites = XmlSiteHelper.LoadSiteFromFile(path);
            if (sites != null)
            {
                SiteController siteController = new SiteController();
                siteController.AddSalesSiteAds(sites);
            }
            LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Sucesso", LogLevels.Info);

        }
        catch (Exception exception)
        {
            if (log != null)
                LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Falha", LogLevels.Info);
            LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
        }
    }

    private string getXmlFilePath(string fileName)
    {
        string result = string.Empty;
        string relUrl = "~/Imovel/Xml/0/";

        relUrl = this.ResolveUrl(relUrl);
        string path = Server.MapPath(relUrl);
        if (Directory.Exists(path))
            result = path + fileName;

        return result;
    }



}