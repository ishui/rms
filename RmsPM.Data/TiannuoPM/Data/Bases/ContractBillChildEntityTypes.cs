namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractBillChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 0
    }
}

