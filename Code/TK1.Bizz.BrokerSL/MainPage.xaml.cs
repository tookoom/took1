using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TK1.Silverlight.Data;
using TK1.Bizz.Data;
using System.ServiceModel.DomainServices.Client;
using TK1.Bizz.Data.Presentation;
using TK1.Silverlight.Data.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TK1.Bizz.BrokerSL.BrokerFileServiceReference;

namespace TK1.Bizz.BrokerSL
{
	public partial class MainPage : UserControl
	{
        #region PRIVATE MEMBERS
        private BrokerContext brokerContext;
        private IEnumerable<SiteAd> siteAdsEntities;
        //private IEnumerable<SiteAdIDGenerator> siteAdIDGeneratorsEntities;
        private IEnumerable<SiteAdStatu> siteAdStatusEntities;


        #endregion

		public MainPage()
		{
			// Required to initialize variables
			InitializeComponent();

            brokerContext = new BrokerContext();

            siteAdsEntities = brokerContext.Load(brokerContext.GetCustomerSiteAdsQuery("pandolfo")).Entities;
            siteAdStatusEntities = brokerContext.Load(brokerContext.GetSiteAdStatusQuery()).Entities;
            brokerContext.Load(brokerContext.GetSiteAdTypesQuery());

            setDefaultState();
            searchSiteAds();

        }

        private void addSiteAd()
        {
            var adType = SiteAdTypes.Rent;
            if (radioButtonSell.IsChecked.HasValue & radioButtonSell.IsChecked.Value)
                adType = SiteAdTypes.Sell;

            var siteAd = new SiteAd()
            {
                CategoryName = "Residencial/Comercial",
                CustomerCodename = "pandolfo",
                CityName = "Porto Alegre",
                DistrictName = "Bairro",
                FeaturedAd = false,
                SiteAdStatusID = 1,
                SiteAdTypeID = (int)adType,
                SiteTypeName = "Apartamento/Casa/Loja",
                Visible = false
            };

            brokerContext.GenerateSiteAdID("pandolfo", adType, dataContextGenerateSiteAdID_Completed, siteAd);
        }
        private void setCodeSearchControlsState()
        {
            labelSiteAdCodeSearch.IsEnabled = checkBoxSiteAdCodeSearch.IsChecked.Value;
            textBoxSiteCode.IsEnabled = checkBoxSiteAdCodeSearch.IsChecked.Value;

        } 
        private void setDefaultState()
        {
            setCodeSearchControlsState();
        }
        private void searchSiteAds()
        {
            var customerCodename = "pandolfo";
            var adType = SiteAdTypes.Rent;
            if (radioButtonSell.IsChecked.HasValue & radioButtonSell.IsChecked.Value)
                adType = SiteAdTypes.Sell;

            LoadOperation<SiteAd> resultSet = brokerContext.Load(brokerContext.GetSiteAdsQuery(customerCodename, adType), dataContextSiteAd_OnLoadCompleted, null);
        }
        //private static void testUpload()
        //{
        //    var fileDialog = new OpenFileDialog();
        //    fileDialog.Filter = "Fotos|*.jpg";
        //    bool? result = fileDialog.ShowDialog();
        //    if (result != null && result == true)
        //    {
        //        Stream stream = fileDialog.File.OpenRead();
        //        byte[] buffer = new byte[stream.Length]; stream.Read(buffer, 0, (int)stream.Length);
        //        stream.Dispose();
        //        stream.Close();

        //        UploadPicture uploadPicture = new UploadPicture();
        //        uploadPicture.FileName = fileDialog.File.Name;
        //        uploadPicture.File = buffer;
        //        uploadPicture.Thumbnail = buffer;

        //        BrokerFileServiceClient serviceClient = new BrokerFileServiceClient();
        //        serviceClient.SaveBrokerSiteAdPicAsync("pandolfo", Data.Presentation.SiteAdTypes.Rent, 1, uploadPicture);
        //    }
        //}

        #region DATA CONTEXT EVENT HANDLERS
        private void dataContextGenerateSiteAdID_Completed(InvokeOperation<int> invokeOperation)
        {
            if (invokeOperation.HasError)
            {
                MessageBox.Show(string.Format("Falha na geração de código para imóvel: {0}", invokeOperation.Error));
                invokeOperation.MarkErrorAsHandled();
            }
            else
            {
                var siteAd = invokeOperation.UserState as SiteAd;
                if (siteAd != null)
                {
                    siteAd.SiteAdID = invokeOperation.Value;
                    brokerContext.SiteAds.Add(siteAd);
                    brokerContext.SubmitChanges();
                    searchSiteAds();
                }
            }
        }
        private void dataContextSiteAd_OnLoadCompleted(LoadOperation<SiteAd> loadOperation)
        {
            if (loadOperation.HasError)
            {
                MessageBox.Show(string.Format("Falha na pesquisa: {0}", loadOperation.Error));
                loadOperation.MarkErrorAsHandled();
            }
            else
            {
                dataGridSiteAdsResult.ItemsSource = loadOperation.Entities;
            }
        } 
        #endregion

        #region UI EVENT HANDLERS
        private void buttonSearchSite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            searchSiteAds();

        }
        private void buttonSiteAdCreate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            addSiteAd();

        }

        private void buttonSiteAdEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var siteAd = dataGridSiteAdsResult.SelectedItem as SiteAd;
            if (siteAd != null)
            {
                SiteAdEditor siteAdEditor = new SiteAdEditor();
                siteAdEditor.BrokerContext = brokerContext;
                siteAdEditor.DataContext = siteAd;
                siteAdEditor.SiteAdsEntities = siteAdsEntities;
                siteAdEditor.SiteAdStatusEntities = siteAdStatusEntities;
                siteAdEditor.Show();
            }
        }
        private void buttonSiteAdRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var siteAd = dataGridSiteAdsResult.SelectedItem as SiteAd;
            if (siteAd != null)
            {
                var result = MessageBox.Show("Deseja remover este imóvel? Este operação não poderá ser desfeita", "Remover Imóvel", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    brokerContext.SiteAds.Remove(siteAd);
                    brokerContext.SubmitChanges();
                    searchSiteAds();

                }
            }
        }

        private void buttonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(brokerContext.HasChanges)
                brokerContext.SubmitChanges();
        }
        private void buttonCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string hostName = Application.Current.Host.Source.Host;
            if (Application.Current.Host.Source.Port != 80)
                hostName += ":" + Application.Current.Host.Source.Port;
            MessageBox.Show(hostName);
            brokerContext.RejectChanges();
        }

        private void checkBoxSiteAdCodeSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            setCodeSearchControlsState();
        }

        private void radioButtonRent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            searchSiteAds();
        }
        private void radioButtonSell_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            searchSiteAds();
        }

        #endregion


	}
}