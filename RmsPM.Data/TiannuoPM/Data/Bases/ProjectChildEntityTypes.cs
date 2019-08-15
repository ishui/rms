namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum ProjectChildEntityTypes
    {
        [ChildEntityType(typeof(TList<Contract>))]
        ContractCollection = 2,
        [ChildEntityType(typeof(TList<InspectSituation>))]
        InspectSituationCollection = 3,
        [ChildEntityType(typeof(TList<MaterialPurchas>))]
        MaterialPurchasCollection = 0,
        [ChildEntityType(typeof(TList<Payment>))]
        PaymentCollection = 4,
        [ChildEntityType(typeof(TList<Payout>))]
        PayoutCollection = 1
    }
}

