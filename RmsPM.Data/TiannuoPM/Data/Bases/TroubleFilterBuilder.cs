namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class TroubleFilterBuilder : SqlFilterBuilder<TroubleColumn>
    {
        public TroubleFilterBuilder()
        {
        }

        public TroubleFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public TroubleFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

