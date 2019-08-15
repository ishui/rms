namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using TiannuoPM.Entities;

    [Serializable, CLSCompliant(true)]
    public class ChildEntityTypesList : StringCollection
    {
        private Dictionary<string, StringCollection> keys;
        private Dictionary<string, StringCollection> properties;

        public void AddProperty(IEntity entity, string key, string property)
        {
            if (entity != null)
            {
                this.GetProperties(entity).Add(property);
                this.GetKeys(entity).Add(key);
            }
        }

        public StringCollection GetKeys(IEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            string key = entity.EntityTrackingKey;
            if (!this.Keys.ContainsKey(key))
            {
                this.Keys[key] = new StringCollection();
            }
            return this.Keys[key];
        }

        public StringCollection GetProperties(IEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            string key = entity.EntityTrackingKey;
            if (!this.Properties.ContainsKey(key))
            {
                this.Properties[key] = new StringCollection();
            }
            return this.Properties[key];
        }

        public bool HasProperty(IEntity entity, string key, string property)
        {
            if (entity == null)
            {
                return false;
            }
            string a = entity.EntityTrackingKey;
            foreach (string text2 in this.Keys.Keys)
            {
                if (!string.Equals(a, text2) && this.Keys[text2].Contains(key))
                {
                    return true;
                }
            }
            return this.GetProperties(entity).Contains(property);
        }

        public Dictionary<string, StringCollection> Keys
        {
            get
            {
                if (this.keys == null)
                {
                    this.keys = new Dictionary<string, StringCollection>();
                }
                return this.keys;
            }
        }

        public Dictionary<string, StringCollection> Properties
        {
            get
            {
                if (this.properties == null)
                {
                    this.properties = new Dictionary<string, StringCollection>();
                }
                return this.properties;
            }
        }
    }
}

