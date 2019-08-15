namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractCostChangeComparer : IComparer<ContractCostChange>
    {
        private ContractCostChangeColumn whichComparison;

        public ContractCostChangeComparer()
        {
        }

        public ContractCostChangeComparer(ContractCostChangeColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractCostChange a, ContractCostChange b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractCostChange a, ContractCostChange b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractCostChange entity)
        {
            return entity.GetHashCode();
        }

        public ContractCostChangeColumn WhichComparison
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

