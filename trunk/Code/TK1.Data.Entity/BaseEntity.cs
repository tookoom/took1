using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Entity
{
    public class BaseEntity
    {
        #region EVENTS
        public event EntityEventHandler PropertyChanged;
        protected void OnPropertyChanged(EntityEventArgs e)
        {
            EntityEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
        #region PUBLIC PROPERTIES
        public string Name { get; set; }
        public EntityKey Key { get; set; }
        public EntityState State { get; set; }

        #endregion

        public BaseEntity()
        {
            Key = new EntityKey();
            Name = string.Empty;
            State = EntityState.New;
        }

    }
}
