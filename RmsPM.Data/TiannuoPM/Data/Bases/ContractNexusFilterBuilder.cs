namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractNexusFilterBuilder : SqlFilterBuilder<ContractNexusColumn>
    {
        public ContractNexusFilterBuilder()
        {
        }

        public ContractNexusFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractNexusFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

