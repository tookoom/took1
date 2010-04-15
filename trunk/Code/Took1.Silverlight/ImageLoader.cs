///////////////////////////////////////////////////////////
//  ImageLoader.cs
//  Implementation of the Class ImageLoader
//  Generated by Enterprise Architect
//  Created on:      13-nov-2008 15:31:41
//  Original author: Andr� Matos
///////////////////////////////////////////////////////////




using System.Windows.Controls;
using System;
using System.Windows.Media.Imaging;
namespace Took1.Silverlight {
	/// <summary>
	/// Cria imagem a partir de endere�o do arquivo
	/// </summary>
	public class ImageLoader {

    public static Image Get(string imageUri)
    {
        Image image = new Image();
        image.Source = new BitmapImage(new Uri(imageUri));
        return image;
    }
    public static Image GetRelative(string imageUri)
    {
        Image image = new Image();
        Uri uri = new Uri(imageUri, UriKind.Relative);
        image.Source = new BitmapImage(uri);
        return image;
    }

	}//end ImageLoader

}//end namespace Silverlight