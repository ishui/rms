namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractCostComparer : IComparer<ContractCost>
    {
        private ContractCostColumn whichComparison;

        public ContractCostComparer()
        {
        }

        public ContractCostComparer(ContractCostColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractCost a, ContractCost b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractCost a, ContractCost b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractCost entity)
        {
            return entity.GetHashCode();
        }

        public ContractCostColumn WhichComparison
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

