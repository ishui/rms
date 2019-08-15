namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialPurchasDtlFilterBuilder : SqlFilterBuilder<MaterialPurchasDtlColumn>
    {
        public MaterialPurchasDtlFilterBuilder()
        {
        }

        public MaterialPurchasDtlFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialPurchasDtlFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

