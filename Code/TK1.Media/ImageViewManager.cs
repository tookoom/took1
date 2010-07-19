using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Media.Data;
using TK1.Media.Collection;
using System.IO;

namespace TK1.Media
{
    public class ImageViewManager
    {
        #region CONST
        protected const int thumbPixelHeigth = 200;
        protected const int thumbPixelWidth = 300;
        protected const int zoomPixelWidth = 1000;
        #endregion
        #region EVENTS
        public event EventHandler QuantityChanged;
        private void onQuantityChanged(EventArgs e)
        {
            EventHandler handler = QuantityChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
        #region PRIVATE MEMBERS
        protected bool isLoading;
        protected bool isUsingUsbDrive = false;
        protected int imageCount = 0;
        protected bool loadSubDirectories = false;

        protected string filePath = string.Empty;
        protected ImageViewCollection images;

        protected int quantity = 0;

        //private List<Volume> usbVolumeList = new List<Volume>();
        //private List<Volume> cdromVolumeList = new List<Volume>();

        #endregion
        #region PUBIC ACTIONS
        public Action<ImageView> AddPictureWorker;
        public Action<ImageView> RemovePictureWorker;
        public Action ResetWorker;

        #endregion        
        #region PUBLIC PROPERTIES
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                onQuantityChanged(new EventArgs());
            }
        }
        public ImageViewCollection Images
        {
            get { return images; }
            set { images = value; }
        }
        #endregion

        public ImageViewManager()
        {
            initialize();
        }

        public void ClearList()
        {
            if (images != null)
                images.Clear();
        }
        public void GetFolderPics()
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;

            //string path = @"C:\Users\andre\Pictures\DJ";
            //string path = @"C:\Users\andre\Pictures\Fotos";
            if(!string.IsNullOrEmpty(path))
                loadDirectories(path);

        }
        public void LoadCdromItems()
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
        public void LoadUsbItems()
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
        public void SavePics(string path)
        {
            try
            {
                if (System.IO.Directory.Exists(path))
                {
                    clearDirectoryContent(path);
                }
                else
                    System.IO.Directory.CreateDirectory(path);

                foreach (var image in images)
                {
                    //if (!pictureControl.IsSaved)
                    //{
                        for (int i = 0; i < image.Quantity; i++)
                        {
                            imageCount++;
                            string newFile = path + imageCount.ToString(); 
                            if (image.Quantity > 1)
                                newFile += " - Cópia " + i.ToString();
                            newFile += ".jpg";

                            try
                            {
                                System.IO.File.Copy(image.Path, newFile);
                            }
                            catch (Exception exception) { }
                            //pictureControl.IsSaved = true;
                        }
                    //}
                }

                //MessageBox.Show("Arquivos gravados em " + path);

                //try
                //{
                //    if (isUsingUsbDrive)
                //        usbVolumeList[0].Eject(true);
                //}
                //catch { }

            }
            catch (Exception exception) {}

        }

        private void initialize()
        {
            images = new ImageViewCollection();
            images.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(pictures_CollectionChanged);
        }

        protected void addPicture(string path)
        {
            ImageView imageView = new ImageView(path, thumbPixelHeigth, thumbPixelWidth) { Quantity = 0, IsSelected = false };
            images.Add(imageView);
        }
        protected void addPicture(ImageView imageView)
        {
            if (AddPictureWorker != null)
                AddPictureWorker(imageView);
        }
        protected static void clearDirectoryContent(string path)
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
        protected void loadDirectories(string path)
        {
            System.ComponentModel.BackgroundWorker folderBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            folderBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(folderBackgroundWorker_DoWork);
            folderBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(folderBackgroundWorker_RunWorkerCompleted);
            folderBackgroundWorker.RunWorkerAsync(path);
        }
        protected void removePicture(ImageView imageView)
        {
            if (RemovePictureWorker != null)
                RemovePictureWorker(imageView);
        }
        protected void reset()
        {
            if (ResetWorker != null)
                ResetWorker();
        }

        #region EVENT HANDLERS
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
        private void pictures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                {
                    reset();
                    Quantity = 0;
                }
                else
                {
                    if (e.NewItems != null)
                    {
                        foreach (ImageView imageView in e.NewItems)
                        {
                            addPicture(imageView);
                            Quantity += imageView.Quantity;
                        }
                    }
                    if (e.OldItems != null)
                    {
                        foreach (ImageView imageView in e.OldItems)
                        {
                            removePicture(imageView);
                            Quantity -= imageView.Quantity;
                        }
                    }
                }
            }
        }

        private void folderBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        private void folderBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                string path = e.Argument as string;
                foreach (string filePath in System.IO.Directory.GetFiles(path))
                {
                    string extension = Path.GetExtension(filePath);
                    if (extension != null & extension != string.Empty)
                    {
                        if(extension.ToLower() == ".jpg")
                            addPicture(filePath);
                    }
                }
                if (loadSubDirectories)
                {
                    foreach (string subDirectory in System.IO.Directory.GetDirectories(path))
                    {
                        loadDirectories(subDirectory);
                    }
                }
            }
            catch (Exception exception) { }
        }


        #endregion

    }
}
