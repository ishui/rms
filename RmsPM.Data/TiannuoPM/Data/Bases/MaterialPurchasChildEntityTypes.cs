namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum MaterialPurchasChildEntityTypes
    {
        [ChildEntityType(typeof(TList<MaterialPurchasDtl>))]
        MaterialPurchasDtlCollection = 1,
        [ChildEntityType(typeof(TiannuoPM.Entities.Project))]
        Project = 0
    }
}

