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
    public class AccountValue
    {
        public string AccountName { get; set; }
        public float Value { get; set; }

        public AccountValue()
        {
            AccountName = "UNDEFINED";

        }
    }
}
