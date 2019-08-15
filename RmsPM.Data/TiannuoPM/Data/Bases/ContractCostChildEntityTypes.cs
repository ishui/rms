namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ContractCostChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 0,
        [ChildEntityType(typeof(TList<PaymentItem>))]
        PaymentItemCollection = 1
    }
}

