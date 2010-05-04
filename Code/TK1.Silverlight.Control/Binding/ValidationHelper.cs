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
using System.Collections.ObjectModel;

namespace Numericon.Web.Silverlight.Control.Binding
{
    public static class ValidationHelper
    {
        public static bool CheckValidity(System.Windows.Controls.Control control)
        {
            bool result = false;
            if (control != null)
            {
                result = GetState(control) == ValidationStates.Valid;
            }
            return result;
        }
        public static bool CheckValidity(UIElementCollection children)
        {
            bool result = getChildrenInvalidControlCount(children) == 0;
            return result;
        }
        public static ValidationStates GetState(System.Windows.Controls.Control control)
        {
            ValidationStates result = getState(control as UIElement);
            return result;
        }
        public static void SetState(System.Windows.Controls.Control control, string message, ValidationStates validationState)
        {
            if (control != null)
            {
                if (validationState == ValidationStates.Valid)
                    clearErrorState(control);
                else
                    setErrorState(control, message);
            }
        }

        private static void clearErrorState(this System.Windows.Controls.Control control)
        {
            control.GotFocus -= control_GotFocus;
            control.LostFocus -= control_LostFocus;
            control.SetValue(Validation.ErrorsProperty, null);
            control.SetValue(Validation.HasErrorProperty, false);
            VisualStateManager.GoToState(control, "Valid", true);
        }
        private static int getChildrenInvalidControlCount(UIElementCollection children)
        {
            int count = 0;
            if (children != null)
            {
                foreach (var child in children)
                {
                    if (getState(child) == ValidationStates.Invalid)
                        count++;
                }
            }
            return count;
        }
        private static ValidationStates getState(UIElement uiElement)
        {
            ValidationStates result = ValidationStates.Valid;
            if (uiElement != null)
            {
                ReadOnlyObservableCollection<System.Windows.Controls.ValidationError> errors = uiElement.GetValue(Validation.ErrorsProperty) as ReadOnlyObservableCollection<System.Windows.Controls.ValidationError>;
                if (errors != null)
                    if (errors.Count > 0)
                        result = ValidationStates.Invalid;
            }
            return result;
        }
        private static void setErrorState(this System.Windows.Controls.Control control, string message)
        {
            control.SetValue(Validation.ErrorsProperty, ValidationErrorBuilder.GetValidationErrors(message));
            control.SetValue(Validation.HasErrorProperty, true);
            VisualStateManager.GoToState(control, "InvalidUnfocused", true);
            control.GotFocus += control_GotFocus;
            control.LostFocus += control_LostFocus;
        }

        #region EVENT HANDLERS
        private static void control_GotFocus(object sender, RoutedEventArgs e)
        {
            var control = sender as System.Windows.Controls.Control;
            if (control != null && (Boolean)control.GetValue(Validation.HasErrorProperty))
            {
                VisualStateManager.GoToState(control, "InvalidFocused", true);
            }
        }
        private static void control_LostFocus(object sender, RoutedEventArgs e)
        {
            var control = sender as System.Windows.Controls.Control;
            if (control != null && (Boolean)control.GetValue(Validation.HasErrorProperty))
            {
                VisualStateManager.GoToState(control, "InvalidUnfocused", true);
            }
        }

        #endregion
    }


}
