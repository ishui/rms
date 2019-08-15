namespace RmsPM.BLL.RefSal
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;

    [GeneratedCode("System.Web.Services", "2.0.50727.42"), DebuggerStepThrough, WebServiceBinding(Name="SalServiceSoap", Namespace="http://tempuri.org/"), DesignerCategory("code")]
    public class SalService : SoapHttpClientProtocol
    {
        private SendOrPostCallback ClearSalImpFlagOperationCompleted;
        private SendOrPostCallback GetSalDataByClientOperationCompleted;
        private SendOrPostCallback GetSalDataHeadClientByClientNameOperationCompleted;
        private SendOrPostCallback GetSalDataHeadClientByProjectOperationCompleted;
        private SendOrPostCallback GetSalProjectByCodeOperationCompleted;
        private SendOrPostCallback GetSalProjectOperationCompleted;
        private SendOrPostCallback SetSalAccountImpFlagOperationCompleted;
        private SendOrPostCallback SetSalContractImpFlagOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        public event ClearSalImpFlagCompletedEventHandler ClearSalImpFlagCompleted;

        public event GetSalDataByClientCompletedEventHandler GetSalDataByClientCompleted;

        public event GetSalDataHeadClientByClientNameCompletedEventHandler GetSalDataHeadClientByClientNameCompleted;

        public event GetSalDataHeadClientByProjectCompletedEventHandler GetSalDataHeadClientByProjectCompleted;

        public event GetSalProjectByCodeCompletedEventHandler GetSalProjectByCodeCompleted;

        public event GetSalProjectCompletedEventHandler GetSalProjectCompleted;

        public event SetSalAccountImpFlagCompletedEventHandler SetSalAccountImpFlagCompleted;

        public event SetSalContractImpFlagCompletedEventHandler SetSalContractImpFlagCompleted;

        public SalService()
        {
            string text = ConfigurationManager.AppSettings["SalServiceUrl"];
            switch (text)
            {
                case null:
                case "":
                    throw new Exception("未设置销售系统接口Url(SalServiceUrl)");
            }
            this.Url = text;
            if (this.IsLocalFileSystemWebService(this.Url))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public IAsyncResult BeginClearSalImpFlag(string ProjectCode, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("ClearSalImpFlag", new object[] { ProjectCode }, callback, asyncState);
        }

        public IAsyncResult BeginGetSalDataByClient(string client_code, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("GetSalDataByClient", new object[] { client_code }, callback, asyncState);
        }

        public IAsyncResult BeginGetSalDataHeadClientByClientName(string ProjectCode, string ClientName, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("GetSalDataHeadClientByClientName", new object[] { ProjectCode, ClientName }, callback, asyncState);
        }

        public IAsyncResult BeginGetSalDataHeadClientByProject(string ProjectCode, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("GetSalDataHeadClientByProject", new object[] { ProjectCode }, callback, asyncState);
        }

        public IAsyncResult BeginGetSalProject(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("GetSalProject", new object[0], callback, asyncState);
        }

        public IAsyncResult BeginGetSalProjectByCode(string ProjectCode, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("GetSalProjectByCode", new object[] { ProjectCode }, callback, asyncState);
        }

        public IAsyncResult BeginSetSalAccountImpFlag(string AccountCodes, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("SetSalAccountImpFlag", new object[] { AccountCodes }, callback, asyncState);
        }

        public IAsyncResult BeginSetSalContractImpFlag(string ContractCodes, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("SetSalContractImpFlag", new object[] { ContractCodes }, callback, asyncState);
        }

        public void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        [SoapDocumentMethod("http://tempuri.org/ClearSalImpFlag", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool ClearSalImpFlag(string ProjectCode)
        {
            return (bool) base.Invoke("ClearSalImpFlag", new object[] { ProjectCode })[0];
        }

        public void ClearSalImpFlagAsync(string ProjectCode)
        {
            this.ClearSalImpFlagAsync(ProjectCode, null);
        }

        public void ClearSalImpFlagAsync(string ProjectCode, object userState)
        {
            if (this.ClearSalImpFlagOperationCompleted == null)
            {
                this.ClearSalImpFlagOperationCompleted = new SendOrPostCallback(this.OnClearSalImpFlagOperationCompleted);
            }
            base.InvokeAsync("ClearSalImpFlag", new object[] { ProjectCode }, this.ClearSalImpFlagOperationCompleted, userState);
        }

        public bool EndClearSalImpFlag(IAsyncResult asyncResult)
        {
            return (bool) base.EndInvoke(asyncResult)[0];
        }

        public DataSet EndGetSalDataByClient(IAsyncResult asyncResult)
        {
            return (DataSet) base.EndInvoke(asyncResult)[0];
        }

        public DataSet EndGetSalDataHeadClientByClientName(IAsyncResult asyncResult)
        {
            return (DataSet) base.EndInvoke(asyncResult)[0];
        }

        public DataSet EndGetSalDataHeadClientByProject(IAsyncResult asyncResult)
        {
            return (DataSet) base.EndInvoke(asyncResult)[0];
        }

        public DataSet EndGetSalProject(IAsyncResult asyncResult)
        {
            return (DataSet) base.EndInvoke(asyncResult)[0];
        }

        public DataSet EndGetSalProjectByCode(IAsyncResult asyncResult)
        {
            return (DataSet) base.EndInvoke(asyncResult)[0];
        }

        public bool EndSetSalAccountImpFlag(IAsyncResult asyncResult)
        {
            return (bool) base.EndInvoke(asyncResult)[0];
        }

        public bool EndSetSalContractImpFlag(IAsyncResult asyncResult)
        {
            return (bool) base.EndInvoke(asyncResult)[0];
        }

        [SoapDocumentMethod("http://tempuri.org/GetSalDataByClient", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet GetSalDataByClient(string client_code)
        {
            return (DataSet) base.Invoke("GetSalDataByClient", new object[] { client_code })[0];
        }

        public void GetSalDataByClientAsync(string client_code)
        {
            this.GetSalDataByClientAsync(client_code, null);
        }

        public void GetSalDataByClientAsync(string client_code, object userState)
        {
            if (this.GetSalDataByClientOperationCompleted == null)
            {
                this.GetSalDataByClientOperationCompleted = new SendOrPostCallback(this.OnGetSalDataByClientOperationCompleted);
            }
            base.InvokeAsync("GetSalDataByClient", new object[] { client_code }, this.GetSalDataByClientOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/GetSalDataHeadClientByClientName", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet GetSalDataHeadClientByClientName(string ProjectCode, string ClientName)
        {
            return (DataSet) base.Invoke("GetSalDataHeadClientByClientName", new object[] { ProjectCode, ClientName })[0];
        }

        public void GetSalDataHeadClientByClientNameAsync(string ProjectCode, string ClientName)
        {
            this.GetSalDataHeadClientByClientNameAsync(ProjectCode, ClientName, null);
        }

        public void GetSalDataHeadClientByClientNameAsync(string ProjectCode, string ClientName, object userState)
        {
            if (this.GetSalDataHeadClientByClientNameOperationCompleted == null)
            {
                this.GetSalDataHeadClientByClientNameOperationCompleted = new SendOrPostCallback(this.OnGetSalDataHeadClientByClientNameOperationCompleted);
            }
            base.InvokeAsync("GetSalDataHeadClientByClientName", new object[] { ProjectCode, ClientName }, this.GetSalDataHeadClientByClientNameOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/GetSalDataHeadClientByProject", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet GetSalDataHeadClientByProject(string ProjectCode)
        {
            return (DataSet) base.Invoke("GetSalDataHeadClientByProject", new object[] { ProjectCode })[0];
        }

        public void GetSalDataHeadClientByProjectAsync(string ProjectCode)
        {
            this.GetSalDataHeadClientByProjectAsync(ProjectCode, null);
        }

        public void GetSalDataHeadClientByProjectAsync(string ProjectCode, object userState)
        {
            if (this.GetSalDataHeadClientByProjectOperationCompleted == null)
            {
                this.GetSalDataHeadClientByProjectOperationCompleted = new SendOrPostCallback(this.OnGetSalDataHeadClientByProjectOperationCompleted);
            }
            base.InvokeAsync("GetSalDataHeadClientByProject", new object[] { ProjectCode }, this.GetSalDataHeadClientByProjectOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/GetSalProject", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet GetSalProject()
        {
            return (DataSet) base.Invoke("GetSalProject", new object[0])[0];
        }

        public void GetSalProjectAsync()
        {
            this.GetSalProjectAsync(null);
        }

        public void GetSalProjectAsync(object userState)
        {
            if (this.GetSalProjectOperationCompleted == null)
            {
                this.GetSalProjectOperationCompleted = new SendOrPostCallback(this.OnGetSalProjectOperationCompleted);
            }
            base.InvokeAsync("GetSalProject", new object[0], this.GetSalProjectOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/GetSalProjectByCode", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public DataSet GetSalProjectByCode(string ProjectCode)
        {
            return (DataSet) base.Invoke("GetSalProjectByCode", new object[] { ProjectCode })[0];
        }

        public void GetSalProjectByCodeAsync(string ProjectCode)
        {
            this.GetSalProjectByCodeAsync(ProjectCode, null);
        }

        public void GetSalProjectByCodeAsync(string ProjectCode, object userState)
        {
            if (this.GetSalProjectByCodeOperationCompleted == null)
            {
                this.GetSalProjectByCodeOperationCompleted = new SendOrPostCallback(this.OnGetSalProjectByCodeOperationCompleted);
            }
            base.InvokeAsync("GetSalProjectByCode", new object[] { ProjectCode }, this.GetSalProjectByCodeOperationCompleted, userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        private void OnClearSalImpFlagOperationCompleted(object arg)
        {
            if (this.ClearSalImpFlagCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.ClearSalImpFlagCompleted(this, new ClearSalImpFlagCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetSalDataByClientOperationCompleted(object arg)
        {
            if (this.GetSalDataByClientCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.GetSalDataByClientCompleted(this, new GetSalDataByClientCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetSalDataHeadClientByClientNameOperationCompleted(object arg)
        {
            if (this.GetSalDataHeadClientByClientNameCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.GetSalDataHeadClientByClientNameCompleted(this, new GetSalDataHeadClientByClientNameCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetSalDataHeadClientByProjectOperationCompleted(object arg)
        {
            if (this.GetSalDataHeadClientByProjectCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.GetSalDataHeadClientByProjectCompleted(this, new GetSalDataHeadClientByProjectCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetSalProjectByCodeOperationCompleted(object arg)
        {
            if (this.GetSalProjectByCodeCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.GetSalProjectByCodeCompleted(this, new GetSalProjectByCodeCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnGetSalProjectOperationCompleted(object arg)
        {
            if (this.GetSalProjectCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.GetSalProjectCompleted(this, new GetSalProjectCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnSetSalAccountImpFlagOperationCompleted(object arg)
        {
            if (this.SetSalAccountImpFlagCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.SetSalAccountImpFlagCompleted(this, new SetSalAccountImpFlagCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        private void OnSetSalContractImpFlagOperationCompleted(object arg)
        {
            if (this.SetSalContractImpFlagCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
                this.SetSalContractImpFlagCompleted(this, new SetSalContractImpFlagCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        [SoapDocumentMethod("http://tempuri.org/SetSalAccountImpFlag", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool SetSalAccountImpFlag(string AccountCodes)
        {
            return (bool) base.Invoke("SetSalAccountImpFlag", new object[] { AccountCodes })[0];
        }

        public void SetSalAccountImpFlagAsync(string AccountCodes)
        {
            this.SetSalAccountImpFlagAsync(AccountCodes, null);
        }

        public void SetSalAccountImpFlagAsync(string AccountCodes, object userState)
        {
            if (this.SetSalAccountImpFlagOperationCompleted == null)
            {
                this.SetSalAccountImpFlagOperationCompleted = new SendOrPostCallback(this.OnSetSalAccountImpFlagOperationCompleted);
            }
            base.InvokeAsync("SetSalAccountImpFlag", new object[] { AccountCodes }, this.SetSalAccountImpFlagOperationCompleted, userState);
        }

        [SoapDocumentMethod("http://tempuri.org/SetSalContractImpFlag", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public bool SetSalContractImpFlag(string ContractCodes)
        {
            return (bool) base.Invoke("SetSalContractImpFlag", new object[] { ContractCodes })[0];
        }

        public void SetSalContractImpFlagAsync(string ContractCodes)
        {
            this.SetSalContractImpFlagAsync(ContractCodes, null);
        }

        public void SetSalContractImpFlagAsync(string ContractCodes, object userState)
        {
            if (this.SetSalContractImpFlagOperationCompleted == null)
            {
                this.SetSalContractImpFlagOperationCompleted = new SendOrPostCallback(this.OnSetSalContractImpFlagOperationCompleted);
            }
            base.InvokeAsync("SetSalContractImpFlag", new object[] { ContractCodes }, this.SetSalContractImpFlagOperationCompleted, userState);
        }

        public string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if (!((!this.IsLocalFileSystemWebService(base.Url) || this.useDefaultCredentialsSetExplicitly) || this.IsLocalFileSystemWebService(value)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

