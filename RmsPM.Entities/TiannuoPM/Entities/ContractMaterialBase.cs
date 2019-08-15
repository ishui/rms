namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ContractMaterialBase : EntityBase, IEntityId<ContractMaterialKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractMaterialKey _entityId;
        private Material _materialCodeSource;
        private ISite _site;
        private ContractMaterialEntityData backupData;
        private ContractMaterialEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractMaterial> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractMaterialEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractMaterialEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractMaterialBase()
        {
            this.inTxn = false;
            this._materialCodeSource = null;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractMaterialEntityData();
            this.backupData = null;
        }

        public ContractMaterialBase(string contractMaterialContractMaterialCode, string contractMaterialContractCode, int? contractMaterialMaterialCode, decimal? contractMaterialQty, decimal? contractMaterialPrice, decimal? contractMaterialMoney)
        {
            this.inTxn = false;
            this._materialCodeSource = null;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractMaterialEntityData();
            this.backupData = null;
            this.ContractMaterialCode = contractMaterialContractMaterialCode;
            this.ContractCode = contractMaterialContractCode;
            this.MaterialCode = contractMaterialMaterialCode;
            this.Qty = contractMaterialQty;
            this.Price = contractMaterialPrice;
            this.Money = contractMaterialMoney;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractMaterialCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractMaterialCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
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

        public virtual ContractMaterial Copy()
        {
            ContractMaterial material = new ContractMaterial();
            material.ContractMaterialCode = this.ContractMaterialCode;
            material.OriginalContractMaterialCode = this.OriginalContractMaterialCode;
            material.ContractCode = this.ContractCode;
            material.MaterialCode = this.MaterialCode;
            material.Qty = this.Qty;
            material.Price = this.Price;
            material.Money = this.Money;
            material.AcceptChanges();
            return material;
        }

        public static ContractMaterial CreateContractMaterial(string contractMaterialContractMaterialCode, string contractMaterialContractCode, int? contractMaterialMaterialCode, decimal? contractMaterialQty, decimal? contractMaterialPrice, decimal? contractMaterialMoney)
        {
            ContractMaterial material = new ContractMaterial();
            material.ContractMaterialCode = contractMaterialContractMaterialCode;
            material.ContractCode = contractMaterialContractCode;
            material.MaterialCode = contractMaterialMaterialCode;
            material.Qty = contractMaterialQty;
            material.Price = contractMaterialPrice;
            material.Money = contractMaterialMoney;
            return material;
        }

        public virtual ContractMaterial DeepCopy()
        {
            return EntityHelper.Clone<ContractMaterial>(this as ContractMaterial);
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

        public virtual bool Equals(ContractMaterialBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractMaterialBase Object1, ContractMaterialBase Object2)
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
            if (Object1.ContractMaterialCode != Object2.ContractMaterialCode)
            {
                flag = false;
            }
            if ((Object1.ContractCode != null) && (Object2.ContractCode != null))
            {
                if (Object1.ContractCode != Object2.ContractCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractCode == null) ^ (Object2.ContractCode == null))
            {
                flag = false;
            }
            if (Object1.MaterialCode.HasValue && Object2.MaterialCode.HasValue)
            {
                if (Object1.MaterialCode != Object2.MaterialCode)
                {
                    flag = false;
                }
            }
            else if (!Object1.MaterialCode.HasValue ^ !Object2.MaterialCode.HasValue)
            {
                flag = false;
            }
            if (Object1.Qty.HasValue && Object2.Qty.HasValue)
            {
                if (Object1.Qty != Object2.Qty)
                {
                    flag = false;
                }
            }
            else if (!Object1.Qty.HasValue ^ !Object2.Qty.HasValue)
            {
                flag = false;
            }
            if (Object1.Price.HasValue && Object2.Price.HasValue)
            {
                if (Object1.Price != Object2.Price)
                {
                    flag = false;
                }
            }
            else if (!Object1.Price.HasValue ^ !Object2.Price.HasValue)
            {
                flag = false;
            }
            if (Object1.Money.HasValue && Object2.Money.HasValue)
            {
                if (Object1.Money != Object2.Money)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.Money.HasValue ^ !Object2.Money.HasValue)
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

        public void OnColumnChanged(ContractMaterialColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractMaterialColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractMaterialEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractMaterialEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractMaterialColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractMaterialColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractMaterialEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractMaterialEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractMaterial);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractMaterialEntityData;
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
                    this.parentCollection.Remove((ContractMaterial) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{7}{6}- ContractMaterialCode: {0}{6}- ContractCode: {1}{6}- MaterialCode: {2}{6}- Qty: {3}{6}- Price: {4}{6}- Money: {5}{6}", new object[] { this.ContractMaterialCode, (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), !this.MaterialCode.HasValue ? string.Empty : this.MaterialCode.ToString(), !this.Qty.HasValue ? string.Empty : this.Qty.ToString(), !this.Price.HasValue ? string.Empty : this.Price.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), Environment.NewLine, base.GetType() });
        }

        [DataObjectField(false, false, true, 50), Description("合同code"), TiannuoPM.Entities.Bindable]
        public virtual string ContractCode
        {
            get
            {
                return this.entityData.ContractCode;
            }
            set
            {
                if (this.entityData.ContractCode != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Browsable(false), XmlIgnore]
        public virtual Contract ContractCodeSource
        {
            get
            {
                return this._contractCodeSource;
            }
            set
            {
                this._contractCodeSource = value;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("合同材料code"), DataObjectField(true, false, false, 50)]
        public virtual string ContractMaterialCode
        {
            get
            {
                return this.entityData.ContractMaterialCode;
            }
            set
            {
                if (this.entityData.ContractMaterialCode != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.ContractMaterialCode, this.entityData.ContractMaterialCode);
                    this.entityData.ContractMaterialCode = value;
                    this.EntityId.ContractMaterialCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.ContractMaterialCode, this.entityData.ContractMaterialCode);
                    this.OnPropertyChanged("ContractMaterialCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractMaterialPlan> ContractMaterialPlanCollection
        {
            get
            {
                return this.entityData.ContractMaterialPlanCollection;
            }
            set
            {
                this.entityData.ContractMaterialPlanCollection = value;
            }
        }

        [XmlIgnore]
        public ContractMaterialKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractMaterialKey(this);
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
                    this.entityTrackingKey = "ContractMaterial" + this.ContractMaterialCode.ToString();
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

        [DataObjectField(false, false, true), Description("材料编号"), TiannuoPM.Entities.Bindable]
        public virtual int? MaterialCode
        {
            get
            {
                return this.entityData.MaterialCode;
            }
            set
            {
                if (this.entityData.MaterialCode != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.MaterialCode, this.entityData.MaterialCode);
                    this.entityData.MaterialCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.MaterialCode, this.entityData.MaterialCode);
                    this.OnPropertyChanged("MaterialCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
        public virtual Material MaterialCodeSource
        {
            get
            {
                return this._materialCodeSource;
            }
            set
            {
                this._materialCodeSource = value;
            }
        }

        [Description("金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? Money
        {
            get
            {
                return this.entityData.Money;
            }
            set
            {
                if (this.entityData.Money != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractMaterialCode
        {
            get
            {
                return this.entityData.OriginalContractMaterialCode;
            }
            set
            {
                this.entityData.OriginalContractMaterialCode = value;
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
                this.parentCollection = value as TList<ContractMaterial>;
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("单价")]
        public virtual decimal? Price
        {
            get
            {
                return this.entityData.Price;
            }
            set
            {
                if (this.entityData.Price != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.Price, this.entityData.Price);
                    this.entityData.Price = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.Price, this.entityData.Price);
                    this.OnPropertyChanged("Price");
                }
            }
        }

        [DataObjectField(false, false, true), Description("数量"), TiannuoPM.Entities.Bindable]
        public virtual decimal? Qty
        {
            get
            {
                return this.entityData.Qty;
            }
            set
            {
                if (this.entityData.Qty != value)
                {
                    this.OnColumnChanging(ContractMaterialColumn.Qty, this.entityData.Qty);
                    this.entityData.Qty = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialColumn.Qty, this.entityData.Qty);
                    this.OnPropertyChanged("Qty");
                }
            }
        }

        [XmlIgnore, Browsable(false), SoapIgnore]
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

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "ContractMaterialCode", "ContractCode", "MaterialCode", "Qty", "Price", "Money" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractMaterial";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractMaterialEntityData : ICloneable
        {
            public string ContractCode = null;
            public string ContractMaterialCode;
            private TList<ContractMaterialPlan> contractMaterialPlanContractMaterialCode;
            public int? MaterialCode = null;
            public decimal? Money = null;
            public string OriginalContractMaterialCode;
            public decimal? Price = null;
            public decimal? Qty = null;

            public object Clone()
            {
                ContractMaterialBase.ContractMaterialEntityData data = new ContractMaterialBase.ContractMaterialEntityData();
                data.ContractMaterialCode = this.ContractMaterialCode;
                data.OriginalContractMaterialCode = this.OriginalContractMaterialCode;
                data.ContractCode = this.ContractCode;
                data.MaterialCode = this.MaterialCode;
                data.Qty = this.Qty;
                data.Price = this.Price;
                data.Money = this.Money;
                return data;
            }

            public TList<ContractMaterialPlan> ContractMaterialPlanCollection
            {
                get
                {
                    if (this.contractMaterialPlanContractMaterialCode == null)
                    {
                        this.contractMaterialPlanContractMaterialCode = new TList<ContractMaterialPlan>();
                    }
                    return this.contractMaterialPlanContractMaterialCode;
                }
                set
                {
                    this.contractMaterialPlanContractMaterialCode = value;
                }
            }
        }
    }
}

