
namespace TK1.Silverlight.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using TK1.Bizz.Data;
    using TK1.Bizz.Data.Presentation;
    using TK1.Data;


    // Implements application logic using the BizzEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class BrokerService : LinqToEntitiesDomainService<BizzEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiteAds' query.
        public IQueryable<SiteAd> GetSiteAds(string customerCodename, SiteAdTypes adType)
        {
            return this.ObjectContext.SiteAds.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType);
        }
        public void InsertSiteAd(SiteAd siteAd)
        {
            try
            {
                if ((siteAd.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAd, EntityState.Added);
                }
                else
                {
                    this.ObjectContext.SiteAds.AddObject(siteAd);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.InsertSiteAd", exception, true);
            }
        }
        public void UpdateSiteAd(SiteAd currentSiteAd)
        {
            try
            {
                this.ObjectContext.SiteAds.AttachAsModified(currentSiteAd, this.ChangeSet.GetOriginal(currentSiteAd));
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.UpdateSiteAd", exception, true);
            }
        }
        public void DeleteSiteAd(SiteAd siteAd)
        {
            try
            {
                if ((siteAd.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAd, EntityState.Deleted);
                }
                else
                {
                    this.ObjectContext.SiteAds.Attach(siteAd);
                    clearReferences(siteAd);
                    this.ObjectContext.SiteAds.DeleteObject(siteAd);
                }

            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.DeleteSiteAd", exception, true);
            }
        }

        private void clearReferences(SiteAd siteAd)
        {
            if (siteAd != null)
            {
                var details = siteAd.SiteAdDetails.ToList();
                foreach (var item in details)
                    this.ObjectContext.SiteAdDetails.DeleteObject(item);

                var pics = siteAd.SiteAdPics.ToList();
                foreach (var item in pics)
                    this.ObjectContext.SiteAdPics.DeleteObject(item);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiteAdIDGenerators' query.
        public IQueryable<SiteAdIDGenerator> GetSiteAdIDGenerators(string customerCodename, SiteAdTypes adType)
        {
            var results = this.ObjectContext.SiteAdIDGenerators.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType);
            return results;
        }

        public void InsertSiteAdIDGenerator(SiteAdIDGenerator siteAdIDGenerator)
        {
            try
            {
                if ((siteAdIDGenerator.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdIDGenerator, EntityState.Added);
                }
                else
                {
                    this.ObjectContext.SiteAdIDGenerators.AddObject(siteAdIDGenerator);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.InsertSiteAdIDGenerator", exception, true);
            }
        }

        public void UpdateSiteAdIDGenerator(SiteAdIDGenerator currentSiteAdIDGenerator)
        {
            try
            {
                this.ObjectContext.SiteAdIDGenerators.AttachAsModified(currentSiteAdIDGenerator, this.ChangeSet.GetOriginal(currentSiteAdIDGenerator));
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.UpdateSiteAdIDGenerator", exception, true);
            }

        }

        public void DeleteSiteAdIDGenerator(SiteAdIDGenerator siteAdIDGenerator)
        {
            try
            {
                if ((siteAdIDGenerator.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdIDGenerator, EntityState.Deleted);
                }
                else
                {
                    this.ObjectContext.SiteAdIDGenerators.Attach(siteAdIDGenerator);
                    this.ObjectContext.SiteAdIDGenerators.DeleteObject(siteAdIDGenerator);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.DeleteSiteAdIDGenerator", exception, true);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiteAdDetails' query.
        public IQueryable<SiteAdDetail> GetSiteAdDetails(string customerCodename, SiteAdTypes adType, int siteAdID)
        {
            var results = this.ObjectContext.SiteAdDetails.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType & o.SiteAdID == siteAdID);
            return results;
        }

        public void InsertSiteAdDetail(SiteAdDetail siteAdDetail)
        {
            try
            {
                if ((siteAdDetail.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdDetail, EntityState.Added);
                }
                else
                {
                    this.ObjectContext.SiteAdDetails.AddObject(siteAdDetail);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.InsertSiteAdDetail", exception, true);
            }
        }

        public void UpdateSiteAdDetail(SiteAdDetail currentSiteAdDetail)
        {
            try
            {
                this.ObjectContext.SiteAdDetails.AttachAsModified(currentSiteAdDetail, this.ChangeSet.GetOriginal(currentSiteAdDetail));
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.UpdateSiteAdDetail", exception, true);
            }
        }

        public void DeleteSiteAdDetail(SiteAdDetail siteAdDetail)
        {
            try
            {
                if ((siteAdDetail.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdDetail, EntityState.Deleted);
                }
                else
                {
                    this.ObjectContext.SiteAdDetails.Attach(siteAdDetail);
                    this.ObjectContext.SiteAdDetails.DeleteObject(siteAdDetail);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.DeleteSiteAdDetail", exception, true);
            }
        }


        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiteAdPics' query.
        public IQueryable<SiteAdPic> GetSiteAdPics(string customerCodename, SiteAdTypes adType, int siteAdID)
        {
            return this.ObjectContext.SiteAdPics.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType & o.SiteAdID == siteAdID);
        }

        public void InsertSiteAdPic(SiteAdPic siteAdPic)
        {
            try
            {
                if ((siteAdPic.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdPic, EntityState.Added);
                }
                else
                {
                    this.ObjectContext.SiteAdPics.AddObject(siteAdPic);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.InsertSiteAdPic", exception, true);
            }
        }

        public void UpdateSiteAdPic(SiteAdPic currentSiteAdPic)
        {
            try
            {
                this.ObjectContext.SiteAdPics.AttachAsModified(currentSiteAdPic, this.ChangeSet.GetOriginal(currentSiteAdPic));
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.UpdateSiteAdPic", exception, true);
            }
        }

        public void DeleteSiteAdPic(SiteAdPic siteAdPic)
        {
            try
            {
                if ((siteAdPic.EntityState != EntityState.Detached))
                {
                    this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdPic, EntityState.Deleted);
                }
                else
                {
                    this.ObjectContext.SiteAdPics.Attach(siteAdPic);
                    this.ObjectContext.SiteAdPics.DeleteObject(siteAdPic);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("TK1.BrokerService.DeleteSiteAdPic", exception, true);
            }
        }

        public int GenerateSiteAdID(string customerCodename, SiteAdTypes adType)
        {
            int result = 0;
            var generator = this.ObjectContext.SiteAdIDGenerators.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType).FirstOrDefault();
            if (generator == null)
            {
                var maxSiteID = this.ObjectContext.SiteAds.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType).Max(o => o.SiteAdID);
                this.ObjectContext.SiteAdIDGenerators.AddObject(new SiteAdIDGenerator()
                {
                    CustomerCodename = customerCodename,
                    SiteAdID = maxSiteID + 1,
                    SiteAdTypeID = (int)adType
                });
                result = maxSiteID + 1;
                this.ObjectContext.SaveChanges();
            }
            else
            {
                generator.SiteAdID += 1;
                this.ObjectContext.SaveChanges();
                result = generator.SiteAdID;
            }
            return result;
        }

        #region MASTER DATA
        public IQueryable<SiteAd> GetCustomerSiteAds(string customerCodename)
        {
            //return this.ObjectContext.SiteAds.Where(o => o.CustomerCodename == customerCodename).Select(o => o.CityName).Distinct().OrderBy(o => o);
            return this.ObjectContext.SiteAds.Where(o => o.CustomerCodename == customerCodename);
        }
        //public IQueryable<SiteAdIDGenerator> GetCustomerSiteAdIDGenerators(string customerCodename)
        //{
        //    var results = this.ObjectContext.SiteAdIDGenerators.Where(o => o.CustomerCodename == customerCodename);
        //    return results;
        //}
        public IQueryable<SiteAdType> GetSiteAdTypes()
        {
            return this.ObjectContext.SiteAdTypes;
        }
        public IQueryable<SiteAdStatu> GetSiteAdStatus()
        {
            return this.ObjectContext.SiteAdStatus;
        }

        #endregion
    }
}


