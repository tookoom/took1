using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
//using Took1.Silverlight.LifeManager.Data.Source.Xml;
using System.IO;
using System.Xml.Linq;
using System.Windows.Browser;
using System.Text;
using Took1.Silverlight.Controls;
using DevExpress.AgMenu;
using System.Windows.Media.Imaging;
using Took1.Silverlight.LifeManager.Control;
using Took1.Web.Cloud;
using System.Windows.Ria.Data;

namespace Took1.Silverlight.LifeManager.App
{
    public partial class Page : UserControl
    {
        #region PRIVATE MEMBERS
        PageController pageController;
        //XmlDataSource xmlDataSource;
        Output outputWindow;

        string dataSourceName = "datasource.xml";
        string dataSourceUploader = @"dataSourceUploader.php";
        //string dataSourcePathUri = @"http://lifemanager.007sites.com/DataSource/";
        string dataSourcePathUri = @"http://nc-desenv01.numericon.com.br/php/datasource/";

        string binUploaderUri = @"http://lifemanager.007sites.com/datasource.xml";
        
        #endregion

        public Page()
        {
            InitializeComponent();

            teste();

            if (HtmlPage.Document.QueryString.ContainsKey("Blank"))
                backgroundRect.Visibility = Visibility.Collapsed;
            else backgroundRect.Visibility = Visibility.Visible;

            //xmlDataSource = new XmlDataSource();
            //loadDataSourceFromUri(dataSourcePathUri + dataSourceName);
            pageController = new PageController(this);
            outputWindow = new Output();
            showOutput();

            //if (xmlDataSource.DataSource != null)
            //{
            //    AccountEditor accountEditor1 = new AccountEditor(xmlDataSource.DataSource);
            //    addWindow(accountEditor1, "Contas 1", LifeManagerWindowNames.ACCOUNT_EDITOR);

            //    AccountEditor accountEditor2 = new AccountEditor(xmlDataSource.DataSource);
            //    addWindow(accountEditor2, "Contas 2", LifeManagerWindowNames.ACCOUNT_EDITOR);
            //}

            dragWindowHost.WriteOutput += new Took1.Silverlight.Controls.WriteOutputEventHandler(dragWindowHost_WriteOutput);

            //dragWindowHost.DragWindows.Add(new Took1.Silverlight.Controls.DragWindow(new DatePicker() { Width = 200, Height = 30 }, "Button 1", null));

            loadLauncherMenuItems();
        }

        private void teste()
        {
            var domain = new LifeManagerDomainContext();
            LoadOperation<Person> io = domain.Load<Person>(domain.GetPersonQuery());
            io.Completed += new EventHandler(io_Completed);

        }

        void io_Completed(object sender, EventArgs e)
        {
            LoadOperation<Person> loadOperation = sender as LoadOperation<Person>;
            if (loadOperation.HasError)
            {
                MessageBox.Show(loadOperation.Error.Message);
            }
        }


        private void addWindow(object content, string title, string tag)
        {
            if (dragWindowHost.GetDragWindow(content)== null)
            {
                dragWindowHost.DragWindows.Add(new DragWindow(content, title, tag));
            }
            else
                writeOutput(string.Format("Window {0} already opened", tag));

        }
        private void handleMenuClick(AgMenuItem menuItem, string tag)
        {
            switch (tag)
            {
                //case LifeManagerMenuItems.DATA_ACCOUNT:
                //    writeOutput("Data Account");
                //    if (xmlDataSource != null)
                //    {
                //        AccountEditor accountEditor = new AccountEditor(xmlDataSource.DataSource);
                //        addWindow(accountEditor, "Contas", LifeManagerWindowNames.ACCOUNT_EDITOR);
                //    }
                //    break;
                //case LifeManagerMenuItems.DATA_CATEGORY:
                //    writeOutput("Data Category");
                //    if (xmlDataSource != null)
                //    {
                //        CategoryEditor categoryEditor = new CategoryEditor(xmlDataSource.DataSource);
                //        addWindow(categoryEditor, "Categorias", LifeManagerWindowNames.CATEGORY_EDITOR);
                //    }
                //    break;
                //case LifeManagerMenuItems.DATA_CHECKPOINT:
                //    writeOutput("Data CheckPoint");
                //    if (xmlDataSource != null)
                //    {
                //        CheckPointEditor checkPointEditor = new CheckPointEditor(xmlDataSource.DataSource);
                //        addWindow(checkPointEditor, "CheckPoints", LifeManagerWindowNames.CHECKPOINT_EDITOR);
                //    }
                //    break;
                //case LifeManagerMenuItems.DATA_EVENT:
                //    writeOutput("Data Event");
                //    if (xmlDataSource != null)
                //    {
                //        EventEditor eventEditor = new EventEditor(xmlDataSource.DataSource);
                //        addWindow(eventEditor, "Eventos", LifeManagerWindowNames.EVENT_EDITOR);
                //    }
                //    break;
                //case LifeManagerMenuItems.FILE_OPEN:
                //    loadDataSourceFromUri(dataSourcePathUri + dataSourceName);
                //    break;
                //case LifeManagerMenuItems.FILE_SAVE:
                //    writeDataSourceToUri(dataSourcePathUri + dataSourceUploader);
                //    break;
                    
                //case LifeManagerMenuItems.VIEW_OUTPUT:
                //    writeOutput("Case Output");
                //    if (menuItem.IsChecked)
                //        showOutput();
                //    else
                //        hideOutput();
                //    break;
                //case LifeManagerMenuItems.TOOLBAR_CASCADE:
                //case LifeManagerMenuItems.WINDOW_CASCADE:
                //    dragWindowHost.OrganizeWindows(WindowLayoutType.Cascade);
                //    break;
                //case LifeManagerMenuItems.TOOLBAR_HORIZONTAL:
                //case LifeManagerMenuItems.WINDOW_HORIZONTAL:
                //    dragWindowHost.OrganizeWindows(WindowLayoutType.Horizontal);
                //    break;
                //case LifeManagerMenuItems.TOOLBAR_VERTICAL:
                //case LifeManagerMenuItems.WINDOW_VERTICAL:
                //    dragWindowHost.OrganizeWindows(WindowLayoutType.Vertical);
                //    break;
                //case LifeManagerMenuItems.TOOLBAR_OUTPUT:
                //    writeOutput("Case ToolBarOutput");
                //    if (menuItem.IsChecked)
                //        showOutput();
                //    else
                //        hideOutput();
                //    break;

                //default:
                //    writeOutput("default");
                //    break;
            }
        }
        private void handleMenuClick(LauncherItem launcherItem, string tag)
        {
            switch (tag)
            {
                case LifeManagerMenuItems.CONTROL_ALARM:
                    break;
                case LifeManagerMenuItems.CONTROL_EVENT:
                    break;
                case LifeManagerMenuItems.CONTROL_TRANSACTION:
                    //if (xmlDataSource != null)
                    //{
                    //    TransactionControl transactionControl = new TransactionControl(xmlDataSource.DataSource);
                    //    addWindow(transactionControl, "Transações", LifeManagerWindowNames.TRANSACTION_CONTROL);
                    //}
                    break;
                case LifeManagerMenuItems.CONTROL_SEARCH:
                    break;
                case LifeManagerMenuItems.CONTROL_STATS:
                    //if (xmlDataSource != null)
                    //{
                    //    StatControl statControl = new StatControl(xmlDataSource.DataSource);
                    //    addWindow(statControl, "Estatísticas", LifeManagerWindowNames.STAT_CONTROL);
                    //}
                    break;

                default:
                    writeOutput("default");
                    break;
            }
        }
        private void hideOutput()
        {
            DragWindow dragWindow = dragWindowHost.GetDragWindow(outputWindow);
            if (dragWindow != null)
                dragWindowHost.DragWindows.Remove(dragWindow);
        }
        private void loadLauncherMenuItems()
        {
            launcherMenu.MenuItemClickEffect = Launcher.ClickEffect.Bounce;
            launcherMenu.MenuItemClicked += new MenuIndexChangedHandler(launcherMenu_MenuItemClicked);

            //launcherMenu.Items.Clear();
            //launcherMenu.Items.Add(LauncherItem.CreateRelative("EXPENSE", "In/Out", "Images/folder_open.png", LifeManagerMenuItems.CONTROL_EXPENSE));
            //launcherMenu.Items.Add(LauncherItem.CreateRelative("EVENTS", "Eventos", "Images/folder_open.png", LifeManagerMenuItems.CONTROL_EVENT));
            //launcherMenu.Items.Add(LauncherItem.CreateRelative("STATS", "Estatísticas", "Images/folder_open.png", LifeManagerMenuItems.CONTROL_STATS));
            //launcherMenu.Items.Add(LauncherItem.CreateRelative("ALARM", "Alertas", "Images/folder_open.png", LifeManagerMenuItems.CONTROL_ALARM));
            //launcherMenu.Items.Add(LauncherItem.CreateRelative("SEARCH", "Pesquisa", "Images/folder_open.png", LifeManagerMenuItems.CONTROL_SEARCH));
        }
        private void removeWindow(string tag)
        {
            foreach (DragWindow dragWindow in dragWindowHost.GetDragWindow(tag))
                dragWindowHost.DragWindows.Remove(dragWindow);
        }

        private void showOutput()
        {
            addWindow(outputWindow, "Output", LifeManagerWindowNames.OUTPUT);
        }
        private void writeOutput(string output)
        {
            outputWindow.Append(output);
        }

        #region DATA SOURCE ###############################################################################################################################
        //private string getAppPath()
        //{
        //    string path = HtmlPage.Document.DocumentUri.AbsolutePath;

        //    path = path.Substring(0, path.LastIndexOf("/") + 1);
        //    return string.Concat("http://", HtmlPage.Document.DocumentUri.Host, ":", HtmlPage.Document.DocumentUri.Port, path);

        //}

        //private void loadDataSourceFromFile()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "XML Files (*.xml)|*.xml";
        //    openFileDialog.Multiselect = false;
        //    openFileDialog.ShowDialog();
        //    if (openFileDialog.File != null)
        //    {
        //        using (StreamReader reader = openFileDialog.File.OpenText())
        //        {
        //            string xmlContent = reader.ReadToEnd();
        //            xmlDataSource.Load(xmlContent);
        //        }
        //    }
        //}
        //private void loadDataSourceFromResource()
        //{
        //    Stream stream = this.GetType().Assembly.GetManifestResourceStream("Took1.Silverlight.LifeManager.App.Resources.DataSource.xml");
        //    StreamReader streamReader = new StreamReader(stream);
        //    string xmlContent = streamReader.ReadToEnd();
        //    xmlDataSource.Load(xmlContent);
        //}
        //private void loadDataSourceFromUri(string dataSourceUri)
        //{
        //    WebClient dataSourceClient = new WebClient();
        //    dataSourceClient.Encoding = System.Text.Encoding.UTF8;

        //    dataSourceClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(dataSourceClient_DownloadProgressChanged);
        //    dataSourceClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(dataSourceClient_DownloadStringCompleted);

        //    try
        //    {
        //        dataSourceClient.DownloadStringAsync(new Uri(dataSourceUri));
        //    }
        //    catch (WebException exception)
        //    {
        //        HtmlPage.Window.Alert("loadDataSourceFromUri" + exception.Message);
        //    }
        //}
        //private void dataSourceClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    try
        //    {
        //        //xmlDataSource.Load(e.Result);
        //        writeOutput("dataSourceClient_DownloadStringCompleted");
        //    }
        //    catch (Exception exception)
        //    {
        //        HtmlPage.Window.Alert("dataSourceClient_DownloadStringCompleted" + exception.Message);
        //    }
        //}
        //private void dataSourceClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //}

        //private void writeDataSourceToFile()
        //{

        //}
        //private void writeDataSourceToUri(string uploaderUri)
        //{
        //    xmlDataSource.GenerateXml();
        //    byte[] fileBytes = Encoding.UTF8.GetBytes(xmlDataSource.XmlContent);
        //    string fileContents = Convert.ToBase64String(fileBytes);


        //    Dictionary<string, string> parameters = new Dictionary<string, string>();

        //    parameters.Add("reldir", "");
        //    parameters.Add("filename", dataSourceName);
        //    parameters.Add("textdata", System.Windows.Browser.HttpUtility.UrlEncode(fileContents));

        //    string parameterString = string.Empty; ;
        //    foreach (KeyValuePair<string, string> keyValue in parameters)
        //    {
        //        parameterString += string.Format("{0}={1}&", keyValue.Key, keyValue.Value);
        //    }
        //    parameterString = parameterString.Remove(parameterString.LastIndexOf("&"));

        //    WebClient dataSourceClient = new WebClient();
        //    dataSourceClient.Encoding = System.Text.Encoding.UTF8;
        //    dataSourceClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
        //    dataSourceClient.UploadProgressChanged += new UploadProgressChangedEventHandler(dataSourceClient_UploadProgressChanged);
        //    dataSourceClient.UploadStringCompleted += new UploadStringCompletedEventHandler(dataSourceClient_UploadStringCompleted);
        //    try
        //    {
        //        dataSourceClient.UploadStringAsync(new Uri(uploaderUri), "Post", parameterString);
        //    }
        //    catch (WebException exception)
        //    {
        //        HtmlPage.Window.Alert("writeDataSourceToUri" + exception.Message);
        //    }
        //}
        //void dataSourceClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        //{
        //    string res = e.Result;
        //    writeOutput("dataSourceClient_UploadStringCompleted");
        //}
        //void dataSourceClient_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        //{
        //}

        #endregion
        #region EVENT HANDLERS ############################################################################################################################
        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            //loadDataSourceFromUri(dataSourcePathUri + dataSourceName);
        }
        private void buttonWrite_Click(object sender, RoutedEventArgs e)
        {
            //xmlDataSource.DataSource.AccountEntities.First().Name = "mudando denovo";
            //writeDataSourceToUri(dataSourcePathUri + dataSourceUploader);
        }

        private void dragWindowHost_WriteOutput(object sender, Took1.Silverlight.Controls.WriteOutputEventArgs e)
        {
            writeOutput(e.Message);
        }
        private void AgMenuItem_Click(object sender, EventArgs e)
        {
            AgMenuItem menuItem = sender as AgMenuItem;
            string tag = menuItem.Tag as string;
            writeOutput("MenuItem.Tag = " + tag);
            handleMenuClick(menuItem, tag);
        }

        void launcherMenu_MenuItemClicked(object sender, SelectedMenuItemArgs e)
        {
            Launcher launcher = sender as Launcher;
            LauncherItem launcherItem = e.Item;
            string tag = launcherItem.Tag as string;
            writeOutput("LauncherItem.Tag = " + tag);
            handleMenuClick(launcherItem, tag);
        }



	    #endregion        
        #region UPLOAD TESTS ##############################################################################################################################
        /*private void UploadImage()
        {
            string dataSourceUri = @"http://lifemanager.007sites.com/Upload/test.php";
            Uri uploadService = new Uri(dataSourceUri);
            WebRequest _request = System.Net.HttpWebRequest.Create(uploadService);
            _request.Method = "POST";
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.BeginGetRequestStream(new AsyncCallback(BeginGetRequestStreamCallback), _request);
        }
        private void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)asyncResult.AsyncState;
            Stream body = (Stream)request.EndGetRequestStream(asyncResult);

            string fileName = "teste.txt";// Soubor.Name;//  ofd.SelectedFile.Name;
            string fileContents = "Convert.ToBase64String(fileBytes)";

            UTF8Encoding encoding = new UTF8Encoding();

            string formBody = "fileName=" + HttpUtility.UrlEncode(fileName) + "&" + "fileContents=" + HttpUtility.UrlEncode(fileContents); //"ok=Upload" + "&" +
            byte[] formBytes = encoding.GetBytes(formBody);

            body.Write(formBytes, 0, formBytes.Length);

            // Send the HTTP request.
            //_request.AllowWriteStreamBuffering = true;
            request.BeginGetResponse(new AsyncCallback(BeginGetResponseCallback), request);


        }
        private void BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)asyncResult.AsyncState;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.EndGetResponse(asyncResult);

            bool result;
            if (response.StatusCode.ToString() == "OK")
                result = true;
            else
                result = false;
            return;
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string xmlContent = streamReader.ReadToEnd();
            xmlDataSource.Load(xmlContent);

            xmlDataSource.DataSource.AccountList.First().Name = "nada a ver";
            writeDataSourceToUri(dataSourceUri);
        }

        static public void UploadImageDlg()
        {

            OpenFileDialog d = new OpenFileDialog();


            //d.Filter = "image JPG (*.jpg) |*.jpg | all files (*.*) | *.*";
            //d.Multiselect = true;

            if (d.ShowDialog() == true)
            {

                foreach (FileInfo f in d.Files) uploadPicture(f, "upload/", "");
            }

        }
        public static void uploadPicture(FileInfo finfo, string relDir, string rename)
        {

            string fileName = finfo.Name;// ofd.SelectedFile.Name;

            Stream f = finfo.OpenRead();// ofd.SelectedFile.OpenRead();

            byte[] fileBytes = new byte[f.Length];
            int byteCount = f.Read(fileBytes, 0, (int)f.Length);

            string fileContents = Convert.ToBase64String(fileBytes);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("reldir", relDir);
            parameters.Add("filename", fileName);
            parameters.Add("textdata", System.Windows.Browser.HttpUtility.UrlEncode(fileContents));

            string res = uploadData(@"http://localhost/upload/test_original.php", parameters);
        }
        public static void uploadFile(string fileContent, string relDir, string fileName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("reldir", relDir);
            parameters.Add("filename", fileName);
            parameters.Add("textdata", System.Windows.Browser.HttpUtility.UrlEncode(fileContent));

            string res = uploadData(@"http://localhost/upload/test.php", parameters);

        }
        public static string uploadData(string URL, Dictionary<string, string> parameters)
        {
            WebClient Client = new WebClient();
            Client.Encoding = System.Text.Encoding.UTF8;

            string parameterString = string.Empty; ;
            foreach (KeyValuePair<string, string> keyValue in parameters)
            {
                parameterString += string.Format("{0}={1}&", keyValue.Key, keyValue.Value);
            }
            parameterString = parameterString.Remove(parameterString.LastIndexOf("&"));

            Client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            Client.UploadProgressChanged += new UploadProgressChangedEventHandler(Client_UploadProgressChanged);

            Client.UploadStringCompleted += new UploadStringCompletedEventHandler(Client_UploadStringCompleted);
            try
            {

                Client.UploadStringAsync(new Uri(URL), "Post", parameterString);
            }

            catch (WebException)
            {

                return ("Error from server");
            }

            return ("OK");
        }

        static void Client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            string result = e.Result;
        }

        static void Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();

        }

        public static string downloadData(string URL)
        {
            WebClient Client = new WebClient();
            Client.Encoding = System.Text.Encoding.UTF8;

            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
            Client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Client_DownloadStringCompleted);
            //dataSourceClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            //dataSourceClient.UploadProgressChanged += new UploadProgressChangedEventHandler(Client_UploadProgressChanged);

            //dataSourceClient.UploadStringCompleted += new UploadStringCompletedEventHandler(Client_UploadStringCompleted);
            try
            {
                Client.DownloadStringAsync(new Uri(URL));
                //dataSourceClient.UploadStringAsync(new Uri(URL), "Post", parameterString);
            }

            catch (WebException)
            {

                return ("Error from server");
            }

            return ("OK");
        }

        static void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string result = e.Result;
            }
            catch (Exception exception)
            {
            }
        }

        static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        } */
        #endregion

    }
}
