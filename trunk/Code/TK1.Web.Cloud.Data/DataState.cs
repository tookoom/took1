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

namespace Took1.Web.Cloud.Data
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

        public event EventHandler ContextChanged;
        private void OnContextChanged(EventArgs e)
        {
            EventHandler handler = ContextChanged;
            if (handler != null)
                handler(this, e);
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

        public event EventHandler MoneyCheckPointChanged;
        private void OnMoneyCheckPointChanged(EventArgs e)
        {
            EventHandler handler = MoneyCheckPointChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler MoneyCheckPointAccountChanged;
        private void OnMoneyCheckPointAccountChanged(EventArgs e)
        {
            EventHandler handler = MoneyCheckPointAccountChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler MoneyTransactionChanged;
        private void OnMoneyTransactionChanged(EventArgs e)
        {
            EventHandler handler = MoneyTransactionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MoneyTransactionAccountChanged;
        private void OnMoneyTransactionAccountChanged(EventArgs e)
        {
            EventHandler handler = MoneyTransactionAccountChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MoneyTransactionEventChanged;
        private void OnMoneyTransactionEventChanged(EventArgs e)
        {
            EventHandler handler = MoneyTransactionEventChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ContactInfoChanged;
        private void OnContactInfoChanged(EventArgs e)
        {
            EventHandler handler = ContactInfoChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler LocalisationInfoChanged;
        private void OnLocalisationInfoChanged(EventArgs e)
        {
            EventHandler handler = LocalisationInfoChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler PersonChanged;
        private void OnPersonChanged(EventArgs e)
        {
            EventHandler handler = PersonChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler UserDataChanged;
        private void OnUserDataChanged(EventArgs e)
        {
            EventHandler handler = UserDataChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
        #region PRIVATE MEMBERS
        private Account account;
        private Category category;
        private Context context;
        private Event ev;
        private MoneyCheckPoint moneyCheckPoint;
        private MoneyCheckPointAccount moneyCheckPointAccount;
        private MoneyTransaction moneyTransaction;
        private MoneyTransactionAccount moneyTransactionAccount;
        private MoneyTransactionEvent moneyTransactionEvent;
        
        private ContactInfo contactInfo;
        private LocalisationInfo localisationInfo;
        private Person person;
        private UserData userData;

        
        

        #endregion
        #region PUBLIC PROPERTIES
        public Account Account
        {
            get { return account; }
            set { account = value; }
        }
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }
        public Context Context
        {
            get { return context; }
            set { context = value; }
        }
        public Event Event
        {
            get { return ev; }
            set { ev = value; }
        }
        public MoneyCheckPoint MoneyCheckPoint
        {
            get { return moneyCheckPoint; }
            set { moneyCheckPoint = value; }
        }
        public MoneyCheckPointAccount MoneyCheckPointAccount
        {
            get { return moneyCheckPointAccount; }
            set { moneyCheckPointAccount = value; }
        }
        public MoneyTransaction MoneyTransaction
        {
            get { return moneyTransaction; }
            set { moneyTransaction = value; }
        }
        public MoneyTransactionAccount MoneyTransactionAccount
        {
            get { return moneyTransactionAccount; }
            set { moneyTransactionAccount = value; }
        }
        public MoneyTransactionEvent MoneyTransactionEvent
        {
            get { return moneyTransactionEvent; }
            set { moneyTransactionEvent = value; }
        }

        public ContactInfo ContactInfo
        {
            get { return contactInfo; }
            set { contactInfo = value; }
        }
        public LocalisationInfo LocalisationInfo
        {
            get { return localisationInfo; }
            set { localisationInfo = value; }
        }
        public Person Person
        {
            get { return person; }
            set { person = value; }
        }
        public UserData UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        #endregion
    }
}
