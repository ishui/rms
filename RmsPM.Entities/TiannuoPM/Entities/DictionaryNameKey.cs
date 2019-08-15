namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class DictionaryNameKey : EntityKeyBase
    {
        private DictionaryNameBase _entity;
        private string dictionaryNameCode;

        public DictionaryNameKey()
        {
        }

        public DictionaryNameKey(string dictionaryNameCode)
        {
            this.dictionaryNameCode = dictionaryNameCode;
        }

        public DictionaryNameKey(DictionaryNameBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.dictionaryNameCode = entity.DictionaryNameCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.DictionaryNameCode = (values["DictionaryNameCode"] != null) ? ((string) EntityUtil.ChangeType(values["DictionaryNameCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("DictionaryNameCode", this.DictionaryNameCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("DictionaryNameCode: {0}{1}", this.DictionaryNameCode, Environment.NewLine);
        }

        public string DictionaryNameCode
        {
            get
            {
                return this.dictionaryNameCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.DictionaryNameCode = value;
                }
                this.dictionaryNameCode = value;
            }
        }

        public DictionaryNameBase Entity
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

