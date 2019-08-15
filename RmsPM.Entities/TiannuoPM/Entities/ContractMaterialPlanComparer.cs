namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractMaterialPlanComparer : IComparer<ContractMaterialPlan>
    {
        private ContractMaterialPlanColumn whichComparison;

        public ContractMaterialPlanComparer()
        {
        }

        public ContractMaterialPlanComparer(ContractMaterialPlanColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractMaterialPlan a, ContractMaterialPlan b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractMaterialPlan a, ContractMaterialPlan b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractMaterialPlan entity)
        {
            return entity.GetHashCode();
        }

        public ContractMaterialPlanColumn WhichComparison
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

