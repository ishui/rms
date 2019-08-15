namespace TiannuoPM.Entities
{
    using System;

    public class ContractBillEventArgs : EventArgs
    {
        private ContractBillColumn column;
        private object value;

        public ContractBillEventArgs(ContractBillColumn column)
        {
            this.column = column;
        }

        public ContractBillEventArgs(ContractBillColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractBillColumn Column
        {
            get
            {
                return this.column;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

