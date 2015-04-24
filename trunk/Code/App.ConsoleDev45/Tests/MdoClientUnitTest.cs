using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Client;
using TK1.Xml;

namespace TK1.Dev.UnitTest
{
    public class MdoClientUnitTest
    {
        public static void NormatizeXmlFile()
        {
            var xml = XmlHelper.NormatizeFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\20110518\VisVen.xml");
        }
        public static void LoadXmlFile()
        {
            try
            {
                //string sourceDir = @"F:\Projetos\TK1\Testes\Lançamentos MDO";
                string sourceDir = @"C:\Users\andre\Desktop\Temp\Pieta\Temp";
                //string sourceDir = @"F:\Projetos\TK1\Code\Web.TK1\Integra\Mdo\SimVendas\Xml";
                string fileFilter = "vendaweb*";

                ClientPropertyAdHelper sellingSiteHelper = new ClientPropertyAdHelper("pieta") { SendReportMail = true };
                var report = sellingSiteHelper.LoadFile(sourceDir, fileFilter);
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
