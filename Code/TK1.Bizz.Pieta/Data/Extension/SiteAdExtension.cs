using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TK1.Bizz.Pieta.Const;

namespace TK1.Bizz.Pieta.Data.Extension
{
    public static class SiteAdExtension
    {
        public static SiteAd Get(this ObjectQuery<SiteAd> siteAds, int siteAdID, int siteAdType)
        {
            return siteAds.Where(o => (o.AdType.AdTypeID == siteAdType & o.SiteAdID == siteAdID)).FirstOrDefault();
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
        ////public static IQueryable<SiteAd> FilterDistrict(this IQueryable<SiteAd> siteAds, PietaEntities entities, List<string> districtNames)
        ////{
        ////    if (entities != null)
        ////    {
        ////        List<District> districts = new List<District>();
        ////        foreach (var name in districtNames)
        ////        {
        ////            var district = entities.Districts.Where(o => o.Name == name).FirstOrDefault();
        ////            if (district != null)
        ////                districts.Add(district);
        ////        }
        ////        return siteAds.All();
        ////    }
        ////}
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


        public static List<SiteAd> FilterAdType(this List<SiteAd> siteAds, SiteAdTypes siteAdType)
        {
            return siteAds.Where(o => o.AdType.AdTypeID == (int)siteAdType).ToList();
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
        public static List<SiteAd> FilterDistrict(this List<SiteAd> siteAds, List<string> districtNames)
        {
            if (districtNames == null)
                return new List<SiteAd>();
            else
            {
                if (districtNames.Contains("Todos"))
                    return siteAds;

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
