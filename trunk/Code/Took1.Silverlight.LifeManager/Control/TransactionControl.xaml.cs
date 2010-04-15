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
    public partial class TransactionControl : UserControl
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

        public TransactionControl(DataSource dataSource)
        {
            InitializeComponent();
            this.DataSource = dataSource;

            //MoneyTransaction nt = new MoneyTransaction()
            //{
            //    DataSource = dataSource,
            //    Category = dataSource.CategoryEntities.FirstOrDefault(),
            //    EntryTimeStamp = DateTime.Now,
            //    Name = "Gasto3",
            //    Type = 0,
            //};
            //TransactionController transactionController = new TransactionController(dataSource);
            //transactionController.Add(nt);
            //AccountController accountController = new AccountController(dataSource);
            //Account account = accountController.Get().FirstOrDefault();

            //MoneyTransaction moneyTransaction = dataSource.TransactionEntities.FirstOrDefault();
            //moneyTransaction.Accounts.Add(account);

            //setCurrentTransaction(moneyTransaction);
        }

        private void initializeValues()
        {
            if (dataSource != null)
            {
                comboBoxAccount.ItemsSource = dataSource.AccountEntities;
                setTransactionSource();
            }
        }
        private void setCurrentTransaction(MoneyTransaction moneyTransaction)
        {
            if (moneyTransaction == null)
            {
                contentControlTransaction.Content = null;
                listBoxTransactionAccount.ItemsSource = null;
            }
            else
            {
                contentControlTransaction.Content = moneyTransaction;
                listBoxTransactionAccount.ItemsSource = moneyTransaction.MoneyTransactionAccount;
            }
            //radioButtonIn.DataContext = moneyTransaction;
            //radioButtonOut.DataContext = moneyTransaction;

            //contentControlEvent.Content = moneyTransaction;
            //if (listBoxEvents.Items.Contains(moneyTransaction))
            //{
            //    listBoxEvents.SelectedItem = moneyTransaction;
            //    listBoxEvents.ScrollIntoView(moneyTransaction);
            //}
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.CurrentValue.MoneyTransactionChanged += new EventHandler(CurrentValue_MoneyTransactionChanged);
            dataSource.CurrentValue.MoneyTransactionAccountChanged += new EventHandler(CurrentValue_MoneyTransactionAccountChanged);

            dataSource.AccountEntitiesChanged += new EventHandler(dataSource_AccountEntitiesChanged);
            dataSource.MoneyTransactionAccountEntitiesChanged += new EventHandler(dataSource_MoneyTransactionAccountEntitiesChanged);

        }

        private void setTransactionSource()
        {
            if (dataSource != null)
            {
                MoneyTransactionCollection collection = null;
                //if (dataSource.TransactionEntities != null)
                //    dataSource.CurrentTransaction = dataSource.TransactionEntities.FirstOrDefault();
                if (dataSource.MoneyTransactionEntities != null)
                {
                    int type = 0;

                    if (radioButtonIn.IsChecked.HasValue)
                        if (radioButtonIn.IsChecked.Value)
                            type = 0;

                    if (radioButtonOut.IsChecked.HasValue)
                        if (radioButtonOut.IsChecked.Value)
                            type = 1;

                    var list = (from el in dataSource.MoneyTransactionEntities
                                where el.Type == type
                                select el).ToList();
                    collection = new MoneyTransactionCollection();
                    foreach (MoneyTransaction moneyTransaction in list)
                        collection.Add(moneyTransaction);
                    dataSource.CurrentValue.MoneyTransaction = collection.FirstOrDefault();
                }

                sliderTransactions.DataContext = collection;
            }
        }


        #region DATA SOURCE EVENT HANDLERS
        void CurrentValue_MoneyTransactionChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
                setCurrentTransaction(dataSource.CurrentValue.MoneyTransaction);
        }
        void CurrentValue_MoneyTransactionAccountChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
                comboBoxAccount.SelectedItem = dataSource.CurrentValue.MoneyTransactionAccount.Account;
        }
        
        void dataSource_AccountEntitiesChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
            {
                comboBoxAccount.ItemsSource = dataSource.AccountEntities;
                setTransactionSource();
            }
        }
        void dataSource_MoneyTransactionAccountEntitiesChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
                setCurrentTransaction(dataSource.CurrentValue.MoneyTransaction);
        }

        #endregion

        #region UI EVENT HANDLERS
        private void buttonNewTransactionAccount_Click(object sender, RoutedEventArgs e)
        {
            if (dataSource.CurrentValue.MoneyTransaction != null)
            {
                Account account = comboBoxAccount.SelectedItem as Account;
                if (account != null)
                {
                    MoneyTransactionAccount transactionAccount = new MoneyTransactionAccount
                    {
                        //DataSource = this.DataSource,
                        Account = account,
                        MoneyTransaction = dataSource.CurrentValue.MoneyTransaction,
                        Value = 0
                    };
//                    dataSource.CurrentValue.Tr
                    dataSource.CurrentValue.MoneyTransaction.MoneyTransactionAccount.Add(transactionAccount);
                }
            }
        }
        private void buttonDeleteTransactionAccount_Click(object sender, RoutedEventArgs e)
        {
            //MoneyTransactionAccount transactionAccount
        }
        private void buttonSaveTransactionAccount_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void buttonGoToFirstTransaction_Click(object sender, RoutedEventArgs e)
        {
            sliderTransactions.Value = sliderTransactions.Minimum;
        }
        private void buttonGoToPreviousTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (sliderTransactions.Value > sliderTransactions.Minimum)
                sliderTransactions.Value--;

        }
        private void buttonGoToNextTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (sliderTransactions.Value < sliderTransactions.Maximum)
                sliderTransactions.Value++;

        }
        private void buttonGoToLastTransaction_Click(object sender, RoutedEventArgs e)
        {
            sliderTransactions.Value = sliderTransactions.Maximum;
        }

        private void listBoxTransactionAccount_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dataSource != null)
                dataSource.CurrentValue.MoneyTransactionAccount = listBoxTransactionAccount.SelectedItem as MoneyTransactionAccount;
        }
        
        private void sliderTransactions_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (dataSource != null)
            {
                //TransactionController transactionController = new TransactionController(DataSource);
                //MoneyTransactionTypes transactionType = TransactionTypes.In;
                //if (radioButtonIn.IsChecked.HasValue)
                //    if (radioButtonIn.IsChecked.Value)
                //        transactionType = TransactionTypes.In;

                //if (radioButtonOut.IsChecked.HasValue)
                //    if (radioButtonOut.IsChecked.Value)
                //        transactionType = TransactionTypes.Out;

                //dataSource.CurrentValue.MoneyTransaction = transactionController.Get(transactionType).Skip((int)sliderTransactions.Value - 1).FirstOrDefault();
                //textBoxTransactionIndex.Text = ((int)sliderTransactions.Value).ToString();
            }
        }

        private void radioButtonIn_Checked(object sender, RoutedEventArgs e)
        {
            setTransactionSource();
        }
        private void radioButtonOut_Checked(object sender, RoutedEventArgs e)
        {
            setTransactionSource();
        }

        
        #endregion
    }
}
