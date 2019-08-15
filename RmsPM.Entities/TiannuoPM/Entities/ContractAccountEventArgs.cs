namespace TiannuoPM.Entities
{
    using System;

    public class ContractAccountEventArgs : EventArgs
    {
        private ContractAccountColumn column;
        private object value;

        public ContractAccountEventArgs(ContractAccountColumn column)
        {
            this.column = column;
        }

        public ContractAccountEventArgs(ContractAccountColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractAccountColumn Column
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

