namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractCostPlanKey : EntityKeyBase
    {
        private ContractCostPlanBase _entity;
        private string contractCostPlanCode;

        public ContractCostPlanKey()
        {
        }

        public ContractCostPlanKey(string contractCostPlanCode)
        {
            this.contractCostPlanCode = contractCostPlanCode;
        }

        public ContractCostPlanKey(ContractCostPlanBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractCostPlanCode = entity.ContractCostPlanCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractCostPlanCode = (values["ContractCostPlanCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractCostPlanCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractCostPlanCode", this.ContractCostPlanCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractCostPlanCode: {0}{1}", this.ContractCostPlanCode, Environment.NewLine);
        }

        public string ContractCostPlanCode
        {
            get
            {
                return this.contractCostPlanCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractCostPlanCode = value;
                }
                this.contractCostPlanCode = value;
            }
        }

        public ContractCostPlanBase Entity
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

