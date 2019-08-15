namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    public class EntityNotValidException : Exception
    {
        private static readonly string defaultMessage = "One or more entities is in an invalid state while trying to persist the entity";
        private EntityBase entity;
        private IList entityList;
        private string executingMethod;

        public EntityNotValidException() : base(defaultMessage)
        {
        }

        public EntityNotValidException(string message) : base(message)
        {
        }

        public EntityNotValidException(IList entityList, string method) : this(entityList, method, string.Format(defaultMessage + " for {0} during {1}.", (entityList != null) ? entityList.GetType().Name : "", method))
        {
        }

        public EntityNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Entity = (EntityBase) info.GetValue("Entity", typeof(EntityBase));
            this.EntityList = (IList) info.GetValue("EntityList", typeof(IList));
            this.ExecutingMethod = info.GetString("ExecutingMethod");
        }

        public EntityNotValidException(string message, Exception exception) : base(message, exception)
        {
        }

        public EntityNotValidException(EntityBase entity, string method) : this(entity, method, string.Format(defaultMessage + " for {0} during {1}.", (entity != null) ? entity.GetType().Name : "", method))
        {
        }

        public EntityNotValidException(IList entityList, string method, string message) : base(message)
        {
            this.entityList = entityList;
            this.executingMethod = method;
        }

        public EntityNotValidException(EntityBase entity, string method, string message) : base(message)
        {
            this.entity = entity;
            this.executingMethod = method;
        }

        public EntityBase Entity
        {
            get
            {
                return this.entity;
            }
            set
            {
                this.entity = value;
            }
        }

        public IList EntityList
        {
            get
            {
                return this.entityList;
            }
            set
            {
                this.entityList = value;
            }
        }

        public string ExecutingMethod
        {
            get
            {
                return this.executingMethod;
            }
            set
            {
                this.executingMethod = value;
            }
        }
    }
}

