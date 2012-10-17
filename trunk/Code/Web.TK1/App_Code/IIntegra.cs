using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TK1.Bizz.Data.Presentation;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIntegra" in both code and config file together.
[ServiceContract]
public interface IIntegra
{
    [OperationContract]
    void DoWork();
    [OperationContract]
    void LogHostingRootPath(string path);
    [OperationContract]
    string ImportXmlFile(string code, string fileName, string fileContent);

    [OperationContract]
    void SaveBrokerSiteAdPic(string customerCodename, SiteAdTypes adType, int siteAdID, UploadPicture uploadfile);
}
