using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Data.Entity;
using Took1.Data.Entity.Model;
using System.Reflection;
using Took1.Data.Presentation;

namespace Took1.Data.Entity.Source
{
    public class DataContext
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

        //public event OutputMessageCreatedEventHandler MessageCreated;
        //private void OnMessageCreated(OutputEventArgs e)
        //{
        //    OutputMessageCreatedEventHandler handler = MessageCreated;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}

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
        public event EventHandler ClientEntitiesChanged;
        private void OnClientEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientGroupEntitiesChanged;
        private void OnClientGroupEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientGroupEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientGroupResourceEntitiesChanged;
        private void OnClientGroupResourceEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientGroupResourceEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientModelCommPortEntitiesChanged;
        private void OnClientModelCommPortEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientModelCommPortEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientModelSignalEntitiesChanged;
        private void OnClientModelSignalEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientModelSignalEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientModelEntitiesChanged;
        private void OnClientModelEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientModelEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientModelEventEntitiesChanged;
        private void OnClientModelEventEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientModelEventEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ClientModelEventScriptEntitiesChanged;
        private void OnClientModelEventScriptEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ClientModelEventScriptEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler CodeMethodEntitiesChanged;
        private void OnCodeMethodEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeMethodEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler CodeActionEntitiesChanged;
        private void OnCodeActionEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionCollectionEntitiesChanged;
        private void OnCodeActionCollectionEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionCollectionEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionCollectionStatementEntitiesChanged;
        private void OnCodeActionCollectionStatementEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionCollectionStatementEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionInstanceEntitiesChanged;
        private void OnCodeActionInstanceEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionInstanceEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionStatementEntitiesChanged;
        private void OnCodeActionStatementEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionStatementEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeMethodHeaderEntitiesChanged;
        private void OnCodeMethodHeaderEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeMethodHeaderEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionExpressionEntitiesChanged;
        private void OnCodeActionExpressionEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionExpressionEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionExpressionValueEntitiesChanged;
        private void OnCodeActionExpressionValueEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeActionExpressionValueEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeTypeEntitiesChanged;
        private void OnCodeTypeEntitiesChanged(EventArgs e)
        {
            EventHandler handler = CodeTypeEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DataColBitmapEntitiesChanged;
        private void OnDataColBitmapEntitiesChanged(EventArgs e)
        {
            EventHandler handler = DataColBitmapEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler DataColGroupEntitiesChanged;
        private void OnDataColGroupEntitiesChanged(EventArgs e)
        {
            EventHandler handler = DataColGroupEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler DncEntitiesChanged;
        private void OnDncEntitiesChanged(EventArgs e)
        {
            EventHandler handler = DncEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DomainEntitiesChanged;
        private void OnDomainEntitiesChanged(EventArgs e)
        {
            EventHandler handler = DomainEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler EventDetailEntitiesChanged;
        private void OnEventDetailEntitiesChanged(EventArgs e)
        {
            EventHandler handler = EventDetailEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler EventTypeEntitiesChanged;
        private void OnEventTypeEntitiesChanged(EventArgs e)
        {
            EventHandler handler = EventTypeEntitiesChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ResourceEntitiesChanged;
        private void OnResourceEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ResourceEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShiftEntitiesChanged;
        private void OnShiftEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ShiftEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShiftDataEntitiesChanged;
        private void OnShiftDataEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ShiftDataEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShiftValidityEntitiesChanged;
        private void OnShiftValidityEntitiesChanged(EventArgs e)
        {
            EventHandler handler = ShiftValidityEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SignalEntitiesChanged;
        private void OnSignalEntitiesChanged(EventArgs e)
        {
            EventHandler handler = SignalEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SignalBehaviorEntitiesChanged;
        private void OnSignalBehaviorEntitiesChanged(EventArgs e)
        {
            EventHandler handler = SignalBehaviorEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler StatusDataEntitiesChanged;
        private void OnStatusDataEntitiesChanged(EventArgs e)
        {
            EventHandler handler = StatusDataEntitiesChanged;
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

        public event EventHandler UserDataGroupEntitiesChanged;
        private void OnUserDataGroupEntitiesChanged(EventArgs e)
        {
            EventHandler handler = UserDataGroupEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserGroupEntitiesChanged;
        private void OnUserGroupEntitiesChanged(EventArgs e)
        {
            EventHandler handler = UserGroupEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserGroupResourceEntitiesChanged;
        private void OnUserGroupResourceEntitiesChanged(EventArgs e)
        {
            EventHandler handler = UserGroupResourceEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserResourceEntitiesChanged;
        private void OnUserResourceEntitiesChanged(EventArgs e)
        {
            EventHandler handler = UserResourceEntitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #endregion
        #region PRIVATE MEMBERS
        #region BASE
        private DataContextState currentValue;
        private bool hasChanges = false;
        private string guid = string.Empty;
        #endregion
        #region ENTITIES
        EntitySet<AppContext> appContextEntities;
        EntitySet<Category> categoryEntities;

        #endregion
        #endregion
        #region PUBLIC ACTIONS
        public Action<string> WriteOutput;
        #endregion
        #region PUBLIC PROPERTIES
        #region BASE
        public DataContextState CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
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
        public bool IsInvoking { get; set; }
        //public bool IsLoading { get { return domain == null ? false : domain.IsLoading; } }
        //public bool IsSubmitting { get { return domain == null ? false : domain.IsSubmitting; } }

        #endregion
        #region ENTITIES
        public EntitySet<AppContext> AppContextEntities
        {
            get
            {
                if (appContextEntities == null)
                {
                    appContextEntities = new EntitySet<AppContext>();
                }
                return appContextEntities;
            }
        }
        public EntitySet<Category> CategoryEntities
        {
            get
            {
                if (categoryEntities == null)
                {
                    categoryEntities = new EntitySet<Category>();
                }
                return categoryEntities;
            }
        }
        #endregion

        #endregion

        public DataContext()
        {
            currentValue = new DataContextState();

            initialize();

            guid = Guid.NewGuid().ToString();
        }

        public bool CheckChangesBeforeAddOperation()
        {
            return checkChangesBeforeAddOperation();
        }
        public bool CheckChangesBeforeDeleteOperation()
        {
            return checkChangesBeforeDeleteOperation();
        }
        public void Close()
        {
            if (WriteOutput != null)
                WriteOutput("Closing BaseDataSourceController");

        }
        public void RejectChanges()
        {
            //if (domain != null)
            //{
            //    if (domain.HasChanges)
            //    {
            //        domain.RejectChanges();
            //        OnRejectedChanges(new EventArgs());
            //    }
            //}
        }
        public void Save()
        {
            //Save(null, null);
        }
        //public void Save(Action<SubmitOperation> submitOperationCompleted)
        //{
        //    Save(submitOperationCompleted, null);
        //}
        //public void Save(Action<SubmitOperation> submitOperationCompleted, object userState)
        //{
        //    if (domain != null)
        //    {
        //        if (domain.HasChanges)
        //        {
        //            if (domain.IsSubmitting)
        //            {
        //                //Thread.SpinWait(10);
        //                //Save(submitOperationCompleted, userState);
        //            }
        //            else
        //            {
        //                SubmitOperation submitOperation = domain.SubmitChanges(submitOperationCompleted, userState);
        //                submitOperation.Completed += new EventHandler(submitOperation_Completed);
        //            }
        //        }
        //    }
        //}
        public void TriggerEntityCollectionEvent(Type type)
        {
            triggerEntityCollectionEvent(type);
        }

        private bool checkChangesBeforeAddOperation()
        {
            bool result = false;
            //if (HasChanges)
            //{
            //    string message = CommonStrings.dialog_HasToSaveChanges;
            //    string caption = CommonStrings.dialog_SaveBeforeAdd;
            //    MessageBoxResult messageBoxResult = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            //    if (messageBoxResult == MessageBoxResult.OK)
            //    {
            //        result = true;
            //        Save();
            //    }
            //}
            //else
            //    result = true;

            return result;
        }
        private bool checkChangesBeforeDeleteOperation()
        {
            bool result = false;
            //if (HasChanges)
            //{
            //    string message = CommonStrings.dialog_HasToSaveChanges;
            //    string caption = CommonStrings.dialog_SaveBeforeAdd;
            //    MessageBoxResult messageBoxResult = MessageBox.Show(message, caption, MessageBoxButton.OKCancel);
            //    if (messageBoxResult == MessageBoxResult.OK)
            //    {
            //        result = true;
            //        Save();
            //    }
            //}
            //else
            //    result = true;

            return result;
        }
        private void initialize()
        {
            //watchDog = new DispatcherTimer();
            //watchDog.Tick += new EventHandler(watchDog_Tick);
            //watchDog.Interval = TimeSpan.FromMilliseconds(100);
            //watchDog.Start();

            //openDomainServiceConnection();

        }
        private void openDomainServiceConnection()
        {
            ////Numericon-Dcs-Web-Silverlight-Data-Ria-Web-DataDomainService.svc
            ////string domainPath = @"http://{0}/Admin/DataService.axd/Numericon-Dcs-Web-Silverlight-Data-Ria-Web-DataDomainService/";
            //string domainPath = @"http://{0}/Numericon-Dcs-Web-Silverlight-Data-Ria-Web-DataDomainService.svc";
            //string host = "localhost";
            //string hostName = QueryStringManager.GetValue(AppQueryStrings.Host);
            //if (hostName == null)
            //{
            //    domain = new DataDomainContext();
            //}
            //else
            //{
            //    host = hostName;
            //    host = string.Format(domainPath, host);

            //    Uri uri = new Uri(host, UriKind.Absolute);
            //    domain = new DataDomainContext(uri);
            //}
            //domain.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(domain_PropertyChanged);
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
                    try
                    {
                        method.Invoke(this, new object[] { new EventArgs() });
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }
        }
        private void writeError(string message)
        {
            writeOutput(null, message, OutputMessageTypes.Error);
        }
        private void writeError(string caption, string message)
        {
            writeOutput(caption ?? "[ERROR]", message, OutputMessageTypes.Error);
        }
        private void writeOutput(string caption, string message, OutputMessageTypes messageType)
        {
            //if (message != null)
            //{
            //    OnMessageCreated(new OutputEventArgs()
            //    {
            //        Message = new Numericon.Data.Presentation.OutputMessage()
            //        {
            //            Caption = caption,
            //            Data = message,
            //            OutputMessageType = messageType,
            //        }
            //    });
            //}
        }

        #region DATA DOMAIN EVENT HANDLERS
        void domain_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //HasChanges = domain.HasChanges;
        }
        void loadOperation_Completed(object sender, EventArgs e)
        {
            //LoadOperation loadOperation = sender as LoadOperation;
            //if (loadOperation != null)
            //{
            //    triggerEntityCollectionEvent(loadOperation.EntityQuery.EntityType);
            //    if (loadOperation.HasError)
            //    {
            //        string caption = "Load Failed";
            //        string message = ErrorMessageBuilder.CreateMessage(loadOperation.Error);
            //        writeError(caption, message);
            //        loadOperation.MarkErrorAsHandled();
            //    }
            //}
            //hasChanges = domain.HasChanges;

        }
        void submitOperation_Completed(object sender, EventArgs e)
        {
            //bool failed = false;
            //SubmitOperation submitOperation = sender as SubmitOperation;
            //if (submitOperation != null)
            //{
            //    if (submitOperation.ChangeSet.AddedEntities != null)
            //        foreach (Entity entity in submitOperation.ChangeSet.AddedEntities)
            //            triggerEntityCollectionEvent(entity.GetType());

            //    if (submitOperation.ChangeSet.RemovedEntities != null)
            //        foreach (Entity entity in submitOperation.ChangeSet.RemovedEntities)
            //            triggerEntityCollectionEvent(entity.GetType());

            //    if (submitOperation.HasError)
            //    {
            //        string caption = "Submit Failed";
            //        string message = ErrorMessageBuilder.CreateMessage(submitOperation.Error);
            //        writeError(caption, message);
            //        submitOperation.MarkErrorAsHandled();
            //        failed = true;
            //    }

            //    if (submitOperation.EntitiesInError.Any())
            //    {
            //        string caption = "Submit Failed: Validation Error";
            //        string message = string.Empty;
            //        int i = 1;
            //        failed = true;
            //        foreach (Entity entityInError in submitOperation.EntitiesInError)
            //        {
            //            if (entityInError.EntityConflict != null)
            //            {
            //                //EntityConflict entityConflict = entityInError.EntityConflict;
            //                //foreach (EntityConflict entityConflictMember in entityConflict.CurrentEntity.EntityConflict)
            //                //{
            //                //    message += string.Format("{0}: Entity {1}, member '{2}' in conflict: Current: {3},Original: {4}, Store: {5}\r\n",
            //                //        i,
            //                //        entityInError.GetType().Name,
            //                //        entityConflictMember.PropertyNames, entityConflictMember.CurrentValue,
            //                //        entityConflictMember.OriginalValue, entityConflictMember.StoreValue);
            //                //}
            //            }
            //            else if (entityInError.ValidationErrors.Any())
            //            {
            //                foreach (var validationError in entityInError.ValidationErrors)
            //                {
            //                    if (validationError != null)
            //                    {
            //                        message += string.Format("{0}: {1}{2}",
            //                            i,
            //                            validationError.ErrorMessage,
            //                            Environment.NewLine);
            //                        i++;
            //                    }
            //                }
            //            }
            //            i++;
            //        }
            //        writeError(caption, message);
            //    }
            //}
            //if (failed)
            //{
            //    OnSubmitFailed(new EventArgs());
            //    submitOperation.MarkErrorAsHandled();
            //}
            //hasChanges = domain.HasChanges;

        }


        #endregion

        #region WATCHDOG
        void watchDog_Tick(object sender, EventArgs e)
        {
            //if (domain.IsLoading | domain.IsSubmitting | IsInvoking)
            //    OnAsynchronousOperationStarted(new EventArgs());
            //else
            //    OnAsynchronousOperationFinished(new EventArgs());
        }

        #endregion

    }
}
