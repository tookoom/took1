using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Numericon.Dcs.Silverlight.Control
{
    public class ControlHelper
    {
        public static object GetParent(FrameworkElement element, Type type)
        {
            object result = null;
            FrameworkElement parent = element;
            while (parent != null)
            {
                Type parentType = parent.GetType();
                if (parentType == type)
                {
                    result = parent;
                    break;
                }
                else
                    parent = parent.Parent as FrameworkElement;
            }
            return result;
        }
        public static void DisableChildren(UIElementCollection children)
        {
            if (children != null)
            {
                foreach (UIElement element in children)
                {
                    DisableElement(element);
                }
            }
        }
        public static void DisableElement(UIElement element)
        {
            setElementEnableState(element, false);
        }
        public static void EnableChildren(UIElementCollection children)
        {
            if (children != null)
            {
                foreach (UIElement element in children)
                {
                    EnableElement(element);
                }
            }
        }
        public static void EnableElement(UIElement element)
        {
            setElementEnableState(element, true);
        }

        private static void setElementEnableState(UIElement element, bool isEnabled)
        {
            System.Windows.Controls.Control control = element as System.Windows.Controls.Control;
            if (control != null)
                control.IsEnabled = isEnabled;

            TextBlock textBlock = element as TextBlock;
            if (textBlock != null)
                textBlock.Opacity = isEnabled ? 1 : 0.5;
        }

    }
}
