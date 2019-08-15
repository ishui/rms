namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractAccountFilterBuilder : SqlFilterBuilder<ContractAccountColumn>
    {
        public ContractAccountFilterBuilder()
        {
        }

        public ContractAccountFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractAccountFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

