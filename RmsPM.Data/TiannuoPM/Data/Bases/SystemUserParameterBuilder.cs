namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class SystemUserParameterBuilder : ParameterizedSqlFilterBuilder<SystemUserColumn>
    {
        public SystemUserParameterBuilder()
        {
        }

        public SystemUserParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public SystemUserParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

