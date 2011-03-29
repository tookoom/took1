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
using TK1.Media.Data;
using TK1.Controls.Binding;
using System.IO;

namespace TK1.Media.Controls
{
	/// <summary>
	/// Interaction logic for ControlPicture.xaml
	/// </summary>
	public partial class ImageSelector
	{
        #region EVENTS
        public event EventHandler QuantityChanged;
        private void onQuantityChanged(EventArgs e)
        {
            EventHandler handler = QuantityChanged;
            if (handler != null)
            {
                // Invokes the delegates.
                handler(this, e);
            }
        }

        public delegate void ZoomEventHandler(Object sender, ZoomEventArgs e);
        public event ZoomEventHandler ShowZoomedImage;
        private void onZoom(ZoomEventArgs e)
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
        private bool hasQuantity = false;
        private bool isSaved = false;
        private ImageView imageView;

        #endregion
        #region PUBLIC PROPERTIES
        public bool HasQuantity
        {
            get { return hasQuantity; }
            set
            {
                hasQuantity = value;
                updateUI();
            }
        }
        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                isSaved = value;
                updateUI();
            }
        }
        public ImageView Picture
        {
            get { return imageView; }
            set { imageView = value;
            setPicture();
            }
        }
        #endregion

        public ImageSelector()
		{
			this.InitializeComponent();
            updateUI();
		}

        public void Update()
        {
            updateUI();
        }

        private void setPicture()
        {
            if (imageView == null)
            {
            }
            else
            {
            }
            this.DataContext = imageView;
            updateUI();
        }
        private void togleQuantity()
        {
            if (!IsSaved)
            {
                if (imageView != null)
                {
                    if (imageView.Quantity == 0)
                    {
                        imageView.Quantity = 1;
                    }
                    else
                    {
                        if (imageView.Quantity == 1)
                            imageView.Quantity = 0;
                    }
                    imageView.IsSelected = imageView.Quantity > 0;
                    updateUI();
                    onQuantityChanged(new EventArgs());
                }
            }
        }
        private void updateSelectedHighlightElement()
        {
            if (imageView != null)
            {
                if (imageView.Quantity > 0)
                    borderSelectedHighlight.Visibility = Visibility.Visible;
                else
                    borderSelectedHighlight.Visibility = Visibility.Collapsed;
            }
        }
        private void updateUI()
        {
            if (hasQuantity)
                gridQuantity.Visibility = Visibility.Visible;
            else
                gridQuantity.Visibility = Visibility.Collapsed;

            updateSelectedHighlightElement();


            BindingHelper.UpdateIsCheckedBindingTarget(checkBoxIsSelected);
            BindingHelper.UpdateTextBindingTarget(textBoxQuantity);

        }


        #region UI EVENT HANDLERS
        private void buttonPicAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (imageView != null)
            {
                imageView.Quantity++;
                imageView.IsSelected = true;
            }
            updateUI();
            onQuantityChanged(new EventArgs());
        }
        private void buttonPicRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (imageView != null)
            {
                if (imageView.Quantity > 0)
                    imageView.Quantity--;
                if(imageView.Quantity == 0)
                    imageView.IsSelected = false;
            }
            updateUI();
            onQuantityChanged(new EventArgs());
        }
        private void buttonZoom_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (imageView != null)
            {
                onZoom(new ZoomEventArgs() { Picture = imageView });
            }
        }

        private void borderSelectedHighlight_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }
        private void imageCheck_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //togleQuantity();
        }

        private void CheckBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                if (checkBox.IsChecked.HasValue)
                {
                    if (checkBox.IsChecked.Value)
                    {
                        imageView.IsSelected = true;
                        if (imageView.Quantity == 0)
                            imageView.Quantity = 1;
                    }
                    else
                    {
                        imageView.IsSelected = false;
                        imageView.Quantity = 0;
                    }
                }
                updateUI();
                onQuantityChanged(new EventArgs());

            }
        }
        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            BindingHelper.UpdateTextBindingSource(sender as TextBox);
            imageView.IsSelected = imageView.Quantity > 0;
            updateUI();
            onQuantityChanged(new EventArgs());
        }

        #endregion    
    }

    public class ZoomEventArgs : EventArgs
    {
        public ImageView Picture { get; set; }
    }


}