using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using TK1.Data;
using System.IO;
using System.Web.Hosting;

[ServiceContract(Namespace = "")]
[SilverlightFaultBehavior]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class BrokerFileService
{

    [OperationContract]
    public void LogCustomerAccess(string customerCodename)
    {
        AppLogController.WriteAppLogEntry("Bizz Service Customer Access", customerCodename);
    }
    [OperationContract]
    public PictureInfo SaveBrokerSiteAdPic(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, UploadPicture uploadPicture)
    {
        PictureInfo result = new PictureInfo() { Success = false };
        string rootPath = HostingEnvironment.MapPath("~/");
        string rootUrl = @"http://www.tk1.net.br/";
        rootPath += "Integra\\Arquivos\\Bizz\\Broker\\RealEstate";
        rootUrl += "Integra/Arquivos/Bizz/Broker/RealEstate";
        if (!Directory.Exists(rootPath))
            Directory.CreateDirectory(rootPath);
        if (uploadPicture != null & !string.IsNullOrEmpty(customerCodename))
        {
            string basePath = string.Format("{0}\\{1}\\{2}\\{3}\\", rootPath, customerCodename, adType.ToString(), siteAdID);
            string baseUrl = string.Format("{0}/{1}/{2}/{3}/", rootUrl, customerCodename, adType.ToString(), siteAdID);

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string thumbnailPath = basePath + "Thumbs\\";
            if (!Directory.Exists(thumbnailPath))
                Directory.CreateDirectory(thumbnailPath);

            string pictureFilePath = basePath + uploadPicture.FileName;
            string thumbnailFilePath = thumbnailPath + uploadPicture.FileName;
            AppLogController.WriteAppLogEntry("SaveBrokerSiteAdPic", pictureFilePath);

            result.PicturePath = pictureFilePath;
            result.ThumbnaiPath = thumbnailFilePath;
            result.PictureUrl = baseUrl + uploadPicture.FileName;
            result.ThumbnailUrl = baseUrl + "Thumbs/" + uploadPicture.FileName;

            try
            {
                FileStream fileStream = new FileStream(pictureFilePath, FileMode.Create);
                fileStream.Write(uploadPicture.File, 0, uploadPicture.File.Length);
                fileStream.Close();
                fileStream.Dispose();

                fileStream = new FileStream(thumbnailFilePath, FileMode.Create);
                fileStream.Write(uploadPicture.Thumbnail, 0, uploadPicture.Thumbnail.Length);
                fileStream.Close();
                fileStream.Dispose();

                result.Success = true;
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("BrokerFileService.SaveBrokerSiteAdPic", exception);
                result.Success = false;
            }
        }
        return result;
    }
}

[DataContract]
public class UploadPicture
{
    [DataMember]
    public string FileName;
    [DataMember]
    public byte[] File;
    [DataMember]
    public byte[] Thumbnail;
}

[DataContract]
public class PictureInfo
{
    [DataMember]
    public string PictureUrl;
    [DataMember]
    public string ThumbnailUrl;
    [DataMember]
    public string PicturePath;
    [DataMember]
    public string ThumbnaiPath;
    [DataMember]
    public bool Success;

}