namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class SystemUserFilterBuilder : SqlFilterBuilder<SystemUserColumn>
    {
        public SystemUserFilterBuilder()
        {
        }

        public SystemUserFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public SystemUserFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

