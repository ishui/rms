namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialPurchasParameterBuilder : ParameterizedSqlFilterBuilder<MaterialPurchasColumn>
    {
        public MaterialPurchasParameterBuilder()
        {
        }

        public MaterialPurchasParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialPurchasParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

