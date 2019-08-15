namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    public enum DictionaryNameChildEntityTypes
    {
        [ChildEntityType(typeof(TList<DictionaryItem>))]
        DictionaryItemCollection = 0
    }
}

