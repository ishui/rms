namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostParameterBuilder : ParameterizedSqlFilterBuilder<ContractCostColumn>
    {
        public ContractCostParameterBuilder()
        {
        }

        public ContractCostParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

