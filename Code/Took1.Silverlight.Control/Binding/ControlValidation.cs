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

namespace Numericon.Web.Silverlight.Control.Binding
{
    public class ControlValidation
    {
        public static bool CheckComboBoxNullSelection(ComboBox comboBox, bool invalidateControl)
        {
            bool result = false;
            if (comboBox != null)
            {
                if(comboBox.SelectedItem!= null)
                {
                    result = true;
                    ValidationHelper.SetState(comboBox, "", ValidationStates.Valid);
                }
                else
                {
                    if (invalidateControl)
                    {
                        ValidationHelper.SetState(comboBox, "Value can't be null or empty", ValidationStates.Invalid);
                    }
                }
            }
            return result;
        }
        public static bool CheckNullEmptyTextBox(TextBox textBox, bool invalidateControl)
        {
            bool result = false;
            if (textBox != null)
            {
                if (textBox.Text != string.Empty & textBox.Text != null)
                {
                    result = true;
                    ValidationHelper.SetState(textBox, "", ValidationStates.Valid);
                }
                else
                {
                    if (invalidateControl)
                    {
                        ValidationHelper.SetState(textBox, "Value can't be null or empty", ValidationStates.Invalid);
                    }
                }
            }
            return result;
        }
        public static bool CheckNullEmptyTextBox(string text, System.Windows.Controls.Control textBox, bool invalidateControl)
        {
            bool result = false;
            if (text != string.Empty & text != null)
            {
                result = true;
                ValidationHelper.SetState(textBox, "", ValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ValidationHelper.SetState(textBox, "Value can't be null or empty", ValidationStates.Invalid);
                }
            }
            return result;
        }
        public static bool CheckNullValue(object obj, System.Windows.Controls.Control control, bool invalidateControl)
        {
            bool result = false;
            if (obj != null)
            {
                result = true;
                ValidationHelper.SetState(control, "", ValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ValidationHelper.SetState(control, "Value can't be null or empty", ValidationStates.Invalid);
                }
            }
            return result;
        }
        public static bool CheckPasswordMatch(string password1, string password2, System.Windows.Controls.Control passwordBox1, System.Windows.Controls.Control passwordBox2, bool invalidateControl)
        {
            bool result = false;
            if (password2 == password1)
            {
                result = true;
                ValidationHelper.SetState(passwordBox1, "", ValidationStates.Valid);
                ValidationHelper.SetState(passwordBox2, "", ValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ValidationHelper.SetState(passwordBox1, "Password must match", ValidationStates.Invalid);
                    ValidationHelper.SetState(passwordBox2, "Password must match", ValidationStates.Invalid);
                }
            }
            return result;
        }

    }
}
