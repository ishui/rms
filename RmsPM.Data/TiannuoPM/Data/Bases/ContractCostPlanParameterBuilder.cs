namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostPlanParameterBuilder : ParameterizedSqlFilterBuilder<ContractCostPlanColumn>
    {
        public ContractCostPlanParameterBuilder()
        {
        }

        public ContractCostPlanParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostPlanParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

