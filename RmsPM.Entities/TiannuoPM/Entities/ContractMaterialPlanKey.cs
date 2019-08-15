namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractMaterialPlanKey : EntityKeyBase
    {
        private ContractMaterialPlanBase _entity;
        private string contractMaterialPlanCode;

        public ContractMaterialPlanKey()
        {
        }

        public ContractMaterialPlanKey(string contractMaterialPlanCode)
        {
            this.contractMaterialPlanCode = contractMaterialPlanCode;
        }

        public ContractMaterialPlanKey(ContractMaterialPlanBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractMaterialPlanCode = entity.ContractMaterialPlanCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractMaterialPlanCode = (values["ContractMaterialPlanCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractMaterialPlanCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractMaterialPlanCode", this.ContractMaterialPlanCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractMaterialPlanCode: {0}{1}", this.ContractMaterialPlanCode, Environment.NewLine);
        }

        public string ContractMaterialPlanCode
        {
            get
            {
                return this.contractMaterialPlanCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractMaterialPlanCode = value;
                }
                this.contractMaterialPlanCode = value;
            }
        }

        public ContractMaterialPlanBase Entity
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

