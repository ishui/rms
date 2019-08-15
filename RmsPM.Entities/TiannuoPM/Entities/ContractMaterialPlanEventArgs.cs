namespace TiannuoPM.Entities
{
    using System;

    public class ContractMaterialPlanEventArgs : EventArgs
    {
        private ContractMaterialPlanColumn column;
        private object value;

        public ContractMaterialPlanEventArgs(ContractMaterialPlanColumn column)
        {
            this.column = column;
        }

        public ContractMaterialPlanEventArgs(ContractMaterialPlanColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractMaterialPlanColumn Column
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

