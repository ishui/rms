namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractAccountComparer : IComparer<ContractAccount>
    {
        private ContractAccountColumn whichComparison;

        public ContractAccountComparer()
        {
        }

        public ContractAccountComparer(ContractAccountColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractAccount a, ContractAccount b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractAccount a, ContractAccount b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractAccount entity)
        {
            return entity.GetHashCode();
        }

        public ContractAccountColumn WhichComparison
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

