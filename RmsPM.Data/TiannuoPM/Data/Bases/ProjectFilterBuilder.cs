namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ProjectFilterBuilder : SqlFilterBuilder<ProjectColumn>
    {
        public ProjectFilterBuilder()
        {
        }

        public ProjectFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ProjectFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

