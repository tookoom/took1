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
using System.IO;
using TK1.Xml;
using TK1.Wpf.Controls;

namespace TK1.Basics.Controls
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        #region EVENTS

        #endregion
        #region PRIVATE MEMBERS
        bool hasPath;
        bool isExpanded;
        string path;
        object source;
        Type type;
        object value;
        string xmlContent;

        #endregion
        #region PUBLIC PROPERTIES
        public object Value
        {
            get { return this.value; }
            set
            {
                this.value = value; 
                Open(value);
            }
        }


        #endregion
        
        public Settings()
        {
            InitializeComponent();
        }

        public void Open(Object obj)
        {
            clearSettings();
            if (obj != null)
            {
                type = obj.GetType();
                value = obj;
                loadSettings();
            }
        }
        public void OpenFile(Type type, string path)
        {
            clearSettings();
            this.path = path;
            this.type = type;
            try
            {
                xmlContent = File.ReadAllText(path);
                loadSettings();
            }
            catch (FileNotFoundException fileException)
            {
                xmlContent = null;
                throw fileException;
            }
        }
        public void OpenContent(Type type, string xmlContent)
        {
            clearSettings();
            this.type = type;
            this.xmlContent = xmlContent;
            loadSettings();
        }

        private void clearSettings()
        {
            this.path = string.Empty;
            this.type = null;
            this.value = null;
            this.xmlContent = string.Empty;
        }
        private void loadSettings()
        {
            if (value == null)
            {
            }
            else
            {
                propertyEditor.Source = value;
            }
        }



        #region UI EVENT HANDLERS
        private void buttonOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        private void buttonReload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        private void buttonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        #endregion
    }
}
