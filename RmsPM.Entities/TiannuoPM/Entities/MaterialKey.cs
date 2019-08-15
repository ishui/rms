namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class MaterialKey : EntityKeyBase
    {
        private MaterialBase _entity;
        private int materialCode;

        public MaterialKey()
        {
        }

        public MaterialKey(int materialCode)
        {
            this.materialCode = materialCode;
        }

        public MaterialKey(MaterialBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.materialCode = entity.MaterialCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.MaterialCode = (values["MaterialCode"] != null) ? ((int) EntityUtil.ChangeType(values["MaterialCode"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("MaterialCode", this.MaterialCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("MaterialCode: {0}{1}", this.MaterialCode, Environment.NewLine);
        }

        public MaterialBase Entity
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

        public int MaterialCode
        {
            get
            {
                return this.materialCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.MaterialCode = value;
                }
                this.materialCode = value;
            }
        }
    }
}

