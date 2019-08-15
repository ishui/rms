namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractCostKey : EntityKeyBase
    {
        private ContractCostBase _entity;
        private string contractCostCode;

        public ContractCostKey()
        {
        }

        public ContractCostKey(string contractCostCode)
        {
            this.contractCostCode = contractCostCode;
        }

        public ContractCostKey(ContractCostBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractCostCode = entity.ContractCostCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractCostCode = (values["ContractCostCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractCostCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractCostCode", this.ContractCostCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractCostCode: {0}{1}", this.ContractCostCode, Environment.NewLine);
        }

        public string ContractCostCode
        {
            get
            {
                return this.contractCostCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractCostCode = value;
                }
                this.contractCostCode = value;
            }
        }

        public ContractCostBase Entity
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

