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
using Microsoft.Win32;
using System.IO;
using Took1.Data;
using Took1.Xml;

namespace Took1.ConfigManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PRIVATE MEMBERS
        string path = string.Empty;
        ParameterCollection parameterCollection;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            parameterCollection = new ParameterCollection();

        }

        private void bindParameterCollection()
        {
            listBoxParameter.ItemsSource = null;
            listBoxParameter.ItemsSource = parameterCollection;
        }
        private void openFile()
        {
            string content = File.ReadAllText(path);
            ParameterCollection collection = XmlSerializer<ParameterCollection>.Load(content);
            if (collection != null)
            {
                parameterCollection = collection;
                bindParameterCollection();
            }
        }
        private void saveFile()
        {
            string content = XmlSerializer<ParameterCollection>.Save(parameterCollection);
            File.WriteAllText(path, content);
        }

        #region UI EVENT HANDLERS

        private void menuItemFileOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue)
            {
                if (result.Value)
                {
                    path = openFileDialog.FileName;
                    openFile();
                }

            }

        }
        private void menuItemFileQuit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        private void menuItemFileSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (path == string.Empty)
            {
                bool? result = saveFileDialog.ShowDialog();
                if (result.HasValue)
                {
                    if (result.Value)
                    {
                        path = saveFileDialog.FileName;
                        saveFile();
                    }
                }
            }
            else
            {
                saveFile();
            }
        }
        private void menuItemFileSaveAs_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            bool? result = saveFileDialog.ShowDialog();
            if (result.HasValue)
            {
                if (result.Value)
                {
                    path = saveFileDialog.FileName;
                    saveFile();
                }
            }
        }
        
        #endregion    
    }
}
