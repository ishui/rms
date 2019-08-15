namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum MaterialChildEntityTypes
    {
        [ChildEntityType(typeof(TList<ContractMaterial>))]
        ContractMaterialCollection = 0
    }
}

