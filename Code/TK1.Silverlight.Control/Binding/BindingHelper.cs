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
using System.Windows.Data;

namespace Numericon.Web.Silverlight.Control.Binding
{
    public class BindingHelper
    {
        public static void UpdateComboBoxBindingSource(ComboBox comboBox)
        {
            if (comboBox != null)
            {
                UpdateBindingSource(comboBox as FrameworkElement, ComboBox.SelectedItemProperty);
            }
        }
        public static void UpdateTextBindingSource(TextBox textBox)
        {
            if (textBox != null)
            {
                UpdateBindingSource(textBox as FrameworkElement, TextBox.TextProperty);
            }
        }
        public static void UpdateBindingSource(FrameworkElement element, DependencyProperty property)
        {
            if (element != null & property != null)
            {
                BindingExpression expression = element.GetBindingExpression(property);
                if (expression != null)
                    expression.UpdateSource();
            }
        }
    }
}
