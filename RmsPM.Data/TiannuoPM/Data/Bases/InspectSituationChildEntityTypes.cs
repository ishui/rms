namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum InspectSituationChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.Project))]
        Project = 0,
        [ChildEntityType(typeof(TiannuoPM.Entities.SystemUser))]
        SystemUser = 1,
        [ChildEntityType(typeof(TList<Trouble>))]
        TroubleCollection = 2
    }
}

