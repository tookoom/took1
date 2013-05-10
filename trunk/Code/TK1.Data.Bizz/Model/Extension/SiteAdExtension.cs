using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TK1.Data.Bizz.Model;

namespace TK1.Data.Bizz.Model.Extension
{
    public static class SiteAdExtension
    {
        public static SiteAd Get(this ObjectQuery<SiteAd> siteAds, string customerCodename, int siteAdTypeID, int siteAdID)
        {
            return siteAds.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == siteAdTypeID & o.SiteAdID == siteAdID).FirstOrDefault();
        }

        public static IQueryable<SiteAd> FilterArea(this IQueryable<SiteAd> siteAds, float areaFrom, float areaTo)
        {
            return siteAds.Where(o => o.TotalArea >= areaFrom & o.TotalArea <= areaTo);
        }
        public static IQueryable<SiteAd> FilterCategory(this IQueryable<SiteAd> siteAds, string category)
        {
            return siteAds.Where(o => o.CategoryName == category);
        }
        public static IQueryable<SiteAd> FilterCity(this IQueryable<SiteAd> siteAds, string cityName)
        {
            return siteAds.Where(o => o.CityName == cityName);
        }
        public static IQueryable<SiteAd> FilterCode(this IQueryable<SiteAd> siteAds, int code)
        {
            return siteAds.Where(o => o.SiteAdID == code);
        }
        public static IQueryable<SiteAd> FilterCustomer(this IQueryable<SiteAd> siteAds, string customerCodename)
        {
            return siteAds.Where(o => o.CustomerCodename == customerCodename);
        }
        public static IQueryable<SiteAd> FilterDistrict(this IQueryable<SiteAd> siteAds, List<string> districtNames)
        {
            return (from o in siteAds
                    where districtNames.Contains(o.DistrictName)
                    select o);

        }
        public static IQueryable<SiteAd> FilterPrice(this IQueryable<SiteAd> siteAds, float valueFrom, float valueTo)
        {
            return siteAds.Where(o => (o.Value >= valueFrom & o.Value <= valueTo));
        }
        public static IQueryable<SiteAd> FilterRooms(this IQueryable<SiteAd> siteAds, int roomsFrom, int roomsTo)
        {
            return siteAds.Where(o => o.TotalRooms >= roomsFrom & o.TotalRooms <= roomsTo);
        }
        public static IQueryable<SiteAd> FilterSiteType(this IQueryable<SiteAd> siteAds, string siteTypeName)
        {
            return siteAds.Where(o => o.SiteTypeName == siteTypeName);
        }

        #region OLD
        //public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, int regionID)
        //{
        //    return siteAds.Where(o => o.Site.District.Region.RegionID == regionID);
        //}
        //public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, string regionName)
        //{
        //    return siteAds.Where(o => o.Site.District.Region.Name == regionName);
        //}
        
        #endregion
    }
}
