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

namespace TK1.Basics.Controls
{
    /// <summary>
    /// Interaction logic for WindowController.xaml
    /// </summary>
    public partial class WindowController : UserControl
    {
        #region EVENTS
        public Action OpenSettings;
        public Action ReloadSettings;
        public Action SaveSettings;

        //public event EventHandler ClientEntitiesChanged;
        //private void OnClientEntitiesChanged(EventArgs e)
        //{
        //    EventHandler handler = ClientEntitiesChanged;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}
        #endregion
        #region PRIVATE MEMBERS
        private bool canMaximize;
        private bool canMinimize;
        private string title;
        private Window window;

        #endregion
        #region PUBLIC PROPERTIES
        public bool CanMaximize
        {
            get { return canMaximize; }
            set { canMaximize = value; }
        }
        public bool CanMinimize
        {
            get { return canMinimize; }
            set { canMinimize = value; }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                textBlockTitle.Text = title ?? "[NULL]";
            }
        }
        public Window Window
        {
            get
            {
                return window;
            }
            set
            {
                if (window == value)
                    return;
                window = value;
                setWindowHandlers();
            }
        }

        #endregion


        public WindowController()
        {
            InitializeComponent();

            initializeUI();
        }

        private void closeWindow()
        {
            if (window != null)
                window.Close();
        }
        private void initializeUI()
        {
        }
        private void maximizeWindow()
        {
            if (window != null)
            {
                if(window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else
                    window.WindowState = WindowState.Maximized;
            }
        }
        private void minimizeWindow()
        {
            if (window != null)
                window.WindowState = WindowState.Minimized;
        }
        private void setWindowHandlers()
        {
            if (window != null)
            {
            }
        }

        #region UI EVENT HANDLERS
        private void buttonClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            closeWindow();
        }
        private void buttonMaximize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            maximizeWindow();
        }
        private void buttonMinimize_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            minimizeWindow();
        }

        private void rectGrip_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (window != null)
                window.DragMove();
        }
        #endregion
    }
}
