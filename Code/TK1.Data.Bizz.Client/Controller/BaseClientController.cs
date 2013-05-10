using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Bizz.Client.Model;

namespace TK1.Data.Bizz.Client.Controller
{
    public class BaseClientController
    {
        #region PRIVATE MEMBERS
        
        private TK1ClientBaseEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public TK1ClientBaseEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private TK1ClientBaseEntities getEntities()
        {
            if (entities == null)
                entities = GetTK1ClientBaseEntities();
            return entities;
        }

        public static TK1ClientBaseEntities GetTK1ClientBaseEntities()
        {
            return new TK1ClientBaseEntities();
        }

    }
}
