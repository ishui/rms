namespace TiannuoPM.Entities
{
    using System;

    public class ContractCostEventArgs : EventArgs
    {
        private ContractCostColumn column;
        private object value;

        public ContractCostEventArgs(ContractCostColumn column)
        {
            this.column = column;
        }

        public ContractCostEventArgs(ContractCostColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractCostColumn Column
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

