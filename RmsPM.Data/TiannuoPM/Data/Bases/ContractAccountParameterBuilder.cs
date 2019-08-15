namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractAccountParameterBuilder : ParameterizedSqlFilterBuilder<ContractAccountColumn>
    {
        public ContractAccountParameterBuilder()
        {
        }

        public ContractAccountParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractAccountParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

