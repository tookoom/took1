using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Mdo.Const;
using TK1.Bizz.Data.Presentation;
using TK1.Bizz.Data;

namespace TK1.Bizz.Mdo.Data
{
    public class MdoSiteAdSearchParameters : SiteAdSearchParameters
    {
        #region PUBLIC PROPERTIES
        public int MdoCode { get; set; }
        #endregion

        public MdoSiteAdSearchParameters()
        {
            MdoCode = 0;
        }
    }
}
