using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TK1.Silverlight.Data.Web;
using TK1.Bizz.Data;
using System.ServiceModel.DomainServices.Client;
using System.Collections;

namespace TK1.Silverlight.Data
{
    public class BrokerDataServiceController
    {
        #region PRIVATE MEMBERS
        private BrokerContext brokerContext;

        #endregion
        #region PUBLIC PROPERTIES
        public BrokerContext BrokerContext
        {
            get { return brokerContext; }
            set { brokerContext = value; }
        }

        #endregion

        public BrokerDataServiceController()
        {
            brokerContext = new BrokerContext();
        }

        //public LoadOperation<SiteAd> LoadSiteAds()
        //{
        //    LoadOperation<SiteAd> result = brokerContext.Load(brokerContext.GetSiteAdsQuery());
        //    return result;
        //}

        //public IEnumerable LoadSiteAdsItems()
        //{
        //    LoadOperation<SiteAd> resultSet = brokerContext.Load(brokerContext.GetSiteAdsQuery(), OnLoadCompleted, null);
        //    return resultSet.Entities;
        //}

        //private void OnLoadCompleted(LoadOperation<SiteAd> loadOperation)
        //{
        //    if (loadOperation.HasError)
        //    {
        //        MessageBox.Show(string.Format("Retrieving data failed: {0}", loadOperation.Error.Message));
        //        loadOperation.MarkErrorAsHandled();
        //    }
        //}
    }
}
