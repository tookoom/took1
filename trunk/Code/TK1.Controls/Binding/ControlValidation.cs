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
using TK1.Controls.Resources;

namespace TK1.Controls.Binding
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
                    ControlValidationHelper.SetState(comboBox, "", ControlValidationStates.Valid);
                }
                else
                {
                    if (invalidateControl)
                    {
                        ControlValidationHelper.SetState(comboBox, ValidationStrings.NullSelection, ControlValidationStates.Invalid);
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
                    ControlValidationHelper.SetState(textBox, "", ControlValidationStates.Valid);
                }
                else
                {
                    if (invalidateControl)
                    {
                        ControlValidationHelper.SetState(textBox, ValidationStrings.NullEmptyText, ControlValidationStates.Invalid);
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
                ControlValidationHelper.SetState(textBox, "", ControlValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ControlValidationHelper.SetState(textBox, ValidationStrings.NullEmptyText, ControlValidationStates.Invalid);
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
                ControlValidationHelper.SetState(control, "", ControlValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ControlValidationHelper.SetState(control, ValidationStrings.NullValue, ControlValidationStates.Invalid);
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
                ControlValidationHelper.SetState(passwordBox1, "", ControlValidationStates.Valid);
                ControlValidationHelper.SetState(passwordBox2, "", ControlValidationStates.Valid);
            }
            else
            {
                if (invalidateControl)
                {
                    ControlValidationHelper.SetState(passwordBox1, ValidationStrings.PasswordMatch, ControlValidationStates.Invalid);
                    ControlValidationHelper.SetState(passwordBox2, ValidationStrings.PasswordMatch, ControlValidationStates.Invalid);
                }
            }
            return result;
        }

    }
}
