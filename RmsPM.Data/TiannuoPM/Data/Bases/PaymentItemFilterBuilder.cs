namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PaymentItemFilterBuilder : SqlFilterBuilder<PaymentItemColumn>
    {
        public PaymentItemFilterBuilder()
        {
        }

        public PaymentItemFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PaymentItemFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

