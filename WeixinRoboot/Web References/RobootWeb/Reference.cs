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

namespace WeixinRoboot.RobootWeb {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://13828081978.zicp.vip/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback UserLogInOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSettingOperationCompleted;
        
        private System.Threading.SendOrPostCallback SaveSettingOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetBossUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserTokenOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::WeixinRoboot.Properties.Settings.Default.WeixinRoboot_RobootWeb_WebService;
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
        public event UserLogInCompletedEventHandler UserLogInCompleted;
        
        /// <remarks/>
        public event GetSettingCompletedEventHandler GetSettingCompleted;
        
        /// <remarks/>
        public event SaveSettingCompletedEventHandler SaveSettingCompleted;
        
        /// <remarks/>
        public event GetBossUsersCompletedEventHandler GetBossUsersCompleted;
        
        /// <remarks/>
        public event GetUserTokenCompletedEventHandler GetUserTokenCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/UserLogIn", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UserLogIn(string UserName, string Password) {
            object[] results = this.Invoke("UserLogIn", new object[] {
                        UserName,
                        Password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UserLogInAsync(string UserName, string Password) {
            this.UserLogInAsync(UserName, Password, null);
        }
        
        /// <remarks/>
        public void UserLogInAsync(string UserName, string Password, object userState) {
            if ((this.UserLogInOperationCompleted == null)) {
                this.UserLogInOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserLogInOperationCompleted);
            }
            this.InvokeAsync("UserLogIn", new object[] {
                        UserName,
                        Password}, this.UserLogInOperationCompleted, userState);
        }
        
        private void OnUserLogInOperationCompleted(object arg) {
            if ((this.UserLogInCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserLogInCompleted(this, new UserLogInCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/GetSetting", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetSetting(string saspnetUserid) {
            object[] results = this.Invoke("GetSetting", new object[] {
                        saspnetUserid});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetSettingAsync(string saspnetUserid) {
            this.GetSettingAsync(saspnetUserid, null);
        }
        
        /// <remarks/>
        public void GetSettingAsync(string saspnetUserid, object userState) {
            if ((this.GetSettingOperationCompleted == null)) {
                this.GetSettingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSettingOperationCompleted);
            }
            this.InvokeAsync("GetSetting", new object[] {
                        saspnetUserid}, this.GetSettingOperationCompleted, userState);
        }
        
        private void OnGetSettingOperationCompleted(object arg) {
            if ((this.GetSettingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSettingCompleted(this, new GetSettingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/SaveSetting", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SaveSetting(string UserName, string Password, string jaspnet_UsersNewGameResultSend) {
            object[] results = this.Invoke("SaveSetting", new object[] {
                        UserName,
                        Password,
                        jaspnet_UsersNewGameResultSend});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SaveSettingAsync(string UserName, string Password, string jaspnet_UsersNewGameResultSend) {
            this.SaveSettingAsync(UserName, Password, jaspnet_UsersNewGameResultSend, null);
        }
        
        /// <remarks/>
        public void SaveSettingAsync(string UserName, string Password, string jaspnet_UsersNewGameResultSend, object userState) {
            if ((this.SaveSettingOperationCompleted == null)) {
                this.SaveSettingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveSettingOperationCompleted);
            }
            this.InvokeAsync("SaveSetting", new object[] {
                        UserName,
                        Password,
                        jaspnet_UsersNewGameResultSend}, this.SaveSettingOperationCompleted, userState);
        }
        
        private void OnSaveSettingOperationCompleted(object arg) {
            if ((this.SaveSettingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveSettingCompleted(this, new SaveSettingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/GetBossUsers", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Guid[] GetBossUsers(string bossaspnetuserid) {
            object[] results = this.Invoke("GetBossUsers", new object[] {
                        bossaspnetuserid});
            return ((System.Guid[])(results[0]));
        }
        
        /// <remarks/>
        public void GetBossUsersAsync(string bossaspnetuserid) {
            this.GetBossUsersAsync(bossaspnetuserid, null);
        }
        
        /// <remarks/>
        public void GetBossUsersAsync(string bossaspnetuserid, object userState) {
            if ((this.GetBossUsersOperationCompleted == null)) {
                this.GetBossUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBossUsersOperationCompleted);
            }
            this.InvokeAsync("GetBossUsers", new object[] {
                        bossaspnetuserid}, this.GetBossUsersOperationCompleted, userState);
        }
        
        private void OnGetBossUsersOperationCompleted(object arg) {
            if ((this.GetBossUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBossUsersCompleted(this, new GetBossUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://13828081978.zicp.vip/GetUserToken", RequestNamespace="http://13828081978.zicp.vip/", ResponseNamespace="http://13828081978.zicp.vip/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetUserToken(string UserName, string Password) {
            object[] results = this.Invoke("GetUserToken", new object[] {
                        UserName,
                        Password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetUserTokenAsync(string UserName, string Password) {
            this.GetUserTokenAsync(UserName, Password, null);
        }
        
        /// <remarks/>
        public void GetUserTokenAsync(string UserName, string Password, object userState) {
            if ((this.GetUserTokenOperationCompleted == null)) {
                this.GetUserTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserTokenOperationCompleted);
            }
            this.InvokeAsync("GetUserToken", new object[] {
                        UserName,
                        Password}, this.GetUserTokenOperationCompleted, userState);
        }
        
        private void OnGetUserTokenOperationCompleted(object arg) {
            if ((this.GetUserTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserTokenCompleted(this, new GetUserTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void UserLogInCompletedEventHandler(object sender, UserLogInCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserLogInCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserLogInCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetSettingCompletedEventHandler(object sender, GetSettingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSettingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSettingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void SaveSettingCompletedEventHandler(object sender, SaveSettingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveSettingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SaveSettingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetBossUsersCompletedEventHandler(object sender, GetBossUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBossUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetBossUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Guid[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Guid[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void GetUserTokenCompletedEventHandler(object sender, GetUserTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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