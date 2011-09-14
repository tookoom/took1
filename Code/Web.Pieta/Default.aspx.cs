using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data.Controller;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //literalSitePics.Text = getSitePicGallery();
        }

    }
    //private string getSitePicGallery()
    //{
    //    string result = string.Empty;

    //    PietaSiteAdController siteController = new PietaSiteAdController();
    //    var siteAdPics = siteController.GetSitePics(siteAdType, siteAdID);


    //    string baseUrl = string.Empty;
    //    if (siteAdType == 1)
    //        baseUrl = string.Format("~/Imovel/Fotos/Aluguel/{0}/", siteAdID);
    //    if (siteAdType == 2)
    //        baseUrl = string.Format("~/Imovel/Fotos/Venda/{0}/", siteAdID);
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
    //                string imageSource = baseUrl + fileName;
    //                string imageThumbSource = baseUrl + fileName;
    //                string imageTitle = string.Format("Foto {0}", index);
    //                string imageDescription = siteAdPics.Where(o => o.FileName == fileName).Select(o => o.Description).FirstOrDefault() ?? string.Empty;
    //                string li = "<li>"
    //                        + "<a class=\"thumb\" name=\"leaf\" href=\"" + imageSource + "\" title=\"" + imageTitle + "\">"
    //                        + "<img src=\"" + imageThumbSource + "\" alt=\"" + imageTitle + "\" />"
    //                        + "</a>"
    //                        + "<div class=\"caption\">"
    //                        + "<div class=\"image-title\">" + imageTitle + "</div>"
    //                        + "<div class=\"image-desc\">" + imageDescription + "</div>"
    //                        + "</div>"
    //                        + "</li>";
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
    //                result = "<img class=\"center\" src=\"http://www.pietaimoveis.com.br/Images/ImageNotFound.png\" title=\"Imagem não disponível\" />";
    //            }

    //        }
    //        else
    //        {
    //            result = "<img class=\"center\" src=\"http://www.pietaimoveis.com.br/Images/ImageNotFound.png\" title=\"Imagem não disponível\" />";
    //        }

    //        //result.Add(string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", baseUrl));
    //    }
    //    //result.Add("~/Images/PicNotFound.jpg");

    //    return result;
    //}

    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {
        //setDataBinding(Page.Session[searchResultSessionKey]);
    }

}
