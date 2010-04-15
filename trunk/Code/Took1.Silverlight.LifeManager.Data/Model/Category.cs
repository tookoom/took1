using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Controller;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class Category : BaseModel
    {
        #region PRIVATE MEMBERS
        private TransactionCollection transactions = null;

        #endregion
        #region PUBLIC PROPERTIES
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public TransactionCollection Transactions
        {
            get
            {
                loadTransactions();
                return transactions;
            }
            //set { transactions = value; }
        }

        #endregion
 
        public Category()
        {
            Name = string.Empty;
        }

        private bool addTransaction(Transaction transaction)
        {
            bool result = true;
            if (transaction != null)
            {
                if (transaction.TransactionID == 0)
                    DataSource.TransactionEntities.Add(transaction);
                transaction.Category = this;
            }
            else
            {
                result = false;
            }
            return result;
        }
        private void loadTransactions()
        {
            if (DataSource != null)
            {
                List<int> transactionIDList = (from el in DataSource.TransactionEntities
                                           where el.CategoryID == CategoryID
                                           select el.TransactionID).Distinct().ToList();
                transactions = new TransactionCollection();
                TransactionController transactionController = new TransactionController(DataSource);
                foreach (int transactionID in transactionIDList)
                {
                    Transaction transaction = transactionController.Get(transactionID);
                    if (transaction != null)
                        transactions.Add(transaction);
                } 
                transactions.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(transactions_CollectionChanged);
            }
        }

        #region EVENT HANDLERS
        void transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Transaction transaction in e.NewItems)
                        addTransaction(transaction);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
