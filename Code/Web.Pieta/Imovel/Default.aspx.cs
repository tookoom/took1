﻿using System;
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
        var baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/{0}/", siteAdID);
        var siteAdPics = siteController.GetSitePics(siteAdType, siteAdID);
        string items = string.Empty;
        int index = 0;
        foreach (var item in siteAdPics)
        {
            index++;
            string fileName = item.FileName;
            string imageSource = baseUrl + fileName;// +"resized\\" + fileName;
            string imageThumbSource = baseUrl + fileName;// +"thumbs\\" + fileName;
            string imageTitle = string.Format("Foto {0}", index);
            string imageDescription = item.Description ?? string.Empty;

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

        return result;
    }

}