namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialParameterBuilder : ParameterizedSqlFilterBuilder<MaterialColumn>
    {
        public MaterialParameterBuilder()
        {
        }

        public MaterialParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

