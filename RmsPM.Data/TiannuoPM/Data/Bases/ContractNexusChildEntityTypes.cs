namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractNexusChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.ContractChange))]
        ContractChange = 0
    }
}

