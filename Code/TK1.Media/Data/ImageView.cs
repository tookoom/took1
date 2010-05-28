using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;

namespace TK1.Media.Data
{
    public class ImageView
    {
        #region PRIVATE MEMBERS
        private byte[] buffer;
        private MemoryStream memoryStream;
        private BitmapImage source;

        #endregion
        #region PUBLIC PROPERTIES
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Path { get; set; }
        public int PixelHeigth { get; set; }
        public int PixelWidth { get; set; }
        public BitmapImage Source
        {
            get { return source; }
        }
        public MemoryStream MemoryStream
        {
            get { return memoryStream; }
        }

        #endregion

        public ImageView(string path, int pixelHeigth, int pixelWidth)
        {
            IsSelected = false;
            Path = path;
            PixelWidth = pixelWidth;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);

            loadSource();
        }

        private void loadSource()
        {
            if (!string.IsNullOrEmpty(Path))
            {
                buffer = File.ReadAllBytes(Path);
                memoryStream = new MemoryStream(buffer);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.DecodePixelWidth = PixelWidth;
                bitmapImage.DecodePixelHeight = PixelHeigth;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                source = bitmapImage;                
            }
        }

    }
}
