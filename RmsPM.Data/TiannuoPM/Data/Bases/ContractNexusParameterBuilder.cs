namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractNexusParameterBuilder : ParameterizedSqlFilterBuilder<ContractNexusColumn>
    {
        public ContractNexusParameterBuilder()
        {
        }

        public ContractNexusParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractNexusParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

