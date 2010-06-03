using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace TK1.Basics.Controls
{
    /// <summary>
    /// Interaction logic for ModalDialog.xaml
    /// </summary>
    public partial class ModalDialog : UserControl
    {
        #region PRIVATE MEMBERS
        private bool hideRequest = false;
        private bool result = false;
        private UIElement parent;

        #endregion
        #region PUBLIC PROPERTIES

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                "Message", typeof(string), typeof(ModalDialog),
                new UIPropertyMetadata(string.Empty));

        #endregion

        public ModalDialog()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
        }

        public void SetParent(UIElement parent)
        {
            this.parent = parent;
        }
        public bool ShowDialog(string message)
        {
            Message = message;
            Visibility = Visibility.Visible;

            if (parent != null)
                parent.IsEnabled = false;

            hideRequest = false;
            while (!hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return result;
        }

        private void hideDialog()
        {
            hideRequest = true;
            Visibility = Visibility.Hidden;
            if(parent!= null)
                parent.IsEnabled = true;
        }

        #region UI EVENT HANDLERS
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            result = true;
            hideDialog();
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            result = false;
            hideDialog();
        }

        #endregion
    }
}
