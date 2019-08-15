namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractKey : EntityKeyBase
    {
        private ContractBase _entity;
        private string contractCode;

        public ContractKey()
        {
        }

        public ContractKey(string contractCode)
        {
            this.contractCode = contractCode;
        }

        public ContractKey(ContractBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractCode = entity.ContractCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractCode = (values["ContractCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractCode", this.ContractCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractCode: {0}{1}", this.ContractCode, Environment.NewLine);
        }

        public string ContractCode
        {
            get
            {
                return this.contractCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractCode = value;
                }
                this.contractCode = value;
            }
        }

        public ContractBase Entity
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

