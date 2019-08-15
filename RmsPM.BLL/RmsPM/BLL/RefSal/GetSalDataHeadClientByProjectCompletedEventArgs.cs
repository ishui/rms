namespace RmsPM.BLL.RefSal
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    [DebuggerStepThrough, GeneratedCode("System.Web.Services", "2.0.50727.42"), DesignerCategory("code")]
    public class GetSalDataHeadClientByProjectCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal GetSalDataHeadClientByProjectCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public DataSet Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (DataSet) this.results[0];
            }
        }
    }
}

