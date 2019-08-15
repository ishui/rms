namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractMaterialChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 1,
        [ChildEntityType(typeof(TList<ContractMaterialPlan>))]
        ContractMaterialPlanCollection = 2,
        [ChildEntityType(typeof(TiannuoPM.Entities.Material))]
        Material = 0
    }
}

