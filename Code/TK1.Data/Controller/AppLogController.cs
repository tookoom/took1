using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data
{
    public class AppLogController
    {

        public static void WriteAppLogEntry(string message, string data)
        {
            WriteAppLogEntry(message, data, AppLogLevels.Info);
        }
        public static void WriteAppLogEntry(string message, string data, AppLogLevels level)
        {
            try
            {
                using (TK1Entities entities = BaseController.GetTK1Entities())
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
            WriteAppLogEntry(message, data, AppLogLevels.Error);
            if (sendMail)
            {
                string body = string.Format("Message: {0}{1}Data: {2}", message, Environment.NewLine, data ?? string.Empty);
                //MailHelper.SendWebMasterMail("Exception", body);
            }
        }
    }
}
