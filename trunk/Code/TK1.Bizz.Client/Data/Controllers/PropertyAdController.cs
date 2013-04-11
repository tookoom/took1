using System;
using System.Collections.Generic;
using System.Linq;
using TK1.Bizz.Data.Extension;
using TK1.Bizz.Data.Presentation;
using TK1.Data.Controller;
using TK1.Bizz.Client.Data.Extension;
using TK1.Bizz.Client.Data.Presentation;

namespace TK1.Bizz.Client.Data.Controller
{
    public class PropertyAdController : BizzBaseClientController
    {
        #region PRIVATE MEMBERS

        #endregion

        public PropertyAdController()
        {
        }

        #region MASTER DATA METHODS
        public List<string> GetCities()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetCities(PropertyAdTypes propertyAdType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetDistricts()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetDistricts(PropertyAdTypes propertyAdType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes()
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.PropertyAd
                          where o.Visible
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetPropertyTypes(PropertyAdTypes propertyAdType)
        {
            List<string> result = new List<string>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = (from o in Entities.PropertyAd
                          where o.PropertyAdTypeID == propertyTypeID & o.Visible
                          select o.PropertyTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyTypes", exception);
            }
            return result;
        }

        #endregion

        #region DATA BINDING METHODS
        public List<PropertyAdView> GetFeaturedPropertyAds(PropertyAdTypes propertyAdType, int count)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                var query = Entities.PropertyAd.Where(o => o.PropertyAdTypeID == propertyTypeID & o.Featured & o.Visible).Take(count);
                foreach (var propertyAd in query)
                {
                    var propertyCategory = PropertyAdCategories.Residencial;
                    if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                        propertyCategory = PropertyAdCategories.Comercial;
                    propertyAd.PropertyAdDetails.Load();
                    propertyAd.PropertyAdPics.Load();

                    string mainPicUrl = string.Empty;
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicUrl = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().PictureUrl;
                    else
                        mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivel.png";

                    PropertyAdView propertyAdView = new PropertyAdView()
                    {
                        AdCategory = propertyCategory,
                        //AdType = propertyAd.PropertyAdTypeID,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        Code = propertyAd.PropertyAdID,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        District = propertyAd.DistrictName,
                        MainPicUrl = mainPicUrl,
                        PropertyInternalArea = (float)propertyAd.InternalArea,
                        PropertyTotalArea = (float)propertyAd.TotalArea,
                        PropertyTotalRooms = propertyAd.TotalRooms,
                        PropertyType = propertyAd.PropertyTypeName,
                        Value = (float)(propertyAd.Value)
                    };
                    result.Add(propertyAdView);

                }

            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdController.GetFeaturedPropertyAds", exception);
            }
            return result;
        }
        public List<PropertyAdView> GetFeaturedRentPropertyAds()
        {
            return GetFeaturedPropertyAds(PropertyAdTypes.Rent, 5);
        }
        public List<PropertyAdView> GetFeaturedSellingPropertyAds()
        {
            return GetFeaturedPropertyAds(PropertyAdTypes.Sell, 5);
        }
        #endregion

        public PropertyAd GetPropertyAd(PropertyAdTypes propertyAdType, int propertyAdID)
        {
            PropertyAd result = null;
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                var propertyAd = Entities.PropertyAd.Get(propertyTypeID, propertyAdID);
                if (propertyAd != null)
                {
                    propertyAd.PropertyAdDetails.Load();
                    propertyAd.PropertyAdPics.Load();
                    result = propertyAd;
                }
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAd", exception);
            }
            return result;
        }
        public PropertyAd GetPropertyAd(string customerCode, PropertyAdTypes propertyAdType, int propertyAdID)
        {
            PropertyAd result = null;
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                var propertyAd = Entities.PropertyAd.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == propertyAdID).FirstOrDefault();
                if (propertyAd != null)
                {
                    propertyAd.PropertyAdDetails.Load();
                    propertyAd.PropertyAdPics.Load();
                    result = propertyAd;
                }
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAd", exception);
            }
            return result;
        }
        public List<PropertyAdDetail> GetPropertyAdDetails(PropertyAdTypes propertyAdType, int propertyAdID)
        {
            List<PropertyAdDetail> result = new List<PropertyAdDetail>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = Entities.PropertyAdDetail.Where(o => o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == propertyAdID).ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAdDetails", exception);
            }
            return result;
        }
        public List<PropertyAdDetail> GetPropertyAdDetails(string customerCode, PropertyAdTypes propertyAdType, int propertyAdID)
        {
            List<PropertyAdDetail> result = new List<PropertyAdDetail>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = Entities.PropertyAdDetail.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == propertyAdID).ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAdDetails", exception);
            }
            return result;
        }
        public List<PropertyAdPic> GetPropertyAdPics(PropertyAdTypes propertyAdType, int propertyAdID)
        {
            List<PropertyAdPic> result = new List<PropertyAdPic>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = Entities.PropertyAdPic.Where(o => o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == propertyAdID).ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAdPics", exception);
            }
            return result;
        }
        public List<PropertyAdPic> GetPropertyAdPics(string customerCode, PropertyAdTypes propertyAdType, int propertyAdID)
        {
            List<PropertyAdPic> result = new List<PropertyAdPic>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                result = Entities.PropertyAdPic.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.PropertyAdID == propertyAdID).ToList();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAdPics", exception);
            }
            return result;
        }
        public PropertyAdView GetPropertyAdView(PropertyAdTypes propertyAdType, int propertyAdID)
        {
            PropertyAdView result = null;
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                var propertyAd = Entities.PropertyAd.Get(propertyTypeID, propertyAdID);
                if (propertyAd != null)
                {
                    var propertyCategory = PropertyAdCategories.Residencial;
                    if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                        propertyCategory = PropertyAdCategories.Comercial;
                    propertyAd.PropertyAdDetails.Load();
                    propertyAd.PropertyAdPics.Load();
                    string mainPicName = string.Empty;
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicName = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().FileName;

                    result = new PropertyAdView()
                    {
                        AdCategory = propertyCategory,
                        AdType = propertyAdType,
                        AdTypeName = propertyAd.ToString(),
                        AreaDescription = propertyAd.AreaDescription,
                        City = propertyAd.CityName,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        Code = propertyAd.PropertyAdID,
                        CondoDescription = propertyAd.CondoDescription,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        District = propertyAd.DistrictName,
                        FullDescription = propertyAd.FullDescription,
                        IsAddressVisible = false,
                        MainPicUrl = mainPicName,
                        PropertyInternalArea = (float)propertyAd.InternalArea,
                        PropertyTotalArea = (float)propertyAd.TotalArea,
                        PropertyTotalRooms = propertyAd.TotalRooms,
                        PropertyType = propertyAd.PropertyTypeName,
                        ShortDescription = propertyAd.ShortDescription,
                        //PropertyTypeRoomName = propertyAd.PropertyType.RoomDisplayName,
                        Title = propertyAd.Title,
                        Value = (float)(propertyAd.Value)

                    };
                }
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyAd", exception);
            }
            return result;
        }
        public List<PropertyAdDetailView> GetPropertyDetail(PropertyAdTypes propertyAdType, int propertyAdID)
        {
            List<PropertyAdDetailView> result = new List<PropertyAdDetailView>();
            try
            {
                string propertyTypeID = propertyAdType.ToString();
                var propertyAd = Entities.PropertyAd.Get(propertyTypeID, propertyAdID);
                if (propertyAd != null)
                {
                    propertyAd.PropertyAdDetails.Load();
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

            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.GetPropertyDetail", exception);
            }
            return result;
        }
        public string GetPropertyPicDescription(string fileName)
        {
            string result = null;
            if (fileName != null)
            {
                try
                {
                    result = (from o in Entities.PropertyAdPic
                              where o.FileName == fileName
                              select o.Description).FirstOrDefault();

                }
                catch (Exception exception)
                {
                    AppLogClientController.WriteException("PropertyController.GetPropertyPicDescription", exception);
                }
            }
            return result;
        }
        public List<PropertyAdView> SearchPropertyAds(PropertyAdSearchParameters parameters)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            try
            {
                if (parameters != null)
                {
                    var query = Entities.PropertyAd.Where(o =>o.Visible & o.PropertyAdTypeID == parameters.AdType.ToString());
                    //var list = query.ToList();
                    if (parameters.Code > 0)
                    {
                        query = query.FilterCode(parameters.Code);
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
                    foreach (var propertyAd in query)
                    {
                        var propertyCategory = PropertyAdCategories.Residencial;
                        if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                            propertyCategory = PropertyAdCategories.Comercial;
                        propertyAd.PropertyAdDetails.Load();
                        propertyAd.PropertyAdPics.Load();

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
                            Code = propertyAd.PropertyAdID,
                            CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                            District = propertyAd.DistrictName,
                            MainPicUrl = mainPicUrl,
                            PropertyInternalArea = (float)propertyAd.InternalArea,
                            PropertyTotalArea = (float)propertyAd.TotalArea,
                            PropertyTotalRooms = propertyAd.TotalRooms,
                            PropertyType = propertyAd.PropertyTypeName,
                            Value = (float)(propertyAd.Value)

                        };
                        result.Add(propertyAdView);
                    }
                }
                if (parameters.ResultOrdering != PropertyAdSearchResultOrders._Undefined)
                    result = OrderResults(result, parameters.ResultOrdering);

            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyController.SearchPropertyAds", exception);
            }
            return result;
        }
        public void RemovePropertyAdDetails(string customerCode, PropertyAdTypes propertyAdType, int propertyAdID)
        {
            if (customerCode == null)
                throw new NullReferenceException("Paramenter customerCode can't be null");

            var details = GetPropertyAdDetails(customerCode, propertyAdType, propertyAdID);
            foreach (var item in details)
            {
                Entities.DeleteObject(item);
            }
            Entities.SaveChanges();
        }
        public void RemovePropertyAdPics(string customerCode, PropertyAdTypes propertyAdType, int propertyAdID)
        {
            if (customerCode == null)
                throw new NullReferenceException("Paramenter customerCode can't be null");

            var details = GetPropertyAdPics(customerCode, propertyAdType, propertyAdID);
            foreach (var item in details)
            {
                Entities.DeleteObject(item);
            }
            Entities.SaveChanges();
        }
        public void SetPropertyAd(string customerCode, PropertyAdView propertyAdView)
        {
            if (propertyAdView == null)
                throw new NullReferenceException("Paramenter propertyAdView can't be null");

            var propertyAd = GetPropertyAd(customerCode, propertyAdView.AdType, propertyAdView.Code);
            if (propertyAd == null)
            {
                var newPropertyAd = new PropertyAd()
                {
                    //PRIMARY KEY
                    PropertyAdID = propertyAdView.Code,
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
                    InternalArea = propertyAdView.PropertyInternalArea,
                    PropertyTypeName = propertyAdView.PropertyType,
                    PicUrl = propertyAdView.MainPicUrl,
                    PropertyAdStatusID = "ACTIVE",
                    ShortDescription = propertyAdView.ShortDescription,
                    Title = propertyAdView.Title,
                    TotalArea = propertyAdView.PropertyTotalArea,
                    TotalRooms = propertyAdView.PropertyTotalRooms,
                    Value = propertyAdView.Value,
                    Visible = true
                };
                Entities.PropertyAd.AddObject(newPropertyAd);
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
                propertyAd.InternalArea = propertyAdView.PropertyInternalArea;
                propertyAd.PropertyTypeName = propertyAdView.PropertyType;
                propertyAd.PicUrl = propertyAdView.MainPicUrl;
                propertyAd.PropertyAdStatusID = "ACTIVE";
                propertyAd.ShortDescription = propertyAdView.ShortDescription;
                propertyAd.Title = propertyAdView.Title;
                propertyAd.TotalArea = propertyAdView.PropertyTotalArea;
                propertyAd.TotalRooms = propertyAdView.PropertyTotalRooms;
                propertyAd.Value = propertyAdView.Value;
                propertyAd.Visible = true;
                Entities.SaveChanges();
            }
        }
        public void SetPropertyAdDetails(string customerCode, PropertyAdTypes propertyAdType, int propertyAdCode, PropertyAdDetailView propertyAdDetailView)
        {
            if (customerCode == null)
                throw new NullReferenceException("Paramenter customerCode can't be null");
            if (propertyAdDetailView == null)
                throw new NullReferenceException("Paramenter propertyAdDetailView can't be null");

            var propertyAd = GetPropertyAd(propertyAdType, propertyAdCode);
            if (propertyAd != null)
            {
                propertyAd.PropertyAdDetails.Add(new PropertyAdDetail
                {
                    CustomerCode = customerCode,
                    Description = propertyAdDetailView.Name,
                    PropertyAdID = propertyAdCode,
                    PropertyAdTypeID = propertyAdType.ToString(),
                    Type = "",
                    Value = propertyAdDetailView.Value
                });
                Entities.SaveChanges();
            }
        }
        public void SetPropertyAdPics(string customerCode, PropertyAdTypes propertyAdType, int propertyAdCode, PropertyAdPicView propertyAdPicView)
        {
            if (customerCode == null)
                throw new NullReferenceException("Paramenter customerCode can't be null");
            if (propertyAdPicView == null)
                throw new NullReferenceException("Paramenter propertyAdPicView can't be null");

            var propertyAd = GetPropertyAd(propertyAdType, propertyAdCode);
            if (propertyAd != null)
            {
                propertyAd.PropertyAdPics.Add(new PropertyAdPic
                {
                    CustomerCode = customerCode,
                    Description = propertyAdPicView.Description,
                    FileName = propertyAdPicView.FileName,
                    PictureFilePath = propertyAdPicView.Path,
                    PictureUrl = propertyAdPicView.Url,
                    PropertyAdID = propertyAdCode,
                    PropertyAdTypeID = propertyAdType.ToString(),
                    ThumbnailFilePath = propertyAdPicView.ThumbnailPath,
                    ThumbnailUrl = propertyAdPicView.ThumbnailUrl
                });
                Entities.SaveChanges();
            }
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
                            result = propertyAds.OrderBy(o => o.PropertyTotalArea).ToList();
                            break;
                        case PropertyAdSearchResultOrders.AreaDescending:
                            result = propertyAds.OrderByDescending(o => o.PropertyTotalArea).ToList();
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



    }
}
