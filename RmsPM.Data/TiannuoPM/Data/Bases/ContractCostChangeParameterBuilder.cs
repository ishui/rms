namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostChangeParameterBuilder : ParameterizedSqlFilterBuilder<ContractCostChangeColumn>
    {
        public ContractCostChangeParameterBuilder()
        {
        }

        public ContractCostChangeParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostChangeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

