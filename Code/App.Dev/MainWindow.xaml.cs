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
//using TK1.Bizz.Pieta.Data;
using TK1.Bizz.Pieta;
using TK1.Html;
using TK1.Collection;
using TK1.Bizz.Pieta.Const;
using TK1.Media.Imaging;
using TK1.Dev.UnitTest;

namespace TK1.Dev
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            //PietaUnitTest.TestXmlLoad();
            //TK1ServicesUnitTest.Test();
            //MdoUnitTest.LoadXmlFile();
            //InetsoftUnitTest.LoadXmlFile();
            //MdoUnitTest.SendTestMail();
            //TK1DataUnitTest.UserLogin();
            //BizzUnitTest.UserTest();
            //BizzUnitTest.MailTest();
            //MySqlUnitTest.Test();
            //BizzClientUnitTest.AppLog();
            IntegraUnitTest.BrokerIntegrationTest();
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
            try
            {
                //AM_DWEntities entities = new AM_DWEntities();
                //int count = 0;
                //DateTime from = new DateTime(1970, 1, 1);
                //DateTime to = new DateTime(2070, 12, 31);
                //DateTime date = from;
                //while (date <= to)
                //{
                //    count++;
                //    var exists = (from el in entities.Times
                //                  where el.TimeID == date
                //                  select el).FirstOrDefault() != null;
                //    if (!exists)
                //    {
                //        entities.Times.AddObject(new Time() { Day = date.Day, Month = date.Month, Year = date.Year, Quarter = ((date.Month - 1) / 3) + 1, TimeID = date });
                //        entities.SaveChanges();
                //    }
                //    date = date.AddDays(1);
                //}
            }
            catch (Exception exception) { }

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
}
