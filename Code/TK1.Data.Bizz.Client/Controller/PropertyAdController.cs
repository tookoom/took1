using System;
using System.Collections.Generic;
using System.Linq;
using TK1.Data.Bizz.Client.Model.Extension;
using TK1.Data.Bizz.Client.Model.Extension;
using TK1.Bizz.Broker.Presentation;
using TK1.Data.Bizz.Client.Model;
using TK1.Bizz.Broker.Presentation.Culture;

namespace TK1.Data.Bizz.Client.Controller
{
    public class PropertyAdController : BizzBaseClientController
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
        public string UICulture
        {
            get { return uiCulture; }
            set { uiCulture = value; }
        }
        #endregion

        public PropertyAdController(string customerCode)
        {
            this.customerCode = customerCode;
        }

        #region MASTER DATA METHODS
        public List<string> GetCities()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible & o.CustomerCode == customerCode
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetCities(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetDistricts()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible & o.CustomerCode == customerCode
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetDistricts(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyCategories()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible & o.CustomerCode == customerCode
                          select o.CategoryName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyCategories(PropertyAdCategories adCategory)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyCategorieName = adCategory.ToString();
                result = (from o in Entities.PropertyAd
                          where o.CategoryName == propertyCategorieName & o.Visible & o.CustomerCode == customerCode
                          select o.CategoryName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetPropertyCategories", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible & o.CustomerCode == customerCode
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes(PropertyAdTypes adType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = adType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible & o.CustomerCode == customerCode
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetPropertyTypes", exception);
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
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == adTypeName & o.CategoryName == categoryName & o.Visible & o.CustomerCode == customerCode
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAsController.GetPropertyTypes", exception);
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
                        AdCategory = propertyCategory,
                        AdType = adType,
                        AdTypeName = PropertyTranslations.GetAdTypeDisplayName(adType, uiCulture),
                        AreaDescription = propertyAd.AreaDescription,
                        City = propertyAd.CityName,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        AdCode = propertyAd.PropertyAdID,
                        CondoDescription = propertyAd.CondoDescription,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        District = propertyAd.DistrictName,
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
                AppLogClientController.WriteException("PropertyAsController.GetPropertyAd", exception);
            }
            return result;
        }
        public PropertyReleaseAdView GetPropertyReleaseAdView(int releaseAdCode)
        {
            var releaseAd = getPropertyReleaseAd(releaseAdCode);
            var result = getPropertyReleaseAdView(releaseAd);
            return result;
        }

        //VIEW COLLECTIONS
        public List<PropertyAdView> GetPropertyAds(PropertyAdTypes adType)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            string propertyTypeID = adType.ToString();
            foreach (var item in Entities.PropertyAd.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID).ToList())
            {
                PropertyAdView propertyAdView = new PropertyAdView()
                {
                    AdType = adType,
                    AdCode = item.PropertyAdID,
                    CustomerCode = customerCode
                };
                result.Add(propertyAdView);
            }

            return result;
        }
        public List<PropertyReleaseAdView> GetPropertyReleaseAds()
        {
            List<PropertyReleaseAdView> result = new List<PropertyReleaseAdView>();
            foreach (var releaseAd in Entities.PropertyReleaseAd.Where(o => o.CustomerCode == this.customerCode).ToList())
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
                            Name = detail.Value + " " + detail.Description,
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
        public List<PropertyAdView> SearchPropertyAds(PropertyAdSearchParameters parameters)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            if (parameters != null)
            {
                var adTypeID = parameters.AdType.ToString();
                var query = Entities.PropertyAd.Where(o => o.CustomerCode == customerCode & o.Visible & o.PropertyAdTypeID == adTypeID);
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
                foreach (var propertyAd in query.ToList())
                {
                    var propertyCategory = PropertyAdCategories.Residencial;
                    if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                        propertyCategory = PropertyAdCategories.Comercial;
                    //propertyAd.PropertyAdDetails.Load();
                    //propertyAd.PropertyAdPics.Load();

                    string mainPicUrl = string.Empty;
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicUrl = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().ThumbnailUrl;
                    else
                        mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivelThumb.png";

                    PropertyAdView propertyAdView = new PropertyAdView()
                    {
                        AdCategory = propertyCategory,
                        AdType = parameters.AdType,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        AdCode = propertyAd.PropertyAdID,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        District = propertyAd.DistrictName,
                        MainPicUrl = mainPicUrl,
                        InternalArea = (float)propertyAd.InternalArea,
                        TotalArea = (float)propertyAd.TotalArea,
                        TotalRooms = propertyAd.TotalRooms,
                        PropertyType = propertyAd.PropertyTypeName,
                        Value = (float)(propertyAd.Value)

                    };
                    result.Add(propertyAdView);
                }
            }
            if (parameters.ResultOrdering != PropertyAdSearchResultOrders._Undefined)
                result = OrderResults(result, parameters.ResultOrdering);
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

        //public string GetPropertyPicDescription(string fileName)
        //{
        //    string result = null;
        //    if (fileName != null)
        //    {
        //        try
        //        {
        //            result = (from o in Entities.PropertyAdPic
        //                      where o.FileName == fileName
        //                      select o.Description).FirstOrDefault();

        //        }
        //        catch (Exception exception)
        //        {
        //            AppLogClientController.WriteException("PropertyAsController.GetPropertyPicDescription", exception);
        //        }
        //    }
        //    return result;
        //}

        #endregion

        #region INTEGRATION METHODS
        public void RemovePropertyAd(PropertyAdTypes adType, int adCode)
        {
            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                //propertyAd.PropertyAdDetails.Load();
                //propertyAd.PropertyAdPics.Load();
                //propertyAd.PropertyReleaseAdReference.Load();

                foreach (var item in propertyAd.PropertyAdDetails.ToList())
                    Entities.PropertyAdDetail.Remove(item);
                foreach (var item in propertyAd.PropertyAdPics.ToList())
                    Entities.PropertyAdPic.Remove(item);
                if (propertyAd.PropertyReleaseAd != null)
                    Entities.PropertyReleaseAd.Remove(propertyAd.PropertyReleaseAd);

                Entities.PropertyAd.Remove(propertyAd);
                Entities.SaveChanges();
            }
        }
        public void RemovePropertyAdDetails(PropertyAdTypes adType, int adCode)
        {
            var details = getPropertyAdDetails(adType, adCode);
            foreach (var item in details)
            {
                Entities.PropertyAdDetail.Remove(item);
            }
            Entities.SaveChanges();
        }
        public void RemovePropertyAdPics(PropertyAdTypes adType, int adCode)
        {
            var pics = getPropertyAdPics(adType, adCode);
            foreach (var item in pics)
            {
                Entities.PropertyAdPic.Remove(item);
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
                    PropertyAdID = propertyAdView.AdCode,
                    PropertyAdTypeID = propertyAdView.AdType.ToString(),
                    CustomerCode = customerCode,

                    //OTHER COLUMNS
                    AreaDescription = propertyAdView.AreaDescription,
                    CategoryName = propertyAdView.AdCategory.ToString(),
                    CityName = propertyAdView.City,
                    CityTaxes = propertyAdView.CityTaxes,
                    CondoDescription = propertyAdView.CondoDescription,
                    CondoTaxes = propertyAdView.CondoTaxes,
                    DistrictName = propertyAdView.District,
                    ExternalArea = 0,
                    Featured = propertyAdView.IsFeatured,
                    FullDescription = propertyAdView.FullDescription,
                    InternalArea = propertyAdView.InternalArea,
                    PropertyTypeName = propertyAdView.PropertyType,
                    PicUrl = propertyAdView.MainPicUrl,
                    PropertyAdStatusID = "ACTIVE",
                    ShortDescription = propertyAdView.ShortDescription,
                    Title = propertyAdView.Title,
                    TotalArea = propertyAdView.TotalArea,
                    TotalRooms = propertyAdView.TotalRooms,
                    Value = propertyAdView.Value,
                    Visible = true
                };
                Entities.PropertyAd.Add(newPropertyAd);
                Entities.SaveChanges();
            }
            else
            {
                propertyAd.AreaDescription = propertyAdView.AreaDescription;
                propertyAd.CategoryName = propertyAdView.AdCategory.ToString();
                propertyAd.CityName = propertyAdView.City;
                propertyAd.CityTaxes = propertyAdView.CityTaxes;
                propertyAd.CustomerCode = customerCode;
                propertyAd.CondoDescription = propertyAdView.CondoDescription;
                propertyAd.CondoTaxes = propertyAdView.CondoTaxes;
                propertyAd.DistrictName = propertyAdView.District;
                propertyAd.ExternalArea = 0;
                propertyAd.Featured = propertyAdView.IsFeatured;
                propertyAd.FullDescription = propertyAdView.FullDescription;
                propertyAd.InternalArea = propertyAdView.InternalArea;
                propertyAd.PropertyTypeName = propertyAdView.PropertyType;
                propertyAd.PicUrl = propertyAdView.MainPicUrl;
                propertyAd.PropertyAdStatusID = "ACTIVE";
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
                    PropertyAdID = releaseAdView.AdCode,
                    PropertyAdTypeID = PropertyAdTypes.Release.ToString(),
                    CustomerCode = customerCode,

                    //OTHER COLUMNS
                    Address = releaseAdView.Address,
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
                Entities.PropertyReleaseAd.Add(newPropertyReleaseAd);
                Entities.SaveChanges();
            }
            else
            {
                releaseAd.Address = releaseAdView.Address;
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
        public void SetPropertyAdDetails(PropertyAdTypes adType, int adCode, PropertyAdDetailView adDetailView)
        {
            if (adDetailView == null)
                throw new NullReferenceException("Paramenter adDetailView can't be null");

            var propertyAd = getPropertyAd(adType, adCode);
            if (propertyAd != null)
            {
                propertyAd.PropertyAdDetails.Add(new PropertyAdDetail
                {
                    CustomerCode = customerCode,
                    Description = adDetailView.Name,
                    PropertyAdID = adCode,
                    PropertyAdTypeID = adType.ToString(),
                    Type = "",
                    Value = adDetailView.Value
                });
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
                    PropertyAdID = adCode,
                    PropertyAdTypeID = adType.ToString(),
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
            var propertyAd = Entities.PropertyAd.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == adCode).FirstOrDefault();
            if (propertyAd != null)
            {
                //propertyAd.PropertyAdDetails.Load();
                //propertyAd.PropertyAdPics.Load();
                result = propertyAd;
            }
            return result;
        }
        private PropertyReleaseAd getPropertyReleaseAd(int releaseAdCode)
        {
            PropertyReleaseAd result = null;
            string propertyTypeID = PropertyAdTypes.Release.ToString();
            var releaseAd = Entities.PropertyReleaseAd.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == releaseAdCode).FirstOrDefault();
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
                    AreaDescription = releaseAd.PropertyAd.AreaDescription,
                    City = releaseAd.PropertyAd.CityName,
                    AdCode = releaseAd.PropertyAd.PropertyAdID,
                    CondoDescription = releaseAd.PropertyAd.CondoDescription,
                    District = releaseAd.PropertyAd.DistrictName,
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
            result = Entities.PropertyAdDetail.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == adCode).ToList();
            return result;
        }
        private List<PropertyAdPic> getPropertyAdPics(PropertyAdTypes adType, int adCode)
        {
            List<PropertyAdPic> result = new List<PropertyAdPic>();
            string propertyTypeID = adType.ToString();
            result = Entities.PropertyAdPic.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == adCode).ToList();
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
