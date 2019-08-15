namespace TiannuoPM.Entities
{
    using System;

    public class EntityFactory : EntityFactoryBase, IEntityFactory
    {
        public EntityFactory()
        {
            base.CurrentEntityAssembly = typeof(EntityFactory).Assembly;
        }
    }
}

