using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Resources;

namespace TK1.Utility
{
    public class ErrorMessageBuilder
    {
        public static string CreateMessage(string errorMessage)
        {
            return CreateMessage(errorMessage, string.Empty, CommonStrings.Error);
        }
        public static string CreateMessage(string errorMessage, string actionName)
        {
            return CreateMessage(errorMessage, actionName, CommonStrings.Error);
        }
        public static string CreateMessage(string errorMessage, string actionName, string errorMessageCaption)
        {
            string result = string.Empty;
            if (errorMessage == null)
            {
                result += CommonStrings.NullMessage;
            }
            else
            {
                if (string.IsNullOrEmpty(actionName))
                {
                    result = string.Format(CommonStrings.ErrorMessageUnknownAction,
                        Environment.NewLine,
                        errorMessageCaption,
                        errorMessage ?? CommonStrings.NullMessage);
                }
                else
                {
                    result = string.Format(CommonStrings.ErrorMessageKnownAction,
                        Environment.NewLine,
                        actionName,
                        errorMessageCaption,
                        errorMessage ?? CommonStrings.NullMessage);
                }
            }
            return result;
        }

        public static string CreateMessage(Exception exception)
        {
            return CreateMessage(exception, string.Empty, CommonStrings.Exception, CommonStrings.InnerException);
        }
        public static string CreateMessage(Exception exception, string actionName)
        {
            return CreateMessage(exception, actionName, CommonStrings.Exception, CommonStrings.InnerException);
        }
        public static string CreateMessage(Exception exception, string exceptionCaption, string innerExceptionCaption)
        {
            return CreateMessage(exception, string.Empty, exceptionCaption, innerExceptionCaption);
        }
        public static string CreateMessage(Exception exception, string actionName, string exceptionCaption, string innerExceptionCaption)
        {
            string result = string.Empty;
            if (exception == null)
            {
                result += CommonStrings.NullException;
            }
            else
            {
                if (string.IsNullOrEmpty(actionName))
                {
                    result = string.Format(CommonStrings.ExceptionMessageUnknownAction,
                        Environment.NewLine,
                        exceptionCaption,
                        exception.Message ?? CommonStrings.NullMessage);
                }
                else
                {
                    result = string.Format(CommonStrings.ExceptionMessageKnownAction,
                        Environment.NewLine,
                        actionName,
                        exceptionCaption,
                        exception.Message ?? CommonStrings.NullMessage);
                }

                Exception innerException = exception.InnerException;
                while (innerException != null)
                {
                    result += string.Format(CommonStrings.InnerExceptionMessage,
                        Environment.NewLine,
                        innerExceptionCaption,
                        innerException.Message ?? CommonStrings.NullMessage);

                    innerException = innerException.InnerException;
                }
            }
            return result;
        }
    }
}
