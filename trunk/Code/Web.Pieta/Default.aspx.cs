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
            literalSiteReleaseAds.Text = getSiteReleaseAdsGallery();
        }

    }
    private string getSiteReleaseAdsGallery()
    {
        string result = string.Empty;

        PietaSiteAdController siteController = new PietaSiteAdController();
        var siteReleaseAdViews = siteController.GetSiteReleaseAds();

        string items = string.Empty;
        string baseUrl = string.Empty;

        foreach (var siteReleaseAd in siteReleaseAdViews)
        {
            int siteReleaseAdID = siteReleaseAd.Code;
            baseUrl = string.Format(@"http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/L{0}/", siteReleaseAdID);
            string imageSource = baseUrl + siteReleaseAd.MainPicUrl;

            //baseUrl = this.ResolveUrl(baseUrl);
            //string imageSource = string.Empty;
            //string path = Server.MapPath(baseUrl);
            //if (Directory.Exists(path))
            //{
            //    //int index = 0;
            //    //foreach (var file in Directory.GetFiles(path, "*.jpg"))
            //    //{
            //    //    index++;
            //    //    string fileName = Path.GetFileName(file);
            //    //    imageSource = baseUrl + fileName;
            //    //    break;
            //    //}
            //}
            string li = "<li>"
                        + "<table width=100% border=\"0\" cellpadding=\"0\" cellspacing=\"0\">"
                            + "<tr>"
                                + "<td colspan=2 style=\"vertical-align: top;\">"
                                    + "<div class=\"releaseViewerImage\">"
                                        + "<img src=\"" + imageSource + "\" />"
                                    + "</div>"
                                + "</td>"
                                + "<td>"
                                    + "<div class=\"releaseViewerInfo\">"
                                        + "<div class=\"releaseViewerDescription\">"
                                            + "<h3>" + siteReleaseAd.Name + "</h3>"
                                            + "<p>" + siteReleaseAd.ShortDescription + "</p>"
                                            + "<h4>" + siteReleaseAd.AreaText + "</h4>"
                                            + "<h4>" + siteReleaseAd.RoomText + "</h4>"
                                        + "</div>"
                                        + "<div class=\"releaseViewerDetailButton\">"
                                        + "<a href=\"/Imovel/Lancamentos/?ID=" + siteReleaseAd.Code.ToString() + "\"><b>Conheça este lançamento!</b></a>"
                                        + "</div>"
                                    + "</div>"
                                + "</td>"
                            + "</tr>"
                        + "</table>"
                    + "</li>";
            items += li + Environment.NewLine;


        }
        if (string.IsNullOrEmpty(items))
        {
            result = string.Empty;
        }
        else
        {
            string div = "<div  id=\"slider\" class=\"releaseViewer\"> {0} </div>";
            string ul = "<ul>{0}</ul>";
            ul = string.Format(ul, items);
            div = string.Format(div, ul);
            result = div;
        }
        return result;
    }
        //<div  id="slider" class="releaseViewer">
        //    <ul>				
                //<li>
                    //<table border="0" cellpadding="0" cellspacing="0">
                    //    <tr>
                    //        <td>
                    //            <div class="releaseViewerImage">
                    //                <img src="Imovel/Fotos/Venda/1/foto_00001_01_Fachada_____________.jpg" />
                    //            </div>
                    //        </td>
                    //        <td>
                    //            <div class="releaseViewerInfo">
                    //                <div class="headerBlueShortLine">
                    //                    <h1>Lançamento</h1>
                    //                </div>
                    //                <h3>Apartamento 2 - bairro Nonoai</h3>
                    //                <p>
                    //                    Descrição resumida do imóvel 2</p>
                    //                <h4>
                    //                    80m² a 100m²</h4>
                    //                <h4>
                    //                    2 a 3 dormitórios</h4>
                    //                <a href="/Imovel/Lancamentos/?ID=1"><b>Detalhes</b></a>
                    //            </div>
                    //        </td>
                    //    </tr>
                    //</table>
                //</li>
            //</ul>
    //</div>


    protected void listViewSearchResults_PagePropertiesChanged(object sender, EventArgs e)
    {
        //setDataBinding(Page.Session[searchResultSessionKey]);
    }

}
