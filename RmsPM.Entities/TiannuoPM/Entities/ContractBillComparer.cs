namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractBillComparer : IComparer<ContractBill>
    {
        private ContractBillColumn whichComparison;

        public ContractBillComparer()
        {
        }

        public ContractBillComparer(ContractBillColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractBill a, ContractBill b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractBill a, ContractBill b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractBill entity)
        {
            return entity.GetHashCode();
        }

        public ContractBillColumn WhichComparison
        {
            get
            {
                return this.whichComparison;
            }
            set
            {
                this.whichComparison = value;
            }
        }
    }
}

