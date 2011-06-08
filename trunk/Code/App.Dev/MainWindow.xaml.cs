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
using TK1.Bizz.Pieta.Xml;
using TK1.Bizz.Pieta.Data;
using TK1.Html;
using TK1.Collection;
using TK1.Bizz.Pieta.Const;
using TK1.Media.Imaging;

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


            try
            {
                //SitePicHelper.ResizeSitePics(@"D:\Projetos\TK1\Code\Web.Pieta\Imovel\Fotos\Venda", 500, 100);
                //ImageHelperTestUnit.Resize();
                //var xml = XmlHelper.NormatizeFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\20110518\VisVen.xml");
                loadXml(@"D:\Projetos\TK1\Code\Web.Pieta\Imovel\Xml\VisVen.xml");

            }
            catch (Exception exception)
            {
                
            }
            //loadSettingsFile();
            //settings.Prop1 = "novo string";
            //saveSettings();

            //
            //sendMail();

            //SiteController siteController = new SiteController();

            //var siteAd = siteController.GetSiteAds();

            //var mailContent = HtmlTemplates.GetContactMailTemplate();

            //searchSite();S

            //var cities = SiteController.GetCities();
            //var districts = SiteController.GetDistricts();
            //var businessSiteTypes = SiteController.GetSiteTypes(SiteAdCategories.Business);
            //var residencialSiteTypes = SiteController.GetSiteTypes(SiteAdCategories.Residence);
        }

        private static void searchSite()
        {
            SiteSearchParameters parameters = new SiteSearchParameters();
            parameters.AdType = SiteAdTypes.Sell;
            var sites = SiteController.SearchSites(parameters);
        }

        private void sendMail()
        {
            HtmlBuilder html = new HtmlBuilder();
            html.Head.Title("Título");
            html.Body.Attributes.Set("style", "font-family: \"Helvetica Neue\", \"Lucida Grande\", \"Segoe UI\", Arial, Helvetica, Verdana, sans-serif");
            html.Body.AppendDiv("Teste");
            html.Body.AppendHeaderN(1, "Título 1");
            html.Body.AppendHeaderN(2, "Título 1");
            html.Body.AppendParagraph("Paragraph");
            html.Body.AppendLiteral(HtmlBuilder.Div(HtmlBuilder.Div("inner content")));
            string content = html.GetHtmlContent();

            //MailHelper mailHelper = new MailHelper() { MailFrom = "contato@pietaimoveis.com.br", MailTo = "andre.v.mattos@gmail.com" };
            MailHelper.SendMail("assunto", content, "contato@pietaimoveis.com.br", "andre.v.mattos@gmail.com");
        }

        private void loadXml_OLD(string path)
        {
            try
            {
                var log = LogController.WriteXmlLoadLogEntry();
                LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos", "", LogLevels.Info);

                var sites = XmlSiteHelper.LoadSiteFromFile(path);
                if (sites != null)
                {
                    SiteController siteController = new SiteController();
                    siteController.AddSalesSiteAds(sites);
                }
                //var pics = XmlSiteHelper.LoadSitePicFromFile(@"D:\Projetos\TK1\Projects\Pietá\Integração\imoveisfoto.xml");
                //if (pics != null)
                //{
                //    SitePicHelper picHelper = new SitePicHelper(@"D:\Projetos\TK1\Projects\Pietá\Integração\Aluguel");
                //    foreach (var pic in pics)
                //    {
                //        picHelper.Set(pic.SiteCode, pic.SitePicCode, pic.FileData);
                //        LogController.WriteXmlLoadMessageLogEntry(log, "Foto gravada", picHelper.Path + picHelper.FileName, LogLevels.Info);
                //    }
                //}
            }
            catch (Exception exception)
            {
                LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
            }
        }
        private void loadXml(string path)
        {
            XmlLoadLog log = null;

            try
            {
                log = LogController.WriteXmlLoadLogEntry();
                LogController.WriteXmlLoadMessageLogEntry(log, "Iniciando Carga de Arquivos DEBUG", "", LogLevels.Info);

                var sites = XmlSiteHelper.LoadSiteFromFile(path);
                if (sites != null)
                {
                    SiteController siteController = new SiteController();
                    siteController.AddSalesSiteAds(sites);
                }
                LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos DEBUG", "Sucesso", LogLevels.Info);

            }
            catch (Exception exception)
            {
                if (log != null)
                    LogController.WriteXmlLoadMessageLogEntry(log, "Finalizando Carga de Arquivos DEBUG", "Falha", LogLevels.Info);
                LogController.WriteAppLogEntry(exception.Message, exception.ToString(), LogLevels.Error);
            }
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
            try
            {
                AM_DWEntities entities = new AM_DWEntities();
                int count = 0;
                DateTime from = new DateTime(1970, 1, 1);
                DateTime to = new DateTime(2070, 12, 31);
                DateTime date = from;
                while (date <= to)
                {
                    count++;
                    var exists = (from el in entities.Times
                                  where el.TimeID == date
                                  select el).FirstOrDefault() != null;
                    if (!exists)
                    {
                        entities.Times.AddObject(new Time() { Day = date.Day, Month = date.Month, Year = date.Year, Quarter = ((date.Month - 1) / 3) + 1, TimeID = date });
                        entities.SaveChanges();
                    }
                    date = date.AddDays(1);
                }
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
    public class foo
    {
        public EnvironmentVariableTarget Target { get; set; }
        public string Name { get; set; }
    }

}
