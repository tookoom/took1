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
using TK1.Utility;

namespace TK1.Bizz.Mdo.Selling
{
    public class SellingSiteHelper
    {
        #region PUBLIC PROPERTIES
        public string MdoAcronym { get; set; }
        public bool SendReportMail { get; set; }

        #endregion

        public SellingSiteHelper()
        {
            SendReportMail = true;
        }

        public string LoadXmlSiteAd(string sourceDir, string fileFilter)
        {
            string result = string.Empty;
            AuditController audit = null;
            bool loadResult = false;
            int errorCount = 0;
            int successCount = 0;
            MdoSiteAdController siteController = null;
            try
            {
                audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Pietá.ToString());
                audit.StartProcessExecution();
                audit.WriteEvent("Iniciando processo de carga de cadastro de imóveis", sourceDir ?? "[NULL DIR]");

                siteController = new MdoSiteAdController(audit);
                var files = FileHelper.GetFiles(sourceDir, fileFilter);
                audit.WriteEvent("Total de arquivos a carregar ", files.Count.ToString());
                foreach (var filePath in files)
                {
                    try
                    {
                        audit.WriteEvent("Início da carga de arquivo", filePath ?? "[NULL PATH]");
                        audit.WriteEvent("Data de modificação do arquivo", File.GetLastWriteTime(filePath).ToString("yyyy-MM-dd HH:mm:ss"));
                        var sites = XmlSiteHelper.LoadSiteFromFile(filePath);
                        if (sites != null)
                        {
                            siteController.CheckDataIntegrity(sites);
                            siteController.AddSalesSiteAds(sites);
                            siteController.AddSalesSiteReleaseAds(sites);
                        }
                        moveXmlFile(filePath);
                        successCount++;
                        audit.WriteEvent("Arquivo carregado com sucesso", filePath ?? "[NULL PATH]");
                    }
                    catch (Exception fileException)
                    {
                        errorCount++;
                        if (audit != null)
                            audit.WriteException("Falha na carga do arquivo", fileException);
                    }
                }
                audit.WriteEvent("Arquivos carregados com sucesso", successCount.ToString());
                audit.WriteEvent("Arquivos com falha na carga", errorCount.ToString());
                audit.WriteEvent("Finalizando processo de carga", "Sucesso");
                siteController.CleanUpDatabase();
                loadResult = true;
            }
            catch (Exception exception)
            {
                if (audit != null)
                    audit.WriteException("Finalizando processo de carga", exception);
            }
            finally
            {
                if (audit != null)
                    audit.FinishProcessExecution(loadResult);
                result = audit.GenerateHtmlReport();

                if (SendReportMail)
                {
                    string mailTo = string.Empty;
                    if (siteController != null)
                        mailTo = siteController.GetXmlSellingLoadEmail(MdoAcronym);
                    sendReportMail(result, mailTo);
                }
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
                    destinationDir += DateTime.Now.ToString("yyyy-MM-dd_HHmmsss") + "\\";
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);

                    string fileName = Path.GetFileName(sourcePath);
                    string destinationPath = destinationDir + fileName;
                    File.Move(sourcePath, destinationPath);

                }
            }
        }
        private static void sendReportMail(string body, string mailTo)
        {

            try
            {
                string subject = "Relatório de carga de cadastros IMÓVEIS VENDA";
                AdminHelper.SendMail(subject, body);
                AdminHelper.SendMail(subject, body, mailTo);
            }
            catch (Exception)
            {
            }
        }


    }
}
