using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Reflection;

namespace TK1.Settings
{
    public class SettingsLoader
    {
        public static object Load(string appName)
        {
            object result = null;
            if (!string.IsNullOrEmpty(appName))
            {
                Type type = getSettingsType(appName);
                if(type!= null)
                    result = SettingsFileLoader.LoadGeneric(appName, type);
            }
            return result;
        }
        private static Type getSettingsType(string appName)
        {
            Type result = null;
            if (!string.IsNullOrEmpty(appName))
            {
                string assemblyPath = AppFolder.GetAppSettingsAssemblyPath(appName);
                //string settingsFilePath = AppFolder.GetAppSettingsFilePath(appName);
                result = TypeLoader.Load(appName + "Settings", assemblyPath);
            }
            return result;
        }
    }
}
