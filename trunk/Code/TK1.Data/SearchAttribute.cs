using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Data
{
    public class SearchAttribute
    {
        public object Attribute { get; set; }
        public SearchAttributeDataTypes DataType { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public SearchOperators Operator { get; set; }
        public object Value { get; set; }
    }
}
