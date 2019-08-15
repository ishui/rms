namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PayoutFilterBuilder : SqlFilterBuilder<PayoutColumn>
    {
        public PayoutFilterBuilder()
        {
        }

        public PayoutFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PayoutFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

