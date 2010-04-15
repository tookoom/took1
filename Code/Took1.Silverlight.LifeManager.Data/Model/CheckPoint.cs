using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Took1.Silverlight.LifeManager.Data.Collection;
using System.Collections.Generic;
using Took1.Silverlight.LifeManager.Data.Controller;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class CheckPoint :  BaseModel
    {
        #region CLASSES
        public class CheckPointTypes
        {
            public const int WEEK = 0;
            public const int MONTH = 1;
            public const int YEAR = 2;
        }

        #endregion
        #region PRIVATE MEMBERS
        private AccountCollection accounts = null;
        #endregion
        #region PUBLIC PROPERTIES
        public int CheckPointID { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }

        public AccountCollection Accounts
        {
            get
            {
                loadAccounts();
                return accounts;
            }
            //set { accounts = value; }
        }
        #endregion

        public CheckPoint()
        {
            Name = string.Empty;
        }

        private bool addAccount(Account account)
        {
            bool result = true;
            if (account != null)
            {
                if (account.AccountID == 0)
                    DataSource.AccountEntities.Add(account);
                bool add = (from el in DataSource.CheckPointAccountEntities
                            where el.CheckPointID == this.CheckPointID & el.AccountID == account.AccountID
                            select el).Count() == 0;
                if (add)
                {
                    DataSource.CheckPointAccountEntities.Add(new CheckPointAccount
                    {
                        AccountID = account.AccountID,
                        CheckPointID = this.CheckPointID
                    });
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        private void loadAccounts()
        {
            if (DataSource != null)
            {
                List<int> accountIDList = (from el in DataSource.CheckPointAccountEntities
                                           where el.CheckPointID == CheckPointID
                                           select el.AccountID).Distinct().ToList();
                accounts = new AccountCollection();
                AccountController accountController = new AccountController(DataSource);
                foreach (int accountID in accountIDList)
                {
                    Account account = accountController.Get(accountID);
                    if (account != null)
                        accounts.Add(account);
                }
                accounts.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(accounts_CollectionChanged);
            }
        }

        #region EVENT HANDLERS
        private void accounts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Account account in e.NewItems)
                        addAccount(account);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
