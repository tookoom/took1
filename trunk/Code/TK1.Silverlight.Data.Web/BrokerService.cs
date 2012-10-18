
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
            if ((siteAd.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAd, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SiteAds.AddObject(siteAd);
            }
        }
        //public void CreateSiteAdAutoID(SiteAd siteAd)
        //{
        //    if (siteAd!= null)
        //    {
        //        var generator = this.ObjectContext.SiteAdIDGenerators.Where(o => o.CustomerCodename == siteAd.CustomerCodename & o.SiteAdTypeID == siteAd.SiteAdTypeID).FirstOrDefault();
        //        if (generator == null)
        //        {
        //            var maxSiteID = this.ObjectContext.SiteAds.Where(o => o.CustomerCodename == siteAd.CustomerCodename & o.SiteAdTypeID == siteAd.SiteAdTypeID).Max(o => o.SiteAdID);
        //            this.ObjectContext.SiteAdIDGenerators.AddObject(new SiteAdIDGenerator()
        //            {
        //                CustomerCodename = siteAd.CustomerCodename,
        //                SiteAdID = maxSiteID + 1,
        //                SiteAdTypeID = siteAd.SiteAdTypeID
        //            });
        //            siteAd.SiteAdID = maxSiteID + 1;
        //        }
        //        else
        //        {
        //            generator.SiteAdID += 1;
        //            siteAd.SiteAdID = generator.SiteAdID;
        //        }
        //    }
        //    if ((siteAd.EntityState != EntityState.Detached))
        //    {
        //        this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAd, EntityState.Added);
        //    }
        //    else
        //    {
        //        this.ObjectContext.SiteAds.AddObject(siteAd);
        //    }
        //}
        public void UpdateSiteAd(SiteAd currentSiteAd)
        {
            this.ObjectContext.SiteAds.AttachAsModified(currentSiteAd, this.ChangeSet.GetOriginal(currentSiteAd));
        }

        public void DeleteSiteAd(SiteAd siteAd)
        {
            if ((siteAd.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAd, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SiteAds.Attach(siteAd);
                this.ObjectContext.SiteAds.DeleteObject(siteAd);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SiteAdIDGenerators' query.
        public IQueryable<SiteAdIDGenerator> GetSiteAdIDGenerators(string customerCodename, SiteAdTypes adType)
        {
            var results = this.ObjectContext.SiteAdIDGenerators.Where(o => o.CustomerCodename == customerCodename & o.SiteAdTypeID == (int)adType );
            return results;
        }

        public void InsertSiteAdIDGenerator(SiteAdIDGenerator siteAdIDGenerator)
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

        public void UpdateSiteAdIDGenerator(SiteAdIDGenerator currentSiteAdIDGenerator)
        {
            this.ObjectContext.SiteAdIDGenerators.AttachAsModified(currentSiteAdIDGenerator, this.ChangeSet.GetOriginal(currentSiteAdIDGenerator));
        }

        public void DeleteSiteAdIDGenerator(SiteAdIDGenerator siteAdIDGenerator)
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
            if ((siteAdDetail.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdDetail, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SiteAdDetails.AddObject(siteAdDetail);
            }
        }

        public void UpdateSiteAdDetail(SiteAdDetail currentSiteAdDetail)
        {
            this.ObjectContext.SiteAdDetails.AttachAsModified(currentSiteAdDetail, this.ChangeSet.GetOriginal(currentSiteAdDetail));
        }

        public void DeleteSiteAdDetail(SiteAdDetail siteAdDetail)
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
            if ((siteAdPic.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(siteAdPic, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SiteAdPics.AddObject(siteAdPic);
            }
        }

        public void UpdateSiteAdPic(SiteAdPic currentSiteAdPic)
        {
            this.ObjectContext.SiteAdPics.AttachAsModified(currentSiteAdPic, this.ChangeSet.GetOriginal(currentSiteAdPic));
        }

        public void DeleteSiteAdPic(SiteAdPic siteAdPic)
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


