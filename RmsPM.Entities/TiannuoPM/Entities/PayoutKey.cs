namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class PayoutKey : EntityKeyBase
    {
        private PayoutBase _entity;
        private string payoutCode;

        public PayoutKey()
        {
        }

        public PayoutKey(string payoutCode)
        {
            this.payoutCode = payoutCode;
        }

        public PayoutKey(PayoutBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.payoutCode = entity.PayoutCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.PayoutCode = (values["PayoutCode"] != null) ? ((string) EntityUtil.ChangeType(values["PayoutCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("PayoutCode", this.PayoutCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("PayoutCode: {0}{1}", this.PayoutCode, Environment.NewLine);
        }

        public PayoutBase Entity
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

        public string PayoutCode
        {
            get
            {
                return this.payoutCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.PayoutCode = value;
                }
                this.payoutCode = value;
            }
        }
    }
}

