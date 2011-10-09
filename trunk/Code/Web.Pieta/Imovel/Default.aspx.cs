using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Pieta;
using System.IO;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Pieta.Data.Controller;

public partial class Imovel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objectDataSourceSiteDetail.Select();
        }
    }

    protected void objectDataSourceSiteDescription_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e != null)
        {
            var siteAd = e.ReturnValue as SiteAdView;
            if (siteAd == null)
            {
                divSiteNotFound.Visible = true;
                divSiteDetails.Visible = false;
            }
            else
            {
                divSiteNotFound.Visible = false;
                divSiteDetails.Visible = true;

                string literal = getSitePicGallery(siteAd.AdTypeID, siteAd.Code);

                //literal = "<ul id=\"picGallery\">"
                //    + "<li><img src=\"../Imovel/Fotos/Aluguel/5/1.jpg\" title=\"1\" /></li>"
                //    + "<li><img src=\"../Imovel/Fotos/Aluguel/5/2.jpg\" title=\"1\" /></li>"
                //    + "<li><img src=\"../Imovel/Fotos/Aluguel/5/3.jpg\" title=\"1\" /></li>"
                //    + "<li><img src=\"../Imovel/Fotos/Aluguel/5/4.jpg\" title=\"1\" /></li>"
                //    + "</ul>";

                literalSitePics.Text = literal;
            }
        }
    }
    protected bool getRentDivVisibility(SiteAdTypes siteAdType)
    {
        bool result = false;
        switch (siteAdType)
        {
            case SiteAdTypes.Rent:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    private string getSitePicGallery(int siteAdType, int siteAdID)
    {
        string result = string.Empty;

        PietaSiteAdController siteController = new PietaSiteAdController();
        var siteAdPics = siteController.GetSitePics(siteAdType, siteAdID);


        string baseUrl = string.Empty;
        if (siteAdType == 1)
            baseUrl = string.Format("~/Imovel/Fotos/Aluguel/{0}/", siteAdID);
        if (siteAdType == 2)
            baseUrl = string.Format("~/Imovel/Fotos/Venda/{0}/", siteAdID);
        if (!string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = this.ResolveUrl(baseUrl);
            string path = Server.MapPath(baseUrl);
            string items = string.Empty;
            if (Directory.Exists(path))
            {
                int index = 0;
                foreach (var file in Directory.GetFiles(path, "*.jpg"))
                {
                    index++;
                    string fileName = Path.GetFileName(file);
                    string imageSource = baseUrl  + fileName;
                    string imageThumbSource = baseUrl + fileName;
                    string imageTitle = string.Format("Foto {0}", index);
                    string imageDescription = siteAdPics.Where(o => o.FileName == fileName).Select(o=>o.Description).FirstOrDefault() ?? string.Empty; 
                    string li = "<li>"
                            + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageTitle + "\">"
                            + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageTitle + "\" />"
                            + "</a>"
                            + "<div class=\"caption\">"
                            //+ "<div class=\"image-title\">" + imageTitle + "</div>"
                            + "<div class=\"image-title\">" + imageDescription + "</div>"
                            + "</div>"
                            + "</li>";
                    items += li + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(items))
            {
                string imageSource = @"http://www.pietaimoveis.com.br/Images/ImageNotFound.png";
                string li = "<li>"
                    + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + "" + "\">"
                    + "<img src=\"" + imageSource + "\" alt=\"" + "" + "\" />"
                    + "</a>"
                    + "<div class=\"caption\">"
                    + "<div class=\"image-title\">" + "Imagem não disponível" + "</div>"
                    + "<div class=\"image-desc\">" + "Aguarde atualização do cadastro"
                    //+ "<p visible=\"false\">" + baseUrl + "</p>"
                    //+ "<p visible=\"false\">" + path + "</p>"
                    + "</div>"
                    + "</div>"
                    + "</li>";
                items += li + Environment.NewLine;
            }
            string ul = "<ul class=\"thumbs noscript\">"
                + "{0}"
                + "</ul>";
            result = string.Format(ul, items);

        }
        return result;
    }

    //private string getSitePicGallery_WEB(int siteAdType, int siteAdID)
    //{
    //    string result = string.Empty;
    //    PietaSiteAdController siteController = new PietaSiteAdController();
    //    var baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/", siteAdID);
    //    var siteAdPics = siteController.GetSitePics(siteAdType, siteAdID);
    //    string items = string.Empty;
    //    int index = 0;
    //    foreach (var item in siteAdPics)
    //    {
    //        index++;
    //        string fileName = item.FileName;
    //        string imageSource = baseUrl + fileName;// +"resized\\" + fileName;
    //        string imageThumbSource = baseUrl + fileName;// +"thumbs\\" + fileName;
    //        string imageTitle = string.Format("Foto {0}", index);
    //        string imageDescription = item.Description ?? string.Empty;

    //        string li = "<li>"
    //                + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageTitle + "\">"
    //                + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageTitle + "\" />"
    //                + "</a>"
    //                + "<div class=\"caption\">"
    //            //+ "<div class=\"download\">"
    //            //+ "<a href=\"" + imageSource + "\">Download Original </a>"
    //            //+ "</div>"
    //                + "<div class=\"image-title\">" + imageTitle + "</div>"
    //                + "<div class=\"image-desc\">" + imageDescription + "</div>"
    //                + "</div>"
    //                + "</li>";
    //        //string li = string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", imageSource);
    //        items += li + Environment.NewLine;
    //    }
    //    if (!string.IsNullOrEmpty(items))
    //    {
    //        string ul = "<ul class=\"thumbs noscript\">"
    //            + "{0}"
    //            + "</ul>";
    //        result = string.Format(ul, items);
    //    }
    //    else
    //    {
    //        result = "<img class=\"center\" src=\"http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png\" title=\"Imagem não disponível\" />";
    //    }

    //    return result;
    //}
    //private string getSitePicGallery_MDO(int siteAdType, int siteAdID)
    //{
    //    PietaSiteAdController siteController = new PietaSiteAdController();
    //    var siteAdPics = siteController.GetSitePics(siteAdType, siteAdID);

    //    string result = string.Empty;
    //    string baseUrl = string.Format("~\\Integra\\Mdo\\SimVendas\\Fotos\\{0}\\{1}\\", mdoCode, siteAdID);

    //    if (!string.IsNullOrEmpty(baseUrl))
    //    {
    //        baseUrl = this.ResolveUrl(baseUrl);
    //        string path = Server.MapPath(baseUrl);
    //        if (Directory.Exists(path))
    //        {
    //            string items = string.Empty;
    //            int index = 0;
    //            foreach (var file in Directory.GetFiles(path, "*.jpg"))
    //            {
    //                index++;
    //                string fileName = Path.GetFileName(file);
    //                string imageSource = baseUrl + fileName;// +"resized\\" + fileName;
    //                string imageThumbSource = baseUrl + fileName;// +"thumbs\\" + fileName;
    //                string imageTitle = string.Format("Foto {0}", index);
    //                string imageDescription = siteController.GetSitePicDescription(fileName) ?? string.Empty;

    //                string li = "<li>"
    //                        + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageTitle + "\">"
    //                        + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageTitle + "\" />"
    //                        + "</a>"
    //                        + "<div class=\"caption\">"
    //                    //+ "<div class=\"download\">"
    //                    //+ "<a href=\"" + imageSource + "\">Download Original </a>"
    //                    //+ "</div>"
    //                        + "<div class=\"image-title\">" + imageTitle + "</div>"
    //                        + "<div class=\"image-desc\">" + imageDescription + "</div>"
    //                        + "</div>"
    //                        + "</li>";
    //                //string li = string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", imageSource);
    //                items += li + Environment.NewLine;
    //            }
    //            if (!string.IsNullOrEmpty(items))
    //            {
    //                string ul = "<ul class=\"thumbs noscript\">"
    //                    + "{0}"
    //                    + "</ul>";
    //                result = string.Format(ul, items);
    //            }
    //            else
    //            {
    //                result = "<img class=\"center\" src=\"http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png\" title=\"Imagem não disponível\" />";
    //            }

    //        }
    //        else
    //        {
    //            result = "<img class=\"center\" src=\"http://www.tk1.net.br/Nav/Mdo/SimVendas/Imagens/ImagemNaoDisponivel.png\" title=\"Imagem não disponível\" />";
    //        }

    //        //result.Add(string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", baseUrl));
    //    }
    //    //result.Add("~/Images/PicNotFound.jpg");

    //    return result;
    //}

}