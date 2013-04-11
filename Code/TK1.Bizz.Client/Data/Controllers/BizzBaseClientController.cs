using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Controller;

namespace TK1.Bizz.Client.Data.Controller
{
    public class BizzBaseClientController
    {
        #region PRIVATE MEMBERS

        private TK1ClientBizzEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public TK1ClientBizzEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private TK1ClientBizzEntities getEntities()
        {
            if (entities == null)
                entities = GetBizzClientEntities();
            return entities;
        }

        public static TK1ClientBizzEntities GetBizzClientEntities()
        {
            return new TK1ClientBizzEntities();
        }

    }
}
