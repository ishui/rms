namespace TiannuoPM.Entities
{
    using System;

    public interface IEntityId<EntityKey> : IEntity where EntityKey: IEntityKey, new()
    {
        EntityKey EntityId { get; set; }
    }
}

