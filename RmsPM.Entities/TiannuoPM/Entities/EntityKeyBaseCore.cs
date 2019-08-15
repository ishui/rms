namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public abstract class EntityKeyBaseCore : IEntityKey
    {
        protected EntityKeyBaseCore()
        {
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (base.GetType() != obj.GetType()))
            {
                return false;
            }
            return (this.ToString() == obj.ToString());
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public abstract void Load(IDictionary values);
        public abstract IDictionary ToDictionary();
    }
}

