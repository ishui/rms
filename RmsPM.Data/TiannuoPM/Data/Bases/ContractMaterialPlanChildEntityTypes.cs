namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractMaterialPlanChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.ContractMaterial))]
        ContractMaterial = 0
    }
}

