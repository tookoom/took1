using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TK1.Xml;
using System.Reflection;

namespace TK1.Settings
{
    public class SettingsFileLoader
    {
        public static T Load<T>(string appName) where T : class
        {
            T result = null;
            string path = AppFolder.GetAppSettingsFilePath(appName);
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    string fileContent = File.ReadAllText(path);
                    result = XmlSerializer<T>.Load(fileContent);
                }
            }
            return result;
        }
        public static void Save<T>(string appName, T content) where T : class
        {
            if (content != null)
            {
                string path = AppFolder.GetAppSettingsFilePath(appName);
                if (!string.IsNullOrEmpty(path))
                {
                    string fileContent = XmlSerializer<T>.Save(content);
                    if (fileContent != null)
                    {
                        string folderPath = AppFolder.GetAppSettingsFolder(appName);
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                        File.WriteAllText(path, fileContent, Encoding.Default);
                    }
                }
            }
        }

        public static object LoadGeneric(string appName, Type type)
        {
            object result = null;
            if (type != null)
            {
                MethodInfo method = typeof(SettingsFileLoader).GetMethod("Load");
                if (method != null)
                {
                    MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { type });
                    result = genericMethod.Invoke(null, new object[] { appName });

                }
            }
            return result;
        }
        public static object SaveGeneric(string appName, Type type, object content)
        {
            object result = null;
            if (type != null)
            {
                MethodInfo method = typeof(SettingsFileLoader).GetMethod("Save");
                if (method != null)
                {
                    MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { type });
                    result = genericMethod.Invoke(null, new object[] { appName, content });

                }
            }
            return result;
        }
    }
}
