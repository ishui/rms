namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    [Serializable, CLSCompliant(true)]
    public class TroubleKey : EntityKeyBase
    {
        private TroubleBase _entity;
        private int troubleID;

        public TroubleKey()
        {
        }

        public TroubleKey(int troubleID)
        {
            this.troubleID = troubleID;
        }

        public TroubleKey(TroubleBase entity)
        {
            this.Entity = entity;
            if (entity != null)
            {
                this.troubleID = entity.TroubleID;
            }
        }

        public override void Load(IDictionary values)
        {
            if (values != null)
            {
                this.TroubleID = (values["TroubleID"] != null) ? ((int) EntityUtil.ChangeType(values["TroubleID"], typeof(int))) : 0;
            }
        }

        public override IDictionary ToDictionary()
        {
            IDictionary dictionary = new Hashtable();
            dictionary.Add("TroubleID", this.TroubleID);
            return dictionary;
        }

        public override string ToString()
        {
            return string.Format("TroubleID: {0}{1}", this.TroubleID, Environment.NewLine);
        }

        public TroubleBase Entity
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

        public int TroubleID
        {
            get
            {
                return this.troubleID;
            }
            set
            {
                if (this.Entity != null)
                {
                    this.Entity.TroubleID = value;
                }
                this.troubleID = value;
            }
        }
    }
}

