namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractChangeFilterBuilder : SqlFilterBuilder<ContractChangeColumn>
    {
        public ContractChangeFilterBuilder()
        {
        }

        public ContractChangeFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractChangeFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

