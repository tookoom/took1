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

public partial class Imovel_Fotos_Carregar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rentPath = Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/"));
        string salesPath = Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Venda/"));
        string exceptionMessage = string.Empty;
        try
        {
            //var stream = File.CreateText(rentPath + "TESTE.TXT");
            //stream.Write(DateTime.Now.ToString());
            //stream.Close();
            //createMessageAlert(this, Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/")), "");
            SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Aluguel/")), 500, 70);
            SitePicHelper.ResizeSitePics(Server.MapPath(this.ResolveUrl(@"~/Imovel/Fotos/Venda/")), 500, 70);
            //createMessageAlert(this, "Fotos carregadas", "");
        }
        catch (Exception exception)
        {
            exceptionMessage = string.Format("{0}", exception);
        }

        string literal = string.Format("<p>PATH ALUGUEL: {0}</p>", rentPath);
        literal += string.Format("<p>PATH VENDA: {0}</p>", salesPath);
        literal += string.Format("<p>EXCEPTION: {0}</p>", exceptionMessage);

        literalResponse.Text = literal;

    }

    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }

}