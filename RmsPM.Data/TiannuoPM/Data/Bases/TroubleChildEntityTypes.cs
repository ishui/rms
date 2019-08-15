namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum TroubleChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.InspectSituation))]
        InspectSituation = 0
    }
}

