using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Xml;
using TK1.Bizz.Pieta;
using TK1.Bizz.Pieta.Data;

public partial class Imovel_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadXmlFiles();
    }

    private void loadXmlFiles()
    {
        try
        {
            var log = LogController.WriteXmlLoadLogEntry();
            LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos", "", LogLevels.Info);
            var sites = XmlSiteHelper.LoadSiteFromFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\imoveis.xml");
            if (sites != null)
            {

            }
            var pics = XmlSiteHelper.LoadSitePicFromFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\imoveisfoto.xml");
            if (pics != null)
            {
                SitePicHelper picHelper = new SitePicHelper(@"D:\Projetos\TK1\Projects\Pietá\Integração\TesteImportação");
                foreach (var pic in pics)
                {
                    picHelper.Set(pic.SiteCode, pic.SitePicCode, pic.FileData);
                    LogController.WriteXmlLoadMessageLogEntry(log, "Foto gravada", picHelper.Path + picHelper.FileName, LogLevels.Info);
                }
            }
        }
        catch (Exception exception)
        {
            LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
        }
    }

    //private void loadXmlFiles()
    //{
    //    var sites = XmlSiteHelper.LoadSiteFromFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\imoveis.xml");
    //    var pics = XmlSiteHelper.LoadSitePicFromFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\imoveisfoto.xml");
    //    if (pics != null)
    //    {
    //        SitePicHelper picHelper = new SitePicHelper(@"D:\Projetos\TK1\Projects\Pietá\Integração\TesteImportação");
    //        foreach (var pic in pics)
    //            picHelper.Set(pic.SiteCode, pic.SitePicCode, pic.FileData);
    //    }
    //}

}