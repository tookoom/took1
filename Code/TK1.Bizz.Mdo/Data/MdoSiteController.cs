using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Data.Extension;
using TK1.Bizz.Mdo.Data.Presentation;
using TK1.Bizz.Mdo.Xml;
using TK1.Utility;
using TK1.Data;
using TK1.Data.Controller;
using TK1.Bizz.Mdo.Selling.Xml;
using TK1.Bizz.Mdo.Selling.Data;

namespace TK1.Bizz.Mdo.Data.Controller
{
    public class MdoSiteController : MdoBaseController
    {
        #region PRIVATE MEMBERS
        private AuditController audit;

        #endregion

        public MdoSiteController()
        {
            audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Mdo.ToString());
        }
        public MdoSiteController(AuditController audit)
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
                        foreach (var xmlSiteAd in xmlSiteFile.Sites)
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
                                    ExternalArea = xmlSiteAd.ExternalArea,
                                    InternalArea = xmlSiteAd.InternalArea,
                                    TotalArea = xmlSiteAd.TotalArea,
                                    TotalRooms = xmlSiteAd.RoomNumber,
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
                                    Price = xmlSiteAd.Value,
                                    Site = site,
                                    SiteAdID = xmlSiteAd.SiteCode,
                                    ShortDescription = xmlSiteAd.ShortDescription
                                };
                                Entities.AddToSiteAds(siteAd);
                                Entities.SaveChanges();

                            }
                        }
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
                    var categories = (from el in xmlSiteFile.Sites
                                      select el.Category).Distinct().ToList();
                    checkCategories(categories);

                    var siteTypes = (from el in xmlSiteFile.Sites
                                     select el.SiteType).Distinct().ToList();
                    checkSiteTypes(siteTypes);


                    var cities = (from el in xmlSiteFile.Sites
                                  select el.City).Distinct().ToList();
                    checkCities(cities);

                    var districts = (from el in xmlSiteFile.Sites
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
                audit.WriteException("MdoSiteController.GetMdoCode", exception);
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
                    siteAd.CategoryReference.Load();
                    siteAd.SiteReference.Load();
                    //if (siteAd.Site != null)
                    //    siteAd.Site.AddressInfoReference.Load();
                    if (siteAd.Site != null)
                        siteAd.Site.CityReference.Load();
                    if (siteAd.Site != null)
                        siteAd.Site.DistrictReference.Load();
                    if (siteAd.Site != null)
                        siteAd.Site.SiteTypeReference.Load();
                    if (string.IsNullOrEmpty(siteAd.Description))
                    {
                        if (siteAd.Site != null)
                            siteAd.Site.SiteDescriptions.Load();
                        if (siteAd.Site.SiteDescriptions != null)
                        {
                            var descriptions = siteAd.Site.SiteDescriptions;
                            var query = from o in descriptions
                                        where o.Value.ToUpper() != "SIM"
                                        select o;
                            string text = string.Empty;
                            foreach (var detail in query)
                                text += string.Format("{0} {1}, ", detail.Value, detail.Description);

                            siteAd.Description = text;
                        }
                    }

                    //siteAd.Site.SiteType.Name
                    result = siteAd;
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
        public List<SiteAd> SearchSites(MdoSiteSearchParameters parameters)
        {
            List<SiteAd> result = new List<SiteAd>();
            try
            {
                //var query = entities.SiteAds as IQueryable<SiteAd>;
                if (parameters != null)
                {
                    ////query = query.FilterAdType(parameters.AdType);
                    ////query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                    ////query = query.FilterCategory(parameters.Category);
                    ////query = query.FilterCity(parameters.CityName);
                    ////query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                    ////query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                    ////query = query.FilterSiteRegion(parameters.RegionID);
                    ////query = query.FilterSiteType(parameters.AdType, parameters.SiteType);

                    var customerID = Entities.CustomerDatas.Where(o=>o.MdoAcronym == parameters.MdoAcronym).Select(o=>o.CustomerID).FirstOrDefault();
                    result = Entities.SiteAds.Where(o => o.Customer.CustomerID == customerID).ToList();

                    foreach (var ad in result)
                    {
                        ad.CategoryReference.Load();
                        ad.SiteReference.Load();
                        if (ad.Site != null)
                        {
                            //ad.Site.AddressInfoReference.Load();
                            ad.Site.CityReference.Load();
                            ad.Site.DistrictReference.Load();
                            ad.Site.SiteDescriptions.Load();
                            ad.Site.SiteTypeReference.Load();
                        }
                    }
                    if (parameters.Code > 0)
                    {
                        result = result.FilterCode(parameters.Code);
                    }
                    else
                    {
                        //query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                        result = result.FilterCategory(parameters.Category);
                        result = result.FilterCity(parameters.CityName);
                        result = result.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                        result = result.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                        //query = query.FilterSiteRegion(parameters.RegionID);
                        if (parameters.SiteType != "*")
                            result = result.FilterSiteType(parameters.Category, parameters.SiteType);
                        if (!parameters.Districts.Contains("Todos"))
                            result = result.FilterDistrict(parameters.Districts);
                    }

                    if (parameters.ResultOrdering != MdoSiteSearchResultOrder._Undefined)
                        result = OrderResults(result, parameters.ResultOrdering);
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.SearchSites", exception);
            }
            return result;
        }

        public static List<SiteAd> OrderResults(List<SiteAd> siteAds, MdoSiteSearchResultOrder resultOrder)
        {
            List<SiteAd> result = null;
            if (siteAds != null & resultOrder != MdoSiteSearchResultOrder._Undefined)
            {
                try
                {
                    switch (resultOrder)
                    {
                        case MdoSiteSearchResultOrder.AreaAscending:
                            result = siteAds.OrderBy(o => o.Site.TotalArea).ToList();
                            break;
                        case MdoSiteSearchResultOrder.AreaDescending:
                            result = siteAds.OrderByDescending(o => o.Site.TotalArea).ToList();
                            break;
                        case MdoSiteSearchResultOrder.PriceAscending:
                            result = siteAds.OrderBy(o => o.Price).ToList();
                            break;
                        case MdoSiteSearchResultOrder.PriceDescending:
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

        private static void addSiteDetails(Site site, Collection.StringDictionary details)
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
        private void addSitePictures(Site site, List<XmlSitePic> pictures)
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
                    if (city == null)
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
                    if (district == null)
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
                    if (siteType == null)
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
                              where o.SiteAds.Count() == 0
                              select o;
            unusedSites.ToList().ForEach(o => Entities.DeleteObject(o));
            Entities.SaveChanges();

        }
        private void clearUnusedSiteChildren()
        {
            var unusedSites = from o in Entities.Sites
                              where o.SiteAds.Count() == 0
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


    }
}
