namespace TiannuoPM.Data.Bases
{
    using System;
    using TiannuoPM.Entities;

    [Serializable, CLSCompliant(true)]
    public class ChildEntityTypeAttribute : Attribute
    {
        private readonly Type entityType;

        public ChildEntityTypeAttribute(Type entityType)
        {
            this.entityType = entityType;
        }

        public static Type GetType(Enum e)
        {
            ChildEntityTypeAttribute attribute = EntityHelper.GetAttribute<ChildEntityTypeAttribute>(e);
            Type entityType = null;
            if (attribute != null)
            {
                entityType = attribute.EntityType;
            }
            return entityType;
        }

        public Type EntityType
        {
            get
            {
                return this.entityType;
            }
        }
    }
}

