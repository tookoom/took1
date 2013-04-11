using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Inetsoft.Data;
//using TK1.Bizz.Inetsoft.Xml;
using TK1.Bizz.Inetsoft;
//using TK1.Bizz.Inetsoft.Const;
using TK1.Xml;
using TK1.Bizz.Inetsoft.Rent;
using TK1.Bizz.Integra;
using TK1.Utility;
using TK1.Bizz;

namespace TK1.Dev.UnitTest
{
    public class InetsoftUnitTest
    {
        public static void NormatizeXmlFile()
        {
            var xml = XmlHelper.NormatizeFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\20110518\VisVen.xml");
        }
        public static void LoadXmlFile()
        {
            try
            {
                //string sourceDir = @"F:\Projetos\TK1\Testes\Lançamentos Inetsoft";
                string sourceDir = @"C:\Users\andre\Desktop\Temp\Pieta\Temp";
                //string sourceDir = @"F:\Projetos\TK1\Code\Web.TK1\Integra\Inetsoft\SimVendas\Xml";
                string fileFilter = "imobiliar*";

                RentSiteHelper RentSiteHelper = new RentSiteHelper();
                RentSiteHelper.InetsoftAcronym = "pieta";
                var report = RentSiteHelper.LoadXmlSiteAd(sourceDir, fileFilter);
            }
            catch (Exception exception)
            {

            }

        }
        public static void SendTestMail()
        {
            sendReportMail("teste", "andre.v.mattos@gmail.com");
        }
        private static void searchSite()
        {
            //SiteSearchParameters parameters = new SiteSearchParameters();
            //parameters.AdType = SiteAdTypes.Sell;
            //var sites = SiteController.SearchSites(parameters);
        }
        private static void sendReportMail(string body, string mailTo)
        {

            try
            {
                string subject = "Relatório de carga de cadastros IMÓVEIS VENDA";
                //AdminHelper.SendMail(subject, body);
                //AdminHelper.SendMail(subject, body, mailTo);
            }
            catch (Exception)
            {
            }
        }


    }
}
