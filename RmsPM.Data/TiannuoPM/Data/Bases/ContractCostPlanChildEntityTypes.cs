namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractCostPlanChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 0
    }
}

