using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Model;
using System.Xml.Linq;
using System.Xml;
using Took1;
using Took1.Silverlight.Diagnostics;
using Took1.Silverlight.Xml;
using System.IO;
using Took1.Silverlight.LifeManager.Data.Collection;
using System.Collections.Specialized;
using Took1.Silverlight.LifeManager.Data.Controller;

namespace Took1.Silverlight.LifeManager.Data.Source
{
    public class DataSource
    {
        #region EVENTS

        public event EventHandler AccountEntitiesChanged;
        private void OnAccountEntitiesChanged(EventArgs e)
        {
            EventHandler handler = AccountEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler TransactionAccountEntitiesChanged;
        private void OnTransactionAccountEntitiesChanged(EventArgs e)
        {
            EventHandler handler = TransactionAccountEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 
        #endregion
        #region PRIVATE MEMBERS
        ErrorManager errorManager = new ErrorManager();

        #endregion
        #region PUBLIC PROPERTIES
        public DataSourceInfo Info { get; set; }
        public DataState CurrentValue { get; set; }

        public AccountCollection AccountEntities { get; set; }
        public CategoryCollection CategoryEntities { get; set; }
        public CheckPointCollection CheckPointEntities { get; set; }
        public CheckPointAccountCollection CheckPointAccountEntities { get; set; }
        public EventCollection EventEntities { get; set; }
        public TransactionCollection TransactionEntities { get; set; }

        public TransactionAccountCollection TransactionAccountEntities { get; set; }
        public TransactionEventCollection TransactionEventEntities { get; set; }

        public SeqGeneratorCollection SeqGeneratorEntities { get; set; }
	    #endregion        
        
        public DataSource() 
        {
            initialize();
        }

        private void initialize()
        {
            Info = new DataSourceInfo();
            CurrentValue= new DataState();

            AccountEntities = new AccountCollection();
            AccountEntities.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(AccountList_CollectionChanged);
            
            CategoryEntities = new CategoryCollection();
            CategoryEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(CategoryList_CollectionChanged);

            CheckPointEntities = new CheckPointCollection();
            CheckPointEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(CheckPointEntities_CollectionChanged);

            CheckPointAccountEntities = new CheckPointAccountCollection();
            CheckPointAccountEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(CheckPointAccountEntities_CollectionChanged);

            EventEntities = new EventCollection();
            EventEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(EventList_CollectionChanged);

            TransactionEntities = new TransactionCollection();
            TransactionEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(TransactionList_CollectionChanged);

            TransactionAccountEntities = new TransactionAccountCollection();
            TransactionAccountEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(TransactionAccountList_CollectionChanged);
            
            TransactionEventEntities = new TransactionEventCollection();
            TransactionEventEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(TransactionEventList_CollectionChanged);

            SeqGeneratorEntities = new SeqGeneratorCollection();
            SeqGeneratorEntities.CollectionChanged += new NotifyCollectionChangedEventHandler(SeqGeneratorList_CollectionChanged);
        }

        #region EVENT HANDLERS
        void AccountList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Account account in e.NewItems)
                    {
                        if (account != null)
                        {
                            if (account.DataSource == null)
                                account.DataSource = this;
                            if (account.AccountID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.Account);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.AccountEntities
                                                  orderby el.AccountID descending
                                                  select el.AccountID).FirstOrDefault();
                                    account.AccountID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.Account, lastID + 1);
                                }
                                else
                                {
                                    account.AccountID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
            OnAccountEntitiesChanged(new EventArgs());
        }
        void CategoryList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Category category in e.NewItems)
                    {
                        if (category != null)
                        {
                            if (category.DataSource == null)
                                category.DataSource = this;
                            if (category.CategoryID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.Category);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.CategoryEntities
                                                  orderby el.CategoryID descending
                                                  select el.CategoryID).FirstOrDefault();
                                    category.CategoryID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.Category, lastID + 1);
                                }
                                else
                                {
                                    category.CategoryID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void CheckPointEntities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (CheckPoint checkPoint in e.NewItems)
                    {
                        if (checkPoint != null)
                        {
                            if (checkPoint.DataSource == null)
                                checkPoint.DataSource = this;
                            if (checkPoint.CheckPointID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.CheckPoint);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.CheckPointEntities
                                                  orderby el.CheckPointID descending
                                                  select el.CheckPointID).FirstOrDefault();
                                    checkPoint.CheckPointID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.CheckPoint, lastID + 1);
                                }
                                else
                                {
                                    checkPoint.CheckPointID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void CheckPointAccountEntities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (CheckPointAccount checkPointAccount in e.NewItems)
                    {
                        if (checkPointAccount != null)
                        {
                            if (checkPointAccount.DataSource == null)
                                checkPointAccount.DataSource = this;
                            if (checkPointAccount.CheckPointAccountID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.CheckPointAccount);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.CheckPointAccountEntities
                                                  orderby el.CheckPointAccountID descending
                                                  select el.CheckPointAccountID).FirstOrDefault();
                                    checkPointAccount.CheckPointAccountID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.CheckPointAccount, lastID + 1);
                                }
                                else
                                {
                                    checkPointAccount.CheckPointAccountID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void EventList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Event ev in e.NewItems)
                    {
                        if (ev != null)
                        {
                            if (ev.DataSource == null)
                                ev.DataSource = this;
                            if (ev.EventID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.Event);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.EventEntities
                                                  orderby el.EventID descending
                                                  select el.EventID).FirstOrDefault();
                                    ev.EventID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.Event, lastID + 1);
                                }
                                else
                                {
                                    ev.EventID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void TransactionList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Transaction transaction in e.NewItems)
                    {
                        if (transaction != null)
                        {
                            if (transaction.DataSource == null)
                                transaction.DataSource = this;
                            if (transaction.TransactionID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.Transaction);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.TransactionEntities
                                                  orderby el.TransactionID descending
                                                  select el.TransactionID).FirstOrDefault();
                                    transaction.TransactionID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.Transaction, lastID + 1);
                                }
                                else
                                {
                                    transaction.TransactionID = nextID;
                                }
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void TransactionAccountList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TransactionAccount transactionAccount in e.NewItems)
                    {
                        if (transactionAccount != null)
                        {
                            if (transactionAccount.DataSource == null)
                                transactionAccount.DataSource = this;
                            if (transactionAccount.TransactionAccountID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.TransactionAccount);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.TransactionAccountEntities
                                                  orderby el.TransactionAccountID descending
                                                  select el.TransactionAccountID).FirstOrDefault();
                                    transactionAccount.TransactionAccountID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.TransactionAccount, lastID + 1);
                                }
                                else
                                {
                                    transactionAccount.TransactionAccountID = nextID;
                                }
                            }
                            //transactionAccount.loadEventReference();
                            //transactionAccount.loadTransactionReference();
                            //Account ev = transactionAccount.Account;
                            //Transaction ev = transactionAccount.Transaction;
                            if (!this.AccountEntities.Contains(transactionAccount.Account))
                                this.AccountEntities.Add(transactionAccount.Account);
                            if (!this.TransactionEntities.Contains(transactionAccount.Transaction))
                                this.TransactionEntities.Add(transactionAccount.Transaction);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
            OnTransactionAccountEntitiesChanged(new EventArgs());
        }
        void TransactionEventList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TransactionEvent transactionEvent in e.NewItems)
                    {
                        if (transactionEvent != null)
                        {
                            if (transactionEvent.DataSource == null)
                                transactionEvent.DataSource = this;
                            if (transactionEvent.TransactionEventID == 0)
                            {
                                SeqGeneratorController seqGeneratorController = new SeqGeneratorController(this);
                                int nextID = seqGeneratorController.GetNextValue(XmlDataSourceNames.TransactionEvent);
                                if (nextID == 0)
                                {
                                    int lastID = (from el in this.TransactionEventEntities
                                                  orderby el.TransactionEventID descending
                                                  select el.TransactionEventID).FirstOrDefault();
                                    transactionEvent.TransactionEventID = lastID + 1;
                                    seqGeneratorController.Add(XmlDataSourceNames.TransactionEvent, lastID + 1);
                                }
                                else
                                {
                                    transactionEvent.TransactionEventID = nextID;
                                }
                            }
                            if (!this.EventEntities.Contains(transactionEvent.Event))
                                this.EventEntities.Add(transactionEvent.Event);
                            if (!this.TransactionEntities.Contains(transactionEvent.Transaction))
                                this.TransactionEntities.Add(transactionEvent.Transaction);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }
        void SeqGeneratorList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (SeqGenerator seqGenerator in e.NewItems)
                    {
                        if (seqGenerator != null)
                        {
                            if (seqGenerator.DataSource == null)
                                seqGenerator.DataSource = this;
                            if (seqGenerator.SeqGeneratorID == 0)
                            {
                                int lastSeqGeneratorID = (from el in this.SeqGeneratorEntities
                                                          orderby el.SeqGeneratorID descending
                                                          select el.SeqGeneratorID).FirstOrDefault();
                                seqGenerator.SeqGeneratorID = lastSeqGeneratorID + 1;
                            }
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                default:
                    break;
            }
        }

    	#endregion    
    }
}
