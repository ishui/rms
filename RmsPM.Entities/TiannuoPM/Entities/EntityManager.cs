namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public static class EntityManager
    {
        private static Dictionary<string, IEntityFactory> entityFactoryList = new Dictionary<string, IEntityFactory>();
        private static TiannuoPM.Entities.EntityLocator entityLocator = new TiannuoPM.Entities.EntityLocator();
        private static object syncObject = new object();

        public static Entity CreateViewEntity<Entity>(string typeString, Type entityFactoryType) where Entity: class, new()
        {
            if (string.IsNullOrEmpty(typeString))
            {
                throw new ArgumentException("typeString");
            }
            if (entityFactoryType == null)
            {
                throw new ArgumentException("entityFactoryType");
            }
            Entity local = default(Entity);
            Type defaultType = typeof(Entity);
            IEntityFactory factory = null;
            if (EntityFactories.ContainsKey(entityFactoryType.FullName))
            {
                factory = EntityFactories[entityFactoryType.FullName];
            }
            else
            {
                factory = TryAddEntityFactory(entityFactoryType);
            }
            return (factory.CreateViewEntity(typeString, defaultType) as Entity);
        }

        public static Entity LocateEntity<Entity>(string key, bool isLocatorEnabled) where Entity: class, IEntity, new()
        {
            Entity local = default(Entity);
            if (((key != null) && isLocatorEnabled) && EntityLocator.Contains(key))
            {
                local = EntityLocator.Get(key) as Entity;
            }
            return local;
        }

        public static Entity LocateOrCreate<Entity>(string key, string typeString, Type entityFactoryType) where Entity: class, IEntity, new()
        {
            return LocateOrCreate<Entity>(key, typeString, entityFactoryType, true);
        }

        public static Entity LocateOrCreate<Entity>(string key, string typeString, Type entityFactoryType, bool isLocatorEnabled) where Entity: class, IEntity, new()
        {
            if (string.IsNullOrEmpty(typeString))
            {
                throw new ArgumentException("typeString");
            }
            if (entityFactoryType == null)
            {
                throw new ArgumentException("entityFactoryType");
            }
            Entity entity = default(Entity);
            Type defaultType = typeof(Entity);
            if (defaultType.GetInterface("IEntityCacheItem") != null)
            {
                entity = EntityCache.GetItem<Entity>(key.ToString());
            }
            if (entity == null)
            {
                IEntityFactory factory = null;
                if (EntityFactories.ContainsKey(entityFactoryType.FullName))
                {
                    factory = EntityFactories[entityFactoryType.FullName];
                }
                else
                {
                    factory = TryAddEntityFactory(entityFactoryType);
                }
                if (((key != null) && isLocatorEnabled) && EntityLocator.Contains(key))
                {
                    entity = EntityLocator.Get(key) as Entity;
                }
                if (entity == null)
                {
                    entity = factory.CreateEntity(typeString, defaultType) as Entity;
                }
                if (!entity.IsEntityTracked)
                {
                    StartTracking(key, entity, isLocatorEnabled);
                }
                if (entity.GetType().GetInterface("IEntityCacheItem") != null)
                {
                    EntityCache.AddCache(key, entity);
                }
            }
            return entity;
        }

        public static void StartTracking(string key, IEntity entity, bool isTrackingEnabled)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!(entity.IsEntityTracked || !isTrackingEnabled))
            {
                EntityLocator.Add(key, entity);
                entity.IsEntityTracked = true;
                entity.EntityTrackingKey = key;
            }
        }

        public static bool StopTracking(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return EntityLocator.Remove(key);
        }

        public static IEntityFactory TryAddEntityFactory(Type entityFactoryTypeToCreate)
        {
            lock (syncObject)
            {
                if (entityFactoryTypeToCreate == null)
                {
                    throw new ArgumentException("entityFactoryTypeToCreate");
                }
                if (!EntityFactories.ContainsKey(entityFactoryTypeToCreate.FullName))
                {
                    IEntityFactory factory = Activator.CreateInstance(entityFactoryTypeToCreate) as IEntityFactory;
                    if (factory == null)
                    {
                        throw new ArgumentException("This factory can not be found.  Please ensure that you are using a valid Entity Factory.", "entityFactoryType");
                    }
                    EntityFactories.Add(entityFactoryTypeToCreate.FullName, factory);
                }
            }
            return EntityFactories[entityFactoryTypeToCreate.FullName];
        }

        public static Dictionary<string, IEntityFactory> EntityFactories
        {
            get
            {
                return entityFactoryList;
            }
            set
            {
                if (value != null)
                {
                    lock (syncObject)
                    {
                        entityFactoryList = value;
                    }
                }
            }
        }

        public static TiannuoPM.Entities.EntityLocator EntityLocator
        {
            get
            {
                return entityLocator;
            }
        }
    }
}

