namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum PaymentChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Contract))]
        Contract = 1,
        [ChildEntityType(typeof(TList<PaymentItem>))]
        PaymentItemCollection = 2,
        [ChildEntityType(typeof(TiannuoPM.Entities.Project))]
        Project = 0
    }
}

