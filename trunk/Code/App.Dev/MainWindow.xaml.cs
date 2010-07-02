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
using TK1.Diagnostics;
using System.Globalization;

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
            loadSettingsFile();
            //settings.Prop1 = "novo string";
            //saveSettings();
        }

        private void initializeUI()
        {
            windowController.Window = this;
            modalDialog.SetParent(layoutRoot);

            writeOutput1("output 1");
            writeOutput2("output 2");

            var culture = KeyboardInputLanguageManager.CultureOfCurrentLayout();

            writeOutput2(DateTime.Now.ToShortDateString());
            writeOutput2(DateTime.Now.ToShortTimeString());
            //writeOutput2(InputLanguageManager.Current.CurrentInputLanguage.EnglishName);
            SetLocale.SetWinCountryRegionName(InputLanguageManager.Current.CurrentInputLanguage);
            //writeOutput2(InputLanguageManager.Current.CurrentInputLanguage.EnglishName);
            writeOutput2(DateTime.Now.ToShortDateString());
            writeOutput2(DateTime.Now.ToShortTimeString());

            

            ////var test = SetLocale.GetLocaleInfo();

            //// Create a CultureInfo initialized to the neutral Arabic culture.
            //CultureInfo ci1 = new CultureInfo(0x1);
            //writeOutput1(string.Format("\nThe .NET Region name: {0}", SetLocale.GetNetCountryRegionName(ci1)));
            //writeOutput1(string.Format("The Win32 Region name: {0}", SetLocale.GetWinCountryRegionName(ci1)));

            //// Create a CultureInfo initialized to the specific 
            //// culture Arabic in Algeria.
            //CultureInfo ci2 = new CultureInfo(0x1401);
            //writeOutput1(string.Format("\nThe .NET Region name: {0}", SetLocale.GetNetCountryRegionName(ci2)));
            //writeOutput1(string.Format("The Win32 Region name: {0}", SetLocale.GetWinCountryRegionName(ci2)));
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

        private void writeOutput1(string text)
        {
            textBlockOutput1.Text += text;
            textBlockOutput1.Text += Environment.NewLine;
        }
        private void writeOutput2(string text)
        {
            textBlockOutput2.Text += text;
            textBlockOutput2.Text += Environment.NewLine;
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

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            initializeUI();
        }

        #endregion
    }
    public class foo
    {
        public EnvironmentVariableTarget Target { get; set; }
        public string Name { get; set; }
    }

}
