using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Pieta.Data.Extension;
using TK1.Bizz.Pieta.Data.Presentation;
using TK1.Bizz.Pieta.Xml;

namespace TK1.Bizz.Pieta.Data
{
    public class SiteController : BaseController
    {
        public SiteController()
        {
        }

        public static List<string> GetCities()
        {
            List<string> result = new List<string>();
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    result = (from o in entities.Cities
                              select o.Name).ToList();
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public static List<string> GetDistricts()
        {
            List<string> result = new List<string>();
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    result = (from o in entities.Districts
                              select o.Name).ToList();
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public static string GetParameterValue(string name)
        {
            string result = null;
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    var parameter = entities.Parameters.Where(el => el.Name == name).FirstOrDefault();
                    if (parameter != null)
                        result = parameter.Value;
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetParameterValue", exception);
            }
            return result;
        }
        public static SiteAd GetSiteAd(int siteAdID, int siteAdType)
        {
            SiteAd result = null;
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    var siteAd = entities.SiteAds.Get(siteAdID, siteAdType);
                    if (siteAd != null)
                    {
                        siteAd.AdTypeReference.Load();
                        siteAd.CategoryReference.Load();
                        siteAd.SiteReference.Load();
                        if (siteAd.Site != null)
                            siteAd.Site.AddressInfoReference.Load();
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

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public static List<SiteDetail> GetSiteDetail(int siteAdID, int siteAdType)
        {
            List<SiteDetail> result = new List<SiteDetail>();
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Frente", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Porteiro Eletrônico", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Playground", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Algum item que venha descrito e que possua vários caracteres, de forma a forçar a existência de duas linhas na listagem de características do imóvel", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });
                    //result.Add(new SiteDetail() { Name = "Garagem", Value = "Sim", ImageUrl = "Check.png" });

                    var siteAd = entities.SiteAds.Get(siteAdID, siteAdType);
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
                                result.Add(new SiteDetail()
                                {
                                    Name = detail.Value + " " + detail.Description,
                                    Value = detail.Value,
                                    ImageUrl = "Check.png"
                                });
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public static List<string> GetSiteTypes(string category)
        {
            List<string> result = null;
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
                {
                    result = (from o in entities.SiteTypes
                              where o.Category.Name == category
                              select o.Name).ToList();
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.GetSiteTypes", exception);
            }
            return result;
        }
        //public static List<SiteAd> SearchSites_NEW(SiteSearchParameters parameters)
        //{
        //    List<SiteAd> result = new List<SiteAd>();
        //    try
        //    {
        //        using (PietaEntities entities = BaseController.GetPietaEntities())
        //        {
        //            var query = entities.SiteAds as IQueryable<SiteAd>;
        //            if (parameters != null)
        //            {
        //                query = query.FilterAdType(parameters.AdType);
        //                //query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
        //                query = query.FilterCategory(parameters.Category);
        //                query = query.FilterCity(parameters.CityName);
        //                query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
        //                query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
        //                //query = query.FilterSiteRegion(parameters.RegionID);
        //                if (parameters.SiteType != "*")
        //                    query = query.FilterSiteType(parameters.Category, parameters.SiteType);
        //                if (!parameters.Districts.Contains("Todos"))
        //                    query = query.FilterDistrict(parameters.Districts);

        //                result = entities.SiteAds.ToList();

        //                foreach (var ad in result)
        //                {
        //                    ad.AdTypeReference.Load();
        //                    ad.CategoryReference.Load();
        //                    ad.SiteReference.Load();
        //                    if (ad.Site != null)
        //                    {
        //                        ad.Site.AddressInfoReference.Load();
        //                        ad.Site.CityReference.Load();
        //                        ad.Site.DistrictReference.Load();
        //                        ad.Site.SiteDescriptions.Load();
        //                        ad.Site.SiteTypeReference.Load();
        //                    }
        //                    //if (string.IsNullOrEmpty(ad.ImageUrl))
        //                    //    ad.ImageUrl = "Images/PicNotFound.jpg";
        //                }

        //                //result = result.FilterAdType(parameters.AdType);
        //                ////query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
        //                //result = result.FilterCategory(parameters.Category);
        //                //result = result.FilterCity(parameters.CityName);
        //                //result = result.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
        //                //result = result.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
        //                ////query = query.FilterSiteRegion(parameters.RegionID);
        //                //if(parameters.SiteType != "*")
        //                //    result = result.FilterSiteType(parameters.Category, parameters.SiteType);
        //                //if(!parameters.Districts.Contains("Todos"))
        //                //    result = result.FilterDistrict(parameters.Districts);
        //            }
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        LogController.WriteException("SiteController.SearchSites", exception);
        //    }
        //    return result;
        //}
        public static List<SiteAd> SearchSites(SiteSearchParameters parameters)
        {
            List<SiteAd> result = new List<SiteAd>();
            try
            {
                using (PietaEntities entities = BaseController.GetPietaEntities())
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
                        result = entities.SiteAds.ToList();

                        foreach (var ad in result)
                        {
                            ad.AdTypeReference.Load();
                            ad.CategoryReference.Load();
                            ad.SiteReference.Load();
                            if (ad.Site != null)
                            {
                                ad.Site.AddressInfoReference.Load();
                                ad.Site.CityReference.Load();
                                ad.Site.DistrictReference.Load();
                                ad.Site.SiteDescriptions.Load();
                                ad.Site.SiteTypeReference.Load();
                            }
                            //if (string.IsNullOrEmpty(ad.ImageUrl))
                            //    ad.ImageUrl = "Images/PicNotFound.jpg";
                        }

                        result = result.FilterAdType(parameters.AdType);
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
                    }
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.SearchSites", exception);
            }
            return result;
        }
        public void AddSalesSiteAds(List<XmlSite> sites)
        {
            try
            {
                if (sites != null)
                {
                    foreach (var item in Entities.SiteAds.Where(o => o.AdType.Name == "Venda"))
                    {
                        Entities.DeleteObject(item);
                        //Entities.SaveChanges();
                    }
                    foreach (var site in sites)
                    {
                        AdType adType = Entities.AdTypes.Where(o => o.Name == "Venda").FirstOrDefault();
                        if (adType == null)
                        {
                            adType = new AdType() { Name = "Venda" };
                            Entities.AddToAdTypes(adType);
                        }
                        SiteType siteType = Entities.SiteTypes.Where(o => o.Name == site.SiteType).FirstOrDefault();
                        if (siteType == null)
                        {
                            //siteType = new SiteType() { Category = category, Name = site.SiteType };
                            //Entities.AddToSiteTypes(siteType);
                        }
                        else
                        {
                            siteType.CategoryReference.Load();
                            Category category = siteType.Category;
                            //Category category = Entities.Categories.Where(o => o.Name == "Residencial").FirstOrDefault();
                            //if (category == null)
                            //{
                            //    category = new Category() { Name = "Residencial" };
                            //    Entities.AddToCategories(category);
                            //}
                            City city = Entities.Cities.Where(o => o.Name == site.City).FirstOrDefault();
                            if (city == null)
                            {
                                city = new City() { Name = site.City };
                                Entities.AddToCities(city);
                            }
                            District district = Entities.Districts.Where(o => o.Name == site.District).FirstOrDefault();
                            if (district == null)
                            {
                                district = new District() { Name = site.District, Region = Entities.Regions.FirstOrDefault() };
                                Entities.AddToDistricts(district);
                            }
                            Site dbSite = new Site()
                            {
                                City = city,
                                District = district,
                                InternalArea = site.Area,
                                TotalRooms = site.RoomNumber,
                                SiteType = siteType
                            };
                            //foreach (var description in site.DescriptionCollection)
                            //{
                            //    SiteDescription siteDescription = new SiteDescription()
                            //    {
                            //        Description = description.Key,
                            //        Value = description.Value,
                            //        Site = dbSite
                            //    };
                            //}
                            SiteAd siteAd = new SiteAd()
                            {
                                AdType = adType,
                                Category = category,
                                Description = site.InternetDescription,
                                Price = site.Value,
                                Site = dbSite,
                                SiteAdID = site.SiteCode,
                            };
                            Entities.AddToSiteAds(siteAd);
                            Entities.SaveChanges();
                        }
                    }
                }
                        
            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.AddSiteAds", exception, true);
            }
        }
    }
}
