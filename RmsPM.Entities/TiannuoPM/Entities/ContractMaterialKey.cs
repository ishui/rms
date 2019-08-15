namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractMaterialKey : EntityKeyBase
    {
        private ContractMaterialBase _entity;
        private string contractMaterialCode;

        public ContractMaterialKey()
        {
        }

        public ContractMaterialKey(string contractMaterialCode)
        {
            this.contractMaterialCode = contractMaterialCode;
        }

        public ContractMaterialKey(ContractMaterialBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractMaterialCode = entity.ContractMaterialCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractMaterialCode = (values["ContractMaterialCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractMaterialCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractMaterialCode", this.ContractMaterialCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractMaterialCode: {0}{1}", this.ContractMaterialCode, Environment.NewLine);
        }

        public string ContractMaterialCode
        {
            get
            {
                return this.contractMaterialCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractMaterialCode = value;
                }
                this.contractMaterialCode = value;
            }
        }

        public ContractMaterialBase Entity
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

