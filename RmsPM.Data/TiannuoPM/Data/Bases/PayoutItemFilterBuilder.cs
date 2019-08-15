namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class PayoutItemFilterBuilder : SqlFilterBuilder<PayoutItemColumn>
    {
        public PayoutItemFilterBuilder()
        {
        }

        public PayoutItemFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public PayoutItemFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

