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
    public partial class AccountEditor : UserControl
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

        public AccountEditor(DataSource dataSource)
        {
            InitializeComponent();

            this.DataSource = dataSource;
        }

        private void initializeValues()
        {
            listBoxAccounts.ItemsSource = dataSource.AccountEntities;
            setCurrentAccount(dataSource.CurrentValue.Account);
        }
        private void setCurrentAccount(Account account)
        {
            contentControlAccount.Content = account;
            if (listBoxAccounts.Items.Contains(account))
            {
                listBoxAccounts.SelectedItem = account;
                listBoxAccounts.ScrollIntoView(account);
            }
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.CurrentValue.AccountChanged += new EventHandler(dataSource_CurrentValue_AccountChanged);
        }


        #region DATA SOURCE EVENT HANDLERS
        void dataSource_CurrentValue_AccountChanged(object sender, EventArgs e)
        {
            if(dataSource!= null)
                setCurrentAccount(dataSource.CurrentValue.Account);
        }
        
        #endregion
        #region UI EVENT HANDLERS
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var source = listBoxAccounts.ItemsSource;
            listBoxAccounts.ItemsSource = null;
            listBoxAccounts.ItemsSource = source;
        }
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAccounts.SelectedItem != null)
            {
                Account account = listBoxAccounts.SelectedItem as Account;
                if (dataSource.AccountEntities.Contains(account))
                {
                    dataSource.AccountEntities.Remove(account);
                    dataSource.CurrentValue.Account = null;
                }
            }
        }
        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account() { Name = "Nova Conta", Description = "Descrição", CreationTimestamp = DateTime.Now };
            //accountEntities.Add(account);
            dataSource.AccountEntities.Add(account);
            dataSource.CurrentValue.Account = account;
            //if (listBoxAccounts.Items.Contains(account))
            //    listBoxAccounts.SelectedItem = account;
            //listBoxAccounts.ScrollIntoView(account);
        }
        private void listBoxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Account account = e.AddedItems[0] as Account;
                    dataSource.CurrentValue.Account = account;
                    //contentControlAccount.Content = account;
                }
            }
        }

        #endregion
    }
}
