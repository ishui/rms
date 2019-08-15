namespace TiannuoPM.Entities
{
    using Microsoft.Practices.ObjectBuilder;
    using System;
    using System.Text;

    public class EntityLocator : Locator
    {
        public EntityLocator() : base(null)
        {
        }

        public void Add(string key, object value)
        {
            base.Add(key, value);
        }

        public static string ConstructKeyFromPkItems(Type type, params object[] pkItems)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (pkItems.Length == 0)
            {
                throw new ArgumentNullException("pkItems");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(type.Name);
            for (int i = 0; i < pkItems.Length; i++)
            {
                if (pkItems[i] != null)
                {
                    builder.Append(pkItems[i].ToString());
                }
            }
            return builder.ToString();
        }

        public bool Contains(string key, SearchMode options)
        {
            return base.Contains(key, options);
        }

        public override object Get(object key, SearchMode options)
        {
            return base.Get(key, options);
        }

        public Entity GetEntity<Entity>(string key) where Entity: EntityBase, new()
        {
            return (this.Get(key, SearchMode.Local) as Entity);
        }

        public EntityList GetList<EntityList>(string key) where EntityList: ListBase<IEntity>, new()
        {
            return (this.Get(key, SearchMode.Local) as EntityList);
        }
    }
}

