using System;
using System.Collections.Generic;
using System.Linq;
using TK1.Bizz.Broker.Presentation;
using TK1.Bizz.Broker.Presentation.Culture;
using TK1.Data.Bizz.Controller;
using TK1.Data.Bizz.Broker.Model;
using TK1.Data.Bizz.Broker.Model.Extension;
using TK1.Data.Bizz.Model;
using TK1.Bizz.Mapper.Model;
using System.Data;

namespace TK1.Data.Bizz.Broker.Controller
{
    public class PropertyAdController : BizzBrokerController
    {
        #region PRIVATE MEMBERS
        private string customerCode;
        private string uiCulture = "pt-BR";
        #endregion
        #region PUBLIC PROPERTIES
        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }
        public bool LoadCategory { get; set; }
        public bool LoadDetails { get; set; }
        public bool LoadPics { get; set; }
        public string UICulture
        {
            get { return uiCulture; }
            set { uiCulture = value; }
        }
        #endregion

        public PropertyAdController(string customerCode)
        {
            this.customerCode = customerCode;
            LoadPics = true;
        }

        #region MASTER DATA METHODS
        public List<PropertyAdTypes> GetAdTypes()
        {
            var result = new List<PropertyAdTypes>();
            foreach (var item in Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.Visible).Select(o => o.PropertyAdType).Distinct())
            {
                var adType = PropertyAdTypes._Undefined;
                if (Enum.TryParse<PropertyAdTypes>(item, out adType))
                    if (!result.Contains(adType))
                        result.Add(adType);
            }
            return result;
        }
        public List<string> GetCities()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAds
                          where o.Visible & o.CustomerCode == customerCode
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetCities(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAds
                          where o.PropertyAdType == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetDistricts()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAds
                          where o.Visible & o.CustomerCode == customerCode
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetDistricts(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAds
                          where o.PropertyAdType == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyCategories()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAds
                          where o.Visible & o.CustomerCode == customerCode
                          select o.CategoryName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyCategories(PropertyAdCategories adCategory)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyCategorieName = adCategory.ToString();
                result = (from o in Entities.PropertyAds
                          where o.CategoryName == propertyCategorieName & o.Visible & o.CustomerCode == customerCode
                          select o.CategoryName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetPropertyCategories", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAds
                          where o.Visible & o.CustomerCode == customerCode & o.Visible 
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAds
                          where o.PropertyAdType == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetPropertyTypes", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes(PropertyAdTypes adType, PropertyAdCategories adCategory)
        {
            List<string> result = new List<string>();
            try
            {
                var adTypeName = adType.ToString();
                var categoryName = adCategory.ToString();
                result = (from o in Entities.PropertyAds
                          where o.PropertyAdType == adTypeName & o.CategoryName == categoryName & o.Visible & o.CustomerCode == customerCode
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetPropertyTypes", exception);
            }
            return result;
        }


        #endregion

        #region PRESENTATION METHODS

        //SINGLE VIEWS
        public PropertyAdView GetPropertyAdView(PropertyAdTypes adType, int adCode)
        {
            PropertyAdView result = null;
            try
            {
                var propertyAd = getPropertyAd(adType, adCode);
                if (propertyAd != null)
                {
                    var propertyCategory = PropertyAdCategories.Residencial;
                    if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                        propertyCategory = PropertyAdCategories.Comercial;
                    //propertyAd.PropertyAdDetails.Load();
                    //propertyAd.PropertyAdPics.Load();
                    string mainPicName = string.Empty;
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicName = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().FileName;

                    result = new PropertyAdView()
                    {
                        CustomerCode = customerCode,
                        AdType = adType,
                        AdCode = propertyAd.PropertyAdCode,

                        AdCategory = propertyCategory,
                        AdTypeName = PropertyTranslations.GetAdTypeDisplayName(adType, uiCulture),
                        Location = new Location()
                        {
                            AddressLine = "",
                            Latitude = propertyAd.PropertyLatitude,
                            Longitude = propertyAd.PropertyLongitude,
                            Locality = new GeoLocation() { Name = propertyAd.CityName, Latitude = propertyAd.CityLatitude, Longitude = propertyAd.CityLongitude },
                            District = new GeoLocation() { Name = propertyAd.DistrictName, Latitude = propertyAd.DistrictLatitude, Longitude = propertyAd.DistrictLongitude }
                        },
                        AreaDescription = propertyAd.AreaDescription,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        CondoDescription = propertyAd.CondoDescription,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        FullDescription = propertyAd.FullDescription,
                        IsAddressVisible = false,
                        MainPicUrl = mainPicName,
                        InternalArea = (float)propertyAd.InternalArea,
                        TotalArea = (float)propertyAd.TotalArea,
                        TotalRooms = propertyAd.TotalRooms,
                        PropertyType = propertyAd.PropertyTypeName,
                        PropertyTypeRoomName = PropertyTranslations.GetRoomDisplayName(propertyAd.PropertyTypeName, uiCulture),
                        ShortDescription = propertyAd.ShortDescription,
                        Title = propertyAd.Title,
                        Value = (float)(propertyAd.Value)

                    };
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAsController.GetPropertyAd", exception);
            }
            return result;
        }
        public PropertyReleaseAdView GetPropertyReleaseAdView(int releaseAdCode)
        {
            var releaseAd = getPropertyReleaseAd(releaseAdCode);
            var result = getPropertyReleaseAdView(releaseAd);
            return result;
        }

        //COLLECTION VIEWS
        public List<PropertyAdView> GetPropertyAds(PropertyAdTypes adType)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            string propertyTypeID = adType.ToString();
            foreach (var item in Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyTypeID).ToList())
            {
                var view = getPropertyAdView(item);
                if (view != null)
                    result.Add(view);
            }

            return result;
        }
        public List<PropertyReleaseAdView> GetPropertyReleaseAds()
        {
            List<PropertyReleaseAdView> result = new List<PropertyReleaseAdView>();
            foreach (var releaseAd in Entities.PropertyReleaseAds.Where(o => o.CustomerCode == this.customerCode).ToList())
            {
                var view = getPropertyReleaseAdView(releaseAd);
                if (view != null)
                    result.Add(view);
            }
            return result;
        }
        public List<PropertyAdDetailView> GetPropertyDetailViews(PropertyAdTypes adType, int adCode)
        {
            List<PropertyAdDetailView> result = new List<PropertyAdDetailView>();
            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                //propertyAd.PropertyAdDetails.Load();
                if (propertyAd.PropertyAdDetails != null)
                {
                    var query = from o in propertyAd.PropertyAdDetails
                                select o;
                    foreach (var detail in query)
                    {
                        result.Add(new PropertyAdDetailView()
                        {
                            Code = detail.Value + " " + detail.Description,
                            Value = detail.Value,
                            ImageUrl = "Dot.png"
                        });
                    }
                }
            }
            return result;
        }
        public List<PropertyAdPicView> GetPropertyPicViews(PropertyAdTypes adType, int adCode)
        {
            List<PropertyAdPicView> result = new List<PropertyAdPicView>();
            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                //propertyAd.PropertyAdPics.Load();
                if (propertyAd.PropertyAdPics != null)
                {
                    foreach (var item in propertyAd.PropertyAdPics)
                    {
                        result.Add(new PropertyAdPicView()
                        {
                            Description = item.Description,
                            FileName = item.FileName,
                            Index = item.PropertyAdPicID,
                            Path = item.PictureFilePath,
                            ThumbnailPath = item.ThumbnailFilePath,
                            ThumbnailUrl = item.ThumbnailUrl,
                            Url = item.PictureUrl
                        });
                    }
                }
            }
            return result;
        }

        //SEARCH METHODS
        public List<PropertyAdView> Search(List<SearchAttribute> searchParams)
        {
            if (searchParams == null)
                throw new ArgumentNullException("searchParams");

            var query = SearchQuery(searchParams);

            List<PropertyAdView> result = GetPropertyAdViewList(query);
            return result;
        }

        public List<PropertyAdView> GetPropertyAdViewList(IQueryable<PropertyAd> query)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            
            List<PropertyAdView> result = new List<PropertyAdView>();
            foreach (var propertyAd in query.ToList())
            {
                var propertyAdView = getPropertyAdView(propertyAd);
                if (propertyAdView != null)
                    result.Add(propertyAdView);
            }
            return result;
        }

        public IQueryable<PropertyAd> SearchQuery(List<SearchAttribute> searchParams)
        {
            if (searchParams == null)
                throw new ArgumentNullException("searchParams");
            
            var query = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.Visible);
            foreach (var param in searchParams)
            {
                var attribute = (PropertyAdSearchAttributes)param.Attribute;
                switch (attribute)
                {
                    case PropertyAdSearchAttributes.AdType:
                        query = query.Where(o => o.PropertyAdType == param.Value as string);
                        break;
                    case PropertyAdSearchAttributes.CityLatitude:
                        query = query.FilterCityLatitude(param.MinValue as float?, param.MaxValue as float?);
                        break;
                    case PropertyAdSearchAttributes.CityLongitude:
                        query = query.FilterCityLongitude(param.MinValue as float?, param.MaxValue as float?);
                        break;
                    case PropertyAdSearchAttributes.DistrictLatitude:
                        query = query.FilterDistrictLatitude(param.MinValue as float?, param.MaxValue as float?);
                        break;
                    case PropertyAdSearchAttributes.DistrictLongitude:
                        query = query.FilterDistrictLongitude(param.MinValue as float?, param.MaxValue as float?);
                        break;
                    case PropertyAdSearchAttributes.PropertyLatitude:
                        query = query.FilterPropertyLatitude(param.MinValue as float?, param.MaxValue as float?);
                        break;
                    case PropertyAdSearchAttributes.PropertyLongitude:
                        query = query.FilterPropertyLongitude(param.MinValue as float?, param.MaxValue as float?);
                        break;

                }
            }
            return query;
        }
        public List<PropertyAdView> SearchPropertyAds(PropertyAdSearchParameters parameters)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            if (parameters != null)
            {
                var adTypeID = parameters.AdType.ToString();
                var query = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.Visible & o.PropertyAdType == adTypeID);
                if (parameters.AdCode > 0)
                {
                    query = query.FilterCode(parameters.AdCode);
                }
                else
                {
                    query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                    if(parameters.CityName!="*")
                        query = query.FilterCity(parameters.CityName);
                    query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                    query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                    if (parameters.PropertyType != "*")
                        query = query.FilterPropertyType(parameters.PropertyType);
                    if (!parameters.Districts.Contains("*"))
                        query = query.FilterDistrict(parameters.Districts);
                }
                foreach (var propertyAd in query.ToList())
                {
                    string mainPicUrl = string.Empty;
                    if (LoadPics)
                    {
                        if (propertyAd.PropertyAdPics.Count > 0)
                            mainPicUrl = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().ThumbnailUrl;
                        else
                            mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivelThumb.png";
                    }

                    var propertyAdView = getPropertyAdView(propertyAd);
                    if (propertyAdView != null)
                        result.Add(propertyAdView);
                }
            }
            if (parameters.ResultOrdering != PropertyAdSearchResultOrders._Undefined)
                result = OrderResults(result, parameters.ResultOrdering);
            return result;
        }
        public List<PropertyAdView> SearchPropertyAds(PropertyAdSearchParameters parameters, int startIndex, int pageSize)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            if (parameters != null)
            {
                var adTypeID = parameters.AdType.ToString();
                var query = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.Visible & o.PropertyAdType == adTypeID);
                if (parameters.AdCode > 0)
                {
                    query = query.FilterCode(parameters.AdCode);
                }
                else
                {
                    query = query.FilterArea(parameters.AreaFrom, parameters.AreaTo);
                    query = query.FilterCity(parameters.CityName);
                    query = query.FilterPrice(parameters.PriceFrom, parameters.PriceTo);
                    query = query.FilterRooms(parameters.RoomsFrom, parameters.RoomsTo);
                    if (parameters.PropertyType != "*")
                        query = query.FilterPropertyType(parameters.PropertyType);
                    if (!parameters.Districts.Contains("*"))
                        query = query.FilterDistrict(parameters.Districts);
                }
                foreach (var propertyAd in query.Skip(startIndex).Take(pageSize))
                {
                    result.Add(getPropertyAdView(propertyAd));
                }
            }
            //if (parameters.ResultOrdering != PropertyAdSearchResultOrders._Undefined)
            //    result = OrderResults(result, parameters.ResultOrdering);
            return result;
        }
        public static List<PropertyAdView> OrderResults(List<PropertyAdView> propertyAds, PropertyAdSearchResultOrders resultOrder)
        {
            List<PropertyAdView> result = null;
            if (propertyAds != null & resultOrder != PropertyAdSearchResultOrders._Undefined)
            {
                try
                {
                    switch (resultOrder)
                    {
                        case PropertyAdSearchResultOrders.AreaAscending:
                            result = propertyAds.OrderBy(o => o.TotalArea).ToList();
                            break;
                        case PropertyAdSearchResultOrders.AreaDescending:
                            result = propertyAds.OrderByDescending(o => o.TotalArea).ToList();
                            break;
                        case PropertyAdSearchResultOrders.PriceAscending:
                            result = propertyAds.OrderBy(o => o.Value).ToList();
                            break;
                        case PropertyAdSearchResultOrders.PriceDescending:
                            result = propertyAds.OrderByDescending(o => o.Value).ToList();
                            break;
                    }
                }
                catch
                {
                    result = propertyAds;
                }
            }
            if (result == null)
                result = new List<PropertyAdView>();
            return result;
        }

        //FEATURED ADS
        public List<PropertyAdView> GetFeaturedRentPropertyAds(int count)
        {
            return GetFeaturedPropertyAds(PropertyAdTypes.Rent, 5);
        }
        public List<PropertyAdView> GetFeaturedSellingPropertyAds(int count)
        {
            return GetFeaturedPropertyAds(PropertyAdTypes.Sell, count);
        }
        public List<PropertyAdView> GetFeaturedPropertyAds(PropertyAdTypes adType, int count)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            try
            {
                string propertyAdType = adType.ToString();
                var query = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyAdType & o.Featured & o.Visible).Take(count);
                foreach (var propertyAd in query.ToList())
                    result.Add(getPropertyAdView(propertyAd));
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("PropertyAdController.GetFeaturedPropertyAds", exception);
            }
            return result;
        }

        #endregion

        #region INTEGRATION METHODS
        public void CleanUp()
        {
            var lastDate = Entities.PropertyAdDetails.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_UPDATE).Select(o => o.Value).Max();
            var ads = Entities.PropertyAdDetails.Where(o => o.CustomerCode == customerCode & o.Code == PropertyAdDetailCodes.PROPERTY_AD_UPDATE && o.Value != lastDate).Select(o => o.PropertyAd).ToList();
            foreach (var item in ads)
            {
                PropertyAdTypes adType = (PropertyAdTypes)Enum.Parse(typeof(PropertyAdTypes), item.PropertyAdType);
                RemovePropertyAd(adType, item.PropertyAdCode);
            }
            ads = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.PropertyAdDetails.Where(d => d.Code == PropertyAdDetailCodes.PROPERTY_AD_UPDATE).Count() == 0).ToList();
            foreach (var item in ads)
            {
                PropertyAdTypes adType = (PropertyAdTypes)Enum.Parse(typeof(PropertyAdTypes), item.PropertyAdType);
                RemovePropertyAd(adType, item.PropertyAdCode);
            }
        }
        public void RemovePropertyAd(PropertyAdTypes adType, int adCode)
        {
            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                //propertyAd.PropertyAdDetails.Load();
                //propertyAd.PropertyAdPics.Load();
                //propertyAd.PropertyReleaseAdReference.Load();

                foreach (var item in propertyAd.PropertyAdDetails.ToList())
                    Entities.PropertyAdDetails.Remove(item);
                foreach (var item in propertyAd.PropertyAdPics.ToList())
                    Entities.PropertyAdPics.Remove(item);
                if (propertyAd.PropertyReleaseAd != null)
                    Entities.PropertyReleaseAds.Remove(propertyAd.PropertyReleaseAd);

                Entities.PropertyAds.Remove(propertyAd);
                Entities.SaveChanges();
            }
        }
        public void RemovePropertyAdDetails(PropertyAdTypes adType, int adCode)
        {
            var details = getPropertyAdDetails(adType, adCode);
            foreach (var item in details)
            {
                Entities.PropertyAdDetails.Remove(item);
            }
            Entities.SaveChanges();
        }
        public void RemovePropertyAdPics(PropertyAdTypes adType, int adCode)
        {
            var details = getPropertyAdPics(adType, adCode);
            foreach (var item in details)
            {
                Entities.PropertyAdPics.Remove(item);
            }
            Entities.SaveChanges();
        }
        public void SetPropertyAd(PropertyAdView propertyAdView)
        {
            if (propertyAdView == null)
                throw new NullReferenceException("Paramenter propertyAdView can't be null");

            var propertyAd = getPropertyAd(propertyAdView.AdType, propertyAdView.AdCode);
            if (propertyAd == null)
            {
                var newPropertyAd = new PropertyAd()
                {
                    //PRIMARY KEY
                    PropertyAdCode = propertyAdView.AdCode,
                    PropertyAdType = propertyAdView.AdType.ToString(),
                    CustomerCode = customerCode,

                    //OTHER COLUMNS
                    AreaDescription = propertyAdView.AreaDescription,
                    CategoryName = propertyAdView.AdCategory.ToString(),
                    CityName = propertyAdView.Location.Locality.Name,
                    CityLatitude = propertyAdView.Location.Locality.Latitude,
                    CityLongitude = propertyAdView.Location.Locality.Longitude,
                    CityTaxes = propertyAdView.CityTaxes,
                    CondoDescription = propertyAdView.CondoDescription,
                    CondoTaxes = propertyAdView.CondoTaxes,
                    DistrictName = propertyAdView.Location.District.Name,
                    DistrictLatitude = propertyAdView.Location.District.Latitude,
                    DistrictLongitude = propertyAdView.Location.District.Longitude,
                    ExternalArea = 0,
                    Featured = propertyAdView.IsFeatured,
                    FullDescription = propertyAdView.FullDescription,
                    InternalArea = propertyAdView.InternalArea,
                    PropertyLatitude = propertyAdView.Location.Latitude,
                    PropertyLongitude = propertyAdView.Location.Longitude,
                    PropertyTypeName = propertyAdView.PropertyType,
                    PicUrl = propertyAdView.MainPicUrl,
                    PropertyAdStatus = "ACTIVE",
                    ShortDescription = propertyAdView.ShortDescription,
                    Title = propertyAdView.Title,
                    TotalArea = propertyAdView.TotalArea,
                    TotalRooms = propertyAdView.TotalRooms,
                    Value = propertyAdView.Value,
                    Visible = true
                };
                Entities.PropertyAds.Add(newPropertyAd);
                Entities.SaveChanges();
            }
            else
            {
                propertyAd.AreaDescription = propertyAdView.AreaDescription;
                propertyAd.CategoryName = propertyAdView.AdCategory.ToString();
                propertyAd.CityName = propertyAdView.Location.Locality.Name;
                propertyAd.CityLatitude = propertyAdView.Location.Locality.Latitude;
                propertyAd.CityLongitude = propertyAdView.Location.Locality.Longitude;
                propertyAd.CityTaxes = propertyAdView.CityTaxes;
                propertyAd.CustomerCode = customerCode;
                propertyAd.CondoDescription = propertyAdView.CondoDescription;
                propertyAd.CondoTaxes = propertyAdView.CondoTaxes;
                propertyAd.DistrictName = propertyAdView.Location.District.Name;
                propertyAd.DistrictLatitude = propertyAdView.Location.District.Latitude;
                propertyAd.DistrictLongitude = propertyAdView.Location.District.Longitude;
                propertyAd.ExternalArea = 0;
                propertyAd.Featured = propertyAdView.IsFeatured;
                propertyAd.FullDescription = propertyAdView.FullDescription;
                propertyAd.InternalArea = propertyAdView.InternalArea;
                propertyAd.PropertyLatitude = propertyAdView.Location.Latitude;
                propertyAd.PropertyLongitude = propertyAdView.Location.Longitude;
                propertyAd.PropertyTypeName = propertyAdView.PropertyType;
                propertyAd.PicUrl = propertyAdView.MainPicUrl;
                propertyAd.PropertyAdStatus = "ACTIVE";
                propertyAd.ShortDescription = propertyAdView.ShortDescription;
                propertyAd.Title = propertyAdView.Title;
                propertyAd.TotalArea = propertyAdView.TotalArea;
                propertyAd.TotalRooms = propertyAdView.TotalRooms;
                propertyAd.Value = propertyAdView.Value;
                propertyAd.Visible = true;
                Entities.SaveChanges();
            }
        }
        public void SetPropertyReleaseAd(PropertyReleaseAdView releaseAdView)
        {
            if (releaseAdView == null)
                throw new NullReferenceException("Paramenter releaseAdView can't be null");

            var releaseAd = getPropertyReleaseAd(releaseAdView.AdCode);
            if (releaseAd == null)
            {
                var newPropertyReleaseAd = new PropertyReleaseAd()
                {
                    //PRIMARY KEY
                    PropertyAdCode = releaseAdView.AdCode,
                    PropertyAdType = PropertyAdTypes.Release.ToString(),
                    CustomerCode = customerCode,

                    //OTHER COLUMNS
                    Address = releaseAdView.Location.AddressLine,
                    AddressComplement = releaseAdView.AddressComplement,
                    AddressNumber = releaseAdView.AddressNumber,
                    ConstructorName = releaseAdView.ConstructorName,
                    MaxExternalArea = releaseAdView.MaxExternalArea,
                    MaxParkingLots = releaseAdView.MaxParkingLots,
                    MaxInternalArea = releaseAdView.MaxInternalArea,
                    MaxSuites = releaseAdView.MaxSuites,
                    MaxTotalArea = releaseAdView.MaxTotalArea,
                    MaxTotalRooms = releaseAdView.MaxTotalRooms,
                    MaxValue = releaseAdView.MaxValue,
                    MinExternalArea = releaseAdView.MinExternalArea,
                    MinParkingLots = releaseAdView.MinParkingLots,
                    MinInternalArea = releaseAdView.MinInternalArea,
                    MinSuites = releaseAdView.MinSuites,
                    MinTotalArea = releaseAdView.MinTotalArea,
                    MinTotalRooms = releaseAdView.MinTotalRooms,
                    MinValue = releaseAdView.MinValue,
                    Name = releaseAdView.Name,
                    TotalElevators = releaseAdView.TotalElevators,
                    TotalFloorUnits = releaseAdView.TotalFloorUnits,
                    TotalUnits = releaseAdView.TotalUnits,
                    TotalTowerFloors = releaseAdView.TotalTowerFloors,
                    TotalTowers = releaseAdView.TotalTowers
                };
                Entities.PropertyReleaseAds.Add(newPropertyReleaseAd);
                Entities.SaveChanges();
            }
            else
            {
                releaseAd.Address = releaseAdView.Location.AddressLine;
                releaseAd.AddressComplement = releaseAdView.AddressComplement;
                releaseAd.AddressNumber = releaseAdView.AddressNumber;
                releaseAd.ConstructorName = releaseAdView.ConstructorName;
                releaseAd.MaxExternalArea = releaseAdView.MaxExternalArea;
                releaseAd.MaxParkingLots = releaseAdView.MaxParkingLots;
                releaseAd.MaxInternalArea = releaseAdView.MaxInternalArea;
                releaseAd.MaxSuites = releaseAdView.MaxSuites;
                releaseAd.MaxTotalArea = releaseAdView.MaxTotalArea;
                releaseAd.MaxTotalRooms = releaseAdView.MaxTotalRooms;
                releaseAd.MaxValue = releaseAdView.MaxValue;
                releaseAd.MinExternalArea = releaseAdView.MinExternalArea;
                releaseAd.MinParkingLots = releaseAdView.MinParkingLots;
                releaseAd.MinInternalArea = releaseAdView.MinInternalArea;
                releaseAd.MinSuites = releaseAdView.MinSuites;
                releaseAd.MinTotalArea = releaseAdView.MinTotalArea;
                releaseAd.MinTotalRooms = releaseAdView.MinTotalRooms;
                releaseAd.MinValue = releaseAdView.MinValue;
                releaseAd.Name = releaseAdView.Name;
                releaseAd.TotalElevators = releaseAdView.TotalElevators;
                releaseAd.TotalFloorUnits = releaseAdView.TotalFloorUnits;
                releaseAd.TotalUnits = releaseAdView.TotalUnits;
                releaseAd.TotalTowerFloors = releaseAdView.TotalTowerFloors;
                releaseAd.TotalTowers = releaseAdView.TotalTowers;
                Entities.SaveChanges();
            }
        }
        public void SetPropertyAdDetails(PropertyAdTypes adType, int adCode, List<PropertyAdDetailView> details)
        {
            if (details == null)
                throw new NullReferenceException("Paramenter details can't be null");

            foreach (var item in details)
            {
                SetPropertyAdDetails(adType, adCode, item);
            }
        }
        public void SetPropertyAdDetails(PropertyAdTypes adType, int adCode, PropertyAdDetailView adDetailView)
        {
            if (adDetailView == null)
                throw new NullReferenceException("Paramenter adDetailView can't be null");

            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                var propertyAdDetail = propertyAd.PropertyAdDetails.Where(o => o.Code == adDetailView.Code).FirstOrDefault();
                if (propertyAdDetail == null)
                {
                    propertyAd.PropertyAdDetails.Add(new PropertyAdDetail
                    {
                        CustomerCode = customerCode,
                        PropertyAdType = adType.ToString(),
                        PropertyAdCode = adCode,

                        Code = adDetailView.Code,
                        Value = adDetailView.Value,
                        Type = "STRING",
                        Description = ""
                    });
                }
                else
                {
                    propertyAdDetail.Value = adDetailView.Value;
                }
                Entities.SaveChanges();
            }
        }
        public void SetPropertyAdPics(PropertyAdTypes adType, int adCode, PropertyAdPicView adPicView)
        {
            if (adPicView == null)
                throw new NullReferenceException("Paramenter adPicView can't be null");

            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                propertyAd.PropertyAdPics.Add(new PropertyAdPic
                {
                    CustomerCode = customerCode,
                    Description = adPicView.Description,
                    FileName = adPicView.FileName,
                    PictureFilePath = adPicView.Path,
                    PictureUrl = adPicView.Url,
                    PropertyAdCode = adCode,
                    PropertyAdType = adType.ToString(),
                    ThumbnailFilePath = adPicView.ThumbnailPath,
                    ThumbnailUrl = adPicView.ThumbnailUrl
                });
                Entities.SaveChanges();
            }
        }

        #endregion


        private PropertyAd getPropertyAd(PropertyAdTypes adType, int adCode)
        {
            PropertyAd result = null;
            string propertyTypeID = adType.ToString();
            var propertyAd = Entities.PropertyAds.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyTypeID & o.PropertyAdCode == adCode).FirstOrDefault();
            if (propertyAd != null)
            {
                result = propertyAd;
            }
            return result;
        }
        private PropertyAdView getPropertyAdView(PropertyAd propertyAd)
        {
            PropertyAdView result = null;
            if (propertyAd != null)
            {
                var propertyCategory = PropertyAdCategories._Undefined;
                Enum.TryParse<PropertyAdCategories>(propertyAd.CategoryName, out propertyCategory);

                string mainPicUrl = string.Empty;
                if (LoadPics)
                {
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicUrl = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().ThumbnailUrl;
                    else
                        mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivelThumb.png";
                }
                result = new PropertyAdView()
                {
                    CustomerCode = customerCode,
                    AdType = (PropertyAdTypes)Enum.Parse(typeof(PropertyAdTypes), propertyAd.PropertyAdType),
                    AdCode = propertyAd.PropertyAdCode,
                    
                    AdCategory = propertyCategory,
                    AreaDescription = propertyAd.AreaDescription,
                    Location = new Location()
                    {
                        AddressLine = "",
                        Latitude = propertyAd.PropertyLatitude,
                        Longitude = propertyAd.PropertyLongitude,
                        Locality = new GeoLocation() { Name = propertyAd.CityName, Latitude = propertyAd.CityLatitude, Longitude = propertyAd.CityLongitude },
                        District = new GeoLocation() { Name = propertyAd.DistrictName, Latitude = propertyAd.DistrictLatitude, Longitude = propertyAd.DistrictLongitude }
                    },
                    CondoDescription = propertyAd.CondoDescription,
                    FullDescription = propertyAd.FullDescription,
                    MainPicUrl = mainPicUrl,
                    InternalArea = (float)propertyAd.InternalArea,
                    Title = propertyAd.Title,
                    TotalArea = (float)propertyAd.TotalArea,
                    TotalRooms = propertyAd.TotalRooms,
                    PropertyType = propertyAd.PropertyTypeName,
                    ShortDescription = propertyAd.ShortDescription,
                    Value = (float)(propertyAd.Value),
                };
                if (LoadDetails){
                    result.Details = getPropertyAdDetailView(propertyAd);
                }
            }
            return result;
        }

        private List<PropertyAdDetailView> getPropertyAdDetailView(PropertyAd propertyAd)
        {
            var result = new List<PropertyAdDetailView>();
            if (propertyAd != null)
            {
                foreach (var detail in propertyAd.PropertyAdDetails )
                {
                    result.Add(new PropertyAdDetailView()
                    {
                        Code = detail.Code,
                        Value = detail.Value
                    });
                }
            }
            return result;
        }
        private PropertyReleaseAd getPropertyReleaseAd(int releaseAdCode)
        {
            PropertyReleaseAd result = null;
            string propertyTypeID = PropertyAdTypes.Release.ToString();
            var releaseAd = Entities.PropertyReleaseAds.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyTypeID & o.PropertyAdCode == releaseAdCode).FirstOrDefault();
            if (releaseAd != null)
            {
                //releaseAd.PropertyAdReference.Load();
                if (releaseAd.PropertyAd != null)
                {
                    //releaseAd.PropertyAd.PropertyAdDetails.Load();
                    //releaseAd.PropertyAd.PropertyAdPics.Load();
                    result = releaseAd;
                }
            }
            return result;
        }
        private PropertyReleaseAdView getPropertyReleaseAdView(PropertyReleaseAd releaseAd)
        {
            PropertyReleaseAdView result = null;
            if (releaseAd != null)
            {
                //releaseAd.PropertyAdReference.Load();
                if (releaseAd.PropertyAd == null)
                    throw new NullReferenceException("Reference releaseAd.PropertyAd can't be null");

                var propertyCategory = PropertyAdCategories._Undefined;
                Enum.TryParse<PropertyAdCategories>(releaseAd.PropertyAd.CategoryName, out propertyCategory);

                string mainPicName = string.Empty;
                //releaseAd.PropertyAd.PropertyAdPics.Load();
                if (releaseAd.PropertyAd.PropertyAdPics.Count > 0)
                    mainPicName = releaseAd.PropertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().FileName;

                result = new PropertyReleaseAdView()
                {
                    AdCategory = propertyCategory,
                    AdType = PropertyAdTypes.Release,
                    AdTypeName = "Venda",
                    Location = new Location()
                    {
                        AddressLine = "",
                        Latitude = releaseAd.PropertyAd.PropertyLatitude,
                        Longitude = releaseAd.PropertyAd.PropertyLongitude,
                        Locality = new GeoLocation() { Name = releaseAd.PropertyAd.CityName, Latitude = releaseAd.PropertyAd.CityLatitude, Longitude = releaseAd.PropertyAd.CityLongitude },
                        District = new GeoLocation() { Name = releaseAd.PropertyAd.DistrictName, Latitude = releaseAd.PropertyAd.DistrictLatitude, Longitude = releaseAd.PropertyAd.DistrictLongitude }
                    },
                    AreaDescription = releaseAd.PropertyAd.AreaDescription,
                    AdCode = releaseAd.PropertyAd.PropertyAdCode,
                    CondoDescription = releaseAd.PropertyAd.CondoDescription,
                    FullDescription = releaseAd.PropertyAd.FullDescription,
                    MainPicUrl = mainPicName,
                    MaxInternalArea = (float)releaseAd.MaxInternalArea,
                    MinInternalArea = (float)releaseAd.MinInternalArea,
                    MaxTotalArea = (float)releaseAd.MaxTotalArea,
                    MinTotalArea = (float)releaseAd.MinTotalArea,
                    MaxTotalRooms = releaseAd.MaxTotalRooms,
                    MinTotalRooms = releaseAd.MinTotalRooms,
                    PropertyType = releaseAd.PropertyAd.PropertyTypeName,
                    //PropertyTypeRoomName = PropertyTranslations.GetRoomDisplayName(releaseAd.PropertyTypeName, uiCulture),
                    ShortDescription = releaseAd.PropertyAd.ShortDescription,
                    MaxValue = (float)(releaseAd.MaxValue),
                    MinValue = (float)(releaseAd.MinValue)

                };
                setReleaseAdText(result);
            }
            return result;
        }
        private List<PropertyAdDetail> getPropertyAdDetails(PropertyAdTypes adType, int adCode)
        {
            List<PropertyAdDetail> result = new List<PropertyAdDetail>();
            string propertyTypeID = adType.ToString();
            result = Entities.PropertyAdDetails.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyTypeID & o.PropertyAdCode == adCode).ToList();
            return result;
        }
        private List<PropertyAdPic> getPropertyAdPics(PropertyAdTypes adType, int adCode)
        {
            List<PropertyAdPic> result = new List<PropertyAdPic>();
            string propertyTypeID = adType.ToString();
            result = Entities.PropertyAdPics.Where(o => o.CustomerCode == customerCode & o.PropertyAdType == propertyTypeID & o.PropertyAdCode == adCode).ToList();
            return result;
        }
        private void setReleaseAdText(PropertyReleaseAdView releaseAdView)
        {
            if (releaseAdView == null)
                throw new ArgumentNullException("Parameter releaseAdView can't be null");

            if (releaseAdView.MinValue == releaseAdView.MaxValue)
                releaseAdView.ValueText = string.Format("{0:c}", releaseAdView.MinValue);
            else
                releaseAdView.ValueText = string.Format("De {0:c} a {1:c}", releaseAdView.MinValue, releaseAdView.MaxValue);

            if (releaseAdView.MinInternalArea == releaseAdView.MaxInternalArea)
                releaseAdView.AreaText = string.Format("{0:0.##} m²", releaseAdView.MinInternalArea);
            else
                releaseAdView.AreaText = string.Format("De {0:0.##} m² a {1:0.##} m²", releaseAdView.MinInternalArea, releaseAdView.MaxInternalArea);

            if (releaseAdView.MinTotalRooms == releaseAdView.MaxTotalRooms)
                releaseAdView.RoomText = string.Format("{0} dormitório{1}", releaseAdView.MinTotalRooms, releaseAdView.MinTotalRooms > 1 ? "s" : "");
            else
                releaseAdView.RoomText = string.Format("De {0} a {1} dormitórios", releaseAdView.MinTotalRooms, releaseAdView.MaxTotalRooms);
        }







    }
}
