namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialPurchasFilterBuilder : SqlFilterBuilder<MaterialPurchasColumn>
    {
        public MaterialPurchasFilterBuilder()
        {
        }

        public MaterialPurchasFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialPurchasFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

