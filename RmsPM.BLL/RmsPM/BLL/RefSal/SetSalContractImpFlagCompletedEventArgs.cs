namespace RmsPM.BLL.RefSal
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;

    [DebuggerStepThrough, GeneratedCode("System.Web.Services", "2.0.50727.42"), DesignerCategory("code")]
    public class SetSalContractImpFlagCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal SetSalContractImpFlagCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public bool Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (bool) this.results[0];
            }
        }
    }
}

