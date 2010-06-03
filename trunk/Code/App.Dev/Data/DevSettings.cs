using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Dev.Data
{
    public class DevSettings
    {
        public string Prop1 { get; set; }
        public int Prop2 { get; set; }

        public DevSettings()
        {
            Prop1 = "Teste";
            Prop2 = 5;
        }
    }
}
