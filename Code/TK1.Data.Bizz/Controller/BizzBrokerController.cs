using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Bizz.Broker.Model;
using TK1.Data.Bizz.Model;

namespace TK1.Data.Bizz.Controller
{
    public class BizzBrokerController
    {
        #region PRIVATE MEMBERS

        private BrokerEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public BrokerEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private BrokerEntities getEntities()
        {
            if (entities == null)
                entities = GetBrokerEntities();
            return entities;
        }

        public static BrokerEntities GetBrokerEntities()
        {
            return new BrokerEntities();
        }

    }
}
