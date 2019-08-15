namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class PaymentItemKey : EntityKeyBase
    {
        private PaymentItemBase _entity;
        private string paymentItemCode;

        public PaymentItemKey()
        {
        }

        public PaymentItemKey(string paymentItemCode)
        {
            this.paymentItemCode = paymentItemCode;
        }

        public PaymentItemKey(PaymentItemBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.paymentItemCode = entity.PaymentItemCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.PaymentItemCode = (values["PaymentItemCode"] != null) ? ((string) EntityUtil.ChangeType(values["PaymentItemCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("PaymentItemCode", this.PaymentItemCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("PaymentItemCode: {0}{1}", this.PaymentItemCode, Environment.NewLine);
        }

        public PaymentItemBase Entity
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

        public string PaymentItemCode
        {
            get
            {
                return this.paymentItemCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.PaymentItemCode = value;
                }
                this.paymentItemCode = value;
            }
        }
    }
}

