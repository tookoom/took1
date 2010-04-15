using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1
{
    public class AppFolder
    {
        public static string GetAppFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Took1\";
        }
        public static string GetAppConfigFolder(string appName)
        {
            return AppFolder.GetAppFolder() + appName + @"\Config\";
        }
        public static string GetAppConfigFilePath(string appName)
        {
            return AppFolder.GetAppConfigFolder(appName) + @"Settings.took1cfg";
        }

    }
}
