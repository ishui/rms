namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class InspectSituationBase : EntityBase, IEntityId<InspectSituationKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private InspectSituationKey _entityId;
        private SystemUser _inspectUserSource;
        private Project _projectCodeSource;
        private ISite _site;
        private InspectSituationEntityData backupData;
        private InspectSituationEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<InspectSituation> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event InspectSituationEventHandler ColumnChanged;

        [field: NonSerialized]
        public event InspectSituationEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public InspectSituationBase()
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._inspectUserSource = null;
            this._site = null;
            this.entityData = new InspectSituationEntityData();
            this.backupData = null;
        }

        public InspectSituationBase(string inspectSituationInspectSituationNO, string inspectSituationProjectCode, DateTime? inspectSituationInspectDate, string inspectSituationWeather, string inspectSituationInspectUserIpecialty, string inspectSituationInspectUser, string inspectSituationKeyPoint, int? inspectSituationStatus)
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._inspectUserSource = null;
            this._site = null;
            this.entityData = new InspectSituationEntityData();
            this.backupData = null;
            this.InspectSituationNO = inspectSituationInspectSituationNO;
            this.ProjectCode = inspectSituationProjectCode;
            this.InspectDate = inspectSituationInspectDate;
            this.Weather = inspectSituationWeather;
            this.InspectUserIpecialty = inspectSituationInspectUserIpecialty;
            this.InspectUser = inspectSituationInspectUser;
            this.KeyPoint = inspectSituationKeyPoint;
            this.Status = inspectSituationStatus;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "InspectSituationNO");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InspectSituationNO", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Weather", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InspectUserIpecialty", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InspectUser", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("KeyPoint", 500));
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

        public virtual InspectSituation Copy()
        {
            InspectSituation situation = new InspectSituation();
            situation.InspectSituationID = this.InspectSituationID;
            situation.InspectSituationNO = this.InspectSituationNO;
            situation.ProjectCode = this.ProjectCode;
            situation.InspectDate = this.InspectDate;
            situation.Weather = this.Weather;
            situation.InspectUserIpecialty = this.InspectUserIpecialty;
            situation.InspectUser = this.InspectUser;
            situation.KeyPoint = this.KeyPoint;
            situation.Status = this.Status;
            situation.AcceptChanges();
            return situation;
        }

        public static InspectSituation CreateInspectSituation(string inspectSituationInspectSituationNO, string inspectSituationProjectCode, DateTime? inspectSituationInspectDate, string inspectSituationWeather, string inspectSituationInspectUserIpecialty, string inspectSituationInspectUser, string inspectSituationKeyPoint, int? inspectSituationStatus)
        {
            InspectSituation situation = new InspectSituation();
            situation.InspectSituationNO = inspectSituationInspectSituationNO;
            situation.ProjectCode = inspectSituationProjectCode;
            situation.InspectDate = inspectSituationInspectDate;
            situation.Weather = inspectSituationWeather;
            situation.InspectUserIpecialty = inspectSituationInspectUserIpecialty;
            situation.InspectUser = inspectSituationInspectUser;
            situation.KeyPoint = inspectSituationKeyPoint;
            situation.Status = inspectSituationStatus;
            return situation;
        }

        public virtual InspectSituation DeepCopy()
        {
            return EntityHelper.Clone<InspectSituation>(this as InspectSituation);
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

        public virtual bool Equals(InspectSituationBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(InspectSituationBase Object1, InspectSituationBase Object2)
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
            if (Object1.InspectSituationID != Object2.InspectSituationID)
            {
                flag = false;
            }
            if (Object1.InspectSituationNO != Object2.InspectSituationNO)
            {
                flag = false;
            }
            if ((Object1.ProjectCode != null) && (Object2.ProjectCode != null))
            {
                if (Object1.ProjectCode != Object2.ProjectCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ProjectCode == null) ^ (Object2.ProjectCode == null))
            {
                flag = false;
            }
            if (Object1.InspectDate.HasValue && Object2.InspectDate.HasValue)
            {
                if (Object1.InspectDate != Object2.InspectDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.InspectDate.HasValue ^ !Object2.InspectDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.Weather != null) && (Object2.Weather != null))
            {
                if (Object1.Weather != Object2.Weather)
                {
                    flag = false;
                }
            }
            else if ((Object1.Weather == null) ^ (Object2.Weather == null))
            {
                flag = false;
            }
            if ((Object1.InspectUserIpecialty != null) && (Object2.InspectUserIpecialty != null))
            {
                if (Object1.InspectUserIpecialty != Object2.InspectUserIpecialty)
                {
                    flag = false;
                }
            }
            else if ((Object1.InspectUserIpecialty == null) ^ (Object2.InspectUserIpecialty == null))
            {
                flag = false;
            }
            if ((Object1.InspectUser != null) && (Object2.InspectUser != null))
            {
                if (Object1.InspectUser != Object2.InspectUser)
                {
                    flag = false;
                }
            }
            else if ((Object1.InspectUser == null) ^ (Object2.InspectUser == null))
            {
                flag = false;
            }
            if ((Object1.KeyPoint != null) && (Object2.KeyPoint != null))
            {
                if (Object1.KeyPoint != Object2.KeyPoint)
                {
                    flag = false;
                }
            }
            else if ((Object1.KeyPoint == null) ^ (Object2.KeyPoint == null))
            {
                flag = false;
            }
            if (Object1.Status.HasValue && Object2.Status.HasValue)
            {
                if (Object1.Status != Object2.Status)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.Status.HasValue ^ !Object2.Status.HasValue)
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

        public void OnColumnChanged(InspectSituationColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(InspectSituationColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                InspectSituationEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new InspectSituationEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(InspectSituationColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(InspectSituationColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                InspectSituationEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new InspectSituationEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as InspectSituation);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as InspectSituationEntityData;
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
                    this.parentCollection.Remove((InspectSituation) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{10}{9}- InspectSituationID: {0}{9}- InspectSituationNO: {1}{9}- ProjectCode: {2}{9}- InspectDate: {3}{9}- Weather: {4}{9}- InspectUserIpecialty: {5}{9}- InspectUser: {6}{9}- KeyPoint: {7}{9}- Status: {8}{9}", new object[] { this.InspectSituationID, this.InspectSituationNO, (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), !this.InspectDate.HasValue ? string.Empty : this.InspectDate.ToString(), (this.Weather == null) ? string.Empty : this.Weather.ToString(), (this.InspectUserIpecialty == null) ? string.Empty : this.InspectUserIpecialty.ToString(), (this.InspectUser == null) ? string.Empty : this.InspectUser.ToString(), (this.KeyPoint == null) ? string.Empty : this.KeyPoint.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), Environment.NewLine, base.GetType() });
        }

        [XmlIgnore]
        public InspectSituationKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new InspectSituationKey(this);
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
                    this.entityTrackingKey = "InspectSituation" + this.InspectSituationID.ToString();
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

        [DataObjectField(false, false, true), Description("巡查日期"), TiannuoPM.Entities.Bindable]
        public virtual DateTime? InspectDate
        {
            get
            {
                return this.entityData.InspectDate;
            }
            set
            {
                if (this.entityData.InspectDate != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.InspectDate, this.entityData.InspectDate);
                    this.entityData.InspectDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.InspectDate, this.entityData.InspectDate);
                    this.OnPropertyChanged("InspectDate");
                }
            }
        }

        [ReadOnly(false), Description(""), TiannuoPM.Entities.Bindable, DataObjectField(true, true, false)]
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
                    this.OnColumnChanging(InspectSituationColumn.InspectSituationID, this.entityData.InspectSituationID);
                    this.entityData.InspectSituationID = value;
                    this.EntityId.InspectSituationID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.InspectSituationID, this.entityData.InspectSituationID);
                    this.OnPropertyChanged("InspectSituationID");
                }
            }
        }

        [Description("记录编号"), DataObjectField(false, false, false, 200), TiannuoPM.Entities.Bindable]
        public virtual string InspectSituationNO
        {
            get
            {
                return this.entityData.InspectSituationNO;
            }
            set
            {
                if (this.entityData.InspectSituationNO != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.InspectSituationNO, this.entityData.InspectSituationNO);
                    this.entityData.InspectSituationNO = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.InspectSituationNO, this.entityData.InspectSituationNO);
                    this.OnPropertyChanged("InspectSituationNO");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string InspectUser
        {
            get
            {
                return this.entityData.InspectUser;
            }
            set
            {
                if (this.entityData.InspectUser != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.InspectUser, this.entityData.InspectUser);
                    this.entityData.InspectUser = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.InspectUser, this.entityData.InspectUser);
                    this.OnPropertyChanged("InspectUser");
                }
            }
        }

        [Description("巡查人员专业"), DataObjectField(false, false, true, 200), TiannuoPM.Entities.Bindable]
        public virtual string InspectUserIpecialty
        {
            get
            {
                return this.entityData.InspectUserIpecialty;
            }
            set
            {
                if (this.entityData.InspectUserIpecialty != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.InspectUserIpecialty, this.entityData.InspectUserIpecialty);
                    this.entityData.InspectUserIpecialty = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.InspectUserIpecialty, this.entityData.InspectUserIpecialty);
                    this.OnPropertyChanged("InspectUserIpecialty");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
        public virtual SystemUser InspectUserSource
        {
            get
            {
                return this._inspectUserSource;
            }
            set
            {
                this._inspectUserSource = value;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 500), Description("")]
        public virtual string KeyPoint
        {
            get
            {
                return this.entityData.KeyPoint;
            }
            set
            {
                if (this.entityData.KeyPoint != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.KeyPoint, this.entityData.KeyPoint);
                    this.entityData.KeyPoint = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.KeyPoint, this.entityData.KeyPoint);
                    this.OnPropertyChanged("KeyPoint");
                }
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
                this.parentCollection = value as TList<InspectSituation>;
            }
        }

        [Description("项目编号"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ProjectCode
        {
            get
            {
                return this.entityData.ProjectCode;
            }
            set
            {
                if (this.entityData.ProjectCode != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [XmlIgnore, TiannuoPM.Entities.Bindable, Browsable(false)]
        public virtual Project ProjectCodeSource
        {
            get
            {
                return this._projectCodeSource;
            }
            set
            {
                this._projectCodeSource = value;
            }
        }

        [SoapIgnore, XmlIgnore, Browsable(false)]
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

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual int? Status
        {
            get
            {
                return this.entityData.Status;
            }
            set
            {
                if (this.entityData.Status != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "InspectSituationID", "InspectSituationNO", "ProjectCode", "InspectDate", "Weather", "InspectUserIpecialty", "InspectUser", "KeyPoint", "Status" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "InspectSituation";
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<Trouble> TroubleCollection
        {
            get
            {
                return this.entityData.TroubleCollection;
            }
            set
            {
                this.entityData.TroubleCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("天气"), DataObjectField(false, false, true, 50)]
        public virtual string Weather
        {
            get
            {
                return this.entityData.Weather;
            }
            set
            {
                if (this.entityData.Weather != value)
                {
                    this.OnColumnChanging(InspectSituationColumn.Weather, this.entityData.Weather);
                    this.entityData.Weather = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(InspectSituationColumn.Weather, this.entityData.Weather);
                    this.OnPropertyChanged("Weather");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class InspectSituationEntityData : ICloneable
        {
            public DateTime? InspectDate = null;
            public int InspectSituationID;
            public string InspectSituationNO = string.Empty;
            public string InspectUser = null;
            public string InspectUserIpecialty = null;
            public string KeyPoint = null;
            public string ProjectCode = null;
            public int? Status = null;
            private TList<Trouble> troubleInspectSituationID;
            public string Weather = null;

            public object Clone()
            {
                InspectSituationBase.InspectSituationEntityData data = new InspectSituationBase.InspectSituationEntityData();
                data.InspectSituationID = this.InspectSituationID;
                data.InspectSituationNO = this.InspectSituationNO;
                data.ProjectCode = this.ProjectCode;
                data.InspectDate = this.InspectDate;
                data.Weather = this.Weather;
                data.InspectUserIpecialty = this.InspectUserIpecialty;
                data.InspectUser = this.InspectUser;
                data.KeyPoint = this.KeyPoint;
                data.Status = this.Status;
                return data;
            }

            public TList<Trouble> TroubleCollection
            {
                get
                {
                    if (this.troubleInspectSituationID == null)
                    {
                        this.troubleInspectSituationID = new TList<Trouble>();
                    }
                    return this.troubleInspectSituationID;
                }
                set
                {
                    this.troubleInspectSituationID = value;
                }
            }
        }
    }
}

