using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace TK1.PicDeveloper.Data
{
    public class Picture
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Path { get; set; }
        public int PixelWidth { get; set; }
        //public BitmapImage ImageSource { get; set; }
        public PaperTypes Type { get; set; }
        public PaperSizes Size { get; set; }

        public Picture(string path, int pixelWidth)
        {
            IsSelected = false;
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
            ////_Image = new Image();
            //ImageSource = new BitmapImage();
            //ImageSource.BeginInit();
            //ImageSource.DecodePixelWidth = pixelWidth;
            //ImageSource.CacheOption = BitmapCacheOption.OnLoad;
            //ImageSource.UriSource = new Uri(Path);
            //ImageSource.EndInit();
            ////_Image.Source = _Bitmap;
        }

    }
}
