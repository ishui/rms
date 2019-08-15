namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractCostChangeChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.ContractChange))]
        ContractChange = 0
    }
}

