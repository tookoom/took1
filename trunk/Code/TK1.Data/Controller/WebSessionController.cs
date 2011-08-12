using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Controller
{
    public class WebSessionController
    {
        public static string Get(string websessionID, string key)
        {
            string result = string.Empty;
            try
            {
                using (TK1Entities entities = BaseController.GetTK1Entities())
                {
                    var webSession = entities.WebSessions.Where(o => o.WebSessionID == websessionID & o.Key == key).FirstOrDefault();
                    if (webSession != null)
                        result = webSession.Value;
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("WebSessionController.Get", exception);
            }
            return result;
        }
        public static void Reset(string websessionID)
        {
            try
            {
                using (TK1Entities entities = BaseController.GetTK1Entities())
                {
                    var webSessions = entities.WebSessions.Where(o => o.WebSessionID == websessionID).ToList();
                    foreach (var item in webSessions)
                    {
                        entities.DeleteObject(item);
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("WebSessionController.Reset", exception);
            }
        }
        public static void Set(string websessionID, string key, string value)
        {
            try
            {
                using (TK1Entities entities = BaseController.GetTK1Entities())
                {
                    var webSession = entities.WebSessions.Where(o => o.WebSessionID == websessionID & o.Key == key).FirstOrDefault();
                    if (webSession == null)
                    {
                        webSession = new WebSession()
                        {
                            CreationTimestamp = DateTime.Now,
                            Key = key,
                            Value = value,
                            WebSessionID = websessionID
                        };
                        entities.AddToWebSessions(webSession);
                    }
                    else
                    {
                        webSession.Value = value;
                        webSession.UpdateTimestamp = DateTime.Now;
                    }
                    entities.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                AppLogController.WriteException("WebSessionController.Set", exception);
            }
        }
    }
}
