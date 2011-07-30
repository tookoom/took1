using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Mdo;
using System.IO;
using TK1.Bizz.Mdo.Data.Controller;

public partial class Nav_Mdo_SimVendas_Imovel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objectDataSourceSiteDetail.Select();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }

    //protected bool getRentDivVisibility(string adType)
    //{
    //    bool result = false;
    //    switch (adType)
    //    {
    //        case "1":
    //            result = true;
    //            break;

    //        default:
    //            result = false;
    //            break;
    //    }
    //    return result;
    //}
    private string getSitePicGallery(int customerID, int siteAdID)
    {
        MdoSiteController siteController = new MdoSiteController();
        var mdoCode = siteController.GetMdoCode(customerID);

        string result = string.Empty;
        string baseUrl = string.Format("~\\Integra\\Mdo\\SimVendas\\Fotos\\{0}\\{1}\\", mdoCode, siteAdID);

        if (!string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = this.ResolveUrl(baseUrl);
            string path = Server.MapPath(baseUrl);
            if (Directory.Exists(path))
            {
                string items = string.Empty;
                int index = 0;
                foreach (var file in Directory.GetFiles(path, "*.jpg"))
                {
                    index++;
                    string fileName = Path.GetFileName(file);
                    string imageSource = baseUrl + "resized\\" + fileName;
                    string imageThumbSource = baseUrl + "thumbs\\" + fileName;
                    string imageTitle = string.Format("Foto {0}", index);
                    string imageDescription = siteController.GetSitePicDescription(fileName) ?? string.Empty;

                    string li = "<li>"
                            + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageTitle + "\">"
                            + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageTitle + "\" />"
                            + "</a>"
                            + "<div class=\"caption\">"
                        //+ "<div class=\"download\">"
                        //+ "<a href=\"" + imageSource + "\">Download Original </a>"
                        //+ "</div>"
                            + "<div class=\"image-title\">" + imageTitle + "</div>"
                            + "<div class=\"image-desc\">" + imageDescription + "</div>"
                            + "</div>"
                            + "</li>";
                    //string li = string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", imageSource);
                    items += li + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(items))
                {
                    string ul = "<ul class=\"thumbs noscript\">"
                        + "{0}"
                        + "</ul>";
                    result = string.Format(ul, items);
                }
                else
                {
                    result = "<img class=\"center\" src=\"http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png\" title=\"Imagem não disponível\" />";
                }

            }
            else
            {
                result = "<img class=\"center\" src=\"http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png\" title=\"Imagem não disponível\" />";
            }

            //result.Add(string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", baseUrl));
        }
        //result.Add("~/Images/PicNotFound.jpg");

        return result;
    }


    #region EVENT HANDLERS
    protected void objectDataSourceSiteDescription_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e != null)
        {
            var siteAd = e.ReturnValue as SiteAd;
            if (siteAd == null)
            {
                divSiteNotFound.Visible = true;
                divSiteDetails.Visible = false;
            }
            else
            {
                divSiteNotFound.Visible = false;
                divSiteDetails.Visible = true;

                string literal = getSitePicGallery(siteAd.CustomerID, siteAd.SiteAdID);

                literalSitePics.Text = literal;
            }
        }
    }

    #endregion
}