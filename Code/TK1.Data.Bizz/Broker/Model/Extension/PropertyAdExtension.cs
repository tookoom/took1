using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using TK1.Data.Bizz.Model;

namespace TK1.Data.Bizz.Broker.Model.Extension
{
    public static class PropertyAdExtension
    {
        public static PropertyAd Get(this ObjectQuery<PropertyAd> siteAds, string siteAdTypeID, int siteAdID)
        {
            return siteAds.Where(o => o.PropertyAdType == siteAdTypeID & o.PropertyAdCode == siteAdID).FirstOrDefault();
        }

        public static IQueryable<PropertyAd> FilterAdType(this IQueryable<PropertyAd> siteAds, string adType)
        {
            return siteAds.Where(o => o.PropertyAdType == adType);
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
        public static IQueryable<PropertyAd> FilterCityLatitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.CityLatitude.HasValue & (o.CityLatitude >= valueFrom & o.CityLatitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterCityLongitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.CityLongitude.HasValue & (o.CityLongitude >= valueFrom & o.CityLongitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterCode(this IQueryable<PropertyAd> siteAds, int code)
        {
            return siteAds.Where(o => o.PropertyAdCode == code);
        }
        public static IQueryable<PropertyAd> FilterDistrict(this IQueryable<PropertyAd> siteAds, List<string> districtNames)
        {
            return (from o in siteAds
                    where districtNames.Contains(o.DistrictName)
                    select o);

        }
        public static IQueryable<PropertyAd> FilterDistrictLatitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.DistrictLatitude.HasValue & (o.DistrictLatitude >= valueFrom & o.DistrictLatitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterDistrictLongitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.DistrictLongitude.HasValue & (o.DistrictLongitude >= valueFrom & o.DistrictLongitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterPropertyLatitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.PropertyLatitude.HasValue & (o.PropertyLatitude >= valueFrom & o.PropertyLatitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterPropertyLongitude(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => o.PropertyLongitude.HasValue & (o.PropertyLongitude >= valueFrom & o.PropertyLongitude <= valueTo));
        }
        public static IQueryable<PropertyAd> FilterNumberOfRooms(this IQueryable<PropertyAd> siteAds, string numberOfRooms)
        {
            return siteAds.Where(o => o.PropertyAdDetails.Where(d=> d.Code == PropertyAdDetailCodes.PROPERTY_AD_ROOMS && d.Value == numberOfRooms).Any());
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
        public static IQueryable<PropertyAd> FilterValue(this IQueryable<PropertyAd> siteAds, float? valueFrom, float? valueTo)
        {
            return siteAds.Where(o => (o.Value >= (valueFrom ?? float.MinValue) & o.Value <= (valueTo ?? float.MaxValue)));
        }

    }
}
