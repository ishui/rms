namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class InspectSituationParameterBuilder : ParameterizedSqlFilterBuilder<InspectSituationColumn>
    {
        public InspectSituationParameterBuilder()
        {
        }

        public InspectSituationParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public InspectSituationParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

