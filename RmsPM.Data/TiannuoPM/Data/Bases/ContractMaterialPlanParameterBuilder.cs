namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractMaterialPlanParameterBuilder : ParameterizedSqlFilterBuilder<ContractMaterialPlanColumn>
    {
        public ContractMaterialPlanParameterBuilder()
        {
        }

        public ContractMaterialPlanParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractMaterialPlanParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

