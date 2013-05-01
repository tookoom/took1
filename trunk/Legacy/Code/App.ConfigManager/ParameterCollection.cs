using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data;
using System.Collections.ObjectModel;
using TK1.Data.Entity.Model;

namespace TK1.ConfigManager
{
    public class ParameterCollection : ObservableCollection<Parameter>
    {
        public ParameterCollection() { }

        public Parameter Get(string name)
        {
            Parameter result = (from el in this
                             where el.Name == name
                             select el).FirstOrDefault();

            return result;
        }
        public string GetValue(string name)
        {
            string result = (from el in this
                             where el.Name == name
                             select el.Value).FirstOrDefault();

            return result;
        }
    }
}
