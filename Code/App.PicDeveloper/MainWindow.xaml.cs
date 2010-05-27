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
using TK1.PicDeveloper.Collection;
using TK1.Xml;
using TK1.Data;
//using System.ComponentModel;
using TK1.PicDeveloper.Control;
using TK1.Data.Converter;
using TK1.PicDeveloper.Settings;
using TK1.Utility;
using TK1.Configuration;
using TK1.Basics.Controls;
using TK1.Data.Model.Presentation;

namespace TK1.PicDeveloper
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region DELEGATES
        delegate void PictureCallback(Picture picture);
        #endregion
        #region PRIVATE MEMBERS
        private bool isLoading;
        private bool isMouseDragging = false;
        private bool isUsingUsbDrive = false;
        private Point offsetPoint;

        private string filePath = string.Empty;
        private string clientPictureFolderPath = string.Empty;
        private string windowName = string.Empty;

        private Picture bottomLeftImage;
        private Picture buttonIconCamera;
        private Picture buttonIconCD;
        private Picture buttonIconFolder;
        private Picture buttonIconPenDrive;
        private Picture topLeftImage;

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
        public string BottomLeftImage
        {
            set
            {
                bottomLeftImage = new Picture(value, 1000);
                contentPresenterBottomLeftImage.Content = new Image();// { Source = bottomLeftImage.ImageSource };
            }
        }
        public string ButtonIconCamera
        {
            set
            {
                buttonIconCamera = new Picture(value, 1000);
                buttonSourceCamera.Content = new Image();// { Source = buttonIconCamera.ImageSource };
            }
        }
        public string ButtonIconCD
        {
            set
            {
                buttonIconCD = new Picture(value, 1000);
                buttonSourceCD.Content = new Image();// { Source = buttonIconCD.ImageSource };
            }
        }
        public string ButtonIconFolder
        {
            set
            {
                buttonIconFolder = new Picture(value, 1000);
                buttonSourceFolder.Content = new Image();// { Source = buttonIconFolder.ImageSource };
            }
        }
        public string ButtonIconPenDrive
        {
            set
            {
                buttonIconPenDrive = new Picture(value, 1000);
                buttonSourcePenDrive.Content = new Image();// { Source = buttonIconPenDrive.ImageSource };
            }
        }
        public string TopLeftImage
        {
            set
            {
                topLeftImage = new Picture(value, 1000);
                contentPresenterTopLeftImage.Content = new Image();//{ Source = topLeftImage.ImageSource };
            }
        }


        #endregion
        
        public MainWindow()
        {
            InitializeComponent();

            ////initialize();
        }

        ////private void addPicture(Picture picture)
        ////{
        ////    if (this.Dispatcher.CheckAccess())
        ////    {
        ////        if (picture != null)
        ////        {
        ////            PictureControl control = new PictureControl();
        ////            control.FileName = picture.Path;
        ////            control.contentPresenterPicture.Content = new Image();// { Source = picture.ImageSource };
        ////            control.PicQuantityChanged += new PictureControl.EventHandler(control_PicQuantityChanged);
        ////            control.ShowZoomedImage += new PictureControl.ZoomEventHandler(control_ShowZoomedImage);
        ////            wrapPanelPictures.Children.Add(control);
                    
        ////        }
        ////    }
        ////    else
        ////    {
        ////        PictureCallback callback = new PictureCallback(addPicture);
        ////        this.Dispatcher.Invoke(callback, picture);
        ////    }
        ////}
        private void addPicture(Picture picture)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (picture != null)
                {
                    PictureControl pictureControl = new PictureControl() { Picture = picture };
                    wrapPanelPictures.Children.Add(pictureControl);
                }
            }
            else
            {
                PictureCallback callback = new PictureCallback(addPicture);
                this.Dispatcher.Invoke(callback, picture);

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
            pictureManager.AddPicture = new Action<Picture>(addPicture);
            pictureManager.RemovePicture = new Action<Picture>(removePicture);
            pictureManager.TotalPriceChanged += new EventHandler(pictureManager_TotalPriceChanged);

            gridClient.DataContext = pictureManager.Client;

            dialogWindow.IsVisible = false;
            dialogWindow.WindowMouseDown += new DialogWindow.EventHandler(dialogWindow_WindowMouseDown);

            textBlockLoading.Text = "";

        }

        private void loadSettingsFile()
        {
            try
            {
                settings = SettingsFileLoader<PicDeveloperSettings>.Load(Constraints.AppName);
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
        private void removePicture(Picture picture)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (picture != null)
                {
                    PictureControl control = (from el in wrapPanelPictures.Children.Cast<PictureControl>()
                                              where el.Picture == picture
                                              select el).FirstOrDefault();
                    if (control != null)
                        wrapPanelPictures.Children.Remove(control);
                }
            }
            else
            {
                PictureCallback callback = new PictureCallback(removePicture);
                this.Dispatcher.Invoke(callback, picture);
            }
        }
        private void saveSettings()
        {
            try
            {
                if (settings == null)
                    settings = new PicDeveloperSettings();
                SettingsFileLoader<PicDeveloperSettings>.Save(Constraints.AppName, settings);
            }
            catch (Exception exception)
            {
                string caption = "loadConfigFile";
                string message = ErrorMessageBuilder.CreateMessage(exception);
                MessageBox.Show(message, caption);
            }
        }

        void control_ShowZoomedImage(object sender, ZoomEventArgs e)
        {
            dialogWindow.IsVisible = true;
            Picture picture = e.Picture;
            dialogWindow.DialogContent = new Image();//{ Source = picture.ImageSource };
        }
        void control_PicQuantityChanged(object sender, EventArgs e)
        {
            pictureManager.CalculateTotalPrice();
        }
        private void dialogWindow_WindowMouseDown(object sender, EventArgs e)
        {
            dialogWindow.IsVisible = false;
        }

        #region EVENT HANDLERS
        void pictureManager_TotalPriceChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region UI EVENT HANDLERS
        private void buttonSaveAndAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.SavePics();
        }
        private void buttonSaveAndFinish_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //savePics();
            //finish();

        }

        private void buttonSourceCamera_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        private void buttonSourceCD_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }
        private void buttonSourceFolder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.GetFolderPics();
        }
        private void buttonSourcePenDrive_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pictureManager.GetFolderPics();
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

        private void textBlockReset_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            pictureManager.ClearList();
        }
        private void textBlockSettings_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            showSettings();
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
