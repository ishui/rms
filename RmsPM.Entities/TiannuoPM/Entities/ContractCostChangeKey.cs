namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractCostChangeKey : EntityKeyBase
    {
        private ContractCostChangeBase _entity;
        private string contractCostChangeCode;

        public ContractCostChangeKey()
        {
        }

        public ContractCostChangeKey(string contractCostChangeCode)
        {
            this.contractCostChangeCode = contractCostChangeCode;
        }

        public ContractCostChangeKey(ContractCostChangeBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractCostChangeCode = entity.ContractCostChangeCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractCostChangeCode = (values["ContractCostChangeCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractCostChangeCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractCostChangeCode", this.ContractCostChangeCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractCostChangeCode: {0}{1}", this.ContractCostChangeCode, Environment.NewLine);
        }

        public string ContractCostChangeCode
        {
            get
            {
                return this.contractCostChangeCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractCostChangeCode = value;
                }
                this.contractCostChangeCode = value;
            }
        }

        public ContractCostChangeBase Entity
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

