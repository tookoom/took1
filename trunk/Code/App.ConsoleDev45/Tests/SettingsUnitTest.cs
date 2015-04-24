using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TK1.Dev.Data;
using TK1.Settings;
using TK1.Utility;
using System.Windows;
using TK1.Xml;

namespace TK1.Dev.UnitTest
{
    public class SettingsUnitTest
    {
        //DevSettings settings;

        //private void loadSettingsFile()
        //{
        //    try
        //    {
        //        DevSettings ds = new DevSettings();
        //        string test = XmlSerializer<DevSettings>.Save(ds);
        //        DevSettings ds2 = XmlSerializer<DevSettings>.Load(test);
        //        //settings = SettingsFileLoader.Load<DevSettings>(Constraints.AppName);
        //        settings = SettingsFileLoader.LoadGeneric(Constraints.AppName, typeof(DevSettings)) as DevSettings;
        //        //object obj = SettingsLoader.Load(Constraints.AppName) as DevSettings;
        //        //settings = SettingsLoader.Load(Constraints.AppName) as DevSettings;
        //        if (settings == null)
        //            saveSettings();
        //    }
        //    catch (Exception exception)
        //    {
        //        string caption = "loadConfigFile";
        //        string message = ErrorMessageBuilder.CreateMessage(exception);
        //        MessageBox.Show(message, caption);
        //    }
        //}
        //private void saveSettings()
        //{
        //    try
        //    {
        //        if (settings == null)
        //            settings = new DevSettings();
        //        //SettingsFileLoader.Save<DevSettings>(Constraints.AppName, settings);
        //        SettingsFileLoader.SaveGeneric(Constraints.AppName, typeof(DevSettings), settings);
        //    }
        //    catch (Exception exception)
        //    {
        //        string caption = "loadConfigFile";
        //        string message = ErrorMessageBuilder.CreateMessage(exception);
        //        MessageBox.Show(message, caption);
        //    }
        //}

    }
}
