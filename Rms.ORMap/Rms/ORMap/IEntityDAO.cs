namespace Rms.ORMap
{
    using System;

    public interface IEntityDAO : IDisposable
    {
        void DeleteEntity(EntityData entitydata);
        void InsertEntity(EntityData entitydata);
        EntityData SelectbyPrimaryKey(object[] keyvalues);
        void UpdateEntity(EntityData entitydata);
    }
}

