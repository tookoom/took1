using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using TK1.Xml;
using TK1.Collection;
using TK1.Data.Converter;

namespace TK1.Bizz.Inetsoft.Rent.Xml
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
                    result.Sites = getXmlSites(root);
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
                fileContent = adjustFileContent(fileContent);
                result = LoadSite(fileContent);
            }
            return result;
        }

        private static string adjustFileContent(string fileContent)
        {
            if (fileContent != null)
            {
                var invalidChar = new string((char)0x08, 1);
                if (fileContent.Contains(invalidChar))
                    fileContent = fileContent.Replace(invalidChar,string.Empty);
                invalidChar = new string((char)0x10, 1);
                if (fileContent.Contains(invalidChar))
                    fileContent = fileContent.Replace(invalidChar, string.Empty);
                invalidChar = new string((char)0x19, 1);
                if (fileContent.Contains(invalidChar))
                    fileContent = fileContent.Replace(invalidChar, string.Empty);
            }
            return fileContent;
        }

        private static List<XmlSite> getXmlSites(XElement root)
        {
            var xmlSites = new List<XmlSite>();
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
                        xmlSite.CityTaxes = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.CityTaxes), 0);
                        xmlSite.CondDescription = XmlLoader.GetElementValue(element, XmlSiteTags.CondDescription);
                        xmlSite.CondoTaxes = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.CondoTaxes), 0);
                        xmlSite.District = XmlLoader.GetElementValue(element, XmlSiteTags.District);
                        if (xmlSite.District != null)
                            xmlSite.District = xmlSite.District.Trim();
                        xmlSite.ExternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.ExternalArea), 0);
                        xmlSite.InternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.InternalArea), 0);
                        xmlSite.InternetDescription = XmlLoader.GetElementValue(element, XmlSiteTags.Description);
                        xmlSite.IsFeatured = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsFeatured), "N").ToUpper() == "S";
                        xmlSite.TotalRooms = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.RoomNumber), 0);
                        xmlSite.AdCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.SiteCode), 0);
                        xmlSite.SiteType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.SiteType), "Não Cadastrado");
                        if (xmlSite.SiteType != null)
                            xmlSite.SiteType = xmlSite.SiteType.Trim();
                        xmlSite.ShortDescription = XmlLoader.GetElementValue(element, XmlSiteTags.ShortDescription);
                        xmlSite.TotalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.TotalArea), 0);
                        xmlSite.UF = XmlLoader.GetElementValue(element, XmlSiteTags.UF);
                        xmlSite.Value = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.Value), 0);
                        xmlSite.ZipCode = XmlLoader.GetElementValue(element, XmlSiteTags.ZipCode);

                        xmlSite.Details = new StringDictionary();
                        if (xmlSite.TotalRooms == 1)
                            xmlSite.Details.Set("Dormitório", xmlSite.TotalRooms.ToString());
                        if (xmlSite.TotalRooms > 1)
                            xmlSite.Details.Set("Dormitórios", xmlSite.TotalRooms.ToString());


                        var auxValue = XmlLoader.GetElementValue(element, XmlSiteTags.SuiteNumber);
                        if (!string.IsNullOrEmpty(auxValue))
                        {
                            if (auxValue != "0")
                            {
                                if (auxValue == "1")
                                    xmlSite.Details.Set("Suíte", auxValue);
                                else
                                    xmlSite.Details.Set("Suítes", auxValue);
                            }
                        }

                        auxValue = XmlLoader.GetElementValue(element, XmlSiteTags.GarageNumber);
                        if (!string.IsNullOrEmpty(auxValue))
                        {
                            if (auxValue != "0")
                            {
                                if (auxValue == "1")
                                    xmlSite.Details.Set("Garagem", auxValue);
                                else
                                    xmlSite.Details.Set("Garagens", auxValue);
                            }
                        }

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
                                        SiteCode = xmlSite.AdCode,
                                        Index = index
                                    });
                                }
                            }
                        }
                        xmlSites.Add(xmlSite);

                        //xmlSite.AdType = XmlLoader.GetElementValue(element, XmlSiteTags.AdType);
                        //xmlSite.BuildingName = XmlLoader.GetElementValue(element, XmlSiteTags.BuildingName);
                        //xmlSite.ExcludeSite = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.ExcludeSite), "N").ToUpper() == "S";
                        //xmlSite.HasBanner = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.HasBanner), "N").ToUpper() == "S";
                        //xmlSite.IsExclusive = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsExclusive), "N").ToUpper() == "S";

                    }
                }
            }
            return xmlSites;
        }
    }
}
