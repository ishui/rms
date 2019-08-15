namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    [CLSCompliant(true)]
    public class ContractMaterialFilterBuilder : SqlFilterBuilder<ContractMaterialColumn>
    {
        public ContractMaterialFilterBuilder()
        {
        }

        public ContractMaterialFilterBuilder(bool ignoreCase) : base(ignoreCase)
        {
        }

        public ContractMaterialFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd)
        {
        }
    }
}

