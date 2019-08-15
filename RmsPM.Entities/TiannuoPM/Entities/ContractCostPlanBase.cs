namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class ContractCostPlanBase : EntityBase, IEntityId<ContractCostPlanKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractCostPlanKey _entityId;
        private ISite _site;
        private ContractCostPlanEntityData backupData;
        private ContractCostPlanEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractCostPlan> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractCostPlanEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractCostPlanEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractCostPlanBase()
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostPlanEntityData();
            this.backupData = null;
        }

        public ContractCostPlanBase(string contractCostPlanContractCostPlanCode, string contractCostPlanContractCostCode, string contractCostPlanContractCode, decimal? contractCostPlanMoney, DateTime? contractCostPlanPlanningPayDate, string contractCostPlanPayConditionText)
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostPlanEntityData();
            this.backupData = null;
            this.ContractCostPlanCode = contractCostPlanContractCostPlanCode;
            this.ContractCostCode = contractCostPlanContractCostCode;
            this.ContractCode = contractCostPlanContractCode;
            this.Money = contractCostPlanMoney;
            this.PlanningPayDate = contractCostPlanPlanningPayDate;
            this.PayConditionText = contractCostPlanPayConditionText;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractCostPlanCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostPlanCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayConditionText", 200));
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

        public virtual ContractCostPlan Copy()
        {
            ContractCostPlan plan = new ContractCostPlan();
            plan.ContractCostPlanCode = this.ContractCostPlanCode;
            plan.OriginalContractCostPlanCode = this.OriginalContractCostPlanCode;
            plan.ContractCostCode = this.ContractCostCode;
            plan.ContractCode = this.ContractCode;
            plan.Money = this.Money;
            plan.PlanningPayDate = this.PlanningPayDate;
            plan.PayConditionText = this.PayConditionText;
            plan.AcceptChanges();
            return plan;
        }

        public static ContractCostPlan CreateContractCostPlan(string contractCostPlanContractCostPlanCode, string contractCostPlanContractCostCode, string contractCostPlanContractCode, decimal? contractCostPlanMoney, DateTime? contractCostPlanPlanningPayDate, string contractCostPlanPayConditionText)
        {
            ContractCostPlan plan = new ContractCostPlan();
            plan.ContractCostPlanCode = contractCostPlanContractCostPlanCode;
            plan.ContractCostCode = contractCostPlanContractCostCode;
            plan.ContractCode = contractCostPlanContractCode;
            plan.Money = contractCostPlanMoney;
            plan.PlanningPayDate = contractCostPlanPlanningPayDate;
            plan.PayConditionText = contractCostPlanPayConditionText;
            return plan;
        }

        public virtual ContractCostPlan DeepCopy()
        {
            return EntityHelper.Clone<ContractCostPlan>(this as ContractCostPlan);
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

        public virtual bool Equals(ContractCostPlanBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractCostPlanBase Object1, ContractCostPlanBase Object2)
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
            if (Object1.ContractCostPlanCode != Object2.ContractCostPlanCode)
            {
                flag = false;
            }
            if ((Object1.ContractCostCode != null) && (Object2.ContractCostCode != null))
            {
                if (Object1.ContractCostCode != Object2.ContractCostCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractCostCode == null) ^ (Object2.ContractCostCode == null))
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
            if (Object1.Money.HasValue && Object2.Money.HasValue)
            {
                if (Object1.Money != Object2.Money)
                {
                    flag = false;
                }
            }
            else if (!Object1.Money.HasValue ^ !Object2.Money.HasValue)
            {
                flag = false;
            }
            if (Object1.PlanningPayDate.HasValue && Object2.PlanningPayDate.HasValue)
            {
                if (Object1.PlanningPayDate != Object2.PlanningPayDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlanningPayDate.HasValue ^ !Object2.PlanningPayDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.PayConditionText != null) && (Object2.PayConditionText != null))
            {
                if (Object1.PayConditionText != Object2.PayConditionText)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.PayConditionText == null) ^ (Object2.PayConditionText == null))
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

        public void OnColumnChanged(ContractCostPlanColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractCostPlanColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractCostPlanEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractCostPlanEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractCostPlanColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractCostPlanColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractCostPlanEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractCostPlanEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractCostPlan);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractCostPlanEntityData;
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
                    this.parentCollection.Remove((ContractCostPlan) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{7}{6}- ContractCostPlanCode: {0}{6}- ContractCostCode: {1}{6}- ContractCode: {2}{6}- Money: {3}{6}- PlanningPayDate: {4}{6}- PayConditionText: {5}{6}", new object[] { this.ContractCostPlanCode, (this.ContractCostCode == null) ? string.Empty : this.ContractCostCode.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), !this.PlanningPayDate.HasValue ? string.Empty : this.PlanningPayDate.ToString(), (this.PayConditionText == null) ? string.Empty : this.PayConditionText.ToString(), Environment.NewLine, base.GetType() });
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("合同code")]
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
                    this.OnColumnChanging(ContractCostPlanColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [XmlIgnore, TiannuoPM.Entities.Bindable, Browsable(false)]
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

        [TiannuoPM.Entities.Bindable, Description("合同金额code"), DataObjectField(false, false, true, 50)]
        public virtual string ContractCostCode
        {
            get
            {
                return this.entityData.ContractCostCode;
            }
            set
            {
                if (this.entityData.ContractCostCode != value)
                {
                    this.OnColumnChanging(ContractCostPlanColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.entityData.ContractCostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.OnPropertyChanged("ContractCostCode");
                }
            }
        }

        [DataObjectField(true, false, false, 50), Description("合同付款计划code"), TiannuoPM.Entities.Bindable]
        public virtual string ContractCostPlanCode
        {
            get
            {
                return this.entityData.ContractCostPlanCode;
            }
            set
            {
                if (this.entityData.ContractCostPlanCode != value)
                {
                    this.OnColumnChanging(ContractCostPlanColumn.ContractCostPlanCode, this.entityData.ContractCostPlanCode);
                    this.entityData.ContractCostPlanCode = value;
                    this.EntityId.ContractCostPlanCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.ContractCostPlanCode, this.entityData.ContractCostPlanCode);
                    this.OnPropertyChanged("ContractCostPlanCode");
                }
            }
        }

        [XmlIgnore]
        public ContractCostPlanKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractCostPlanKey(this);
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
                    this.entityTrackingKey = "ContractCostPlan" + this.ContractCostPlanCode.ToString();
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

        [Description("金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostPlanColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractCostPlanCode
        {
            get
            {
                return this.entityData.OriginalContractCostPlanCode;
            }
            set
            {
                this.entityData.OriginalContractCostPlanCode = value;
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
                this.parentCollection = value as TList<ContractCostPlan>;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("付款条件"), DataObjectField(false, false, true, 200)]
        public virtual string PayConditionText
        {
            get
            {
                return this.entityData.PayConditionText;
            }
            set
            {
                if (this.entityData.PayConditionText != value)
                {
                    this.OnColumnChanging(ContractCostPlanColumn.PayConditionText, this.entityData.PayConditionText);
                    this.entityData.PayConditionText = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.PayConditionText, this.entityData.PayConditionText);
                    this.OnPropertyChanged("PayConditionText");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("预计付款日期")]
        public virtual DateTime? PlanningPayDate
        {
            get
            {
                return this.entityData.PlanningPayDate;
            }
            set
            {
                if (this.entityData.PlanningPayDate != value)
                {
                    this.OnColumnChanging(ContractCostPlanColumn.PlanningPayDate, this.entityData.PlanningPayDate);
                    this.entityData.PlanningPayDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostPlanColumn.PlanningPayDate, this.entityData.PlanningPayDate);
                    this.OnPropertyChanged("PlanningPayDate");
                }
            }
        }

        [Browsable(false), SoapIgnore, XmlIgnore]
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
                return new string[] { "ContractCostPlanCode", "ContractCostCode", "ContractCode", "Money", "PlanningPayDate", "PayConditionText" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "ContractCostPlan";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractCostPlanEntityData : ICloneable
        {
            public string ContractCode = null;
            public string ContractCostCode = null;
            public string ContractCostPlanCode;
            public decimal? Money = null;
            public string OriginalContractCostPlanCode;
            public string PayConditionText = null;
            public DateTime? PlanningPayDate = null;

            public object Clone()
            {
                ContractCostPlanBase.ContractCostPlanEntityData data = new ContractCostPlanBase.ContractCostPlanEntityData();
                data.ContractCostPlanCode = this.ContractCostPlanCode;
                data.OriginalContractCostPlanCode = this.OriginalContractCostPlanCode;
                data.ContractCostCode = this.ContractCostCode;
                data.ContractCode = this.ContractCode;
                data.Money = this.Money;
                data.PlanningPayDate = this.PlanningPayDate;
                data.PayConditionText = this.PayConditionText;
                return data;
            }
        }
    }
}

