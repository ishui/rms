namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class PaymentKey : EntityKeyBase
    {
        private PaymentBase _entity;
        private string paymentCode;

        public PaymentKey()
        {
        }

        public PaymentKey(string paymentCode)
        {
            this.paymentCode = paymentCode;
        }

        public PaymentKey(PaymentBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.paymentCode = entity.PaymentCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.PaymentCode = (values["PaymentCode"] != null) ? ((string) EntityUtil.ChangeType(values["PaymentCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("PaymentCode", this.PaymentCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("PaymentCode: {0}{1}", this.PaymentCode, Environment.NewLine);
        }

        public PaymentBase Entity
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

        public string PaymentCode
        {
            get
            {
                return this.paymentCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.PaymentCode = value;
                }
                this.paymentCode = value;
            }
        }
    }
}

