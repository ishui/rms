namespace TiannuoPM.Entities
{
    using System;

    public class ContractEventArgs : EventArgs
    {
        private ContractColumn column;
        private object value;

        public ContractEventArgs(ContractColumn column)
        {
            this.column = column;
        }

        public ContractEventArgs(ContractColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractColumn Column
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

