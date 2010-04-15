using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Took1.Silverlight.LifeManager.Data.Model;

namespace Took1.Silverlight.LifeManager.Data.Source
{
    public class DataState
    {
        #region EVENTS
        public event EventHandler AccountChanged;
        private void OnAccountChanged(EventArgs e)
        {
            EventHandler handler = AccountChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CategoryChanged;
        private void OnCategoryChanged(EventArgs e)
        {
            EventHandler handler = CategoryChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CheckPointChanged;
        private void OnCheckPointChanged(EventArgs e)
        {
            EventHandler handler = CheckPointChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler EventChanged;
        private void OnEventChanged(EventArgs e)
        {
            EventHandler handler = EventChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler TransactionChanged;
        private void OnTransactionChanged(EventArgs e)
        {
            EventHandler handler = TransactionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event EventHandler TransactionAccountChanged;
        private void OnTransactionAccountChanged(EventArgs e)
        {
            EventHandler handler = TransactionAccountChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 

        #endregion
        #region PRIVATE MEMBERS

        private Account account;
        private Category category;
        private CheckPoint checkPoint;
        private Event evt;
        private Transaction transaction;
        private TransactionAccount transactionAccount;

        #endregion
        #region PUBLIC PROPERTIES
        public Account Account
        {
            get { return account; }
            set
            {
                account = value;
                OnAccountChanged(new EventArgs());
            }
        }
        public CheckPoint CheckPoint
        {
            get { return checkPoint; }
            set
            {
                checkPoint = value;
                OnCheckPointChanged(new EventArgs());
            }
        }
        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                OnCategoryChanged(new EventArgs());
            }
        }
        public Event Event
        {
            get { return evt; }
            set
            {
                evt = value;
                OnEventChanged(new EventArgs());
            }
        }
        public Transaction Transaction
        {
            get { return transaction; }
            set
            {
                transaction = value;
                OnTransactionChanged(new EventArgs());
            }
        }
        public TransactionAccount TransactionAccount
        {
            get { return transactionAccount; }
            set
            {
                transactionAccount = value;
                OnTransactionAccountChanged(new EventArgs());
            }
        }

        #endregion

    }
}
