using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Pieta.Data.Extension;

namespace TK1.Bizz.Pieta.Data
{
    public class SiteController : BaseController
    {
        public SiteController()
        {
        }

        public static List<string> GetCities()
        {
            List<string> result = null;
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
            List<string> result = null;
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
                LogController.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
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
                        //query = query.FilterAdType(parameters.AdType);
                        //query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                        //query = query.FilterCategory(parameters.Category);
                        //query = query.FilterCity(parameters.CityName);
                        //query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                        //query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                        //query = query.FilterSiteRegion(parameters.RegionID);
                        //query = query.FilterSiteType(parameters.AdType, parameters.SiteType);
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
                        }

                        result = result.FilterAdType(parameters.AdType);
                        //query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                        result = result.FilterCategory(parameters.Category);
                        result = result.FilterCity(parameters.CityName);
                        result = result.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                        result = result.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                        //query = query.FilterSiteRegion(parameters.RegionID);
                        result = result.FilterSiteType(parameters.Category, parameters.SiteType);
                        result = result.FilterDistrict(parameters.Districts);
                    }
                }

            }
            catch (Exception exception)
            {
                LogController.WriteException("SiteController.SearchSites", exception);
            }
            return result;
        }
    }
}
