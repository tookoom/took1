using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta;
using System.IO;

public partial class Imovel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //objectDataSourceSiteDetail.Select();
        }
    }

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

                string literal = getSitePicGallery(siteAd.AdTypeID, siteAd.SiteAdID);

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

    private string getSitePicGallery(int siteAdType, int siteAdID)
    {
        string result = string.Empty;

        string baseUrl = string.Empty;
        if (siteAdType == 1)
            baseUrl = string.Format("~/Imovel/Fotos/Aluguel/{0}/", siteAdID);
        if (siteAdType == 2)
            baseUrl = string.Format("~/Imovel/Fotos/Venda/{0}/", siteAdID);
        if (!string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = this.ResolveUrl(baseUrl);
            string path = Server.MapPath(baseUrl);
            if(Directory.Exists(path))
            {
                string items = string.Empty;
                foreach (var file in Directory.GetFiles(path,"*.jpg"))
                {
                    string fileName = Path.GetFileName(file);
                    string imageSource = baseUrl + fileName;
                    string li = string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", imageSource);
                    items += li + Environment.NewLine;
                }
                if(!string.IsNullOrEmpty(items))
                {
                    string ul = "<ul id=\"picGallery\">" + Environment.NewLine
                        + "{0}"
                        + "</ul>";
                    result = string.Format(ul,items);
                }

            }
            //result.Add(string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", baseUrl));
        }
        //result.Add("~/Images/PicNotFound.jpg");

        return result;
    }
    private static List<string> getSitePics(int siteAdID, int siteAdType)
    {
        List<string> result = new List<string>();// "~/Images/PicNotFound.jpg";
        string url = string.Empty;
        if (siteAdType == 1)
            url = string.Format("../Imovel/Fotos/Aluguel/{0}/1.jpg", siteAdID);
        if (siteAdType == 2)
            url = string.Format("../Imovel/Fotos/Venda/{0}/1.jpg", siteAdID);
        if (!string.IsNullOrEmpty(url))
            result.Add(string.Format("<li><img src=\"{0}\" title=\"1\" /></li>", url));
        //result.Add("~/Images/PicNotFound.jpg");
        return result;
    }

}