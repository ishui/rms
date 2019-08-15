namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum DictionaryItemChildEntityTypes
    {
        [ChildEntityType(typeof(TiannuoPM.Entities.DictionaryName))]
        DictionaryName = 0
    }
}

