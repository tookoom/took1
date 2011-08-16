﻿using System;
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
        public static string LoadXmlSiteAd(string sourceDir, string fileFilter, bool sendMail)
        {
            string result = string.Empty;
            AuditController audit = null;
            bool loadResult = false;
            int errorCount = 0;
            int successCount = 0;
            try
            {
                audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Pietá.ToString());
                audit.StartProcessExecution();
                audit.WriteEvent("Iniciando processo de carga de cadastro de imóveis", sourceDir ?? "[NULL DIR]");

                MdoSiteController siteController = new MdoSiteController(audit);
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
                    destinationDir += DateTime.Now.ToString("yyyy-MM-dd_HHmmsss") + "\\";
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
