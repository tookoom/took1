﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using TK1.Xml;
using TK1.Collection;
using TK1.Data.Converter;

namespace TK1.Bizz.Pieta.Xml
{
    public class XmlSiteHelper
    {
        public static List<XmlSite> LoadSite(string fileContent)
        {
            List<XmlSite> result = new List<XmlSite>();
            if (!string.IsNullOrEmpty(fileContent))
            {
                XElement root = XElement.Load(new StringReader(fileContent));
                if (root != null)
                {
                    var sites = root.Elements(XmlSiteTags.Site);
                    if (sites != null)
                    {
                        foreach (var element in sites)
                        {
                            XmlSite xmlSite = new XmlSite();
                            xmlSite.Address = XmlLoader.GetElementValue(element, XmlSiteTags.Address);
                            xmlSite.AddressNumber = XmlLoader.GetElementValue(element, XmlSiteTags.AddressNumber);
                            xmlSite.AdType = XmlLoader.GetElementValue(element, XmlSiteTags.AdType);
                            xmlSite.Area = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.Area), -1);
                            xmlSite.BuildingName = XmlLoader.GetElementValue(element, XmlSiteTags.BuildingName);
                            xmlSite.City = XmlLoader.GetElementValue(element, XmlSiteTags.City);
                            xmlSite.District = XmlLoader.GetElementValue(element, XmlSiteTags.District);
                            xmlSite.ExcludeSite = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.ExcludeSite), "N").ToUpper() == "S";
                            xmlSite.HasBanner = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.HasBanner), "N").ToUpper() == "S";
                            xmlSite.InternetDescription = XmlLoader.GetElementValue(element, XmlSiteTags.InternetDescription);
                            xmlSite.IsExclusive = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsExclusive), "N").ToUpper() == "S";
                            xmlSite.IsHighlighted = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.IsHighlighted), "N").ToUpper() == "S";
                            xmlSite.RoomNumber = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.RoomNumber), -1);
                            xmlSite.SiteCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSiteTags.SiteCode), -1);
                            xmlSite.SiteType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSiteTags.SiteType), "Não Cadastrado");
                            xmlSite.UF = XmlLoader.GetElementValue(element, XmlSiteTags.UF);
                            xmlSite.Value = StringConverter.ToFloat(XmlLoader.GetElementValue(element, XmlSiteTags.Value), -1);
                            xmlSite.ZipCode = XmlLoader.GetElementValue(element, XmlSiteTags.ZipCode);
                            xmlSite.DescriptionCollection = new StringDictionary();
                            var details = element.Element(XmlSiteTags.DescriptionCollection);
                            if (details != null)
                            {
                                var descriptionCollection = details.Elements(XmlSiteTags.Description);
                                if (descriptionCollection != null)
                                {
                                    foreach (var description in descriptionCollection)
                                    {
                                        string key = XmlLoader.GetElementValue(description, XmlSiteTags.DescriptionName);
                                        if (!string.IsNullOrEmpty(key))
                                            xmlSite.DescriptionCollection.Add(new StringPair() { Key = key, Value = XmlLoader.GetElementValue(description, XmlSiteTags.DescriptionValue) });
                                    }
                                }
                            }
                            result.Add(xmlSite);
                        }
                    }
                }
            }

            return result;
        }
        public static List<XmlSite> LoadSiteFromFile(string path)
        {
            List<XmlSite> result = new List<XmlSite>();
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                result = LoadSite(fileContent);
            }
            return result;
        }
        public static List<XmlSitePic> LoadSitePic(string fileContent)
        {
            List<XmlSitePic> result = new List<XmlSitePic>();
            if (!string.IsNullOrEmpty(fileContent))
            {
                XElement root = XElement.Load(new StringReader(fileContent));
                if (root != null)
                {
                    var sites = root.Elements(XmlSitePicTags.SitePic);
                    if (sites != null)
                    {
                        foreach (var element in sites)
                        {
                            XmlSitePic xmlSitePic = new XmlSitePic();
                            xmlSitePic.ExcludeSitePic = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSitePicTags.ExcludeSitePic), "N").ToUpper() == "S";
                            xmlSitePic.SiteCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSitePicTags.SiteCode), -1);
                            xmlSitePic.SitePicCode = StringConverter.ToInt(XmlLoader.GetElementValue(element, XmlSitePicTags.SitePicCode), -1);
                            xmlSitePic.FileData = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSitePicTags.FileData), null);
                            xmlSitePic.FileType = StringConverter.ToString(XmlLoader.GetElementValue(element, XmlSitePicTags.FileType), null);
                            result.Add(xmlSitePic);
                        }
                    }
                }
            }

            return result;
        }
        public static List<XmlSitePic> LoadSitePicFromFile(string path)
        {
            List<XmlSitePic> result = new List<XmlSitePic>();
            if (File.Exists(path))
            {
                string fileContent = File.ReadAllText(path);
                result = LoadSitePic(fileContent);
            }
            return result;
        }
    }
}