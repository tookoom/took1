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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace TK1.Wpf.Controls.Binding
{
    public class ControlValidationErrorBuilder : System.Windows.Controls.Control
    {
        public static ReadOnlyObservableCollection<ValidationError> GetValidationErrors(String errorMessage)
        {
            ControlValidationErrorBuilder eb = new ControlValidationErrorBuilder();
            return GetErrors(eb, errorMessage);
        }

        private static readonly DependencyProperty ErrorProviderProperty =
            DependencyProperty.Register("ErrorProvider", typeof(ErrorProvider), typeof(ControlValidationErrorBuilder), new PropertyMetadata(null));

        private static ReadOnlyObservableCollection<ValidationError> GetErrors(System.Windows.Controls.Control control, string errorMessage)
        {
            var validationHelper = new ErrorProvider(errorMessage);

            control.SetBinding(ErrorProviderProperty, new System.Windows.Data.Binding("ValidationError")
            {
                Mode = BindingMode.TwoWay,
                NotifyOnValidationError = false,
                ValidatesOnExceptions = true,
                UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
                Source = validationHelper
            });

            control.GetBindingExpression(ErrorProviderProperty).UpdateSource();

            return (ReadOnlyObservableCollection<ValidationError>)control.GetValue(Validation.ErrorsProperty);
        }

        #region Nested type: ErrorProvider

        public class ErrorProvider
        {
            private readonly string _message;

            public ErrorProvider(string message)
            {
                _message = message;
                ThrowValidationError = true;
            }

            public bool ThrowValidationError { get; set; }

            [DebuggerNonUserCode]
            public object ValidationError
            {
                get { return null; }
                set
                {
                    if (ThrowValidationError)
                    {
                        throw new Exception(_message);
                    }
                }
            }
        }

        #endregion
    }
}
