using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Silverlight.LifeManager.Data.Controller;

namespace Took1.Silverlight.LifeManager.Data.Model
{
    public class TransactionEvent : BaseModel
    {
        #region PRIVATE MEMBERS
        private Transaction transaction = null;
        private Event ev = null;

        #endregion
        #region PUBLIC PROPERTIES
        public int TransactionEventID { get; set; }
        public int TransactionID { get; set; }
        public int EventID { get; set; }

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
        public Event Event
        {
            get
            {
                loadEventReference();
                return ev;
            }
            set
            {
                ev = value;
                if (value != null)
                {
                    if (!DataSource.EventEntities.Contains(ev))
                        DataSource.EventEntities.Add(ev);
                    EventID = ev.EventID;
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
        private void loadEventReference()
        {
            if (DataSource != null)
            {
                EventController eventController = new EventController(DataSource);
                ev = eventController.Get(EventID);
            }
        }

    }
}
