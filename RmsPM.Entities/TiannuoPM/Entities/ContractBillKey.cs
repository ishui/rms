namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractBillKey : EntityKeyBase
    {
        private ContractBillBase _entity;
        private int code;

        public ContractBillKey()
        {
        }

        public ContractBillKey(int code)
        {
            this.code = code;
        }

        public ContractBillKey(ContractBillBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.code = entity.Code;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.Code = (values["Code"] != null) ? ((int) EntityUtil.ChangeType(values["Code"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("Code", this.Code);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("Code: {0}{1}", this.Code, Environment.NewLine);
        }

        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.Code = value;
                }
                this.code = value;
            }
        }

        public ContractBillBase Entity
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

