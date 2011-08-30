using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Data.Controller
{
    public class BizzBaseController
    {
        #region PRIVATE MEMBERS

        private BizzEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public BizzEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private BizzEntities getEntities()
        {
            if (entities == null)
                entities = GetMdoEntities();
            return entities;
        }

        public static BizzEntities GetMdoEntities()
        {
            return new BizzEntities();
        }

    }
}
