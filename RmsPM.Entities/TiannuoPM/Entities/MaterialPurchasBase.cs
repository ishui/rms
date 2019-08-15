namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class MaterialPurchasBase : EntityBase, IEntityId<MaterialPurchasKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private MaterialPurchasKey _entityId;
        private Project _projectCodeSource;
        private ISite _site;
        private MaterialPurchasEntityData backupData;
        private MaterialPurchasEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<MaterialPurchas> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event MaterialPurchasEventHandler ColumnChanged;

        [field: NonSerialized]
        public event MaterialPurchasEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public MaterialPurchasBase()
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new MaterialPurchasEntityData();
            this.backupData = null;
        }

        public MaterialPurchasBase(string materialPurchasMaterialPurchasCode, string materialPurchasPurchasUnitCode, DateTime? materialPurchasPurchasDate, string materialPurchasProjectCode, string materialPurchasTitle, string materialPurchasDescription, string materialPurchasFollowUserCode, string materialPurchasStatus)
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new MaterialPurchasEntityData();
            this.backupData = null;
            this.MaterialPurchasCode = materialPurchasMaterialPurchasCode;
            this.PurchasUnitCode = materialPurchasPurchasUnitCode;
            this.PurchasDate = materialPurchasPurchasDate;
            this.ProjectCode = materialPurchasProjectCode;
            this.Title = materialPurchasTitle;
            this.Description = materialPurchasDescription;
            this.FollowUserCode = materialPurchasFollowUserCode;
            this.Status = materialPurchasStatus;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MaterialPurchasCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PurchasUnitCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Title", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Description", 0x1388));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("FollowUserCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Status", 50));
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

        public virtual MaterialPurchas Copy()
        {
            MaterialPurchas purchas = new MaterialPurchas();
            purchas.MaterialPurchasID = this.MaterialPurchasID;
            purchas.MaterialPurchasCode = this.MaterialPurchasCode;
            purchas.PurchasUnitCode = this.PurchasUnitCode;
            purchas.PurchasDate = this.PurchasDate;
            purchas.ProjectCode = this.ProjectCode;
            purchas.Title = this.Title;
            purchas.Description = this.Description;
            purchas.FollowUserCode = this.FollowUserCode;
            purchas.Status = this.Status;
            purchas.AcceptChanges();
            return purchas;
        }

        public static MaterialPurchas CreateMaterialPurchas(string materialPurchasMaterialPurchasCode, string materialPurchasPurchasUnitCode, DateTime? materialPurchasPurchasDate, string materialPurchasProjectCode, string materialPurchasTitle, string materialPurchasDescription, string materialPurchasFollowUserCode, string materialPurchasStatus)
        {
            MaterialPurchas purchas = new MaterialPurchas();
            purchas.MaterialPurchasCode = materialPurchasMaterialPurchasCode;
            purchas.PurchasUnitCode = materialPurchasPurchasUnitCode;
            purchas.PurchasDate = materialPurchasPurchasDate;
            purchas.ProjectCode = materialPurchasProjectCode;
            purchas.Title = materialPurchasTitle;
            purchas.Description = materialPurchasDescription;
            purchas.FollowUserCode = materialPurchasFollowUserCode;
            purchas.Status = materialPurchasStatus;
            return purchas;
        }

        public virtual MaterialPurchas DeepCopy()
        {
            return EntityHelper.Clone<MaterialPurchas>(this as MaterialPurchas);
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

        public virtual bool Equals(MaterialPurchasBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(MaterialPurchasBase Object1, MaterialPurchasBase Object2)
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
            if (Object1.MaterialPurchasID != Object2.MaterialPurchasID)
            {
                flag = false;
            }
            if ((Object1.MaterialPurchasCode != null) && (Object2.MaterialPurchasCode != null))
            {
                if (Object1.MaterialPurchasCode != Object2.MaterialPurchasCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.MaterialPurchasCode == null) ^ (Object2.MaterialPurchasCode == null))
            {
                flag = false;
            }
            if ((Object1.PurchasUnitCode != null) && (Object2.PurchasUnitCode != null))
            {
                if (Object1.PurchasUnitCode != Object2.PurchasUnitCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PurchasUnitCode == null) ^ (Object2.PurchasUnitCode == null))
            {
                flag = false;
            }
            if (Object1.PurchasDate.HasValue && Object2.PurchasDate.HasValue)
            {
                if (Object1.PurchasDate != Object2.PurchasDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PurchasDate.HasValue ^ !Object2.PurchasDate.HasValue)
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
            if ((Object1.Title != null) && (Object2.Title != null))
            {
                if (Object1.Title != Object2.Title)
                {
                    flag = false;
                }
            }
            else if ((Object1.Title == null) ^ (Object2.Title == null))
            {
                flag = false;
            }
            if ((Object1.Description != null) && (Object2.Description != null))
            {
                if (Object1.Description != Object2.Description)
                {
                    flag = false;
                }
            }
            else if ((Object1.Description == null) ^ (Object2.Description == null))
            {
                flag = false;
            }
            if ((Object1.FollowUserCode != null) && (Object2.FollowUserCode != null))
            {
                if (Object1.FollowUserCode != Object2.FollowUserCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.FollowUserCode == null) ^ (Object2.FollowUserCode == null))
            {
                flag = false;
            }
            if ((Object1.Status != null) && (Object2.Status != null))
            {
                if (Object1.Status != Object2.Status)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.Status == null) ^ (Object2.Status == null))
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

        public void OnColumnChanged(MaterialPurchasColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(MaterialPurchasColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                MaterialPurchasEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new MaterialPurchasEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(MaterialPurchasColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(MaterialPurchasColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                MaterialPurchasEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new MaterialPurchasEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as MaterialPurchas);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as MaterialPurchasEntityData;
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
                    this.parentCollection.Remove((MaterialPurchas) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{10}{9}- MaterialPurchasID: {0}{9}- MaterialPurchasCode: {1}{9}- PurchasUnitCode: {2}{9}- PurchasDate: {3}{9}- ProjectCode: {4}{9}- Title: {5}{9}- Description: {6}{9}- FollowUserCode: {7}{9}- Status: {8}{9}", new object[] { this.MaterialPurchasID, (this.MaterialPurchasCode == null) ? string.Empty : this.MaterialPurchasCode.ToString(), (this.PurchasUnitCode == null) ? string.Empty : this.PurchasUnitCode.ToString(), !this.PurchasDate.HasValue ? string.Empty : this.PurchasDate.ToString(), (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.Title == null) ? string.Empty : this.Title.ToString(), (this.Description == null) ? string.Empty : this.Description.ToString(), (this.FollowUserCode == null) ? string.Empty : this.FollowUserCode.ToString(), (this.Status == null) ? string.Empty : this.Status.ToString(), Environment.NewLine, base.GetType() });
        }

        [Description("描述"), DataObjectField(false, false, true, 0x1388), TiannuoPM.Entities.Bindable]
        public virtual string Description
        {
            get
            {
                return this.entityData.Description;
            }
            set
            {
                if (this.entityData.Description != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.Description, this.entityData.Description);
                    this.entityData.Description = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.Description, this.entityData.Description);
                    this.OnPropertyChanged("Description");
                }
            }
        }

        [XmlIgnore]
        public MaterialPurchasKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new MaterialPurchasKey(this);
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
                    this.entityTrackingKey = "MaterialPurchas" + this.MaterialPurchasID.ToString();
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

        [DataObjectField(false, false, true, 50), Description("跟单人Code"), TiannuoPM.Entities.Bindable]
        public virtual string FollowUserCode
        {
            get
            {
                return this.entityData.FollowUserCode;
            }
            set
            {
                if (this.entityData.FollowUserCode != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.FollowUserCode, this.entityData.FollowUserCode);
                    this.entityData.FollowUserCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.FollowUserCode, this.entityData.FollowUserCode);
                    this.OnPropertyChanged("FollowUserCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string MaterialPurchasCode
        {
            get
            {
                return this.entityData.MaterialPurchasCode;
            }
            set
            {
                if (this.entityData.MaterialPurchasCode != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.MaterialPurchasCode, this.entityData.MaterialPurchasCode);
                    this.entityData.MaterialPurchasCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.MaterialPurchasCode, this.entityData.MaterialPurchasCode);
                    this.OnPropertyChanged("MaterialPurchasCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<MaterialPurchasDtl> MaterialPurchasDtlCollection
        {
            get
            {
                return this.entityData.MaterialPurchasDtlCollection;
            }
            set
            {
                this.entityData.MaterialPurchasDtlCollection = value;
            }
        }

        [DataObjectField(true, true, false), TiannuoPM.Entities.Bindable, ReadOnly(false), Description("材料Code")]
        public virtual int MaterialPurchasID
        {
            get
            {
                return this.entityData.MaterialPurchasID;
            }
            set
            {
                if (this.entityData.MaterialPurchasID != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.MaterialPurchasID, this.entityData.MaterialPurchasID);
                    this.entityData.MaterialPurchasID = value;
                    this.EntityId.MaterialPurchasID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.MaterialPurchasID, this.entityData.MaterialPurchasID);
                    this.OnPropertyChanged("MaterialPurchasID");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<MaterialPurchas>;
            }
        }

        [Description("项目名称"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(MaterialPurchasColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
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

        [DataObjectField(false, false, true), Description("采购日期"), TiannuoPM.Entities.Bindable]
        public virtual DateTime? PurchasDate
        {
            get
            {
                return this.entityData.PurchasDate;
            }
            set
            {
                if (this.entityData.PurchasDate != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.PurchasDate, this.entityData.PurchasDate);
                    this.entityData.PurchasDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.PurchasDate, this.entityData.PurchasDate);
                    this.OnPropertyChanged("PurchasDate");
                }
            }
        }

        [Description("采购部门"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string PurchasUnitCode
        {
            get
            {
                return this.entityData.PurchasUnitCode;
            }
            set
            {
                if (this.entityData.PurchasUnitCode != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.PurchasUnitCode, this.entityData.PurchasUnitCode);
                    this.entityData.PurchasUnitCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.PurchasUnitCode, this.entityData.PurchasUnitCode);
                    this.OnPropertyChanged("PurchasUnitCode");
                }
            }
        }

        [XmlIgnore, SoapIgnore, Browsable(false)]
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

        [DataObjectField(false, false, true, 50), Description("状态"), TiannuoPM.Entities.Bindable]
        public virtual string Status
        {
            get
            {
                return this.entityData.Status;
            }
            set
            {
                if (this.entityData.Status != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "MaterialPurchasID", "MaterialPurchasCode", "PurchasUnitCode", "PurchasDate", "ProjectCode", "Title", "Description", "FollowUserCode", "Status" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "MaterialPurchas";
            }
        }

        [DataObjectField(false, false, true, 50), Description("标题"), TiannuoPM.Entities.Bindable]
        public virtual string Title
        {
            get
            {
                return this.entityData.Title;
            }
            set
            {
                if (this.entityData.Title != value)
                {
                    this.OnColumnChanging(MaterialPurchasColumn.Title, this.entityData.Title);
                    this.entityData.Title = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasColumn.Title, this.entityData.Title);
                    this.OnPropertyChanged("Title");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class MaterialPurchasEntityData : ICloneable
        {
            public string Description = null;
            public string FollowUserCode = null;
            public string MaterialPurchasCode = null;
            private TList<MaterialPurchasDtl> materialPurchasDtlMaterialPurchasID;
            public int MaterialPurchasID;
            public string ProjectCode = null;
            public DateTime? PurchasDate = null;
            public string PurchasUnitCode = null;
            public string Status = null;
            public string Title = null;

            public object Clone()
            {
                MaterialPurchasBase.MaterialPurchasEntityData data = new MaterialPurchasBase.MaterialPurchasEntityData();
                data.MaterialPurchasID = this.MaterialPurchasID;
                data.MaterialPurchasCode = this.MaterialPurchasCode;
                data.PurchasUnitCode = this.PurchasUnitCode;
                data.PurchasDate = this.PurchasDate;
                data.ProjectCode = this.ProjectCode;
                data.Title = this.Title;
                data.Description = this.Description;
                data.FollowUserCode = this.FollowUserCode;
                data.Status = this.Status;
                return data;
            }

            public TList<MaterialPurchasDtl> MaterialPurchasDtlCollection
            {
                get
                {
                    if (this.materialPurchasDtlMaterialPurchasID == null)
                    {
                        this.materialPurchasDtlMaterialPurchasID = new TList<MaterialPurchasDtl>();
                    }
                    return this.materialPurchasDtlMaterialPurchasID;
                }
                set
                {
                    this.materialPurchasDtlMaterialPurchasID = value;
                }
            }
        }
    }
}

