namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ContractCostBase : EntityBase, IEntityId<ContractCostKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractCostKey _entityId;
        private ISite _site;
        private ContractCostEntityData backupData;
        private ContractCostEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractCost> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractCostEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractCostEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractCostBase()
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostEntityData();
            this.backupData = null;
        }

        public ContractCostBase(string contractCostContractCostCode, string contractCostContractCode, string contractCostCostCode, decimal? contractCostAmount, decimal? contractCostMoney, decimal? contractCostUnitPrise, decimal? contractCostMoneycash, decimal? contractCostOriginalMoneycash, string contractCostMoneyType, decimal? contractCostExchangeRate, string contractCostCostBudgetSetCode, string contractCostDescription, decimal? contractCostOriginalMoney)
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostEntityData();
            this.backupData = null;
            this.ContractCostCode = contractCostContractCostCode;
            this.ContractCode = contractCostContractCode;
            this.CostCode = contractCostCostCode;
            this.Amount = contractCostAmount;
            this.Money = contractCostMoney;
            this.UnitPrise = contractCostUnitPrise;
            this.Moneycash = contractCostMoneycash;
            this.OriginalMoneycash = contractCostOriginalMoneycash;
            this.MoneyType = contractCostMoneyType;
            this.ExchangeRate = contractCostExchangeRate;
            this.CostBudgetSetCode = contractCostCostBudgetSetCode;
            this.Description = contractCostDescription;
            this.OriginalMoney = contractCostOriginalMoney;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractCostCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CostBudgetSetCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Description", 800));
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

        public virtual ContractCost Copy()
        {
            ContractCost cost = new ContractCost();
            cost.ContractCostCode = this.ContractCostCode;
            cost.OriginalContractCostCode = this.OriginalContractCostCode;
            cost.ContractCode = this.ContractCode;
            cost.CostCode = this.CostCode;
            cost.Amount = this.Amount;
            cost.Money = this.Money;
            cost.UnitPrise = this.UnitPrise;
            cost.Moneycash = this.Moneycash;
            cost.OriginalMoneycash = this.OriginalMoneycash;
            cost.MoneyType = this.MoneyType;
            cost.ExchangeRate = this.ExchangeRate;
            cost.CostBudgetSetCode = this.CostBudgetSetCode;
            cost.Description = this.Description;
            cost.OriginalMoney = this.OriginalMoney;
            cost.AcceptChanges();
            return cost;
        }

        public static ContractCost CreateContractCost(string contractCostContractCostCode, string contractCostContractCode, string contractCostCostCode, decimal? contractCostAmount, decimal? contractCostMoney, decimal? contractCostUnitPrise, decimal? contractCostMoneycash, decimal? contractCostOriginalMoneycash, string contractCostMoneyType, decimal? contractCostExchangeRate, string contractCostCostBudgetSetCode, string contractCostDescription, decimal? contractCostOriginalMoney)
        {
            ContractCost cost = new ContractCost();
            cost.ContractCostCode = contractCostContractCostCode;
            cost.ContractCode = contractCostContractCode;
            cost.CostCode = contractCostCostCode;
            cost.Amount = contractCostAmount;
            cost.Money = contractCostMoney;
            cost.UnitPrise = contractCostUnitPrise;
            cost.Moneycash = contractCostMoneycash;
            cost.OriginalMoneycash = contractCostOriginalMoneycash;
            cost.MoneyType = contractCostMoneyType;
            cost.ExchangeRate = contractCostExchangeRate;
            cost.CostBudgetSetCode = contractCostCostBudgetSetCode;
            cost.Description = contractCostDescription;
            cost.OriginalMoney = contractCostOriginalMoney;
            return cost;
        }

        public virtual ContractCost DeepCopy()
        {
            return EntityHelper.Clone<ContractCost>(this as ContractCost);
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

        public virtual bool Equals(ContractCostBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractCostBase Object1, ContractCostBase Object2)
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
            if (Object1.ContractCostCode != Object2.ContractCostCode)
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
            if ((Object1.CostCode != null) && (Object2.CostCode != null))
            {
                if (Object1.CostCode != Object2.CostCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.CostCode == null) ^ (Object2.CostCode == null))
            {
                flag = false;
            }
            if (Object1.Amount.HasValue && Object2.Amount.HasValue)
            {
                if (Object1.Amount != Object2.Amount)
                {
                    flag = false;
                }
            }
            else if (!Object1.Amount.HasValue ^ !Object2.Amount.HasValue)
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
            if (Object1.UnitPrise.HasValue && Object2.UnitPrise.HasValue)
            {
                if (Object1.UnitPrise != Object2.UnitPrise)
                {
                    flag = false;
                }
            }
            else if (!Object1.UnitPrise.HasValue ^ !Object2.UnitPrise.HasValue)
            {
                flag = false;
            }
            if (Object1.Moneycash.HasValue && Object2.Moneycash.HasValue)
            {
                if (Object1.Moneycash != Object2.Moneycash)
                {
                    flag = false;
                }
            }
            else if (!Object1.Moneycash.HasValue ^ !Object2.Moneycash.HasValue)
            {
                flag = false;
            }
            if (Object1.OriginalMoneycash.HasValue && Object2.OriginalMoneycash.HasValue)
            {
                if (Object1.OriginalMoneycash != Object2.OriginalMoneycash)
                {
                    flag = false;
                }
            }
            else if (!Object1.OriginalMoneycash.HasValue ^ !Object2.OriginalMoneycash.HasValue)
            {
                flag = false;
            }
            if ((Object1.MoneyType != null) && (Object2.MoneyType != null))
            {
                if (Object1.MoneyType != Object2.MoneyType)
                {
                    flag = false;
                }
            }
            else if ((Object1.MoneyType == null) ^ (Object2.MoneyType == null))
            {
                flag = false;
            }
            if (Object1.ExchangeRate.HasValue && Object2.ExchangeRate.HasValue)
            {
                if (Object1.ExchangeRate != Object2.ExchangeRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.ExchangeRate.HasValue ^ !Object2.ExchangeRate.HasValue)
            {
                flag = false;
            }
            if ((Object1.CostBudgetSetCode != null) && (Object2.CostBudgetSetCode != null))
            {
                if (Object1.CostBudgetSetCode != Object2.CostBudgetSetCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.CostBudgetSetCode == null) ^ (Object2.CostBudgetSetCode == null))
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
            if (Object1.OriginalMoney.HasValue && Object2.OriginalMoney.HasValue)
            {
                if (Object1.OriginalMoney != Object2.OriginalMoney)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.OriginalMoney.HasValue ^ !Object2.OriginalMoney.HasValue)
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

        public void OnColumnChanged(ContractCostColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractCostColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractCostEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractCostEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractCostColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractCostColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractCostEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractCostEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractCost);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractCostEntityData;
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
                    this.parentCollection.Remove((ContractCost) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{14}{13}- ContractCostCode: {0}{13}- ContractCode: {1}{13}- CostCode: {2}{13}- Amount: {3}{13}- Money: {4}{13}- UnitPrise: {5}{13}- Moneycash: {6}{13}- OriginalMoneycash: {7}{13}- MoneyType: {8}{13}- ExchangeRate: {9}{13}- CostBudgetSetCode: {10}{13}- Description: {11}{13}- OriginalMoney: {12}{13}", new object[] { this.ContractCostCode, (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.CostCode == null) ? string.Empty : this.CostCode.ToString(), !this.Amount.HasValue ? string.Empty : this.Amount.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), !this.UnitPrise.HasValue ? string.Empty : this.UnitPrise.ToString(), !this.Moneycash.HasValue ? string.Empty : this.Moneycash.ToString(), !this.OriginalMoneycash.HasValue ? string.Empty : this.OriginalMoneycash.ToString(), (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), (this.CostBudgetSetCode == null) ? string.Empty : this.CostBudgetSetCode.ToString(), (this.Description == null) ? string.Empty : this.Description.ToString(), !this.OriginalMoney.HasValue ? string.Empty : this.OriginalMoney.ToString(), Environment.NewLine, base.GetType() });
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("数量")]
        public virtual decimal? Amount
        {
            get
            {
                return this.entityData.Amount;
            }
            set
            {
                if (this.entityData.Amount != value)
                {
                    this.OnColumnChanging(ContractCostColumn.Amount, this.entityData.Amount);
                    this.entityData.Amount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.Amount, this.entityData.Amount);
                    this.OnPropertyChanged("Amount");
                }
            }
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
                    this.OnColumnChanging(ContractCostColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.ContractCode, this.entityData.ContractCode);
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

        [TiannuoPM.Entities.Bindable, Description("款项code(主键)"), DataObjectField(true, false, false, 50)]
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
                    this.OnColumnChanging(ContractCostColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.entityData.ContractCostCode = value;
                    this.EntityId.ContractCostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.OnPropertyChanged("ContractCostCode");
                }
            }
        }

        [Description("预算设置表编号 CostBudgetSet.CostBudgetSetCode"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string CostBudgetSetCode
        {
            get
            {
                return this.entityData.CostBudgetSetCode;
            }
            set
            {
                if (this.entityData.CostBudgetSetCode != value)
                {
                    this.OnColumnChanging(ContractCostColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.entityData.CostBudgetSetCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.OnPropertyChanged("CostBudgetSetCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("费用项"), TiannuoPM.Entities.Bindable]
        public virtual string CostCode
        {
            get
            {
                return this.entityData.CostCode;
            }
            set
            {
                if (this.entityData.CostCode != value)
                {
                    this.OnColumnChanging(ContractCostColumn.CostCode, this.entityData.CostCode);
                    this.entityData.CostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.CostCode, this.entityData.CostCode);
                    this.OnPropertyChanged("CostCode");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description("说明"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostColumn.Description, this.entityData.Description);
                    this.entityData.Description = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.Description, this.entityData.Description);
                    this.OnPropertyChanged("Description");
                }
            }
        }

        [XmlIgnore]
        public ContractCostKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractCostKey(this);
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
                    this.entityTrackingKey = "ContractCost" + this.ContractCostCode.ToString();
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

        [Description("汇率"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? ExchangeRate
        {
            get
            {
                return this.entityData.ExchangeRate;
            }
            set
            {
                if (this.entityData.ExchangeRate != value)
                {
                    this.OnColumnChanging(ContractCostColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("金额"), DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(ContractCostColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Description("原币金额 "), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? Moneycash
        {
            get
            {
                return this.entityData.Moneycash;
            }
            set
            {
                if (this.entityData.Moneycash != value)
                {
                    this.OnColumnChanging(ContractCostColumn.Moneycash, this.entityData.Moneycash);
                    this.entityData.Moneycash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.Moneycash, this.entityData.Moneycash);
                    this.OnPropertyChanged("Moneycash");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("币种"), DataObjectField(false, false, true, 50)]
        public virtual string MoneyType
        {
            get
            {
                return this.entityData.MoneyType;
            }
            set
            {
                if (this.entityData.MoneyType != value)
                {
                    this.OnColumnChanging(ContractCostColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractCostCode
        {
            get
            {
                return this.entityData.OriginalContractCostCode;
            }
            set
            {
                this.entityData.OriginalContractCostCode = value;
            }
        }

        [Description("原始金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? OriginalMoney
        {
            get
            {
                return this.entityData.OriginalMoney;
            }
            set
            {
                if (this.entityData.OriginalMoney != value)
                {
                    this.OnColumnChanging(ContractCostColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.entityData.OriginalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.OnPropertyChanged("OriginalMoney");
                }
            }
        }

        [Description("原币原始金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? OriginalMoneycash
        {
            get
            {
                return this.entityData.OriginalMoneycash;
            }
            set
            {
                if (this.entityData.OriginalMoneycash != value)
                {
                    this.OnColumnChanging(ContractCostColumn.OriginalMoneycash, this.entityData.OriginalMoneycash);
                    this.entityData.OriginalMoneycash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.OriginalMoneycash, this.entityData.OriginalMoneycash);
                    this.OnPropertyChanged("OriginalMoneycash");
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
                this.parentCollection = value as TList<ContractCost>;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<PaymentItem> PaymentItemCollection
        {
            get
            {
                return this.entityData.PaymentItemCollection;
            }
            set
            {
                this.entityData.PaymentItemCollection = value;
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
                return new string[] { "ContractCostCode", "ContractCode", "CostCode", "Amount", "Money", "UnitPrise", "Moneycash", "OriginalMoneycash", "MoneyType", "ExchangeRate", "CostBudgetSetCode", "Description", "OriginalMoney" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractCost";
            }
        }

        [Description("单价"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? UnitPrise
        {
            get
            {
                return this.entityData.UnitPrise;
            }
            set
            {
                if (this.entityData.UnitPrise != value)
                {
                    this.OnColumnChanging(ContractCostColumn.UnitPrise, this.entityData.UnitPrise);
                    this.entityData.UnitPrise = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostColumn.UnitPrise, this.entityData.UnitPrise);
                    this.OnPropertyChanged("UnitPrise");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractCostEntityData : ICloneable
        {
            public decimal? Amount = null;
            public string ContractCode = null;
            public string ContractCostCode;
            public string CostBudgetSetCode = null;
            public string CostCode = null;
            public string Description = null;
            public decimal? ExchangeRate = null;
            public decimal? Money = null;
            public decimal? Moneycash = null;
            public string MoneyType = null;
            public string OriginalContractCostCode;
            public decimal? OriginalMoney = null;
            public decimal? OriginalMoneycash = null;
            private TList<PaymentItem> paymentItemContractCostCode;
            public decimal? UnitPrise = null;

            public object Clone()
            {
                ContractCostBase.ContractCostEntityData data = new ContractCostBase.ContractCostEntityData();
                data.ContractCostCode = this.ContractCostCode;
                data.OriginalContractCostCode = this.OriginalContractCostCode;
                data.ContractCode = this.ContractCode;
                data.CostCode = this.CostCode;
                data.Amount = this.Amount;
                data.Money = this.Money;
                data.UnitPrise = this.UnitPrise;
                data.Moneycash = this.Moneycash;
                data.OriginalMoneycash = this.OriginalMoneycash;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                data.CostBudgetSetCode = this.CostBudgetSetCode;
                data.Description = this.Description;
                data.OriginalMoney = this.OriginalMoney;
                return data;
            }

            public TList<PaymentItem> PaymentItemCollection
            {
                get
                {
                    if (this.paymentItemContractCostCode == null)
                    {
                        this.paymentItemContractCostCode = new TList<PaymentItem>();
                    }
                    return this.paymentItemContractCostCode;
                }
                set
                {
                    this.paymentItemContractCostCode = value;
                }
            }
        }
    }
}

