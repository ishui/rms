namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ContractNexusKey : EntityKeyBase
    {
        private ContractNexusBase _entity;
        private string contractNexusCode;

        public ContractNexusKey()
        {
        }

        public ContractNexusKey(string contractNexusCode)
        {
            this.contractNexusCode = contractNexusCode;
        }

        public ContractNexusKey(ContractNexusBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.contractNexusCode = entity.ContractNexusCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ContractNexusCode = (values["ContractNexusCode"] != null) ? ((string) EntityUtil.ChangeType(values["ContractNexusCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ContractNexusCode", this.ContractNexusCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ContractNexusCode: {0}{1}", this.ContractNexusCode, Environment.NewLine);
        }

        public string ContractNexusCode
        {
            get
            {
                return this.contractNexusCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ContractNexusCode = value;
                }
                this.contractNexusCode = value;
            }
        }

        public ContractNexusBase Entity
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

