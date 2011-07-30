using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Mdo.Selling;
using TK1.Html.Elements;
using System.IO;
using TK1.Media.Imaging;

public partial class Integra_Mdo_SimVendas_Xml_Carregar_CargaFotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var mdoCode = 3;
        var siteID = 5;
        var div = new HtmlDiv();
        string picturesPath = getPictureFilesPath(mdoCode, siteID);
        div.Children.Add(new HtmlParagraph(picturesPath));
        var report = resizeSitePics(picturesPath, 500, 70);
        if(report!= null)
            div.Children.Add(report);
        literalResponse.Text = div.GetHtml();

    }

    private string getPictureFilesPath(int mdoCode, int siteID)
    {
        string result = string.Empty;
        string relUrl = "~\\Integra\\Mdo\\SimVendas\\Fotos\\";

        relUrl = this.ResolveUrl(relUrl);
        result = Server.MapPath(relUrl) + string.Format("{0}\\{1}", mdoCode, siteID);
        return result;
    }
    private HtmlDiv resizeSitePics(string picRootPath, int picSize, int thumbSize)
    {
        var audit = new HtmlDiv();
        if (!string.IsNullOrEmpty(picRootPath))
        {
            if (Directory.Exists(picRootPath))
            {
                var dir = picRootPath;
                string resizedDir = string.Format("{0}\\resized\\", dir);
                if (Directory.Exists(resizedDir))
                    Directory.Delete(resizedDir, true);
                Directory.CreateDirectory(resizedDir);

                string thumbnailDir = string.Format("{0}\\thumbs\\", dir);
                if (Directory.Exists(thumbnailDir))
                    Directory.Delete(thumbnailDir, true);
                Directory.CreateDirectory(thumbnailDir);

                foreach (string filePath in System.IO.Directory.GetFiles(dir))
                {
                    string extension = System.IO.Path.GetExtension(filePath);
                    if (extension != null & extension != string.Empty)
                    {
                        if (extension.ToLower() == ".jpg")
                        {
                            string fileName = System.IO.Path.GetFileName(filePath);
                            string resizedFilePath = string.Format("{0}{1}", resizedDir, fileName);
                            string thumbnailFilePath = string.Format("{0}{1}", thumbnailDir, fileName);

                            try
                            {
                                ImageHelper.CreateThumbnail(filePath, thumbnailFilePath, thumbSize, thumbSize);
                                ImageHelper.Resize(filePath, resizedFilePath, picSize, picSize);

                            }
                            catch (Exception exception)
                            {
                                audit.Children.Add(new HtmlParagraph(string.Format("{0}", exception)));

                            }
                        }
                    }

                }
            }
        }
        return audit;
    }


}