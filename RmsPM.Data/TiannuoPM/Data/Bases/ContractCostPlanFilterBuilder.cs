namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostPlanFilterBuilder : SqlFilterBuilder<ContractCostPlanColumn>
    {
        public ContractCostPlanFilterBuilder()
        {
        }

        public ContractCostPlanFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostPlanFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

