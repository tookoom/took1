﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.60310.0
// 
namespace TK1.Bizz.BrokerSL.BrokerFileServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UploadPicture", Namespace="http://schemas.datacontract.org/2004/07/")]
    public partial class UploadPicture : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] FileField;
        
        private string FileNameField;
        
        private byte[] ThumbnailField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] File {
            get {
                return this.FileField;
            }
            set {
                if ((object.ReferenceEquals(this.FileField, value) != true)) {
                    this.FileField = value;
                    this.RaisePropertyChanged("File");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Thumbnail {
            get {
                return this.ThumbnailField;
            }
            set {
                if ((object.ReferenceEquals(this.ThumbnailField, value) != true)) {
                    this.ThumbnailField = value;
                    this.RaisePropertyChanged("Thumbnail");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PictureInfo", Namespace="http://schemas.datacontract.org/2004/07/")]
    public partial class PictureInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string PicturePathField;
        
        private string PictureUrlField;
        
        private bool SuccessField;
        
        private string ThumbnaiPathField;
        
        private string ThumbnailUrlField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PicturePath {
            get {
                return this.PicturePathField;
            }
            set {
                if ((object.ReferenceEquals(this.PicturePathField, value) != true)) {
                    this.PicturePathField = value;
                    this.RaisePropertyChanged("PicturePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PictureUrl {
            get {
                return this.PictureUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.PictureUrlField, value) != true)) {
                    this.PictureUrlField = value;
                    this.RaisePropertyChanged("PictureUrl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ThumbnaiPath {
            get {
                return this.ThumbnaiPathField;
            }
            set {
                if ((object.ReferenceEquals(this.ThumbnaiPathField, value) != true)) {
                    this.ThumbnaiPathField = value;
                    this.RaisePropertyChanged("ThumbnaiPath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ThumbnailUrl {
            get {
                return this.ThumbnailUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.ThumbnailUrlField, value) != true)) {
                    this.ThumbnailUrlField = value;
                    this.RaisePropertyChanged("ThumbnailUrl");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="BrokerFileServiceReference.BrokerFileService")]
    public interface BrokerFileService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:BrokerFileService/LogCustomerAccess", ReplyAction="urn:BrokerFileService/LogCustomerAccessResponse")]
        System.IAsyncResult BeginLogCustomerAccess(string customerCodename, System.AsyncCallback callback, object asyncState);
        
        void EndLogCustomerAccess(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:BrokerFileService/SaveBrokerSiteAdPic", ReplyAction="urn:BrokerFileService/SaveBrokerSiteAdPicResponse")]
        System.IAsyncResult BeginSaveBrokerSiteAdPic(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture, System.AsyncCallback callback, object asyncState);
        
        TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo EndSaveBrokerSiteAdPic(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BrokerFileServiceChannel : TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SaveBrokerSiteAdPicCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SaveBrokerSiteAdPicCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BrokerFileServiceClient : System.ServiceModel.ClientBase<TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService>, TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService {
        
        private BeginOperationDelegate onBeginLogCustomerAccessDelegate;
        
        private EndOperationDelegate onEndLogCustomerAccessDelegate;
        
        private System.Threading.SendOrPostCallback onLogCustomerAccessCompletedDelegate;
        
        private BeginOperationDelegate onBeginSaveBrokerSiteAdPicDelegate;
        
        private EndOperationDelegate onEndSaveBrokerSiteAdPicDelegate;
        
        private System.Threading.SendOrPostCallback onSaveBrokerSiteAdPicCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public BrokerFileServiceClient() {
        }
        
        public BrokerFileServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BrokerFileServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BrokerFileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BrokerFileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> LogCustomerAccessCompleted;
        
        public event System.EventHandler<SaveBrokerSiteAdPicCompletedEventArgs> SaveBrokerSiteAdPicCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService.BeginLogCustomerAccess(string customerCodename, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLogCustomerAccess(customerCodename, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService.EndLogCustomerAccess(System.IAsyncResult result) {
            base.Channel.EndLogCustomerAccess(result);
        }
        
        private System.IAsyncResult OnBeginLogCustomerAccess(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string customerCodename = ((string)(inValues[0]));
            return ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService)(this)).BeginLogCustomerAccess(customerCodename, callback, asyncState);
        }
        
        private object[] OnEndLogCustomerAccess(System.IAsyncResult result) {
            ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService)(this)).EndLogCustomerAccess(result);
            return null;
        }
        
        private void OnLogCustomerAccessCompleted(object state) {
            if ((this.LogCustomerAccessCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LogCustomerAccessCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LogCustomerAccessAsync(string customerCodename) {
            this.LogCustomerAccessAsync(customerCodename, null);
        }
        
        public void LogCustomerAccessAsync(string customerCodename, object userState) {
            if ((this.onBeginLogCustomerAccessDelegate == null)) {
                this.onBeginLogCustomerAccessDelegate = new BeginOperationDelegate(this.OnBeginLogCustomerAccess);
            }
            if ((this.onEndLogCustomerAccessDelegate == null)) {
                this.onEndLogCustomerAccessDelegate = new EndOperationDelegate(this.OnEndLogCustomerAccess);
            }
            if ((this.onLogCustomerAccessCompletedDelegate == null)) {
                this.onLogCustomerAccessCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLogCustomerAccessCompleted);
            }
            base.InvokeAsync(this.onBeginLogCustomerAccessDelegate, new object[] {
                        customerCodename}, this.onEndLogCustomerAccessDelegate, this.onLogCustomerAccessCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService.BeginSaveBrokerSiteAdPic(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSaveBrokerSiteAdPic(customerCodename, adType, siteAdID, uploadPicture, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService.EndSaveBrokerSiteAdPic(System.IAsyncResult result) {
            return base.Channel.EndSaveBrokerSiteAdPic(result);
        }
        
        private System.IAsyncResult OnBeginSaveBrokerSiteAdPic(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string customerCodename = ((string)(inValues[0]));
            TK1.Bizz.Data.Presentation.SiteAdTypes adType = ((TK1.Bizz.Data.Presentation.SiteAdTypes)(inValues[1]));
            int siteAdID = ((int)(inValues[2]));
            TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture = ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture)(inValues[3]));
            return ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService)(this)).BeginSaveBrokerSiteAdPic(customerCodename, adType, siteAdID, uploadPicture, callback, asyncState);
        }
        
        private object[] OnEndSaveBrokerSiteAdPic(System.IAsyncResult result) {
            TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo retVal = ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService)(this)).EndSaveBrokerSiteAdPic(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSaveBrokerSiteAdPicCompleted(object state) {
            if ((this.SaveBrokerSiteAdPicCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SaveBrokerSiteAdPicCompleted(this, new SaveBrokerSiteAdPicCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SaveBrokerSiteAdPicAsync(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture) {
            this.SaveBrokerSiteAdPicAsync(customerCodename, adType, siteAdID, uploadPicture, null);
        }
        
        public void SaveBrokerSiteAdPicAsync(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture, object userState) {
            if ((this.onBeginSaveBrokerSiteAdPicDelegate == null)) {
                this.onBeginSaveBrokerSiteAdPicDelegate = new BeginOperationDelegate(this.OnBeginSaveBrokerSiteAdPic);
            }
            if ((this.onEndSaveBrokerSiteAdPicDelegate == null)) {
                this.onEndSaveBrokerSiteAdPicDelegate = new EndOperationDelegate(this.OnEndSaveBrokerSiteAdPic);
            }
            if ((this.onSaveBrokerSiteAdPicCompletedDelegate == null)) {
                this.onSaveBrokerSiteAdPicCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSaveBrokerSiteAdPicCompleted);
            }
            base.InvokeAsync(this.onBeginSaveBrokerSiteAdPicDelegate, new object[] {
                        customerCodename,
                        adType,
                        siteAdID,
                        uploadPicture}, this.onEndSaveBrokerSiteAdPicDelegate, this.onSaveBrokerSiteAdPicCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService CreateChannel() {
            return new BrokerFileServiceClientChannel(this);
        }
        
        private class BrokerFileServiceClientChannel : ChannelBase<TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService>, TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService {
            
            public BrokerFileServiceClientChannel(System.ServiceModel.ClientBase<TK1.Bizz.BrokerSL.BrokerFileServiceReference.BrokerFileService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginLogCustomerAccess(string customerCodename, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = customerCodename;
                System.IAsyncResult _result = base.BeginInvoke("LogCustomerAccess", _args, callback, asyncState);
                return _result;
            }
            
            public void EndLogCustomerAccess(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("LogCustomerAccess", _args, result);
            }
            
            public System.IAsyncResult BeginSaveBrokerSiteAdPic(string customerCodename, TK1.Bizz.Data.Presentation.SiteAdTypes adType, int siteAdID, TK1.Bizz.BrokerSL.BrokerFileServiceReference.UploadPicture uploadPicture, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[4];
                _args[0] = customerCodename;
                _args[1] = adType;
                _args[2] = siteAdID;
                _args[3] = uploadPicture;
                System.IAsyncResult _result = base.BeginInvoke("SaveBrokerSiteAdPic", _args, callback, asyncState);
                return _result;
            }
            
            public TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo EndSaveBrokerSiteAdPic(System.IAsyncResult result) {
                object[] _args = new object[0];
                TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo _result = ((TK1.Bizz.BrokerSL.BrokerFileServiceReference.PictureInfo)(base.EndInvoke("SaveBrokerSiteAdPic", _args, result)));
                return _result;
            }
        }
    }
}