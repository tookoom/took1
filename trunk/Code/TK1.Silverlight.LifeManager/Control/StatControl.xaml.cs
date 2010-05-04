using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Took1.Web.Cloud.Data;
using Took1.Web.Cloud;
using Took1.Silverlight.LifeManager.Data.Collection;
using Took1.Web.Data.Ria.Collection;

namespace Took1.Silverlight.LifeManager
{
	public partial class StatControl : UserControl
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

        public StatControl(DataSource dataSource)
        {
            InitializeComponent();

            this.DataSource = dataSource;
        }

        private void initializeValues()
        {
            setCurrentMoneyCheckPoint(dataSource.CurrentValue.MoneyCheckPoint);
            setAccountValues();
        }
        private void setAccountValues()
        {
            if (DataSource != null)
            {
                AccountValueCollection collection = new AccountValueCollection();
                MoneyCheckPoint moneyCheckPoint = (from el in dataSource.MoneyCheckPointEntities
                                         orderby el.Timestamp descending
                                         select el).FirstOrDefault();
                if (moneyCheckPoint != null)
                {
                    //foreach (Account account in moneyCheckPoint.MoneyCheckPointAccount)
                    //{
                    //    float inputs = (from el in dataSource.MoneyTransactionAccountEntities
                    //                    where el.MoneyTransaction.EntryTimestamp >= moneyCheckPoint.Timestamp & el.MoneyTransaction.Type == (int)(TransactionTypes.In) & el.Account == account
                    //                    select el.Value).Sum();
                    //    float outputs = (from el in dataSource.MoneyTransactionAccountEntities
                    //                    where el.MoneyTransaction.EntryTimestamp >= moneyCheckPoint.Timestamp & el.MoneyTransaction.Type == (int)(TransactionTypes.Out) & el.Account == account
                    //                    select el.Value).Sum();
                    //    MoneyCheckPointAccount moneyCheckPointAccount = (from el in dataSource.MoneyCheckPointAccountEntities
                    //                                           where el.Account==account & el.MoneyCheckPoint == moneyCheckPoint 
                    //                                           select el).FirstOrDefault();
                    //    if (moneyCheckPointAccount != null)
                    //    {
                    //        AccountValue accountValue = new AccountValue()
                    //        {
                    //            AccountName = account.Name,
                    //            Value = moneyCheckPointAccount.Value + inputs - outputs
                    //        };
                    //        collection.Add(accountValue);
                    //    }
                        
                    //}
                    //listBoxAccountValue.ItemsSource = collection;
                }
            }
        }
        private void setCurrentMoneyCheckPoint(MoneyCheckPoint moneyCheckPoint)
        {
            //contentControlMoneyCheckPoint.Content = moneyCheckPoint;
            //if (listBoxMoneyCheckPoints.Items.Contains(moneyCheckPoint))
            //{
            //    listBoxMoneyCheckPoints.SelectedItem = moneyCheckPoint;
            //    listBoxMoneyCheckPoints.ScrollIntoView(moneyCheckPoint);
            //}
        }
        private void setCurrentMoneyCheckPointAccount(MoneyCheckPoint moneyCheckPoint)
        {
            MoneyCheckPointAccountCollection collection = new MoneyCheckPointAccountCollection();
            if (moneyCheckPoint != null)
            {
                var query = from el in DataSource.MoneyCheckPointAccountEntities
                            where el.MoneyCheckPointID == moneyCheckPoint.MoneyCheckPointID
                            select el;
                foreach (MoneyCheckPointAccount moneyCheckPointAccount in query)
                    collection.Add(moneyCheckPointAccount);
            }
            //listBoxMoneyCheckPointAccount.ItemsSource = collection;
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.CurrentValue.MoneyCheckPointChanged += new EventHandler(dataSource_CurrentValue_MoneyCheckPointChanged);
        }
    

        #region DATA SOURCE EVENT HANDLERS
        void dataSource_CurrentValue_MoneyCheckPointChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
            {
                setCurrentMoneyCheckPoint(dataSource.CurrentValue.MoneyCheckPoint);
                setCurrentMoneyCheckPointAccount(dataSource.CurrentValue.MoneyCheckPoint);
            }
        }

        #endregion
        #region UI EVENT HANDLERS

        #endregion

	}
}