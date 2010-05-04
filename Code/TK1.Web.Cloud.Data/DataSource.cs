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
using System.Windows.Ria.Data;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections;
using Took1.Silverlight;
using Took1.Silverlight.LifeManager;
using System.Reflection;

namespace Took1.Web.Cloud.Data
{
    public class DataSource
    {
        #region EVENTS
        #region BASE
        public event EventHandler AsynchronousOperationStarted;
        protected void OnAsynchronousOperationStarted(EventArgs e)
        {
            EventHandler handler = AsynchronousOperationStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler AsynchronousOperationFinished;
        protected void OnAsynchronousOperationFinished(EventArgs e)
        {
            EventHandler handler = AsynchronousOperationFinished;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler Loaded;
        protected void OnLoaded(EventArgs e)
        {
            EventHandler handler = Loaded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler HasChangesStatusModified;
        private void OnHasChangesStatusModified(EventArgs e)
        {
            EventHandler handler = HasChangesStatusModified;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SubmitFailed;
        private void OnSubmitFailed(EventArgs e)
        {
            EventHandler handler = SubmitFailed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler RejectedChanges;
        private void OnRejectedChanges(EventArgs e)
        {
            EventHandler handler = RejectedChanges;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
        #region ENTITIES
        public event EventHandler AccountEntitiesChanged;
        private void OnAccountEntitiesChanged(EventArgs e)
        {
            EventHandler handler = AccountEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CategoryEntitiesChanged;
        private void OnCategoryEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CategoryEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ContextEntitiesChanged;
        private void OnContextEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ContextEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler EventEntitiesChanged;
        private void OnEventEntitiesChanged(EventArgs e)
        {
            EventHandler handler = EventEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MoneyCheckPointEntitiesChanged;
        private void OnMoneyCheckPointEntitiesChanged(EventArgs e)
        {
            EventHandler handler = MoneyCheckPointEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MoneyCheckPointAccountEntitiesChanged;
        private void OnMoneyCheckPointAccountEntitiesChanged(EventArgs e)
        {
            EventHandler handler = MoneyCheckPointAccountEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MoneyTransactionEntitiesChanged;
        private void OnMoneyTransactionEntitiesChanged(EventArgs e)
        {
            EventHandler handler = MoneyTransactionEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler MoneyTransactionAccountEntitiesChanged;
        private void OnMoneyTransactionAccountEntitiesChanged(EventArgs e)
        {
            EventHandler handler = MoneyTransactionAccountEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ContactInfoEntitiesChanged;
        private void OnContactInfoEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ContactInfoEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler LocalisationInfoEntitiesChanged;
        private void OnLocalisationInfoEntitiesChanged(EventArgs e)
        {
            EventHandler handler = LocalisationInfoEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler PersonEntitiesChanged;
        private void OnPersonEntitiesChanged(EventArgs e)
        {
            EventHandler handler = PersonEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserDataEntitiesChanged;
        private void OnUserDataEntitiesChanged(EventArgs e)
        {
            EventHandler handler = UserDataEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #endregion
        #region PRIVATE MEMBERS
        #region BASE
        private LifeManagerDomainContext domain;
        private DataState currentValue;
        private bool hasChanges = false;
        private string previousErrorMessage = string.Empty;

        DispatcherTimer watchDog;
        #endregion
        #region ENTITIES
        EntityList<Account> accountEntities;
        EntityList<Category> categoryEntities;
        EntityList<Context> contextEntities;
        EntityList<Event> eventEntities;
        EntityList<MoneyCheckPoint> moneyCheckPointEntities;
        EntityList<MoneyCheckPointAccount> moneyCheckPointAccountEntities;
        EntityList<MoneyTransaction> moneyTransactionEntities;
        EntityList<MoneyTransactionAccount> moneyTransactionAccountEntities;
        EntityList<MoneyTransactionEvent> moneyTransactionEventEntities;

        EntityList<ContactInfo> contactInfoEntities;
        EntityList<LocalisationInfo> localisationInfoEntities;
        EntityList<Person> personEntities;
        EntityList<UserData> userDataEntities;


        #endregion
        #region LOADING FLAGS
        //bool isLoadingClientEntities = false;
        //bool isLoadingClientGroupEntities = false;
        //bool isLoadingClientGroupResourceEntities = false;
        //bool isLoadingClientModelEntities = false;
        //bool isLoadingClientModelCommPortEntities = false;
        //bool isLoadingClientModelEventEntities = false;
        //bool isLoadingClientModelEventScriptEntities = false;
        //bool isLoadingClientModelSignalEntities = false;
        //bool isLoadingCodeActionEntities = false;
        //bool isLoadingCodeActionCollectionEntities = false;
        //bool isLoadingCodeActionCollectionStatementEntities = false;
        //bool isLoadingCodeActionInstanceEntities = false;
        //bool isLoadingCodeActionStatementEntities = false;
        //bool isLoadingCodeMethodEntities = false;
        //bool isLoadingCodeMethodHeaderEntities = false;
        //bool isLoadingCodeActionExpressionEntities = false;
        //bool isLoadingCodeActionExpressionValueEntities = false;
        //bool isLoadingDataColBitmapEntities = false;
        //bool isLoadingDataColGroupEntities = false;
        //bool isLoadingDncEntities = false;
        //bool isLoadingEventDetailEntities = false;
        //bool isLoadingEventTypeEntities = false;
        //bool isLoadingResourceEntities = false;
        //bool isLoadingSignalEntities = false;
        //bool isLoadingSignalBehaviorEntities = false;

        #endregion
        #endregion
        #region PUBLIC ACTIONS
        public Action<string> WriteOutput;
        #endregion
        #region PUBLIC PROPERTIES
        #region BASE
        public DataState CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }
        public LifeManagerDomainContext Domain
        {
            get { return domain; }
        }
        public bool HasChanges
        {
            set
            {
                hasChanges = value;
                OnHasChangesStatusModified(new EventArgs());
            }
            get { return hasChanges; }
        }
        public bool IsLoaded { get; set; }
        public bool IsLoading { get; set; }

        #endregion
        #region ENTITIES
        public EntityList<Account> AccountEntities
        {
            get
            {
                if (accountEntities == null & domain != null)
                {
                    if (domain != null)
                    {
                        LoadOperation<Account> loadOperation = domain.Load<Account>(domain.GetAccountQuery());
                        loadOperation.Completed += new EventHandler(loadOperation_Completed);
                        accountEntities = domain.Accounts;
                    }
                }
                return accountEntities;
            }
        }
        public EntityList<Category> CategoryEntities
        {
            get
            {
                if (categoryEntities == null & domain != null)
                {
                    LoadOperation<Category> loadOperation = domain.Load<Category>(domain.GetCategoryQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    categoryEntities = domain.Categories;
                }
                return categoryEntities;
            }
        }
        public EntityList<Context> ContextEntities
        {
            get
            {
                if (contextEntities == null & domain != null)
                {
                    if (domain != null)
                    {
                        LoadOperation<Context> loadOperation = domain.Load<Context>(domain.GetContextQuery());
                        loadOperation.Completed += new EventHandler(loadOperation_Completed);
                        contextEntities = domain.Contexts;
                    }
                }
                return contextEntities;
            }
        }
        public EntityList<Event> EventEntities
        {
            get
            {
                if (eventEntities == null & domain != null)
                {
                    LoadOperation<Event> loadOperation = domain.Load<Event>(domain.GetEventQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    eventEntities = domain.Events;
                }
                return eventEntities;
            }
        }
        public EntityList<MoneyCheckPoint> MoneyCheckPointEntities
        {
            get
            {
                if (moneyCheckPointEntities == null & domain != null)
                {
                    LoadOperation<MoneyCheckPoint> loadOperation = domain.Load<MoneyCheckPoint>(domain.GetMoneyCheckPointQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    moneyCheckPointEntities = domain.MoneyCheckPoints;
                }
                return moneyCheckPointEntities;
            }
        }
        public EntityList<MoneyCheckPointAccount> MoneyCheckPointAccountEntities
        {
            get
            {
                if (moneyCheckPointAccountEntities == null & domain != null)
                {
                    LoadOperation<MoneyCheckPointAccount> loadOperation = domain.Load<MoneyCheckPointAccount>(domain.GetMoneyCheckPointAccountQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    moneyCheckPointAccountEntities = domain.MoneyCheckPointAccounts;
                }
                return moneyCheckPointAccountEntities;
            }
        }
        public EntityList<MoneyTransaction> MoneyTransactionEntities
        {
            get
            {
                if (moneyTransactionEntities == null & domain != null)
                {
                    if (domain != null)
                    {
                        LoadOperation<MoneyTransaction> loadOperation = domain.Load<MoneyTransaction>(domain.GetMoneyTransactionQuery());
                        loadOperation.Completed += new EventHandler(loadOperation_Completed);
                        moneyTransactionEntities = domain.MoneyTransactions;
                    }
                }
                return moneyTransactionEntities;
            }
        }
        public EntityList<MoneyTransactionAccount> MoneyTransactionAccountEntities
        {
            get
            {
                if (moneyTransactionAccountEntities == null & domain != null)
                {
                    LoadOperation<MoneyTransactionAccount> loadOperation = domain.Load<MoneyTransactionAccount>(domain.GetMoneyTransactionAccountQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    moneyTransactionAccountEntities = domain.MoneyTransactionAccounts;
                }
                return moneyTransactionAccountEntities;
            }
        }
        public EntityList<MoneyTransactionEvent> MoneyTransactionEventEntities
        {
            get
            {
                if (moneyTransactionEventEntities == null & domain != null)
                {
                    LoadOperation<MoneyTransactionEvent> loadOperation = domain.Load<MoneyTransactionEvent>(domain.GetMoneyTransactionEventQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    moneyTransactionEventEntities = domain.MoneyTransactionEvents;
                }
                return moneyTransactionEventEntities;
            }
        }

        public EntityList<ContactInfo> ContactInfoEntities
        {
            get
            {
                if (contactInfoEntities == null & domain != null)
                {
                    LoadOperation<ContactInfo> loadOperation = domain.Load<ContactInfo>(domain.GetContactInfoQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    contactInfoEntities = domain.ContactInfos;
                }
                return contactInfoEntities;
            }
        }
        public EntityList<LocalisationInfo> LocalisationInfoEntities
        {
            get
            {
                if (localisationInfoEntities == null & domain != null)
                {
                    LoadOperation<LocalisationInfo> loadOperation = domain.Load<LocalisationInfo>(domain.GetLocalisationInfoQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    localisationInfoEntities = domain.LocalisationInfos;
                }
                return localisationInfoEntities;
            }
        }
        public EntityList<Person> PersonEntities
        {
            get
            {
                if (personEntities == null & domain != null)
                {
                    LoadOperation<Person> loadOperation = domain.Load<Person>(domain.GetPersonQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    personEntities = domain.Persons;
                }
                return personEntities;
            }
        }
        public EntityList<UserData> UserDataEntities
        {
            get
            {
                if (userDataEntities == null & domain != null)
                {
                    LoadOperation<UserData> loadOperation = domain.Load<UserData>(domain.GetUserDataQuery());
                    loadOperation.Completed += new EventHandler(loadOperation_Completed);
                    userDataEntities = domain.UserDatas;
                }
                return userDataEntities;
            }
        }

        #endregion

        #endregion

        public DataSource()
        {
            currentValue = new DataState();

            initialize();
        }

        public void Close()
        {
            if (WriteOutput != null)
                WriteOutput("Closing BaseDataSourceController");

        }
        public void Load(EntityList entitiList)
        {
            //NÃO HÁ necessidade carregamento manual, basta chamar propriedade pela primeira vez
        }
        public void RejectChanges()
        {
            if (domain != null)
            {
                if (domain.HasChanges)
                {
                    domain.RejectChanges();
                    OnRejectedChanges(new EventArgs());
                }
            }
        }
        public void Save()
        {
            Save(null, null);
            //if (domain != null)
            //{
            //    if (domain.HasChanges)
            //    {
            //        SubmitOperation submitOperation = domain.SubmitChanges();
            //        submitOperation.Completed += new EventHandler(submitOperation_Completed);
            //    }
            //}
        }
        public void Save(Action<SubmitOperation> submitOperationCompleted)
        {
            Save(submitOperationCompleted, null);
            //if (domain != null)
            //{
            //    if (domain.HasChanges)
            //    {
            //        SubmitOperation submitOperation = domain.SubmitChanges();
            //        submitOperation.Completed += new EventHandler(submitOperation_Completed);
            //    }
            //}
        }
        public void Save(Action<SubmitOperation> submitOperationCompleted, object userState)
        {
            if (domain != null)
            {
                if (domain.HasChanges)
                {
                    if (domain.IsSubmitting)
                    {
                        //Thread.SpinWait(10);
                        //Save(submitOperationCompleted, userState);
                    }
                    else
                    {
                        SubmitOperation submitOperation = domain.SubmitChanges(submitOperationCompleted, userState);
                        submitOperation.Completed += new EventHandler(submitOperation_Completed);
                    }
                }
            }
        }
        public void TriggerEntityCollectionEvent(Type type)
        {
            triggerEntityCollectionEvent(type);
        }

        private void addEntity(object entity)
        {
            if (entity != null)
            {
                //Type entityType = entity.GetType();
                //if (entityType == typeof(DataColBitmap))
                //{
                //    DataColBitmap dataColBitmap = entity as DataColBitmap;
                //    if (!DataColBitmapEntities.Contains(dataColBitmap))
                //        DataColBitmapEntities.Add(dataColBitmap);
                //}
                //if (entityType == typeof(DataColGroup))
                //{
                //    DataColGroup dataColGroup = entity as DataColGroup;
                //    if (!DataColGroupEntities.Contains(dataColGroup))
                //        DataColGroupEntities.Add(dataColGroup);
                //}

            }
        }
        private void initialize()
        {
            watchDog = new DispatcherTimer();
            watchDog.Tick += new EventHandler(watchDog_Tick);
            watchDog.Interval = TimeSpan.FromMilliseconds(100);
            watchDog.Start();

            openDomainServiceConnection();

        }
        private void openDomainServiceConnection()
        {
            string domainPath = @"http://{0}/Admin/DataService.axd/Numericon-DM-Web-Silverlight-Data-Ria-Web-DataDomainService/";
            string host = "localhost";
            string hostName = QueryStringManager.GetValue(AppQueryStrings.Host);
            if (hostName == null)
            {
                domain = new LifeManagerDomainContext();
            }
            else
            {
                host = hostName;
                host = string.Format(domainPath, host);

                Uri uri = new Uri(host, UriKind.Absolute);
                domain = new LifeManagerDomainContext(uri);
            }
            domain.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(domain_PropertyChanged);
        }
        private void saveEntity(object entity)
        {
            if (entity != null)
            {
                ICollection collection = entity as ICollection;
                if (collection != null)
                {
                    saveCollection(collection);
                }
                else
                {
                    //setReferences(entity as EntityObject);
                    //Type entityType = entity.GetType();
                    //if (entityType == typeof(ClientModelEventScript))
                    //    saveClientModelEventScript(entity);
                    //if (entityType == typeof(CodeActionExpressionValue))
                    //    saveCodeActionExpressionValue(entity);
                    //if (entityType == typeof(CodeActionInstance))
                    //    saveCodeActionInstance(entity);
                    //if (entityType == typeof(DataColBitmap))
                    //    saveDataColBitmap(entity);
                    //if (entityType == typeof(DataColGroup))
                    //    saveDataColGroup(entity);
                    //if (entityType == typeof(Dnc))
                    //    saveDnc(entity);
                    //if (entityType == typeof(Signal))
                    //    saveSignal(entity);
                }
            }
        }
        private void saveCollection(ICollection collection)
        {
            if (collection != null)
            {
                foreach (object obj in collection)
                {
                    saveEntity(obj);
                }
            }
        }

        private void setDataServiceEventHandlers()
        {
            //dataServiceClient.DeleteEntityCompleted += new EventHandler<DeleteEntityCompletedEventArgs>(dataServiceClient_DeleteEntityCompleted);

            //dataServiceClient.GetClientEntitiesCompleted += new EventHandler<Data.GetClientEntitiesCompletedEventArgs>(dataServiceClient_GetClientEntitiesCompleted);
            //dataServiceClient.GetClientGroupEntitiesCompleted += new EventHandler<Data.GetClientGroupEntitiesCompletedEventArgs>(dataServiceClient_GetClientGroupEntitiesCompleted);
            //dataServiceClient.GetClientGroupResourceEntitiesCompleted += new EventHandler<Data.GetClientGroupResourceEntitiesCompletedEventArgs>(dataServiceClient_GetClientGroupResourceEntitiesCompleted);
            //dataServiceClient.GetClientModelCommPortEntitiesCompleted += new EventHandler<Data.GetClientModelCommPortEntitiesCompletedEventArgs>(dataServiceClient_GetClientModelCommPortEntitiesCompleted);
            //dataServiceClient.GetClientModelEntitiesCompleted += new EventHandler<Data.GetClientModelEntitiesCompletedEventArgs>(dataServiceClient_GetClientModelEntitiesCompleted);
            //dataServiceClient.GetClientModelEventEntitiesCompleted += new EventHandler<Data.GetClientModelEventEntitiesCompletedEventArgs>(dataServiceClient_GetClientModelEventEntitiesCompleted);
            //dataServiceClient.GetClientModelEventScriptEntitiesCompleted += new EventHandler<Data.GetClientModelEventScriptEntitiesCompletedEventArgs>(dataServiceClient_GetClientModelEventScriptEntitiesCompleted);
            //dataServiceClient.GetClientModelSignalEntitiesCompleted += new EventHandler<Data.GetClientModelSignalEntitiesCompletedEventArgs>(dataServiceClient_GetClientModelSignalEntitiesCompleted);
            //dataServiceClient.GetCodeActionCollectionEntitiesCompleted+=new EventHandler<GetCodeActionCollectionEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionCollectionEntitiesCompleted);
            //dataServiceClient.GetCodeActionCollectionStatementEntitiesCompleted+=new EventHandler<GetCodeActionCollectionStatementEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionCollectionStatementEntitiesCompleted);
            //dataServiceClient.GetCodeActionEntitiesCompleted+=new EventHandler<GetCodeActionEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionEntitiesCompleted);
            //dataServiceClient.GetCodeActionExpressionEntitiesCompleted+=new EventHandler<GetCodeActionExpressionEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionExpressionEntitiesCompleted);
            //dataServiceClient.GetCodeActionExpressionValueEntitiesCompleted+=new EventHandler<GetCodeActionExpressionValueEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionExpressionValueEntitiesCompleted);
            //dataServiceClient.GetCodeActionInstanceEntitiesCompleted+=new EventHandler<GetCodeActionInstanceEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionInstanceEntitiesCompleted);
            //dataServiceClient.GetCodeActionStatementEntitiesCompleted+=new EventHandler<GetCodeActionStatementEntitiesCompletedEventArgs>(dataServiceClient_GetCodeActionStatementEntitiesCompleted);
            //dataServiceClient.GetCodeByNameCompleted+=new EventHandler<GetCodeByNameCompletedEventArgs>(dataServiceClient_GetCodeByNameCompleted);
            //dataServiceClient.GetCodeCompleted+=new EventHandler<GetCodeCompletedEventArgs>(dataServiceClient_GetCodeCompleted);
            //dataServiceClient.GetCodeMethodEntitiesCompleted+=new EventHandler<GetCodeMethodEntitiesCompletedEventArgs>(dataServiceClient_GetCodeMethodEntitiesCompleted);
            //dataServiceClient.GetCodeMethodHeaderEntitiesCompleted+=new EventHandler<GetCodeMethodHeaderEntitiesCompletedEventArgs>(dataServiceClient_GetCodeMethodHeaderEntitiesCompleted);
            //dataServiceClient.GetDataColBitmapEntitiesCompleted += new EventHandler<Data.GetDataColBitmapEntitiesCompletedEventArgs>(dataServiceClient_GetDataColBitmapEntitiesCompleted);
            //dataServiceClient.GetDataColGroupEntitiesCompleted += new EventHandler<Data.GetDataColGroupEntitiesCompletedEventArgs>(dataServiceClient_GetDataColGroupEntitiesCompleted);
            //dataServiceClient.GetDncEntitiesCompleted += new EventHandler<Data.GetDncEntitiesCompletedEventArgs>(dataServiceClient_GetDncEntitiesCompleted);
            //dataServiceClient.GetEventDetailEntitiesCompleted += new EventHandler<Data.GetEventDetailEntitiesCompletedEventArgs>(dataServiceClient_GetEventDetailEntitiesCompleted);
            //dataServiceClient.GetEventTypeEntitiesCompleted+=new EventHandler<GetEventTypeEntitiesCompletedEventArgs>(dataServiceClient_GetEventTypeEntitiesCompleted);
            //dataServiceClient.GetResourceEntitiesCompleted += new EventHandler<Data.GetResourceEntitiesCompletedEventArgs>(dataServiceClient_GetResourceEntitiesCompleted);
            //dataServiceClient.GetSignalEntitiesCompleted += new EventHandler<Data.GetSignalEntitiesCompletedEventArgs>(dataServiceClient_GetSignalEntitiesCompleted);
            //dataServiceClient.GetSignalBehaviorEntitiesCompleted += new EventHandler<Data.GetSignalBehaviorEntitiesCompletedEventArgs>(dataServiceClient_GetSignalBehaviorEntitiesCompleted);

            //dataServiceClient.SetClientModelEventScriptCompleted += new EventHandler<Data.SetClientModelEventScriptCompletedEventArgs>(dataServiceClient_SetClientModelEventScriptCompleted);
            //dataServiceClient.SetCodeActionExpressionValueCompleted += new EventHandler<SetCodeActionExpressionValueCompletedEventArgs>(dataServiceClient_SetCodeActionExpressionValueCompleted);
            //dataServiceClient.SetCodeActionInstanceCompleted += new EventHandler<SetCodeActionInstanceCompletedEventArgs>(dataServiceClient_SetCodeActionInstanceCompleted);
            //dataServiceClient.SetDataColBitmapCompleted += new EventHandler<Data.SetDataColBitmapCompletedEventArgs>(dataServiceClient_SetDataColBitmapCompleted);
            //dataServiceClient.SetDataColGroupCompleted += new EventHandler<SetDataColGroupCompletedEventArgs>(dataServiceClient_SetDataColGroupCompleted);
            //dataServiceClient.SetDncCompleted += new EventHandler<Data.SetDncCompletedEventArgs>(dataServiceClient_SetDncCompleted);
            //dataServiceClient.SetSignalCompleted += new EventHandler<Data.SetSignalCompletedEventArgs>(dataServiceClient_SetSignalCompleted);
        }
        private void triggerEntityCollectionEvent(Type type)
        {
            if (type != null)
            {
                string eventName = string.Format("On{0}EntitiesChanged", type.Name);

                MethodInfo method = (from el in this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                                     where el.Name == eventName
                                     select el).FirstOrDefault();
                if (method != null)
                {
                    method.Invoke(this, new object[] { new EventArgs() });
                }
            }
        }
        private void writeOutput(string message)
        {
            if (WriteOutput != null)
                WriteOutput(message);
        }

        #region DATA DOMAIN EVENT HANDLERS
        void domain_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HasChanges = domain.HasChanges;
        }
        void loadOperation_Completed(object sender, EventArgs e)
        {
            LoadOperation loadOperation = sender as LoadOperation;
            if (loadOperation != null)
            {
                triggerEntityCollectionEvent(loadOperation.EntityQuery.EntityType);
                if (loadOperation.HasError)
                {
                    string caption = "LoadOperationCompleted -> Error";
                    string message = string.Format("{0}", loadOperation.Error);
                    if (message != previousErrorMessage)
                    {
                        previousErrorMessage = message;
                        MessageBox.Show(message, caption, MessageBoxButton.OK);
                    }
                }
                else
                {
                    previousErrorMessage = string.Empty;
                }
            }
            hasChanges = domain.HasChanges;

        }
        void submitOperation_Completed(object sender, EventArgs e)
        {
            SubmitOperation submitOperation = sender as SubmitOperation;
            if (submitOperation != null)
            {
                //triggerEntityCollectionEvent(submitOperation.EntityQuery.EntityType);
                if (submitOperation.Error != null)
                {
                    OnSubmitFailed(new EventArgs());
                    string caption = "SubmitOperation_Completed -> Error";
                    string message = submitOperation.Error.Message;
                    if (submitOperation.EntitiesInError.Any())
                    {
                        message = string.Empty;
                        int i = 1;
                        foreach (Entity entityInError in submitOperation.EntitiesInError)
                        {
                            if (entityInError.Conflict != null)
                            {
                                EntityConflict conflict = entityInError.Conflict;
                                foreach (EntityConflictMember entityConflictMember in conflict.MemberConflicts)
                                {
                                    message += string.Format("{0}: Entity {1}, member '{2}' in conflict: Current: {3},Original: {4}, Store: {5}\r\n",
                                        i,
                                        entityInError.GetType().Name,
                                        entityConflictMember.PropertyName, entityConflictMember.CurrentValue,
                                        entityConflictMember.OriginalValue, entityConflictMember.StoreValue);
                                }
                            }
                            else if (entityInError.ValidationErrors.Any())
                            {
                                message += string.Format("{0}: {1}",
                                    i,
                                    entityInError.ValidationErrors.First().Message);
                            }
                            i++;
                        }
                    }
                    MessageBox.Show(message, caption, MessageBoxButton.OK);
                }
            }
            hasChanges = domain.HasChanges;

        }

        #endregion

        #region WATCHDOG
        void watchDog_Tick(object sender, EventArgs e)
        {
            if (domain.IsLoading | domain.IsSubmitting)
                OnAsynchronousOperationStarted(new EventArgs());
            else
                OnAsynchronousOperationFinished(new EventArgs());
        }

        #endregion

    }
}
