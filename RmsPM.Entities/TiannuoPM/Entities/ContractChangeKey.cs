namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractChangeKey : EntityKeyBase
    {
        private ContractChangeBase _entity;
        private string contractChangeCode;

        public ContractChangeKey()
        {
        }

        public ContractChangeKey(string contractChangeCode)
        {
            this.contractChangeCode = contractChangeCode;
        }

        public ContractChangeKey(ContractChangeBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractChangeCode = entity.ContractChangeCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractChangeCode = (values["ContractChangeCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractChangeCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractChangeCode", this.ContractChangeCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractChangeCode: {0}{1}", this.ContractChangeCode, Environment.NewLine);
        }

        public string ContractChangeCode
        {
            get
            {
                return this.contractChangeCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractChangeCode = value;
                }
                this.contractChangeCode = value;
            }
        }

        public ContractChangeBase Entity
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

