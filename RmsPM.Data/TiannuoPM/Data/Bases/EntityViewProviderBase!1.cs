namespace TiannuoPM.Data.Bases
{
    using System;

    [Serializable, CLSCompliant(true)]
    public abstract class EntityViewProviderBase<Entity> : EntityViewProviderBaseCore<Entity> where Entity: new()
    {
        protected EntityViewProviderBase()
        {
        }
    }
}

