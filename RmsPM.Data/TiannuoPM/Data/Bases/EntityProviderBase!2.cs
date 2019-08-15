namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;
    [Serializable, CLSCompliant(true)]
    public abstract class EntityProviderBase<Entity, EntityKey> : EntityProviderBaseCore<Entity, EntityKey> where Entity: IEntityId<EntityKey>, new() where EntityKey: IEntityKey, new()
    {
        protected EntityProviderBase()
        {
        }
    }
}

