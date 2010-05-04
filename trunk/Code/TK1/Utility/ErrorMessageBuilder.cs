using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Utility
{
    public class ErrorMessageBuilder
    {
        public static string CreateMessage(Exception exception, string exceptionCaption, string innerExceptionCaption)
        {
            string result = string.Empty;
            if (exception != null)
            {
                result = string.Format("{0}:{1}{2}", 
                    exceptionCaption, 
                    Environment.NewLine, 
                    exception.Message ?? "[NULL MESSAGE]");
                
                Exception innerException = exception.InnerException;
                while (innerException != null)
                {
                    result += string.Format("{1}{0}:{1}{2}",
                        innerExceptionCaption,
                        Environment.NewLine,
                        innerException.Message ?? "[NULL MESSAGE]");

                    innerException = innerException.InnerException;
                }
            }
            return result;
        }
        public static string CreateMessage(Exception exception)
        {
            return CreateMessage(exception, "[EXCEPTION]", "[INNER EXCEPTION]");
        }
}
}
