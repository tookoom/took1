using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Controller;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class TransactionAccount : BaseModel
    {
        #region PRIVATE MEMBERS
        private Transaction transaction = null;
        private Account account = null;
        
        #endregion
        #region PUBLIC PROPERTIES
        public int TransactionAccountID { get; set; }
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public float Value { get; set; }

        public Transaction Transaction
        {
            get
            {
                loadTransactionReference();
                return transaction;
            }
            set
            {
                transaction = value;
                if (value != null)
                {
                    if (!DataSource.TransactionEntities.Contains(transaction))
                        DataSource.TransactionEntities.Add(transaction);
                    TransactionID = transaction.TransactionID;
                }
            }
        }
        public Account Account
        {
            get
            {
                loadAccountReference();
                return account;
            }
            set
            {
                account = value;
                if (account != null)
                {
                    if (!DataSource.AccountEntities.Contains(account))
                        DataSource.AccountEntities.Add(account);
                    AccountID = account.AccountID;
                }
            }
        }

        #endregion

        private void loadTransactionReference()
        {
            if (DataSource != null)
            {
                TransactionController transactionController = new TransactionController(DataSource);
                transaction = transactionController.Get(TransactionID);
            }
        }
        private void loadAccountReference()
        {
            if (DataSource != null)
            {
                AccountController accountController = new AccountController(DataSource);
                account = accountController.Get(AccountID);
            }
        }
    }
}
