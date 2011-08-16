using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Mdo.Xml;
using TK1.Bizz.Mdo;
using TK1.Bizz.Mdo.Const;
using TK1.Xml;
using TK1.Bizz.Mdo.Selling;
using TK1.Bizz.Integra;
using TK1.Utility;

namespace TK1.Dev.UnitTest
{
    public class MdoUnitTest
    {
        public static void NormatizeXmlFile()
        {
            var xml = XmlHelper.NormatizeFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\20110518\VisVen.xml");
        }
        public static void LoadXmlFile()
        {
            try
            {
                string sourceDir = @"F:\Projetos\TK1\Code\Web.TK1\Integra\Mdo\SimVendas\Xml";
                string fileFilter = "VisVen*";

                //foreach (var item in FileHelper.GetFiles(sourceDir, fileFilter))
                //{
                    var report = SellingSiteHelper.LoadXmlSiteAd(sourceDir, fileFilter, false);
                //}

                //string picturesPath = @"F:\Projetos\TK1\Code\Web.TK1\Integra\Mdo\SimVendas\Fotos\3";
                //SellingSitePicHelper.ResizeSitePics(picturesPath, 500, 70);
                
                //ImageHelperTestUnit.Resize();
                //

                //if (sourceDirectory == null)
                //    sourceDirectory = string.Empty;
                //string destinationDirectory = @"\Carregados";
                //string errorDirectory = @"\Erro";
                //FileLoader loader = new FileLoader(sourceDirectory, destinationDirectory, errorDirectory);
                //loader.LoadFiles(fileFilter);
                //while (loader.ReadFile())
                //{
                //    SellingSiteHelper.LoadXmlSiteAd(loader.CurrentFile, false);
                //}
            }
            catch (Exception exception)
            {

            }

        }

        private static void searchSite()
        {
            //SiteSearchParameters parameters = new SiteSearchParameters();
            //parameters.AdType = SiteAdTypes.Sell;
            //var sites = SiteController.SearchSites(parameters);
        }


    }
}
