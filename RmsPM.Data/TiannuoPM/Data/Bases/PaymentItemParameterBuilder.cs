namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PaymentItemParameterBuilder : ParameterizedSqlFilterBuilder<PaymentItemColumn>
    {
        public PaymentItemParameterBuilder()
        {
        }

        public PaymentItemParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PaymentItemParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

