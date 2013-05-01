using System;
using System.Collections.Generic;
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


namespace TK1.PicDeveloper.Control
{
	/// <summary>
	/// Interaction logic for DialogWindow.xaml
	/// </summary>
	public partial class DialogWindow
	{
        #region EVENTS
        public delegate void EventHandler(Object sender, EventArgs e);
        public event EventHandler WindowMouseDown;
        protected virtual void OnWindowMouseDown(EventArgs e)
        {
            EventHandler handler = WindowMouseDown;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }

        public event EventHandler ContentMouseDown;
        protected virtual void OnContentMouseDown(EventArgs e)
        {
            EventHandler handler = ContentMouseDown;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }

        #endregion
        #region PRIVATE MEMBERS
        private bool isVisible = false;

        #endregion
        #region PUBLIC PROPERTIES
        public object DialogContent
        {
            get { return contentPresenteMain.Content; }
            set { contentPresenteMain.Content = value; }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                if (isVisible) this.Visibility = Visibility.Visible;
                else this.Visibility = Visibility.Collapsed;
            }
        }
        public string ReturnMessage
        {
            get { return textBlockReturnMessage.Text; }
            set { textBlockReturnMessage.Text = value; }
        }

        #endregion

		public DialogWindow()
		{
			this.InitializeComponent();
		}

        #region UI EVENT HANDLERS
        private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnWindowMouseDown(new EventArgs());
        }
        private void contentPresenteMain_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnContentMouseDown(new EventArgs());

        }
        
        #endregion

	}
}