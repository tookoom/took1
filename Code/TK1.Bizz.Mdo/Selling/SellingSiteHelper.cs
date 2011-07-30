using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Data;
using TK1.Bizz.Mdo.Xml;
using TK1.Bizz.Mdo.Const;
using System.IO;
using TK1.Data.Controller;
using TK1.Data;
using TK1.Bizz.Mdo.Data.Controller;
using TK1.Bizz.Mdo.Selling.Xml;
using TK1.Bizz.Integra;

namespace TK1.Bizz.Mdo.Selling
{
    public class SellingSiteHelper
    {
        public static string LoadXmlSiteAd(string filePath, bool sendMail)
        {
            string result = string.Empty;
            AuditController audit = null;
            bool loadResult = false;
            try
            {
                audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Pietá.ToString());
                audit.StartProcessExecution();
                audit.WriteEvent("Iniciando Carga de Arquivos", filePath ?? "[NULL PATH]");

                var sites = XmlSiteHelper.LoadSiteFromFile(filePath);
                MdoSiteController siteController = new MdoSiteController(audit);
                if (sites != null)
                {
                    siteController.CheckDataIntegrity(sites);
                    siteController.AddSalesSiteAds(sites);
                }
                audit.WriteEvent("Finalizando Carga de Arquivos", "Sucesso");
                siteController.CleanUpDatabase();
                moveXmlFile(filePath);
                loadResult = true;
            }
            catch (Exception exception)
            {
                if (audit != null)
                    audit.WriteEvent("Finalizando Carga de Arquivos", "Falha");
            }
            finally
            {
                if (audit != null)
                    audit.FinishProcessExecution(loadResult);
                result = audit.GenerateHtmlReport();

                if (sendMail)
                    sendReportMail(result);
            }
            return result;
        }

        private static void moveXmlFile(string sourcePath)
        {
            if (!string.IsNullOrEmpty(sourcePath))
            {
                if (File.Exists(sourcePath))
                {
                    string destinationDir = Path.GetDirectoryName(sourcePath) + @"\Carregado\";
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);
                    destinationDir += DateTime.Now.ToString("yyyy-MM-dd_hhmmsss") + "\\";
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);

                    string fileName = Path.GetFileName(sourcePath);
                    string destinationPath = destinationDir + fileName;
                    File.Move(sourcePath, destinationPath);

                }
            }
        }
        private static void sendReportMail(string body)
        {
            string subject = "Relatório de carga de cadastros IMÓVEIS VENDA";
            AdminHelper.SendMail(subject, body);
            //MailHelper.SendMail(subject, body, MailAdresses.WebMaster, MailAdresses.Vendas);
        }


    }
}
