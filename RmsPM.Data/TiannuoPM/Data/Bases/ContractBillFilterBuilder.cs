namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractBillFilterBuilder : SqlFilterBuilder<ContractBillColumn>
    {
        public ContractBillFilterBuilder()
        {
        }

        public ContractBillFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractBillFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

