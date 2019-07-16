﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace WeixinRoboot.RobotWebAdmin {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SysadminServicesSoap", Namespace="http://13828081978.zicp.vip/")]
    public partial class SysadminServices : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetAllUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetUserLockOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback BuidMD5ActiveCodeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SysadminServices() {
            this.Url = global::WeixinRoboot.Properties.Settings.Default.WeixinRoboot_RobotWebAdmin_SysadminServices;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetAllUsersCompletedEventHandler GetAllUsersCompleted;
        
        /// <remarks/>
        public event CreateUserCompletedEventHandler CreateUserCompleted;
        
        /// <remarks/>
        public event SetUserLockCompletedEventHandler SetUserLockCompleted;
        
        /// <remarks/>
        public event GetUserInfoCompletedEventHandler GetUserInfoCompleted;
        
        /// <remarks/>
        public event BuidMD5ActiveCodeCompletedEventHandler BuidMD5ActiveCodeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/GetAllUsers", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetAllUsers() {
            object[] results = this.Invoke("GetAllUsers", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllUsersAsync() {
            this.GetAllUsersAsync(null);
        }
        
        /// <remarks/>
        public void GetAllUsersAsync(object userState) {
            if ((this.GetAllUsersOperationCompleted == null)) {
                this.GetAllUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllUsersOperationCompleted);
            }
            this.InvokeAsync("GetAllUsers", new object[0], this.GetAllUsersOperationCompleted, userState);
        }
        
        private void OnGetAllUsersOperationCompleted(object arg) {
            if ((this.GetAllUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllUsersCompleted(this, new GetAllUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/CreateUser", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateUser(string UserName, string Password) {
            object[] results = this.Invoke("CreateUser", new object[] {
                        UserName,
                        Password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateUserAsync(string UserName, string Password) {
            this.CreateUserAsync(UserName, Password, null);
        }
        
        /// <remarks/>
        public void CreateUserAsync(string UserName, string Password, object userState) {
            if ((this.CreateUserOperationCompleted == null)) {
                this.CreateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateUserOperationCompleted);
            }
            this.InvokeAsync("CreateUser", new object[] {
                        UserName,
                        Password}, this.CreateUserOperationCompleted, userState);
        }
        
        private void OnCreateUserOperationCompleted(object arg) {
            if ((this.CreateUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateUserCompleted(this, new CreateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/SetUserLock", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SetUserLock(string UserName, bool IsLockOut) {
            object[] results = this.Invoke("SetUserLock", new object[] {
                        UserName,
                        IsLockOut});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SetUserLockAsync(string UserName, bool IsLockOut) {
            this.SetUserLockAsync(UserName, IsLockOut, null);
        }
        
        /// <remarks/>
        public void SetUserLockAsync(string UserName, bool IsLockOut, object userState) {
            if ((this.SetUserLockOperationCompleted == null)) {
                this.SetUserLockOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetUserLockOperationCompleted);
            }
            this.InvokeAsync("SetUserLock", new object[] {
                        UserName,
                        IsLockOut}, this.SetUserLockOperationCompleted, userState);
        }
        
        private void OnSetUserLockOperationCompleted(object arg) {
            if ((this.SetUserLockCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetUserLockCompleted(this, new SetUserLockCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/GetUserInfo", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetUserInfo(string UserName) {
            object[] results = this.Invoke("GetUserInfo", new object[] {
                        UserName});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserInfoAsync(string UserName) {
            this.GetUserInfoAsync(UserName, null);
        }
        
        /// <remarks/>
        public void GetUserInfoAsync(string UserName, object userState) {
            if ((this.GetUserInfoOperationCompleted == null)) {
                this.GetUserInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserInfoOperationCompleted);
            }
            this.InvokeAsync("GetUserInfo", new object[] {
                        UserName}, this.GetUserInfoOperationCompleted, userState);
        }
        
        private void OnGetUserInfoOperationCompleted(object arg) {
            if ((this.GetUserInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserInfoCompleted(this, new GetUserInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/BuidMD5ActiveCode", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string BuidMD5ActiveCode(System.DateTime EndDate, System.Guid MyGuid) {
            object[] results = this.Invoke("BuidMD5ActiveCode", new object[] {
                        EndDate,
                        MyGuid});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void BuidMD5ActiveCodeAsync(System.DateTime EndDate, System.Guid MyGuid) {
            this.BuidMD5ActiveCodeAsync(EndDate, MyGuid, null);
        }
        
        /// <remarks/>
        public void BuidMD5ActiveCodeAsync(System.DateTime EndDate, System.Guid MyGuid, object userState) {
            if ((this.BuidMD5ActiveCodeOperationCompleted == null)) {
                this.BuidMD5ActiveCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnBuidMD5ActiveCodeOperationCompleted);
            }
            this.InvokeAsync("BuidMD5ActiveCode", new object[] {
                        EndDate,
                        MyGuid}, this.BuidMD5ActiveCodeOperationCompleted, userState);
        }
        
        private void OnBuidMD5ActiveCodeOperationCompleted(object arg) {
            if ((this.BuidMD5ActiveCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.BuidMD5ActiveCodeCompleted(this, new BuidMD5ActiveCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void GetAllUsersCompletedEventHandler(object sender, GetAllUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void CreateUserCompletedEventHandler(object sender, CreateUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void SetUserLockCompletedEventHandler(object sender, SetUserLockCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetUserLockCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetUserLockCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void GetUserInfoCompletedEventHandler(object sender, GetUserInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void BuidMD5ActiveCodeCompletedEventHandler(object sender, BuidMD5ActiveCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BuidMD5ActiveCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal BuidMD5ActiveCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591