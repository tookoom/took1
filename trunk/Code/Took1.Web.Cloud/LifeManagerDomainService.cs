
namespace Took1.Web.Cloud
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Ria;
    using System.Web.Ria.Data;
    using System.Web.DomainServices;
    using System.Data;
    using System.Web.DomainServices.LinqToEntities;


    // Implements application logic using the TestDBEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    [EnableClientAccess()]
    public class LifeManagerDomainService : LinqToEntitiesDomainService<TestDBEntities>
    {

        #region ACCOUNT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<Account> GetAccount()
        {
            return this.Context.Account;
        }

        public void InsertAccount(Account account)
        {
            this.Context.AddToAccount(account);
        }

        public void UpdateAccount(Account currentAccount)
        {
            this.Context.AttachAsModified(currentAccount, this.ChangeSet.GetOriginal(currentAccount));
        }

        public void DeleteAccount(Account account)
        {
            if ((account.EntityState == EntityState.Detached))
            {
                this.Context.Attach(account);
            }
            this.Context.DeleteObject(account);
        }

        #endregion

        #region CATEGORY
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<Category> GetCategory()
        {
            return this.Context.Category;
        }

        public void InsertCategory(Category category)
        {
            this.Context.AddToCategory(category);
        }

        public void UpdateCategory(Category currentCategory)
        {
            this.Context.AttachAsModified(currentCategory, this.ChangeSet.GetOriginal(currentCategory));
        }

        public void DeleteCategory(Category category)
        {
            if ((category.EntityState == EntityState.Detached))
            {
                this.Context.Attach(category);
            }
            this.Context.DeleteObject(category);
        }

        #endregion

        #region CONTACT INFO
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<ContactInfo> GetContactInfo()
        {
            return this.Context.ContactInfo;
        }

        public void InsertContactInfo(ContactInfo contactInfo)
        {
            this.Context.AddToContactInfo(contactInfo);
        }

        public void UpdateContactInfo(ContactInfo currentContactInfo)
        {
            this.Context.AttachAsModified(currentContactInfo, this.ChangeSet.GetOriginal(currentContactInfo));
        }

        public void DeleteContactInfo(ContactInfo contactInfo)
        {
            if ((contactInfo.EntityState == EntityState.Detached))
            {
                this.Context.Attach(contactInfo);
            }
            this.Context.DeleteObject(contactInfo);
        }

        #endregion

        #region CONTEXT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<Context> GetContext()
        {
            return this.Context.Context;
        }

        public void InsertContext(Context context)
        {
            this.Context.AddToContext(context);
        }

        public void UpdateContext(Context currentContext)
        {
            this.Context.AttachAsModified(currentContext, this.ChangeSet.GetOriginal(currentContext));
        }

        public void DeleteContext(Context context)
        {
            if ((context.EntityState == EntityState.Detached))
            {
                this.Context.Attach(context);
            }
            this.Context.DeleteObject(context);
        }

        #endregion

        #region EVENT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<Event> GetEvent()
        {
            return this.Context.Event;
        }

        public void InsertEvent(Event @event)
        {
            this.Context.AddToEvent(@event);
        }

        public void UpdateEvent(Event currentEvent)
        {
            this.Context.AttachAsModified(currentEvent, this.ChangeSet.GetOriginal(currentEvent));
        }

        public void DeleteEvent(Event @event)
        {
            if ((@event.EntityState == EntityState.Detached))
            {
                this.Context.Attach(@event);
            }
            this.Context.DeleteObject(@event);
        }

        #endregion

        #region LOCALISATION INFO
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<LocalisationInfo> GetLocalisationInfo()
        {
            return this.Context.LocalisationInfo;
        }

        public void InsertLocalisationInfo(LocalisationInfo localisationInfo)
        {
            this.Context.AddToLocalisationInfo(localisationInfo);
        }

        public void UpdateLocalisationInfo(LocalisationInfo currentLocalisationInfo)
        {
            this.Context.AttachAsModified(currentLocalisationInfo, this.ChangeSet.GetOriginal(currentLocalisationInfo));
        }

        public void DeleteLocalisationInfo(LocalisationInfo localisationInfo)
        {
            if ((localisationInfo.EntityState == EntityState.Detached))
            {
                this.Context.Attach(localisationInfo);
            }
            this.Context.DeleteObject(localisationInfo);
        }

        #endregion

        #region MONEY CHECK POINT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<MoneyCheckPoint> GetMoneyCheckPoint()
        {
            return this.Context.MoneyCheckPoint;
        }

        public void InsertMoneyCheckPoint(MoneyCheckPoint moneyCheckPoint)
        {
            this.Context.AddToMoneyCheckPoint(moneyCheckPoint);
        }

        public void UpdateMoneyCheckPoint(MoneyCheckPoint currentMoneyCheckPoint)
        {
            this.Context.AttachAsModified(currentMoneyCheckPoint, this.ChangeSet.GetOriginal(currentMoneyCheckPoint));
        }

        public void DeleteMoneyCheckPoint(MoneyCheckPoint moneyCheckPoint)
        {
            if ((moneyCheckPoint.EntityState == EntityState.Detached))
            {
                this.Context.Attach(moneyCheckPoint);
            }
            this.Context.DeleteObject(moneyCheckPoint);
        }

        #endregion

        #region MONEY CHECK POINT ACCOUNT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<MoneyCheckPointAccount> GetMoneyCheckPointAccount()
        {
            return this.Context.MoneyCheckPointAccount;
        }

        public void InsertMoneyCheckPointAccount(MoneyCheckPointAccount moneyCheckPointAccount)
        {
            this.Context.AddToMoneyCheckPointAccount(moneyCheckPointAccount);
        }

        public void UpdateMoneyCheckPointAccount(MoneyCheckPointAccount currentMoneyCheckPointAccount)
        {
            this.Context.AttachAsModified(currentMoneyCheckPointAccount, this.ChangeSet.GetOriginal(currentMoneyCheckPointAccount));
        }

        public void DeleteMoneyCheckPointAccount(MoneyCheckPointAccount moneyCheckPointAccount)
        {
            if ((moneyCheckPointAccount.EntityState == EntityState.Detached))
            {
                this.Context.Attach(moneyCheckPointAccount);
            }
            this.Context.DeleteObject(moneyCheckPointAccount);
        }

        #endregion

        #region MONEY TRANSACTION
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<MoneyTransaction> GetMoneyTransaction()
        {
            return this.Context.MoneyTransaction;
        }

        public void InsertMoneyTransaction(MoneyTransaction moneyTransaction)
        {
            this.Context.AddToMoneyTransaction(moneyTransaction);
        }

        public void UpdateMoneyTransaction(MoneyTransaction currentMoneyTransaction)
        {
            this.Context.AttachAsModified(currentMoneyTransaction, this.ChangeSet.GetOriginal(currentMoneyTransaction));
        }

        public void DeleteMoneyTransaction(MoneyTransaction moneyTransaction)
        {
            if ((moneyTransaction.EntityState == EntityState.Detached))
            {
                this.Context.Attach(moneyTransaction);
            }
            this.Context.DeleteObject(moneyTransaction);
        }

        #endregion

        #region MONEY TRANSACTION ACCOUNT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<MoneyTransactionAccount> GetMoneyTransactionAccount()
        {
            return this.Context.MoneyTransactionAccount;
        }

        public void InsertMoneyTransactionAccount(MoneyTransactionAccount moneyTransactionAccount)
        {
            this.Context.AddToMoneyTransactionAccount(moneyTransactionAccount);
        }

        public void UpdateMoneyTransactionAccount(MoneyTransactionAccount currentMoneyTransactionAccount)
        {
            this.Context.AttachAsModified(currentMoneyTransactionAccount, this.ChangeSet.GetOriginal(currentMoneyTransactionAccount));
        }

        public void DeleteMoneyTransactionAccount(MoneyTransactionAccount moneyTransactionAccount)
        {
            if ((moneyTransactionAccount.EntityState == EntityState.Detached))
            {
                this.Context.Attach(moneyTransactionAccount);
            }
            this.Context.DeleteObject(moneyTransactionAccount);
        }

        #endregion

        #region MONEY TRANSACTION EVENT
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<MoneyTransactionEvent> GetMoneyTransactionEvent()
        {
            return this.Context.MoneyTransactionEvent;
        }

        public void InsertMoneyTransactionEvent(MoneyTransactionEvent moneyTransactionEvent)
        {
            this.Context.AddToMoneyTransactionEvent(moneyTransactionEvent);
        }

        public void UpdateMoneyTransactionEvent(MoneyTransactionEvent currentMoneyTransactionEvent)
        {
            this.Context.AttachAsModified(currentMoneyTransactionEvent, this.ChangeSet.GetOriginal(currentMoneyTransactionEvent));
        }

        public void DeleteMoneyTransactionEvent(MoneyTransactionEvent moneyTransactionEvent)
        {
            if ((moneyTransactionEvent.EntityState == EntityState.Detached))
            {
                this.Context.Attach(moneyTransactionEvent);
            }
            this.Context.DeleteObject(moneyTransactionEvent);
        }

        #endregion

        #region PERSON
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<Person> GetPerson()
        {
            return this.Context.Person;
        }

        public void InsertPerson(Person person)
        {
            this.Context.AddToPerson(person);
        }

        public void UpdatePerson(Person currentPerson)
        {
            this.Context.AttachAsModified(currentPerson, this.ChangeSet.GetOriginal(currentPerson));
        }

        public void DeletePerson(Person person)
        {
            if ((person.EntityState == EntityState.Detached))
            {
                this.Context.Attach(person);
            }
            this.Context.DeleteObject(person);
        }

        #endregion

        #region USER DATA
        // TODO: Consider
        // 1. Adding parameters to this method and constraining returned results, and/or
        // 2. Adding query methods taking different parameters.
        public IQueryable<UserData> GetUserData()
        {
            return this.Context.UserData;
        }

        public void InsertUserData(UserData userData)
        {
            this.Context.AddToUserData(userData);
        }

        public void UpdateUserData(UserData currentUserData)
        {
            this.Context.AttachAsModified(currentUserData, this.ChangeSet.GetOriginal(currentUserData));
        }

        public void DeleteUserData(UserData userData)
        {
            if ((userData.EntityState == EntityState.Detached))
            {
                this.Context.Attach(userData);
            }
            this.Context.DeleteObject(userData);
        }

        #endregion


        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<Account> GetAccount()
        //{
        //    return this.Context.Account;
        //}

        //public void InsertAccount(Account account)
        //{
        //    this.Context.AddToAccount(account);
        //}

        //public void UpdateAccount(Account currentAccount)
        //{
        //    this.Context.AttachAsModified(currentAccount, this.ChangeSet.GetOriginal(currentAccount));
        //}

        //public void DeleteAccount(Account account)
        //{
        //    if ((account.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(account);
        //    }
        //    this.Context.DeleteObject(account);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<Category> GetCategory()
        //{
        //    return this.Context.Category;
        //}

        //public void InsertCategory(Category category)
        //{
        //    this.Context.AddToCategory(category);
        //}

        //public void UpdateCategory(Category currentCategory)
        //{
        //    this.Context.AttachAsModified(currentCategory, this.ChangeSet.GetOriginal(currentCategory));
        //}

        //public void DeleteCategory(Category category)
        //{
        //    if ((category.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(category);
        //    }
        //    this.Context.DeleteObject(category);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<ContactInfo> GetContactInfo()
        //{
        //    return this.Context.ContactInfo;
        //}

        //public void InsertContactInfo(ContactInfo contactInfo)
        //{
        //    this.Context.AddToContactInfo(contactInfo);
        //}

        //public void UpdateContactInfo(ContactInfo currentContactInfo)
        //{
        //    this.Context.AttachAsModified(currentContactInfo, this.ChangeSet.GetOriginal(currentContactInfo));
        //}

        //public void DeleteContactInfo(ContactInfo contactInfo)
        //{
        //    if ((contactInfo.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(contactInfo);
        //    }
        //    this.Context.DeleteObject(contactInfo);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<Context> GetContext()
        //{
        //    return this.Context.Context;
        //}

        //public void InsertContext(Context context)
        //{
        //    this.Context.AddToContext(context);
        //}

        //public void UpdateContext(Context currentContext)
        //{
        //    this.Context.AttachAsModified(currentContext, this.ChangeSet.GetOriginal(currentContext));
        //}

        //public void DeleteContext(Context context)
        //{
        //    if ((context.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(context);
        //    }
        //    this.Context.DeleteObject(context);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<Event> GetEvent()
        //{
        //    return this.Context.Event;
        //}

        //public void InsertEvent(Event @event)
        //{
        //    this.Context.AddToEvent(@event);
        //}

        //public void UpdateEvent(Event currentEvent)
        //{
        //    this.Context.AttachAsModified(currentEvent, this.ChangeSet.GetOriginal(currentEvent));
        //}

        //public void DeleteEvent(Event @event)
        //{
        //    if ((@event.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(@event);
        //    }
        //    this.Context.DeleteObject(@event);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<LocalisationInfo> GetLocalisationInfo()
        //{
        //    return this.Context.LocalisationInfo;
        //}

        //public void InsertLocalisationInfo(LocalisationInfo localisationInfo)
        //{
        //    this.Context.AddToLocalisationInfo(localisationInfo);
        //}

        //public void UpdateLocalisationInfo(LocalisationInfo currentLocalisationInfo)
        //{
        //    this.Context.AttachAsModified(currentLocalisationInfo, this.ChangeSet.GetOriginal(currentLocalisationInfo));
        //}

        //public void DeleteLocalisationInfo(LocalisationInfo localisationInfo)
        //{
        //    if ((localisationInfo.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(localisationInfo);
        //    }
        //    this.Context.DeleteObject(localisationInfo);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<MoneyTransaction> GetMoneyTransaction()
        //{
        //    return this.Context.MoneyTransaction;
        //}

        //public void InsertMoneyTransaction(MoneyTransaction moneyTransaction)
        //{
        //    this.Context.AddToMoneyTransaction(moneyTransaction);
        //}

        //public void UpdateMoneyTransaction(MoneyTransaction currentMoneyTransaction)
        //{
        //    this.Context.AttachAsModified(currentMoneyTransaction, this.ChangeSet.GetOriginal(currentMoneyTransaction));
        //}

        //public void DeleteMoneyTransaction(MoneyTransaction moneyTransaction)
        //{
        //    if ((moneyTransaction.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(moneyTransaction);
        //    }
        //    this.Context.DeleteObject(moneyTransaction);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<MoneyTransactionAccount> GetMoneyTransactionAccount()
        //{
        //    return this.Context.MoneyTransactionAccount;
        //}

        //public void InsertMoneyTransactionAccount(MoneyTransactionAccount moneyTransactionAccount)
        //{
        //    this.Context.AddToMoneyTransactionAccount(moneyTransactionAccount);
        //}

        //public void UpdateMoneyTransactionAccount(MoneyTransactionAccount currentMoneyTransactionAccount)
        //{
        //    this.Context.AttachAsModified(currentMoneyTransactionAccount, this.ChangeSet.GetOriginal(currentMoneyTransactionAccount));
        //}

        //public void DeleteMoneyTransactionAccount(MoneyTransactionAccount moneyTransactionAccount)
        //{
        //    if ((moneyTransactionAccount.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(moneyTransactionAccount);
        //    }
        //    this.Context.DeleteObject(moneyTransactionAccount);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<MoneyTransactionEvent> GetMoneyTransactionEvent()
        //{
        //    return this.Context.MoneyTransactionEvent;
        //}

        //public void InsertMoneyTransactionEvent(MoneyTransactionEvent moneyTransactionEvent)
        //{
        //    this.Context.AddToMoneyTransactionEvent(moneyTransactionEvent);
        //}

        //public void UpdateMoneyTransactionEvent(MoneyTransactionEvent currentMoneyTransactionEvent)
        //{
        //    this.Context.AttachAsModified(currentMoneyTransactionEvent, this.ChangeSet.GetOriginal(currentMoneyTransactionEvent));
        //}

        //public void DeleteMoneyTransactionEvent(MoneyTransactionEvent moneyTransactionEvent)
        //{
        //    if ((moneyTransactionEvent.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(moneyTransactionEvent);
        //    }
        //    this.Context.DeleteObject(moneyTransactionEvent);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<Person> GetPerson()
        //{
        //    return this.Context.Person;
        //}

        //public void InsertPerson(Person person)
        //{
        //    this.Context.AddToPerson(person);
        //}

        //public void UpdatePerson(Person currentPerson)
        //{
        //    this.Context.AttachAsModified(currentPerson, this.ChangeSet.GetOriginal(currentPerson));
        //}

        //public void DeletePerson(Person person)
        //{
        //    if ((person.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(person);
        //    }
        //    this.Context.DeleteObject(person);
        //}

        //// TODO: Consider
        //// 1. Adding parameters to this method and constraining returned results, and/or
        //// 2. Adding query methods taking different parameters.
        //public IQueryable<UserData> GetUserData()
        //{
        //    return this.Context.UserData;
        //}

        //public void InsertUserData(UserData userData)
        //{
        //    this.Context.AddToUserData(userData);
        //}

        //public void UpdateUserData(UserData currentUserData)
        //{
        //    this.Context.AttachAsModified(currentUserData, this.ChangeSet.GetOriginal(currentUserData));
        //}

        //public void DeleteUserData(UserData userData)
        //{
        //    if ((userData.EntityState == EntityState.Detached))
        //    {
        //        this.Context.Attach(userData);
        //    }
        //    this.Context.DeleteObject(userData);
        //}
    }
}


