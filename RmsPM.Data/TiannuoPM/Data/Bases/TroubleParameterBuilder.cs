namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class TroubleParameterBuilder : ParameterizedSqlFilterBuilder<TroubleColumn>
    {
        public TroubleParameterBuilder()
        {
        }

        public TroubleParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public TroubleParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

