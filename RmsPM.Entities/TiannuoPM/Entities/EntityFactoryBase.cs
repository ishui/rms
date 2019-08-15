namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class EntityFactoryBase : IEntityFactory
    {
        protected static Assembly currentAssembly = typeof(EntityFactoryBase).Assembly;
        private static string currentEntityAssemblyNamespace = defaultCreationalNamespace;
        protected static string defaultCreationalNamespace = typeof(EntityFactoryBase).Namespace;
        protected static Dictionary<string, Type> internalTypeCache = new Dictionary<string, Type>();
        protected static object syncRoot = new object();

        [field: NonSerialized]
        public static  event EntityCreatedEventHandler EntityCreated;

        [field: NonSerialized]
        public static  event EntityCreatingEventHandler EntityCreating;

        protected EntityFactoryBase()
        {
        }

        private static object CoreCreate(Type type)
        {
            if (!internalTypeCache.ContainsValue(type))
            {
                lock (syncRoot)
                {
                    internalTypeCache.Add(type.FullName, type);
                }
            }
            OnEntityCreating(type);
            object obj2 = Activator.CreateInstance(type);
            if (obj2 == null)
            {
                throw new ArgumentException(string.Format("This type '{0}' can not be resolved correctly to instatiate your entity.  Please ensure that your NetTiersService Section is correct in the configuration file.", type.FullName));
            }
            OnEntityCreated(obj2 as IEntity, type);
            return obj2;
        }

        private static object CoreCreate(string typeString, Type defaultType)
        {
            if (string.IsNullOrEmpty(typeString))
            {
                throw new ArgumentException("Entity can not be null or empty when being passed into the factory.");
            }
            typeString = string.Format("{0}.{1}", currentEntityAssemblyNamespace, typeString);
            if (internalTypeCache.ContainsKey(typeString))
            {
                return CoreCreate(internalTypeCache[typeString]);
            }
            Type type = ResolveType(typeString);
            if (type == null)
            {
                if (defaultType == null)
                {
                    if (defaultCreationalNamespace == null)
                    {
                        throw new ArgumentException(string.Format("This type '{0}' can not be resolved.  Please ensure that your NetTiersService Section is correct in the configuration file.", typeString));
                    }
                    type = ResolveType(typeString, defaultCreationalNamespace);
                }
                else
                {
                    type = defaultType;
                }
            }
            return CoreCreate(type);
        }

        public static T Create<T>() where T: IEntity, new()
        {
            Type type = typeof(T);
            if (!internalTypeCache.ContainsValue(type))
            {
                internalTypeCache.Add(type.FullName, type);
            }
            OnEntityCreating(type);
            T entity = new T();
            OnEntityCreated(entity, type);
            return entity;
        }

        public static IEntity Create(string typeString)
        {
            return Create(typeString, null);
        }

        public static IEntity Create(Type type)
        {
            if (type.GetInterface("IEntity") == null)
            {
                throw new ArgumentException("Type Parameter must implement the IEntity interface.");
            }
            return (CoreCreate(type) as IEntity);
        }

        public static IEntity Create(string typeString, Type defaultType)
        {
            return (CoreCreate(typeString, defaultType) as IEntity);
        }

        public virtual IEntity CreateEntity(string typeString, Type defaultType)
        {
            return Create(typeString, defaultType);
        }

        public static object CreateReadOnlyEntity(Type type)
        {
            return CoreCreate(type);
        }

        public static object CreateReadOnlyEntity(string typeString, Type defaultType)
        {
            return CoreCreate(typeString, defaultType);
        }

        public virtual object CreateViewEntity(string typeString, Type defaultType)
        {
            return CreateReadOnlyEntity(typeString, defaultType);
        }

        public static void FlushTypeCache()
        {
            internalTypeCache.Clear();
        }

        private static string GetAssemblyNameFromString(string typeString)
        {
            int index = typeString.IndexOf(",");
            if ((index > 0) && (typeString.Length >= (index + 1)))
            {
                return typeString.Substring(index + 1).Trim();
            }
            return typeString;
        }

        private static string GetClassNameFromString(string typeString)
        {
            int length = typeString.IndexOf(",");
            if (length > 0)
            {
                return typeString.Substring(0, length).Trim();
            }
            return typeString;
        }

        private static void OnEntityCreated(IEntity entity, Type type)
        {
            EntityCreatedEventHandler entityCreated = EntityCreated;
            if (entityCreated != null)
            {
                entityCreated(null, new EntityEventArgs(entity, type));
            }
        }

        private static void OnEntityCreating(Type type)
        {
            EntityCreatingEventHandler entityCreating = EntityCreating;
            if (entityCreating != null)
            {
                entityCreating(null, new EntityEventArgs(null, type));
            }
        }

        private static Type ResolveType(string typeString)
        {
            return currentAssembly.GetType(typeString, false, true);
        }

        private static Type ResolveType(string typeString, string defaultNamespace)
        {
            return currentAssembly.GetType(string.Format("{0}.{1}", defaultNamespace, typeString), false, true);
        }

        public virtual Assembly CurrentEntityAssembly
        {
            get
            {
                return currentAssembly;
            }
            set
            {
                if (value != null)
                {
                    lock (syncRoot)
                    {
                        currentAssembly = value;
                        this.CurrentEntityAssemblyNamespace = currentAssembly.FullName.Split(new char[] { ',' })[0];
                    }
                }
            }
        }

        public virtual string CurrentEntityAssemblyNamespace
        {
            get
            {
                return currentEntityAssemblyNamespace;
            }
            set
            {
                if (value != null)
                {
                    lock (syncRoot)
                    {
                        currentEntityAssemblyNamespace = value;
                    }
                }
            }
        }

        public delegate void EntityCreatedEventHandler(object sender, EntityFactoryBase.EntityEventArgs e);

        public delegate void EntityCreatingEventHandler(object sender, EntityFactoryBase.EntityEventArgs e);

        public class EntityEventArgs : EventArgs
        {
            private Type creationalType;
            private IEntity entity;

            public EntityEventArgs(IEntity entity, Type type)
            {
                this.entity = entity;
                this.creationalType = type;
            }

            public Type CreationalType
            {
                get
                {
                    return this.creationalType;
                }
            }

            public IEntity Entity
            {
                get
                {
                    return this.entity;
                }
            }
        }
    }
}

