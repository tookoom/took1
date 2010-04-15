using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Collections.Specialized;

namespace Took1.Silverlight.Controls
{
	/// <summary>
	/// Controle das janelas em NCWebAdmin. Contm menu animado para acesso �storyboard janelas,
	/// lan�ador r�pido, etc
	/// </summary>
    public partial class Launcher : UserControl
    {
        #region CONST
        private const double LARGE_ICON_SIZE = 1;
        private const double MEDIUM_ICON_SIZE = 0.8;
        private const double SMALL_ICON_SIZE = 0.75;
        private const double NORMAL_ICON_SIZE = 0.7;
        private const double SMALLER_ICON_SIZE = 0.65;

        public enum ClickEffect { None, Bounce };
        #endregion
        #region EVENTS
        public event MenuIndexChangedHandler MenuIndexChanged;
        public event MenuIndexChangedHandler MenuItemClicked;
        #endregion
        #region PROPERTIES
        public ObservableCollection<LauncherItem> Items { get; set; }
        public double MaxItemHeight { get; set; }
        public double MaxItemWidth { get; set; }
        public ClickEffect MenuItemClickEffect { get; set; }
        
        #endregion

        public Launcher()
        {
            InitializeComponent();

            Items = new ObservableCollection<LauncherItem>();
            Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);

            stackPanelLauncherItems.MouseLeave += new MouseEventHandler(stackPanelLauncherItems_MouseLeave);

            MaxItemHeight = 80;
            MaxItemWidth = 80;

        }

        private void adjustSizes(int index)
        {
            foreach (LauncherItem launcherItem in Items)
            {
                if (!launcherItem.IsFocused)
                {
                    if (index == -1)
                    {
                        applyResizeEffect(launcherItem.Content as Image, NORMAL_ICON_SIZE, this.MaxItemWidth, this.MaxItemHeight);
                        continue;
                    }

                    if (launcherItem.Index == index)
                        applyResizeEffect(launcherItem.Content as Image, LARGE_ICON_SIZE, this.MaxItemWidth, this.MaxItemHeight);
                    else if (launcherItem.Index == index - 1 || launcherItem.Index == index + 1)
                        applyResizeEffect(launcherItem.Content as Image, MEDIUM_ICON_SIZE, this.MaxItemWidth, this.MaxItemHeight);
                    else if (launcherItem.Index == index - 2 || launcherItem.Index == index + 2)
                        applyResizeEffect(launcherItem.Content as Image, SMALL_ICON_SIZE, this.MaxItemWidth, this.MaxItemHeight);
                    else
                        applyResizeEffect(launcherItem.Content as Image, SMALLER_ICON_SIZE, this.MaxItemWidth, this.MaxItemHeight);
                }
            }
        }
        private void applyResizeEffect(FrameworkElement frameworkElement, double factor, double width, double height)
        {
            if (frameworkElement != null)
            {
                TimeSpan speed = TimeSpan.FromMilliseconds(100);
                DoubleAnimation daWidth = new DoubleAnimation { To = factor * width, Duration = new Duration(speed) };
                DoubleAnimation daHeight = new DoubleAnimation { To = factor * height, Duration = new Duration(speed) };
                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget(daWidth, frameworkElement);
                Storyboard.SetTarget(daHeight, frameworkElement);
                Storyboard.SetTargetProperty(daHeight, new PropertyPath("(UIElement.Height)"));
                Storyboard.SetTargetProperty(daWidth, new PropertyPath("(UIElement.Width)"));
                storyboard.Children.Add(daWidth);
                storyboard.Children.Add(daHeight);
                storyboard.Begin();
            }
        }
        private void applyBounceEffect(FrameworkElement frameworkElement)
        {
            if (frameworkElement != null)
            {
                var doubleAnimation = new DoubleAnimationUsingKeyFrames();
                var k1 = new SplineDoubleKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100)),
                    Value = this.MaxItemHeight * 0.30
                };
                var k2 = new SplineDoubleKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)),
                    Value = 0
                };
                var k3 = new SplineDoubleKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
                    Value = this.MaxItemHeight * 0.10
                };
                var k4 = new SplineDoubleKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(350)),
                    Value = 0
                };
                doubleAnimation.KeyFrames.Add(k1);
                doubleAnimation.KeyFrames.Add(k2);
                doubleAnimation.KeyFrames.Add(k3);
                doubleAnimation.KeyFrames.Add(k4);

                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget(doubleAnimation, frameworkElement);
                Storyboard.SetTargetProperty(doubleAnimation,
                    new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Begin();
            }
        }
        private TransformGroup buildTransformGroup()
        {
            TransformGroup tg = new TransformGroup();
            TranslateTransform tt = new TranslateTransform();
            tt.Y = 0;
            tg.Children.Add(tt);
            return tg;
        }

        void stackPanelLauncherItems_MouseLeave(object sender, MouseEventArgs e)
        {
            adjustSizes(-1);
        }

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (LauncherItem launcherItem in e.NewItems)
                        {
                            stackPanelLauncherItems.Children.Add(launcherItem);
                            if (launcherItem.Content != null)
                            {
                                Image image = launcherItem.Content as Image;
                                if (image != null)
                                {
                                    image.Height = this.MaxItemHeight * NORMAL_ICON_SIZE;
                                    image.Width = this.MaxItemWidth * NORMAL_ICON_SIZE;
                                    image.RenderTransform = buildTransformGroup();
                                    image.VerticalAlignment = VerticalAlignment.Bottom;
                                }
                            }
                            launcherItem.Index = Items.Count;
                            launcherItem.MouseEnter += new MouseEventHandler(launcherItem_MouseEnter);
                            //launcherItem.MouseLeave += new MouseEventHandler(launcherItem_MouseLeave);
                            launcherItem.MouseLeftButtonDown += new MouseButtonEventHandler(launcherItem_MouseLeftButtonDown);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    stackPanelLauncherItems.Children.Clear();
                break;
            }
            


        }

        void launcherItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LauncherItem launcherItem = (LauncherItem)sender;

            if (this.MenuItemClickEffect == ClickEffect.Bounce)
                applyBounceEffect(launcherItem.Content as Image);

            if (MenuItemClicked != null)
            {
                int index = launcherItem.Index;
                SelectedMenuItemArgs menuArgs = new SelectedMenuItemArgs(launcherItem, index);
                MenuItemClicked(this, menuArgs);
            }

            launcherItem.IsFocused = true;
            foreach (LauncherItem item in Items)
            {
                if (item != launcherItem)
                    item.IsFocused = false;
            }

            adjustSizes(launcherItem.Index);
        }
        void launcherItem_MouseEnter(object sender, MouseEventArgs e)
        {
            LauncherItem launcherItem = (LauncherItem)sender;
            adjustSizes(launcherItem.Index);

            if (MenuIndexChanged != null)
            {
                SelectedMenuItemArgs args = new SelectedMenuItemArgs(launcherItem, launcherItem.Index);
                MenuIndexChanged(this, args);
            }
        }        
    }

    /// <summary>
    /// Delegate for the MenuIndexChanged and MenuItemClicked events.
    /// </summary>
    /// <param name="sender">Represents the object that fired the event.</param>
    /// <param name="e">Event data for the event.</param>
    public delegate void MenuIndexChangedHandler(object sender, SelectedMenuItemArgs e);

    /// <summary>
    /// Event data for the MenuIndexChanged and MenuItemClicked events.
    /// </summary>
    public class SelectedMenuItemArgs : EventArgs
    {
        public LauncherItem Item { get; set; }
        public int Index { get; set; }
        public SelectedMenuItemArgs(LauncherItem launcherItem, int index)
        {
            Index = index;
            Item = launcherItem;
        }
    }
}
