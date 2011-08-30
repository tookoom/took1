using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Pieta;
using TK1.Bizz.Pieta.Const;
using TK1.Xml;
using TK1.Bizz.Mdo.Selling;

namespace TK1.Dev.UnitTest
{
    public class PietaUnitTest
    {
        public static void NormatizeXmlFile()
        {
            var xml = XmlHelper.NormatizeFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\20110518\VisVen.xml");
        }
        public static void TestXmlLoad()
        {
            //try
            //{
            //    string sourcePath = @"F:\Projetos\TK1\Projects\Pietá\Integração\20110619\VisVen.xml";
            //    string salesPicuresPath = @"F:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda";
            //    SellingSiteHelper.LoadXmlSiteAd(sourcePath, false);

            //    //SitePicHelper.ResizeSitePics(@"F:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda", 500, 70);
            //    //ImageHelperTestUnit.Resize();
            //    //
            //}
            //catch (Exception exception)
            //{

            //}

        }

        private static void searchSite()
        {
            //SiteSearchParameters parameters = new SiteSearchParameters();
            //parameters.AdType = SiteAdTypes.Sell;
            //var sites = SiteController.SearchSites(parameters);
        }


    }
}
