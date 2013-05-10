using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz;
using TK1.Data.Bizz.Client.Model;
using TK1.Html;
using TK1.Html.Elements;

namespace TK1.Data.Bizz.Client.Controller
{
    public class AuditClientController : BaseClientController
    {        
        #region PRIVATE MEMBERS
        private string customerCode;
        private string guid;
        private string processName;

        private DateTime beginTimeStamp;
        private DateTime endTimeStamp;

        private bool hasError = false;


        #endregion
        #region PUBLIC PROPERTIES
        public string CustomerCode
        {
            get { return customerCode; }
        }
        public string Guid
        {
            get { return guid; }
        }
        public bool HasError
        {
            get { return hasError; }
        }
        public string ProcessName
        {
            get { return processName; }
        }
        #endregion

        public AuditClientController(string customerCode, string processName)
        {
            this.customerCode = customerCode;
            this.guid = System.Guid.NewGuid().ToString();
            this.processName = processName;
        }
        public AuditClientController(string customerCode, string processName, string guid)
        {
            this.customerCode = customerCode;
            this.guid = guid;
            this.processName = processName;
        }

        public void FinishProcessExecution(bool status)
        {
            endTimeStamp = DateTime.Now;
            WriteAuditEvent("[AUDIT EXECUTION END]", status.ToString());
        }
        public string GenerateHtmlReport()
        {
            HtmlDocument report = new HtmlDocument();
            report.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");

            report.Body.Children.Add(new HtmlHeading(2, "Informações"));
            report.Body.Children.Add(new HtmlParagraph(string.Format("Início: {0}", beginTimeStamp.ToString())));
            report.Body.Children.Add(new HtmlParagraph(string.Format("Fim: {0}", endTimeStamp.ToString())));
            report.Body.Children.Add(new HtmlParagraph(string.Format("Status: {0}", hasError ? "Erro" : "Concluído com Sucesso")));

            report.Body.Children.Add(new HtmlHeading(2, "Eventos"));
            var events = Entities.ClientAudit.Where(o => o.GUID == guid & o.ReportVisible == true).ToList();
            if (events.Count > 0)
            {
                HtmlTable table = new HtmlTable();
                table.Attributes.Set("style", "");
                HtmlTableRow tableHeader = new HtmlTableRow();
                tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Data<\b>") }));
                tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Mensagem<\b>") }));
                tableHeader.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral("<b>Dados<\b>") }));
                table.Children.Add(tableHeader);
                foreach (var item in events)
                {
                    HtmlTableRow tableRow = new HtmlTableRow();
                    tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.EventTimestamp.ToString()) }));
                    tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.Message) }));
                    tableRow.Children.Add(new HtmlTableDivision(new Html.Collection.HtmlElementCollection() { new HtmlLiteral(item.Data) }));
                    table.Children.Add(tableRow);
                }
                report.Body.Children.Add(table);
            }
            return report.GetHtml();
        }
        public void StartProcessExecution()
        {
            beginTimeStamp = DateTime.Now;
            WriteAuditEvent("[AUDIT EXECUTION BEGIN]", "");
        }
        public void WriteAuditEvent(string message, string data)
        {
            try
            {
                Entities.ClientAudit.AddObject(new ClientAudit
                {
                    Assembly = string.Empty,
                    CustomerCode = customerCode,
                    Data = data,
                    EventTimestamp = DateTime.Now,
                    EventType = (int)AppLogLevels.Info,
                    GUID = guid,
                    Message = message,
                    ProcessName = processName,
                    ReportVisible = false,
                    Source = string.Empty
                });
                Entities.SaveChanges();
            }
            catch (Exception exception)
            {

            }
        }
        public void WriteEvent(string message, string data)
        {
            WriteEvent("[UNKNOWN]", "[UNKNOWN]", message, data, AppLogLevels.Info);
        }
        public void WriteEvent(string assembly, string source, string message, string data)
        {
            WriteEvent(assembly, source, message, data, AppLogLevels.Info);
        }
        public void WriteEvent(string assembly, string source, string message, string data, AppLogLevels level)
        {
            try
            {
                Entities.ClientAudit.AddObject(new ClientAudit
                {
                    Assembly = assembly,
                    CustomerCode = customerCode,
                    Data = data,
                    EventTimestamp = DateTime.Now,
                    EventType = (int)level,
                    GUID = guid,
                    Message = message,
                    ProcessName = processName,
                    ReportVisible = true,
                    Source = source
                });
                Entities.SaveChanges();
            }
            catch (Exception exception)
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
            hasError = true;
            string data = string.Format("{0}", exception);
            WriteEvent(assembly, source, message, data, AppLogLevels.Error);
            //if (sendMail)
            //{
            //    string body = string.Format("Message: {0}{1}Data: {2}", message, Environment.NewLine, data ?? string.Empty);
            //    MailHelper.SendWebMasterMail("Exception", body);
            //}
        }

    }
}
