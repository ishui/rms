namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum PayoutChildEntityTypes
    {
        [ChildEntityType(typeof(TList<PayoutItem>))]
        PayoutItemCollection = 1,
        [ChildEntityType(typeof(TiannuoPM.Entities.Project))]
        Project = 0
    }
}

