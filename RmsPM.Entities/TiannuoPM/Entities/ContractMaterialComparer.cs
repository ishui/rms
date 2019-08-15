namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractMaterialComparer : IComparer<ContractMaterial>
    {
        private ContractMaterialColumn whichComparison;

        public ContractMaterialComparer()
        {
        }

        public ContractMaterialComparer(ContractMaterialColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractMaterial a, ContractMaterial b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractMaterial a, ContractMaterial b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractMaterial entity)
        {
            return entity.GetHashCode();
        }

        public ContractMaterialColumn WhichComparison
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

