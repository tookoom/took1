
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
using System.Windows.Markup;
using DevExpress.AgMenu;
using DevExpress.AgMenu.Utils;


using Took1.Silverlight.LifeManager.App.Styles;
using System.Windows.Controls.Primitives;
using System.Windows.Browser;

using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Threading;

namespace Took1.Silverlight.LifeManager.App.Styles {
    public partial class AppleStyle : UserControl {
        Style currentStyle;
        System.Windows.Controls.Panel oldOptionsPanel;
        Dictionary<AgMenuItem, AppleStylePopupMenuAnimator> subMenuAnimators = new Dictionary<AgMenuItem, AppleStylePopupMenuAnimator>();

        public AppleStyle(Style style, System.Windows.Controls.Panel options)
            : this(style) {
            oldOptionsPanel = options;
        }
        public AppleStyle(Style style)
            : this() {
            currentStyle = style;
        }
        public AppleStyle() {
            InitializeComponent();
        }

        void LoadMenu() {
            //			CurrentStyle = Resources["AppleStyleMenuStyle"] as Style;
            LayoutRoot.Children.Remove(menu1);
            var stream = GetType().Assembly.GetManifestResourceStream("Took1.Silverlight.LifeManager.App.Styles.Menus.AppleMenu.xaml");
            var xaml = new System.IO.StreamReader(stream).ReadToEnd();
            menu1 = XamlReader.Load(xaml) as AgMenu;
            menu1.Style = Resources["AppleStyleMenuStyle"] as Style; ;
            LayoutRoot.Children.Add(menu1);
        }

        void UserControl_Loaded(object sender, RoutedEventArgs e) {
            ApplyOptions();
        }
        void ItemsPresenter_SizeChanged(object sender, SizeChangedEventArgs e) {
            ItemsPresenter ip = sender as ItemsPresenter;
            RectangleGeometry rg = new RectangleGeometry() { Rect = new Rect(0, 0, ip.DesiredSize.Width, Math.Max(0, ip.DesiredSize.Height - 10)) };
            ip.Clip = rg;
        }
        private void menu1_PopupAnimate(object sender, AgMenuPopupAnimateEventArgs e) {
            e.Handled = false;
            AppleStylePopupMenuAnimator subMenuAnimator = null;
            if (subMenuAnimators.ContainsKey((AgMenuItem)e.PopupOwnerItem)) {
                if (e.IsShowing)
                    //subMenuAnimators.Remove((AgMenuItem)e.PopupOwnerItem);
                    throw new Exception("Yx");
                else
                    subMenuAnimator = subMenuAnimators[(AgMenuItem)e.PopupOwnerItem];
            };
            if (subMenuAnimator == null) {
                subMenuAnimator = new AppleStylePopupMenuAnimator(((AgMenuItem)e.PopupOwnerItem).SubMenu);
                subMenuAnimators.Add((AgMenuItem)e.PopupOwnerItem, subMenuAnimator);
            }
            subMenuAnimator.Animate(e.IsShowing);
        }
        void menu1_PopupClosed(object sender, AgMenuPopupEventArgs e) {
            if (subMenuAnimators.ContainsKey((AgMenuItem)e.PopupOwnerItem)) {
                AppleStylePopupMenuAnimator subMenuAnimator = subMenuAnimators[(AgMenuItem)e.PopupOwnerItem];
                subMenuAnimator.StopAnimation();
                subMenuAnimators.Remove((AgMenuItem)e.PopupOwnerItem);
            }
        }
    }

    class AppleStylePopupMenuAnimator {
        struct ItemTransformData {
            public double Angle;
            public double X;
            public double Y;
        }

        const double AngleDelta = 2;
        const double AnimationDuration = 300;
        const double InitialAngle = -1;
        const double R = 1000;
        const double RX = -10;
        const double RY = -35;

        bool isShowing;
        ItemTransformData[] itemFinalTransformDatas;
        double[] itemPositions;
        AgPopupMenu menu;
        int startTickCount;
        DispatcherTimer timer;

        public AppleStylePopupMenuAnimator(AgPopupMenu menu) {
            this.menu = menu;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += OnTimerTick;
        }
        public void Animate(bool isShowing) {
            this.isShowing = isShowing;
            CalcItemPositions();
            CalcItemFinalTransformDatas();

            for (int i = 0; i < menu.Items.Count; i++) {
                menu[i].RenderTransform = GetItemTransform(i, 0, 0);
            }

            startTickCount = Environment.TickCount;
            timer.Start();
        }
        public void StopAnimation() {
            timer.Stop();
        }
        void CalcItemFinalTransformDatas() {
            itemFinalTransformDatas = new ItemTransformData[menu.Items.Count];
            double angle = InitialAngle;
            for (int i = 0; i < menu.Items.Count; i++) {
                AgMenuItemBase item = menu[i];
                itemFinalTransformDatas[i].X = RX + R * (1 - Math.Cos(angle * Math.PI / 180));
                itemFinalTransformDatas[i].Y = RY + (item.GetParent()).DesiredSize.Height - 1 - R * Math.Sin(angle * Math.PI / 180) - item.DesiredSize.Height / 2;
                itemFinalTransformDatas[i].Angle = angle;
                angle += AngleDelta;
            }
        }
        void CalcItemPositions() {
            itemPositions = new double[menu.Items.Count];
            for (int i = 1; i < itemPositions.Length; i++)
                itemPositions[i] = itemPositions[i - 1] + menu[i - 1].DesiredSize.Height;
        }
        Transform GetItemTransform(int itemIndex, int startTickCount, int tickCount) {
            TransformGroup result = new TransformGroup();
            AgMenuItemBase item = menu[itemIndex];
            result.Children.Add(new RotateTransform() {
                Angle = GetItemTransformParam(itemFinalTransformDatas[0].Angle, itemFinalTransformDatas[itemIndex].Angle, startTickCount, tickCount),
                CenterX = item.DesiredSize.Width,
                CenterY = item.DesiredSize.Height / 2
            });
            result.Children.Add(new TranslateTransform() {
                X = GetItemTransformParam(itemFinalTransformDatas[0].X, itemFinalTransformDatas[itemIndex].X, startTickCount, tickCount),
                Y = GetItemTransformParam(itemFinalTransformDatas[0].Y, itemFinalTransformDatas[itemIndex].Y, startTickCount, tickCount) - itemPositions[itemIndex]
            });
            return result;
        }
        double GetItemTransformParam(double initialValue, double finalValue, int startTickCount, int tickCount) {
            if (isShowing)
                return initialValue + (finalValue - initialValue) * (tickCount - startTickCount) / AnimationDuration;
            else
                return finalValue - (finalValue - initialValue) * (tickCount - startTickCount) / AnimationDuration;
        }
        void OnTimerTick(object sender, EventArgs e) {
            int tickCount = Environment.TickCount;
            if (tickCount - startTickCount > AnimationDuration) {
                timer.Stop();
                for (int i = 0; i < menu.Items.Count; i++) {
                    menu[i].RenderTransform = GetItemTransform(i, startTickCount, startTickCount + (int)AnimationDuration);
                }
                if (!isShowing)
                    menu.MenuPopup.IsOpen = false;
            } else {
                for (int i = 0; i < menu.Items.Count; i++) {
                    menu[i].RenderTransform = GetItemTransform(i, startTickCount, tickCount);
                }
            }
        }
    }

    public class AgApplePanel : StackPanel {
        DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
        List<Size> InitialSizes = new List<Size>();
        double InitialHeight = -1;
        double InitialWidth {
            get {
                double res = 0;
                foreach (Size s in InitialSizes) {
                    res += s.Width;
                }
                return res;
            }
        }
        void GetInitialSize() {
            GoToInitial();
            InitialSizes.Clear();
            foreach (UIElement child in Children) {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                InitialSizes.Add(child.DesiredSize);
            }
        }

        public AgApplePanel() {
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
            MouseMove += new MouseEventHandler(AgApplePanel_MouseMove);
            MouseLeave += new MouseEventHandler(AgApplePanel_MouseLeave);
            MouseEnter += new MouseEventHandler(AgApplePanel_MouseEnter);
        }

        void dt_Tick(object sender, EventArgs e) {
            if (Increase) {
                if (Diff < MaxDiff) {
                    Diff += 5;
                } else {
                    dt.Stop();
                }
            } else {
                if (Diff > 0) {
                    Diff -= 5;
                } else {
                    if (Height != InitialHeight && InitialHeight > 0)
                        Height = InitialHeight;
                    dt.Stop();
                }
            }
        }
        void AgApplePanel_MouseEnter(object sender, MouseEventArgs e) {
            GetInitialSize();
            InitialHeight = DesiredSize.Height;
            Height = DesiredSize.Height + MaxDiff;
            Increase = true;
        }
        void GoToInitial() {
            for (int i = 0; i < InitialSizes.Count(); i++) {
                FrameworkElement elem = Children[i] as FrameworkElement;
                elem.Width = InitialSizes[i].Width;
                elem.Height = InitialSizes[i].Height;
            }
        }
        void AgApplePanel_MouseLeave(object sender, MouseEventArgs e) {
            Increase = false;
        }
        double Position(UIElement elem) {
            elem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return elem.GetPosition(Application.Current.RootVisual).X + elem.DesiredSize.Width / 2;
        }
        double LastPosition = 0;
        void AgApplePanel_MouseMove(object sender, MouseEventArgs e) {
            LastPosition = e.GetPosition(null).X;
            doTransform(LastPosition);
        }
        bool increase = false;
        bool Increase {
            get { return increase; }
            set {
                if (value == Increase) return;
                increase = value;
                dt.Start();
            }
        }
        double diff = 0;
        double Diff {
            get { return diff; }
            set {
                if (diff == value) return;
                diff = value;
                doTransform(LastPosition);
            }
        }
        const double sigma = 3;
        public static double MagWidth = 250;
        public static double MaxDiff = 80;
        void doTransform(double mouse) {
            double totalScale = 0;
            List<double> scale = new List<double>();
            UIElement nearest = null;
            double far = double.PositiveInfinity;
            foreach (UIElement elem in Children) {
                //if(elem is AgMenuSeparator) continue;
                double d = Math.Abs(Position(elem) - mouse);
                double s = Math.Exp(-Math.Pow(d * sigma / MagWidth, 2));
                if (d < far) {
                    far = d;
                    nearest = elem;
                }
                scale.Add(s);
                totalScale += s;
            }
            double total = 0;
            int nearestIndex = Children.IndexOf(nearest);
            for (int i = 0; i < Children.Count(); i++) {
                FrameworkElement elem = Children[i] as FrameworkElement;
                //if(elem is AgMenuSeparator) continue;
                elem.Width = Math.Round(InitialSizes[i].Width + scale[i] / totalScale * Diff);
                elem.Height = Math.Round(InitialSizes[i].Height + scale[i] / totalScale * Diff);
                elem.Margin = new Thickness();
                total += elem.Width;
            }
            //if(total != (InitialWidth + Diff)) {
            ((FrameworkElement)nearest).Margin = new Thickness(0, 0, Math.Round((InitialWidth + Diff) - total), 0);
            //((FrameworkElement)nearest).Width += Math.Round((InitialWidth + Diff) - total);
            //((FrameworkElement)nearest).Height += Math.Ceiling((InitialWidth + Diff) - total);
            //((FrameworkElement)nearest).Height += Math.Round((InitialWidth + Diff) - total);
            //}
        }
    }

}
