using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Inetsoft.Data;
//using TK1.Bizz.Inetsoft.Xml;
//using TK1.Bizz.Inetsoft.Const;
using System.IO;
using TK1.Data.Controller;
using TK1.Data;
using TK1.Bizz.Inetsoft.Data.Controller;
using TK1.Bizz.Inetsoft.Rent.Xml;
using TK1.Bizz.Integra;
using TK1.Utility;
using TK1.Bizz.Net;

namespace TK1.Bizz.Inetsoft.Rent
{
    public class RentSiteHelper
    {
        #region PUBLIC PROPERTIES
        public string InetsoftAcronym { get; set; }
        public bool SendReportMail { get; set; }
        public bool SendErrorOnly { get; set; }

        #endregion

        public RentSiteHelper()
        {
            SendReportMail = true;
            SendErrorOnly = true;
        }

        public string LoadXmlSiteAd(string sourceDir, string fileFilter)
        {
            string result = string.Empty;
            AuditController audit = null;
            bool loadResult = false;
            int errorCount = 0;
            int successCount = 0;
            InetsoftSiteAdController siteController = null;
            try
            {
                audit = new AuditController(AppNames.IntegraInetsoftRent.ToString(), CustomerNames.Pietá.ToString());
                audit.StartProcessExecution();
                audit.WriteEvent("Iniciando processo de carga de cadastro de imóveis", sourceDir ?? "[NULL DIR]");

                siteController = new InetsoftSiteAdController(audit);
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
                            siteController.AddRentSiteAds(sites);
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
                        mailTo = siteController.GetXmlRentLoadEmail(InetsoftAcronym);
                    sendReportMail(result, mailTo, errorCount > 0);
                }
            }
            return result;
        }

        private void moveXmlFile(string sourcePath)
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
        private void sendReportMail(string body, string mailTo, bool hasError)
        {

            try
            {
                string subject = "Relatório de carga de cadastros IMÓVEIS ALUGUEL";
                if (SendReportMail)
                {
                    var mailHelper = new MailHelper();
                    if (SendErrorOnly)
                        mailHelper.SendMail(subject, body, "suporte@tk1.net.br", false);
                    mailHelper.SendMail(subject, body, mailTo, false);
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("Bizz.Inetsoft.RentSiteHelper.sendReportMail", exception);
            }
        }


    }
}
