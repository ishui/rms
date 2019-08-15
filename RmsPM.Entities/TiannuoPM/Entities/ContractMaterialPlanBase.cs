namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class ContractMaterialPlanBase : EntityBase, IEntityId<ContractMaterialPlanKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ContractMaterial _contractMaterialCodeSource;
        private ContractMaterialPlanKey _entityId;
        private ISite _site;
        private ContractMaterialPlanEntityData backupData;
        private ContractMaterialPlanEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractMaterialPlan> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractMaterialPlanEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractMaterialPlanEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractMaterialPlanBase()
        {
            this.inTxn = false;
            this._contractMaterialCodeSource = null;
            this._site = null;
            this.entityData = new ContractMaterialPlanEntityData();
            this.backupData = null;
        }

        public ContractMaterialPlanBase(string contractMaterialPlanContractMaterialPlanCode, string contractMaterialPlanContractMaterialCode, string contractMaterialPlanContractCode, DateTime? contractMaterialPlanPlanDate, decimal? contractMaterialPlanPlanQty)
        {
            this.inTxn = false;
            this._contractMaterialCodeSource = null;
            this._site = null;
            this.entityData = new ContractMaterialPlanEntityData();
            this.backupData = null;
            this.ContractMaterialPlanCode = contractMaterialPlanContractMaterialPlanCode;
            this.ContractMaterialCode = contractMaterialPlanContractMaterialCode;
            this.ContractCode = contractMaterialPlanContractCode;
            this.PlanDate = contractMaterialPlanPlanDate;
            this.PlanQty = contractMaterialPlanPlanQty;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractMaterialPlanCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractMaterialPlanCode", 50));
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

        public virtual ContractMaterialPlan Copy()
        {
            ContractMaterialPlan plan = new ContractMaterialPlan();
            plan.ContractMaterialPlanCode = this.ContractMaterialPlanCode;
            plan.OriginalContractMaterialPlanCode = this.OriginalContractMaterialPlanCode;
            plan.ContractMaterialCode = this.ContractMaterialCode;
            plan.ContractCode = this.ContractCode;
            plan.PlanDate = this.PlanDate;
            plan.PlanQty = this.PlanQty;
            plan.AcceptChanges();
            return plan;
        }

        public static ContractMaterialPlan CreateContractMaterialPlan(string contractMaterialPlanContractMaterialPlanCode, string contractMaterialPlanContractMaterialCode, string contractMaterialPlanContractCode, DateTime? contractMaterialPlanPlanDate, decimal? contractMaterialPlanPlanQty)
        {
            ContractMaterialPlan plan = new ContractMaterialPlan();
            plan.ContractMaterialPlanCode = contractMaterialPlanContractMaterialPlanCode;
            plan.ContractMaterialCode = contractMaterialPlanContractMaterialCode;
            plan.ContractCode = contractMaterialPlanContractCode;
            plan.PlanDate = contractMaterialPlanPlanDate;
            plan.PlanQty = contractMaterialPlanPlanQty;
            return plan;
        }

        public virtual ContractMaterialPlan DeepCopy()
        {
            return EntityHelper.Clone<ContractMaterialPlan>(this as ContractMaterialPlan);
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

        public virtual bool Equals(ContractMaterialPlanBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractMaterialPlanBase Object1, ContractMaterialPlanBase Object2)
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
            if (Object1.ContractMaterialPlanCode != Object2.ContractMaterialPlanCode)
            {
                flag = false;
            }
            if ((Object1.ContractMaterialCode != null) && (Object2.ContractMaterialCode != null))
            {
                if (Object1.ContractMaterialCode != Object2.ContractMaterialCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractMaterialCode == null) ^ (Object2.ContractMaterialCode == null))
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
            if (Object1.PlanDate.HasValue && Object2.PlanDate.HasValue)
            {
                if (Object1.PlanDate != Object2.PlanDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlanDate.HasValue ^ !Object2.PlanDate.HasValue)
            {
                flag = false;
            }
            if (Object1.PlanQty.HasValue && Object2.PlanQty.HasValue)
            {
                if (Object1.PlanQty != Object2.PlanQty)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.PlanQty.HasValue ^ !Object2.PlanQty.HasValue)
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

        public void OnColumnChanged(ContractMaterialPlanColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractMaterialPlanColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractMaterialPlanEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractMaterialPlanEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractMaterialPlanColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractMaterialPlanColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractMaterialPlanEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractMaterialPlanEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractMaterialPlan);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractMaterialPlanEntityData;
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
                    this.parentCollection.Remove((ContractMaterialPlan) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{6}{5}- ContractMaterialPlanCode: {0}{5}- ContractMaterialCode: {1}{5}- ContractCode: {2}{5}- PlanDate: {3}{5}- PlanQty: {4}{5}", new object[] { this.ContractMaterialPlanCode, (this.ContractMaterialCode == null) ? string.Empty : this.ContractMaterialCode.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), !this.PlanDate.HasValue ? string.Empty : this.PlanDate.ToString(), !this.PlanQty.HasValue ? string.Empty : this.PlanQty.ToString(), Environment.NewLine, base.GetType() });
        }

        [Description("合同code"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractMaterialPlanColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialPlanColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("合同材料code")]
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
                    this.OnColumnChanging(ContractMaterialPlanColumn.ContractMaterialCode, this.entityData.ContractMaterialCode);
                    this.entityData.ContractMaterialCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialPlanColumn.ContractMaterialCode, this.entityData.ContractMaterialCode);
                    this.OnPropertyChanged("ContractMaterialCode");
                }
            }
        }

        [XmlIgnore, TiannuoPM.Entities.Bindable, Browsable(false)]
        public virtual ContractMaterial ContractMaterialCodeSource
        {
            get
            {
                return this._contractMaterialCodeSource;
            }
            set
            {
                this._contractMaterialCodeSource = value;
            }
        }

        [Description("材料需求月度计划code"), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
        public virtual string ContractMaterialPlanCode
        {
            get
            {
                return this.entityData.ContractMaterialPlanCode;
            }
            set
            {
                if (this.entityData.ContractMaterialPlanCode != value)
                {
                    this.OnColumnChanging(ContractMaterialPlanColumn.ContractMaterialPlanCode, this.entityData.ContractMaterialPlanCode);
                    this.entityData.ContractMaterialPlanCode = value;
                    this.EntityId.ContractMaterialPlanCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialPlanColumn.ContractMaterialPlanCode, this.entityData.ContractMaterialPlanCode);
                    this.OnPropertyChanged("ContractMaterialPlanCode");
                }
            }
        }

        [XmlIgnore]
        public ContractMaterialPlanKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractMaterialPlanKey(this);
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
                    this.entityTrackingKey = "ContractMaterialPlan" + this.ContractMaterialPlanCode.ToString();
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

        [Browsable(false)]
        public virtual string OriginalContractMaterialPlanCode
        {
            get
            {
                return this.entityData.OriginalContractMaterialPlanCode;
            }
            set
            {
                this.entityData.OriginalContractMaterialPlanCode = value;
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
                this.parentCollection = value as TList<ContractMaterialPlan>;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("计划日期")]
        public virtual DateTime? PlanDate
        {
            get
            {
                return this.entityData.PlanDate;
            }
            set
            {
                if (this.entityData.PlanDate != value)
                {
                    this.OnColumnChanging(ContractMaterialPlanColumn.PlanDate, this.entityData.PlanDate);
                    this.entityData.PlanDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialPlanColumn.PlanDate, this.entityData.PlanDate);
                    this.OnPropertyChanged("PlanDate");
                }
            }
        }

        [Description("计划数量"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? PlanQty
        {
            get
            {
                return this.entityData.PlanQty;
            }
            set
            {
                if (this.entityData.PlanQty != value)
                {
                    this.OnColumnChanging(ContractMaterialPlanColumn.PlanQty, this.entityData.PlanQty);
                    this.entityData.PlanQty = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractMaterialPlanColumn.PlanQty, this.entityData.PlanQty);
                    this.OnPropertyChanged("PlanQty");
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

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "ContractMaterialPlanCode", "ContractMaterialCode", "ContractCode", "PlanDate", "PlanQty" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "ContractMaterialPlan";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractMaterialPlanEntityData : ICloneable
        {
            public string ContractCode = null;
            public string ContractMaterialCode = null;
            public string ContractMaterialPlanCode;
            public string OriginalContractMaterialPlanCode;
            public DateTime? PlanDate = null;
            public decimal? PlanQty = null;

            public object Clone()
            {
                ContractMaterialPlanBase.ContractMaterialPlanEntityData data = new ContractMaterialPlanBase.ContractMaterialPlanEntityData();
                data.ContractMaterialPlanCode = this.ContractMaterialPlanCode;
                data.OriginalContractMaterialPlanCode = this.OriginalContractMaterialPlanCode;
                data.ContractMaterialCode = this.ContractMaterialCode;
                data.ContractCode = this.ContractCode;
                data.PlanDate = this.PlanDate;
                data.PlanQty = this.PlanQty;
                return data;
            }
        }
    }
}

