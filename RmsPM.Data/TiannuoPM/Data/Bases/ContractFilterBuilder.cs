namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractFilterBuilder : SqlFilterBuilder<ContractColumn>
    {
        public ContractFilterBuilder()
        {
        }

        public ContractFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

