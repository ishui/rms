namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractMaterialParameterBuilder : ParameterizedSqlFilterBuilder<ContractMaterialColumn>
    {
        public ContractMaterialParameterBuilder()
        {
        }

        public ContractMaterialParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractMaterialParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

