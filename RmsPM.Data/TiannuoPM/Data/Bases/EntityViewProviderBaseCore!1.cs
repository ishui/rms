namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    [Serializable, CLSCompliant(true)]
    public abstract class EntityViewProviderBaseCore<Entity> : IEntityViewProvider<Entity> where Entity: new()
    {
        protected EntityViewProviderBaseCore()
        {
        }

        public virtual VList<Entity> Find(SqlFilterParameterCollection parameters)
        {
            return this.Find(null, parameters);
        }

        public virtual VList<Entity> Find(SqlFilterParameterCollection parameters, string orderBy)
        {
            return this.Find(null, parameters, orderBy);
        }

        public virtual VList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters)
        {
            return this.Find(transactionManager, parameters, null);
        }

        public virtual VList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy)
        {
            int count = 0;
            return this.Find(transactionManager, parameters, orderBy, 0, 0x7fffffff, out count);
        }

        public virtual VList<Entity> Find(SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            return this.Find(null, parameters, orderBy, start, pageLength, out count);
        }

        public virtual VList<Entity> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            count = 0;
            return null;
        }

        public virtual VList<Entity> Get()
        {
            return this.Get(null, null, null, 0, 0x7fffffff);
        }

        public virtual VList<Entity> Get(TransactionManager transactionManager)
        {
            return this.Get(transactionManager, null, null, 0, 0x7fffffff);
        }

        public virtual VList<Entity> Get(int start, int pageLength)
        {
            return this.Get(null, null, null, start, pageLength);
        }

        public virtual VList<Entity> Get(string whereClause, string orderBy)
        {
            return this.Get(whereClause, orderBy, 0, 0x7fffffff);
        }

        public virtual VList<Entity> Get(int start, int pageLength, out int count)
        {
            return this.Get(null, null, null, start, pageLength, out count);
        }

        public virtual VList<Entity> Get(TransactionManager transactionManager, int start, int pageLength)
        {
            return this.Get(transactionManager, null, null, start, pageLength);
        }

        public virtual VList<Entity> Get(TransactionManager transactionManager, string whereClause, string orderBy)
        {
            return this.Get(transactionManager, whereClause, orderBy, 0, 0x7fffffff);
        }

        public virtual VList<Entity> Get(string whereClause, string orderBy, int start, int pageLength)
        {
            return this.Get(null, whereClause, orderBy, start, pageLength);
        }

        public virtual VList<Entity> Get(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            return this.Get(transactionManager, null, null, start, pageLength, out count);
        }

        public virtual VList<Entity> Get(string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            return this.Get(null, whereClause, orderBy, start, pageLength, out count);
        }

        public virtual VList<Entity> Get(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength)
        {
            int count;
            return this.Get(transactionManager, whereClause, orderBy, start, pageLength, out count);
        }

        public abstract VList<Entity> Get(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count);
        public virtual VList<Entity> GetAll()
        {
            return this.GetAll(0, 0x7fffffff);
        }

        public virtual VList<Entity> GetAll(TransactionManager transactionManager)
        {
            return this.GetAll(transactionManager, 0, 0x7fffffff);
        }

        public virtual VList<Entity> GetAll(int start, int pageLength)
        {
            return this.GetAll(null, start, pageLength);
        }

        public virtual VList<Entity> GetAll(int start, int pageLength, out int count)
        {
            return this.GetAll(null, start, pageLength, out count);
        }

        public virtual VList<Entity> GetAll(TransactionManager transactionManager, int start, int pageLength)
        {
            int count = 0;
            return this.GetAll(transactionManager, start, pageLength, out count);
        }

        public abstract VList<Entity> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count);
        public virtual VList<Entity> GetPaged(out int count)
        {
            return this.GetPaged(null, out count);
        }

        public virtual VList<Entity> GetPaged(TransactionManager mgr, out int count)
        {
            return this.GetPaged(mgr, string.Empty, string.Empty, 0, 0, out count);
        }

        public virtual VList<Entity> GetPaged(int start, int pageLength, out int count)
        {
            return this.GetPaged(null, start, pageLength, out count);
        }

        public virtual VList<Entity> GetPaged(TransactionManager mgr, int start, int pageLength, out int count)
        {
            return this.GetPaged(mgr, string.Empty, string.Empty, start, pageLength, out count);
        }

        public virtual VList<Entity> GetPaged(string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            return this.GetPaged(null, whereClause, orderBy, start, pageLength, out count);
        }

        public virtual VList<Entity> GetPaged(TransactionManager mgr, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            return this.Get(mgr, whereClause, orderBy, start, pageLength, out count);
        }

        public virtual int GetTotalItems(string whereClause, out int count)
        {
            return this.GetTotalItems(null, whereClause, out count);
        }

        public virtual int GetTotalItems(TransactionManager mgr, string whereClause, out int count)
        {
            this.GetPaged(mgr, whereClause, string.Empty, 0, 0, out count);
            return count;
        }
    }
}

