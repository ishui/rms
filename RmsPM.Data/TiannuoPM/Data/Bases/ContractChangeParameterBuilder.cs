namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractChangeParameterBuilder : ParameterizedSqlFilterBuilder<ContractChangeColumn>
    {
        public ContractChangeParameterBuilder()
        {
        }

        public ContractChangeParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractChangeParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

