namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class InspectSituationKey : EntityKeyBase
    {
        private InspectSituationBase _entity;
        private int inspectSituationID;

        public InspectSituationKey()
        {
        }

        public InspectSituationKey(int inspectSituationID)
        {
            this.inspectSituationID = inspectSituationID;
        }

        public InspectSituationKey(InspectSituationBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.inspectSituationID = entity.InspectSituationID;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.InspectSituationID = (values["InspectSituationID"] != null) ? ((int) EntityUtil.ChangeType(values["InspectSituationID"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("InspectSituationID", this.InspectSituationID);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("InspectSituationID: {0}{1}", this.InspectSituationID, Environment.NewLine);
        }

        public InspectSituationBase Entity
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

        public int InspectSituationID
        {
            get
            {
                return this.inspectSituationID;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.InspectSituationID = value;
                }
                this.inspectSituationID = value;
            }
        }
    }
}

