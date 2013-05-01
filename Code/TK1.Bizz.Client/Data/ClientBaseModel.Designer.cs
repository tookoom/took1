﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace TK1.Bizz.Client.Data
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TK1ClientBaseEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TK1ClientBaseEntities object using the connection string found in the 'TK1ClientBaseEntities' section of the application configuration file.
        /// </summary>
        public TK1ClientBaseEntities() : base("name=TK1ClientBaseEntities", "TK1ClientBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TK1ClientBaseEntities object.
        /// </summary>
        public TK1ClientBaseEntities(string connectionString) : base(connectionString, "TK1ClientBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TK1ClientBaseEntities object.
        /// </summary>
        public TK1ClientBaseEntities(EntityConnection connection) : base(connection, "TK1ClientBaseEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ClientAppLog> ClientAppLog
        {
            get
            {
                if ((_ClientAppLog == null))
                {
                    _ClientAppLog = base.CreateObjectSet<ClientAppLog>("ClientAppLog");
                }
                return _ClientAppLog;
            }
        }
        private ObjectSet<ClientAppLog> _ClientAppLog;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ClientConfiguration> ClientConfiguration
        {
            get
            {
                if ((_ClientConfiguration == null))
                {
                    _ClientConfiguration = base.CreateObjectSet<ClientConfiguration>("ClientConfiguration");
                }
                return _ClientConfiguration;
            }
        }
        private ObjectSet<ClientConfiguration> _ClientConfiguration;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ClientAudit> ClientAudit
        {
            get
            {
                if ((_ClientAudit == null))
                {
                    _ClientAudit = base.CreateObjectSet<ClientAudit>("ClientAudit");
                }
                return _ClientAudit;
            }
        }
        private ObjectSet<ClientAudit> _ClientAudit;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ClientAppLog EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToClientAppLog(ClientAppLog clientAppLog)
        {
            base.AddObject("ClientAppLog", clientAppLog);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ClientConfiguration EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToClientConfiguration(ClientConfiguration clientConfiguration)
        {
            base.AddObject("ClientConfiguration", clientConfiguration);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ClientAudit EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToClientAudit(ClientAudit clientAudit)
        {
            base.AddObject("ClientAudit", clientAudit);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TK1ClientBaseModel", Name="ClientAppLog")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ClientAppLog : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ClientAppLog object.
        /// </summary>
        /// <param name="appLogID">Initial value of the AppLogID property.</param>
        /// <param name="logTimestamp">Initial value of the LogTimestamp property.</param>
        /// <param name="logType">Initial value of the LogType property.</param>
        public static ClientAppLog CreateClientAppLog(global::System.Int32 appLogID, global::System.DateTime logTimestamp, global::System.Int32 logType)
        {
            ClientAppLog clientAppLog = new ClientAppLog();
            clientAppLog.AppLogID = appLogID;
            clientAppLog.LogTimestamp = logTimestamp;
            clientAppLog.LogType = logType;
            return clientAppLog;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 AppLogID
        {
            get
            {
                return _AppLogID;
            }
            set
            {
                if (_AppLogID != value)
                {
                    OnAppLogIDChanging(value);
                    ReportPropertyChanging("AppLogID");
                    _AppLogID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("AppLogID");
                    OnAppLogIDChanged();
                }
            }
        }
        private global::System.Int32 _AppLogID;
        partial void OnAppLogIDChanging(global::System.Int32 value);
        partial void OnAppLogIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime LogTimestamp
        {
            get
            {
                return _LogTimestamp;
            }
            set
            {
                OnLogTimestampChanging(value);
                ReportPropertyChanging("LogTimestamp");
                _LogTimestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LogTimestamp");
                OnLogTimestampChanged();
            }
        }
        private global::System.DateTime _LogTimestamp;
        partial void OnLogTimestampChanging(global::System.DateTime value);
        partial void OnLogTimestampChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 LogType
        {
            get
            {
                return _LogType;
            }
            set
            {
                OnLogTypeChanging(value);
                ReportPropertyChanging("LogType");
                _LogType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LogType");
                OnLogTypeChanged();
            }
        }
        private global::System.Int32 _LogType;
        partial void OnLogTypeChanging(global::System.Int32 value);
        partial void OnLogTypeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Data
        {
            get
            {
                return _Data;
            }
            set
            {
                OnDataChanging(value);
                ReportPropertyChanging("Data");
                _Data = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Data");
                OnDataChanged();
            }
        }
        private global::System.String _Data;
        partial void OnDataChanging(global::System.String value);
        partial void OnDataChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TK1ClientBaseModel", Name="ClientAudit")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ClientAudit : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ClientAudit object.
        /// </summary>
        /// <param name="auditID">Initial value of the AuditID property.</param>
        /// <param name="gUID">Initial value of the GUID property.</param>
        /// <param name="eventTimestamp">Initial value of the EventTimestamp property.</param>
        /// <param name="eventType">Initial value of the EventType property.</param>
        /// <param name="customerCode">Initial value of the CustomerCode property.</param>
        /// <param name="processName">Initial value of the ProcessName property.</param>
        /// <param name="reportVisible">Initial value of the ReportVisible property.</param>
        public static ClientAudit CreateClientAudit(global::System.Int32 auditID, global::System.String gUID, global::System.DateTime eventTimestamp, global::System.Int32 eventType, global::System.String customerCode, global::System.String processName, global::System.Boolean reportVisible)
        {
            ClientAudit clientAudit = new ClientAudit();
            clientAudit.AuditID = auditID;
            clientAudit.GUID = gUID;
            clientAudit.EventTimestamp = eventTimestamp;
            clientAudit.EventType = eventType;
            clientAudit.CustomerCode = customerCode;
            clientAudit.ProcessName = processName;
            clientAudit.ReportVisible = reportVisible;
            return clientAudit;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 AuditID
        {
            get
            {
                return _AuditID;
            }
            set
            {
                if (_AuditID != value)
                {
                    OnAuditIDChanging(value);
                    ReportPropertyChanging("AuditID");
                    _AuditID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("AuditID");
                    OnAuditIDChanged();
                }
            }
        }
        private global::System.Int32 _AuditID;
        partial void OnAuditIDChanging(global::System.Int32 value);
        partial void OnAuditIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String GUID
        {
            get
            {
                return _GUID;
            }
            set
            {
                OnGUIDChanging(value);
                ReportPropertyChanging("GUID");
                _GUID = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("GUID");
                OnGUIDChanged();
            }
        }
        private global::System.String _GUID;
        partial void OnGUIDChanging(global::System.String value);
        partial void OnGUIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime EventTimestamp
        {
            get
            {
                return _EventTimestamp;
            }
            set
            {
                OnEventTimestampChanging(value);
                ReportPropertyChanging("EventTimestamp");
                _EventTimestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EventTimestamp");
                OnEventTimestampChanged();
            }
        }
        private global::System.DateTime _EventTimestamp;
        partial void OnEventTimestampChanging(global::System.DateTime value);
        partial void OnEventTimestampChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 EventType
        {
            get
            {
                return _EventType;
            }
            set
            {
                OnEventTypeChanging(value);
                ReportPropertyChanging("EventType");
                _EventType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EventType");
                OnEventTypeChanged();
            }
        }
        private global::System.Int32 _EventType;
        partial void OnEventTypeChanging(global::System.Int32 value);
        partial void OnEventTypeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Assembly
        {
            get
            {
                return _Assembly;
            }
            set
            {
                OnAssemblyChanging(value);
                ReportPropertyChanging("Assembly");
                _Assembly = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Assembly");
                OnAssemblyChanged();
            }
        }
        private global::System.String _Assembly;
        partial void OnAssemblyChanging(global::System.String value);
        partial void OnAssemblyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Source
        {
            get
            {
                return _Source;
            }
            set
            {
                OnSourceChanging(value);
                ReportPropertyChanging("Source");
                _Source = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Source");
                OnSourceChanged();
            }
        }
        private global::System.String _Source;
        partial void OnSourceChanging(global::System.String value);
        partial void OnSourceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Data
        {
            get
            {
                return _Data;
            }
            set
            {
                OnDataChanging(value);
                ReportPropertyChanging("Data");
                _Data = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Data");
                OnDataChanged();
            }
        }
        private global::System.String _Data;
        partial void OnDataChanging(global::System.String value);
        partial void OnDataChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                OnCustomerCodeChanging(value);
                ReportPropertyChanging("CustomerCode");
                _CustomerCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CustomerCode");
                OnCustomerCodeChanged();
            }
        }
        private global::System.String _CustomerCode;
        partial void OnCustomerCodeChanging(global::System.String value);
        partial void OnCustomerCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ProcessName
        {
            get
            {
                return _ProcessName;
            }
            set
            {
                OnProcessNameChanging(value);
                ReportPropertyChanging("ProcessName");
                _ProcessName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ProcessName");
                OnProcessNameChanged();
            }
        }
        private global::System.String _ProcessName;
        partial void OnProcessNameChanging(global::System.String value);
        partial void OnProcessNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean ReportVisible
        {
            get
            {
                return _ReportVisible;
            }
            set
            {
                OnReportVisibleChanging(value);
                ReportPropertyChanging("ReportVisible");
                _ReportVisible = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ReportVisible");
                OnReportVisibleChanged();
            }
        }
        private global::System.Boolean _ReportVisible;
        partial void OnReportVisibleChanging(global::System.Boolean value);
        partial void OnReportVisibleChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TK1ClientBaseModel", Name="ClientConfiguration")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ClientConfiguration : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ClientConfiguration object.
        /// </summary>
        /// <param name="configurationID">Initial value of the ConfigurationID property.</param>
        /// <param name="customerCode">Initial value of the CustomerCode property.</param>
        /// <param name="key">Initial value of the Key property.</param>
        public static ClientConfiguration CreateClientConfiguration(global::System.Int32 configurationID, global::System.String customerCode, global::System.String key)
        {
            ClientConfiguration clientConfiguration = new ClientConfiguration();
            clientConfiguration.ConfigurationID = configurationID;
            clientConfiguration.CustomerCode = customerCode;
            clientConfiguration.Key = key;
            return clientConfiguration;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ConfigurationID
        {
            get
            {
                return _ConfigurationID;
            }
            set
            {
                if (_ConfigurationID != value)
                {
                    OnConfigurationIDChanging(value);
                    ReportPropertyChanging("ConfigurationID");
                    _ConfigurationID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ConfigurationID");
                    OnConfigurationIDChanged();
                }
            }
        }
        private global::System.Int32 _ConfigurationID;
        partial void OnConfigurationIDChanging(global::System.Int32 value);
        partial void OnConfigurationIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                OnCustomerCodeChanging(value);
                ReportPropertyChanging("CustomerCode");
                _CustomerCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CustomerCode");
                OnCustomerCodeChanged();
            }
        }
        private global::System.String _CustomerCode;
        partial void OnCustomerCodeChanging(global::System.String value);
        partial void OnCustomerCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Key
        {
            get
            {
                return _Key;
            }
            set
            {
                OnKeyChanging(value);
                ReportPropertyChanging("Key");
                _Key = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Key");
                OnKeyChanged();
            }
        }
        private global::System.String _Key;
        partial void OnKeyChanging(global::System.String value);
        partial void OnKeyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                OnValueChanging(value);
                ReportPropertyChanging("Value");
                _Value = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Value");
                OnValueChanged();
            }
        }
        private global::System.String _Value;
        partial void OnValueChanging(global::System.String value);
        partial void OnValueChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Group
        {
            get
            {
                return _Group;
            }
            set
            {
                OnGroupChanging(value);
                ReportPropertyChanging("Group");
                _Group = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Group");
                OnGroupChanged();
            }
        }
        private global::System.String _Group;
        partial void OnGroupChanging(global::System.String value);
        partial void OnGroupChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Type
        {
            get
            {
                return _Type;
            }
            set
            {
                OnTypeChanging(value);
                ReportPropertyChanging("Type");
                _Type = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Type");
                OnTypeChanged();
            }
        }
        private global::System.String _Type;
        partial void OnTypeChanging(global::System.String value);
        partial void OnTypeChanged();

        #endregion

    
    }

    #endregion

    
}