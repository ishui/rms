namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class MaterialPurchasDtlKey : EntityKeyBase
    {
        private MaterialPurchasDtlBase _entity;
        private int materialPurchasDtlID;

        public MaterialPurchasDtlKey()
        {
        }

        public MaterialPurchasDtlKey(int materialPurchasDtlID)
        {
            this.materialPurchasDtlID = materialPurchasDtlID;
        }

        public MaterialPurchasDtlKey(MaterialPurchasDtlBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.materialPurchasDtlID = entity.MaterialPurchasDtlID;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.MaterialPurchasDtlID = (values["MaterialPurchasDtlID"] != null) ? ((int) EntityUtil.ChangeType(values["MaterialPurchasDtlID"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("MaterialPurchasDtlID", this.MaterialPurchasDtlID);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("MaterialPurchasDtlID: {0}{1}", this.MaterialPurchasDtlID, Environment.NewLine);
        }

        public MaterialPurchasDtlBase Entity
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

        public int MaterialPurchasDtlID
        {
            get
            {
                return this.materialPurchasDtlID;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.MaterialPurchasDtlID = value;
                }
                this.materialPurchasDtlID = value;
            }
        }
    }
}

