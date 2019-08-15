namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class DictionaryItemBase : EntityBase, IEntityId<DictionaryItemKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private DictionaryName _dictionaryNameCodeSource;
        private DictionaryItemKey _entityId;
        private ISite _site;
        private DictionaryItemEntityData backupData;
        private DictionaryItemEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<DictionaryItem> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event DictionaryItemEventHandler ColumnChanged;

        [field: NonSerialized]
        public event DictionaryItemEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public DictionaryItemBase()
        {
            this.inTxn = false;
            this._dictionaryNameCodeSource = null;
            this._site = null;
            this.entityData = new DictionaryItemEntityData();
            this.backupData = null;
        }

        public DictionaryItemBase(string dictionaryItemDictionaryItemCode, string dictionaryItemProjectCode, string dictionaryItemDictionaryNameCode, int? dictionaryItemSortID, string dictionaryItemName)
        {
            this.inTxn = false;
            this._dictionaryNameCodeSource = null;
            this._site = null;
            this.entityData = new DictionaryItemEntityData();
            this.backupData = null;
            this.DictionaryItemCode = dictionaryItemDictionaryItemCode;
            this.ProjectCode = dictionaryItemProjectCode;
            this.DictionaryNameCode = dictionaryItemDictionaryNameCode;
            this.SortID = dictionaryItemSortID;
            this.Name = dictionaryItemName;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "DictionaryItemCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DictionaryItemCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DictionaryNameCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Name", 50));
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

        public virtual DictionaryItem Copy()
        {
            DictionaryItem item = new DictionaryItem();
            item.DictionaryItemCode = this.DictionaryItemCode;
            item.OriginalDictionaryItemCode = this.OriginalDictionaryItemCode;
            item.ProjectCode = this.ProjectCode;
            item.DictionaryNameCode = this.DictionaryNameCode;
            item.SortID = this.SortID;
            item.Name = this.Name;
            item.AcceptChanges();
            return item;
        }

        public static DictionaryItem CreateDictionaryItem(string dictionaryItemDictionaryItemCode, string dictionaryItemProjectCode, string dictionaryItemDictionaryNameCode, int? dictionaryItemSortID, string dictionaryItemName)
        {
            DictionaryItem item = new DictionaryItem();
            item.DictionaryItemCode = dictionaryItemDictionaryItemCode;
            item.ProjectCode = dictionaryItemProjectCode;
            item.DictionaryNameCode = dictionaryItemDictionaryNameCode;
            item.SortID = dictionaryItemSortID;
            item.Name = dictionaryItemName;
            return item;
        }

        public virtual DictionaryItem DeepCopy()
        {
            return EntityHelper.Clone<DictionaryItem>(this as DictionaryItem);
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

        public virtual bool Equals(DictionaryItemBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(DictionaryItemBase Object1, DictionaryItemBase Object2)
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
            if (Object1.DictionaryItemCode != Object2.DictionaryItemCode)
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
            if ((Object1.DictionaryNameCode != null) && (Object2.DictionaryNameCode != null))
            {
                if (Object1.DictionaryNameCode != Object2.DictionaryNameCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.DictionaryNameCode == null) ^ (Object2.DictionaryNameCode == null))
            {
                flag = false;
            }
            if (Object1.SortID.HasValue && Object2.SortID.HasValue)
            {
                if (Object1.SortID != Object2.SortID)
                {
                    flag = false;
                }
            }
            else if (!Object1.SortID.HasValue ^ !Object2.SortID.HasValue)
            {
                flag = false;
            }
            if ((Object1.Name != null) && (Object2.Name != null))
            {
                if (Object1.Name != Object2.Name)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.Name == null) ^ (Object2.Name == null))
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

        public void OnColumnChanged(DictionaryItemColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(DictionaryItemColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                DictionaryItemEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new DictionaryItemEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(DictionaryItemColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(DictionaryItemColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                DictionaryItemEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new DictionaryItemEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as DictionaryItem);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as DictionaryItemEntityData;
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
                    this.parentCollection.Remove((DictionaryItem) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{6}{5}- DictionaryItemCode: {0}{5}- ProjectCode: {1}{5}- DictionaryNameCode: {2}{5}- SortID: {3}{5}- Name: {4}{5}", new object[] { this.DictionaryItemCode, (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.DictionaryNameCode == null) ? string.Empty : this.DictionaryNameCode.ToString(), !this.SortID.HasValue ? string.Empty : this.SortID.ToString(), (this.Name == null) ? string.Empty : this.Name.ToString(), Environment.NewLine, base.GetType() });
        }

        [DataObjectField(true, false, false, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string DictionaryItemCode
        {
            get
            {
                return this.entityData.DictionaryItemCode;
            }
            set
            {
                if (this.entityData.DictionaryItemCode != value)
                {
                    this.OnColumnChanging(DictionaryItemColumn.DictionaryItemCode, this.entityData.DictionaryItemCode);
                    this.entityData.DictionaryItemCode = value;
                    this.EntityId.DictionaryItemCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryItemColumn.DictionaryItemCode, this.entityData.DictionaryItemCode);
                    this.OnPropertyChanged("DictionaryItemCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(DictionaryItemColumn.DictionaryNameCode, this.entityData.DictionaryNameCode);
                    this.entityData.DictionaryNameCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryItemColumn.DictionaryNameCode, this.entityData.DictionaryNameCode);
                    this.OnPropertyChanged("DictionaryNameCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
        public virtual DictionaryName DictionaryNameCodeSource
        {
            get
            {
                return this._dictionaryNameCodeSource;
            }
            set
            {
                this._dictionaryNameCodeSource = value;
            }
        }

        [XmlIgnore]
        public DictionaryItemKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new DictionaryItemKey(this);
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
                    this.entityTrackingKey = "DictionaryItem" + this.DictionaryItemCode.ToString();
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

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Name
        {
            get
            {
                return this.entityData.Name;
            }
            set
            {
                if (this.entityData.Name != value)
                {
                    this.OnColumnChanging(DictionaryItemColumn.Name, this.entityData.Name);
                    this.entityData.Name = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryItemColumn.Name, this.entityData.Name);
                    this.OnPropertyChanged("Name");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalDictionaryItemCode
        {
            get
            {
                return this.entityData.OriginalDictionaryItemCode;
            }
            set
            {
                this.entityData.OriginalDictionaryItemCode = value;
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
                this.parentCollection = value as TList<DictionaryItem>;
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(DictionaryItemColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryItemColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
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

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual int? SortID
        {
            get
            {
                return this.entityData.SortID;
            }
            set
            {
                if (this.entityData.SortID != value)
                {
                    this.OnColumnChanging(DictionaryItemColumn.SortID, this.entityData.SortID);
                    this.entityData.SortID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(DictionaryItemColumn.SortID, this.entityData.SortID);
                    this.OnPropertyChanged("SortID");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "DictionaryItemCode", "ProjectCode", "DictionaryNameCode", "SortID", "Name" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "DictionaryItem";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class DictionaryItemEntityData : ICloneable
        {
            public string DictionaryItemCode;
            public string DictionaryNameCode = null;
            public string Name = null;
            public string OriginalDictionaryItemCode;
            public string ProjectCode = null;
            public int? SortID = null;

            public object Clone()
            {
                DictionaryItemBase.DictionaryItemEntityData data = new DictionaryItemBase.DictionaryItemEntityData();
                data.DictionaryItemCode = this.DictionaryItemCode;
                data.OriginalDictionaryItemCode = this.OriginalDictionaryItemCode;
                data.ProjectCode = this.ProjectCode;
                data.DictionaryNameCode = this.DictionaryNameCode;
                data.SortID = this.SortID;
                data.Name = this.Name;
                return data;
            }
        }
    }
}

