using System;
using System.Collections.Generic;
using System.Linq;
using TK1.Bizz.Data.Extension;
using TK1.Bizz.Data.Presentation;
using TK1.Data.Controller;

namespace TK1.Bizz.Data.Controller
{
    public class SiteAdController : BizzBaseController
    {
        #region PRIVATE MEMBERS
        private AuditController audit;

        #endregion

        public SiteAdController()
        {
            audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.TK1.ToString());
        }
        public SiteAdController(string customerName)
        {
            audit = new AuditController(AppNames.BizzSites.ToString(), string.IsNullOrEmpty(customerName) ? CustomerNames.TK1.ToString() : customerName);
        }
        public SiteAdController(AuditController audit)
        {
            this.audit = audit;
        }

        #region MASTER DATA METHODS
        public List<string> GetCities(string customerCodename)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.Visible
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetCities(string customerCodename, SiteAdTypes siteAdType)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)siteAdType & o.Visible
                          select o.CityName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetCities", exception);
            }
            return result;
        }
        public List<string> GetDistricts(string customerCodename)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.Visible
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetDistricts(string customerCodename, SiteAdTypes siteAdType)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)siteAdType & o.Visible
                          select o.DistrictName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetSiteTypes(string customerCodename)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.Visible
                          select o.SiteTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetDistricts", exception);
            }
            return result;
        }
        public List<string> GetSiteTypes(string customerCodename, SiteAdTypes siteAdType)
        {
            List<string> result = new List<string>();
            try
            {
                result = (from o in Entities.SiteAds
                          where o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)siteAdType & o.Visible
                          select o.SiteTypeName).Distinct().ToList();
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteTypes", exception);
            }
            return result;
        }

        #endregion

        #region DATA BINDING METHODS
        public List<SiteAdView> GetFeaturedSiteAds(string customerName, SiteAdTypes siteAdType, int count)
        {
            List<SiteAdView> result = new List<SiteAdView>();
            try
            {
                var query = Entities.SiteAds.Where(o => o.CustomerCodename == customerName & o.SiteAdTypeID == (int)siteAdType & o.FeaturedAd == true & o.Visible).Take(count);
                foreach (var siteAd in query)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    if (siteAd.CategoryName != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    siteAd.SiteAdDetails.Load();
                    siteAd.SiteAdPics.Load();

                    string mainPicUrl = string.Empty;
                    if (siteAd.SiteAdPics.Count > 0)
                        mainPicUrl = siteAd.SiteAdPics.OrderBy(o => o.PicID).FirstOrDefault().PictureUrl;
                    else
                        mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivel.png";

                    SiteAdView siteAdView = new SiteAdView()
                    {
                        AdCategory = siteCategory,
                        AdType = (SiteAdTypes)siteAd.SiteAdTypeID,
                        CityTaxes = (float)(siteAd.CityTaxes ?? 0),
                        Code = siteAd.SiteAdID,
                        CondoTaxes = (float)(siteAd.CondoTaxes ?? 0),
                        District = siteAd.DistrictName,
                        MainPicUrl = mainPicUrl,
                        SiteInternalArea = (float)siteAd.InternalArea,
                        SiteTotalArea = (float)siteAd.TotalArea,
                        SiteTotalRooms = siteAd.TotalRooms,
                        SiteType = siteAd.SiteTypeName,
                        Value = (float)(siteAd.Value)
                    };
                    result.Add(siteAdView);

                }

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteAdController.GetFeaturedSiteAds", exception);
            }
            return result;
        }
        public List<SiteAdView> GetFeaturedRentSiteAds(string customerName)
        {
            return GetFeaturedSiteAds(customerName, SiteAdTypes.Rent, 5);
        }
        public List<SiteAdView> GetFeaturedSellingSiteAds(string customerName)
        {
            return GetFeaturedSiteAds(customerName, SiteAdTypes.Sell, 5);
        }
        #endregion

        public SiteAd GetSiteAd(string customerCodename, int siteAdTypeID, int siteAdID)
        {
            SiteAd result = null;
            try
            {
                var siteAd = Entities.SiteAds.Get(customerCodename, siteAdTypeID, siteAdID);
                if (siteAd != null)
                {
                    siteAd.SiteAdDetails.Load();
                    siteAd.SiteAdPics.Load();
                    siteAd.SiteAdTypeReference.Load();
                    result = siteAd;
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public SiteAdView GetSiteAdView(string customerCodename, int siteAdTypeID, int siteAdID)
        {
            SiteAdView result = null;
            try
            {
                var siteAd = Entities.SiteAds.Get(customerCodename,siteAdTypeID, siteAdID);
                if (siteAd != null)
                {
                    var siteCategory = SiteAdCategories.Residencial;
                    if (siteAd.CategoryName != SiteAdCategories.Residencial.ToString())
                        siteCategory = SiteAdCategories.Comercial;
                    //ad.Site.AddressInfoReference.Load();
                    siteAd.SiteAdDetails.Load();
                    siteAd.SiteAdPics.Load();
                    siteAd.SiteAdTypeReference.Load();
                    string mainPicName = string.Empty;
                    if (siteAd.SiteAdPics.Count > 0)
                        mainPicName = siteAd.SiteAdPics.OrderBy(o => o.PicID).FirstOrDefault().FileName;

                    result = new SiteAdView()
                    {
                        AdCategory = siteCategory,
                        AdType = (SiteAdTypes)siteAdTypeID,
                        AdTypeName = siteAd.SiteAdType.Description,
                        City = siteAd.CityName,
                        CityTaxes = (float)(siteAd.CityTaxes ?? 0),
                        Code = siteAd.SiteAdID,
                        CondoTaxes = (float)(siteAd.CondoTaxes ?? 0),
                        District = siteAd.DistrictName,
                        FullDescription = siteAd.FullDescription,
                        MainPicUrl = mainPicName,
                        SiteInternalArea = (float)siteAd.InternalArea,
                        SiteTotalArea = (float)siteAd.TotalArea,
                        SiteTotalRooms = siteAd.TotalRooms,
                        SiteType = siteAd.SiteTypeName,
                        ShortDescription = siteAd.ShortDescription,
                        //SiteTypeRoomName = siteAd.SiteType.RoomDisplayName,
                        Value = (float)(siteAd.Value)

                    };
                }
            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.GetSiteAd", exception);
            }
            return result;
        }
        public List<SiteDetail> GetSiteDetail(string customerCodename, int siteAdTypeID, int siteAdID)
        {
            List<SiteDetail> result = new List<SiteDetail>();
            try
            {
                var siteAd = Entities.SiteAds.Get(customerCodename, siteAdTypeID, siteAdID);
                if (siteAd != null)
                {
                    siteAd.SiteAdDetails.Load();
                    if (siteAd.SiteAdDetails != null)
                    {
                        var query = from o in siteAd.SiteAdDetails
                                    select o;
                        foreach (var detail in query)
                        {
                            result.Add(new SiteDetail()
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
                audit.WriteException("SiteController.GetSiteDetail", exception);
            }
            return result;
        }
        public string GetSitePicDescription(string customerCodename, string fileName)
        {
            string result = null;
            if (fileName != null)
            {
                try
                {
                    result = (from o in Entities.SiteAdPics
                              where o.CustomerCodename == customerCodename & o.FileName == fileName
                              select o.Description).FirstOrDefault();

                }
                catch (Exception exception)
                {
                    audit.WriteException("SiteController.GetSitePicDescription", exception);
                }
            }
            return result;
        }
        public List<SiteAdView> SearchSites(SiteAdSearchParameters parameters)
        {
            List<SiteAdView> result = new List<SiteAdView>();
            try
            {
                if (parameters != null)
                {
                    var query = Entities.SiteAds.Where(o => o.CustomerCodename == parameters.CustomerCodename & o.Visible & o.SiteAdTypeID == parameters.AdTypeID);
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
                        if (parameters.SiteType != "*")
                            query = query.FilterSiteType(parameters.SiteType);
                        if (!parameters.Districts.Contains("*"))
                            query = query.FilterDistrict(parameters.Districts);


                    }
                    foreach (var siteAd in query)
                    {
                        var siteCategory = SiteAdCategories.Residencial;
                        if (siteAd.CategoryName != SiteAdCategories.Residencial.ToString())
                            siteCategory = SiteAdCategories.Comercial;
                        siteAd.SiteAdDetails.Load();
                        siteAd.SiteAdPics.Load();

                        string mainPicUrl = string.Empty;
                        if (siteAd.SiteAdPics.Count > 0)
                            mainPicUrl = siteAd.SiteAdPics.OrderBy(o => o.PicID).FirstOrDefault().ThumbnailUrl;
                        else
                            mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivelThumb.png";

                        SiteAdView siteAdView = new SiteAdView()
                        {
                            AdCategory = siteCategory,
                            AdType = parameters.AdType,
                            CityTaxes = (float)(siteAd.CityTaxes ?? 0),
                            Code = siteAd.SiteAdID,
                            CondoTaxes = (float)(siteAd.CondoTaxes ?? 0),
                            District = siteAd.DistrictName,
                            MainPicUrl = mainPicUrl,
                            SiteInternalArea = (float)siteAd.InternalArea,
                            SiteTotalArea = (float)siteAd.TotalArea,
                            SiteTotalRooms = siteAd.TotalRooms,
                            SiteType = siteAd.SiteTypeName,
                            Value = (float)(siteAd.Value)

                        };
                        result.Add(siteAdView);
                    }
                }
                if (parameters.ResultOrdering != SiteAdSearchResultOrders._Undefined)
                    result = OrderResults(result, parameters.ResultOrdering);

            }
            catch (Exception exception)
            {
                audit.WriteException("SiteController.SearchSites", exception);
            }
            return result;
        }

        public static List<SiteAdView> OrderResults(List<SiteAdView> siteAds, SiteAdSearchResultOrders resultOrder)
        {
            List<SiteAdView> result = null;
            if (siteAds != null & resultOrder != SiteAdSearchResultOrders._Undefined)
            {
                try
                {
                    switch (resultOrder)
                    {
                        case SiteAdSearchResultOrders.AreaAscending:
                            result = siteAds.OrderBy(o => o.SiteTotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.AreaDescending:
                            result = siteAds.OrderByDescending(o => o.SiteTotalArea).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceAscending:
                            result = siteAds.OrderBy(o => o.Value).ToList();
                            break;
                        case SiteAdSearchResultOrders.PriceDescending:
                            result = siteAds.OrderByDescending(o => o.Value).ToList();
                            break;
                    }
                }
                catch
                {
                    result = siteAds;
                }
            }
            if (result == null)
                result = new List<SiteAdView>();
            return result;
        }

    }
}
