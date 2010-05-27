using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1
{
    public class AppFolder
    {
        public static string GetAppFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\TK1\";
        }
        public static string GetAppSettingsFolder(string appName)
        {
            return AppFolder.GetAppFolder() + appName + @"\Settings\";
        }
        public static string GetAppSettingsFilePath(string appName)
        {
            return AppFolder.GetAppSettingsFolder(appName) + @"Settings.tk1cfg";
        }

    }
}
