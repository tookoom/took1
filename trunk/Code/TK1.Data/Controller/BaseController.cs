using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data
{
    public class BaseController
    {
        #region PRIVATE MEMBERS
        
        private TK1Entities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public TK1Entities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private TK1Entities getEntities()
        {
            if (entities == null)
                entities = GetTK1Entities();
            return entities;
        }

        public static TK1Entities GetTK1Entities()
        {
            return new TK1Entities();
        }

    }
}
