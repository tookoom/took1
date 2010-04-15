using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Entity
{
    public class EntityKey
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public EntityKey()
        {
            Name = string.Empty;
            Value = 0;
        }
    }
}
