namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum PaymentItemChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.ContractCost))]
        ContractCost = 0,
        [ChildEntityType(typeof(TiannuoPM.Entities.Payment))]
        Payment = 1,
        [ChildEntityType(typeof(TList<PayoutItem>))]
        PayoutItemCollection = 2
    }
}

