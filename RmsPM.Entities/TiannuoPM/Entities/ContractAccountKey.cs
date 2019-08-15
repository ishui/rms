namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractAccountKey : EntityKeyBase
    {
        private ContractAccountBase _entity;
        private string contractAccountCode;

        public ContractAccountKey()
        {
        }

        public ContractAccountKey(string contractAccountCode)
        {
            this.contractAccountCode = contractAccountCode;
        }

        public ContractAccountKey(ContractAccountBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractAccountCode = entity.ContractAccountCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractAccountCode = (values["ContractAccountCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractAccountCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractAccountCode", this.ContractAccountCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractAccountCode: {0}{1}", this.ContractAccountCode, Environment.NewLine);
        }

        public string ContractAccountCode
        {
            get
            {
                return this.contractAccountCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractAccountCode = value;
                }
                this.contractAccountCode = value;
            }
        }

        public ContractAccountBase Entity
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

