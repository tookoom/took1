using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta;
using System.IO;
using TK1.Bizz.Pieta.Data.Controller;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Client.Controller;

public partial class Imovel_Default : System.Web.UI.Page
{
    #region PRIVATE MEMBERS
    private static string customerCode = "pieta";

    #endregion
    
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
            var propertyAdView = e.ReturnValue as PropertyAdView;
            if (propertyAdView == null)
            {
                divSiteNotFound.Visible = true;
                divSiteDetails.Visible = false;
            }
            else
            {
                divSiteNotFound.Visible = false;
                divSiteDetails.Visible = true;

                string literal = getSitePicGallery(propertyAdView.AdType, propertyAdView.AdCode);

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
    protected bool getRentDivVisibility(PropertyAdTypes adType)
    {
        bool result = false;
        switch (adType)
        {
            case PropertyAdTypes.Rent:
                result = true;
                break;

            default:
                result = false;
                break;
        }
        return result;
    }
    private string getSitePicGallery(PropertyAdTypes adType, int adCode)
    {
        string result = string.Empty;
        string html = string.Empty;
        PropertyAdController customerController = new PropertyAdController(customerCode);
        var pics = customerController.GetPropertyPicViews(adType, adCode);
        int index = 0;
        foreach (var item in pics.OrderBy(o=>o.Index))
        {
            index++;
            string fileName = item.FileName;
            string imageSource = item.Url;
            string imageThumbSource = item.Url;
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
            html += li + Environment.NewLine;
        }
        if (string.IsNullOrEmpty(html))
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
            html += li + Environment.NewLine;
        }
        string ul = "<ul class=\"thumbs noscript\">"
            + "{0}"
            + "</ul>";
        result = string.Format(ul, html);
        return result;

    }

}