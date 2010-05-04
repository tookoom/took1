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
using Took1.Web.Cloud.Data;
using Took1.Web.Cloud;

namespace Took1.Silverlight.LifeManager.Control
{
    public partial class CategoryEditor : UserControl
    {
        #region PRIVATE MEMBERS
        private DataSource dataSource;
        #endregion
        #region PUBLIC PROPERTIES
        public DataSource DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                if (dataSource != null)
                {
                    setDataSourceEventHandlers();
                    initializeValues();
                }
            }
        }





        #endregion        

        public CategoryEditor(DataSource dataSource)
        {
            InitializeComponent();

            this.DataSource = dataSource;

        }

        private void initializeValues()
        {
            listBoxCategories.ItemsSource = dataSource.CategoryEntities;
            setCurrentCategory(dataSource.CurrentValue.Category);
        }
        private void setCurrentCategory(Category category)
        {
            contentControlCategory.Content = category;
            if (listBoxCategories.Items.Contains(category))
            {
                listBoxCategories.SelectedItem = category;
                listBoxCategories.ScrollIntoView(category);
            }
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.CurrentValue.CategoryChanged += new EventHandler(dataSource_CurrentValue_CategoryChanged);
        }



        private void listBoxCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Category category = e.AddedItems[0] as Category;
                    contentControlCategory.Content = category;
                }
            }
        }

        #region DATA SOURCE EVENT HANDLERS
        void dataSource_CurrentValue_CategoryChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
                setCurrentCategory(dataSource.CurrentValue.Category);
        }
        #endregion
        #region UI EVENT HANDLERS
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCategories.SelectedItem != null)
            {
                Category category = listBoxCategories.SelectedItem as Category;
                if (dataSource.CategoryEntities.Contains(category))
                {
                    dataSource.CategoryEntities.Remove(category);
                    dataSource.CurrentValue.Category = null;
                }
            }
        }
        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category() { Name = "Nova Categoria" };
            dataSource.CategoryEntities.Add(category);
            dataSource.CurrentValue.Category = category;
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var source = listBoxCategories.ItemsSource;
            listBoxCategories.ItemsSource = null;
            listBoxCategories.ItemsSource = source;
        }
        private void listBoxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Category category = e.AddedItems[0] as Category;
                    dataSource.CurrentValue.Category = category;
                    //contentControlAccount.Content = account;
                }
            }
        }

        #endregion

    }
}
