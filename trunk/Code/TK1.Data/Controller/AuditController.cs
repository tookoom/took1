using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Html;
using TK1.Html.Elements;

namespace TK1.Data.Controller
{
    public class AuditController: BaseController
    {
        private string appName;
        private string customerName;
        private int processID;
        private ProcExec processExecution;

        public AuditController(int processID) 
        {
            this.processID = processID;
        }
        public AuditController(string appName, string customerName)
        {
            Process process = Entities.Processes.Where(o => o.App.Name == appName & o.Customer.Name == customerName).FirstOrDefault();
            if(process!= null)
                this.processID = process.ProcessID;
        }

        public void FinishProcessExecution(bool status)
        {
            if (processExecution != null)
            {
                processExecution.Status = status ? "S" : "E";
                processExecution.EndTimestamp = DateTime.Now;
                Entities.SaveChanges();
            }
        }
        public string GenerateHtmlReport()
        {
            HtmlDocument report = new HtmlDocument();
            report.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");

            if (processExecution == null)
            {
                report.Body.Children.Add(new HtmlHeading(2,"Falha na geração de log"));
            }
            else
            {
                processExecution.ProcExecStatusReference.Load();
                report.Body.Children.Add(new HtmlHeading(2, "Informações"));
                report.Body.Children.Add(new HtmlParagraph(string.Format("Início: {0}",processExecution.BeginTimestamp.ToString())));
                report.Body.Children.Add(new HtmlParagraph(string.Format("Fim: {0}",processExecution.EndTimestamp.ToString())));
                report.Body.Children.Add(new HtmlParagraph(string.Format("Status: {0}",processExecution.ProcExecStatus.Description)));

                report.Body.Children.Add(new HtmlHeading(2, "Eventos"));
                processExecution.EventLogs.Load();
                if (processExecution.EventLogs.Count > 0)
                {
                    HtmlTable table = new HtmlTable();
                    table.Attributes.Set("style", "");
                    HtmlTableRow tableHeader = new HtmlTableRow();
                    tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Data<\b>") }));
                    tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Mensagem<\b>") }));
                    tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Dados<\b>") }));
                    table.Children.Add(tableHeader);
                    foreach (var item in processExecution.EventLogs)
                    {
                        HtmlTableRow tableRow = new HtmlTableRow();
                        tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.EventTimestamp.ToString()) }));
                        tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.Message) }));
                        tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.Data) }));
                        table.Children.Add(tableRow);
                    }
                    report.Body.Children.Add(table);
                }
            }
            return report.GetHtml();
        }
        public void StartProcessExecution()
        {
            processExecution = new ProcExec()
            {
                ProcessID = processID,
                BeginTimestamp = DateTime.Now,
                Status = "O"
            };
            Entities.AddToProcExecs(processExecution);
            Entities.SaveChanges();
        }
        public void WriteEvent(string message, string data)
        {
            WriteEvent("[UNKNOWN]", "[UNKNOWN]", message, data, EventTypes.Info);
        }
        public void WriteEvent(string assembly, string source, string message, string data)
        {
            WriteEvent(assembly, source, message, data, EventTypes.Info);
        }
        public void WriteEvent(string assembly, string source, string message, string data, EventTypes level)
        {
            try
            {
                if (processExecution != null)
                {
                    EventType eventType = Entities.EventTypes.Where(o => o.EventTypeID == (int)level).FirstOrDefault();
                    if (eventType != null)
                    {
                        EventLog log = new EventLog()
                        {
                            EventType = eventType,
                            Data = data,
                            Message = message,
                            ProcExec = processExecution,
                            EventTimestamp = DateTime.Now,
                            Source = source,
                            Assembly = assembly
                        };
                        processExecution.EventLogs.Add(log);
                        Entities.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                
            }
        }
        public void WriteException(string message, Exception exception)
        {
            WriteException("[UNKNOWN]", "[UNKNOWN]", message, exception, false);
        }
        public void WriteException(string assembly, string source, string message, Exception exception)
        {
            WriteException(assembly, source, message, exception, false);
        }
        public void WriteException(string assembly, string source, string message, Exception exception, bool sendMail)
        {
            string data = string.Format("{0}", exception);
            WriteEvent(assembly, source, message, data, EventTypes.Error);
            //if (sendMail)
            //{
            //    string body = string.Format("Message: {0}{1}Data: {2}", message, Environment.NewLine, data ?? string.Empty);
            //    MailHelper.SendWebMasterMail("Exception", body);
            //}
        }


    }
}
