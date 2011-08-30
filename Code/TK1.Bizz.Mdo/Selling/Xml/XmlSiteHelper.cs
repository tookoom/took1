using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using TK1.Xml;
using TK1.Collection;
using TK1.Data.Converter;
using TK1.Bizz.Mdo.Xml;

namespace TK1.Bizz.Mdo.Selling.Xml
{
    public class XmlSiteHelper
    {
        public static XmlSiteFile LoadSite(string fileContent)
        {
            XmlSiteFile result = new XmlSiteFile();
            if (!string.IsNullOrEmpty(fileContent))
            {
                XElement root = XElement.Load(new StringReader(fileContent));
                if (root != null)
                {
                    var header = root.Element(XmlSiteTags.Header.FileInfo);
                    if (header != null)
                    {
                        XmlHeader xmlHeader = new XmlHeader();
                        xmlHeader.CustomerCode = XmlLoader.GetElementValue(header, XmlSiteTags.Header.CustomerCode);
                        xmlHeader.CustomerName = XmlLoader.GetElementValue(header, XmlSiteTags.Header.CustomerName);

                        result.Header = xmlHeader;
                        result.Sites = new List<XmlSite>();
                        var sites = root.Element(XmlSiteTags.Sites);
                        if (sites != null)
                        {
                            var siteCollection = sites.Elements(XmlSiteTags.Site);
                            if (siteCollection != null)
                            {
                                foreach (var element in siteCollection)
                                {
                                    XmlSite xmlSite = new XmlSite();
                                    xmlSite.Address = XmlLoader.GetElementValue(element, XmlSiteTags.Address);
                                    if (xmlSite.Address != null)
                                        xmlSite.Address = xmlSite.Address.Trim();
                                    xmlSite.AddressNumber = XmlLoader.GetElementValue(element, XmlSiteTags.AddressNumber);
                                    xmlSite.AreaDescription = XmlLoader.GetElementValue(element, XmlSiteTags.AreaDescription);
                                    xmlSite.Category = XmlLoader.GetElementValue(element, XmlSiteTags.Category);
                                    xmlSite.City = XmlLoader.GetElementValue(element, XmlSiteTags.City);
                                    if (xmlSite.City != null)
                                        xmlSite.City = xmlSite.City.Trim();
                                    xmlSite.CondDescription = XmlLoader.GetElementValue(element, XmlSiteTags.CondDescription);
                                    xmlSite.District = XmlLoader.GetElementValue(element, XmlSiteTags.District);
                                    if (xmlSite.District != null)
                                        xmlSite.District = xmlSite.District.Trim();
                                    xmlSite.ExternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.ExternalArea), 0);
                                    xmlSite.InternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.InternalArea), 0);
                                    xmlSite.InternetDescription = XmlLoader.GetElementValue(element, XmlSiteTags.Description);
                                    xmlSite.IsFeatured = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsFeatured), "N").ToUpper() == "S";
                                    xmlSite.RoomNumber = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.RoomNumber), 0);
                                    xmlSite.SiteCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.SiteCode), 0);
                                    xmlSite.SiteType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.SiteType), "Não Cadastrado");
                                    if (xmlSite.SiteType != null)
                                        xmlSite.SiteType = xmlSite.SiteType.Trim();
                                    xmlSite.ShortDescription = XmlLoader.GetElementValue(element, XmlSiteTags.ShortDescription);
                                    xmlSite.TotalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.TotalArea), 0);
                                    xmlSite.UF = XmlLoader.GetElementValue(element, XmlSiteTags.UF);
                                    xmlSite.Value = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.Value), 0);
                                    xmlSite.ZipCode = XmlLoader.GetElementValue(element, XmlSiteTags.ZipCode);

                                    xmlSite.Details = new StringDictionary();
                                    string auxValue = XmlLoader.GetElementValue(element, XmlSiteTags.RoomNumber);
                                    if (!string.IsNullOrEmpty(auxValue))
                                        xmlSite.Details.Set("Dormitórios", auxValue);
                                    auxValue = XmlLoader.GetElementValue(element, XmlSiteTags.SuiteNumber);
                                    if (!string.IsNullOrEmpty(auxValue))
                                        xmlSite.Details.Set("Suítes", auxValue);
                                    auxValue = XmlLoader.GetElementValue(element, XmlSiteTags.GarageNumber);
                                    if (!string.IsNullOrEmpty(auxValue))
                                        xmlSite.Details.Set("Garagens", auxValue);

                                    xmlSite.Pictures = new List<XmlSitePic>();
                                    var pictures = element.Element(XmlSiteTags.Pictures);
                                    if (pictures != null)
                                    {
                                        var pictureCollection = pictures.Elements(XmlSiteTags.Picture);
                                        if (pictureCollection != null)
                                        {
                                            int index = 0;
                                            foreach (var picture in pictureCollection)
                                            {
                                                index++;
                                                string fileName = XmlLoader.GetElementValue(picture, XmlSiteTags.PictureFileName);
                                                string description = XmlLoader.GetElementValue(picture, XmlSiteTags.PictureDescription);
                                                xmlSite.Pictures.Add(new XmlSitePic()
                                                {
                                                    Description = description,
                                                    FileName = fileName,
                                                    SiteCode = xmlSite.SiteCode,
                                                    Index = index
                                                });
                                            }
                                        }
                                    }
                                    result.Sites.Add(xmlSite);

                                    //xmlSite.AdType = XmlLoader.GetElementValue(element, XmlSiteTags.AdType);
                                    //xmlSite.BuildingName = XmlLoader.GetElementValue(element, XmlSiteTags.BuildingName);
                                    //xmlSite.ExcludeSite = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.ExcludeSite), "N").ToUpper() == "S";
                                    //xmlSite.HasBanner = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.HasBanner), "N").ToUpper() == "S";
                                    //xmlSite.IsExclusive = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsExclusive), "N").ToUpper() == "S";

                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
        public static XmlSiteFile LoadSiteFromFile(string path)
        {
            XmlSiteFile result = new XmlSiteFile();
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                result = LoadSite(fileContent);
            }
            return result;
        }
    }
}
