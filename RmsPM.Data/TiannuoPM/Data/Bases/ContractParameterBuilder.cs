namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractParameterBuilder : ParameterizedSqlFilterBuilder<ContractColumn>
    {
        public ContractParameterBuilder()
        {
        }

        public ContractParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

