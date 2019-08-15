namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostChangeFilterBuilder : SqlFilterBuilder<ContractCostChangeColumn>
    {
        public ContractCostChangeFilterBuilder()
        {
        }

        public ContractCostChangeFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostChangeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

