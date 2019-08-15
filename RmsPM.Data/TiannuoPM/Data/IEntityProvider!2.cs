namespace TiannuoPM.Data
{
    using System;
    using System.Runtime.InteropServices;
    using TiannuoPM.Entities;

    public interface IEntityProvider<Entity, EntityKey> where Entity: IEntityId<EntityKey>, new() where EntityKey: IEntityKey, new()
    {
        bool Delete(Entity entity);
        Entity Get(EntityKey key);
        TList<Entity> GetAll();
        TList<Entity> GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count);
        bool Insert(Entity entity);
        bool Update(Entity entity);
    }
}

