using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Pieta.Data
{
    public class BaseController
    {
        #region PRIVATE MEMBERS
        private PietaEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public PietaEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion
        private PietaEntities getEntities()
        {
            if (entities == null)
                entities = GetPietaEntities();
            return entities;
        }

        public static PietaEntities GetPietaEntities()
        {
            return new PietaEntities();
        }

    }
}
