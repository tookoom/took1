using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using TK1.PicDeveloper.Data;
using System.IO;
using TK1.Xml;
using TK1.Data;
//using System.ComponentModel;
using TK1.PicDeveloper.Control;
using TK1.Data.Converter;
using TK1.Utility;
using TK1.Basics.Controls;
using TK1.Data.Model.Presentation;
using TK1.Media.Controls;
using TK1.Media.Data;
using System.ComponentModel;
using TK1.Settings;

namespace TK1.PicDeveloper
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region DELEGATES
        delegate void PictureCallback(ImageView imageView);
        delegate void PictureSelectorCallback(ImageSelector imageSelector);
        delegate void VoidCallback();

        #endregion
        #region PRIVATE MEMBERS
        private bool isLoading;
        private bool isMouseDragging = false;
        private bool isUsingUsbDrive = false;
        private Point offsetPoint;

        private string filePath = string.Empty;
        private string clientPictureFolderPath = string.Empty;
        private string windowName = string.Empty;

        PicDeveloperSettings settings = null;
        PictureManager pictureManager = null;

        #endregion   
        #region PUBLIC PROPERTIES
        //public string FilePath
        //{
        //    get
        //    {
        //        //filePath = TextBoxPath.Text;
        //        return filePath;
        //    }
        //    set
        //    {
        //        try
        //        {
        //            filePath = value;
        //            //TextBoxPath.Text = filePath;
        //        }
        //        catch (InvalidOperationException) { }
        //    }
        //}
        public string WindowName
        {
            get
            {
                windowName = textBlockWindowName.Text;
                return windowName;
            }
            set
            {
                windowName = value;
                textBlockWindowName.Text = windowName;
            }
        }


        #endregion
        
        public MainWindow()
        {
            InitializeComponent();

            ////initialize();
        }

        private void addPicture(ImageView imageView)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (imageView != null)
                {

                    ImageSelector imageSelector = new ImageSelector() { Picture = imageView, HasQuantity = true };
                    imageSelector.QuantityChanged += new EventHandler(imageSelector_QuantityChanged);
                    imageSelector.ShowZoomedImage += new ImageSelector.ZoomEventHandler(imageSelector_ShowZoomedImage);
                    wrapPanelPictures.Children.Add(imageSelector);
                }
            }
            else
            {
                PictureCallback callback = new PictureCallback(addPicture);
                this.Dispatcher.Invoke(callback, imageView);
            }
        }
        private void changePicInfo()
        {
            if (pictureManager != null)
            {
                if (pictureManager.PicSize == PaperSizes._10x15)
                    radioButtonSize1.IsChecked = true;

                if (pictureManager.PicSize == PaperSizes._13x18)
                    radioButtonSize2.IsChecked = true;

                if (pictureManager.PicSize == PaperSizes._15x21)
                    radioButtonSize3.IsChecked = true;

                if (pictureManager.PicSize == PaperSizes._20x25)
                    radioButtonSize4.IsChecked = true;

                if (pictureManager.PicSize == PaperSizes._20x30)
                    radioButtonSize5.IsChecked = true;

                if (pictureManager.PicSize == PaperSizes._30x45)
                    radioButtonSize6.IsChecked = true;

                if (pictureManager.PicType == PaperTypes.Gloss)
                    radioButtonTypeGloss.IsChecked = true;

                if (pictureManager.PicType == PaperTypes.Regular)
                    radioButtonTypeRegular.IsChecked = true;
            }
        }
        private void showSettings()
        {
            PropertySetter propertySetter = new PropertySetter() { Value = settings };
            propertySetter.OpenSettings = new Action(loadSettingsFile);
            propertySetter.ReloadSettings = new Action(loadSettingsFile);
            propertySetter.SaveSettings = new Action(saveSettings);
            dialogWindow.DialogContent = propertySetter;
            dialogWindow.Visibility = Visibility.Visible;
        }
        private void initialize()
        {
            loadSettingsFile();

            pictureManager = new PictureManager(settings);
            pictureManager.AddPictureWorker = new Action<ImageView>(addPicture);
            pictureManager.RemovePictureWorker = new Action<ImageView>(removePicture);
            pictureManager.ResetWorker = new Action(reset);
            pictureManager.QuantityChanged += new EventHandler(pictureManager_QuantityChanged);
            pictureManager.TotalPriceChanged += new EventHandler(pictureManager_TotalPriceChanged);

            gridClient.DataContext = pictureManager.Client;
            gridBottom.DataContext = pictureManager;

            dialogWindow.IsVisible = false;
            dialogWindow.WindowMouseDown += new DialogWindow.EventHandler(dialogWindow_WindowMouseDown);

            textBlockLoading.Text = "";

            changePicInfo();
        }
        private void loadSettingsFile()
        {
            try
            {
                settings = SettingsFileLoader.Load<PicDeveloperSettings>(Constraints.AppName);
                if (settings == null)
                    saveSettings();
            }
            catch (Exception exception)
            {
                string caption = "loadConfigFile";
                string message = ErrorMessageBuilder.CreateMessage(exception);
                MessageBox.Show(message, caption);
            }
        }
        private void removePicture(ImageView imageView)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (imageView != null)
                {
                    ImageSelector control = (from el in wrapPanelPictures.Children.Cast<ImageSelector>()
                                              where el.Picture == imageView
                                              select el).FirstOrDefault();
                    if (control != null)
                        wrapPanelPictures.Children.Remove(control);
                }
            }
            else
            {
                PictureCallback callback = new PictureCallback(removePicture);
                this.Dispatcher.Invoke(callback, imageView);
            }
        }
        protected void reset()
        {
            if (this.Dispatcher.CheckAccess())
            {
                wrapPanelPictures.Children.Clear();
            }
            else
            {
                VoidCallback callback = new VoidCallback(reset);
                this.Dispatcher.Invoke(callback);
            }
        }
        private void saveSettings()
        {
            try
            {
                if (settings == null)
                    settings = new PicDeveloperSettings();
                SettingsFileLoader.Save<PicDeveloperSettings>(Constraints.AppName, settings);
            }
            catch (Exception exception)
            {
                string caption = "loadConfigFile";
                string message = ErrorMessageBuilder.CreateMessage(exception);
                MessageBox.Show(message, caption);
            }
        }


        #region EVENT HANDLERS
        void pictureManager_QuantityChanged(object sender, EventArgs e)
        {
        }
        void pictureManager_TotalPriceChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region UI EVENT HANDLERS

        private void buttonClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.ClearList();
        }
        private void buttonLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.GetFolderPics();
        }
        private void buttonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (pictureManager.Quantity > 0)
            {
                pictureManager.SavePics();
                string caption = "Fotos gravadas com sucesso!";
                string message = string.Format("Foram gravadas {0} fotos, com valor total de R${1}.\nDeseja finalizar?", pictureManager.Quantity, pictureManager.TotalPrice);
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    pictureManager.ClearList();
                else
                {
                    caption = "Atenção";
                    message = "Gravar novamente as fotos irá substituir as fotos previamente gravadas";
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
        private void buttonSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            showSettings();
        }

        private void buttonWindowClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        private void buttonWindowMaximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
        private void buttonWindowMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void dialogWindow_WindowMouseDown(object sender, EventArgs e)
        {
            dialogWindow.IsVisible = false;
        }

        private void imageSelector_QuantityChanged(object sender, EventArgs e)
        {
            pictureManager.CalculateTotalPrice();
        }
        private void imageSelector_ShowZoomedImage(object sender, ZoomEventArgs e)
        {
            dialogWindow.IsVisible = true;
            ImageView imageView = e.Picture;
            dialogWindow.DialogContent = new Image(){ Source = new BitmapImage( new Uri( imageView.Path))  };
        }

        private void radioButtonSize1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._10x15;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonSize2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._13x18;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonSize3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._15x21;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonSize4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._20x25;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonSize5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._20x30;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonSize6_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicSize = PaperSizes._30x45;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonTypeGloss_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicType = PaperTypes.Gloss;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }
        private void radioButtonTypeRegular_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.PicType = PaperTypes.Regular;
            changePicInfo();
            pictureManager.CalculateTotalPrice();
        }

        private void rectangleBar_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            offsetPoint = e.GetPosition(this);
            isMouseDragging = true;
            this.Opacity = 0.7;
            this.rectangleBar.CaptureMouse();
            e.Handled = true;
        }
        private void rectangleBar_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Opacity = 1;
            isMouseDragging = false;
            this.rectangleBar.ReleaseMouseCapture();
        }
        private void rectangleBar_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseDragging)
            {
                Point BarPoint = e.GetPosition(rectangleBar);
                Point WindowPoint = new Point(this.Left, this.Top);
                Point TouchPoint = new Point(WindowPoint.X + BarPoint.X, WindowPoint.Y + BarPoint.Y);
                this.Left = WindowPoint.X + BarPoint.X - offsetPoint.X;
                this.Top = WindowPoint.Y + BarPoint.Y - offsetPoint.Y;
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            initialize();

            //pictureManager.LoadUsbItems();
            //pictureManager.LoadCdromItems();
        }


	    #endregion    
    }
}
