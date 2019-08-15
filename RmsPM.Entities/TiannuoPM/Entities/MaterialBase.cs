namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class MaterialBase : EntityBase, IEntityId<MaterialKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private MaterialKey _entityId;
        private ISite _site;
        private MaterialEntityData backupData;
        private MaterialEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Material> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event MaterialEventHandler ColumnChanged;

        [field: NonSerialized]
        public event MaterialEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public MaterialBase()
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new MaterialEntityData();
            this.backupData = null;
        }

        public MaterialBase(int materialMaterialCode, string materialMaterialName, string materialGroupCode, string materialSpec, string materialUnit, decimal? materialStandardPrice, string materialInputPerson, DateTime? materialInputDate, string materialRemark)
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new MaterialEntityData();
            this.backupData = null;
            this.MaterialCode = materialMaterialCode;
            this.MaterialName = materialMaterialName;
            this.GroupCode = materialGroupCode;
            this.Spec = materialSpec;
            this.Unit = materialUnit;
            this.StandardPrice = materialStandardPrice;
            this.InputPerson = materialInputPerson;
            this.InputDate = materialInputDate;
            this.Remark = materialRemark;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MaterialName", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("GroupCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Spec", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Unit", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InputPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
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

        public virtual Material Copy()
        {
            Material material = new Material();
            material.MaterialCode = this.MaterialCode;
            material.OriginalMaterialCode = this.OriginalMaterialCode;
            material.MaterialName = this.MaterialName;
            material.GroupCode = this.GroupCode;
            material.Spec = this.Spec;
            material.Unit = this.Unit;
            material.StandardPrice = this.StandardPrice;
            material.InputPerson = this.InputPerson;
            material.InputDate = this.InputDate;
            material.Remark = this.Remark;
            material.AcceptChanges();
            return material;
        }

        public static Material CreateMaterial(int materialMaterialCode, string materialMaterialName, string materialGroupCode, string materialSpec, string materialUnit, decimal? materialStandardPrice, string materialInputPerson, DateTime? materialInputDate, string materialRemark)
        {
            Material material = new Material();
            material.MaterialCode = materialMaterialCode;
            material.MaterialName = materialMaterialName;
            material.GroupCode = materialGroupCode;
            material.Spec = materialSpec;
            material.Unit = materialUnit;
            material.StandardPrice = materialStandardPrice;
            material.InputPerson = materialInputPerson;
            material.InputDate = materialInputDate;
            material.Remark = materialRemark;
            return material;
        }

        public virtual Material DeepCopy()
        {
            return EntityHelper.Clone<Material>(this as Material);
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

        public virtual bool Equals(MaterialBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(MaterialBase Object1, MaterialBase Object2)
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
            if (Object1.MaterialCode != Object2.MaterialCode)
            {
                flag = false;
            }
            if ((Object1.MaterialName != null) && (Object2.MaterialName != null))
            {
                if (Object1.MaterialName != Object2.MaterialName)
                {
                    flag = false;
                }
            }
            else if ((Object1.MaterialName == null) ^ (Object2.MaterialName == null))
            {
                flag = false;
            }
            if ((Object1.GroupCode != null) && (Object2.GroupCode != null))
            {
                if (Object1.GroupCode != Object2.GroupCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.GroupCode == null) ^ (Object2.GroupCode == null))
            {
                flag = false;
            }
            if ((Object1.Spec != null) && (Object2.Spec != null))
            {
                if (Object1.Spec != Object2.Spec)
                {
                    flag = false;
                }
            }
            else if ((Object1.Spec == null) ^ (Object2.Spec == null))
            {
                flag = false;
            }
            if ((Object1.Unit != null) && (Object2.Unit != null))
            {
                if (Object1.Unit != Object2.Unit)
                {
                    flag = false;
                }
            }
            else if ((Object1.Unit == null) ^ (Object2.Unit == null))
            {
                flag = false;
            }
            if (Object1.StandardPrice.HasValue && Object2.StandardPrice.HasValue)
            {
                if (Object1.StandardPrice != Object2.StandardPrice)
                {
                    flag = false;
                }
            }
            else if (!Object1.StandardPrice.HasValue ^ !Object2.StandardPrice.HasValue)
            {
                flag = false;
            }
            if ((Object1.InputPerson != null) && (Object2.InputPerson != null))
            {
                if (Object1.InputPerson != Object2.InputPerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.InputPerson == null) ^ (Object2.InputPerson == null))
            {
                flag = false;
            }
            if (Object1.InputDate.HasValue && Object2.InputDate.HasValue)
            {
                if (Object1.InputDate != Object2.InputDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.InputDate.HasValue ^ !Object2.InputDate.HasValue)
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

        public void OnColumnChanged(MaterialColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(MaterialColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                MaterialEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new MaterialEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(MaterialColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(MaterialColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                MaterialEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new MaterialEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Material);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as MaterialEntityData;
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
                    this.parentCollection.Remove((Material) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{10}{9}- MaterialCode: {0}{9}- MaterialName: {1}{9}- GroupCode: {2}{9}- Spec: {3}{9}- Unit: {4}{9}- StandardPrice: {5}{9}- InputPerson: {6}{9}- InputDate: {7}{9}- Remark: {8}{9}", new object[] { this.MaterialCode, (this.MaterialName == null) ? string.Empty : this.MaterialName.ToString(), (this.GroupCode == null) ? string.Empty : this.GroupCode.ToString(), (this.Spec == null) ? string.Empty : this.Spec.ToString(), (this.Unit == null) ? string.Empty : this.Unit.ToString(), !this.StandardPrice.HasValue ? string.Empty : this.StandardPrice.ToString(), (this.InputPerson == null) ? string.Empty : this.InputPerson.ToString(), !this.InputDate.HasValue ? string.Empty : this.InputDate.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), Environment.NewLine, base.GetType() });
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractMaterial> ContractMaterialCollection
        {
            get
            {
                return this.entityData.ContractMaterialCollection;
            }
            set
            {
                this.entityData.ContractMaterialCollection = value;
            }
        }

        [XmlIgnore]
        public MaterialKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new MaterialKey(this);
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
                    this.entityTrackingKey = "Material" + this.MaterialCode.ToString();
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
        public virtual string GroupCode
        {
            get
            {
                return this.entityData.GroupCode;
            }
            set
            {
                if (this.entityData.GroupCode != value)
                {
                    this.OnColumnChanging(MaterialColumn.GroupCode, this.entityData.GroupCode);
                    this.entityData.GroupCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.GroupCode, this.entityData.GroupCode);
                    this.OnPropertyChanged("GroupCode");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? InputDate
        {
            get
            {
                return this.entityData.InputDate;
            }
            set
            {
                if (this.entityData.InputDate != value)
                {
                    this.OnColumnChanging(MaterialColumn.InputDate, this.entityData.InputDate);
                    this.entityData.InputDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.InputDate, this.entityData.InputDate);
                    this.OnPropertyChanged("InputDate");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string InputPerson
        {
            get
            {
                return this.entityData.InputPerson;
            }
            set
            {
                if (this.entityData.InputPerson != value)
                {
                    this.OnColumnChanging(MaterialColumn.InputPerson, this.entityData.InputPerson);
                    this.entityData.InputPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.InputPerson, this.entityData.InputPerson);
                    this.OnPropertyChanged("InputPerson");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(true, false, false), Description("")]
        public virtual int MaterialCode
        {
            get
            {
                return this.entityData.MaterialCode;
            }
            set
            {
                if (this.entityData.MaterialCode != value)
                {
                    this.OnColumnChanging(MaterialColumn.MaterialCode, this.entityData.MaterialCode);
                    this.entityData.MaterialCode = value;
                    this.EntityId.MaterialCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.MaterialCode, this.entityData.MaterialCode);
                    this.OnPropertyChanged("MaterialCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 100)]
        public virtual string MaterialName
        {
            get
            {
                return this.entityData.MaterialName;
            }
            set
            {
                if (this.entityData.MaterialName != value)
                {
                    this.OnColumnChanging(MaterialColumn.MaterialName, this.entityData.MaterialName);
                    this.entityData.MaterialName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.MaterialName, this.entityData.MaterialName);
                    this.OnPropertyChanged("MaterialName");
                }
            }
        }

        [Browsable(false)]
        public virtual int OriginalMaterialCode
        {
            get
            {
                return this.entityData.OriginalMaterialCode;
            }
            set
            {
                this.entityData.OriginalMaterialCode = value;
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
                this.parentCollection = value as TList<Material>;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 800)]
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
                    this.OnColumnChanging(MaterialColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
            }
        }

        [Browsable(false), XmlIgnore, SoapIgnore]
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

        [Description(""), DataObjectField(false, false, true, 200), TiannuoPM.Entities.Bindable]
        public virtual string Spec
        {
            get
            {
                return this.entityData.Spec;
            }
            set
            {
                if (this.entityData.Spec != value)
                {
                    this.OnColumnChanging(MaterialColumn.Spec, this.entityData.Spec);
                    this.entityData.Spec = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.Spec, this.entityData.Spec);
                    this.OnPropertyChanged("Spec");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? StandardPrice
        {
            get
            {
                return this.entityData.StandardPrice;
            }
            set
            {
                if (this.entityData.StandardPrice != value)
                {
                    this.OnColumnChanging(MaterialColumn.StandardPrice, this.entityData.StandardPrice);
                    this.entityData.StandardPrice = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.StandardPrice, this.entityData.StandardPrice);
                    this.OnPropertyChanged("StandardPrice");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "MaterialCode", "MaterialName", "GroupCode", "Spec", "Unit", "StandardPrice", "InputPerson", "InputDate", "Remark" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "Material";
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Unit
        {
            get
            {
                return this.entityData.Unit;
            }
            set
            {
                if (this.entityData.Unit != value)
                {
                    this.OnColumnChanging(MaterialColumn.Unit, this.entityData.Unit);
                    this.entityData.Unit = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialColumn.Unit, this.entityData.Unit);
                    this.OnPropertyChanged("Unit");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class MaterialEntityData : ICloneable
        {
            private TList<ContractMaterial> contractMaterialMaterialCode;
            public string GroupCode = null;
            public DateTime? InputDate = null;
            public string InputPerson = null;
            public int MaterialCode;
            public string MaterialName = null;
            public int OriginalMaterialCode;
            public string Remark = null;
            public string Spec = null;
            public decimal? StandardPrice = null;
            public string Unit = null;

            public object Clone()
            {
                MaterialBase.MaterialEntityData data = new MaterialBase.MaterialEntityData();
                data.MaterialCode = this.MaterialCode;
                data.OriginalMaterialCode = this.OriginalMaterialCode;
                data.MaterialName = this.MaterialName;
                data.GroupCode = this.GroupCode;
                data.Spec = this.Spec;
                data.Unit = this.Unit;
                data.StandardPrice = this.StandardPrice;
                data.InputPerson = this.InputPerson;
                data.InputDate = this.InputDate;
                data.Remark = this.Remark;
                return data;
            }

            public TList<ContractMaterial> ContractMaterialCollection
            {
                get
                {
                    if (this.contractMaterialMaterialCode == null)
                    {
                        this.contractMaterialMaterialCode = new TList<ContractMaterial>();
                    }
                    return this.contractMaterialMaterialCode;
                }
                set
                {
                    this.contractMaterialMaterialCode = value;
                }
            }
        }
    }
}

