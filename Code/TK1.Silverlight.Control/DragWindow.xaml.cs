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

namespace Took1.Silverlight.Controls
{
    public partial class DragWindow : UserControl
    {
        public delegate void CloseContentCallback();

        public DragWindow()
        {
            // Required to initialize variables
            InitializeComponent();
            Initialize();
        }
        public DragWindow(object content, string title, object tag)
        {
            // Required to initialize variables
			InitializeComponent();
            Initialize();

            SetContent(content);
            Title = title;
            Tag = tag;
        }

        private void Initialize()
        {
            IsCollapsed = false;
            IsVisible = false;
            IsDocked = false;

            rectangleBar.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleBar_MouseLeftButtonDown);
            rectangleBar.MouseLeftButtonUp += new MouseButtonEventHandler(rectangleBar_MouseLeftButtonUp);
            rectangleBar.MouseMove += new MouseEventHandler(rectangleBar_MouseMove);

            UnFocus();
        }

        #region DELEGATES
        public CloseContentCallback CloseContent;
        #endregion
        #region EVENTS
        public event EventHandler GotFocus;
        private void OnGotFocus(EventArgs e)
        {
            EventHandler handler = GotFocus;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        public event EventHandler LostFocus;
        private void OnClosing(EventArgs e)
        {
            EventHandler handler = LostFocus;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        public event RoutedEventHandler Closing;
        private void OnClosing(RoutedEventArgs e)
        {
            RoutedEventHandler handler = Closing;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        public event MouseButtonEventHandler StartDragging;
        public virtual void OnrectangleBarMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = StartDragging;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        public event MouseButtonEventHandler StopDragging;
        public virtual void OnrectangleBarMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = StopDragging;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        public event MouseEventHandler Move;
        public virtual void OnrectangleBarMouseMove(MouseEventArgs e)
        {
            MouseEventHandler handler = Move;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }
        }

        #endregion
        #region PRIVATE MEMBERS
        private bool isClosing = false;
        private double maxContentHeight;
        private double minHeight;
        private double maxHeight;

        #endregion
        #region PROPERTIES
        private Object content = null;
        private bool isCollapsed = false;
        private bool isDocked = false;
        private bool isVisible = false;
        private string title = "";

        public Object Content
        {
            get { return contentPresenterMain.Content; }
            set
            {
                content = value;
                SetContent(content);
            }
        }
        public bool IsCollapsed
        {
            get { return isCollapsed; }
            set
            {
                isCollapsed = value;
                SetStatus(isCollapsed);
            }
        }
        public bool IsDocked
        {
            get { return isDocked; }
            set
            {
                isDocked = value;
                SetDockingStatus();
            }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                SetVisibility(isVisible);
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                textBlockTitle.Text = title;
            }
        }
        public Rectangle DragBar
        {
            get { return rectangleBar; }
        }
        #endregion

        public void Focus()
        {
            gridFocus.Visibility = Visibility.Visible;
            OnGotFocus(new EventArgs());
        }
        public void UnFocus()
        {
            gridFocus.Visibility = Visibility.Collapsed;
            OnLostFocus(new RoutedEventArgs());
        }

        private void SetContent(object content)
        {
            if (content == null)
            {
                contentPresenterMain.Content = null;
            }
            else
            {
                FrameworkElement obj = (FrameworkElement)content;
                contentPresenterMain.Width = obj.Width;
                contentPresenterMain.Height = obj.Height;
                maxContentHeight = obj.Height;
                minHeight = 16 + rectangleBar.Height;
                maxHeight = minHeight + maxContentHeight;
                this.Width = contentPresenterMain.Width + 16;
                this.Height = maxHeight;
                contentPresenterMain.Content = content;
                SetStatus(isCollapsed);
            }
        }
        private void SetDockingStatus()
        {
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = canvasDock.Width/2;
            rotateTransform.CenterY = canvasDock.Height /2;
            canvasDock.RenderTransform = rotateTransform;

            Storyboard s = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            Duration dur = new Duration(new TimeSpan(0, 0, 0, 0, 100));
            da.Duration = dur;
            s.Duration = dur;
            s.Children.Add(da);

            if (IsDocked)
            {
                da.From = 90;
                da.To = 0;
                //rotateTransform.Angle = 0;
            }
            else
            {
                //rotateTransform.Angle = 90;
                da.From = 0;
                da.To = 90;
            }
            
            Storyboard.SetTarget(da, rotateTransform);
            Storyboard.SetTargetProperty(da, new PropertyPath("(rotateTransform.Angle)", new object[] { }));
            s.Begin();
        }
        private void SetStatus(bool collapsed)
        {
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            Duration duration = new Duration(new TimeSpan(0, 0, 0, 0, 100));
            doubleAnimation.Duration = duration;
            storyBoard.Duration = duration;
            storyBoard.Children.Add(doubleAnimation);

            if (collapsed)
            {
                doubleAnimation.From = maxHeight;
                doubleAnimation.To = minHeight;

                pathMax.Visibility = Visibility.Visible;
                pathMin.Visibility = Visibility.Collapsed;
            }
            else
            {
                doubleAnimation.From = minHeight;
                doubleAnimation.To = maxHeight + 2;

                pathMax.Visibility = Visibility.Collapsed;
                pathMin.Visibility = Visibility.Visible;
            }

            Storyboard.SetTarget(doubleAnimation, this);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(this.Height)", new object[] { }));
            storyBoard.Begin();
        }
        private void SetVisibility(bool Visible)
        {
            if (Visible)
                this.Visibility = Visibility.Visible;
            else this.Visibility = Visibility.Collapsed;
        }
        

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void RectVisible_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.3;
        }
        private void RectVisible_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.15;
        }
        private void RectVisible_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsCollapsed)
                IsCollapsed = false;
            else IsCollapsed = true;
        }

        private void RectExit_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.3;
        }
        private void RectExit_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.15;
        }
        private void RectExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isClosing = true;
            OnClosing(new RoutedEventArgs());
            //this.Visibility = Visibility.Collapsed;
        }

        private void rectDock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.3;
        }
        private void rectDock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle Rect = (Rectangle)sender;
            Rect.Opacity = 0.15;
        }
        private void rectDock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsDocked = !IsDocked;
        }

        void rectangleBar_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnrectangleBarMouseMove(e);
        }
        void rectangleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnrectangleBarMouseLeftButtonUp(e);
        }
        void rectangleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnrectangleBarMouseLeftButtonDown(e);
            //focus();
        }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!isClosing)
                Focus();
        }
    }
}
