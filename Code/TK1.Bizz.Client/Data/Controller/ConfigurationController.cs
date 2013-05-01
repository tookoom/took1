using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Client.Data.Controller
{
    public class ConfigurationController : BaseClientController
    {
        #region PRIVATE MEMBERS
        private string customerCode;

        #endregion
        #region PUBLIC PROPERTIES
        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }

        #endregion

        public ConfigurationController(string customerCode)
        {
            this.customerCode = customerCode;
        }


        public bool CheckValue(string key)
        {
            bool result = false; ;
            result = Entities.ClientConfiguration.Where(o => o.CustomerCode == customerCode & o.Key == key).Count() > 0;
            return result;
        }
        public bool CheckValue(ConfigurationKeys key)
        {
            return CheckValue(key.ToString());
        }
        public string GetValue(string key)
        {
            string result = null;
            result = Entities.ClientConfiguration.Where(o => o.CustomerCode == customerCode & o.Key == key).Select(o => o.Value).FirstOrDefault();
            if (result == null)
            {
                var message = "Configuration Key = " + key + " not found on ClientConfiguration table.";
                throw new KeyNotFoundException(message);
            }
            return result;
        }
        public string GetValue(ConfigurationKeys key)
        {
            return GetValue(key.ToString());
        }
    }
}
