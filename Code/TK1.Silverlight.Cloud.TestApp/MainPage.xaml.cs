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
using Took1.Web.Cloud;
using System.Windows.Ria.Data;

namespace Took1.Silverlight.Cloud.TestApp
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var domain = new LifeManagerDomainContext();
            LoadOperation<Person> io = domain.Load<Person>(domain.GetPersonQuery());
            io.Completed += new EventHandler(io_Completed);

        }

        void io_Completed(object sender, EventArgs e)
        {
            LoadOperation<Person> loadOperation = sender as LoadOperation<Person>;
            if (loadOperation.HasError)
            {
                MessageBox.Show(loadOperation.Error.Message);
            }
        }

        void invokeOperation_Completed(object sender, EventArgs e)
        {
            InvokeOperation<string> invokeOperation = sender as InvokeOperation<string>;
            if (invokeOperation != null)
            {
                WorldResponse.Text = invokeOperation.Value;
            }
        }
    }
}
