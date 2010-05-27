using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TK1.Xml;

namespace TK1.Configuration
{
    public class SettingsFileLoader<T> where T : class
    {
        public static T Load(string appName)
        {
            T result = null;
            string path = AppFolder.GetAppSettingsFilePath(appName);
            if (path != null & path != string.Empty)
            {
                if (File.Exists(path))
                {
                    string fileContent = File.ReadAllText(path);
                    result = XmlSerializer<T>.Load(fileContent);
                }
            }
            return result;
        }
        public static void Save(string appName, T content)
        {
            if (content != null)
            {
                string path = AppFolder.GetAppSettingsFilePath(appName);
                if (path != null & path != string.Empty)
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
    }
}
