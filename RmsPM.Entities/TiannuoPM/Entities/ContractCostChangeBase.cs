namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ContractCostChangeBase : EntityBase, IEntityId<ContractCostChangeKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ContractChange _contractChangeCodeSource;
        private ContractCostChangeKey _entityId;
        private ISite _site;
        private ContractCostChangeEntityData backupData;
        private ContractCostChangeEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractCostChange> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractCostChangeEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractCostChangeEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractCostChangeBase()
        {
            this.inTxn = false;
            this._contractChangeCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostChangeEntityData();
            this.backupData = null;
        }

        public ContractCostChangeBase(string contractCostChangeContractCostChangeCode, string contractCostChangeContractCode, string contractCostChangeContractCostCode, string contractCostChangeContractChangeCode, decimal? contractCostChangeCash, decimal? contractCostChangeChangeCash, decimal? contractCostChangeNewCash, decimal? contractCostChangeOriginalCash, decimal? contractCostChangeTotalChangeCash, string contractCostChangeMoneyType, decimal? contractCostChangeExchangeRate, decimal? contractCostChangeMoney, decimal? contractCostChangeChangeMoney, decimal? contractCostChangeNewMoney, decimal? contractCostChangeOriginalMoney, decimal? contractCostChangeTotalChangeMoney, string contractCostChangeCostCode, string contractCostChangeCostBudgetSetCode, string contractCostChangeDescription, int? contractCostChangeStatus)
        {
            this.inTxn = false;
            this._contractChangeCodeSource = null;
            this._site = null;
            this.entityData = new ContractCostChangeEntityData();
            this.backupData = null;
            this.ContractCostChangeCode = contractCostChangeContractCostChangeCode;
            this.ContractCode = contractCostChangeContractCode;
            this.ContractCostCode = contractCostChangeContractCostCode;
            this.ContractChangeCode = contractCostChangeContractChangeCode;
            this.Cash = contractCostChangeCash;
            this.ChangeCash = contractCostChangeChangeCash;
            this.NewCash = contractCostChangeNewCash;
            this.OriginalCash = contractCostChangeOriginalCash;
            this.TotalChangeCash = contractCostChangeTotalChangeCash;
            this.MoneyType = contractCostChangeMoneyType;
            this.ExchangeRate = contractCostChangeExchangeRate;
            this.Money = contractCostChangeMoney;
            this.ChangeMoney = contractCostChangeChangeMoney;
            this.NewMoney = contractCostChangeNewMoney;
            this.OriginalMoney = contractCostChangeOriginalMoney;
            this.TotalChangeMoney = contractCostChangeTotalChangeMoney;
            this.CostCode = contractCostChangeCostCode;
            this.CostBudgetSetCode = contractCostChangeCostBudgetSetCode;
            this.Description = contractCostChangeDescription;
            this.Status = contractCostChangeStatus;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractCostChangeCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostChangeCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractChangeCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CostCode", 50));
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

        public virtual ContractCostChange Copy()
        {
            ContractCostChange change = new ContractCostChange();
            change.ContractCostChangeCode = this.ContractCostChangeCode;
            change.OriginalContractCostChangeCode = this.OriginalContractCostChangeCode;
            change.ContractCode = this.ContractCode;
            change.ContractCostCode = this.ContractCostCode;
            change.ContractChangeCode = this.ContractChangeCode;
            change.Cash = this.Cash;
            change.ChangeCash = this.ChangeCash;
            change.NewCash = this.NewCash;
            change.OriginalCash = this.OriginalCash;
            change.TotalChangeCash = this.TotalChangeCash;
            change.MoneyType = this.MoneyType;
            change.ExchangeRate = this.ExchangeRate;
            change.Money = this.Money;
            change.ChangeMoney = this.ChangeMoney;
            change.NewMoney = this.NewMoney;
            change.OriginalMoney = this.OriginalMoney;
            change.TotalChangeMoney = this.TotalChangeMoney;
            change.CostCode = this.CostCode;
            change.CostBudgetSetCode = this.CostBudgetSetCode;
            change.Description = this.Description;
            change.Status = this.Status;
            change.AcceptChanges();
            return change;
        }

        public static ContractCostChange CreateContractCostChange(string contractCostChangeContractCostChangeCode, string contractCostChangeContractCode, string contractCostChangeContractCostCode, string contractCostChangeContractChangeCode, decimal? contractCostChangeCash, decimal? contractCostChangeChangeCash, decimal? contractCostChangeNewCash, decimal? contractCostChangeOriginalCash, decimal? contractCostChangeTotalChangeCash, string contractCostChangeMoneyType, decimal? contractCostChangeExchangeRate, decimal? contractCostChangeMoney, decimal? contractCostChangeChangeMoney, decimal? contractCostChangeNewMoney, decimal? contractCostChangeOriginalMoney, decimal? contractCostChangeTotalChangeMoney, string contractCostChangeCostCode, string contractCostChangeCostBudgetSetCode, string contractCostChangeDescription, int? contractCostChangeStatus)
        {
            ContractCostChange change = new ContractCostChange();
            change.ContractCostChangeCode = contractCostChangeContractCostChangeCode;
            change.ContractCode = contractCostChangeContractCode;
            change.ContractCostCode = contractCostChangeContractCostCode;
            change.ContractChangeCode = contractCostChangeContractChangeCode;
            change.Cash = contractCostChangeCash;
            change.ChangeCash = contractCostChangeChangeCash;
            change.NewCash = contractCostChangeNewCash;
            change.OriginalCash = contractCostChangeOriginalCash;
            change.TotalChangeCash = contractCostChangeTotalChangeCash;
            change.MoneyType = contractCostChangeMoneyType;
            change.ExchangeRate = contractCostChangeExchangeRate;
            change.Money = contractCostChangeMoney;
            change.ChangeMoney = contractCostChangeChangeMoney;
            change.NewMoney = contractCostChangeNewMoney;
            change.OriginalMoney = contractCostChangeOriginalMoney;
            change.TotalChangeMoney = contractCostChangeTotalChangeMoney;
            change.CostCode = contractCostChangeCostCode;
            change.CostBudgetSetCode = contractCostChangeCostBudgetSetCode;
            change.Description = contractCostChangeDescription;
            change.Status = contractCostChangeStatus;
            return change;
        }

        public virtual ContractCostChange DeepCopy()
        {
            return EntityHelper.Clone<ContractCostChange>(this as ContractCostChange);
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

        public virtual bool Equals(ContractCostChangeBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractCostChangeBase Object1, ContractCostChangeBase Object2)
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
            if (Object1.ContractCostChangeCode != Object2.ContractCostChangeCode)
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
            if ((Object1.ContractChangeCode != null) && (Object2.ContractChangeCode != null))
            {
                if (Object1.ContractChangeCode != Object2.ContractChangeCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractChangeCode == null) ^ (Object2.ContractChangeCode == null))
            {
                flag = false;
            }
            if (Object1.Cash.HasValue && Object2.Cash.HasValue)
            {
                if (Object1.Cash != Object2.Cash)
                {
                    flag = false;
                }
            }
            else if (!Object1.Cash.HasValue ^ !Object2.Cash.HasValue)
            {
                flag = false;
            }
            if (Object1.ChangeCash.HasValue && Object2.ChangeCash.HasValue)
            {
                if (Object1.ChangeCash != Object2.ChangeCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.ChangeCash.HasValue ^ !Object2.ChangeCash.HasValue)
            {
                flag = false;
            }
            if (Object1.NewCash.HasValue && Object2.NewCash.HasValue)
            {
                if (Object1.NewCash != Object2.NewCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.NewCash.HasValue ^ !Object2.NewCash.HasValue)
            {
                flag = false;
            }
            if (Object1.OriginalCash.HasValue && Object2.OriginalCash.HasValue)
            {
                if (Object1.OriginalCash != Object2.OriginalCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.OriginalCash.HasValue ^ !Object2.OriginalCash.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalChangeCash.HasValue && Object2.TotalChangeCash.HasValue)
            {
                if (Object1.TotalChangeCash != Object2.TotalChangeCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalChangeCash.HasValue ^ !Object2.TotalChangeCash.HasValue)
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
            if (Object1.ChangeMoney.HasValue && Object2.ChangeMoney.HasValue)
            {
                if (Object1.ChangeMoney != Object2.ChangeMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.ChangeMoney.HasValue ^ !Object2.ChangeMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.NewMoney.HasValue && Object2.NewMoney.HasValue)
            {
                if (Object1.NewMoney != Object2.NewMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.NewMoney.HasValue ^ !Object2.NewMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.OriginalMoney.HasValue && Object2.OriginalMoney.HasValue)
            {
                if (Object1.OriginalMoney != Object2.OriginalMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.OriginalMoney.HasValue ^ !Object2.OriginalMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalChangeMoney.HasValue && Object2.TotalChangeMoney.HasValue)
            {
                if (Object1.TotalChangeMoney != Object2.TotalChangeMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalChangeMoney.HasValue ^ !Object2.TotalChangeMoney.HasValue)
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

        public void OnColumnChanged(ContractCostChangeColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractCostChangeColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractCostChangeEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractCostChangeEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractCostChangeColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractCostChangeColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractCostChangeEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractCostChangeEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractCostChange);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractCostChangeEntityData;
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
                    this.parentCollection.Remove((ContractCostChange) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{21}{20}- ContractCostChangeCode: {0}{20}- ContractCode: {1}{20}- ContractCostCode: {2}{20}- ContractChangeCode: {3}{20}- Cash: {4}{20}- ChangeCash: {5}{20}- NewCash: {6}{20}- OriginalCash: {7}{20}- TotalChangeCash: {8}{20}- MoneyType: {9}{20}- ExchangeRate: {10}{20}- Money: {11}{20}- ChangeMoney: {12}{20}- NewMoney: {13}{20}- OriginalMoney: {14}{20}- TotalChangeMoney: {15}{20}- CostCode: {16}{20}- CostBudgetSetCode: {17}{20}- Description: {18}{20}- Status: {19}{20}", new object[] { 
                this.ContractCostChangeCode, (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.ContractCostCode == null) ? string.Empty : this.ContractCostCode.ToString(), (this.ContractChangeCode == null) ? string.Empty : this.ContractChangeCode.ToString(), !this.Cash.HasValue ? string.Empty : this.Cash.ToString(), !this.ChangeCash.HasValue ? string.Empty : this.ChangeCash.ToString(), !this.NewCash.HasValue ? string.Empty : this.NewCash.ToString(), !this.OriginalCash.HasValue ? string.Empty : this.OriginalCash.ToString(), !this.TotalChangeCash.HasValue ? string.Empty : this.TotalChangeCash.ToString(), (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), !this.ChangeMoney.HasValue ? string.Empty : this.ChangeMoney.ToString(), !this.NewMoney.HasValue ? string.Empty : this.NewMoney.ToString(), !this.OriginalMoney.HasValue ? string.Empty : this.OriginalMoney.ToString(), !this.TotalChangeMoney.HasValue ? string.Empty : this.TotalChangeMoney.ToString(), 
                (this.CostCode == null) ? string.Empty : this.CostCode.ToString(), (this.CostBudgetSetCode == null) ? string.Empty : this.CostBudgetSetCode.ToString(), (this.Description == null) ? string.Empty : this.Description.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [Description("原币当前金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? Cash
        {
            get
            {
                return this.entityData.Cash;
            }
            set
            {
                if (this.entityData.Cash != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.Cash, this.entityData.Cash);
                    this.entityData.Cash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.Cash, this.entityData.Cash);
                    this.OnPropertyChanged("Cash");
                }
            }
        }

        [DataObjectField(false, false, true), Description("原币变更金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? ChangeCash
        {
            get
            {
                return this.entityData.ChangeCash;
            }
            set
            {
                if (this.entityData.ChangeCash != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.ChangeCash, this.entityData.ChangeCash);
                    this.entityData.ChangeCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ChangeCash, this.entityData.ChangeCash);
                    this.OnPropertyChanged("ChangeCash");
                }
            }
        }

        [Description("本币变更金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? ChangeMoney
        {
            get
            {
                return this.entityData.ChangeMoney;
            }
            set
            {
                if (this.entityData.ChangeMoney != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.ChangeMoney, this.entityData.ChangeMoney);
                    this.entityData.ChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ChangeMoney, this.entityData.ChangeMoney);
                    this.OnPropertyChanged("ChangeMoney");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("合同变更code"), TiannuoPM.Entities.Bindable]
        public virtual string ContractChangeCode
        {
            get
            {
                return this.entityData.ContractChangeCode;
            }
            set
            {
                if (this.entityData.ContractChangeCode != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.entityData.ContractChangeCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.OnPropertyChanged("ContractChangeCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
        public virtual ContractChange ContractChangeCodeSource
        {
            get
            {
                return this._contractChangeCodeSource;
            }
            set
            {
                this._contractChangeCodeSource = value;
            }
        }

        [Description("合同code"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(ContractCostChangeColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("合同金额变更code"), DataObjectField(true, false, false, 50)]
        public virtual string ContractCostChangeCode
        {
            get
            {
                return this.entityData.ContractCostChangeCode;
            }
            set
            {
                if (this.entityData.ContractCostChangeCode != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.ContractCostChangeCode, this.entityData.ContractCostChangeCode);
                    this.entityData.ContractCostChangeCode = value;
                    this.EntityId.ContractCostChangeCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ContractCostChangeCode, this.entityData.ContractCostChangeCode);
                    this.OnPropertyChanged("ContractCostChangeCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("合同金额code")]
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
                    this.OnColumnChanging(ContractCostChangeColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.entityData.ContractCostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ContractCostCode, this.entityData.ContractCostCode);
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
                    this.OnColumnChanging(ContractCostChangeColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.entityData.CostBudgetSetCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.OnPropertyChanged("CostBudgetSetCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("费用项编号 CBS.CostCode"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.CostCode, this.entityData.CostCode);
                    this.entityData.CostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.CostCode, this.entityData.CostCode);
                    this.OnPropertyChanged("CostCode");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description("描述"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.Description, this.entityData.Description);
                    this.entityData.Description = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.Description, this.entityData.Description);
                    this.OnPropertyChanged("Description");
                }
            }
        }

        [XmlIgnore]
        public ContractCostChangeKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractCostChangeKey(this);
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
                    this.entityTrackingKey = "ContractCostChange" + this.ContractCostChangeCode.ToString();
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

        [Description("汇率"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [DataObjectField(false, false, true), Description("本币变更前金额"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("币种"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [DataObjectField(false, false, true), Description("原币变更后金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? NewCash
        {
            get
            {
                return this.entityData.NewCash;
            }
            set
            {
                if (this.entityData.NewCash != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.NewCash, this.entityData.NewCash);
                    this.entityData.NewCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.NewCash, this.entityData.NewCash);
                    this.OnPropertyChanged("NewCash");
                }
            }
        }

        [DataObjectField(false, false, true), Description("本币变更后金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? NewMoney
        {
            get
            {
                return this.entityData.NewMoney;
            }
            set
            {
                if (this.entityData.NewMoney != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.NewMoney, this.entityData.NewMoney);
                    this.entityData.NewMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.NewMoney, this.entityData.NewMoney);
                    this.OnPropertyChanged("NewMoney");
                }
            }
        }

        [Description("原币 原始金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? OriginalCash
        {
            get
            {
                return this.entityData.OriginalCash;
            }
            set
            {
                if (this.entityData.OriginalCash != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.OriginalCash, this.entityData.OriginalCash);
                    this.entityData.OriginalCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.OriginalCash, this.entityData.OriginalCash);
                    this.OnPropertyChanged("OriginalCash");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractCostChangeCode
        {
            get
            {
                return this.entityData.OriginalContractCostChangeCode;
            }
            set
            {
                this.entityData.OriginalContractCostChangeCode = value;
            }
        }

        [Description("本币原始金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.entityData.OriginalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.OnPropertyChanged("OriginalMoney");
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
                this.parentCollection = value as TList<ContractCostChange>;
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

        [DataObjectField(false, false, true), Description("是否为新增的款项"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractCostChangeColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "ContractCostChangeCode", "ContractCode", "ContractCostCode", "ContractChangeCode", "Cash", "ChangeCash", "NewCash", "OriginalCash", "TotalChangeCash", "MoneyType", "ExchangeRate", "Money", "ChangeMoney", "NewMoney", "OriginalMoney", "TotalChangeMoney", 
                    "CostCode", "CostBudgetSetCode", "Description", "Status"
                 };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "ContractCostChange";
            }
        }

        [DataObjectField(false, false, true), Description("累计变更"), TiannuoPM.Entities.Bindable]
        public virtual decimal? TotalChangeCash
        {
            get
            {
                return this.entityData.TotalChangeCash;
            }
            set
            {
                if (this.entityData.TotalChangeCash != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.TotalChangeCash, this.entityData.TotalChangeCash);
                    this.entityData.TotalChangeCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.TotalChangeCash, this.entityData.TotalChangeCash);
                    this.OnPropertyChanged("TotalChangeCash");
                }
            }
        }

        [Description("累计变更金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? TotalChangeMoney
        {
            get
            {
                return this.entityData.TotalChangeMoney;
            }
            set
            {
                if (this.entityData.TotalChangeMoney != value)
                {
                    this.OnColumnChanging(ContractCostChangeColumn.TotalChangeMoney, this.entityData.TotalChangeMoney);
                    this.entityData.TotalChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractCostChangeColumn.TotalChangeMoney, this.entityData.TotalChangeMoney);
                    this.OnPropertyChanged("TotalChangeMoney");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractCostChangeEntityData : ICloneable
        {
            public decimal? Cash = null;
            public decimal? ChangeCash = null;
            public decimal? ChangeMoney = null;
            public string ContractChangeCode = null;
            public string ContractCode = null;
            public string ContractCostChangeCode;
            public string ContractCostCode = null;
            public string CostBudgetSetCode = null;
            public string CostCode = null;
            public string Description = null;
            public decimal? ExchangeRate = null;
            public decimal? Money = null;
            public string MoneyType = null;
            public decimal? NewCash = null;
            public decimal? NewMoney = null;
            public decimal? OriginalCash = null;
            public string OriginalContractCostChangeCode;
            public decimal? OriginalMoney = null;
            public int? Status = null;
            public decimal? TotalChangeCash = null;
            public decimal? TotalChangeMoney = null;

            public object Clone()
            {
                ContractCostChangeBase.ContractCostChangeEntityData data = new ContractCostChangeBase.ContractCostChangeEntityData();
                data.ContractCostChangeCode = this.ContractCostChangeCode;
                data.OriginalContractCostChangeCode = this.OriginalContractCostChangeCode;
                data.ContractCode = this.ContractCode;
                data.ContractCostCode = this.ContractCostCode;
                data.ContractChangeCode = this.ContractChangeCode;
                data.Cash = this.Cash;
                data.ChangeCash = this.ChangeCash;
                data.NewCash = this.NewCash;
                data.OriginalCash = this.OriginalCash;
                data.TotalChangeCash = this.TotalChangeCash;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                data.Money = this.Money;
                data.ChangeMoney = this.ChangeMoney;
                data.NewMoney = this.NewMoney;
                data.OriginalMoney = this.OriginalMoney;
                data.TotalChangeMoney = this.TotalChangeMoney;
                data.CostCode = this.CostCode;
                data.CostBudgetSetCode = this.CostBudgetSetCode;
                data.Description = this.Description;
                data.Status = this.Status;
                return data;
            }
        }
    }
}

