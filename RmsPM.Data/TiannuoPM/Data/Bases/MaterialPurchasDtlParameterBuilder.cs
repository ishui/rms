namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class MaterialPurchasDtlParameterBuilder : ParameterizedSqlFilterBuilder<MaterialPurchasDtlColumn>
    {
        public MaterialPurchasDtlParameterBuilder()
        {
        }

        public MaterialPurchasDtlParameterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public MaterialPurchasDtlParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

