using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class SeqGenerator : BaseModel
    {
        public int SeqGeneratorID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

    }
}
