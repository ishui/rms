namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractMaterialPlanFilterBuilder : SqlFilterBuilder<ContractMaterialPlanColumn>
    {
        public ContractMaterialPlanFilterBuilder()
        {
        }

        public ContractMaterialPlanFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractMaterialPlanFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

