namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PaymentParameterBuilder : ParameterizedSqlFilterBuilder<PaymentColumn>
    {
        public PaymentParameterBuilder()
        {
        }

        public PaymentParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PaymentParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

