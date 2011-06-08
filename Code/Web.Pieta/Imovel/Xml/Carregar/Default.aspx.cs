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

public partial class Imovel_Xml_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = getXmlFilePath("VisVen.xml");
        loadXml(path);

        //try
        //{
        //    createMessageAlert(this, Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/")), "");
        //    SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/")), 500, 50);
        //    SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Venda/")), 500, 50);
        //    createMessageAlert(this, "Fotos carregadas", "");
        //}
        //catch (Exception exception)
        //{
        //    string message = string.Format("{0}", exception);
        //    createMessageAlert(this, message, "");
        //    throw;
        //}
    }

    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }
    private string createSalesMailBody(XmlLoadLog log, bool success)
    {
        string result = string.Empty;
        string loadResult = success ? "CARGA REALIZADA COM SUCESSO" : "FALHA NA CARGA DE CADASTROS";
        string timestamp = DateTime.Now.ToString();
        string messages = "";

        try
        {

            if (log != null)
            {
                using (PietaEntities entities = new PietaEntities())
                {
                    var attachedLog = entities.XmlLoadLogs.Where(o => o.XmlLoadLogID == log.XmlLoadLogID).FirstOrDefault();
                    if (attachedLog != null)
                    {
                        attachedLog.XmlLoadMessageLogs.Load();
                        foreach (var logMessage in attachedLog.XmlLoadMessageLogs)
                            messages += "<p>" + logMessage.Message + (string.IsNullOrEmpty(logMessage.Data) ? string.Empty : (" - " + logMessage.Data)) + "</p>";
                    }
                }
            }
            string body = HtmlTemplates.GetXmlSalesFileLoadTemplate();
            if (body.Contains(MailTemplateTags.General.Timestamp))
                body = body.Replace(MailTemplateTags.General.Timestamp, timestamp);
            if (body.Contains(MailTemplateTags.General.Result))
                body = body.Replace(MailTemplateTags.General.Result, loadResult);
            if (body.Contains(MailTemplateTags.General.Message))
                body = body.Replace(MailTemplateTags.General.Message, messages);

            result = body;

        }
        catch (Exception exception)
        {
            LogController.WriteException("Imovel_Xml_Carregar_Default.createSalesMailBody", exception, true);
        }
        return result;
    }
    private string getXmlFilePath(string fileName)
    {
        string result = string.Empty;
        string relUrl = "~/Imovel/Xml/";

        relUrl = this.ResolveUrl(relUrl);
        string path = Server.MapPath(relUrl);
        if (Directory.Exists(path))
            result = path + fileName;

        return result;
    }
    private void loadXml(string path)
    {
        XmlLoadLog log = null;
        bool loadResult = false;
        try
        {
            log = LogController.WriteXmlLoadLogEntry();
            LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos", "", LogLevels.Info);

            var sites = XmlSiteHelper.LoadSiteFromFile(path);
            LogController.WriteXmlLoadMessageLogEntry(log, string.Format("{0} imóveis carregados",sites.Count), "", LogLevels.Info);
            if (sites.Count > 0)
            {
                SiteController siteController = new SiteController();
                siteController.AddSalesSiteAds(sites);
                loadResult = true;
            }
            LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Concluído", LogLevels.Info);

        }
        catch (Exception exception)
        {
            if (log != null)
                LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Falha", LogLevels.Info);
            LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
        }
        finally
        {
            sendMail(log, loadResult);
        }
    }
    private void sendMail(XmlLoadLog log, bool success)
    {
        string subject = "Relatório de carga de cadastros IMÓVEIS VENDA PIETÁ";
        string body = createSalesMailBody(log, success);
        MailHelper.SendWebMasterMail(subject, body);
        MailHelper.SendMail(subject, body, MailAdresses.WebMaster, MailAdresses.Vendas);
        string message = success ? "CARGA REALIZADA COM SUCESSO. " : "FALHA NA CARGA DE CADASTROS. ";
        //message += Environment.NewLine;
        message += "Um e-mail foi enviado para vendas@pietaimoveis.com.br com maiores informações.";
        createMessageAlert(this, message, "");
    }


}