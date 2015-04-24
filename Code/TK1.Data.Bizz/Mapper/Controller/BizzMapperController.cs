using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TK1.Bizz.Mapper.Model;
using TK1.Data.Bizz.Mapper.Model;
using TK1.Data.Bizz.Model;
using TK1.Extension;
using TK1.Net.Services;
using TK1.Web;

namespace TK1.Data.Bizz.Mapper.Controller
{
    public class BizzMapperController
    {
        #region PRIVATE MEMBERS

        private MapperEntities entities;
        private List<LocationGroup> locationGroupCache;
        #endregion
        
        #region PUBLIC PROPERTIES
        public MapperEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        public BizzMapperController()
        {
            locationGroupCache = new List<LocationGroup>();
        }

        public void RollBack()
        {
            try
            {
                var changedEntries = Entities.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                {
                    entry.State = EntityState.Detached;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                {
                    entry.State = EntityState.Unchanged;
                }


            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateLocation(ref Location location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            var locationResult = getLocation(location);
            if (locationResult != null)
            {
                if (locationResult.Locality.Name == null)
                    locationResult.Locality.Name = location.Locality.Name;

                location = locationResult;
            }
        }
        public void UpdateLocationGroup(ref Location location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            var locationGroup = getLocationGroup(location);

            if (locationGroup != null)
            {
                if (location.District.Name == null)
                    location.District.Name = locationGroup.District;
                location.District.Latitude = locationGroup.DistrictLatitude;
                location.District.Longitude = locationGroup.DistrictLongitude;

                if (location.Locality.Name == null)
                    location.Locality.Name = locationGroup.City;
                location.Locality.Latitude = locationGroup.CityLatitude;
                location.Locality.Longitude = locationGroup.CityLongitude;
            }
        }

        public static MapperEntities GetMapperEntities()
        {
            return new MapperEntities();
        }


        private MapperEntities getEntities()
        {
            if (entities == null)
                entities = GetMapperEntities();
            return entities;
        }
        private Location getLocation(Location location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            Location result = null;
            var address = WebHelper.GetUriValue(getValidQuery(location.FormattedAddress));
            string request = BingMapsRESTServices.CreateRequest(address);

            try
            {
                if (!string.IsNullOrEmpty(address))
                {
                    XDocument locationResponse = BingMapsRESTServices.MakeRequest(request);
                    var response = BingMapsResponse.Parse(locationResponse);
                    if (response.HasLocations)
                        result = response.Locations.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        private LocationGroup getLocationGroup(Location location)
        {
            var adminDistrict = location.AdminDistrict.Name ?? string.Empty;
            var adminSubDistrict = location.AdminSubDistrict.Name ?? string.Empty;
            var country = location.Country.Name ?? string.Empty;
            var district = location.District.Name ?? string.Empty;
            var locality = location.Locality.Name ?? string.Empty;
            var query = string.Empty;

            if (string.IsNullOrEmpty(locality))
                throw new ArgumentNullException("locality");

            if(string.IsNullOrEmpty(district))
                district = location.Locality.Name;       

            LocationGroup result = null;
            try
            {
                var cachedLocationGroup = locationGroupCache.Where(o => o.CountryRegion == country && o.AdminDistrict == adminDistrict && o.City == locality && o.District == district).FirstOrDefault();
                if (cachedLocationGroup == null)
                {
                    var dbLocationGroup = Entities.LocationGroups.Where(o => o.CountryRegion == country && o.AdminDistrict == adminDistrict && o.City == locality && o.District == district).FirstOrDefault();
                    if (dbLocationGroup == null)
                    {
                        var queryComplement = string.Empty;

                        if (!string.IsNullOrEmpty(adminDistrict))
                            queryComplement += adminDistrict;

                        if (!string.IsNullOrEmpty(queryComplement))
                            queryComplement += ", ";

                        if (!string.IsNullOrEmpty(country))
                            queryComplement += country;

                        query = string.Format("{0}, {1}", district, locality);
                        if (!string.IsNullOrEmpty(queryComplement))
                            query += ", " + queryComplement;
                        query = getValidQuery(query);
                        var locationsResponse = BingMapsRESTServices.MakeRequest(BingMapsRESTServices.CreateRequest(query));
                        var distrinctResponse = BingMapsResponse.Parse(locationsResponse);
                        var districtLocation = distrinctResponse.Locations.FirstOrDefault();

                        query = locality;
                        if (!string.IsNullOrEmpty(queryComplement))
                            query += ", " + queryComplement;
                        query = getValidQuery(query);
                        locationsResponse = BingMapsRESTServices.MakeRequest(BingMapsRESTServices.CreateRequest(query));
                        var localityLocation = BingMapsResponse.Parse(locationsResponse).Locations.FirstOrDefault();

                        if (localityLocation != null)
                        {
                            if (districtLocation == null)
                            {
                                districtLocation = new Location()
                                {
                                    Name = district,
                                    Latitude = localityLocation.Latitude,
                                    Longitude = localityLocation.Longitude
                                };
                            }

                            result = new LocationGroup()
                            {
                                District = district,
                                DistrictLatitude = districtLocation.Latitude,
                                DistrictLongitude = districtLocation.Longitude,
                                City = locality,
                                CityLatitude = localityLocation.Latitude,
                                CityLongitude = localityLocation.Longitude,
                                AdminDistrict = adminDistrict,
                                AdminSubDistrict = adminSubDistrict,
                                CountryRegion = country
                            };
                            Entities.LocationGroups.Add(result);
                            Entities.SaveChanges();

                            if (!locationGroupCache.Contains(result))
                                locationGroupCache.Add(result);
                        }

                    }
                    else
                    {
                        if (!locationGroupCache.Contains(dbLocationGroup))
                            locationGroupCache.Add(dbLocationGroup);
                        result = dbLocationGroup;
                    }
                }
                else
                {
                    result = cachedLocationGroup;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            } 
            return result;
        }
        private string getValidQuery(string query)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            query = query.ReplaceIfExists("#", string.Empty);
            query = query.ReplaceIfExists("?", string.Empty);
            query = query.ReplaceIfExists(";", " ");
            return query;
        }



    }
}
