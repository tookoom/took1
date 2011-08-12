using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta.Xml;
using TK1.Bizz.Pieta.Const;
using System.IO;

namespace TK1.Bizz.Pieta
{
    public class SiteHelper
    {
        public static string LoadXmlSiteAd(string sourcePath, bool sendMail)
        {
            string result = string.Empty;
            XmlLoadLog log = null;
            bool loadResult = false;
            try
            {
                log = LogController.WriteXmlLoadLogEntry();
                LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivo", sourcePath, LogLevels.Info);

                if (!string.IsNullOrEmpty(sourcePath))
                {
                    if (File.Exists(sourcePath))
                    {
                        var fileTimestamp = File.GetLastWriteTime(sourcePath).ToString("yyyy-MM-dd HH:mm:ss");
                        LogController.WriteXmlLoadMessageLogEntry(log, "Data e hora de escrita do arquivo: " + fileTimestamp, sourcePath, LogLevels.Info);
                    }
                }
                var sites = XmlSiteHelper.LoadSiteFromFile(sourcePath);
                if (sites != null)
                {
                    SiteController siteController = new SiteController();
                    siteController.AddSalesSiteAds(sites);

                }
                LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivo", "Sucesso", LogLevels.Info);
                SiteController.CleanUpDatabase();
                //moveXmlFile(sourcePath);
                loadResult = true;
            }
            catch (Exception exception)
            {
                if (log != null)
                    LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos", "Falha", LogLevels.Info);
                LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
            }
            finally
            {
                result = createSalesLogReport(log, loadResult);
                if(sendMail)
                    sendReportMail(result);
            }
            return result;
        }


        private static string createSalesLogReport(XmlLoadLog log, bool success)
        {
            string result = string.Empty;
            string loadResult = success ? "CARGA REALIZADA COM SUCESSO" : "FALHA NA CARGA DE CADASTROS";
            string timestamp = DateTime.Now.ToString();
            string messages = "";

            try
            {

                if (log != null)
                {
                    using (PietaEntities entities = new PietaEntities())
                    {
                        var attachedLog = entities.XmlLoadLogs.Where(o => o.XmlLoadLogID == log.XmlLoadLogID).FirstOrDefault();
                        if (attachedLog != null)
                        {
                            attachedLog.XmlLoadMessageLogs.Load();
                            foreach (var logMessage in attachedLog.XmlLoadMessageLogs)
                                messages += "<p>" + logMessage.Message + (string.IsNullOrEmpty(logMessage.Data) ? string.Empty : (" - " + logMessage.Data)) + "</p>";
                        }
                    }
                }
                string body = HtmlTemplates.GetXmlSalesFileLoadTemplate();
                if (body.Contains(MailTemplateTags.General.Timestamp))
                    body = body.Replace(MailTemplateTags.General.Timestamp, timestamp);
                if (body.Contains(MailTemplateTags.General.Result))
                    body = body.Replace(MailTemplateTags.General.Result, loadResult);
                if (body.Contains(MailTemplateTags.General.Message))
                    body = body.Replace(MailTemplateTags.General.Message, messages);

                result = body;

            }
            catch (Exception exception)
            {
                LogController.WriteException("Imovel_Xml_Carregar_Default.createSalesMailBody", exception, true);
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
            string subject = "Relatório de carga de cadastros IMÓVEIS VENDA PIETÁ";
            MailHelper.SendWebMasterMail(subject, body);
            MailHelper.SendMail(subject, body, MailAdresses.WebMaster, MailAdresses.Vendas);
        }


    }
}
