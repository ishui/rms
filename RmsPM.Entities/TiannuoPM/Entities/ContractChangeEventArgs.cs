namespace TiannuoPM.Entities
{
    using System;

    public class ContractChangeEventArgs : EventArgs
    {
        private ContractChangeColumn column;
        private object value;

        public ContractChangeEventArgs(ContractChangeColumn column)
        {
            this.column = column;
        }

        public ContractChangeEventArgs(ContractChangeColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractChangeColumn Column
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

