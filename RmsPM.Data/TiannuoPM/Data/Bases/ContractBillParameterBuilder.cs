namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractBillParameterBuilder : ParameterizedSqlFilterBuilder<ContractBillColumn>
    {
        public ContractBillParameterBuilder()
        {
        }

        public ContractBillParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractBillParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

