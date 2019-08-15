namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PayoutItemParameterBuilder : ParameterizedSqlFilterBuilder<PayoutItemColumn>
    {
        public PayoutItemParameterBuilder()
        {
        }

        public PayoutItemParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PayoutItemParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

