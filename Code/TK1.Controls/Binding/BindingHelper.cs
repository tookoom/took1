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

namespace TK1.Controls.Binding
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
        public static void UpdateGridBindingSource(Grid grid)
        {
            if (grid != null)
            {
                UpdateBindingSource(grid as FrameworkElement, Grid.DataContextProperty);
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

        public static void UpdateIsCheckedBindingTarget(CheckBox checkBox)
        {
            if (checkBox != null)
            {
                UpdateBindingTarget(checkBox as FrameworkElement, CheckBox.IsCheckedProperty);
            }
        }
        public static void UpdateTextBindingTarget(TextBox textBox)
        {
            if (textBox != null)
            {
                UpdateBindingTarget(textBox as FrameworkElement, TextBox.TextProperty);
            }
        }
        public static void UpdateBindingTarget(FrameworkElement element, DependencyProperty property)
        {
            if (element != null & property != null)
            {
                BindingExpression expression = element.GetBindingExpression(property);
                if (expression != null)
                    expression.UpdateTarget();
            }
        }
    }
}
