using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace TK1.Media.Data
{
    public class Picture
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Path { get; set; }
        public int PixelWidth { get; set; }

        public Picture(string path, int pixelWidth)
        {
            IsSelected = false;
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }

    }
}
