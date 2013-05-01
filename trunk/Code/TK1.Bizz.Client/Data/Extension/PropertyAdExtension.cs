using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace TK1.Bizz.Client.Data.Extension
{
    public static class PropertyAdExtension
    {
        public static PropertyAd Get(this ObjectQuery<PropertyAd> siteAds, string siteAdTypeID, int siteAdID)
        {
            return siteAds.Where(o => o.PropertyAdTypeID == siteAdTypeID & o.PropertyAdID == siteAdID).FirstOrDefault();
        }

        public static IQueryable<PropertyAd> FilterArea(this IQueryable<PropertyAd> siteAds, float areaFrom, float areaTo)
        {
            return siteAds.Where(o => o.TotalArea >= areaFrom & o.TotalArea <= areaTo);
        }
        public static IQueryable<PropertyAd> FilterCategory(this IQueryable<PropertyAd> siteAds, string category)
        {
            return siteAds.Where(o => o.CategoryName == category);
        }
        public static IQueryable<PropertyAd> FilterCity(this IQueryable<PropertyAd> siteAds, string cityName)
        {
            return siteAds.Where(o => o.CityName == cityName);
        }
        public static IQueryable<PropertyAd> FilterCode(this IQueryable<PropertyAd> siteAds, int code)
        {
            return siteAds.Where(o => o.PropertyAdID == code);
        }
        public static IQueryable<PropertyAd> FilterDistrict(this IQueryable<PropertyAd> siteAds, List<string> districtNames)
        {
            return (from o in siteAds
                    where districtNames.Contains(o.DistrictName)
                    select o);

        }
        public static IQueryable<PropertyAd> FilterPrice(this IQueryable<PropertyAd> siteAds, float valueFrom, float valueTo)
        {
            return siteAds.Where(o => (o.Value >= valueFrom & o.Value <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterRooms(this IQueryable<PropertyAd> siteAds, int roomsFrom, int roomsTo)
        {
            return siteAds.Where(o => o.TotalRooms >= roomsFrom & o.TotalRooms <= roomsTo);
        }
        public static IQueryable<PropertyAd> FilterPropertyType(this IQueryable<PropertyAd> siteAds, string siteTypeName)
        {
            return siteAds.Where(o => o.PropertyTypeName == siteTypeName);
        }

    }
}
