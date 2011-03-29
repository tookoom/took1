using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TK1.Data.Converter;
using System.Drawing;

namespace TK1.Bizz.Pieta
{
    public class SitePicHelper
    {
        public string RootPicPath { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }

        public SitePicHelper(string rootPath)
        {
            RootPicPath = rootPath;
        }

        public bool Remove(int siteCode, int sitePicCode)
        {
            bool result = false;
            return result;
        }
        public bool Set(int siteCode, int sitePicCode, string data)
        {
            return Set(siteCode, sitePicCode, data, "jpg");
        }
        public bool Set(int siteCode, int sitePicCode, string data, string fileExtension)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(RootPicPath))
                    Directory.CreateDirectory(RootPicPath);

                FileName = GetPicName(siteCode, sitePicCode, fileExtension);
                Path = RootPicPath + string.Format(@"\{0}\", siteCode);
                if (!Directory.Exists(Path))
                    Directory.CreateDirectory(Path);

                Image image = Base64Converter.Base64ToImage(data);
                if (image != null)
                    image.Save(Path + FileName);
            }
            catch (Exception exception)
            {
                
            }

            return result;
        }

        public static string GetMainPicUrl(int siteAdID, int siteAdType)
        {
            string result = "~/Images/PicNotFound.jpg";
            string url = string.Empty;
            if (siteAdType == 1)
                url = string.Format( "~/Imovel/Fotos/Aluguel/{0}/1.jpg",siteAdID);
            if (siteAdType == 2)
                url = string.Format("~/Imovel/Fotos/Venda/{0}/1.jpg", siteAdID);
            if (!string.IsNullOrEmpty(url))
                result = url;
            return result;
        }
        public static List<string> GetSitePics(int siteAdID, int siteAdType)
        {
            List<string> result = new List<string>();// "~/Images/PicNotFound.jpg";
            string url = string.Empty;
            if (siteAdType == 1)
                url = string.Format("~/Imovel/Fotos/Aluguel/{0}/1.jpg", siteAdID);
            if (siteAdType == 2)
                url = string.Format("~/Imovel/Fotos/Venda/{0}/1.jpg", siteAdID);
            if (!string.IsNullOrEmpty(url))
                result.Add(url);
            result.Add("~/Images/PicNotFound.jpg");
            return result;
        }

        public static string GetPicName(int siteCode, int sitePicCode, string fileExtension)
        {
            return string.Format(@"{0}_{1}.{2}", siteCode, sitePicCode, fileExtension);
        }
    }
}
