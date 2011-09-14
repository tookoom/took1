using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Data;
using TK1.Bizz.Data.Controller;
using TK1.Data.Controller;
using TK1.Bizz.Mdo.Data.Controller;

namespace TK1.Bizz.Pieta.Data.Controller
{
    public class PietaSiteAdController
    {
        #region PRIVATE MEMBERS
        private string codename = "pieta";

        #endregion

        public SiteAdView GetSiteAd(int adType, int siteAdID)
        {
            SiteAdView result = null;
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    var siteController = new SiteAdController(audit);
                    result = siteController.GetSiteAdView(codename, adType, siteAdID);
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    result = mdoSiteAdController.GetSiteAdView(customerID, siteAdID);
                    break;
            }
            return result;
        }
        public List<SiteDetail> GetSiteDetail(int adType, int siteAdID)
        {
            List<SiteDetail> result = null;
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    var siteController = new SiteAdController(audit);
                    result = siteController.GetSiteDetail(codename, adType, siteAdID);
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    result = mdoSiteAdController.GetSiteDetail(customerID, siteAdID);
                    break;
            }
            if (result == null)
                result = new List<SiteDetail>();
            return result;
        }
        public List<SiteDetail> GetSiteReleaseDetail(int siteReleaseAdID)
        {
            List<SiteDetail> result = null;
            int adType = (int)SiteAdTypes.Sell;
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    var release = mdoSiteAdController.GetSiteReleaseAdView(customerID, siteReleaseAdID);
                    result = new List<SiteDetail>();
                    if (release != null)
                    {
                        result.Add(new SiteDetail() { Name = release.AreaText });
                        result.Add(new SiteDetail() { Name = release.RoomText });
                    }
                    //result = mdoSiteAdController.GetSiteDetail(customerID, siteReleaseAdID);
                    break;
            }
            if (result == null)
                result = new List<SiteDetail>();
            return result;
        }
        public List<SiteAdPicView> GetSitePics(int adType, int siteAdID)
        {
            List<SiteAdPicView> result = new List<SiteAdPicView>();
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    var siteController = new SiteAdController(audit);
                    var siteAd = siteController.GetSiteAd(codename, adType, siteAdID);
                    if (siteAd != null)
                    {
                        foreach (var item in siteAd.SiteAdPics)
                            result.Add(new SiteAdPicView()
                            {
                                Description = item.Description,
                                FileName = item.FileName,
                                Index = item.PicID
                            });
                    }
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    var mdoSiteAd = mdoSiteAdController.GetSiteAd(customerID, siteAdID);
                    if (mdoSiteAd != null)
                    {
                        mdoSiteAd.SiteReference.Load();
                        mdoSiteAd.Site.SitePics.Load();
                        foreach (var item in mdoSiteAd.Site.SitePics)
                            result.Add(new SiteAdPicView()
                            {
                                Description = item.Description,
                                FileName = item.FileName,
                                Index = item.PicID
                            });
                    }
                    break;
            }
            return result;
        }
        public SiteReleaseAdView GetSiteReleaseAd(int siteReleaseAdID)
        {
            SiteReleaseAdView result = null;
            int adType = (int)SiteAdTypes.Sell;
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    result = mdoSiteAdController.GetSiteReleaseAdView(customerID, siteReleaseAdID);
                    break;
            }
            return result;
        }
        public List<SiteReleaseAdView> GetSiteReleaseAds()
        {
            List<SiteReleaseAdView> result = null;
            int adType = (int)SiteAdTypes.Sell;
            var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
            switch (adType)
            {
                case (int)SiteAdTypes.Rent:
                    break;
                case (int)SiteAdTypes.Sell:
                    var mdoSiteAdController = new MdoSiteAdController(audit);
                    int customerID = mdoSiteAdController.GetCustomerID(codename);
                    result = mdoSiteAdController.GetSiteReleaseAds(customerID);
                    break;
            }
            if (result == null)
                result = new List<SiteReleaseAdView>();
            return result;
        }
        public List<SiteAdView> SearchSites(MdoSiteAdSearchParameters mdoParameters)
        {
            List<SiteAdView> result = null;
            if (mdoParameters != null)
            {
                var audit = new AuditController(AppNames.BizzSites.ToString(), CustomerNames.Pietá.ToString());
                switch (mdoParameters.AdType)
                {
                    case SiteAdTypes.Rent:
                        var parameters = mdoParameters as SiteAdSearchParameters;
                        var siteController = new SiteAdController(audit);
                        result = siteController.SearchSites(parameters);
                        break;
                    case SiteAdTypes.Sell:
                        var mdoSiteAdController = new MdoSiteAdController(audit);
                        result = mdoSiteAdController.SearchSites(mdoParameters);
                        break;
                }
            }
            if (result == null)
                result = new List<SiteAdView>();
            return result;
        }
    }
}
