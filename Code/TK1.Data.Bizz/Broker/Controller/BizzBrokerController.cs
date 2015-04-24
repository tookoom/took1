using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TK1.Data.Bizz.Broker.Model;
using TK1.Data.Bizz.Model;

namespace TK1.Data.Bizz.Broker.Controller
{
    public class BizzBrokerController
    {
        #region PRIVATE MEMBERS

        private BrokerEntities entities;
        
        #endregion

        #region PUBLIC PROPERTIES
        public BrokerEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        public void RollBack()
        {
            try
            {
                var changedEntries = Entities.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
                {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                {
                    entry.State = EntityState.Detached;
                }

                foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                {
                    entry.State = EntityState.Unchanged;
                }


            }
            catch (Exception ex)
            {
                
            }
        }

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
