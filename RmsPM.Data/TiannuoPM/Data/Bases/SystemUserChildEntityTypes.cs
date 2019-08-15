namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum SystemUserChildEntityTypes
    {
        [ChildEntityType(typeof(TList<InspectSituation>))]
        InspectSituationCollection = 0
    }
}

