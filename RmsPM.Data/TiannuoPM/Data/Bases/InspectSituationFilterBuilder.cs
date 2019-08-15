namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class InspectSituationFilterBuilder : SqlFilterBuilder<InspectSituationColumn>
    {
        public InspectSituationFilterBuilder()
        {
        }

        public InspectSituationFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public InspectSituationFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

