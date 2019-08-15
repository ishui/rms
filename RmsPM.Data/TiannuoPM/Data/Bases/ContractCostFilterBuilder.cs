namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractCostFilterBuilder : SqlFilterBuilder<ContractCostColumn>
    {
        public ContractCostFilterBuilder()
        {
        }

        public ContractCostFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractCostFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

