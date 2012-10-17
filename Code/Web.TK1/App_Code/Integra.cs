using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TK1.Data;
using System.IO;
using System.Web.Hosting;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Integra" in code, svc and config file together.
public class Integra : IIntegra
{
	public void DoWork()
	{
        string data = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        AppLogController.WriteAppLogEntry("Teste", data);
	}
    public void LogHostingRootPath(string path)
    {
        string data = System.Web.Hosting.HostingEnvironment.MapPath("~/" + path);
        AppLogController.WriteAppLogEntry("LogHostingRootPath", data);
    }

    public string ImportXmlFile(string code, string fileName, string fileContent)
    {
        string result = string.Empty;

        if (code == "1234")
        {
            AppLogController.WriteAppLogEntry("ImportXmlFile: File Name", fileName);
            if (!string.IsNullOrEmpty(fileName) & !string.IsNullOrEmpty(fileContent))
            {

                try
                {
                    string directory = System.Web.Hosting.HostingEnvironment.MapPath("~\\Integra\\Mdo\\SimVendas\\Xml\\");
                    string path = string.Format("{0}{1}_{2}.xml", directory, fileName, DateTime.Now.ToString("yyyyMMddhhmmss"));
                    AppLogController.WriteAppLogEntry("ImportXmlFile: Directory", directory);
                    AppLogController.WriteAppLogEntry("ImportXmlFile: Full Path", path);

                    File.WriteAllText(path, fileContent);
                }
                catch (Exception exception)
                {
                    
                    AppLogController.WriteException("ImportXmlFile: Exception", exception);
                }
            }
        }
        return result;
    }

    public void SaveBrokerSiteAdPic(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, UploadPicture uploadPicture)
    {
        string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        rootPath += "Integra\\Arquivos\\Bizz\\Broker\\RealEstate\\";
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);
        if (uploadPicture != null & !string.IsNullOrEmpty(customerCodename))
        {
            string basePath = string.Format("{0}\\{1}\\{2}\\{3}\\", rootPath, customerCodename, adType.ToString(), siteAdID);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            string thumbnailPath = basePath + "Thumbs\\";
            if (!Directory.Exists(thumbnailPath))
                Directory.CreateDirectory(thumbnailPath);

            string filePath = basePath + uploadPicture.FileName;
            string thumbnailFilePath = thumbnailPath + uploadPicture.FileName;
            AppLogController.WriteAppLogEntry("SaveBrokerSiteAdPic", filePath);

            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                fileStream.Write(uploadPicture.File, 0, uploadPicture.File.Length);
                fileStream.Close();
                fileStream.Dispose();

                fileStream = new FileStream(thumbnailFilePath, FileMode.Create);
                fileStream.Write(uploadPicture.Thumbnail, 0, uploadPicture.Thumbnail.Length);
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("Services.Integra.SaveBrokerSiteAdPic", exception);
            }
        }
    }

}

