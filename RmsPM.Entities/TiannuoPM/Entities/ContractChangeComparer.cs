namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractChangeComparer : IComparer<ContractChange>
    {
        private ContractChangeColumn whichComparison;

        public ContractChangeComparer()
        {
        }

        public ContractChangeComparer(ContractChangeColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractChange a, ContractChange b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractChange a, ContractChange b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractChange entity)
        {
            return entity.GetHashCode();
        }

        public ContractChangeColumn WhichComparison
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

