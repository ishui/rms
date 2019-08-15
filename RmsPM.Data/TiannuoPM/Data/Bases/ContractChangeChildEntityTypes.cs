namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractChangeChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 0,
        [ChildEntityType(typeof(TList<ContractCostChange>))]
        ContractCostChangeCollection = 1,
        [ChildEntityType(typeof(TList<ContractNexus>))]
        ContractNexusCollection = 2
    }
}

