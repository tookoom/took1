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
using System.Collections.Specialized;
using System.Windows.Shapes;

namespace Took1.Silverlight.Controls
{
    public enum WindowLayoutType
    {
        Cascade,
        Horizontal,
        Vertical
    };

    public partial class DragWindowHost : UserControl
    {
        #region EVENTS
        public event WriteOutputEventHandler WriteOutput;
        public virtual void OnWriteOutput(WriteOutputEventArgs e)
        {
            WriteOutputEventHandler handler = WriteOutput;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }

        } 
        #endregion
        #region PRIVATE MEMBERS
        private bool isMouseDragging = false;
        Point dragStartingPoint;
        private int highestElement = 0;
        private DragWindowCollection dragWindowCollection = new DragWindowCollection();


        #endregion
        #region PUBLIC PROPERTIES

        public DragWindowCollection DragWindows
        {
            get { return dragWindowCollection; }
            set { dragWindowCollection = value; }
        }

        #endregion

        public DragWindowHost()
        {
            InitializeComponent();
            dragWindowCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(dragWindowCollection_CollectionChanged);
        }

        public List<DragWindow> GetDragWindow(string windowName)
        {
            var query = from el in dragWindowCollection
                        where el.Tag == windowName
                        select el;
            return query.ToList();
        }
        public DragWindow GetDragWindow(object content)
        {
            return (from el in dragWindowCollection
                    where el.Content == content
                    select el).ToList().FirstOrDefault();
        }
        public void OrganizeWindows(WindowLayoutType windowLayoutType)
        {
            const int CASCADE_OFFSET = 40;
            const int HORIZONTAL_OFFSET = 10;
            const int VERTICAL_OFFSET = 10;

            int windowCount = 0;
            double totalHeight = 0, totalWidth = 0, highestWindow = 0, widestWindow = 0;
            double left, top;
            foreach (DragWindow dragWindow in canvas.Children)
            {
                windowCount++;
                totalHeight += dragWindow.Height;
                totalWidth += dragWindow.Width;
                if (dragWindow.Height > highestWindow) highestWindow = dragWindow.Height;
                if (dragWindow.Width > widestWindow) widestWindow = dragWindow.Width;
            }

            switch (windowLayoutType)
            {
                case WindowLayoutType.Cascade:
                    totalHeight = totalHeight / windowCount;
                    totalWidth = totalWidth / windowCount;
                    totalHeight += (windowCount - 1) * CASCADE_OFFSET;
                    totalWidth += (windowCount - 1) * CASCADE_OFFSET;
                    left = -totalWidth / 2;
                    top = -totalHeight / 2;
                    foreach (DragWindow dragWindow in canvas.Children)
                    {
                        Canvas.SetLeft(dragWindow, left);
                        Canvas.SetTop(dragWindow, top);
                        left += CASCADE_OFFSET;
                        top += CASCADE_OFFSET;
                        moveToTop(dragWindow);
                    }

                    break;
                case WindowLayoutType.Vertical:
                    totalHeight += (windowCount - 1) * VERTICAL_OFFSET;
                    totalWidth += (windowCount - 1) * HORIZONTAL_OFFSET;
                    left = -totalWidth / 2;
                    top = -highestWindow / 2;
                    foreach (DragWindow dragWindow in canvas.Children)
                    {
                        Canvas.SetLeft(dragWindow, left);
                        Canvas.SetTop(dragWindow, top);
                        left += dragWindow.Width + HORIZONTAL_OFFSET;
                        moveToTop(dragWindow);
                    }
                    break;
                default:
                    break;
            }
        }


        private void addDragWindow(DragWindow dragWindow)
        {
            dragWindow.IsVisible = true;

            dragWindow.StartDragging += new MouseButtonEventHandler(dragWindow_StartDragging);
            dragWindow.StopDragging += new MouseButtonEventHandler(dragWindow_StopDragging);
            dragWindow.Move += new MouseEventHandler(dragWindow_Move);
            dragWindow.Loaded += new RoutedEventHandler(dragWindow_Loaded);
            dragWindow.Closing += new RoutedEventHandler(dragWindow_Closing);
            dragWindow.GotFocus += new EventHandler(dragWindow_GotFocus);
            dragWindow.LostFocus += new EventHandler(dragWindow_LostFocus);

            dragWindow.Focus();

            canvas.Children.Add(dragWindow);
            centerWindow(dragWindow);
            moveToTop(dragWindow);
        }
        private void centerWindow(DragWindow dragWindow)
        {
            setCanvasSize();
            Point point = new Point();
            point.X = canvas.ActualWidth - dragWindow.Width / 2;
            point.Y = canvas.ActualHeight - dragWindow.Height / 2;
            Canvas.SetLeft(dragWindow, point.X);
            Canvas.SetTop(dragWindow, point.Y);
            writeOutput("centered");
        }
        private void focusHighestWindow()
        {
            DragWindow highestWindow = null;
            int highestLevel = 0;
            foreach (DragWindow dragWindow in canvas.Children)
            {
                if (Canvas.GetZIndex(dragWindow) > highestLevel)
                {
                    highestLevel = Canvas.GetZIndex(dragWindow);
                    highestWindow = dragWindow;
                }
            }
            if (highestWindow != null)
                highestWindow.Focus();
        }
        private void moveToTop(FrameworkElement element)
        {
            highestElement++;
            Canvas.SetZIndex(element, highestElement);
        }
        private void removeDragWindow(DragWindow dragWindow)
        {
            if(dragWindow.CloseContent != null)
                dragWindow.CloseContent();
            dragWindow.Content = null;
            canvas.Children.Remove(dragWindow);
            focusHighestWindow();
        }
        private void setCanvasSize()
        {
            /*try
            {
                canvas.Height = LayoutRoot.ActualHeight - gridBottom.ActualHeight - gridLauncher.ActualHeight;
                canvas.Width = LayoutRoot.ActualWidth - gridBottom.ActualWidth - gridLauncher.ActualWidth;
                
            }
            catch
            {
                canvas.Height = 1;
                canvas.Width = 1;
            }*/
        }
        private void updateElementPosition(FrameworkElement element, Point point)
        {
            Canvas.SetLeft(element, point.X - dragStartingPoint.X);
            Canvas.SetTop(element, point.Y - dragStartingPoint.Y);

        }
        private void writeOutput(string message)
        {
            OnWriteOutput(new WriteOutputEventArgs { Message = message });
        }

        #region EVENT HANDLERS
        void dragWindowCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DragWindow dragWindow in e.NewItems)
                        addDragWindow(dragWindow);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (DragWindow dragWindow in e.OldItems)
                        removeDragWindow(dragWindow);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (DragWindow dragWindow in dragWindowCollection)
                        removeDragWindow(dragWindow);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
            OrganizeWindows(WindowLayoutType.Horizontal);
        }

        void dragWindow_Move(object sender, MouseEventArgs e)
        {
            if (isMouseDragging)
            {
                DragWindow dragWindow = (DragWindow)sender;
                if (!dragWindow.IsDocked)
                {
                    Point point = e.GetPosition(this.canvas);
                    updateElementPosition(dragWindow, point);
                }
            }
        }
        void dragWindow_StopDragging(object sender, MouseButtonEventArgs e)
        {
            DragWindow dragWindow = (DragWindow)sender;
            if (!dragWindow.IsDocked)
            {
                dragWindow.DragBar.ReleaseMouseCapture();
                dragWindow.Opacity = 1;
                isMouseDragging = false;
            }
        }
        void dragWindow_StartDragging(object sender, MouseButtonEventArgs e)
        {
            DragWindow dragWindow = (DragWindow)sender;
            if (!dragWindow.IsDocked)
            {
                dragWindow.DragBar.CaptureMouse();
                dragStartingPoint = e.GetPosition(dragWindow);
                isMouseDragging = true;
                dragWindow.Opacity = 0.7;
            }
        }
        void dragWindow_Closing(object sender, RoutedEventArgs e)
        {
            removeDragWindow((DragWindow)sender);
        }
        void dragWindow_Loaded(object sender, RoutedEventArgs e)
        {
            writeOutput("loaded");
            centerWindow((DragWindow)sender);
        }
        void dragWindow_LostFocus(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
        void dragWindow_GotFocus(object sender, EventArgs e)
        {
            DragWindow focusedWindow = (DragWindow)sender;
            moveToTop(focusedWindow);
            foreach (DragWindow dragWindow in canvas.Children)
            {
                if (focusedWindow != dragWindow)
                    dragWindow.UnFocus();

            }
        }
        #endregion

        

    }
}
