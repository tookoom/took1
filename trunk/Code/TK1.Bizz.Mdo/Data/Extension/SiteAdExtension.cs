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

        public static IQueryable<SiteAd> FilterArea(this IQueryable<SiteAd> siteAds, float areaFrom, float areaTo)
        {
            return siteAds.Where(o => o.Site.TotalArea >= areaFrom & o.Site.TotalArea <= areaTo);
        }
        public static IQueryable<SiteAd> FilterCategory(this IQueryable<SiteAd> siteAds, string category)
        {
            return siteAds.Where(o => o.Category.Name == category);
        }
        public static IQueryable<SiteAd> FilterCity(this IQueryable<SiteAd> siteAds, string cityName)
        {
            return siteAds.Where(o => o.Site.City.Name == cityName);
        }
        public static IQueryable<SiteAd> FilterCode(this IQueryable<SiteAd> siteAds, int code)
        {
            return siteAds.Where(o => o.SiteAdID == code);
        }
        public static IQueryable<SiteAd> FilterDistrict(this IQueryable<SiteAd> siteAds, List<string> districtNames)
        {
            return (from o in siteAds
                    where districtNames.Contains(o.Site.District.Name)
                    select o);

        }
        public static IQueryable<SiteAd> FilterPrice(this IQueryable<SiteAd> siteAds, float priceFrom, float priceTo)
        {
            return siteAds.Where(o => (o.Price >= priceFrom & o.Price <= priceTo));
        }
        public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, int regionID)
        {
            return siteAds.Where(o => o.Site.District.Region.RegionID == regionID);
        }
        public static IQueryable<SiteAd> FilterSiteRegion(this IQueryable<SiteAd> siteAds, string regionName)
        {
            return siteAds.Where(o => o.Site.District.Region.Name == regionName);
        }
        public static IQueryable<SiteAd> FilterRooms(this IQueryable<SiteAd> siteAds, int roomsFrom, int roomsTo)
        {
            return siteAds.Where(o => o.Site.TotalRooms >= roomsFrom & o.Site.TotalRooms <= roomsTo);
        }
        public static IQueryable<SiteAd> FilterSiteType(this IQueryable<SiteAd> siteAds, string category, string siteTypeName)
        {
            return siteAds.Where(o => o.Site.SiteType.Category.Name == category & o.Site.SiteType.Name == siteTypeName);
        }


    }
}
