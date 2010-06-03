using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1
{
    public class AppFolder
    {
        public static string GetBaseAppFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\TK1\";
        }
        public static string GetAppFolder(string appName)
        {
            return AppFolder.GetBaseAppFolder() + appName + @"\";
        }
        public static string GetAppFolder(string appName, string child)
        {
            return AppFolder.GetBaseAppFolder() + appName + string.Format(@"\{0}\", child);
        }
        public static string GetAppSettingsFolder(string appName)
        {
            return AppFolder.GetBaseAppFolder() + appName + @"\Settings\";
        }
        public static string GetAppSettingsAssemblyPath(string appName)
        {
            return AppFolder.GetAppSettingsFolder(appName) + @"Settings.assembly";
        }
        public static string GetAppSettingsFilePath(string appName)
        {
            return AppFolder.GetAppSettingsFolder(appName) + @"Settings.tk1cfg";
        }

    }
}
