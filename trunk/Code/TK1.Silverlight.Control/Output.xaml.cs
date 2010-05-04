using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Numericon.Web.Silverlight.Controls
{
    public partial class Output : UserControl
    {
        #region PUBLIC PROPERTIES
        public string Text
        {
            get { return textBlockOutput.Text; }
            set { textBlockOutput.Text = value; }
        }

        #endregion

        public Output()
        {
            InitializeComponent();
        }

        public void Append(string text)
        {
            Text += text + Environment.NewLine;
        }
        public void Close()
        {
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            Text = "";
        }

    }
}
