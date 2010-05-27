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
using TK1.Wpf.Controls.Binding;

namespace TK1.Media.Controls
{
	/// <summary>
	/// Interaction logic for ControlPicture.xaml
	/// </summary>
	public partial class PictureSelector
	{
        #region EVENTS
        public event EventHandler PicQuantityChanged;
        private void onQuantityChanged(EventArgs e)
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
        private bool isSaved = false;

        private Picture picture;


        #endregion
        #region PUBLIC PROPERTIES
        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                isSaved = value;
                setControlStatus();
            }
        }
        public Picture Picture
        {
            get { return picture; }
            set { picture = value;
            setPicture();
            }
        }
        #endregion

        public PictureSelector()
		{
			this.InitializeComponent();

            setControlStatus();
		}

        private void setControlStatus()
        {
            //if (isChecked & !isSaved)
            //{
            //    imageCheck.Visibility = Visibility.Visible;
            //    imageSave.Visibility = Visibility.Collapsed;

            //    buttonPicAdd.IsEnabled = true;
            //    buttonPicRemove.IsEnabled = true;
            //    buttonZoom.IsEnabled = true;
            //    textBoxQuantity.IsEnabled = true;

            //}
            //else if (isChecked & isSaved)
            //{
            //    imageCheck.Visibility = Visibility.Collapsed;
            //    imageSave.Visibility = Visibility.Visible;

            //    buttonPicAdd.IsEnabled = false;
            //    buttonPicRemove.IsEnabled = false;
            //    buttonZoom.IsEnabled = false;
            //    textBoxQuantity.IsEnabled = false;
                
            //}
            //else
            //{
            //    imageCheck.Visibility = Visibility.Collapsed;
            //    imageSave.Visibility = Visibility.Collapsed;
            //}

        }
        private void setFadeElement()
        {
            if (picture != null)
            {
                if (picture.Quantity > 0)
                    borderFadeElement.Visibility = Visibility.Collapsed;
                else
                    borderFadeElement.Visibility = Visibility.Visible;
            }
        }
        private void setPicture()
        {
            if (picture == null)
            {
            }
            else
            {
            }
            this.DataContext = picture;
            updateUI();
        }
        private void togleQuantity()
        {
            if (!IsSaved)
            {
                if (picture != null)
                {
                    if (picture.Quantity == 0)
                    {
                        picture.Quantity = 1;
                        picture.IsSelected = true;
                    }
                    else
                    {
                        if (picture.Quantity == 1)
                        {
                            picture.Quantity = 0;
                            picture.IsSelected = false;
                        }
                        picture.IsSelected = true;
                    }
                    updateUI();
                    onQuantityChanged(new EventArgs());
                }
            }
        }
        private void updateUI()
        {
            setFadeElement();
            BindingHelper.UpdateIsCheckedBindingTarget(checkBoxIsSelected);
            BindingHelper.UpdateTextBindingTarget(textBoxQuantity);

        }


        #region UI EVENT HANDLERS
        private void buttonPicAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (picture != null)
                picture.Quantity++;
            setFadeElement();
            BindingHelper.UpdateTextBindingTarget(textBoxQuantity);
            onQuantityChanged(new EventArgs());
        }
        private void buttonPicRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (picture != null)
                if (picture.Quantity > 0)
                    picture.Quantity--;
            setFadeElement();
            BindingHelper.UpdateTextBindingTarget(textBoxQuantity);
            onQuantityChanged(new EventArgs());
        }
        private void buttonZoom_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (picture != null)
            {
                onZoom(new ZoomEventArgs() { Picture = picture });
            }
        }

        private void contentPresenterPicture_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }

        private void imageCheck_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
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
                        picture.IsSelected = true;
                        if (picture.Quantity == 0)
                            picture.Quantity = 1;
                    }
                    else
                    {
                        picture.IsSelected = true;
                    }
                }
                updateUI();
            }
        }
        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            togleQuantity();
        }
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            BindingHelper.UpdateTextBindingSource(sender as TextBox);
        }

        #endregion    
    }

    public class ZoomEventArgs : EventArgs
    {
        public Picture Picture { get; set; }
    }


}