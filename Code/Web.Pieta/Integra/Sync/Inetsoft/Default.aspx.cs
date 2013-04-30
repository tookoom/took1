﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Client.Data.Controller;
using TK1.Bizz.Inetsoft.Client;
using TK1.Bizz.Mdo.Client;

public partial class Integra_Sync_Inetsoft_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sourceDir = getISoftXmlFilePath();
            string fileFilter = "imobiliar*";

            var adHelper = new ClientPropertyRentAdHelper("pieta") { SendReportMail = true };
            var report = adHelper.LoadFile(sourceDir, fileFilter);
            literalResponse.Text = report;


        }
        catch (Exception exception)
        {
            AppLogClientController.WriteException("Integra_Inetsoft_Xml_Carregar_Default.Page_Load", exception);
        }

    }

    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
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