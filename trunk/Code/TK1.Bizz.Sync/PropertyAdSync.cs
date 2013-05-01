using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Data.Controller;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Client.Data.Controller;
using TK1.Bizz.Client.Data.Presentation;
using TK1.Bizz.Mdo.Data.Controller;

namespace TK1.Bizz.Sync
{
    public class PropertyAdSync
    {
        private string customerCode = string.Empty;

        public PropertyAdSync(string customerCode)
        {
            this.customerCode = customerCode;
        }

        public bool UpdateClientBizzPropertyAds()
        {
            bool result = false;
            var controller = new SiteAdController(customerCode);
            var propertyAds = controller.GetSiteAdsToSync(customerCode);
            foreach (var ad in propertyAds)
            {
                var siteAdView = controller.GetSiteAdView(customerCode, ad.SiteAdTypeID, ad.SiteAdID);
                var siteAdDetails = controller.GetSiteDetail(customerCode, siteAdView.AdTypeID, siteAdView.Code);
                var siteAdPics = controller.GetSiteAdPics(customerCode, siteAdView.AdTypeID, siteAdView.Code);
                importSiteAd(siteAdView);
                importSiteAdDetails(siteAdView, siteAdDetails);
                importSiteAdPics(siteAdView, siteAdPics);
            }
            return result;
        }
        public bool UpdateClientMdoPropertyAds()
        {
            bool result = false;
            var controller = new MdoSiteAdController();
            var siteAds = controller.GetSiteAdsToSync(customerCode);
            foreach (var ad in siteAds)
            {
                int customerID = ad.CustomerID;
                var siteAdView = controller.GetSiteAdView(customerID, ad.SiteAdID);
                var siteAdDetails = controller.GetSiteDetail(customerID, ad.SiteAdID);
                var siteAdPics = controller.GetSitePics(customerID, ad.SiteAdID);
                importSiteAd(siteAdView);
                importSiteAdDetails(siteAdView, siteAdDetails);
                importSiteAdPics(siteAdView, siteAdPics);
            }
            return result;
        }

        private void importSiteAdDetails(SiteAdView siteAdView, List<SiteDetail> siteAdDetails)
        {
            if (siteAdView == null)
                throw new NullReferenceException("Paramenter siteAdView can't be null");
            if (siteAdDetails == null)
                throw new NullReferenceException("Paramenter siteAdDetails can't be null");


            var propertyAdController = new PropertyAdController(customerCode);
            var propertyAdType = getPropertyAdTypes(siteAdView.AdTypeID);

            propertyAdController.RemovePropertyAdDetails(propertyAdType, siteAdView.Code);

            foreach (var item in siteAdDetails)
            {
                propertyAdController.SetPropertyAdDetails(propertyAdType, siteAdView.Code, new PropertyAdDetailView
                {
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Value = item.Value
                });
            }

        }
        private void importSiteAdPics(SiteAdView siteAdView, List<SiteAdPicView> siteAdPics)
        {
            if (siteAdView == null)
                throw new NullReferenceException("Paramenter siteAdView can't be null");
            if (siteAdPics == null)
                throw new NullReferenceException("Paramenter siteAdPics can't be null");


            var propertyAdController = new PropertyAdController(customerCode);
            var propertyAdType = getPropertyAdTypes(siteAdView.AdTypeID);

            propertyAdController.RemovePropertyAdPics(propertyAdType, siteAdView.Code);

            foreach (var item in siteAdPics)
            {
                propertyAdController.SetPropertyAdPics(propertyAdType, siteAdView.Code, new PropertyAdPicView
                {
                    Description = item.Description,
                    FileName = item.FileName,
                    Index = item.Index,
                    Path = item.Path,
                    ThumbnailPath = item.ThumbnailPath,
                    ThumbnailUrl = item.ThumbnailUrl,
                    Url = item.Url
                });
            }

        }
        private void importSiteAd(SiteAdView siteAdView)
        {
            if (siteAdView != null)
            {
                var propertyAdController = new PropertyAdController(customerCode);
                var propertyAdCategory = getPropertyAdCategories(siteAdView.AdCategory);
                var propertyAdType = getPropertyAdTypes(siteAdView.AdTypeID);

                propertyAdController.SetPropertyAd(new PropertyAdView()
                {
                    AdCategory = propertyAdCategory,
                    Address = siteAdView.Address,
                    AdType = propertyAdType,
                    AdTypeName = propertyAdType.ToString(),
                    AreaDescription = siteAdView.AreaDescription,
                    City = siteAdView.City,
                    CityTaxes = siteAdView.CityTaxes,
                    AdCode = siteAdView.Code,
                    CondoDescription = siteAdView.CondoDescription,
                    CondoTaxes = siteAdView.CondoTaxes,
                    District = siteAdView.District,
                    FullDescription = siteAdView.FullDescription,
                    IsFeatured = siteAdView.IsFeatured,
                    MainPicUrl = siteAdView.MainPicUrl,
                    InternalArea = siteAdView.SiteInternalArea,
                    TotalArea = siteAdView.SiteTotalArea,
                    TotalRooms = siteAdView.SiteTotalRooms,
                    PropertyType = siteAdView.SiteType,
                    PropertyTypeRoomName = siteAdView.SiteTypeRoomName,
                    ShortDescription = siteAdView.ShortDescription,
                    Title = siteAdView.Title,
                    Value = siteAdView.Value
                });
            }
        }

        private PropertyAdCategories getPropertyAdCategories(SiteAdCategories siteAdCategory)
        {
            switch (siteAdCategory)
            {
                case SiteAdCategories.Comercial: return PropertyAdCategories.Comercial;
                case SiteAdCategories.Residencial: return PropertyAdCategories.Residencial;
                default: throw new ArgumentOutOfRangeException("Parameter propertyCategoryName is not valid");
            }
        }

        private PropertyAdTypes getPropertyAdTypes(int propertyAdID)
        {
            switch (propertyAdID)
            {
                case 1: return PropertyAdTypes.Rent;
                case 2: return PropertyAdTypes.Sell;
                case 3: return PropertyAdTypes.Release;
                default: throw new ArgumentOutOfRangeException("Parameter propertyAdID is not valid");
            }
        }
    }
}
