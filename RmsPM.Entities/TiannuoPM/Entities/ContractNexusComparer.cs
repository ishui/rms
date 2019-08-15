namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractNexusComparer : IComparer<ContractNexus>
    {
        private ContractNexusColumn whichComparison;

        public ContractNexusComparer()
        {
        }

        public ContractNexusComparer(ContractNexusColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractNexus a, ContractNexus b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractNexus a, ContractNexus b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractNexus entity)
        {
            return entity.GetHashCode();
        }

        public ContractNexusColumn WhichComparison
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

