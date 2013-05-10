using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz;
using TK1.Data;
using TK1.Data.Bizz.Client.Model;

namespace TK1.Data.Bizz.Client.Controller
{
    public class AppLogClientController
    {

        public static void WriteAppLogEntry(string message, string data)
        {
            WriteAppLogEntry(message, data, AppLogLevels.Info);
        }
        public static void WriteAppLogEntry(string message, string data, AppLogLevels level)
        {
            try
            {
                using (TK1ClientBaseEntities entities = BaseClientController.GetTK1ClientBaseEntities())
                {
                    ClientAppLog result = new ClientAppLog()
                    {
                        LogTimestamp = DateTime.Now,
                        LogType = (int)level,
                        Message = message,
                        Data = data
                    };
                    entities.AddToClientAppLog(result);
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
