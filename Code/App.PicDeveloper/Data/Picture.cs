using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Took1.PicDeveloper.Data
{
    public class Picture
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public PictureType Type { get; set; }
        public PictureSize Size { get; set; }
        public string Path { get; set; }
        public BitmapImage ImageSource { get; set; }

        public Picture(string path, int pixelWidth)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            //_Image = new Image();
            ImageSource = new BitmapImage();
            ImageSource.BeginInit();
            ImageSource.DecodePixelWidth = pixelWidth;
            ImageSource.CacheOption = BitmapCacheOption.OnLoad;
            ImageSource.UriSource = new Uri(Path);
            ImageSource.EndInit();
            //_Image.Source = _Bitmap;
        }

    }
}
