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
using Took1.PicDeveloper.Data;
using System.IO;
using Took1.PicDeveloper.Collection;
using Took1.Xml;
using Took1.Data;
//using System.ComponentModel;
using Took1.PicDeveloper.Control;
using Took1.Data.Converter;

namespace Took1.PicDeveloper
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CONST
        private const string appName = "PicDeveloper";
        private const int thumbPixelWidth = 300;
        private const int zoomPixelWidth = 1000;
        #endregion
        #region DELEGATES
        delegate void PictureCallback(Picture picture);
        delegate void PathCallback(string path);
        #endregion
        #region PRIVATE MEMBERS
        private bool isLoading;
        private bool isMouseDragging = false;
        private bool isUsingUsbDrive = false;
        private Point offsetPoint;
        private PictureSize selectedPicSize;
        private PictureType selectedPicType;
        private int pictureCounter = 0;

        private string configFilePath = string.Empty;
        private string filePath = string.Empty;
        private string clientPictureFolderPath = string.Empty;
        private string windowName = string.Empty;
        private ParameterCollection parameterCollection = new ParameterCollection();

        private Picture bottomLeftImage;
        private Picture buttonIconCamera;
        private Picture buttonIconCD;
        private Picture buttonIconFolder;
        private Picture buttonIconPenDrive;
        private Picture topLeftImage;

        private PictureCollection pictureList;
        private PicturePriceCollection picturePriceCollection;
        //private List<Volume> usbVolumeList = new List<Volume>();
        //private List<Volume> cdromVolumeList = new List<Volume>();

        private float totalPrice = 0;
        private int quantity = 0;


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
                contentPresenterBottomLeftImage.Content = new Image() { Source = bottomLeftImage.ImageSource };
            }
        }
        public string ButtonIconCamera
        {
            set
            {
                buttonIconCamera = new Picture(value, 1000);
                buttonSourceCamera.Content = new Image() { Source = buttonIconCamera.ImageSource };
            }
        }
        public string ButtonIconCD
        {
            set
            {
                buttonIconCD = new Picture(value, 1000);
                buttonSourceCD.Content = new Image() { Source = buttonIconCD.ImageSource };
            }
        }
        public string ButtonIconFolder
        {
            set
            {
                buttonIconFolder = new Picture(value, 1000);
                buttonSourceFolder.Content = new Image() { Source = buttonIconFolder.ImageSource };
            }
        }
        public string ButtonIconPenDrive
        {
            set
            {
                buttonIconPenDrive = new Picture(value, 1000);
                buttonSourcePenDrive.Content = new Image() { Source = buttonIconPenDrive.ImageSource };
            }
        }
        public string TopLeftImage
        {
            set
            {
                topLeftImage = new Picture(value, 1000);
                contentPresenterTopLeftImage.Content = new Image() { Source = topLeftImage.ImageSource };
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                textBlockQuantity.Text = string.Format("{0} Fotos", quantity);
            }
        }
        public float TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                textBlockTotalPrice.Text = string.Format("Total: R$ {0:0.00}", totalPrice);
            }
        }

        #endregion
        
        public MainWindow()
        {
            InitializeComponent();

            initialize();
        }

        private void addPicture(Picture picture)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (picture != null)
                {
                    PictureControl control = new PictureControl();
                    control.FileName = picture.Path;
                    control.contentPresenterPicture.Content = new Image() { Source = picture.ImageSource };
                    control.PicQuantityChanged += new PictureControl.EventHandler(control_PicQuantityChanged);
                    control.ShowZoomedImage += new PictureControl.ZoomEventHandler(control_ShowZoomedImage);
                    wrapPanelPictures.Children.Add(control);
                }
            }
            else
            {
                PictureCallback callback = new PictureCallback(addPicture);
                this.Dispatcher.Invoke(callback, picture);
            }
        }
        private void addPicture(string path)
        {
            if (this.Dispatcher.CheckAccess())
            {
                Picture picture = new Picture(path, thumbPixelWidth);
                pictureList.Add(picture);
                //addPicture(picture);
            }
            else
            {
                PathCallback callback = new PathCallback(addPicture);
                this.Dispatcher.Invoke(callback, path);

            }
        }
        private void bindParameterCollection()
        {
            //dialogWindow.IsVisible = false;
            WindowName = string.Format("{0} - {1}", 
                parameterCollection.GetValue(ParameterNames.WindowName),
                parameterCollection.GetValue(ParameterNames.CustomMessage));
            try
            {
                BottomLeftImage = parameterCollection.GetValue(ParameterNames.BottonLeftImage);
                ButtonIconCamera = parameterCollection.GetValue(ParameterNames.ButtonIconCamera);
                ButtonIconCD = parameterCollection.GetValue(ParameterNames.ButtonIconCD);
                ButtonIconFolder = parameterCollection.GetValue(ParameterNames.ButtonIconFolder);
                ButtonIconPenDrive = parameterCollection.GetValue(ParameterNames.ButtonIconPenDrive);
                TopLeftImage = parameterCollection.GetValue(ParameterNames.TopLeftImage);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Erro ao carregar Imagens",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            clientPictureFolderPath = parameterCollection.GetValue(ParameterNames.PicDirectory);
            selectedPicSize = PictureSizeConverter.StringToPictureType(parameterCollection.GetValue(ParameterNames.StandardPicSize));
            selectedPicType = PictureTypeConverter.StringToPictureType(parameterCollection.GetValue(ParameterNames.StandardPicType));
            changePicInfo();
            loadPrices();
        }
        private void calculateTotalPrice()
        {
            float totalprice = 0;
            float unitprice = 0;
            int count = 0;

            var query = from el in picturePriceCollection
                        where el.Size == selectedPicSize & el.Type == selectedPicType
                        select el.Price;

            if (query.Count() > 0)
                unitprice = query.FirstOrDefault();

            foreach (PictureControl pictureControl in wrapPanelPictures.Children)
            {
                totalprice += pictureControl.Quantity * unitprice;
                count += (int)pictureControl.Quantity;
            }
            TotalPrice = totalprice;
            Quantity = count;
        }
        private static void clearDirectoryContent(string path)
        {
            foreach (string filename in System.IO.Directory.GetFiles(path))
            {
                try
                {
                    System.IO.File.Delete(filename);
                }
                catch { }
            }
        }
        private void changePicInfo()
        {
            if (selectedPicSize == PictureSize._10x15)
                radioButtonSize1.IsChecked = true;

            if (selectedPicSize == PictureSize._13x18)
                radioButtonSize2.IsChecked = true;

            if (selectedPicSize == PictureSize._15x21)
                radioButtonSize3.IsChecked = true;

            if (selectedPicSize == PictureSize._20x25)
                radioButtonSize4.IsChecked = true;

            if (selectedPicSize == PictureSize._20x30)
                radioButtonSize5.IsChecked = true;

            if (selectedPicSize == PictureSize._30x45)
                radioButtonSize6.IsChecked = true;

            if (selectedPicType == PictureType.Gloss)
                radioButtonTypeGloss.IsChecked = true;

            if (selectedPicType == PictureType.Regular)
                radioButtonTypeRegular.IsChecked = true;
        }
        private string createInfoFile()
        {
            string info = "";
            string newline = Environment.NewLine;
            info += string.Format("Registro {0} " + newline, DateTime.Now);
            info += string.Format("Nome do cliente: {0} " + newline, textBoxClientName.Text);
            info += string.Format("Telefone do cliente: {0} " + newline, textBoxClientPhone.Text);
            info += string.Format("Tamanho de foto: {0} " + newline, selectedPicSize);
            info += string.Format("Tipo do papel: {0} " + newline, selectedPicType);
            info += string.Format("Total de fotos: {0} " + newline, pictureCounter);
            info += string.Format("Preço total: R$ {0} " + newline, TotalPrice);
            return info;
        }
        private void finish()
        {
            pictureList.Clear();
            //wrapPanelPictures.Children.Clear();
            Quantity = 0;
            TotalPrice = 0;
            textBoxClientName.Text = "";
            textBoxClientPhone.Text = "";
        }
        private void generateStandardConfigFile()
        {
            if (parameterCollection == null)
                parameterCollection = new ParameterCollection();

            #region BUTTON ICONS
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.ButtonIconCamera,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\Camera.png"
            });
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.ButtonIconCD,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\CD.png"
            });
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.ButtonIconFolder,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\Folder.png"
            });
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.ButtonIconPenDrive,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\PenDrive.png"
            });

            #endregion
            #region PRICE LIST
            if(picturePriceCollection == null)
                picturePriceCollection = new PicturePriceCollection();

            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow10x15, Value = 1.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow13x18, Value = 2.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow15x21, Value = 3.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow20x25, Value = 4.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow20x30, Value = 5.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name = ParameterNames.PriceGlow30x45, Value = 6.ToString(), Type = ParameterTypes.Int });

            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular10x15, Value= 7.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular13x18, Value= 8.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular15x21, Value= 9.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular20x25, Value= 10.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular20x30, Value= 11.ToString(), Type = ParameterTypes.Int });
            parameterCollection.Add(new Parameter() { Name=ParameterNames.PriceRegular30x45, Value= 12.ToString(), Type = ParameterTypes.Int });

            #endregion
            #region STANDARD VALUES
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.PicDirectory,
                Type = ParameterTypes.FilePath,
                Value = @"C:\PicDeveloper"
            });

            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.StandardPicSize,
                Type = ParameterTypes.String,
                Value = "10x15"
            });

            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.StandardPicType,
                Type = ParameterTypes.String,
                Value = "Gloss"
            });

            #endregion
            #region WINDOW IMAGES
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.BottonLeftImage,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\Easter Bunny.jpg"
            });
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.TopLeftImage,
                Type = ParameterTypes.FilePath,
                Value = @"C:\Documents and Settings\All Users\Application Data\Took1\PicDeveloper\Image\porco.bmp"
            });
            
            #endregion
            #region WINDOW MESSAGES
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.CustomMessage,
                Type = ParameterTypes.String,
                Value = "Feliz dia das crianças!"
            });
            parameterCollection.Add(new Parameter
            {
                Name = ParameterNames.WindowName,
                Type = ParameterTypes.String,
                Value = ".:: escolha suas fotos ::."
            });

            #endregion


            generateConfigFile();
        }
        private void generateConfigFile()
        {
            string content = XmlSerializer<ParameterCollection>.Save(parameterCollection);
            if (!Directory.Exists(AppFolder.GetAppFolder()))
                Directory.CreateDirectory(AppFolder.GetAppFolder());
            if (!Directory.Exists(AppFolder.GetAppConfigFolder(appName)))
                Directory.CreateDirectory(AppFolder.GetAppConfigFolder(appName));
            File.WriteAllText(configFilePath, content);

        }
        private void getFolderPics()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;
            loadDirectories(path);

        }
        private void initialize()
        {
            dialogWindow.IsVisible = false;
            dialogWindow.WindowMouseDown += new DialogWindow.EventHandler(dialogWindow_WindowMouseDown);


            pictureList = new PictureCollection();
            pictureList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(pictureList_CollectionChanged);
            configFilePath = AppFolder.GetAppConfigFilePath(appName);
            loadConfigFile();

            TotalPrice = 0;
            textBlockLoading.Text = "";

        }
        private void loadConfigFile()
        {
            try
            {
                if (configFilePath != string.Empty)
                {
                    if (!File.Exists(configFilePath))
                        generateStandardConfigFile();
                    string content = File.ReadAllText(configFilePath);
                    ParameterCollection collection = XmlSerializer<ParameterCollection>.Load(content);
                    if (collection != null)
                    {
                        parameterCollection = collection;
                        bindParameterCollection();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void loadCdromItems()
        {
            //cdromVolumeList.Clear();
            //VolumeDeviceClass volumeDeviceClass = new VolumeDeviceClass();
            //foreach (Volume volume in volumeDeviceClass.Devices)
            //{
            //    if (volume.Class == "CDROM")
            //        cdromVolumeList.Add(volume);
            //}
            //if (cdromVolumeList.Count > 0)
            //{
            //    //if(cdromVolumeList[0].Disks.Count>0)
            //    buttonSourceCD.IsEnabled = true;
            //}
            //else buttonSourceCD.IsEnabled = false;

        }
        private void loadDirectories(string path)
        {
            System.ComponentModel.BackgroundWorker folderBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            folderBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(folderBackgroundWorker_DoWork);
            folderBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(folderBackgroundWorker_RunWorkerCompleted);
            folderBackgroundWorker.RunWorkerAsync(path);
        }
        private void loadUsbItems()
        {
            //isLoading = true;
            //usbVolumeList.Clear();

            //// display volumes
            //VolumeDeviceClass volumeDeviceClass = new VolumeDeviceClass();
            //foreach (Volume volume in volumeDeviceClass.Devices)
            //{
            //    if (volume.IsUsb)
            //        usbVolumeList.Add(volume);
            //}
            //if (usbVolumeList.Count > 0)
            //    buttonSourcePenDrive.IsEnabled = true;
            //else buttonSourcePenDrive.IsEnabled = false;
            //isLoading = false;
        }
        private void loadPrices()
        {
            if (picturePriceCollection == null)
                picturePriceCollection = new PicturePriceCollection();

            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow10x15)),
                Size = PictureSize._10x15,
                Type = PictureType.Gloss
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow13x18)),
                Size = PictureSize._13x18,
                Type = PictureType.Gloss
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow15x21)),
                Size = PictureSize._15x21,
                Type = PictureType.Gloss
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow20x25)),
                Size = PictureSize._20x25,
                Type = PictureType.Gloss
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow20x30)),
                Size = PictureSize._20x30,
                Type = PictureType.Gloss
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow30x45)),
                Size = PictureSize._30x45,
                Type = PictureType.Gloss
            });

            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular10x15)),
                Size = PictureSize._10x15,
                Type = PictureType.Regular
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular13x18)),
                Size = PictureSize._13x18,
                Type = PictureType.Regular
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular15x21)),
                Size = PictureSize._15x21,
                Type = PictureType.Regular
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular20x25)),
                Size = PictureSize._20x25,
                Type = PictureType.Regular
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular20x30)),
                Size = PictureSize._20x30,
                Type = PictureType.Regular
            });
            picturePriceCollection.Add(new PicturePrice
            {
                Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular30x45)),
                Size = PictureSize._30x45,
                Type = PictureType.Regular
            });


        }
        private void removePicture(Picture picture)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (picture != null)
                {
                    PictureControl control = (from el in wrapPanelPictures.Children.Cast<PictureControl>()
                                              where (el.Content as Image).Source == picture.ImageSource
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
        private void resetPicture()
        {
            wrapPanelPictures.Children.Clear();
        }
        private void savePics()
        {
            string path = string.Format("{0}\\{1}_{2}_{3}\\", clientPictureFolderPath,selectedPicSize,selectedPicType, textBoxClientName.Text);
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    //string 
                    clearDirectoryContent(path);
                }
                else
                    System.IO.Directory.CreateDirectory(path);

                foreach (PictureControl pictureControl in wrapPanelPictures.Children)
                {
                    if (!pictureControl.IsSaved)
                    {
                        for (int i = 0; i < pictureControl.Quantity; i++)
                        {
                            pictureCounter++;
                            string newFile = path + pictureCounter.ToString() + ".jpg";
                            try
                            {
                                System.IO.File.Copy(pictureControl.FileName, newFile);
                            }
                            catch { }
                            pictureControl.IsSaved = true;
                        }
                    }
                }
                string info = createInfoFile();
                System.IO.File.WriteAllText(path + "Info.txt", info);

                MessageBox.Show("Arquivos gravados em " + path);

                //try
                //{
                //    if (isUsingUsbDrive)
                //        usbVolumeList[0].Eject(true);
                //}
                //catch { }

            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "ERRO!!!"); }

        }




        //public static string PicTypeToString(PictureType Type)
        //{
        //    switch (Type)
        //    {
        //        case PictureType.Gloss: return "Gloss";
        //        case PictureType.Regular: return "Regular";
        //        default: return "";
        //    }
        //}
        //public static string PicSizeToString(PictureSize Size)
        //{
        //    switch (Size)
        //    {
        //        case PictureSize._10x15: return "10x15";
        //        case PictureSize._13x18: return "13x18";
        //        case PictureSize._15x21: return "15x21";
        //        case PictureSize._20x25: return "20x25";
        //        case PictureSize._20x30: return "20x30";
        //        case PictureSize._30x45: return "30x45";
        //        default: return "";
        //    }
        //}
        //public static PictureType StringToPicType(string Type)
        //{
        //    switch (Type)
        //    {
        //        case "Gloss": return PictureType.Gloss;
        //        case "Regular": return PictureType.Regular;
        //        default: return PictureType.Gloss; ;
        //    }
        //}
        //public static PictureSize StringToPicSize(string Size)
        //{
        //    switch (Size)
        //    {
        //        case "10x15": return PictureSize._10x15;
        //        case "13x18": return PictureSize._13x18;
        //        case "15x21": return PictureSize._15x21;
        //        case "20x25": return PictureSize._20x25;
        //        case "20x30": return PictureSize._20x30;
        //        case "30x45": return PictureSize._30x45;
        //        default: return PictureSize._10x15;
        //    }
        //}

        private IntPtr wndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr result = IntPtr.Zero;
            return result;
            //if (msg == Native.WM_DEVICECHANGE)
            //{
            //    if (!isLoading)
            //    {
            //        loadUsbItems();
            //        loadCdromItems();
            //    }
            //}
            //return IntPtr.Zero;
        }
        void pictureList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    resetPicture();
                    Quantity = 0;
                    calculateTotalPrice();
                }
                else
                {
                    if (e.NewItems != null)
                    {
                        foreach (Picture picture in e.NewItems)
                            addPicture(picture);
                    }
                    if (e.OldItems != null)
                    {
                        foreach (Picture picture in e.OldItems)
                            removePicture(picture);

                    }
                }
            }
        }


        void control_ShowZoomedImage(object sender, ZoomEventArgs e)
        {
            dialogWindow.IsVisible = true;
            Picture picture = new Picture(e.FileName, zoomPixelWidth);
            dialogWindow.DialogContent = new Image() { Source = picture.ImageSource };
        }
        void control_PicQuantityChanged(object sender, EventArgs e)
        {
            calculateTotalPrice();
        }
        private void dialogWindow_WindowMouseDown(object sender, EventArgs e)
        {
            dialogWindow.IsVisible = false;
        }
        void folderBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void folderBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string path = e.Argument as string;
                foreach (string filename in System.IO.Directory.GetFiles(path))
                {
                    addPicture(filename);
                }
                foreach (string subDirectory in System.IO.Directory.GetDirectories(path))
                {
                    loadDirectories(subDirectory);
                }
            }
            catch (Exception exception) { }
        }

        #region UI EVENT HANDLERS
        private void buttonSaveAndAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            savePics();
        }
        private void buttonSaveAndFinish_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            savePics();
            finish();

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
            getFolderPics();
        }
        private void buttonSourcePenDrive_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            getFolderPics();
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
            selectedPicSize = PictureSize._10x15;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonSize2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicSize = PictureSize._13x18;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonSize3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicSize = PictureSize._15x21;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonSize4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicSize = PictureSize._20x25;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonSize5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicSize = PictureSize._20x30;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonSize6_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicSize = PictureSize._30x45;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonTypeGloss_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicType = PictureType.Gloss;
            changePicInfo();
            calculateTotalPrice();
        }
        private void radioButtonTypeRegular_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            selectedPicType = PictureType.Regular;
            changePicInfo();
            calculateTotalPrice();
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
            pictureList.Clear();
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(new HwndSourceHook(wndProc));

            loadUsbItems();
            loadCdromItems();
        }


	    #endregion    
    }
}
