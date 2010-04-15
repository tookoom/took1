using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Entity
{
    public class EntityReference<T> where T : class 
    {
        #region EVENTS
        public event EntityEventHandler ReferencePropertyChanged;
        protected void OnReferencePropertyChanged(EntityEventArgs e)
        {
            EntityEventHandler handler = ReferencePropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region PRIVATE MEMBERS
        private T entity;
        private EntityKey key;
        private Type referenceType;



        #endregion
        #region PRIVATE MEMBERS
        public T Entity
        {
            get { return entity; }
            set
            {
                entity = value;
                BaseEntity baseEntity = entity as BaseEntity;
                if (baseEntity != null)
                {
                    key = baseEntity.Key;
                }
            }
        }
        public EntitySet<T> ReferenceEntitySource { get; set; }
        public EntityKey ReferenceKey
        {
            get { return key; }
            set { key = value; }
        }
        public Type ReferenceType
        {
            get { return referenceType; }
            set { referenceType = value; }
        }

        #endregion

        public EntityReference()
        {
            ReferenceKey = new EntityKey();
            referenceType = typeof(T);
        }
    }
}
