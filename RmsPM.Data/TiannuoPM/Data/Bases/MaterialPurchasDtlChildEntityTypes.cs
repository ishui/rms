namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum MaterialPurchasDtlChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.MaterialPurchas))]
        MaterialPurchas = 0
    }
}

