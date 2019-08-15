namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class DictionaryNameBase : EntityBase, IEntityId<DictionaryNameKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private DictionaryNameKey _entityId;
        private ISite _site;
        private DictionaryNameEntityData backupData;
        private DictionaryNameEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<DictionaryName> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event DictionaryNameEventHandler ColumnChanged;

        [field: NonSerialized]
        public event DictionaryNameEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public DictionaryNameBase()
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new DictionaryNameEntityData();
            this.backupData = null;
        }

        public DictionaryNameBase(string dictionaryNameDictionaryNameCode, string dictionaryNameNAME, string dictionaryNameProjectCode, string dictionaryNameRemark)
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new DictionaryNameEntityData();
            this.backupData = null;
            this.DictionaryNameCode = dictionaryNameDictionaryNameCode;
            this.NAME = dictionaryNameNAME;
            this.ProjectCode = dictionaryNameProjectCode;
            this.Remark = dictionaryNameRemark;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "DictionaryNameCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DictionaryNameCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("NAME", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 400));
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

        public virtual DictionaryName Copy()
        {
            DictionaryName name = new DictionaryName();
            name.DictionaryNameCode = this.DictionaryNameCode;
            name.OriginalDictionaryNameCode = this.OriginalDictionaryNameCode;
            name.NAME = this.NAME;
            name.ProjectCode = this.ProjectCode;
            name.Remark = this.Remark;
            name.AcceptChanges();
            return name;
        }

        public static DictionaryName CreateDictionaryName(string dictionaryNameDictionaryNameCode, string dictionaryNameNAME, string dictionaryNameProjectCode, string dictionaryNameRemark)
        {
            DictionaryName name = new DictionaryName();
            name.DictionaryNameCode = dictionaryNameDictionaryNameCode;
            name.NAME = dictionaryNameNAME;
            name.ProjectCode = dictionaryNameProjectCode;
            name.Remark = dictionaryNameRemark;
            return name;
        }

        public virtual DictionaryName DeepCopy()
        {
            return EntityHelper.Clone<DictionaryName>(this as DictionaryName);
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

        public virtual bool Equals(DictionaryNameBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(DictionaryNameBase Object1, DictionaryNameBase Object2)
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
            if (Object1.DictionaryNameCode != Object2.DictionaryNameCode)
            {
                flag = false;
            }
            if ((Object1.NAME != null) && (Object2.NAME != null))
            {
                if (Object1.NAME != Object2.NAME)
                {
                    flag = false;
                }
            }
            else if ((Object1.NAME == null) ^ (Object2.NAME == null))
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
            if ((Object1.Remark != null) && (Object2.Remark != null))
            {
                if (Object1.Remark != Object2.Remark)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.Remark == null) ^ (Object2.Remark == null))
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

        public void OnColumnChanged(DictionaryNameColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(DictionaryNameColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                DictionaryNameEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new DictionaryNameEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(DictionaryNameColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(DictionaryNameColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                DictionaryNameEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new DictionaryNameEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as DictionaryName);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as DictionaryNameEntityData;
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
                    this.parentCollection.Remove((DictionaryName) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{5}{4}- DictionaryNameCode: {0}{4}- NAME: {1}{4}- ProjectCode: {2}{4}- Remark: {3}{4}", new object[] { this.DictionaryNameCode, (this.NAME == null) ? string.Empty : this.NAME.ToString(), (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), Environment.NewLine, base.GetType() });
        }

        [TiannuoPM.Entities.Bindable]
        public TList<DictionaryItem> DictionaryItemCollection
        {
            get
            {
                return this.entityData.DictionaryItemCollection;
            }
            set
            {
                this.entityData.DictionaryItemCollection = value;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
        public virtual string DictionaryNameCode
        {
            get
            {
                return this.entityData.DictionaryNameCode;
            }
            set
            {
                if (this.entityData.DictionaryNameCode != value)
                {
                    this.OnColumnChanging(DictionaryNameColumn.DictionaryNameCode, this.entityData.DictionaryNameCode);
                    this.entityData.DictionaryNameCode = value;
                    this.EntityId.DictionaryNameCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryNameColumn.DictionaryNameCode, this.entityData.DictionaryNameCode);
                    this.OnPropertyChanged("DictionaryNameCode");
                }
            }
        }

        [XmlIgnore]
        public DictionaryNameKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new DictionaryNameKey(this);
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
                    this.entityTrackingKey = "DictionaryName" + this.DictionaryNameCode.ToString();
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

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string NAME
        {
            get
            {
                return this.entityData.NAME;
            }
            set
            {
                if (this.entityData.NAME != value)
                {
                    this.OnColumnChanging(DictionaryNameColumn.NAME, this.entityData.NAME);
                    this.entityData.NAME = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryNameColumn.NAME, this.entityData.NAME);
                    this.OnPropertyChanged("NAME");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalDictionaryNameCode
        {
            get
            {
                return this.entityData.OriginalDictionaryNameCode;
            }
            set
            {
                this.entityData.OriginalDictionaryNameCode = value;
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
                this.parentCollection = value as TList<DictionaryName>;
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(DictionaryNameColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryNameColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 400), Description("")]
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
                    this.OnColumnChanging(DictionaryNameColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryNameColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
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

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "DictionaryNameCode", "NAME", "ProjectCode", "Remark" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "DictionaryName";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class DictionaryNameEntityData : ICloneable
        {
            private TList<DictionaryItem> dictionaryItemDictionaryNameCode;
            public string DictionaryNameCode;
            public string NAME = null;
            public string OriginalDictionaryNameCode;
            public string ProjectCode = null;
            public string Remark = null;

            public object Clone()
            {
                DictionaryNameBase.DictionaryNameEntityData data = new DictionaryNameBase.DictionaryNameEntityData();
                data.DictionaryNameCode = this.DictionaryNameCode;
                data.OriginalDictionaryNameCode = this.OriginalDictionaryNameCode;
                data.NAME = this.NAME;
                data.ProjectCode = this.ProjectCode;
                data.Remark = this.Remark;
                return data;
            }

            public TList<DictionaryItem> DictionaryItemCollection
            {
                get
                {
                    if (this.dictionaryItemDictionaryNameCode == null)
                    {
                        this.dictionaryItemDictionaryNameCode = new TList<DictionaryItem>();
                    }
                    return this.dictionaryItemDictionaryNameCode;
                }
                set
                {
                    this.dictionaryItemDictionaryNameCode = value;
                }
            }
        }
    }
}

