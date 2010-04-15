using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Took1.Data.Entity
{
    public class EntitySet<T> : List<T> where T : class 
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

        public void Add(T entity)
        {
            BaseEntity baseEntity = entity as BaseEntity;
            if (baseEntity != null)
            {
                baseEntity.Key.Name = string.Format("{0}ID", entity.GetType().Name);
                baseEntity.Key.Value = getEntityID();
                baseEntity.PropertyChanged += new EntityEventHandler(baseEntity_PropertyChanged);
            }
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.PropertyType.Name.Contains("EntityReference"))
                {
                    object value = property.GetValue(entity, null);
                    IEnumerable collection = value as IEnumerable;

                    IList list = property.GetValue(entity, null) as IList;
                    if (list != null)
                    { }

                    //if (entityReference != null)
                    //{
                    //    entityReference.ReferencePropertyChanged += new EntityEventHandler(entityReference_ReferencePropertyChanged);
                    //}
                }
            }
            base.Add(entity);
        }

        private int getEntityID()
        {
            int result = 0;
            foreach (var entity in this)
            {
                BaseEntity baseEntity = entity as BaseEntity;
                if (baseEntity != null)
                {
                    if (baseEntity.Key.Value >= result)
                        result = baseEntity.Key.Value + 1;
                }
            }
            return result == 0 ? 1 : result;
        }

        #region EVENT HANDLERS
        void baseEntity_PropertyChanged(object sender, EntityEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void entityReference_ReferencePropertyChanged(object sender, EntityEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

    }
}
