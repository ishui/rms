namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class SystemUserKey : EntityKeyBase
    {
        private SystemUserBase _entity;
        private string userCode;

        public SystemUserKey()
        {
        }

        public SystemUserKey(string userCode)
        {
            this.userCode = userCode;
        }

        public SystemUserKey(SystemUserBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.userCode = entity.UserCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.UserCode = (values["UserCode"] != null) ? ((string) EntityUtil.ChangeType(values["UserCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("UserCode", this.UserCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("UserCode: {0}{1}", this.UserCode, Environment.NewLine);
        }

        public SystemUserBase Entity
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

        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.UserCode = value;
                }
                this.userCode = value;
            }
        }
    }
}

