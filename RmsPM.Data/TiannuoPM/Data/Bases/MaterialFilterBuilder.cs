namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialFilterBuilder : SqlFilterBuilder<MaterialColumn>
    {
        public MaterialFilterBuilder()
        {
        }

        public MaterialFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

