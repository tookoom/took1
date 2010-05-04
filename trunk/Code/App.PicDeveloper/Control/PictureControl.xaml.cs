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
	/// Interaction logic for ControlPicture.xaml
	/// </summary>
	public partial class PictureControl
	{
        #region EVENTS
        public delegate void EventHandler(Object sender, EventArgs e);
        public event EventHandler PicQuantityChanged;
        protected virtual void OnQuantityChanged(EventArgs e)
        {
            EventHandler handler = PicQuantityChanged;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }

        public delegate void ZoomEventHandler(Object sender, ZoomEventArgs e);
        public event ZoomEventHandler ShowZoomedImage;
        protected virtual void OnZoom(ZoomEventArgs e)
        {
            ZoomEventHandler handler = ShowZoomedImage;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }



        #endregion
        #region PRIVATE MEMBERS
        private string fileName = "";
        private int quantity = 0;
        private bool isChecked = false;
        private bool isSaved = false;

        #endregion
        #region PUBLIC PROPERTIES
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                setControlStatus();
            }
        }
        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                isSaved = value;
                setControlStatus();
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                textBoxQuantity.Text = quantity.ToString();
                if (quantity == 0) IsChecked = false;
                else IsChecked = true;
            }
        }

        #endregion

        public PictureControl()
		{
			this.InitializeComponent();

            setControlStatus();
		}

        private void setControlStatus()
        {
            if (isChecked & !isSaved)
            {
                imageCheck.Visibility = Visibility.Visible;
                imageSave.Visibility = Visibility.Collapsed;

                buttonPicAdd.IsEnabled = true;
                buttonPicRemove.IsEnabled = true;
                buttonZoom.IsEnabled = true;
                textBoxQuantity.IsEnabled = true;

            }
            else if (isChecked & isSaved)
            {
                imageCheck.Visibility = Visibility.Collapsed;
                imageSave.Visibility = Visibility.Visible;

                buttonPicAdd.IsEnabled = false;
                buttonPicRemove.IsEnabled = false;
                buttonZoom.IsEnabled = false;
                textBoxQuantity.IsEnabled = false;
                
            }
            else
            {
                imageCheck.Visibility = Visibility.Collapsed;
                imageSave.Visibility = Visibility.Collapsed;
            }

        }
        private void togleQuantity()
        {
            if (!IsSaved)
            {
                if (Quantity == 0) Quantity++;
                else
                {
                    if (Quantity == 1) Quantity = 0;
                }
                OnQuantityChanged(new EventArgs());
            }
        }

        #region UI EVENT HANDLERS
        private void buttonPicAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Quantity++;
            OnQuantityChanged(new EventArgs());
        }
        private void buttonPicRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Quantity > 0) Quantity--;
            OnQuantityChanged(new EventArgs());
        }
        private void buttonZoom_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnZoom(new ZoomEventArgs() { FileName = fileName });
        }

        private void contentPresenterPicture_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }

        private void imageCheck_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }
        
        #endregion    
    }

    public class ZoomEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }


}