using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Took1.Data;
using Took1.Data.Entity.Model;

namespace Took1.PicDeveloper.Collection
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
