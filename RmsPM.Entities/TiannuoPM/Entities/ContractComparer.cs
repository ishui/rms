namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractComparer : IComparer<Contract>
    {
        private ContractColumn whichComparison;

        public ContractComparer()
        {
        }

        public ContractComparer(ContractColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Contract a, Contract b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Contract a, Contract b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Contract entity)
        {
            return entity.GetHashCode();
        }

        public ContractColumn WhichComparison
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

