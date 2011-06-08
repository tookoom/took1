using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Pieta.Data
{
    public class LogController
    {

        public static void WriteAppLogEntry(string message, string data)
        {
             WriteAppLogEntry(message, data, LogLevels.Info);
        }
        public static void WriteAppLogEntry(string message, string data, LogLevels level)
        {
            try
            {
                using (PietaEntities entities = new PietaEntities())
                {
                    AppLog result = new AppLog()
                    {
                        LogTimestamp = DateTime.Now,
                        Level = (int)level,
                        Message = message,
                        Data = data
                    };
                    entities.AddToAppLogs(result);
                    entities.SaveChanges();
                }
            }
            catch (Exception)
            {
                
            }
        }

        public static void WriteException(string message, Exception exception)
        {
            WriteException(message, exception, false);
        }
        public static void WriteException(string message, Exception exception, bool sendMail)
        {
            string data = string.Format("{0}", exception);
            WriteAppLogEntry(message, data, LogLevels.Error);
            if (sendMail)
            {
                string body = string.Format("Message: {0}{1}Data: {2}", message, Environment.NewLine, data ?? string.Empty);
                MailHelper.SendWebMasterMail("Exception", body);
            }
        }

        public static XmlLoadLog WriteXmlLoadLogEntry()
        {
            using (PietaEntities entities = new PietaEntities())
            {
                XmlLoadLog result = new XmlLoadLog()
                {
                    Timestamp = DateTime.Now
                };
                entities.AddToXmlLoadLogs(result);
                entities.SaveChanges();
                //entities.Detach(result);
                return result;
            }
        }
        public static XmlLoadMessageLog WriteXmlLoadMessageLogEntry(XmlLoadLog xmlLoadLog, string message, string data, LogLevels level)
        {
            using (PietaEntities entities = new PietaEntities())
            {
                XmlLoadMessageLog result = null;
                if (xmlLoadLog != null)
                {
                    var xmlLoadLogAttached = entities.XmlLoadLogs.Where(o => o.XmlLoadLogID == xmlLoadLog.XmlLoadLogID).FirstOrDefault();
                    if (xmlLoadLogAttached != null)
                    {
                        result = new XmlLoadMessageLog()
                        {
                            Data = data,
                            Level = (int)level,
                            LogTimestamp = DateTime.Now,
                            Message = message,
                            XmlLoadLog = xmlLoadLogAttached
                        };
                        entities.AddToXmlLoadMessageLogs(result);
                        entities.SaveChanges();
                    }
                }
                return result;
            }
        }

    }
}
