using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Data.Extension;
using TK1.Bizz.Mdo.Xml;
using TK1.Utility;
using TK1.Data;
using TK1.Data.Controller;
using TK1.Bizz.Mdo.Selling.Xml;
using TK1.Bizz.Mdo.Selling.Data;
using System.Threading;
using TK1.Data.Converter;
using TK1.Data.Bizz.Presentation;

namespace TK1.Bizz.Mdo.Data.Controller
{
    public class MdoSiteAdController : MdoBaseController
    {
        #region PRIVATE MEMBERS
        private AuditController audit;

        #endregion

        public MdoSiteAdController()
        {
            audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Mdo.ToString());
        }
        public MdoSiteAdController(AuditController audit)
        {
            this.audit = audit;
        }

        public void AddSalesSiteAds(XmlSiteFile xmlSiteFile)
        {
            try
            {
                if (xmlSiteFile != null)
                {
                    Customer customer = getCustomer(xmlSiteFile);
                    if (customer != null)
                    {
                        removeCustomerSiteAds(customer);
                        foreach (var xmlSiteAd in xmlSiteFile.Sites.ToList())
                        {
                            SiteType siteType = Entities.SiteTypes.Where(o => o.Name == xmlSiteAd.SiteType).FirstOrDefault();
                            City city = Entities.Cities.Where(o => o.Name == xmlSiteAd.City).FirstOrDefault();
                            District district = Entities.Districts.Where(o => o.Name == xmlSiteAd.District).FirstOrDefault();

                            if (city != null & district != null & siteType != null)
                            {
                                siteType.CategoryReference.Load();
                                Category category = siteType.Category;
                                Site site = new Site()
                                {
                                    City = city,
                                    District = district,
                                    ExternalArea = xmlSiteAd.MinExternalArea,
                                    InternalArea = xmlSiteAd.MinInternalArea,
                                    TotalArea = xmlSiteAd.MinTotalArea,
                                    TotalRooms = xmlSiteAd.MinTotalRooms,
                                    SiteType = siteType
                                };
                                addSitePictures(site, xmlSiteAd.Pictures);
                                addSiteDetails(site, xmlSiteAd.Details);
                                SiteAd siteAd = new SiteAd()
                                {
                                    AreaDescription = xmlSiteAd.AreaDescription,
                                    Category = category,
                                    CondDescription = xmlSiteAd.CondDescription,
                                    Customer = customer,
                                    Description = xmlSiteAd.InternetDescription,
                                    IsFeatured = xmlSiteAd.IsFeatured,
                                    Price = xmlSiteAd.MinValue,
                                    Site = site,
                                    SiteAdID = xmlSiteAd.AdCode,
                                    ShortDescription = xmlSiteAd.ShortDescription
                                };
                                Entities.AddToSiteAds(siteAd);
                                Entities.SaveChanges();

                            }
                            Thread.Sleep(10);
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.AddSiteAds", exception);
            }
        }
        public void AddSalesSiteReleaseAds(XmlSiteFile xmlSiteFile)
        {
            try
            {
                if (xmlSiteFile != null)
                {
                    Customer customer = getCustomer(xmlSiteFile);
                    if (customer != null)
                    {
                        if (xmlSiteFile.SiteReleases.Count > 0)
                        {
                            removeCustomerSiteReleaseAds(customer);
                            foreach (var xmlSiteAd in xmlSiteFile.SiteReleases)
                            {
                                SiteType siteType = Entities.SiteTypes.Where(o => o.Name == xmlSiteAd.SiteType).FirstOrDefault();
                                City city = Entities.Cities.Where(o => o.Name == xmlSiteAd.City).FirstOrDefault();
                                District district = Entities.Districts.Where(o => o.Name == xmlSiteAd.District).FirstOrDefault();

                                if (city != null & district != null & siteType != null)
                                {
                                    siteType.CategoryReference.Load();
                                    Category category = siteType.Category;
                                    Site site = new Site()
                                    {
                                        City = city,
                                        District = district,
                                        ExternalArea = xmlSiteAd.MinExternalArea,
                                        InternalArea = xmlSiteAd.MinInternalArea,
                                        TotalArea = xmlSiteAd.MinTotalArea,
                                        TotalRooms = xmlSiteAd.MinTotalRooms,
                                        SiteType = siteType
                                    };
                                    addSitePictures(site, xmlSiteAd.Pictures);
                                    addSiteDetails(site, xmlSiteAd.Details);
                                    SiteReleaseAd siteReleaseAd = new SiteReleaseAd()
                                    {
                                        Address = xmlSiteAd.Address,
                                        AddressComplement = xmlSiteAd.AddressComplement,
                                        AddressNumber = StringConverter.ToInt(xmlSiteAd.AddressNumber),
                                        AreaDescription = xmlSiteAd.AreaDescription,
                                        Category = category,
                                        CondoDescription = xmlSiteAd.CondDescription,
                                        ConstructorName = xmlSiteAd.ConstructorName,
                                        Customer = customer,
                                        FullDescription = xmlSiteAd.AdText,
                                        MinExternalArea = xmlSiteAd.MinExternalArea,
                                        MinInternalArea = xmlSiteAd.MinInternalArea,
                                        MinGarageNumber = xmlSiteAd.MinParkingLots,
                                        MinSuites = xmlSiteAd.MinSuites,
                                        MinTotalArea = xmlSiteAd.MinTotalArea,
                                        MinTotalRooms = xmlSiteAd.MinTotalRooms,
                                        MinValue = xmlSiteAd.MinValue,
                                        MaxExternalArea = xmlSiteAd.MaxInternalArea,
                                        MaxGarageNumber = xmlSiteAd.MaxParkingLots,
                                        MaxInternalArea = xmlSiteAd.MaxInternalArea,
                                        MaxSuites = xmlSiteAd.MaxSuites,
                                        MaxTotalArea = xmlSiteAd.MaxTotalArea,
                                        MaxTotalRooms = xmlSiteAd.MaxTotalRooms,
                                        MaxValue = xmlSiteAd.MaxValue,
                                        Name = xmlSiteAd.Name,
                                        Site = site,
                                        SiteReleaseAdID = xmlSiteAd.AdCode,
                                        ShortDescription = xmlSiteAd.ShortAdText
                                    };
                                    Entities.AddToSiteReleaseAds(siteReleaseAd);
                                    Entities.SaveChanges();

                                }
                                Thread.Sleep(10);
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.AddSalesSiteReleaseAds", exception);
            }
        }
        public void CheckDataIntegrity(XmlSiteFile xmlSiteFile)
        {
            try
            {
                if (xmlSiteFile != null)
                {
                    var categories = (from el in xmlSiteFile.Sites
                                      select el.Category).Distinct().ToList();
                    checkCategories(categories);

                    var siteTypes = (from el in xmlSiteFile.Sites
                                     select el.SiteType).Distinct().ToList();
                    checkSiteTypes(siteTypes);


                    var cities = (from el in xmlSiteFile.Sites
                                  select el.City).Distinct().ToList();
                    checkCities(cities);
                    cities = (from el in xmlSiteFile.SiteReleases
                              select el.City).Distinct().ToList();
                    checkCities(cities);

                    var districts = (from el in xmlSiteFile.Sites
                                     select el.District).Distinct().ToList();
                    checkDistricts(districts);
                    districts = (from el in xmlSiteFile.SiteReleases
                                 select el.District).Distinct().ToList();
                    checkDistricts(districts);
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
                clearUnusedSiteChildren();
                clearUnusedSites();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.CleanUpDatabase", exception);
            }
        }
        public List<string> GetCities()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.Cities
                          select o.Name).ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetCities(string clientAcronym)
        {
            List<string> result = new List<string>();
            try
            {

                int customerID = GetCustomerID(clientAcronym);
                var customer = (from o in Entities.Customers
                                where o.CustomerID == customerID
                                select o).FirstOrDefault();
                if (customer != null)
                {
                    result = (from o in customer.SiteAds
                                          select o.Site.CityName).Distinct().ToList();
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public int GetCustomerID(string clientAcronym)
        {
            int result = 0;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.MdoAcronym == clientAcronym).FirstOrDefault();
                if (customerData != null)
                    result = customerData.CustomerID;

            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetCustomerID", exception);
            }
            return result;
        }
        public int GetCustomerID(int mdoCode)
        {
            int result = 0;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.MdoCode == mdoCode).FirstOrDefault();
                if (customerData != null)
                    result = customerData.CustomerID;
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetCustomerID", exception);
            }
            return result;
        }
        public List<string> GetDistricts()
        {
            List<string> result = new List<string>();
            try
            {

                result = (from o in Entities.Districts
                          select o.Name).ToList();

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetDistricts(string clientAcronym)
        {
            List<string> result = new List<string>();
            try
            {

                int customerID = GetCustomerID(clientAcronym);
                var customer = (from o in Entities.Customers
                                where o.CustomerID == customerID
                                select o).FirstOrDefault();
                if (customer != null)
                {
                    result = (from o in customer.SiteAds
                              select o.Site.DistrictName).Distinct().ToList();
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetDistricts", exception);
            }
            return result;
        }
        public List<SiteAdView> GetFeaturedSiteAds(string mdoAcronym)
        {
            return GetFeaturedSiteAds(mdoAcronym, 5);
        }
        public List<SiteAdView> GetFeaturedSiteAds(string mdoAcronym, int count)
        {
            List<SiteAdView> result = new List<SiteAdView>();
            try
            {
                var customerID = Entities.CustomerDatas.Where(o => o.MdoAcronym == mdoAcronym).Select(o => o.CustomerID).FirstOrDefault();
                var query = Entities.SiteAds.Where(o => o.Customer.CustomerID == customerID & o.IsFeatured == true).Take(count);
                //var list = query.ToList();
                foreach (var siteAd in query)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    siteAd.CategoryReference.Load();
                    if (siteAd.Category.Name != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteAd.SiteReference.Load();
                    if (siteAd.Site != null)
                    {
                        //ad.Site.AddressInfoReference.Load();
                        siteAd.Site.CityReference.Load();
                        siteAd.Site.DistrictReference.Load();
                        siteAd.Site.SiteDescriptions.Load();
                        siteAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteAd.Site.SitePics.Count > 0)
                            mainPicName = siteAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                        siteAd.Site.SiteTypeReference.Load();

                        SiteAdView siteAdView = new SiteAdView()
                        {
                            AdCategory = siteCategory,
                            AdType = SiteAdTypes.Sell,
                            Code = siteAd.SiteAdID,
                            District = siteAd.Site.DistrictName,
                            MainPicUrl = mainPicName,
                            SiteInternalArea = (float)siteAd.Site.InternalArea,
                            SiteTotalArea = (float)siteAd.Site.TotalArea,
                            SiteTotalRooms = siteAd.Site.TotalRooms,
                            SiteType = siteAd.Site.SiteType.Name,
                            SiteTypeRoomName = siteAd.Site.SiteType.RoomDisplayName,
                            Value = (float)(siteAd.Price.HasValue ? siteAd.Price.Value : 0)

                        };
                        result.Add(siteAdView);
                    }
                }

            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteAdController.GetFeaturedSiteAds", exception);
            }
            return result;
        }
        public int GetMdoCode(string clientAcronym)
        {
            int result = 0;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.MdoAcronym == clientAcronym).FirstOrDefault();
                if (customerData != null)
                    result = customerData.MdoCode;
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetFeaturedSiteAds", exception);
            }
            return result;
        }
        public int GetMdoCode(int customerID)
        {
            int result = 0;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.CustomerID == customerID).FirstOrDefault();
                if (customerData != null)
                    result = customerData.MdoCode;
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetMdoCode", exception);
            }
            return result;
        }
        public NavData GetNavData(int customerID)
        {
            NavData result = new NavData();
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.CustomerID == customerID).FirstOrDefault();
                if (customerData != null)
                {
                    result.LogoUrl = customerData.NavLogoUrl;
                    result.ContactMail = customerData.NavContactMail;
                    result.ContactPhone = customerData.NavContactPhone;
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetNavData", exception);
            }
            return result;
        }
        public SiteAd GetSiteAd(int customerID, int siteAdID)
        {
            SiteAd result = null;
            try
            {
                var siteAd = Entities.SiteAds.Get(customerID, siteAdID);
                if (siteAd != null)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    siteAd.CategoryReference.Load();
                    if (siteAd.Category.Name != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteAd.SiteReference.Load();
                    if (siteAd.Site != null)
                    {
                        //ad.Site.AddressInfoReference.Load();
                        siteAd.Site.CityReference.Load();
                        siteAd.Site.DistrictReference.Load();
                        siteAd.Site.SiteDescriptions.Load();
                        siteAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteAd.Site.SitePics.Count > 0)
                            mainPicName = siteAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                        siteAd.Site.SiteTypeReference.Load();

                        result = siteAd;
                    }
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public SiteAdView GetSiteAdView(int customerID, int siteAdID)
        {
            SiteAdView result = null;
            try
            {
                var siteAd = Entities.SiteAds.Get(customerID, siteAdID);
                if (siteAd != null)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    siteAd.CategoryReference.Load();
                    if (siteAd.Category.Name != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteAd.SiteReference.Load();
                    if (siteAd.Site != null)
                    {
                        //ad.Site.AddressInfoReference.Load();
                        siteAd.Site.CityReference.Load();
                        siteAd.Site.DistrictReference.Load();
                        siteAd.Site.SiteDescriptions.Load();
                        siteAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteAd.Site.SitePics.Count > 0)
                            mainPicName = siteAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                        siteAd.Site.SiteTypeReference.Load();

                        result = new SiteAdView()
                        {
                            AdCategory = siteCategory,
                            AdType = SiteAdTypes.Sell,
                            AdTypeName = "Venda",
                            AreaDescription = siteAd.AreaDescription,
                            City = siteAd.Site.CityName,
                            Code = siteAd.SiteAdID,
                            CondoDescription = siteAd.CondDescription,
                            CustomerID = customerID,
                            District = siteAd.Site.DistrictName,
                            FullDescription = siteAd.Description,
                            MainPicUrl = mainPicName,
                            SiteInternalArea = (float)siteAd.Site.InternalArea,
                            SiteTotalArea = (float)siteAd.Site.TotalArea,
                            SiteTotalRooms = siteAd.Site.TotalRooms,
                            SiteType = siteAd.Site.SiteType.Name,
                            SiteTypeRoomName = siteAd.Site.SiteType.RoomDisplayName,
                            ShortDescription = siteAd.ShortDescription,
                            Value = (float)(siteAd.Price.HasValue ? siteAd.Price.Value : 0)

                        };
                    }
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public List<SiteDetail> GetSiteDetail(int customerID, int siteAdID)
        {
            List<SiteDetail> result = new List<SiteDetail>();
            try
            {
                var siteAd = Entities.SiteAds.Get(customerID, siteAdID);
                if (siteAd != null)
                {
                    siteAd.SiteReference.Load();
                    if (siteAd.Site != null)
                        siteAd.Site.SiteDescriptions.Load();
                    if (siteAd.Site.SiteDescriptions != null)
                    {
                        var descriptions = siteAd.Site.SiteDescriptions;
                        var query = from o in descriptions
                                    //where o.Value.ToUpper() == "SIM"
                                    select o;
                        foreach (var detail in query)
                        {
                            result.Add(new SiteDetail()
                            {
                                Name = detail.Value + " " + detail.Description,
                                Value = detail.Value,
                                ImageUrl = "Dot.png"
                            });
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteDetail", exception);
            }
            return result;
        }
        public string GetSitePicDescription(string fileName)
        {
            string result = null;
            if (fileName != null)
            {
                try
                {
                    result = (from o in Entities.SitePics
                              where o.FileName == fileName
                              select o.Description).FirstOrDefault();

                }
                catch (Exception exception)
                {
                    audit.WriteException("SiteController.GetSitePicDescription", exception);
                }
            }
            return result;
        }
        public List<SiteAdPicView> GetSitePics(int customerID, int siteAdID)
        {
            List<SiteAdPicView> result = new List<SiteAdPicView>();
            try
            {
                var siteAd = Entities.SiteAds.Get(customerID, siteAdID);
                if (siteAd != null)
                {
                    siteAd.SiteReference.Load();
                    if (siteAd.Site != null)
                        siteAd.Site.SitePics.Load();
                    if (siteAd.Site.SitePics != null)
                    {
                        var query = from o in siteAd.Site.SitePics
                                    select o;
                        foreach (var item in query)
                        {
                            result.Add(new SiteAdPicView()
                            {
                                Description = item.Description,
                                FileName = item.FileName,
                                Index = item.PicID
                            });
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteDetail", exception);
            }
            return result;
        }

        public SiteReleaseAd GetSiteReleaseAd(int customerID, int siteReleaseAdID)
        {
            SiteReleaseAd result = null;
            try
            {
                var siteReleaseAd = Entities.SiteReleaseAds.Get(customerID, siteReleaseAdID);
                if (siteReleaseAd != null)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    siteReleaseAd.CategoryReference.Load();
                    if (siteReleaseAd.Category.Name != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteReleaseAd.SiteReference.Load();
                    if (siteReleaseAd.Site != null)
                    {
                        //ad.Site.AddressInfoReference.Load();
                        siteReleaseAd.Site.CityReference.Load();
                        siteReleaseAd.Site.DistrictReference.Load();
                        siteReleaseAd.Site.SiteDescriptions.Load();
                        siteReleaseAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteReleaseAd.Site.SitePics.Count > 0)
                            mainPicName = siteReleaseAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                        siteReleaseAd.Site.SiteTypeReference.Load();

                        result = siteReleaseAd;
                    }
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public SiteReleaseAdView GetSiteReleaseAdView(int customerID, int siteReleaseAdID)
        {
            SiteReleaseAdView result = null;
            try
            {
                var siteReleaseAd = Entities.SiteReleaseAds.Get(customerID, siteReleaseAdID);
                if (siteReleaseAd != null)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    siteReleaseAd.CategoryReference.Load();
                    if (siteReleaseAd.Category.Name != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteReleaseAd.SiteReference.Load();
                    if (siteReleaseAd.Site != null)
                    {
                        //ad.Site.AddressInfoReference.Load();
                        siteReleaseAd.Site.CityReference.Load();
                        siteReleaseAd.Site.DistrictReference.Load();
                        siteReleaseAd.Site.SiteDescriptions.Load();
                        siteReleaseAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteReleaseAd.Site.SitePics.Count > 0)
                            mainPicName = siteReleaseAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                        siteReleaseAd.Site.SiteTypeReference.Load();

                        result = new SiteReleaseAdView()
                        {
                            AdCategory = siteCategory,
                            AdType = SiteAdTypes.Sell,
                            AdTypeName = "Venda",
                            AreaDescription = siteReleaseAd.AreaDescription,
                            City = siteReleaseAd.Site.CityName,
                            Code = siteReleaseAd.SiteReleaseAdID,
                            CondoDescription = siteReleaseAd.CondoDescription,
                            District = siteReleaseAd.Site.DistrictName,
                            FullDescription = siteReleaseAd.FullDescription,
                            MainPicUrl = mainPicName,
                            SiteInternalArea = (float)siteReleaseAd.MaxInternalArea,
                            SiteMinInternalArea = (float)siteReleaseAd.MinInternalArea,
                            SiteTotalArea = (float)siteReleaseAd.MaxTotalArea,
                            SiteTotalRooms = siteReleaseAd.MaxTotalRooms,
                            SiteMinTotalRooms = siteReleaseAd.MinTotalRooms,
                            SiteType = siteReleaseAd.Site.SiteType.Name,
                            SiteTypeRoomName = siteReleaseAd.Site.SiteType.RoomDisplayName,
                            ShortDescription = siteReleaseAd.ShortDescription,
                            Value = (float)(siteReleaseAd.MinValue)

                        };
                        setReleaseText(result);

                    }
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public List<SiteReleaseAdView> GetSiteReleaseAds(int customerID)
        {
            List<SiteReleaseAdView> result = new List<SiteReleaseAdView>();
            try
            {
                var query = Entities.SiteReleaseAds.Where(o => o.Customer.CustomerID == customerID);
                var list = query.ToList();
                foreach (var siteReleaseAd in query)
                {
                    siteReleaseAd.CategoryReference.Load();
                    siteReleaseAd.SiteReference.Load();
                    if (siteReleaseAd.Site != null)
                    {
                        siteReleaseAd.Site.SiteTypeReference.Load();
                        var siteCategory = SiteAdCategories.Residencial;
                        if (siteReleaseAd.Category.Name != SiteAdCategories.Residencial.ToString())
                            siteCategory = SiteAdCategories.Comercial;
                        //siteAd.SiteAdDetails.Load();
                        siteReleaseAd.Site.SitePics.Load();
                        string mainPicName = string.Empty;
                        if (siteReleaseAd.Site.SitePics.Count > 0)
                            mainPicName = siteReleaseAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;

                        SiteReleaseAdView siteReleaseAdView = new SiteReleaseAdView()
                        {
                            AdCategory = siteCategory,
                            AdType = SiteAdTypes.Sell,
                            Code = siteReleaseAd.SiteReleaseAdID,
                            District = siteReleaseAd.Site.DistrictName,
                            MainPicUrl = mainPicName,
                            Name = siteReleaseAd.Name,
                            ShortDescription = siteReleaseAd.ShortDescription,
                            SiteInternalArea = (float)siteReleaseAd.MaxInternalArea,
                            SiteTotalArea = (float)siteReleaseAd.MaxTotalArea,
                            SiteMinInternalArea = (float)siteReleaseAd.MinInternalArea,
                            SiteMinTotalArea = (float)siteReleaseAd.MinTotalArea,
                            SiteTotalRooms = siteReleaseAd.MaxTotalRooms,
                            SiteType = siteReleaseAd.Site.SiteType.Name,
                            SiteMinTotalRooms = siteReleaseAd.MinTotalRooms,
                            //SiteTypeRoomName = siteAd.SiteType.RoomDisplayName,
                            Value = (float)(siteReleaseAd.MinValue)

                        };
                        setReleaseText(siteReleaseAdView);
                        result.Add(siteReleaseAdView);
                    }
                }
            }



            catch (Exception exception)
            {
                audit.WriteException("MdoSiteAdController.GetSiteReleaseAds", exception);
            }
            return result;
        }

        public List<string> GetSiteTypes(string category)
        {
            List<string> result = null;
            try
            {
                result = (from o in Entities.SiteTypes
                          where o.Category.Name == category
                          select o.Name).ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteTypes", exception);
            }
            if (result == null)
                result = new List<string>();
            return result;
        }
        public string GetXmlSellingLoadEmail(string clientAcronym)
        {
            string result = string.Empty;
            try
            {
                var customerData = Entities.CustomerDatas.Where(o => o.MdoAcronym == clientAcronym).FirstOrDefault();
                if (customerData != null)
                    result = customerData.XmlSellingEmail;
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteController.GetXmlSellingLoadEmail", exception);
            }
            return result;
        }
        public List<SiteAdView> SearchSites(MdoSiteAdSearchParameters parameters)
        {
            List<SiteAdView> result = new List<SiteAdView>();
            try
            {
                if (parameters != null)
                {
                    var customerID = Entities.CustomerDatas.Where(o => o.MdoAcronym == parameters.CustomerCodename).Select(o => o.CustomerID).FirstOrDefault();
                    var query = Entities.SiteAds.Where(o => o.Customer.CustomerID == customerID);
                    var list = query.ToList();
                    if (parameters.Code > 0)
                    {
                        query = query.FilterCode(parameters.Code);
                    }
                    else
                    {
                        query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                        list = query.ToList();
                        query = query.FilterCategory(parameters.Category);
                        list = query.ToList();
                        query = query.FilterCity(parameters.CityName);
                        list = query.ToList();
                        query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                        list = query.ToList();
                        query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                        list = query.ToList();
                        if (parameters.SiteType != "*")
                            query = query.FilterSiteType(parameters.Category, parameters.SiteType);
                        if (!parameters.Districts.Contains("Todos"))
                            query = query.FilterDistrict(parameters.Districts);


                    }
                    foreach (var siteAd in query)
                    {
                        var siteCategory = SiteAdCategories.Residencial;
                        siteAd.CategoryReference.Load();
                        if (siteAd.Category.Name != SiteAdCategories.Residencial.ToString())
                            siteCategory = SiteAdCategories.Comercial;
                        siteAd.SiteReference.Load();
                        if (siteAd.Site != null)
                        {
                            //ad.Site.AddressInfoReference.Load();
                            siteAd.Site.CityReference.Load();
                            siteAd.Site.DistrictReference.Load();
                            siteAd.Site.SiteDescriptions.Load();
                            siteAd.Site.SitePics.Load();
                            string mainPicName = string.Empty;
                            if (siteAd.Site.SitePics.Count > 0)
                                mainPicName = siteAd.Site.SitePics.OrderBy(o => o.PicID).FirstOrDefault().FileName;
                            siteAd.Site.SiteTypeReference.Load();

                            SiteAdView siteAdView = new SiteAdView() {
                                AdCategory = siteCategory,
                                AdType = SiteAdTypes.Sell,
                                Code = siteAd.SiteAdID,
                                CustomerID = customerID,
                                District = siteAd.Site.DistrictName,
                                MainPicUrl = mainPicName,
                                SiteInternalArea = (float)siteAd.Site.InternalArea,
                                SiteTotalArea = (float)siteAd.Site.TotalArea,
                                SiteTotalRooms = siteAd.Site.TotalRooms,
                                SiteType = siteAd.Site.SiteType.Name,
                                SiteTypeRoomName = siteAd.Site.SiteType.RoomDisplayName,
                                Value = (float)(siteAd.Price.HasValue ? siteAd.Price.Value : 0)

                            };
                            result.Add(siteAdView);
                        }
                    }
                    if (parameters.ResultOrdering != SiteAdSearchResultOrders._Undefined)
                        result = OrderResults(result, parameters.ResultOrdering);
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.SearchSites", exception);
            }
            return result;
        }

        #region SYNC METHODS
        public List<SiteAd> GetSiteAdsToSync(string clientAcronym)
        {
            var result = new List<SiteAd>();
            try
            {
                int customerID = GetCustomerID(clientAcronym);
                var customer = (from o in Entities.Customers
                                where o.CustomerID == customerID
                                select o).FirstOrDefault();
                if (customer != null)
                {
                    var siteAds = (from o in customer.SiteAds
                                   select o).ToList();


                    foreach (var siteAd in siteAds)
                    {
                        siteAd.SiteReference.Load();
                        siteAd.Site.SiteDescriptions.Load();
                        siteAd.Site.SitePics.Load();
                        siteAd.Site.SiteTypeReference.Load();
                    }
                    result = siteAds;
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("MdoSiteAdController.GetSiteAdToSync", exception);
            }
            return result;
        }

        #endregion


        public static List<SiteAd> OrderResults(List<SiteAd> siteAds, SiteAdSearchResultOrders resultOrder)
        {
            List<SiteAd> result = null;
            if (siteAds != null & resultOrder != SiteAdSearchResultOrders._Undefined)
            {
                try
                {
                    switch (resultOrder)
                    {
                        case SiteAdSearchResultOrders.AreaAscending:
                            result = siteAds.OrderBy(o => o.Site.TotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.AreaDescending:
                            result = siteAds.OrderByDescending(o => o.Site.TotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceAscending:
                            result = siteAds.OrderBy(o => o.Price).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceDescending:
                            result = siteAds.OrderByDescending(o => o.Price).ToList();
                            break;
                    }
                }
                catch
                {
                    result = siteAds;
                }
            }
            if (result == null)
                result = new List<SiteAd>();
            return result;
        }
        public static List<SiteAdView> OrderResults(List<SiteAdView> siteAds, SiteAdSearchResultOrders resultOrder)
        {
            List<SiteAdView> result = null;
            if (siteAds != null & resultOrder != SiteAdSearchResultOrders._Undefined)
            {
                try
                {
                    switch (resultOrder)
                    {
                        case SiteAdSearchResultOrders.AreaAscending:
                            result = siteAds.OrderBy(o => o.SiteTotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.AreaDescending:
                            result = siteAds.OrderByDescending(o => o.SiteTotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceAscending:
                            result = siteAds.OrderBy(o => o.Value).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceDescending:
                            result = siteAds.OrderByDescending(o => o.Value).ToList();
                            break;
                    }
                }
                catch
                {
                    result = siteAds;
                }
            }
            if (result == null)
                result = new List<SiteAdView>();
            return result;
        }


        private static void addSiteDetails(Site site, Collection.StringDictionary details)
        {
            if (details != null)
            {
                foreach (var description in details)
                {
                    SiteDescription siteDescription = new SiteDescription()
                    {
                        Description = description.Key,
                        Value = description.Value,
                        Site = site
                    };
                }
            }
        }
        private void addSitePictures(Site site, List<XmlSitePic> pictures)
        {
            if (pictures != null)
            {
                foreach (var pic in pictures)
                {
                    SitePic sitePic = new SitePic()
                    {
                        Description = pic.Description,
                        FileName = pic.FileName,
                        Site = site,
                        PicID = pic.Index
                    };
                    Entities.AddToSitePics(sitePic);
                }
            }
        }
        private void checkCategories(List<string> categories)
        {
            if (categories != null)
            {
                foreach (var item in categories)
                {
                    string categoryName = string.Empty;
                    string roomDisplayName = string.Empty;
                    switch (item)
                    {
                        case "C":
                            categoryName = "Comercial";
                            break;
                        case "I":
                            categoryName = "Industrial";
                            break;
                        case "R":
                            categoryName = "Residencial";
                            roomDisplayName = "dormitório(s)";
                            break;
                    }
                    if (!string.IsNullOrEmpty(categoryName))
                    {
                        Category category = Entities.Categories.Where(o => o.Name == categoryName).FirstOrDefault();
                        if (category == null)
                        {
                            category = new Category()
                            {
                                Name = categoryName,
                                Caption = categoryName,
                                Description = categoryName,
                                CustomData = roomDisplayName
                            };
                            Entities.AddToCategories(category);
                            Entities.SaveChanges();
                        }
                    }
                }
            }
        }
        private void checkCities(List<string> cities)
        {
            if (cities != null)
            {
                foreach (var item in cities)
                {
                    string cityName = StringHelper.ConvertCaseString(item.Trim(), StringHelper.UpperCase.UpperFirstWord);
                    City city = Entities.Cities.Where(o => o.Name == cityName).FirstOrDefault();
                    if (city == null & !string.IsNullOrEmpty(cityName))
                    {
                        city = new City() { Name = cityName };
                        Entities.AddToCities(city);
                        Entities.SaveChanges();
                    }
                }
            }
        }
        private void checkDistricts(List<string> districts)
        {
            if (districts != null)
            {
                foreach (var item in districts)
                {
                    string districtName = StringHelper.ConvertCaseString(item.Trim(), StringHelper.UpperCase.UpperFirstWord);
                    District district = Entities.Districts.Where(o => o.Name == districtName).FirstOrDefault();
                    if (district == null & !string.IsNullOrEmpty(districtName))
                    {
                        district = new District() { Name = districtName, Region = Entities.Regions.FirstOrDefault() };
                        Entities.AddToDistricts(district);
                        Entities.SaveChanges();
                    }
                }
            }
        }
        private void checkSiteTypes(List<string> siteTypes)
        {
            if (siteTypes != null)
            {
                foreach (var item in siteTypes)
                {
                    string siteTypeName = StringHelper.ConvertCaseString(item.Trim(), StringHelper.UpperCase.UpperFirstWord);
                    SiteType siteType = Entities.SiteTypes.Where(o => o.Name == siteTypeName).FirstOrDefault();
                    if (siteType == null & !string.IsNullOrEmpty(siteTypeName))
                    {
                        string categoryName = string.Empty;
                        string roomDisplayName = string.Empty;
                        //switch (xmlSiteAd.Category)
                        //{
                        //    case "C":
                        //        categoryName = "Comercial";
                        //        break;
                        //    case "I":
                        //        categoryName = "Industrial";
                        //        break;
                        //    case "R":
                        //        categoryName = "Residencial";
                        //        roomDisplayName = "dormitório(s)";
                        //        break;
                        //}
                        Category category = Entities.Categories.Where(o => o.Name == categoryName).FirstOrDefault();
                        if (category == null)
                            category = Entities.Categories.FirstOrDefault();
                        if (category != null)
                        {
                            siteType = new SiteType() { Category = category, Name = siteTypeName, RoomDisplayName = roomDisplayName };
                            Entities.AddToSiteTypes(siteType);
                            Entities.SaveChanges();
                        }

                    }
                }
            }
        }
        private void clearUnusedSites()
        {
            var unusedSites = from o in Entities.Sites
                              where o.SiteAds.Count() == 0 & o.SiteReleaseAds.Count() == 0
                              select o;
            unusedSites.ToList().ForEach(o => Entities.DeleteObject(o));
            Entities.SaveChanges();

        }
        private void clearUnusedSiteChildren()
        {
            var unusedSites = from o in Entities.Sites
                              where o.SiteAds.Count() == 0 & o.SiteReleaseAds.Count() == 0
                              select o;
            foreach (var site in unusedSites)
            {
                site.SitePics.Load();
                var unusedPics = from o in site.SitePics
                                 select o;
                unusedPics.ToList().ForEach(o => Entities.DeleteObject(o));

                site.SiteDescriptions.Load();
                var unusedDescription = from o in site.SiteDescriptions
                                        select o;
                unusedDescription.ToList().ForEach(o => Entities.DeleteObject(o));
            }
            Entities.SaveChanges();
        }
        private Customer getCustomer(XmlSiteFile xmlSiteFile)
        {
            Customer result = null;
            if (xmlSiteFile.Header == null)
            {
                audit.WriteEvent("Cadastro de imóveis sem cabeçalho", "");
            }
            else
            {
                int mdoCode = 0;
                if (int.TryParse(xmlSiteFile.Header.CustomerCode, out mdoCode))
                {
                    var customerData = Entities.CustomerDatas.Where(o => o.MdoCode == mdoCode).FirstOrDefault();
                    if (customerData != null)
                    {
                        customerData.CustomerReference.Load();
                        result = customerData.Customer;
                    }
                }
                if (result == null)
                    audit.WriteEvent("Cliente não cadastrado", xmlSiteFile.Header.CustomerCode);

            }
            return result;
        }
        private void removeCustomerSiteAds(Customer customer)
        {
            customer.SiteAds.Load();
            foreach (var item in customer.SiteAds.ToList())
            {
                Entities.DeleteObject(item);
            }
        }
        private void removeCustomerSiteReleaseAds(Customer customer)
        {
            customer.SiteReleaseAds.Load();
            foreach (var item in customer.SiteReleaseAds.ToList())
            {
                Entities.DeleteObject(item);
            }
        }
        private void setReleaseText(SiteReleaseAdView siteReleaseAdView)
        {
            if (siteReleaseAdView != null)
            {
                if (siteReleaseAdView.Value == siteReleaseAdView.MinValue)
                    siteReleaseAdView.ValueText = string.Format("{0:c}", siteReleaseAdView.Value);
                else
                    siteReleaseAdView.ValueText = string.Format("De {0:c} a {1:c}", siteReleaseAdView.MinValue, siteReleaseAdView.Value);

                if (siteReleaseAdView.SiteInternalArea == siteReleaseAdView.SiteMinInternalArea)
                    siteReleaseAdView.AreaText = string.Format("{0:0.##} m²", siteReleaseAdView.SiteInternalArea);
                else
                    siteReleaseAdView.AreaText = string.Format("De {0:0.##} m² a {1:0.##} m²", siteReleaseAdView.SiteMinInternalArea, siteReleaseAdView.SiteInternalArea);

                if (siteReleaseAdView.SiteMinTotalRooms == siteReleaseAdView.SiteTotalRooms)
                    siteReleaseAdView.RoomText = string.Format("{0} dormitório{1}", siteReleaseAdView.SiteMinTotalRooms, siteReleaseAdView.SiteMinTotalRooms > 1 ? "s" : "");
                else
                    siteReleaseAdView.RoomText = string.Format("De {0} a {1} dormitórios", siteReleaseAdView.SiteMinTotalRooms, siteReleaseAdView.SiteTotalRooms);
            }
        }


        #region OLD
        //public List<SiteAd> _SearchSites(MdoSiteSearchParameters parameters)
        //{
        //    List<SiteAd> result = new List<SiteAd>();
        //    try
        //    {
        //        //var query = entities.SiteAds as IQueryable<SiteAd>;
        //        if (parameters != null)
        //        {
        //            ////query = query.FilterAdType(parameters.AdType);
        //            ////query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
        //            ////query = query.FilterCategory(parameters.Category);
        //            ////query = query.FilterCity(parameters.CityName);
        //            ////query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
        //            ////query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
        //            ////query = query.FilterSiteRegion(parameters.RegionID);
        //            ////query = query.FilterSiteType(parameters.AdType, parameters.SiteType);

        //            var customerID = Entities.CustomerDatas.Where(o => o.MdoAcronym == parameters.MdoAcronym).Select(o => o.CustomerID).FirstOrDefault();
        //            result = Entities.SiteAds.Where(o => o.Customer.CustomerID == customerID).ToList();

        //            foreach (var ad in result)
        //            {
        //                ad.CategoryReference.Load();
        //                ad.SiteReference.Load();
        //                if (ad.Site != null)
        //                {
        //                    //ad.Site.AddressInfoReference.Load();
        //                    ad.Site.CityReference.Load();
        //                    ad.Site.DistrictReference.Load();
        //                    ad.Site.SiteDescriptions.Load();
        //                    ad.Site.SiteTypeReference.Load();
        //                }
        //            }
        //            if (parameters.Code > 0)
        //            {
        //                result = result.FilterCode(parameters.Code);
        //            }
        //            else
        //            {
        //                //query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
        //                result = result.FilterCategory(parameters.Category);
        //                result = result.FilterCity(parameters.CityName);
        //                result = result.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
        //                result = result.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
        //                //query = query.FilterSiteRegion(parameters.RegionID);
        //                if (parameters.SiteType != "*")
        //                    result = result.FilterSiteType(parameters.Category, parameters.SiteType);
        //                if (!parameters.Districts.Contains("Todos"))
        //                    result = result.FilterDistrict(parameters.Districts);
        //            }

        //            if (parameters.ResultOrdering != SiteSearchResultOrders._Undefined)
        //                result = OrderResults(result, parameters.ResultOrdering);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        audit.WriteException("SiteController.SearchSites", exception);
        //    }
        //    return result;
        //} 
        #endregion



    }
}
