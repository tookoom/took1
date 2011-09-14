using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TK1.Bizz.Mdo.Const;

namespace TK1.Bizz.Mdo.Data.Extension
{
    public static class SiteReleaseAdExtension
    {
        public static SiteReleaseAd Get(this ObjectQuery<SiteReleaseAd> SiteReleaseAds, int customerID, int SiteReleaseAdID)
        {
            return SiteReleaseAds.Where(o => o.CustomerID == customerID & o.SiteReleaseAdID == SiteReleaseAdID).FirstOrDefault();
        }

        public static IQueryable<SiteReleaseAd> FilterArea(this IQueryable<SiteReleaseAd> SiteReleaseAds, float areaFrom, float areaTo)
        {
            return SiteReleaseAds.Where(o => o.Site.TotalArea >= areaFrom & o.Site.TotalArea <= areaTo);
        }
        public static IQueryable<SiteReleaseAd> FilterCategory(this IQueryable<SiteReleaseAd> SiteReleaseAds, string category)
        {
            return SiteReleaseAds.Where(o => o.Category.Name == category);
        }
        public static IQueryable<SiteReleaseAd> FilterCity(this IQueryable<SiteReleaseAd> SiteReleaseAds, string cityName)
        {
            return SiteReleaseAds.Where(o => o.Site.City.Name == cityName);
        }
        public static IQueryable<SiteReleaseAd> FilterCode(this IQueryable<SiteReleaseAd> SiteReleaseAds, int code)
        {
            return SiteReleaseAds.Where(o => o.SiteReleaseAdID == code);
        }
        public static IQueryable<SiteReleaseAd> FilterDistrict(this IQueryable<SiteReleaseAd> SiteReleaseAds, List<string> districtNames)
        {
            return (from o in SiteReleaseAds
                    where districtNames.Contains(o.Site.District.Name)
                    select o);

        }
        public static IQueryable<SiteReleaseAd> FilterMinValue(this IQueryable<SiteReleaseAd> SiteReleaseAds, float MinValueFrom, float MinValueTo)
        {
            return SiteReleaseAds.Where(o => (o.MinValue >= MinValueFrom & o.MinValue <= MinValueTo));
        }
        public static IQueryable<SiteReleaseAd> FilterSiteRegion(this IQueryable<SiteReleaseAd> SiteReleaseAds, int regionID)
        {
            return SiteReleaseAds.Where(o => o.Site.District.Region.RegionID == regionID);
        }
        public static IQueryable<SiteReleaseAd> FilterSiteRegion(this IQueryable<SiteReleaseAd> SiteReleaseAds, string regionName)
        {
            return SiteReleaseAds.Where(o => o.Site.District.Region.Name == regionName);
        }
        public static IQueryable<SiteReleaseAd> FilterRooms(this IQueryable<SiteReleaseAd> SiteReleaseAds, int roomsFrom, int roomsTo)
        {
            return SiteReleaseAds.Where(o => o.Site.TotalRooms >= roomsFrom & o.Site.TotalRooms <= roomsTo);
        }
        public static IQueryable<SiteReleaseAd> FilterSiteType(this IQueryable<SiteReleaseAd> SiteReleaseAds, string category, string siteTypeName)
        {
            return SiteReleaseAds.Where(o => o.Site.SiteType.Category.Name == category & o.Site.SiteType.Name == siteTypeName);
        }


    }
}
