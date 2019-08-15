namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PaymentFilterBuilder : SqlFilterBuilder<PaymentColumn>
    {
        public PaymentFilterBuilder()
        {
        }

        public PaymentFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PaymentFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

