namespace TiannuoPM.Data
{
    using System;
    using System.Runtime.InteropServices;
    using TiannuoPM.Entities;

    public interface IEntityViewProvider<Entity> where Entity: new()
    {
        VList<Entity> Find(SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count);
        VList<Entity> Get(string whereClause, string orderBy, int start, int pageLength, out int count);
        VList<Entity> GetAll(int start, int pageLength, out int count);
        VList<Entity> GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count);
    }
}

