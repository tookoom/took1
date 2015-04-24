using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TK1.Utility
{
    public class AppOutput
    {
        #region PRIVATE MEMBERS
        private static string logFileDirectory;
        private static string logFileName;
        private static string logFilePath;
        private static object lockObject;
        #endregion        
        #region PUBLIC PROPERTIES
        public static string LogFileDirectory
        {
            get
            {
                if (logFileDirectory == null)
                {
                    logFileDirectory = Environment.CurrentDirectory ;
                }

                return logFileDirectory;
            }
            set {
                if (Directory.Exists(value))
                {
                    logFileDirectory = value;
                }
            }
        }
        public static string LogFileName
        {
            get
            {
                if (logFileName == null)
                {
                    logFileName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                }

                return logFileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    logFileName = value;
                }
            }
        }
        public static string LogFilePath
        {
            get
            {
                if (logFilePath == null)
                {
                    logFilePath = Path.Combine(LogFileDirectory ?? string.Empty, getFileLogName());
                }
                return logFilePath;
            }
        }
        public static DateTime StartTimestamp { get; set; }
        public static bool WriteConsole { get; set; }
        public static bool WriteFile { get; set; }

        #endregion

        public static void Write(Exception exception)
        {
            var builder = new StringBuilder();
            var count = 0;
            var innerException = exception.InnerException;

            builder.AppendLine("### EXCEPTION");
            builder.AppendLine(exception.Message);

            while (innerException != null)
            {
                count++;
                builder.AppendLine("### INNER EXCEPTION " + count.ToString());
                builder.AppendLine(innerException.Message);
                innerException = innerException.InnerException;
            }

            var message = builder.ToString();
            if (WriteConsole)
            {
                Console.WriteLine(message);
            }
            if (WriteFile)
            {
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }

        }
        public static void Write(string message)
        {
            Write(message, false);

        }
        public static void Write(string message, bool includeTimestamp)
        {
            if (includeTimestamp)
                message = DateTime.Now.ToString() + " " + message;
            if (WriteConsole)
            {
                Console.WriteLine(message);
            }
            if (WriteFile)
            {
                File.AppendAllText(LogFilePath, message + Environment.NewLine);
            }

        }
        public static void WriteExecutionEnd()
        {
            Write(string.Format("END: {0}", DateTime.Now));
            Write(string.Format("DURATION: {0}", DateTime.Now - StartTimestamp));
        }
        public static void WriteExecutionStart()
        {
            StartTimestamp = DateTime.Now;
            Write(string.Format("START: {0}", StartTimestamp));
        }
        public static void WriteToFile(Exception exception)
        {
            if (lockObject == null)
                lockObject = string.Empty;
            lock (lockObject)
            {
                var prevState = WriteConsole;
                WriteConsole = false;
                Write(exception);
                WriteConsole = prevState;
            }
        }
        public static void WriteToFile(string message)
        {
            WriteToFile(message, false);

        }
        public static void WriteToFile(string message, bool includeTimestamp)
        {
            if (lockObject == null)
                lockObject = string.Empty;
            lock (lockObject)
            {
                var prevState = WriteConsole;
                WriteConsole = false;
                Write(message, includeTimestamp);
                WriteConsole = prevState;
            }
        }

        private static string getFileLogName()
        {
            return string.Format("{0}_{1}.log", LogFileName, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }
    
    }
}
