using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Source;
using Took1.Silverlight.LifeManager.Data.Controller;
using Took1.Silverlight.LifeManager.Data.Collection;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class Transaction : BaseModel
    {
        #region CLASSES
        public class TransactionTypes
        {
            public const int IN = 0;
            public const int OUT = 1;
        }
        public class TagNames
        {
            public const string TRANSFER = "#TRANSFER";
        }

        #endregion
        #region PRIVATE MEMBERS
        private AccountCollection accounts = null;
        private EventCollection events = null;
        private TransactionAccountCollection transactionAccounts = null;
        private Category category = null;

        #endregion
        #region PUBLIC PROPERTIES
        public int TransactionID { get; set; }
        public string Name { get; set; }
        public DateTime EntryTimeStamp { get; set; }
        /// <summary>
        /// Tipo de gasto (0= entrada; 1 = saida)
        /// </summary>
        public int Type { get; set; }
        public int CategoryID { get; set; }
        public string Tag { get; set; }

        public AccountCollection Accounts
        {
            get
            {
                loadAccounts();
                return accounts;
            }
            //set { accounts = value; }
        }
        public EventCollection Events
        {
            get
            {
                loadEvents();
                return events;
            }
            //set { events = value; }
        }
        public TransactionAccountCollection TransactionAccounts
        {
            get
            {
                loadTransactionAccounts();
                return transactionAccounts;
            }
            //set { transactionAccounts = value; }
        }
        public Category Category
        {
            get
            {
                loadCategory();
                return category;
            }
            set
            {
                category = value;
                if (DataSource != null)
                {
                    if (!DataSource.CategoryEntities.Contains(category))
                        DataSource.CategoryEntities.Add(category);
                    CategoryID = category.CategoryID;
                }
            }
        }

        #endregion

        public Transaction()
        {
            Name = string.Empty;
            Tag = string.Empty;
        }

        public void AddTag(string tag)
        {
            if (!Tag.Contains(tag))
                Tag += tag;
        }
        public bool CheckTag(string tag)
        {
            return Tag.Contains(tag);
        }

        private bool addAccount(Account account)
        {
            bool result = true;
            if (account != null)
            {
                if (account.AccountID == 0)
                    DataSource.AccountEntities.Add(account);
                bool add = (from el in DataSource.TransactionAccountEntities
                            where el.TransactionID == this.TransactionID & el.AccountID == account.AccountID
                            select el).Count() == 0;
                if (add)
                {
                    DataSource.TransactionAccountEntities.Add(new TransactionAccount
                    {
                        AccountID = account.AccountID,
                        TransactionID = this.TransactionID,
                    });
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        private bool addEvent(Event ev)
        {
            bool result = true;
            if (ev != null)
            {
                if (ev.EventID == 0)
                    DataSource.EventEntities.Add(ev);
                bool add = (from el in DataSource.TransactionEventEntities
                            where el.TransactionID == this.TransactionID & el.EventID == ev.EventID
                            select el).Count() == 0;
                if (add)
                {
                    DataSource.TransactionEventEntities.Add(new TransactionEvent
                    {
                        EventID = ev.EventID,
                        TransactionID = this.TransactionID
                    });
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        private bool addTransactionAccount(TransactionAccount transactionAccount)
        {
            bool result = true;
            if (transactionAccount != null)
            {
                if (transactionAccount.TransactionAccountID == 0)
                    DataSource.TransactionAccountEntities.Add(transactionAccount);
                //bool add = (from el in DataSource.TransactionTransactionAccountEntities
                //            where el.TransactionID == this.TransactionID & el.TransactionAccountID == transactionAccount.TransactionAccountID
                //            select el).Count() == 0;
                //if (add)
                //{
                //    DataSource.TransactionTransactionAccountEntities.Add(new TransactionTransactionAccount
                //    {
                //        TransactionAccountID = transactionAccount.TransactionAccountID,
                //        TransactionID = this.TransactionID
                //    });
                //}
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
                List<int> accountIDList = (from el in DataSource.TransactionAccountEntities
                                           where el.TransactionID == TransactionID
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
        private void loadCategory()
        {
            if (DataSource != null)
            {
                CategoryController categoryController = new CategoryController(DataSource);
                category = categoryController.Get(CategoryID);
            }
        }
        private void loadEvents()
        {
            if (DataSource != null)
            {
                List<int> eventIDList = (from el in DataSource.TransactionEventEntities
                                         where el.TransactionID == TransactionID
                                         select el.EventID).Distinct().ToList();
                events = new EventCollection();
                EventController evController = new EventController(DataSource);
                foreach (int eventID in eventIDList)
                {
                    Event ev = evController.Get(eventID);
                    if (ev != null)
                        events.Add(ev);
                }
                events.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(events_CollectionChanged);
            }
        }
        private void loadTransactionAccounts()
        {
            if (DataSource != null)
            {
                transactionAccounts = new TransactionAccountCollection();
                var items = (from el in DataSource.TransactionAccountEntities
                             where el.TransactionID == TransactionID
                             select el).Distinct();
                foreach (TransactionAccount transactionAccount in items)
                    transactionAccounts.Add(transactionAccount);
                transactionAccounts.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(transactionAccounts_CollectionChanged);
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
        private void events_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Event ev in e.NewItems)
                        addEvent(ev);
                    break;
                default:
                    break;
            }
        }
        private void transactionAccounts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (TransactionAccount transactionAccount in e.NewItems)
                        addTransactionAccount(transactionAccount);
                    break;
                default:
                    break;
            }

        }


        #endregion

    }
}
