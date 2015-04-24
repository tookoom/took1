using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Presentation;
using TK1.Bizz.Broker.Presentation.Culture;
using TK1.Bizz.Inetsoft.Rent.Xml;
using TK1.Bizz.Mapper.Model;
using TK1.Bizz.Pieta;
using TK1.Data.Bizz.Client.Controller;
using TK1.Utility;

namespace TK1.Bizz.Inetsoft.Client
{
    public class ClientPropertyRentAdHelper
    {
        #region PRIVATE MEMBERS
        private string basePicUrl = string.Empty;
        private string customerCode;
        private string uiCulture = "pt-BR";

        #endregion
        #region PUBLIC PROPERTIES
        public string BasePicUrl
        {
            get { return basePicUrl; }
            set { basePicUrl = value; }
        }
        public string CustomerCode
        {
            get { return customerCode; }
        }
        public bool SendReportMail { get; set; }
        public bool SendErrorOnly { get; set; }
        public string UICulture
        {
            get { return uiCulture; }
            set { uiCulture = value; }
        }

        #endregion

        public ClientPropertyRentAdHelper(string customerCode)
        {
            this.customerCode = customerCode;
        }

        public string LoadFile(string sourceDir, string fileFilter)
        {
            AuditClientController audit = new AuditClientController(customerCode, AppNames.IntegraInetsoftRent.ToString());
            var result = string.Empty;
            var loadResult = false;
            var errorCount = 0;
            var successCount = 0;
            try
            {
                audit.StartProcessExecution();
                audit.WriteEvent("Iniciando processo de carga de cadastro de imóveis (aluguel)", sourceDir ?? "[NULL DIR]");

                var files = FileHelper.GetFiles(sourceDir, fileFilter);
                audit.WriteEvent("Total de arquivos a carregar ", files.Count.ToString());
                if (files.Count == 0)
                    SendReportMail = false;
                foreach (var filePath in files)
                {
                    try
                    {
                        audit.WriteEvent("Início da carga de arquivo", filePath ?? "[NULL PATH]");
                        audit.WriteEvent("Data de modificação do arquivo", File.GetLastWriteTime(filePath).ToString("yyyy-MM-dd HH:mm:ss"));
                        var sites = XmlSiteHelper.LoadSiteFromFile(filePath);
                        if (sites != null)
                        {
                            audit.WriteEvent("Adicionando imóveis para alugar", string.Format("{0} imóveis", sites.Sites.Count));
                            importPropertyAds(sites.Sites);
                            int count = removePropertyAds(sites.Sites);
                            audit.WriteEvent("Imóveis para alugar removidos do cadastro", string.Format("{0} imóveis", count));
                        }
                        moveXmlFile(filePath);
                        successCount++;
                        audit.WriteEvent("Arquivo carregado com sucesso", filePath ?? "[NULL PATH]");
                    }
                    catch (Exception fileException)
                    {
                        errorCount++;
                        if (audit != null)
                            audit.WriteException("Falha na carga do arquivo", fileException);
                    }
                }
                audit.WriteEvent("Arquivos carregados com sucesso", successCount.ToString());
                audit.WriteEvent("Arquivos com falha na carga", errorCount.ToString());
                audit.WriteEvent("Finalizando processo de carga", "Sucesso");
                loadResult = true;

            }
            catch (Exception exception)
            {
                audit.WriteException("Finalizando processo de carga devido a erros", exception);
            }
            finally
            {
                audit.FinishProcessExecution(loadResult);
                result = audit.GenerateHtmlReport();
                if (SendReportMail)
                {
                    sendReportMail(result, errorCount > 0);
                }
            }
            return result;
        }

        private PropertyAdCategories getPropertyAdCategories(string categoryName)
        {
            PropertyAdCategories result = PropertyAdCategories._Undefined;
            if (Enum.TryParse<PropertyAdCategories>(categoryName, out result))
                result = PropertyAdCategories._Undefined;
            return result;
        }
        private void importPropertyAds(List<XmlSite> ads)
        {
            if (ads == null)
                throw new ArgumentNullException("Parameter ads can't be null");

            var propertyAdController = new PropertyAdController(customerCode);
            foreach (var item in ads)
            {
                var propertyAdCategory = getPropertyAdCategories(item.Category);

                propertyAdController.SetPropertyAd(new PropertyAdView()
                {
                    AdCategory = propertyAdCategory,
                    AdType = PropertyAdTypes.Rent,
                    AdTypeName = PropertyAdTypes.Rent.ToString(),
                    Location = new Location()
                    {
                        AddressLine = StringHelper.ConvertCaseString(item.Address.Trim(), StringHelper.UpperCase.UpperFirstWord),
                        Locality = new GeoLocation() { Name = StringHelper.ConvertCaseString(item.City.Trim(), StringHelper.UpperCase.UpperFirstWord) },
                        District = new GeoLocation() { Name = StringHelper.ConvertCaseString(item.District.Trim(), StringHelper.UpperCase.UpperFirstWord) }
                    },
                    AreaDescription = item.AreaDescription,
                    CityTaxes = 0,
                    AdCode = item.AdCode,
                    CondoDescription = item.CondDescription,
                    CondoTaxes = 0,
                    FullDescription = item.InternetDescription,
                    IsFeatured = item.IsFeatured,
                    InternalArea = item.InternalArea,
                    TotalArea = item.TotalArea,
                    TotalRooms = item.TotalRooms,
                    PropertyType = StringHelper.ConvertCaseString(item.SiteType.Trim(), StringHelper.UpperCase.UpperFirstWord),
                    ShortDescription = item.ShortDescription,
                    Value = item.Value
                });

                propertyAdController.RemovePropertyAdDetails(PropertyAdTypes.Rent, item.AdCode);
                if (item.TotalArea > 0)
                {
                    propertyAdController.SetPropertyAdDetails(PropertyAdTypes.Rent, item.AdCode, new PropertyAdDetailView
                    {
                        Code = "InternalArea",
                        Value = PropertyTranslations.GetAreaDisplayName(item.InternalArea, uiCulture)
                    });
                }
                if (item.TotalRooms > 0)
                {
                    propertyAdController.SetPropertyAdDetails(PropertyAdTypes.Rent, item.AdCode, new PropertyAdDetailView
                    {
                        Code = "TotalRooms",
                        Value = PropertyTranslations.GetRoomQuantityDisplayName(item.SiteType, item.TotalRooms, uiCulture)
                    });
                }

                propertyAdController.RemovePropertyAdPics(PropertyAdTypes.Rent, item.AdCode);
                foreach (var pic in item.Pictures)
                {
                    propertyAdController.SetPropertyAdPics(PropertyAdTypes.Rent, item.AdCode, new PropertyAdPicView
                    {
                        Description = pic.Description,
                        FileName = pic.FileName,
                        Index = pic.Index,
                        Url = string.Format("{0}/{1}/{2}", basePicUrl, item.AdCode, pic.FileName),
                        ThumbnailUrl = string.Format("{0}/{1}/Thumbnail/{2}", basePicUrl, item.AdCode, pic.FileName)
                    });

                }

            }
        }
        private void moveXmlFile(string sourcePath)
        {
            if (!string.IsNullOrEmpty(sourcePath))
            {
                if (File.Exists(sourcePath))
                {
                    string destinationDir = Path.GetDirectoryName(sourcePath) + @"\Carregado\";
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);
                    destinationDir += DateTime.Now.ToString("yyyy-MM-dd_HHmmsss") + "\\";
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);

                    string fileName = Path.GetFileName(sourcePath);
                    string destinationPath = destinationDir + fileName;
                    File.Move(sourcePath, destinationPath);

                }
            }
        }
        private int removePropertyAds(List<XmlSite> ads)
        {
            int result = 0;
            if (ads == null)
                throw new ArgumentNullException("Parameter ads can't be null");

            var propertyAdController = new PropertyAdController(customerCode);
            var propertyAds = propertyAdController.GetPropertyAds(PropertyAdTypes.Rent);
            foreach (var item in propertyAds)
            {
                var xmlAd = ads.Where(o => o.AdCode == item.AdCode).FirstOrDefault();
                if (xmlAd == null)
                {
                    propertyAdController.RemovePropertyAd(PropertyAdTypes.Rent, item.AdCode);
                    result++;
                }
            }
            return result;
        }
        private void sendReportMail(string body, bool hasError)
        {
            try
            {
                var subject = "Relatório de carga de cadastros IMÓVEIS ALUGUEL";
                var mailTo = new ConfigurationController(customerCode).GetValue(ConfigurationKeys.XmlLoadNotificationMailAddress);

                if (SendReportMail)
                {
                    var mailHelper = new MailHelper();
                    if (SendErrorOnly)
                        mailHelper.SendMail(subject, body, mailTo);
                    mailHelper.SendMail(subject, body, mailTo);
                }
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("TK1.Bizz.Inetsoft.Client.ClientPropertyRentAdHelper.sendReportMail", exception);
            }
        }

    }
}
