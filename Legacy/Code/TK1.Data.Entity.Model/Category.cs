using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Entity;

namespace TK1.Data.Entity.Model
{
    public class Category :  PresentationEntity
    {
        public EntityReference<AppContext> AppContext { get; set; }

        public Category()
        {
            AppContext = new EntityReference<AppContext>();
        }
    }
}
