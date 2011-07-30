using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Mdo.Data.Controller
{
    public class MdoBaseController
    {
        #region PRIVATE MEMBERS

        private MdoEntities entities;
        
        #endregion

        #region PUBLIC MEMBERS
        public MdoEntities Entities
        {
            get { return getEntities(); }
        }

        #endregion

        private MdoEntities getEntities()
        {
            if (entities == null)
                entities = GetMdoEntities();
            return entities;
        }

        public static MdoEntities GetMdoEntities()
        {
            return new MdoEntities();
        }

    }
}
