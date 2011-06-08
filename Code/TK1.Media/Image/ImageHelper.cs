﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace TK1.Media.Imaging
{
    public class ImageHelper
    {
        public static void Resize(string pathFrom, string pathTo, int height, int width)
        {
            if (File.Exists(pathFrom))
            {
                var image = Image.FromFile(pathFrom);
                var resizedImage = resizeImage(image, new Size(width, height));
                resizedImage.Save(pathTo);
            }
        }
        public static void CreateThumbnail(string pathFrom, string pathTo, int height, int width)
        {
            if (File.Exists(pathFrom))
            {
                var image = Image.FromFile(pathFrom);
                var myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                var thumbnail = image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
                //var resizedImage = resizeImage(image, new Size(width, height));
                thumbnail.Save(pathTo);
            }
        }


        public static bool ThumbnailCallback()
        {
            return false;
        }
        //public void Example_GetThumb(PaintEventArgs e)
        //{
        //    Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
        //    Bitmap myBitmap = new Bitmap("Climber.jpg");
        //    Image myThumbnail = myBitmap.GetThumbnailImage(
        //    40, 40, myCallback, IntPtr.Zero);
        //    e.Graphics.DrawImage(myThumbnail, 150, 75);
        //}
        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
        //private static Image resizeImage(Image imgToResize, Size size)
        //{
        //    int sourceWidth = imgToResize.Width;
        //    int sourceHeight = imgToResize.Height;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;
        //}
        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }
        private static void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

    }
}
