namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum PayoutItemChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.PaymentItem))]
        PaymentItem = 1,
        [ChildEntityType(typeof(TiannuoPM.Entities.Payout))]
        Payout = 0
    }
}

