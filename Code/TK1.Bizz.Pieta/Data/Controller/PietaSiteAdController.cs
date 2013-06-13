using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TK1.Bizz.Pieta.Data.Controller
{
    public class PietaSiteAdController_OLD
    {
        #region PRIVATE MEMBERS
        private string customerCode = "pieta";

        #endregion

        //public PropertyAdView GetSiteAd(PropertyAdTypes adType, int adCode)
        //{
        //    PropertyAdView result = null;
        //    var propertyAdController = new PropertyAdController(customerCode);
        //    result = propertyAdController.GetPropertyAdView(adType, adCode);
        //    return result;
        //}
        //public List<PropertyAdDetailView> GetSiteDetail(PropertyAdTypes adType, int adCode)
        //{
        //    List<PropertyAdDetailView> result = null;
        //    var propertyAdController = new PropertyAdController(customerCode);
        //    result = propertyAdController.GetPropertyDetailViews(adType, adCode);
        //    return result;
        //}
        //public List<PropertyAdPicView> GetSitePics(PropertyAdTypes adType, int adCode)
        //{
        //    List<PropertyAdPicView> result = new List<PropertyAdPicView>();
        //    var propertyAdController = new PropertyAdController(customerCode);
        //    result = propertyAdController.GetPropertyPicViews(adType, adCode);
        //    return result;
        //}
        //public List<PropertyAdView> SearchSites(PropertyAdSearchParameters parameters)
        //{
        //    List<PropertyAdView> result = null;
        //    var propertyAdController = new PropertyAdController(customerCode);
        //    result = propertyAdController.SearchPropertyAds(parameters);
        //    return result;
        //}


        //public PropertyReleaseAdView GetSiteReleaseAd(int siteReleaseAdID)
        //{
        //    PropertyReleaseAdView result = null;
        //    //int adType = (int)SiteAdTypes.Sell;
        //    //var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
        //    //switch (adType)
        //    //{
        //    //    case (int)SiteAdTypes.Rent:
        //    //        break;
        //    //    case (int)SiteAdTypes.Sell:
        //    //        var mdoSiteAdController = new MdoSiteAdController(audit);
        //    //        int customerID = mdoSiteAdController.GetCustomerID(customerCode);
        //    //        result = mdoSiteAdController.GetSiteReleaseAdView(customerID, siteReleaseAdID);
        //    //        break;
        //    //}
        //    return result;
        //}
        //public List<PropertyReleaseAdView> GetSiteReleaseAds()
        //{
        //    List<PropertyReleaseAdView> result = null;
        //    //int adType = (int)SiteAdTypes.Sell;
        //    //var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
        //    //switch (adType)
        //    //{
        //    //    case (int)SiteAdTypes.Rent:
        //    //        break;
        //    //    case (int)SiteAdTypes.Sell:
        //    //        var mdoSiteAdController = new MdoSiteAdController(audit);
        //    //        int customerID = mdoSiteAdController.GetCustomerID(customerCode);
        //    //        result = mdoSiteAdController.GetSiteReleaseAds(customerID);
        //    //        break;
        //    //}
        //    if (result == null)
        //        result = new List<PropertyReleaseAdView>();
        //    return result;
        //}
        //public List<PropertyAdDetailView> GetSiteReleaseDetail(int siteReleaseAdID)
        //{
        //    List<PropertyAdDetailView> result = null;
        //    //int adType = (int)SiteAdTypes.Sell;
        //    //var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
        //    //switch (adType)
        //    //{
        //    //    case (int)SiteAdTypes.Rent:
        //    //        break;
        //    //    case (int)SiteAdTypes.Sell:
        //    //        var mdoSiteAdController = new MdoSiteAdController(audit);
        //    //        int customerID = mdoSiteAdController.GetCustomerID(customerCode);
        //    //        var release = mdoSiteAdController.GetSiteReleaseAdView(customerID, siteReleaseAdID);
        //    //        result = new List<PropertyAdDetailView>();
        //    //        if (release != null)
        //    //        {
        //    //            result.Add(new PropertyAdDetailView() { Name = release.AreaText });
        //    //            result.Add(new PropertyAdDetailView() { Name = release.RoomText });
        //    //        }
        //    //        //result = mdoSiteAdController.GetSiteDetail(customerID, siteReleaseAdID);
        //    //        break;
        //    //}
        //    if (result == null)
        //        result = new List<PropertyAdDetailView>();
        //    return result;
        //}
        //public List<PropertyAdPicView> GetSiteReleasePics(int siteReleaseAdID)
        //{
        //    List<PropertyAdPicView> result = new List<PropertyAdPicView>();
        //    //var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
        //    //var mdoSiteAdController = new MdoSiteAdController(audit);
        //    //int customerID = mdoSiteAdController.GetCustomerID(customerCode);
        //    //var mdoSiteAd = mdoSiteAdController.GetSiteReleaseAd(customerID, siteReleaseAdID);
        //    //if (mdoSiteAd != null)
        //    //{
        //    //    mdoSiteAd.SiteReference.Load();
        //    //    mdoSiteAd.Site.SitePics.Load();
        //    //    foreach (var item in mdoSiteAd.Site.SitePics)
        //    //        result.Add(new PropertyAdPicView()
        //    //        {
        //    //            Description = item.Description,
        //    //            FileName = item.FileName,
        //    //            Index = item.PicID
        //    //        });
        //    //}
        //    return result;
        //}
    }
}
