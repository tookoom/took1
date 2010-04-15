using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Entity
{
    public class PresentationEntity : BaseEntity
    {
        public string Caption { get; set; }
        public string Description { get; set; }

        public PresentationEntity()
        {
            Caption = string.Empty;
            Description = string.Empty;
        }
    }

}
