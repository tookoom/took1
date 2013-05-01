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
                        result.Sites = getXmlSites(root);
                        result.SiteReleases = getXmlSiteReleases(root); 

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
                fileContent = adjustFileContent(fileContent);
                result = LoadSite(fileContent);
            }
            return result;
        }

        private static string adjustFileContent(string fileContent)
        {
            if (!string.IsNullOrEmpty(fileContent))
            {
                fileContent = adjustFileContent(fileContent, "<FotoLanc{0}>", "<FotoLanc>");
                fileContent = adjustFileContent(fileContent, "</FotoLanc{0}>", "</FotoLanc>");
                fileContent = adjustFileContent(fileContent, "<FotoPlanta{0}>", "<FotoPlanta>");
                fileContent = adjustFileContent(fileContent, "</FotoPlanta{0}>", "</FotoPlanta>");
                fileContent = adjustFileContent(fileContent, "<FotoMapa{0}>", "<FotoMapa>");
                fileContent = adjustFileContent(fileContent, "</FotoMapa{0}>", "</FotoMapa>");
            }
            return fileContent;
        }

        private static string adjustFileContent(string fileContent, string xmlTag, string xmlReplaceTag)
        {
            if (!string.IsNullOrEmpty(fileContent) & !string.IsNullOrEmpty(xmlTag))
            {
                for (int i = 0; i < 100; i++)
                {
                    string replacement = string.Format(xmlTag, i<10 ? "0" + i.ToString() : i.ToString());
                    fileContent = fileContent.Replace(replacement, xmlReplaceTag);
                }
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
                        xmlSite.CondDescription = XmlLoader.GetElementValue(element, XmlSiteTags.CondDescription);
                        xmlSite.District = XmlLoader.GetElementValue(element, XmlSiteTags.District);
                        if (xmlSite.District != null)
                            xmlSite.District = xmlSite.District.Trim();
                        xmlSite.MinExternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.ExternalArea), 0);
                        xmlSite.MinInternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.InternalArea), 0);
                        xmlSite.InternetDescription = XmlLoader.GetElementValue(element, XmlSiteTags.Description);
                        xmlSite.IsFeatured = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsFeatured), "N").ToUpper() == "S";
                        xmlSite.MinTotalRooms = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.RoomNumber), 0);
                        xmlSite.AdCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.SiteCode), 0);
                        xmlSite.SiteType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.SiteType), "Não Cadastrado");
                        if (xmlSite.SiteType != null)
                            xmlSite.SiteType = xmlSite.SiteType.Trim();
                        xmlSite.ShortDescription = XmlLoader.GetElementValue(element, XmlSiteTags.ShortDescription);
                        xmlSite.MinTotalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.TotalArea), 0);
                        xmlSite.UF = XmlLoader.GetElementValue(element, XmlSiteTags.UF);
                        xmlSite.MinValue = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.Value), 0);
                        xmlSite.ZipCode = XmlLoader.GetElementValue(element, XmlSiteTags.ZipCode);

                        xmlSite.Details = new StringDictionary();
                        if (xmlSite.MinTotalRooms == 1)
                            xmlSite.Details.Set("Dormitório", xmlSite.MinTotalRooms.ToString());
                        if (xmlSite.MinTotalRooms > 1)
                            xmlSite.Details.Set("Dormitórios", xmlSite.MinTotalRooms.ToString());


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
        private static List<XmlSiteRelease> getXmlSiteReleases(XElement root)
        {
            var result = new List<XmlSiteRelease>();
            var releaseRoot = root.Element(XmlSiteReleaseTags.Releases);
            if (releaseRoot != null)
            {
                var releases = releaseRoot.Elements(XmlSiteReleaseTags.Release);
                if (releases != null)
                {
                    foreach (var element in releases)
                    {
                        XmlSiteRelease xmlSiteRelease = new XmlSiteRelease();
                        xmlSiteRelease.AdCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.Code), 0);
                        xmlSiteRelease.Name = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.Name);
                        xmlSiteRelease.Address = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.Address);
                        if (xmlSiteRelease.Address != null)
                            xmlSiteRelease.Address = xmlSiteRelease.Address.Trim();
                        xmlSiteRelease.AddressNumber = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.AddressNumber);
                        xmlSiteRelease.District = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.District);
                        if (xmlSiteRelease.District != null)
                            xmlSiteRelease.District = xmlSiteRelease.District.Trim();
                        xmlSiteRelease.City = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.City);
                        if (xmlSiteRelease.City != null)
                            xmlSiteRelease.City = xmlSiteRelease.City.Trim();
                        xmlSiteRelease.AddressComplement = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.AddressComplement);
                        if (xmlSiteRelease.AddressComplement != null)
                            xmlSiteRelease.AddressComplement = xmlSiteRelease.AddressComplement.Trim();
                        xmlSiteRelease.ConstructorName = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.ConstructorName);
                        if (xmlSiteRelease.ConstructorName != null)
                            xmlSiteRelease.ConstructorName = xmlSiteRelease.ConstructorName.Trim();
                        xmlSiteRelease.MinTotalRooms = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinRoomNumber), 0);
                        xmlSiteRelease.MaxTotalRooms = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxRoomNumber), 0);
                        xmlSiteRelease.MinSuites = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinSuiteNumber), 0);
                        xmlSiteRelease.MaxSuites = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxSuiteNumber), 0);
                        xmlSiteRelease.MinParkingLots = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinGarageNumber), 0);
                        xmlSiteRelease.MaxParkingLots = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxGarageNumber), 0);
                        xmlSiteRelease.MinValue = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinValue), 0);
                        xmlSiteRelease.MaxValue = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxValue), 0);
                        xmlSiteRelease.MinInternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinInternalArea), 0);
                        xmlSiteRelease.MaxInternalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxInternalArea), 0);
                        xmlSiteRelease.MinTotalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MinTotalArea), 0);
                        xmlSiteRelease.MaxTotalArea = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.MaxTotalArea), 0);

                        
                        xmlSiteRelease.AreaDescription = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.AreaDescription);
                        xmlSiteRelease.CondDescription = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.CondoDescription);
                        xmlSiteRelease.SiteType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteReleaseTags.UnitName), "Não Cadastrado");
                        if (xmlSiteRelease.SiteType != null)
                            xmlSiteRelease.SiteType = xmlSiteRelease.SiteType.Trim();
                        xmlSiteRelease.AdText = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.AdText);
                        xmlSiteRelease.ShortAdText = XmlLoader.GetElementValue(element, XmlSiteReleaseTags.ShortAdText);

                        xmlSiteRelease.BluePrints = new List<XmlSitePic>();
                        xmlSiteRelease.Maps = new List<XmlSitePic>();
                        xmlSiteRelease.Pictures = new List<XmlSitePic>();
                        var pictures = element.Element(XmlSiteTags.Pictures);
                        if (pictures != null)
                        {
                            var pictureCollection = pictures.Elements(XmlSiteReleaseTags.Picture);
                            if (pictureCollection != null)
                            {
                                int index = 0;
                                foreach (var picture in pictureCollection)
                                {
                                    index++;
                                    string fileName = XmlLoader.GetElementValue(picture, XmlSiteReleaseTags.PictureFileName);
                                    string description = XmlLoader.GetElementValue(picture, XmlSiteTags.PictureDescription);
                                    xmlSiteRelease.Pictures.Add(new XmlSitePic()
                                    {
                                        Description = description,
                                        FileName = fileName,
                                        SiteCode = xmlSiteRelease.AdCode,
                                        Index = index
                                    });
                                }
                            }

                            var mapCollection = pictures.Elements(XmlSiteReleaseTags.Map);
                            if (mapCollection != null)
                            {
                                int index = 0;
                                foreach (var map in mapCollection)
                                {
                                    index++;
                                    string fileName = XmlLoader.GetElementValue(map, XmlSiteReleaseTags.PictureFileName);
                                    string description = XmlLoader.GetElementValue(map, XmlSiteTags.PictureDescription);
                                    xmlSiteRelease.Maps.Add(new XmlSitePic()
                                    {
                                        Description = description,
                                        FileName = fileName,
                                        SiteCode = xmlSiteRelease.AdCode,
                                        Index = index
                                    });
                                }
                            }
                            var bluePrintCollection = pictures.Elements(XmlSiteReleaseTags.BluePrint);
                            if (bluePrintCollection != null)
                            {
                                int index = 0;
                                foreach (var bluePrint in bluePrintCollection)
                                {
                                    index++;
                                    string fileName = XmlLoader.GetElementValue(bluePrint, XmlSiteReleaseTags.PictureFileName);
                                    string description = XmlLoader.GetElementValue(bluePrint, XmlSiteTags.PictureDescription);
                                    xmlSiteRelease.BluePrints.Add(new XmlSitePic()
                                    {
                                        Description = description,
                                        FileName = fileName,
                                        SiteCode = xmlSiteRelease.AdCode,
                                        Index = index
                                    });
                                }
                            }
                        }

                        result.Add(xmlSiteRelease);
                    }
                }
            }
            return result;
        }

    }
}
