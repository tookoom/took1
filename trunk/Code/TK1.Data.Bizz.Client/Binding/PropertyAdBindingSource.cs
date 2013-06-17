using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Presentation;
using TK1.Bizz.Broker.Presentation.Culture;
using TK1.Data.Bizz.Client.Controller;

namespace TK1.Data.Bizz.Client.Binding
{
    public class PropertyAdBindingSource : BizzBaseClientController
    {
        #region PRIVATE MEMBERS
        private string uiCulture = "pt-BR";
        #endregion
        #region PUBLIC PROPERTIES
        public string UICulture
        {
            get { return uiCulture; }
            set { uiCulture = value; }
        }
        #endregion

        public PropertyAdBindingSource() { }

        public List<PropertyAdView> GetFeaturedRentPropertyAds(string customerCode, int count)
        {
            return getFeaturedPropertyAds(customerCode, PropertyAdTypes.Rent, 5);
        }
        public List<PropertyAdView> GetFeaturedSellingPropertyAds(string customerCode, int count)
        {
            return getFeaturedPropertyAds(customerCode, PropertyAdTypes.Sell, count);
        }
        public List<PropertyAdDetailView> GetPropertyAdDetails(string customerCode, string adType, int adCode)
        {
            List<PropertyAdDetailView> result = new List<PropertyAdDetailView>();
            try
            {
                PropertyAdController controller = new PropertyAdController(customerCode);
                var propertyAdType = PropertyAdTypes._Undefined;
                if(Enum.TryParse<PropertyAdTypes>(adType, out propertyAdType))
                    result = controller.GetPropertyDetailViews(propertyAdType, adCode);
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdBindingSource.GetPropertyAdDetails", exception);
            }
            return result;
        }
        public PropertyAdView GetPropertyAd(string customerCode, string adType, int adCode)
        {
            var result = new PropertyAdView();
            try
            {
                PropertyAdController controller = new PropertyAdController(customerCode);
                var propertyAdType = PropertyAdTypes._Undefined;
                if (Enum.TryParse<PropertyAdTypes>(adType, out propertyAdType))
                    result = controller.GetPropertyAdView(propertyAdType, adCode);
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdBindingSource.GetPropertyAd", exception);
            }
            return result;
        }
        public PropertyReleaseAdView GetPropertyReleaseAd(string customerCode, int adCode)
        {
            var result = new PropertyReleaseAdView();
            try
            {
                PropertyAdController controller = new PropertyAdController(customerCode);
                result = controller.GetPropertyReleaseAdView(adCode);
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdBindingSource.GetPropertyReleaseAd", exception);
            }
            return result;
        }
        public List<PropertyReleaseAdView> GetPropertyReleaseAds(string customerCode)
        {
            return getPropertyReleaseAds(customerCode,20);
        }

        private List<PropertyAdView> getFeaturedPropertyAds(string customerCode, PropertyAdTypes adType, int count)
        {
            List<PropertyAdView> result = new List<PropertyAdView>();
            try
            {
                string propertyTypeID = adType.ToString();
                var query = Entities.PropertyAd.Where(o => o.CustomerCode == customerCode & o.PropertyAdTypeID == propertyTypeID & o.Featured & o.Visible).Take(count);
                foreach (var propertyAd in query.ToList())
                {
                    var propertyCategory = PropertyAdCategories.Residencial;
                    if (propertyAd.CategoryName != PropertyAdCategories.Residencial.ToString())
                        propertyCategory = PropertyAdCategories.Comercial;
                    //propertyAd.PropertyAdDetails.Load();
                    //propertyAd.PropertyAdPics.Load();

                    string mainPicUrl = string.Empty;
                    if (propertyAd.PropertyAdPics.Count > 0)
                        mainPicUrl = propertyAd.PropertyAdPics.OrderBy(o => o.PropertyAdPicID).FirstOrDefault().PictureUrl;
                    else
                        mainPicUrl = @"http://www.tk1.net.br/Images/ImagemNaoDisponivel.png";

                    PropertyAdView propertyAdView = new PropertyAdView()
                    {
                        AdCategory = propertyCategory,
                        AdType = adType,
                        CityTaxes = (float)(propertyAd.CityTaxes ?? 0),
                        AdCode = propertyAd.PropertyAdID,
                        CondoTaxes = (float)(propertyAd.CondoTaxes ?? 0),
                        District = propertyAd.DistrictName,
                        MainPicUrl = mainPicUrl,
                        InternalArea = (float)propertyAd.InternalArea,
                        TotalArea = (float)propertyAd.TotalArea,
                        TotalRooms = propertyAd.TotalRooms,
                        PropertyType = propertyAd.PropertyTypeName,
                        PropertyTypeRoomName = PropertyTranslations.GetRoomDisplayName(propertyAd.PropertyTypeName, propertyAd.TotalRooms, uiCulture),
                        Value = (float)(propertyAd.Value)
                    };
                    result.Add(propertyAdView);

                }

            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdBindingSource.getFeaturedPropertyAds", exception);
            }
            return result;
        }
        private List<PropertyReleaseAdView> getPropertyReleaseAds(string customerCode, int count)
        {
            List<PropertyReleaseAdView> result = new List<PropertyReleaseAdView>();
            try
            {
                PropertyAdController controller = new PropertyAdController(customerCode);
                result = controller.GetPropertyReleaseAds();
            }
            catch (Exception exception)
            {
                AppLogClientController.WriteException("PropertyAdBindingSource.getPropertyReleaseAds", exception);
            }
            return result;
        }

    }
}
