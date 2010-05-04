using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data.Entity
{
    public delegate void EntityEventHandler(object sender, EntityEventArgs e);

    public class EntityEventArgs
    {
        public BaseEntity Entity { get; set; }
    }
}
