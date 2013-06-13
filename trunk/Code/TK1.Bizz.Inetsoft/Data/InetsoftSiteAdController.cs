using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TK1.Bizz.Inetsoft.Data.Extension;
//using TK1.Bizz.Inetsoft.Xml;
using TK1.Utility;
using TK1.Data;
using TK1.Data.Controller;
using TK1.Bizz.Inetsoft.Rent.Xml;
//using TK1.Bizz.Inetsoft.Rent.Data;
using System.Threading;
using TK1.Data.Converter;
using TK1.Data.Bizz.Model;
using TK1.Data.Bizz.Controller;
using TK1.Data.Bizz.Presentation;

namespace TK1.Bizz.Inetsoft.Data.Controller
{
    public class InetsoftSiteAdController : BizzBaseController
    {
        #region PRIVATE MEMBERS
        private AuditController audit;
        private static string customerCodeName = "pieta";

        #endregion

        public InetsoftSiteAdController()
        {
            audit = new AuditController(AppNames.IntegraInetsoftRent.ToString(), CustomerNames.Inetsoft.ToString());
        }
        public InetsoftSiteAdController(AuditController audit)
        {
            this.audit = audit;
        }

        public void AddRentSiteAds(XmlSiteFile xmlSiteFile)
        {
            try
            {
                if (xmlSiteFile != null)
                {
                    removeCustomerSiteAds(customerCodeName);

                    foreach (var xmlSiteAd in xmlSiteFile.Sites)
                    {
                        SiteAd siteAd = new SiteAd()
                        {
                            AreaDescription = StringHelper.ConvertCaseString(xmlSiteAd.AreaDescription.Trim(), StringHelper.UpperCase.UpperFirstParagraph),
                            CategoryName = "",
                            CityName = StringHelper.ConvertCaseString(xmlSiteAd.City.Trim(), StringHelper.UpperCase.UpperFirstWord),
                            CityTaxes = xmlSiteAd.CityTaxes,
                            CondoTaxes = xmlSiteAd.CondoTaxes,
                            DistrictName = StringHelper.ConvertCaseString(xmlSiteAd.District.Trim(), StringHelper.UpperCase.UpperFirstWord),
                            ExternalArea = xmlSiteAd.ExternalArea,
                            FullDescription = StringHelper.ConvertCaseString(xmlSiteAd.InternetDescription.Trim(), StringHelper.UpperCase.UpperFirstParagraph),
                            InternalArea = xmlSiteAd.InternalArea,
                            SiteAdTypeID = (int)SiteAdTypes.Rent,
                            SiteTypeName = StringHelper.ConvertCaseString(xmlSiteAd.SiteType.Trim(), StringHelper.UpperCase.UpperFirstWord),
                            CondoDescription = StringHelper.ConvertCaseString(xmlSiteAd.CondDescription.Trim(), StringHelper.UpperCase.UpperFirstParagraph),
                            CustomerCodename = customerCodeName,
                            Value = xmlSiteAd.Value,
                            SiteAdID = xmlSiteAd.AdCode,
                            ShortDescription = StringHelper.ConvertCaseString(xmlSiteAd.ShortDescription.Trim(), StringHelper.UpperCase.UpperFirstParagraph)
                        };
                        Entities.AddToSiteAds(siteAd);
                        addSiteAdPictures(siteAd, xmlSiteAd.Pictures);
                        addSiteAdDetails(siteAd, xmlSiteAd.Details);
                        Entities.SaveChanges();

                        Thread.Sleep(10);
                    }
                }
            }



            catch (Exception exception)
            {
                audit.WriteException("SiteController.AddSiteAds", exception);
            }
        }

        public void CheckDataIntegrity(XmlSiteFile xmlSiteFile)
        {
            try
            {
                if (xmlSiteFile != null)
                {
                    //var categories = (from el in xmlSiteFile.Sites
                    //                  select el.Category).Distinct().ToList();
                    //checkCategories(categories);

                    //var siteTypes = (from el in xmlSiteFile.Sites
                    //                 select el.SiteType).Distinct().ToList();
                    //checkSiteTypes(siteTypes);


                    //var cities = (from el in xmlSiteFile.Sites
                    //              select el.City).Distinct().ToList();
                    //checkCities(cities);

                    //var districts = (from el in xmlSiteFile.Sites
                    //                 select el.District).Distinct().ToList();
                    //checkDistricts(districts);

                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.CheckDataIntegrity", exception);
            }
        }
        public void CleanUpDatabase()
        {
            try
            {
                //clearUnusedSiteChildren();
                //clearUnusedSites();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.CleanUpDatabase", exception);
            }
        }
        public string GetXmlRentLoadEmail(string clientAcronym)
        {
            string result = string.Empty;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.BizzAcronym == clientAcronym).FirstOrDefault();
                if (customerData != null)
                    result = customerData.DataLoadEmail;
            }
            catch (Exception exception)
            {
                audit.WriteException("InetsoftSiteAdController.GetXmlRentLoadEmail", exception);
            }
            return result;
        }

        private void addSiteAdDetails(SiteAd siteAd, Collection.StringDictionary details)
        {
            if (details != null)
            {
                foreach (var description in details)
                {

                    SiteAdDetail siteAdDetail = new SiteAdDetail()
                    {
                        Description = description.Key,
                        Value = description.Value,
                        SiteAd = siteAd
                    };
                    Entities.AddToSiteAdDetails(siteAdDetail);
                }
            }
        }
        private void addSiteAdPictures(SiteAd siteAd, List<XmlSitePic> pictures)
        {
            if (pictures != null)
            {
                foreach (var pic in pictures)
                {
                    SiteAdPic siteAdPic = new SiteAdPic()
                    {
                        Description = pic.Description,
                        FileName = pic.FileName,
                        SiteAd = siteAd,
                        PicID = pic.Index
                    };
                    Entities.AddToSiteAdPics(siteAdPic);
                }
            }
        }
        private void removeCustomerSiteAds(string customerCodeName)
        {
            try
            {
                foreach (var item in Entities.SiteAdPics.Where(o => o.SiteAd.CustomerCodename == customerCodeName).ToList())
                    Entities.DeleteObject(item);
                foreach (var item in Entities.SiteAdDetails.Where(o => o.SiteAd.CustomerCodename == customerCodeName).ToList())
                    Entities.DeleteObject(item);

                foreach (var item in Entities.SiteAds.Where(o => o.CustomerCodename == customerCodeName).ToList())
                    Entities.DeleteObject(item);
                Entities.SaveChanges();
            }
            catch (Exception exception)
            {
                audit.WriteException("InetsoftSiteAdController.removeCustomerSiteAds", exception);
            }
        }


    }
}
