namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class TroubleBase : EntityBase, IEntityId<TroubleKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private TroubleKey _entityId;
        private InspectSituation _inspectSituationIDSource;
        private ISite _site;
        private TroubleEntityData backupData;
        private TroubleEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Trouble> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event TroubleEventHandler ColumnChanged;

        [field: NonSerialized]
        public event TroubleEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public TroubleBase()
        {
            this.inTxn = false;
            this._inspectSituationIDSource = null;
            this._site = null;
            this.entityData = new TroubleEntityData();
            this.backupData = null;
        }

        public TroubleBase(int troubleInspectSituationID, string troubleRequirement, string troubleSuggestion, DateTime? troubleExecutionTime, string troublePlace, string troubleTroubleCompendium, string troubleRemark, int troubleStatus)
        {
            this.inTxn = false;
            this._inspectSituationIDSource = null;
            this._site = null;
            this.entityData = new TroubleEntityData();
            this.backupData = null;
            this.InspectSituationID = troubleInspectSituationID;
            this.Requirement = troubleRequirement;
            this.Suggestion = troubleSuggestion;
            this.ExecutionTime = troubleExecutionTime;
            this.Place = troublePlace;
            this.TroubleCompendium = troubleTroubleCompendium;
            this.Remark = troubleRemark;
            this.Status = troubleStatus;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Requirement", 0x7d0));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Suggestion", 0x7d0));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Place", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("TroubleCompendium", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 0xfa0));
        }

        public override void CancelChanges()
        {
            IEditableObject obj2 = this;
            obj2.CancelEdit();
        }

        public object Clone()
        {
            return this.Copy();
        }

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual Trouble Copy()
        {
            Trouble trouble = new Trouble();
            trouble.TroubleID = this.TroubleID;
            trouble.InspectSituationID = this.InspectSituationID;
            trouble.Requirement = this.Requirement;
            trouble.Suggestion = this.Suggestion;
            trouble.ExecutionTime = this.ExecutionTime;
            trouble.Place = this.Place;
            trouble.TroubleCompendium = this.TroubleCompendium;
            trouble.Remark = this.Remark;
            trouble.Status = this.Status;
            trouble.AcceptChanges();
            return trouble;
        }

        public static Trouble CreateTrouble(int troubleInspectSituationID, string troubleRequirement, string troubleSuggestion, DateTime? troubleExecutionTime, string troublePlace, string troubleTroubleCompendium, string troubleRemark, int troubleStatus)
        {
            Trouble trouble = new Trouble();
            trouble.InspectSituationID = troubleInspectSituationID;
            trouble.Requirement = troubleRequirement;
            trouble.Suggestion = troubleSuggestion;
            trouble.ExecutionTime = troubleExecutionTime;
            trouble.Place = troublePlace;
            trouble.TroubleCompendium = troubleTroubleCompendium;
            trouble.Remark = troubleRemark;
            trouble.Status = troubleStatus;
            return trouble;
        }

        public virtual Trouble DeepCopy()
        {
            return EntityHelper.Clone<Trouble>(this as Trouble);
        }

        public void Dispose()
        {
            this.parentCollection = null;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventHandler disposed = this.Disposed;
                if (disposed != null)
                {
                    disposed(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Equals(TroubleBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(TroubleBase Object1, TroubleBase Object2)
        {
            if ((Object1 == null) && (Object2 == null))
            {
                return true;
            }
            if ((Object1 == null) ^ (Object2 == null))
            {
                return false;
            }
            bool flag = true;
            if (Object1.TroubleID != Object2.TroubleID)
            {
                flag = false;
            }
            if (Object1.InspectSituationID != Object2.InspectSituationID)
            {
                flag = false;
            }
            if ((Object1.Requirement != null) && (Object2.Requirement != null))
            {
                if (Object1.Requirement != Object2.Requirement)
                {
                    flag = false;
                }
            }
            else if ((Object1.Requirement == null) ^ (Object2.Requirement == null))
            {
                flag = false;
            }
            if ((Object1.Suggestion != null) && (Object2.Suggestion != null))
            {
                if (Object1.Suggestion != Object2.Suggestion)
                {
                    flag = false;
                }
            }
            else if ((Object1.Suggestion == null) ^ (Object2.Suggestion == null))
            {
                flag = false;
            }
            if (Object1.ExecutionTime.HasValue && Object2.ExecutionTime.HasValue)
            {
                if (Object1.ExecutionTime != Object2.ExecutionTime)
                {
                    flag = false;
                }
            }
            else if (!Object1.ExecutionTime.HasValue ^ !Object2.ExecutionTime.HasValue)
            {
                flag = false;
            }
            if ((Object1.Place != null) && (Object2.Place != null))
            {
                if (Object1.Place != Object2.Place)
                {
                    flag = false;
                }
            }
            else if ((Object1.Place == null) ^ (Object2.Place == null))
            {
                flag = false;
            }
            if ((Object1.TroubleCompendium != null) && (Object2.TroubleCompendium != null))
            {
                if (Object1.TroubleCompendium != Object2.TroubleCompendium)
                {
                    flag = false;
                }
            }
            else if ((Object1.TroubleCompendium == null) ^ (Object2.TroubleCompendium == null))
            {
                flag = false;
            }
            if ((Object1.Remark != null) && (Object2.Remark != null))
            {
                if (Object1.Remark != Object2.Remark)
                {
                    flag = false;
                }
            }
            else if ((Object1.Remark == null) ^ (Object2.Remark == null))
            {
                flag = false;
            }
            if (Object1.Status != Object2.Status)
            {
                flag = false;
            }
            return flag;
        }

        public static object MakeCopyOf(object x)
        {
            if (x is ICloneable)
            {
                return ((ICloneable) x).Clone();
            }
            throw new NotSupportedException("Object Does Not Implement the ICloneable Interface.");
        }

        public void OnCancelAddNew()
        {
            if (!base.SuppressEntityEvents)
            {
                CancelAddNewEventHandler cancelAddNew = this.CancelAddNew;
                if (cancelAddNew != null)
                {
                    cancelAddNew(this, EventArgs.Empty);
                }
            }
        }

        public void OnColumnChanged(TroubleColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(TroubleColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                TroubleEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new TroubleEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(TroubleColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(TroubleColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                TroubleEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new TroubleEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Trouble);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as TroubleEntityData;
                this.inTxn = true;
            }
        }

        void IEditableObject.CancelEdit()
        {
            if (this.inTxn)
            {
                this.entityData = this.backupData;
                this.backupData = null;
                this.inTxn = false;
                if (base.bindingIsNew && (this.parentCollection != null))
                {
                    this.parentCollection.Remove((Trouble) this);
                }
            }
        }

        void IEditableObject.EndEdit()
        {
            if (this.inTxn)
            {
                this.backupData = null;
                if (base.IsDirty)
                {
                    if (base.bindingIsNew)
                    {
                        this.EntityState = EntityState.Added;
                        base.bindingIsNew = false;
                    }
                    else if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                }
                base.bindingIsNew = false;
                this.inTxn = false;
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{10}{9}- TroubleID: {0}{9}- InspectSituationID: {1}{9}- Requirement: {2}{9}- Suggestion: {3}{9}- ExecutionTime: {4}{9}- Place: {5}{9}- TroubleCompendium: {6}{9}- Remark: {7}{9}- Status: {8}{9}", new object[] { this.TroubleID, this.InspectSituationID, (this.Requirement == null) ? string.Empty : this.Requirement.ToString(), (this.Suggestion == null) ? string.Empty : this.Suggestion.ToString(), !this.ExecutionTime.HasValue ? string.Empty : this.ExecutionTime.ToString(), (this.Place == null) ? string.Empty : this.Place.ToString(), (this.TroubleCompendium == null) ? string.Empty : this.TroubleCompendium.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), this.Status, Environment.NewLine, base.GetType() });
        }

        [XmlIgnore]
        public TroubleKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new TroubleKey(this);
                }
                return this._entityId;
            }
            set
            {
                if (value != null)
                {
                    value.Entity = this;
                }
                this._entityId = value;
            }
        }

        [XmlIgnore]
        public override string EntityTrackingKey
        {
            get
            {
                if (this.entityTrackingKey == null)
                {
                    this.entityTrackingKey = "Trouble" + this.TroubleID.ToString();
                }
                return this.entityTrackingKey;
            }
            set
            {
                if (value != null)
                {
                    this.entityTrackingKey = value;
                }
            }
        }

        [Description("解决方案设计完成时间"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? ExecutionTime
        {
            get
            {
                return this.entityData.ExecutionTime;
            }
            set
            {
                if (this.entityData.ExecutionTime != value)
                {
                    this.OnColumnChanging(TroubleColumn.ExecutionTime, this.entityData.ExecutionTime);
                    this.entityData.ExecutionTime = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.ExecutionTime, this.entityData.ExecutionTime);
                    this.OnPropertyChanged("ExecutionTime");
                }
            }
        }

        [DataObjectField(false, false, false), Description(""), TiannuoPM.Entities.Bindable]
        public virtual int InspectSituationID
        {
            get
            {
                return this.entityData.InspectSituationID;
            }
            set
            {
                if (this.entityData.InspectSituationID != value)
                {
                    this.OnColumnChanging(TroubleColumn.InspectSituationID, this.entityData.InspectSituationID);
                    this.entityData.InspectSituationID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.InspectSituationID, this.entityData.InspectSituationID);
                    this.OnPropertyChanged("InspectSituationID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
        public virtual InspectSituation InspectSituationIDSource
        {
            get
            {
                return this._inspectSituationIDSource;
            }
            set
            {
                this._inspectSituationIDSource = value;
            }
        }

        [Browsable(false), XmlIgnore]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<Trouble>;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("部位"), DataObjectField(false, false, true, 200)]
        public virtual string Place
        {
            get
            {
                return this.entityData.Place;
            }
            set
            {
                if (this.entityData.Place != value)
                {
                    this.OnColumnChanging(TroubleColumn.Place, this.entityData.Place);
                    this.entityData.Place = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.Place, this.entityData.Place);
                    this.OnPropertyChanged("Place");
                }
            }
        }

        [Description("备注"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 0xfa0)]
        public virtual string Remark
        {
            get
            {
                return this.entityData.Remark;
            }
            set
            {
                if (this.entityData.Remark != value)
                {
                    this.OnColumnChanging(TroubleColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("与本专业相关的图纸/技术要求"), DataObjectField(false, false, true, 0x7d0)]
        public virtual string Requirement
        {
            get
            {
                return this.entityData.Requirement;
            }
            set
            {
                if (this.entityData.Requirement != value)
                {
                    this.OnColumnChanging(TroubleColumn.Requirement, this.entityData.Requirement);
                    this.entityData.Requirement = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.Requirement, this.entityData.Requirement);
                    this.OnPropertyChanged("Requirement");
                }
            }
        }

        [SoapIgnore, Browsable(false), XmlIgnore]
        public ISite Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        [Description(""), DataObjectField(false, false, false), TiannuoPM.Entities.Bindable]
        public virtual int Status
        {
            get
            {
                return this.entityData.Status;
            }
            set
            {
                if (this.entityData.Status != value)
                {
                    this.OnColumnChanging(TroubleColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [DataObjectField(false, false, true, 0x7d0), TiannuoPM.Entities.Bindable, Description("解决方案或改进性建议")]
        public virtual string Suggestion
        {
            get
            {
                return this.entityData.Suggestion;
            }
            set
            {
                if (this.entityData.Suggestion != value)
                {
                    this.OnColumnChanging(TroubleColumn.Suggestion, this.entityData.Suggestion);
                    this.entityData.Suggestion = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.Suggestion, this.entityData.Suggestion);
                    this.OnPropertyChanged("Suggestion");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "TroubleID", "InspectSituationID", "Requirement", "Suggestion", "ExecutionTime", "place", "TroubleCompendium", "Remark", "Status" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "Trouble";
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 200), Description("问题简述")]
        public virtual string TroubleCompendium
        {
            get
            {
                return this.entityData.TroubleCompendium;
            }
            set
            {
                if (this.entityData.TroubleCompendium != value)
                {
                    this.OnColumnChanging(TroubleColumn.TroubleCompendium, this.entityData.TroubleCompendium);
                    this.entityData.TroubleCompendium = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.TroubleCompendium, this.entityData.TroubleCompendium);
                    this.OnPropertyChanged("TroubleCompendium");
                }
            }
        }

        [ReadOnly(false), DataObjectField(true, true, false), TiannuoPM.Entities.Bindable, Description("")]
        public virtual int TroubleID
        {
            get
            {
                return this.entityData.TroubleID;
            }
            set
            {
                if (this.entityData.TroubleID != value)
                {
                    this.OnColumnChanging(TroubleColumn.TroubleID, this.entityData.TroubleID);
                    this.entityData.TroubleID = value;
                    this.EntityId.TroubleID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(TroubleColumn.TroubleID, this.entityData.TroubleID);
                    this.OnPropertyChanged("TroubleID");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class TroubleEntityData : ICloneable
        {
            public DateTime? ExecutionTime = null;
            public int InspectSituationID = 0;
            public string Place = null;
            public string Remark = null;
            public string Requirement = null;
            public int Status = 0;
            public string Suggestion = null;
            public string TroubleCompendium = null;
            public int TroubleID;

            public object Clone()
            {
                TroubleBase.TroubleEntityData data = new TroubleBase.TroubleEntityData();
                data.TroubleID = this.TroubleID;
                data.InspectSituationID = this.InspectSituationID;
                data.Requirement = this.Requirement;
                data.Suggestion = this.Suggestion;
                data.ExecutionTime = this.ExecutionTime;
                data.Place = this.Place;
                data.TroubleCompendium = this.TroubleCompendium;
                data.Remark = this.Remark;
                data.Status = this.Status;
                return data;
            }
        }
    }
}

