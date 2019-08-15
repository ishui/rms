namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ContractCostPlanComparer : IComparer<ContractCostPlan>
    {
        private ContractCostPlanColumn whichComparison;

        public ContractCostPlanComparer()
        {
        }

        public ContractCostPlanComparer(ContractCostPlanColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(ContractCostPlan a, ContractCostPlan b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(ContractCostPlan a, ContractCostPlan b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(ContractCostPlan entity)
        {
            return entity.GetHashCode();
        }

        public ContractCostPlanColumn WhichComparison
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

