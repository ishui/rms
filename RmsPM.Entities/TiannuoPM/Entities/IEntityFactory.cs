namespace TiannuoPM.Entities
{
    using System;
    using System.Reflection;

    public interface IEntityFactory
    {
        IEntity CreateEntity(string typeString, Type defaultType);
        object CreateViewEntity(string typeString, Type defaultType);

        Assembly CurrentEntityAssembly { get; set; }
    }
}

