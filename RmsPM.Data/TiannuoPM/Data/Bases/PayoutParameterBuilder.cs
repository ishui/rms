namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PayoutParameterBuilder : ParameterizedSqlFilterBuilder<PayoutColumn>
    {
        public PayoutParameterBuilder()
        {
        }

        public PayoutParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PayoutParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

