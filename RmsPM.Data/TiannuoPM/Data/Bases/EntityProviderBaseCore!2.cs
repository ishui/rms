namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    [Serializable, CLSCompliant(true)]
    public abstract class EntityProviderBaseCore<Entity, EntityKey> : IEntityProvider<Entity, EntityKey> where Entity: IEntityId<EntityKey>, new() where EntityKey: IEntityKey, new()
    {
        protected EntityProviderBaseCore()
        {
        }

        public virtual void BulkInsert(TList<Entity> entities)
        {
            this.BulkInsert(null, entities);
        }

        public abstract void BulkInsert(TransactionManager mgr, TList<Entity> entities);
        protected bool CanDeepLoad(IEntity entity, string key, string property, DeepLoadType deepLoadType, ChildEntityTypesList innerList)
        {
            if (innerList != null)
            {
                if (deepLoadType == DeepLoadType.ExcludeChildren)
                {
                    if (!(innerList.Contains(key) || innerList.HasProperty(entity, key, property)))
                    {
                        innerList.AddProperty(entity, key, property);
                        return true;
                    }
                }
                else if ((deepLoadType == DeepLoadType.IncludeChildren) && !(!innerList.Contains(key) || innerList.HasProperty(entity, key, property)))
                {
                    innerList.AddProperty(entity, key, property);
                    return true;
                }
            }
            return false;
        }

        protected bool CanDeepSave(IEntity entity, string key, string property, DeepSaveType deepSaveType, ChildEntityTypesList innerList)
        {
            if (innerList != null)
            {
                if (deepSaveType == DeepSaveType.ExcludeChildren)
                {
                    if (!(innerList.Contains(key) || innerList.HasProperty(entity, key, property)))
                    {
                        innerList.AddProperty(entity, key, property);
                        return true;
                    }
                }
                else if ((deepSaveType == DeepSaveType.IncludeChildren) && (innerList.Contains(key) && innerList.HasProperty(entity, key, property)))
                {
                    innerList.AddProperty(entity, key, property);
                    return true;
                }
            }
            return false;
        }

        public virtual void DeepLoad(Entity entity)
        {
            this.DeepLoad(null, entity);
        }

        public virtual void DeepLoad(TList<Entity> entities)
        {
            this.DeepLoad(null, entities);
        }

        public virtual void DeepLoad(TransactionManager mgr, TList<Entity> entities)
        {
            this.DeepLoad(mgr, entities, false, DeepLoadType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual void DeepLoad(TList<Entity> entities, bool deep)
        {
            this.DeepLoad(null, entities, deep);
        }

        public virtual void DeepLoad(Entity entity, bool deep)
        {
            this.DeepLoad(null, entity, deep);
        }

        public virtual void DeepLoad(TransactionManager mgr, Entity entity)
        {
            this.DeepLoad(mgr, entity, false, DeepLoadType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual void DeepLoad(TransactionManager mgr, TList<Entity> entities, bool deep)
        {
            this.DeepLoad(mgr, entities, deep, DeepLoadType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual void DeepLoad(TransactionManager mgr, Entity entity, bool deep)
        {
            this.DeepLoad(mgr, entity, deep, DeepLoadType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual void DeepLoad(Entity entity, bool deep, DeepLoadType deepLoadType, params Type[] childTypes)
        {
            this.DeepLoad(null, entity, deep, deepLoadType, childTypes);
        }

        public virtual void DeepLoad(TList<Entity> entities, bool deep, DeepLoadType deepLoadType, params Type[] childTypes)
        {
            this.DeepLoad(null, entities, deep, deepLoadType, childTypes);
        }

        public virtual void DeepLoad(TransactionManager mgr, TList<Entity> entities, bool deep, DeepLoadType deepLoadType, params Type[] childTypes)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities", "A valid non-null, TList<Entity> object is not present.");
            }
            if (!Enum.IsDefined(typeof(DeepLoadType), deepLoadType))
            {
                throw new ArgumentException("A valid DeepLoadType option is not present.", deepLoadType.ToString());
            }
            if (childTypes == null)
            {
                throw new ArgumentNullException("childTypes", "A valid Type[] array is not present.");
            }
            if (deepLoadType != DeepLoadType.Ignore)
            {
                foreach (Entity local in entities)
                {
                    this.DeepLoad(mgr, local, deep, deepLoadType, childTypes);
                }
            }
        }

        public virtual void DeepLoad(TransactionManager mgr, Entity entity, bool deep, DeepLoadType deepLoadType, params Type[] childTypes)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The argument entity, can not be null.");
            }
            if (!Enum.IsDefined(typeof(DeepLoadType), deepLoadType))
            {
                throw new ArgumentException("A valid DeepLoadType option is not present.", "DeepLoadType");
            }
            if (childTypes == null)
            {
                throw new ArgumentNullException("childTypes", "A valid Type[] array is not present.");
            }
            if ((deepLoadType != DeepLoadType.Ignore) && (entity.EntityState != EntityState.Added))
            {
                ChildEntityTypesList innerList = new ChildEntityTypesList();
                for (int i = 0; i < childTypes.Length; i++)
                {
                    if (childTypes[i] != null)
                    {
                        if (!childTypes[i].IsGenericType)
                        {
                            innerList.Add(childTypes[i].Name);
                        }
                        else
                        {
                            innerList.Add(string.Format("List<{0}>", childTypes[i].GetGenericArguments()[0].Name));
                        }
                    }
                }
                this.DeepLoad(mgr, entity, deep, deepLoadType, childTypes, innerList);
            }
        }

        internal abstract void DeepLoad(TransactionManager mgr, Entity entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList);
        internal void DeepLoad(TransactionManager mgr, TList<Entity> entities, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities", "A valid non-null, TList<Entity> object is not present.");
            }
            if (!Enum.IsDefined(typeof(DeepLoadType), deepLoadType))
            {
                throw new ArgumentException("A valid DeepLoadType option is not present.", deepLoadType.ToString());
            }
            if (childTypes == null)
            {
                throw new ArgumentNullException("childTypes", "A valid Type[] array is not present.");
            }
            if (deepLoadType != DeepLoadType.Ignore)
            {
                foreach (Entity local in entities)
                {
                    this.DeepLoad(mgr, local, deep, deepLoadType, childTypes, innerList);
                    innerList.Keys.Remove(local.EntityTrackingKey);
                }
            }
        }

        public virtual bool DeepSave(Entity entity)
        {
            return this.DeepSave(null, entity, DeepSaveType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual bool DeepSave(TList<Entity> entities)
        {
            return this.DeepSave(null, entities, DeepSaveType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual bool DeepSave(TransactionManager mgr, TList<Entity> entities)
        {
            return this.DeepSave(mgr, entities, DeepSaveType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual bool DeepSave(TransactionManager mgr, Entity entity)
        {
            return this.DeepSave(mgr, entity, DeepSaveType.ExcludeChildren, Type.EmptyTypes);
        }

        public virtual bool DeepSave(TList<Entity> entities, DeepSaveType deepSaveType, params Type[] childTypes)
        {
            return this.DeepSave(null, entities, deepSaveType, childTypes);
        }

        public virtual bool DeepSave(Entity entity, DeepSaveType deepSaveType, params Type[] childTypes)
        {
            return this.DeepSave(null, entity, deepSaveType, childTypes);
        }

        public virtual bool DeepSave(TransactionManager mgr, Entity entity, DeepSaveType deepSaveType, params Type[] childTypes)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "The argument entity, can not be null.");
            }
            if (!Enum.IsDefined(typeof(DeepSaveType), deepSaveType))
            {
                throw new ArgumentNullException("A valid DeepSaveType option is not present.", "deepSaveType");
            }
            if (childTypes == null)
            {
                throw new ArgumentNullException("childTypes", "A valid Type[] array is not present.");
            }
            if (deepSaveType != DeepSaveType.Ignore)
            {
                ChildEntityTypesList innerList = new ChildEntityTypesList();
                for (int i = 0; i < childTypes.Length; i++)
                {
                    if (childTypes[i] != null)
                    {
                        if (!childTypes[i].IsGenericType)
                        {
                            innerList.Add(childTypes[i].Name);
                        }
                        else
                        {
                            innerList.Add(string.Format("List<{0}>", childTypes[i].GetGenericArguments()[0].Name));
                        }
                    }
                }
                this.DeepSave(mgr, entity, deepSaveType, childTypes, innerList);
            }
            return true;
        }

        public virtual bool DeepSave(TransactionManager mgr, TList<Entity> entities, DeepSaveType deepSaveType, params Type[] childTypes)
        {
            return this.DeepSave(mgr, entities, deepSaveType, childTypes, null);
        }

        internal virtual bool DeepSave(TransactionManager mgr, TList<Entity> entities, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entityCollection", "A valid non-null, TList<Entity> object is not present.");
            }
            if (!Enum.IsDefined(typeof(DeepSaveType), deepSaveType))
            {
                throw new ArgumentException("A valid DeepSaveType option is not present.", "deepSaveType");
            }
            if (childTypes == null)
            {
                throw new ArgumentNullException("childTypes", "A valid Type[] array is not present.");
            }
            if (deepSaveType == DeepSaveType.Ignore)
            {
                return true;
            }
            bool flag = true;
            if (innerList == null)
            {
                innerList = new ChildEntityTypesList();
                for (int i = 0; i < childTypes.Length; i++)
                {
                    if (childTypes[i] != null)
                    {
                        if (!childTypes[i].IsGenericType)
                        {
                            innerList.Add(childTypes[i].Name);
                        }
                        else
                        {
                            innerList.Add(string.Format("List<{0}>", childTypes[i].GetGenericArguments()[0].Name));
                        }
                    }
                }
            }
            foreach (Entity local in entities)
            {
                if (!this.DeepSave(mgr, local, deepSaveType, childTypes, innerList))
                {
                    flag = false;
                }
            }
            foreach (Entity local in entities.DeletedItems)
            {
                if (!this.DeepSave(mgr, local, deepSaveType, childTypes, innerList))
                {
                    flag = false;
                }
            }
            entities.DeletedItems.Clear();
            return flag;
        }

        internal abstract bool DeepSave(TransactionManager mgr, Entity entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList);
        public virtual bool Delete(Entity entity)
        {
            return this.Delete(null, entity);
        }

        public virtual int Delete(TList<Entity> entities)
        {
            return this.Delete(null, entities);
        }

        public virtual bool Delete(EntityKey key)
        {
            return this.Delete(null, key);
        }

        public virtual int Delete(TransactionManager mgr, TList<Entity> entities)
        {
            int num = 0;
            foreach (Entity local in entities)
            {
                if (this.Delete(mgr, local))
                {
                    num++;
                }
            }
            return num;
        }

        public virtual bool Delete(TransactionManager mgr, Entity entity)
        {
            return this.Delete(mgr, entity.EntityId);
        }

        public abstract bool Delete(TransactionManager mgr, EntityKey key);
        public virtual TList<Entity> Find(string whereClause)
        {
            int count = -1;
            return this.Find((TransactionManager) null, whereClause, 0, 0x7fffffff, out count);
        }

        public virtual TList<Entity> Find(SqlFilterParameterCollection parameters)
        {
            return this.Find(null, parameters);
        }

        public virtual TList<Entity> Find(SqlFilterParameterCollection parameters, string orderBy)
        {
            return this.Find(null, parameters, orderBy);
        }

        public virtual TList<Entity> Find(TransactionManager transactionManager, string whereClause)
        {
            int count = -1;
            return this.Find(transactionManager, whereClause, 0, 0x7fffffff, out count);
        }

        public virtual TList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters)
        {
            return this.Find(transactionManager, parameters, null);
        }

        public virtual TList<Entity> Find(string whereClause, int start, int pageLength)
        {
            int count = -1;
            return this.Find((TransactionManager) null, whereClause, start, pageLength, out count);
        }

        public virtual TList<Entity> Find(TransactionManager transactionManager, string whereClause, out int count)
        {
            return this.Find(transactionManager, whereClause, 0, 0x7fffffff, out count);
        }

        public virtual TList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy)
        {
            int count = 0;
            return this.Find(transactionManager, parameters, orderBy, 0, 0x7fffffff, out count);
        }

        public virtual TList<Entity> Find(string whereClause, int start, int pageLength, out int count)
        {
            return this.Find((TransactionManager) null, whereClause, start, pageLength, out count);
        }

        public virtual TList<Entity> Find(SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            return this.Find(null, parameters, orderBy, start, pageLength, out count);
        }

        public abstract TList<Entity> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count);
        public virtual TList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            count = 0;
            return null;
        }

        public virtual Entity Get(EntityKey key)
        {
            return this.Get(null, key);
        }

        public virtual Entity Get(TransactionManager mgr, EntityKey key)
        {
            return this.Get(mgr, key, 0, 0x7fffffff);
        }

        public virtual Entity Get(EntityKey key, int start, int pageLength)
        {
            return this.Get(null, key, start, pageLength);
        }

        public abstract Entity Get(TransactionManager mgr, EntityKey key, int start, int pageLength);
        public virtual TList<Entity> GetAll()
        {
            return this.GetAll(null);
        }

        public virtual TList<Entity> GetAll(TransactionManager mgr)
        {
            return this.GetAll(mgr, 0, 0x7fffffff);
        }

        public virtual TList<Entity> GetAll(int start, int pageLength)
        {
            return this.GetAll(null, start, pageLength);
        }

        public virtual TList<Entity> GetAll(int start, int pageLength, out int count)
        {
            return this.GetAll(null, start, pageLength, out count);
        }

        public virtual TList<Entity> GetAll(TransactionManager mgr, int start, int pageLength)
        {
            int count;
            return this.GetAll(mgr, start, pageLength, out count);
        }

        public abstract TList<Entity> GetAll(TransactionManager mgr, int start, int pageLength, out int count);
        public virtual TList<Entity> GetPaged(out int count)
        {
            return this.GetPaged(null, out count);
        }

        public virtual TList<Entity> GetPaged(TransactionManager mgr, out int count)
        {
            return this.GetPaged(mgr, string.Empty, string.Empty, 0, 0, out count);
        }

        public virtual TList<Entity> GetPaged(int start, int pageLength, out int count)
        {
            return this.GetPaged(null, start, pageLength, out count);
        }

        public virtual TList<Entity> GetPaged(TransactionManager mgr, int start, int pageLength, out int count)
        {
            return this.GetPaged(mgr, string.Empty, string.Empty, start, pageLength, out count);
        }

        public virtual TList<Entity> GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            return this.GetPaged(null, whereClause, orderBy, start, pageLength, out count);
        }

        public abstract TList<Entity> GetPaged(TransactionManager mgr, string whereClause, string orderBy, int start, int pageLength, out int count);
        public virtual int GetTotalItems(string whereClause, out int count)
        {
            return this.GetTotalItems(null, whereClause, out count);
        }

        public virtual int GetTotalItems(TransactionManager mgr, string whereClause, out int count)
        {
            this.GetPaged(mgr, whereClause, string.Empty, 0, 0, out count);
            return count;
        }

        public virtual bool Insert(Entity entity)
        {
            return this.Insert(null, entity);
        }

        public virtual int Insert(TList<Entity> entities)
        {
            return this.Insert(null, entities);
        }

        public abstract bool Insert(TransactionManager mgr, Entity entity);
        public virtual int Insert(TransactionManager mgr, TList<Entity> entities)
        {
            int num = 0;
            foreach (Entity local in entities)
            {
                if ((local.EntityState == EntityState.Added) && this.Insert(mgr, local))
                {
                    num++;
                }
            }
            return num;
        }

        public virtual void Save(Entity entity)
        {
            this.Save(null, entity);
        }

        public virtual void Save(TList<Entity> entities)
        {
            this.Save(null, entities);
        }

        public virtual void Save(TransactionManager mgr, TList<Entity> entities)
        {
            foreach (Entity local in entities)
            {
                this.Save(mgr, local);
            }
            foreach (Entity local in entities.DeletedItems)
            {
                this.Delete(mgr, local);
            }
            entities.DeletedItems.Clear();
        }

        public virtual void Save(TransactionManager mgr, Entity entity)
        {
            switch (entity.EntityState)
            {
                case EntityState.Added:
                    this.Insert(mgr, entity);
                    break;

                case EntityState.Changed:
                    this.Update(mgr, entity);
                    break;

                case EntityState.Deleted:
                    this.Delete(mgr, entity);
                    break;
            }
        }

        public virtual int Update(TList<Entity> entities)
        {
            return this.Update(null, entities);
        }

        public virtual bool Update(Entity entity)
        {
            return this.Update(null, entity);
        }

        public abstract bool Update(TransactionManager mgr, Entity entity);
        public virtual int Update(TransactionManager mgr, TList<Entity> entities)
        {
            int num = 0;
            foreach (Entity local in entities)
            {
                if ((local.EntityState == EntityState.Changed) && this.Update(mgr, local))
                {
                    num++;
                }
            }
            return num;
        }

        private class DeepSession
        {
            private List<WeakReference> innerList;

            public void Add(object obj)
            {
                if (obj != null)
                {
                    this.InnerList.Add(new WeakReference(obj));
                }
            }

            public bool HasRun(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                return this.InnerList.Contains(obj as WeakReference);
            }

            internal List<WeakReference> InnerList
            {
                get
                {
                    if (this.innerList == null)
                    {
                        this.innerList = new List<WeakReference>();
                    }
                    return this.innerList;
                }
            }
        }
    }
}

