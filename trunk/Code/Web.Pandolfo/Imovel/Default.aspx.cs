using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Data.Controller;
using TK1.Bizz;

public partial class Imovel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objectDataSourceSiteDetail.Select();
        }
    }

    protected bool getRentDivVisibility(string adType)
    {
        bool result = false;
        switch (adType)
        {
            case "1":
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    private string getSitePicGallery(int siteAdTypeID, int siteAdID)
    {
        SiteAdController siteController = new SiteAdController();
        var customerName = CustomerNames.Pandolfo.ToString();

        string result = string.Empty;
        string baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Arquivos/Bizz/Broker/RealEstate/pandolfo/{0}/{1}/", siteAdTypeID == 1 ? "Rent" : "Sell", siteAdID);

        var siteAd = siteController.GetSiteAd(customerName, siteAdTypeID, siteAdID);
        var siteAdPics = new List<SiteAdPicView>();
        if (siteAd != null)
        {
            foreach (var item in siteAd.SiteAdPics)
            {
                siteAdPics.Add(new SiteAdPicView()
                {
                    Description = item.Description,
                    FileName = item.FileName,
                    Index = item.PicID
                });
            }
        }
        string items = string.Empty;
        int index = 0;
        foreach (var item in siteAdPics)
        {
            index++;
            string fileName = item.FileName;
            string imageSource = baseUrl + fileName;// +"resized\\" + fileName;
            string imageThumbSource = baseUrl +"thumbs/" + fileName;
            //string imageTitle = string.Format("Foto {0}", index);
            string imageDescription = item.Description ?? string.Empty;

            string li = "<li>"
                    + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageDescription + "\">"
                    + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageDescription + "\" />"
                    + "</a>"
                    + "<div class=\"caption\">"
                    + "<div class=\"image-title\">" + imageDescription + "</div>"
                //+ "<div class=\"image-desc\">" + imageDescription + "</div>"
                    + "</div>"
                    + "</li>";
            items += li + Environment.NewLine;
        }
        if (string.IsNullOrEmpty(items))
        {
            string imageSource = @"http://www.tk1.net.br/Images/ImagemNaoDisponivel.png";
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
        return result;
    }


    #region EVENT HANDLERS
    protected void objectDataSourceSiteDescription_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e != null)
        {
            var siteAdView = e.ReturnValue as SiteAdView;
            if (siteAdView == null)
            {
                divSiteNotFound.Visible = true;
                divSiteDetails.Visible = false;
            }
            else
            {
                divSiteNotFound.Visible = false;
                divSiteDetails.Visible = true;

                string literal = getSitePicGallery(siteAdView.AdTypeID, siteAdView.Code);

                literalSitePics.Text = literal;
            }
        }
    }

    #endregion
}