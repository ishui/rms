namespace RmsPM.BLL.RefSal
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;

    [DesignerCategory("code"), GeneratedCode("System.Web.Services", "2.0.50727.42"), DebuggerStepThrough]
    public class GetSalDataHeadClientByClientNameCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal GetSalDataHeadClientByClientNameCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
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

