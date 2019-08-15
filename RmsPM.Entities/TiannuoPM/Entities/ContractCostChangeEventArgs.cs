namespace TiannuoPM.Entities
{
    using System;

    public class ContractCostChangeEventArgs : EventArgs
    {
        private ContractCostChangeColumn column;
        private object value;

        public ContractCostChangeEventArgs(ContractCostChangeColumn column)
        {
            this.column = column;
        }

        public ContractCostChangeEventArgs(ContractCostChangeColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractCostChangeColumn Column
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

