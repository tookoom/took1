using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TK1.Silverlight.Data.Web;
using System.ServiceModel.DomainServices.Client;
using TK1.Silverlight.Data.Collection;
using System.IO;
using TK1.Bizz.BrokerSL.BrokerFileServiceReference;
using TK1.Bizz.Data;

namespace TK1.Bizz.BrokerSL
{
    public partial class SiteAdEditor : ChildWindow
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
        public object DataContext
        {
            get { return base.DataContext; }
            set
            {
                base.DataContext = value;
                loadSiteAd(value as SiteAd);
            }
        }
        public IEnumerable<SiteAd> SiteAdsEntities
        {
            set
            {
                if(autoCompleteBoxCityNames!= null)
                    autoCompleteBoxCityNames.ItemsSource = value.Select(o => o.CityName).Distinct().OrderBy(o => o);
                if (autoCompleteBoxDistrictNames != null)
                    autoCompleteBoxDistrictNames.ItemsSource = value.Select(o => o.DistrictName).Distinct().OrderBy(o => o);
                if (autoCompleteBoxCategoryNames != null)
                    autoCompleteBoxCategoryNames.ItemsSource = value.Select(o => o.CategoryName).Distinct().OrderBy(o => o);
                if (autoCompleteBoxSiteTypeNames != null)
                    autoCompleteBoxSiteTypeNames.ItemsSource = value.Select(o => o.SiteTypeName).Distinct().OrderBy(o => o);
            }
        }
        public IEnumerable<SiteAdStatu> SiteAdStatusEntities
        {
            set
            {
                if (comboBoxStatus != null)
                    comboBoxStatus.ItemsSource = value;
            }
        } 
        #endregion

        public SiteAdEditor()
        {
            InitializeComponent();

            loadConstraints();
        }

        private void addSiteAdPic()
        {
            var siteAd = DataContext as SiteAd;
            if (siteAd != null)
            {

                BrokerFileServiceClient serviceClient = new BrokerFileServiceClient();
                var fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Arquivos de Fotos|*.jpg";
                bool? result = fileDialog.ShowDialog();
                if (result != null && result == true)
                {
                    int picID = 0;
                    if (siteAd.SiteAdPics != null & siteAd.SiteAdPics.Count > 0)
                        picID = siteAd.SiteAdPics.Max(o => o.PicID);
                    picID += 1;
                    var siteAdPic = new SiteAdPic()
                    {
                        FileName = fileDialog.File.Name,
                        PicID = picID
                    };
                    siteAd.SiteAdPics.Add(siteAdPic);
                    dataGridSiteAdPics.ItemsSource = siteAd.SiteAdPics;

                    Stream stream = fileDialog.File.OpenRead();
                    byte[] buffer = new byte[stream.Length]; stream.Read(buffer, 0, (int)stream.Length);
                    stream.Dispose();
                    stream.Close();

                    UploadPicture file = new UploadPicture();
                    file.FileName = fileDialog.File.Name;
                    file.File = buffer;
                    file.Thumbnail = buffer;
                    serviceClient.SaveBrokerSiteAdPicAsync(siteAd.CustomerCodename, (Data.Presentation.SiteAdTypes)siteAd.SiteAdTypeID, siteAd.SiteAdID, file, siteAdPic);
                    serviceClient.SaveBrokerSiteAdPicCompleted += new EventHandler<SaveBrokerSiteAdPicCompletedEventArgs>(service_SaveFileCompleted);
                }
            }
        }
        private void loadConstraints()
        {
            //BrokerContext brokerContext = new BrokerContext();
            //autoCompleteBoxCityNames.ItemsSource = brokerContext.Load(brokerContext.GetCustomerSiteAdsQuery("pandolfo")).Entities.Select(o => o.CityName).Distinct().OrderBy(o => o);
            //autoCompleteBoxCategoryNames.ItemsSource = brokerContext.Load(brokerContext.GetCustomerSiteAdsQuery("pandolfo")).Entities.Select(o => o.CategoryName).Distinct().OrderBy(o => o);
        }
        private void loadSiteAd(SiteAd siteAd)
        {
            if (siteAd != null)
            {
                brokerContext.Load(brokerContext.GetSiteAdDetailsQuery("pandolfo", (Data.Presentation.SiteAdTypes)siteAd.SiteAdTypeID, siteAd.SiteAdID), dataContextSiteAdDetails_OnLoadCompleted, null);
                brokerContext.Load(brokerContext.GetSiteAdPicsQuery("pandolfo", (Data.Presentation.SiteAdTypes)siteAd.SiteAdTypeID, siteAd.SiteAdID), dataContextSiteAdPics_OnLoadCompleted, null);
                //dataFormSiteAdDetails.ItemsSource = brokerContext.Load(brokerContext.GetSiteAdDetailsQuery("pandolfo", (Data.Presentation.SiteAdTypes)siteAd.SiteAdTypeID,siteAd.SiteAdID)).Entities;
                //dataFormSiteAdDetails.ItemsSource = brokerContext.Load(brokerContext.GetSiteAdPicsQuery("pandolfo", (Data.Presentation.SiteAdTypes)siteAd.SiteAdTypeID, siteAd.SiteAdID)).Entities.OrderBy(o=>o.PicID);
            }
        }

        #region UI EVENT HANDLERS
        private void buttonAddSiteAdPic_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            addSiteAdPic();
        }
        private void buttonSiteAdPicRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var siteAd = DataContext as SiteAd;
            if (siteAd != null)
            {
                var siteAdPic = dataGridSiteAdPics.SelectedItem as SiteAdPic;
                if (siteAdPic != null)
                {
                    siteAd.SiteAdPics.Remove(siteAdPic);
                    dataGridSiteAdPics.ItemsSource = siteAd.SiteAdPics;
                }
            }
        }
        private void buttonGetFilePath_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (brokerContext != null)
                brokerContext.SubmitChanges();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            if (brokerContext != null)
                brokerContext.RejectChanges();
        }
        
        #endregion

        #region DATA CONTEXT EVENT HANDLERS
        private void dataContextSiteAdDetails_OnLoadCompleted(LoadOperation<SiteAdDetail> loadOperation)
        {
            if (loadOperation.HasError)
            {
                MessageBox.Show(string.Format("Retrieving data failed: {0}", loadOperation.Error));
                loadOperation.MarkErrorAsHandled();
            }
            else
            {
                // dataFormSiteAdDetails.ItemsSource = loadOperation.Entities as SiteAdDetailCollection;
                //dataFormSiteAdDetails.DataContext = loadOperation.Entities;
            }
        }
        private void dataContextSiteAdPics_OnLoadCompleted(LoadOperation<SiteAdPic> loadOperation)
        {
            if (loadOperation.HasError)
            {
                MessageBox.Show(string.Format("Retrieving data failed: {0}", loadOperation.Error));
                loadOperation.MarkErrorAsHandled();
            }
            else
            {
                dataGridSiteAdPics.ItemsSource = loadOperation.Entities;
            }
        }
        private void service_SaveFileCompleted(object sender, SaveBrokerSiteAdPicCompletedEventArgs e)
        {
            bool fileSaveSuccess = false;
            if (e.Error == null & e.Result != null)
            {
                var siteAdPic = e.UserState as SiteAdPic;
                if (siteAdPic != null)
                {
                    if (e.Result.Success)
                    {
                        siteAdPic.PictureUrl = e.Result.PictureUrl;
                        siteAdPic.PictureFilePath = e.Result.PicturePath;
                        siteAdPic.ThumbnailFilePath = e.Result.ThumbnaiPath;
                        siteAdPic.ThumbnailUrl = e.Result.ThumbnailUrl;
                        fileSaveSuccess = true;
                    }
                }
            }
            if (fileSaveSuccess)
            {
                MessageBox.Show("Arquivo salvo com sucesso");
            }
            else
            {
                MessageBox.Show("Falha no envio do arquivo");
            }
        }

        #endregion

    }
}

