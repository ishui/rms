namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class MaterialPurchasKey : EntityKeyBase
    {
        private MaterialPurchasBase _entity;
        private int materialPurchasID;

        public MaterialPurchasKey()
        {
        }

        public MaterialPurchasKey(int materialPurchasID)
        {
            this.materialPurchasID = materialPurchasID;
        }

        public MaterialPurchasKey(MaterialPurchasBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.materialPurchasID = entity.MaterialPurchasID;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.MaterialPurchasID = (values["MaterialPurchasID"] != null) ? ((int) EntityUtil.ChangeType(values["MaterialPurchasID"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("MaterialPurchasID", this.MaterialPurchasID);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("MaterialPurchasID: {0}{1}", this.MaterialPurchasID, Environment.NewLine);
        }

        public MaterialPurchasBase Entity
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

        public int MaterialPurchasID
        {
            get
            {
                return this.materialPurchasID;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.MaterialPurchasID = value;
                }
                this.materialPurchasID = value;
            }
        }
    }
}

