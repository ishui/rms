namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class PayoutItemKey : EntityKeyBase
    {
        private PayoutItemBase _entity;
        private string payoutItemCode;

        public PayoutItemKey()
        {
        }

        public PayoutItemKey(string payoutItemCode)
        {
            this.payoutItemCode = payoutItemCode;
        }

        public PayoutItemKey(PayoutItemBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.payoutItemCode = entity.PayoutItemCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.PayoutItemCode = (values["PayoutItemCode"] != null) ? ((string) EntityUtil.ChangeType(values["PayoutItemCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("PayoutItemCode", this.PayoutItemCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("PayoutItemCode: {0}{1}", this.PayoutItemCode, Environment.NewLine);
        }

        public PayoutItemBase Entity
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

        public string PayoutItemCode
        {
            get
            {
                return this.payoutItemCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.PayoutItemCode = value;
                }
                this.payoutItemCode = value;
            }
        }
    }
}

