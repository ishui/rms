namespace TiannuoPM.Entities
{
    using System;

    public class ContractCostPlanEventArgs : EventArgs
    {
        private ContractCostPlanColumn column;
        private object value;

        public ContractCostPlanEventArgs(ContractCostPlanColumn column)
        {
            this.column = column;
        }

        public ContractCostPlanEventArgs(ContractCostPlanColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractCostPlanColumn Column
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

