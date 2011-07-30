using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TK1.Bizz.Mdo.Const;

namespace TK1.Bizz.Mdo.Data.Extension
{
    public static class SiteAdExtension
    {
        public static SiteAd Get(this ObjectQuery<SiteAd> siteAds, int customerID, int siteAdID)
        {
            return siteAds.Where(o => o.CustomerID == customerID & o.SiteAdID == siteAdID).FirstOrDefault();
        }

        //public static IQueryable<SiteAd> FilterAdType(this IQueryable<SiteAd> siteAds, SiteAdTypes siteAdType)
        //{
        //    return siteAds.Where(o => o.AdType.AdTypeID == (int)siteAdType);
        //}
        //public static IQueryable<SiteAd> FilterArea(this IQueryable<SiteAd> siteAds, float areaFrom, float areaTo)
        //{
        //    return siteAds.Where(o => o.Site.TotalArea >= areaFrom & o.Site.TotalArea <= areaTo);
        //}
        //public static IQueryable<SiteAd> FilterCategory(this IQueryable<SiteAd> siteAds, string category)
        //{
        //    return siteAds.Where(o => o.Category.Name == category);
        //}
        //public static IQueryable<SiteAd> FilterCity(this IQueryable<SiteAd> siteAds, string cityName)
        //{
        //    return siteAds.Where(o => o.Site.City.Name == cityName);
        //}
        //public static IQueryable<SiteAd> FilterDistrict(this IQueryable<SiteAd> siteAds, List<string> districtNames)
        //{
        //    return (from o in siteAds
        //            where districtNames.Contains(o.Site.District.Name)
        //            select o);

        //}
        //public static IQueryable<SiteAd> FilterPrice(this IQueryable<SiteAd> siteAds, float priceFrom, float priceTo)
        //{
        //    return siteAds.Where(o => (o.Price >= priceFrom & o.Price <= priceTo));
        //}
        //public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, int regionID)
        //{
        //    return siteAds.Where(o => o.Site.District.Region.RegionID == regionID);
        //}
        //public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, string regionName)
        //{
        //    return siteAds.Where(o => o.Site.District.Region.Name == regionName);
        //}
        //public static IQueryable<SiteAd> FilterRooms(this IQueryable<SiteAd> siteAds, int roomsFrom, int roomsTo)
        //{
        //    return siteAds.Where(o => o.Site.TotalRooms >= roomsFrom & o.Site.TotalRooms <= roomsTo);
        //}
        //public static IQueryable<SiteAd> FilterSiteType(this IQueryable<SiteAd> siteAds, string category, string siteTypeName)
        //{
        //    return siteAds.Where(o => o.Site.SiteType.Category.Name == category & o.Site.SiteType.Name == siteTypeName);
        //}


        public static List<SiteAd> FilterCustomer(this List<SiteAd> siteAds, int customerID)
        {
            return siteAds.Where(o => o.CustomerID == customerID).ToList();
        }
        public static List<SiteAd> FilterArea(this List<SiteAd> siteAds, float areaFrom, float areaTo)
        {
            return siteAds.Where(o => o.Site.TotalArea >= areaFrom & o.Site.TotalArea <= areaTo).ToList();
        }
        public static List<SiteAd> FilterCategory(this List<SiteAd> siteAds, string category)
        {
            return siteAds.Where(o => o.Category.Name == category).ToList();
        }
        public static List<SiteAd> FilterCity(this List<SiteAd> siteAds, string cityName)
        {
            return siteAds.Where(o => o.Site.City.Name == cityName).ToList();
        }
        public static List<SiteAd> FilterCode(this List<SiteAd> siteAds, int siteCode)
        {
            return siteAds.Where(o => o.SiteAdID == siteCode).ToList();
        }
        public static List<SiteAd> FilterDistrict(this List<SiteAd> siteAds, List<string> districtNames)
        {
            if (districtNames == null)
                return new List<SiteAd>();
            else
            {
                return (from o in siteAds
                        where districtNames.Contains(o.Site.District.Name)
                        select o).ToList();
            }
        }
        public static List<SiteAd> FilterPrice(this List<SiteAd> siteAds, float priceFrom, float priceTo)
        {
            return siteAds.Where(o => (o.Price >= priceFrom & o.Price <= priceTo)).ToList();
        }
        public static List<SiteAd> FilterSiteRegion(this List<SiteAd> siteAds, int regionID)
        {
            return siteAds.Where(o => o.Site.District.Region.RegionID == regionID).ToList();
        }
        public static List<SiteAd> FilterSiteRegion(this List<SiteAd> siteAds, string regionName)
        {
            return siteAds.Where(o => o.Site.District.Region.Name == regionName).ToList();
        }
        public static List<SiteAd> FilterRooms(this List<SiteAd> siteAds, int roomsFrom, int roomsTo)
        {
            return siteAds.Where(o => o.Site.TotalRooms >= roomsFrom & o.Site.TotalRooms <= roomsTo).ToList();
        }
        public static List<SiteAd> FilterSiteType(this List<SiteAd> siteAds, string category, string siteTypeName)
        {
            return siteAds.Where(o => o.Site.SiteType.Category.Name == category & o.Site.SiteType.Name == siteTypeName).ToList();
        }

    }
}
