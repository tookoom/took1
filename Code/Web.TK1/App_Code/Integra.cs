using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TK1.Data;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Integra" in code, svc and config file together.
public class Integra : IIntegra
{
	public void DoWork()
	{
        string data = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        AppLogController.WriteAppLogEntry("Teste", data);
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
}
