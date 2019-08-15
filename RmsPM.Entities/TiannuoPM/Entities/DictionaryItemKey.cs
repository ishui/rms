namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class DictionaryItemKey : EntityKeyBase
    {
        private DictionaryItemBase _entity;
        private string dictionaryItemCode;

        public DictionaryItemKey()
        {
        }

        public DictionaryItemKey(string dictionaryItemCode)
        {
            this.dictionaryItemCode = dictionaryItemCode;
        }

        public DictionaryItemKey(DictionaryItemBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.dictionaryItemCode = entity.DictionaryItemCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.DictionaryItemCode = (values["DictionaryItemCode"] != null) ? ((string) EntityUtil.ChangeType(values["DictionaryItemCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("DictionaryItemCode", this.DictionaryItemCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("DictionaryItemCode: {0}{1}", this.DictionaryItemCode, Environment.NewLine);
        }

        public string DictionaryItemCode
        {
            get
            {
                return this.dictionaryItemCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.DictionaryItemCode = value;
                }
                this.dictionaryItemCode = value;
            }
        }

        public DictionaryItemBase Entity
        {
            get
            {
                return this._entity;
            }
            set
            {
                this._entity = value;
            }
        }
    }
}

