namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class ProjectKey : EntityKeyBase
    {
        private ProjectBase _entity;
        private string projectCode;

        public ProjectKey()
        {
        }

        public ProjectKey(string projectCode)
        {
            this.projectCode = projectCode;
        }

        public ProjectKey(ProjectBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.projectCode = entity.ProjectCode;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.ProjectCode = (values["ProjectCode"] != null) ? ((string) EntityUtil.ChangeType(values["ProjectCode"], typeof(string))) : string.Empty;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("ProjectCode", this.ProjectCode);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("ProjectCode: {0}{1}", this.ProjectCode, Environment.NewLine);
        }

        public ProjectBase Entity
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

        public string ProjectCode
        {
            get
            {
                return this.projectCode;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.ProjectCode = value;
                }
                this.projectCode = value;
            }
        }
    }
}

