using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TK1.Basics.Controls;
using TK1.Utility;
using TK1.Dev.Data;
using TK1.Settings;
using TK1.Xml;

namespace TK1.Dev
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DevSettings settings;

        public MainWindow()
        {
            InitializeComponent();
            initializeUI();
            loadSettingsFile();
            //settings.Prop1 = "novo string";
            //saveSettings();
        }

        private void initializeUI()
        {
            modalDialog.SetParent(layoutRoot);
        }

        private void loadSettingsFile()
        {
            try
            {
                DevSettings ds = new DevSettings();
                string test = XmlSerializer<DevSettings>.Save(ds);
                DevSettings ds2 = XmlSerializer<DevSettings>.Load(test);
                //settings = SettingsFileLoader.Load<DevSettings>(Constraints.AppName);
                settings = SettingsFileLoader.LoadGeneric(Constraints.AppName, typeof(DevSettings)) as DevSettings;
                //object obj = SettingsLoader.Load(Constraints.AppName) as DevSettings;
                //settings = SettingsLoader.Load(Constraints.AppName) as DevSettings;
                if (settings == null)
                    saveSettings();
            }
            catch (Exception exception)
            {
                string caption = "loadConfigFile";
                string message = ErrorMessageBuilder.CreateMessage(exception);
                MessageBox.Show(message, caption);
            }
        }
        private void saveSettings()
        {
            try
            {
                if (settings == null)
                    settings = new DevSettings();
                //SettingsFileLoader.Save<DevSettings>(Constraints.AppName, settings);
                SettingsFileLoader.SaveGeneric(Constraints.AppName, typeof(DevSettings), settings);
            }
            catch (Exception exception)
            {
                string caption = "loadConfigFile";
                string message = ErrorMessageBuilder.CreateMessage(exception);
                MessageBox.Show(message, caption);
            }
        }

        #region UI EVENT HANDLERS
        private void buttonDev1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //SettingsFileLoader

            //var res = modalDialog.ShowDialog("teste");
            //var resultMessagePrefix = "Result: ";
            //if (res)
            //    MessageBox.Show(resultMessagePrefix + "Ok");
            //else
            //    MessageBox.Show(resultMessagePrefix + "Cancel");
        }

        private void buttonDev2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void buttonDev3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void buttonDev4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        #endregion
    }
    public class foo
    {
        public EnvironmentVariableTarget Target { get; set; }
        public string Name { get; set; }
    }

}
