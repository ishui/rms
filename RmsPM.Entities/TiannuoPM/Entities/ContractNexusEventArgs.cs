namespace TiannuoPM.Entities
{
    using System;

    public class ContractNexusEventArgs : EventArgs
    {
        private ContractNexusColumn column;
        private object value;

        public ContractNexusEventArgs(ContractNexusColumn column)
        {
            this.column = column;
        }

        public ContractNexusEventArgs(ContractNexusColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ContractNexusColumn Column
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

