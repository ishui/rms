namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IEntity
    {
        event PropertyChangedEventHandler PropertyChanged;

        void AcceptChanges();
        void MarkToDelete();

        TiannuoPM.Entities.EntityState EntityState { get; }

        string EntityTrackingKey { get; set; }

        bool IsDeleted { get; }

        bool IsDirty { get; }

        bool IsEntityTracked { get; set; }

        bool IsNew { get; }

        bool IsValid { get; }

        object ParentCollection { get; set; }

        string[] TableColumns { get; }

        string TableName { get; }

        object Tag { get; set; }
    }
}

