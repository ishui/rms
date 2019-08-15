namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ProjectParameterBuilder : ParameterizedSqlFilterBuilder<ProjectColumn>
    {
        public ProjectParameterBuilder()
        {
        }

        public ProjectParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ProjectParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

