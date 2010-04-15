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
using Took1.Web.Data.Ria.Collection;

namespace Took1.Silverlight.LifeManager.Control
{
    public partial class CheckPointEditor : UserControl
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

        public CheckPointEditor(DataSource dataSource)
        {
            InitializeComponent();

            this.DataSource = dataSource;
        }

        private void initializeValues()
        {
            listBoxCheckPoints.ItemsSource = dataSource.MoneyCheckPointEntities;
            setCurrentCheckPoint(dataSource.CurrentValue.MoneyCheckPoint);
            setAccountEntities();
        }
        private void setAccountEntities()
        {
            if(DataSource!= null)
                comboBoxAccount.ItemsSource = DataSource.AccountEntities;
        }
        private void setCurrentCheckPoint(MoneyCheckPoint moneyCheckPoint)
        {
            contentControlCheckPoint.Content = moneyCheckPoint;
            if (listBoxCheckPoints.Items.Contains(moneyCheckPoint))
            {
                try
                {
                    listBoxCheckPoints.SelectedItem = moneyCheckPoint;
                    listBoxCheckPoints.ScrollIntoView(moneyCheckPoint);
                }
                catch{}
            }
        }
        private void setCurrentCheckPointAccount(MoneyCheckPoint moneyCheckPoint)
        {
            MoneyCheckPointAccountCollection collection = new MoneyCheckPointAccountCollection();
            if (moneyCheckPoint != null)
            {
                var query = from el in DataSource.MoneyCheckPointAccountEntities
                            where el.MoneyCheckPointID == moneyCheckPoint.MoneyCheckPointID
                            select el;
                foreach (MoneyCheckPointAccount checkPointAccount in query)
                    collection.Add(checkPointAccount);
            }
            listBoxCheckPointAccount.ItemsSource = collection;
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.AccountEntitiesChanged +=new EventHandler(dataSource_AccountEntitiesChanged);
            dataSource.CurrentValue.MoneyCheckPointChanged += new EventHandler(dataSource_CurrentValue_MoneyCheckPointChanged);
        }

        void dataSource_AccountEntitiesChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
   

        #region DATA SOURCE EVENT HANDLERS
        void AccountEntities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            setAccountEntities();
        }
        void dataSource_CurrentValue_MoneyCheckPointChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
            {
                setCurrentCheckPoint(dataSource.CurrentValue.MoneyCheckPoint);
                setCurrentCheckPointAccount(dataSource.CurrentValue.MoneyCheckPoint);
            }
        }

        #endregion
        #region UI EVENT HANDLERS
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCheckPoints.SelectedItem != null)
            {
                MoneyCheckPoint moneyCheckPoint = listBoxCheckPoints.SelectedItem as MoneyCheckPoint;
                if (dataSource.MoneyCheckPointEntities.Contains(moneyCheckPoint))
                {
                    dataSource.MoneyCheckPointEntities.Remove(moneyCheckPoint);
                    dataSource.CurrentValue.MoneyCheckPoint = null;
                }
            }
        }
        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            MoneyCheckPoint moneyCheckPoint = new MoneyCheckPoint() { Name = "Novo MoneyCheckPoint", Timestamp= DateTime.Now };
            dataSource.MoneyCheckPointEntities.Add(moneyCheckPoint);
            dataSource.CurrentValue.MoneyCheckPoint = moneyCheckPoint;
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var source = listBoxCheckPoints.ItemsSource;
            listBoxCheckPoints.ItemsSource = null;
            listBoxCheckPoints.ItemsSource = source;
        }

        private void buttonDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxCheckPointAccount.SelectedItem != null)
            {
                MoneyCheckPointAccount checkPointAccount = listBoxCheckPointAccount.SelectedItem as MoneyCheckPointAccount;
                if (dataSource.MoneyCheckPointAccountEntities.Contains(checkPointAccount))
                {
                    dataSource.MoneyCheckPointAccountEntities.Remove(checkPointAccount);
                    setCurrentCheckPointAccount(dataSource.CurrentValue.MoneyCheckPoint);
                    //dataSource.CurrentCheckPoint = null;
                }

            }
        }
        private void buttonNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (DataSource.CurrentValue.MoneyCheckPoint != null)
            {
                Account account = comboBoxAccount.SelectedItem as Account;
                if (account != null)
                {
                    MoneyCheckPointAccount moneyCheckPointAccount = new MoneyCheckPointAccount() { Value = 0 };
                    dataSource.MoneyCheckPointAccountEntities.Add(moneyCheckPointAccount);
                    moneyCheckPointAccount.Account = account;
                    moneyCheckPointAccount.MoneyCheckPoint = DataSource.CurrentValue.MoneyCheckPoint;
                    setCurrentCheckPointAccount(dataSource.CurrentValue.MoneyCheckPoint);
                    //dataSource.CurrentCheckPoint = checkPointAccount;
                }
            }
        }
        private void buttonSaveAccount_Click(object sender, RoutedEventArgs e)
        {
            var source = listBoxCheckPointAccount.ItemsSource;
            listBoxCheckPointAccount.ItemsSource = null;
            listBoxCheckPointAccount.ItemsSource = source;

        }

        private void listBoxCheckPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    MoneyCheckPoint moneyCheckPoint = e.AddedItems[0] as MoneyCheckPoint;
                    dataSource.CurrentValue.MoneyCheckPoint = moneyCheckPoint;
                }
            }
        }

        #endregion


    }
}
