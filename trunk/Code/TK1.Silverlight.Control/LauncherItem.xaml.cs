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
using System.Windows.Media.Imaging;


namespace Took1.Silverlight.Controls
{
	/// <summary>
	/// Controle usado no menu que contm �cone e t�tulo
	/// </summary>
    public partial class LauncherItem : UserControl
    {
        #region PRIVATE MEMBERS
        private bool isFocused;
        private object content;

        #endregion
        #region PUBLIC PROPERTIES
        public string Caption
        {
            get { return textBlockCaption.Text; }
            set { textBlockCaption.Text = value; }
        }
        public Object Content
        {
            get { return content; }
            set
            {
                content = value;
                contentPresenterIcon.Content = content;
            }
        }
        //public Image Icon
        //{
        //    get { return content as Image; }
        //    set
        //    {
        //        content = value;
        //        contentPresenterIcon.Content = content;
        //    }
        //}


        public Image Icon
        {
            get { return (Image)GetValue(IconProperty); }
            set
            {
                SetValue(IconProperty, value);
                content = value;
                contentPresenterIcon.Content = content;
            }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Image), typeof(LauncherItem), new PropertyMetadata(null));



        public bool IsFocused
        {
            get { return isFocused; }
            set
            {
                isFocused = value;
                if (isFocused)
                {
                    textBlockCaption.Visibility = Visibility.Visible;
                    focus();
                }
                else
                {
                    textBlockCaption.Visibility = Visibility.Collapsed;
                    unfocus();
                }
            }
        }
        public string Name { get; set; }
        public int Index { get; set; } 
        #endregion
        #region STATIC MEMBERS
        public static LauncherItem CreateRelative(string name, string caption, string iconRelativeUri, string tag)
        {
            Image image = ImageLoader.GetRelative(iconRelativeUri);
            image.Stretch = Stretch.Uniform;
            image.UpdateLayout();
            return new LauncherItem { Name = name, Caption = caption, Content = image, Tag = tag };
        }
		public static LauncherItem CreateAbsolute(string name, string caption, string iconUri, string tag)
        {
            Image image = ImageLoader.Get(iconUri);
            image.Stretch = Stretch.Uniform;
            return new LauncherItem { Name = name, Caption = caption, Content = image, Tag = tag };
        }

	    #endregion        

        public LauncherItem() 
        {
            InitializeComponent();

            textBlockCaption.Visibility = Visibility.Collapsed;
            rectFocus.Visibility = Visibility.Collapsed;
            rectOver.Visibility = Visibility.Collapsed;
            rectDown.Visibility = Visibility.Collapsed;
        }

        private void focus()
        {
            rectFocus.Visibility = Visibility.Visible;
        }
        private void unfocus()
        {
            rectFocus.Visibility = Visibility.Collapsed;
        }

        private void rectangle_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            textBlockCaption.Visibility = Visibility.Visible;
            rectOver.Visibility = Visibility.Visible;
        }
        private void rectangle_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isFocused)
            {
                textBlockCaption.Visibility = Visibility.Collapsed;
                
            }
            rectOver.Visibility = Visibility.Collapsed;
        }
        private void rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rectDown.Visibility = Visibility.Visible;
        }
        private void rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rectDown.Visibility = Visibility.Collapsed;
        }

    }
}
