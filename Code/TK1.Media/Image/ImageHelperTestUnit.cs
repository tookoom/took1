using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Media.Imaging
{
    public class ImageHelperTestUnit
    {
        public static void Resize()
        {
            string sourcePath = @"D:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda\1\TESTE.jpg";
            string resizedPath = @"D:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda\1\resized\TESTE.jpg";
            string thumbnailPath = @"D:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda\1\thumbs\TESTE.jpg";
            int height = 500, width = 500;
            ImageHelper.CreateThumbnail(sourcePath, thumbnailPath, 100, 50);
            ImageHelper.Resize(sourcePath, resizedPath, 500, 500);
        }
    }
}
