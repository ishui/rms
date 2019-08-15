namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class ContractChangeBase : EntityBase, IEntityId<ContractChangeKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractChangeKey _entityId;
        private ISite _site;
        private ContractChangeEntityData backupData;
        private ContractChangeEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractChange> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractChangeEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractChangeEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractChangeBase()
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractChangeEntityData();
            this.backupData = null;
        }

        public ContractChangeBase(string contractChangeContractChangeCode, string contractChangeContractChangeId, string contractChangeContractCode, string contractChangeVoucher, decimal? contractChangeMoney, decimal? contractChangeChangeMoney, decimal? contractChangeNewMoney, decimal? contractChangeOriginalMoney, decimal? contractChangeTotalChangeMoney, decimal? contractChangeSupplierChangeMoney, decimal? contractChangeConsultantAuditMoney, decimal? contractChangeProjectAuditMoney, string contractChangeChangeReason, int? contractChangeStatus, string contractChangeChangePerson, DateTime? contractChangeChangeDate, string contractChangeChangeType, string contractChangeCheckPerson, DateTime? contractChangeCheckDate)
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractChangeEntityData();
            this.backupData = null;
            this.ContractChangeCode = contractChangeContractChangeCode;
            this.ContractChangeId = contractChangeContractChangeId;
            this.ContractCode = contractChangeContractCode;
            this.Voucher = contractChangeVoucher;
            this.Money = contractChangeMoney;
            this.ChangeMoney = contractChangeChangeMoney;
            this.NewMoney = contractChangeNewMoney;
            this.OriginalMoney = contractChangeOriginalMoney;
            this.TotalChangeMoney = contractChangeTotalChangeMoney;
            this.SupplierChangeMoney = contractChangeSupplierChangeMoney;
            this.ConsultantAuditMoney = contractChangeConsultantAuditMoney;
            this.ProjectAuditMoney = contractChangeProjectAuditMoney;
            this.ChangeReason = contractChangeChangeReason;
            this.Status = contractChangeStatus;
            this.ChangePerson = contractChangeChangePerson;
            this.ChangeDate = contractChangeChangeDate;
            this.ChangeType = contractChangeChangeType;
            this.CheckPerson = contractChangeCheckPerson;
            this.CheckDate = contractChangeCheckDate;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractChangeCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractChangeCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractChangeId", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Voucher", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ChangeReason", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ChangePerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ChangeType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckPerson", 50));
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

        public virtual ContractChange Copy()
        {
            ContractChange change = new ContractChange();
            change.ContractChangeCode = this.ContractChangeCode;
            change.OriginalContractChangeCode = this.OriginalContractChangeCode;
            change.ContractChangeId = this.ContractChangeId;
            change.ContractCode = this.ContractCode;
            change.Voucher = this.Voucher;
            change.Money = this.Money;
            change.ChangeMoney = this.ChangeMoney;
            change.NewMoney = this.NewMoney;
            change.OriginalMoney = this.OriginalMoney;
            change.TotalChangeMoney = this.TotalChangeMoney;
            change.SupplierChangeMoney = this.SupplierChangeMoney;
            change.ConsultantAuditMoney = this.ConsultantAuditMoney;
            change.ProjectAuditMoney = this.ProjectAuditMoney;
            change.ChangeReason = this.ChangeReason;
            change.Status = this.Status;
            change.ChangePerson = this.ChangePerson;
            change.ChangeDate = this.ChangeDate;
            change.ChangeType = this.ChangeType;
            change.CheckPerson = this.CheckPerson;
            change.CheckDate = this.CheckDate;
            change.AcceptChanges();
            return change;
        }

        public static ContractChange CreateContractChange(string contractChangeContractChangeCode, string contractChangeContractChangeId, string contractChangeContractCode, string contractChangeVoucher, decimal? contractChangeMoney, decimal? contractChangeChangeMoney, decimal? contractChangeNewMoney, decimal? contractChangeOriginalMoney, decimal? contractChangeTotalChangeMoney, decimal? contractChangeSupplierChangeMoney, decimal? contractChangeConsultantAuditMoney, decimal? contractChangeProjectAuditMoney, string contractChangeChangeReason, int? contractChangeStatus, string contractChangeChangePerson, DateTime? contractChangeChangeDate, string contractChangeChangeType, string contractChangeCheckPerson, DateTime? contractChangeCheckDate)
        {
            ContractChange change = new ContractChange();
            change.ContractChangeCode = contractChangeContractChangeCode;
            change.ContractChangeId = contractChangeContractChangeId;
            change.ContractCode = contractChangeContractCode;
            change.Voucher = contractChangeVoucher;
            change.Money = contractChangeMoney;
            change.ChangeMoney = contractChangeChangeMoney;
            change.NewMoney = contractChangeNewMoney;
            change.OriginalMoney = contractChangeOriginalMoney;
            change.TotalChangeMoney = contractChangeTotalChangeMoney;
            change.SupplierChangeMoney = contractChangeSupplierChangeMoney;
            change.ConsultantAuditMoney = contractChangeConsultantAuditMoney;
            change.ProjectAuditMoney = contractChangeProjectAuditMoney;
            change.ChangeReason = contractChangeChangeReason;
            change.Status = contractChangeStatus;
            change.ChangePerson = contractChangeChangePerson;
            change.ChangeDate = contractChangeChangeDate;
            change.ChangeType = contractChangeChangeType;
            change.CheckPerson = contractChangeCheckPerson;
            change.CheckDate = contractChangeCheckDate;
            return change;
        }

        public virtual ContractChange DeepCopy()
        {
            return EntityHelper.Clone<ContractChange>(this as ContractChange);
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

        public virtual bool Equals(ContractChangeBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractChangeBase Object1, ContractChangeBase Object2)
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
            if (Object1.ContractChangeCode != Object2.ContractChangeCode)
            {
                flag = false;
            }
            if ((Object1.ContractChangeId != null) && (Object2.ContractChangeId != null))
            {
                if (Object1.ContractChangeId != Object2.ContractChangeId)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractChangeId == null) ^ (Object2.ContractChangeId == null))
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
            if ((Object1.Voucher != null) && (Object2.Voucher != null))
            {
                if (Object1.Voucher != Object2.Voucher)
                {
                    flag = false;
                }
            }
            else if ((Object1.Voucher == null) ^ (Object2.Voucher == null))
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
            if (Object1.SupplierChangeMoney.HasValue && Object2.SupplierChangeMoney.HasValue)
            {
                if (Object1.SupplierChangeMoney != Object2.SupplierChangeMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.SupplierChangeMoney.HasValue ^ !Object2.SupplierChangeMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.ConsultantAuditMoney.HasValue && Object2.ConsultantAuditMoney.HasValue)
            {
                if (Object1.ConsultantAuditMoney != Object2.ConsultantAuditMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.ConsultantAuditMoney.HasValue ^ !Object2.ConsultantAuditMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.ProjectAuditMoney.HasValue && Object2.ProjectAuditMoney.HasValue)
            {
                if (Object1.ProjectAuditMoney != Object2.ProjectAuditMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.ProjectAuditMoney.HasValue ^ !Object2.ProjectAuditMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.ChangeReason != null) && (Object2.ChangeReason != null))
            {
                if (Object1.ChangeReason != Object2.ChangeReason)
                {
                    flag = false;
                }
            }
            else if ((Object1.ChangeReason == null) ^ (Object2.ChangeReason == null))
            {
                flag = false;
            }
            if (Object1.Status.HasValue && Object2.Status.HasValue)
            {
                if (Object1.Status != Object2.Status)
                {
                    flag = false;
                }
            }
            else if (!Object1.Status.HasValue ^ !Object2.Status.HasValue)
            {
                flag = false;
            }
            if ((Object1.ChangePerson != null) && (Object2.ChangePerson != null))
            {
                if (Object1.ChangePerson != Object2.ChangePerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.ChangePerson == null) ^ (Object2.ChangePerson == null))
            {
                flag = false;
            }
            if (Object1.ChangeDate.HasValue && Object2.ChangeDate.HasValue)
            {
                if (Object1.ChangeDate != Object2.ChangeDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.ChangeDate.HasValue ^ !Object2.ChangeDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.ChangeType != null) && (Object2.ChangeType != null))
            {
                if (Object1.ChangeType != Object2.ChangeType)
                {
                    flag = false;
                }
            }
            else if ((Object1.ChangeType == null) ^ (Object2.ChangeType == null))
            {
                flag = false;
            }
            if ((Object1.CheckPerson != null) && (Object2.CheckPerson != null))
            {
                if (Object1.CheckPerson != Object2.CheckPerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.CheckPerson == null) ^ (Object2.CheckPerson == null))
            {
                flag = false;
            }
            if (Object1.CheckDate.HasValue && Object2.CheckDate.HasValue)
            {
                if (Object1.CheckDate != Object2.CheckDate)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.CheckDate.HasValue ^ !Object2.CheckDate.HasValue)
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

        public void OnColumnChanged(ContractChangeColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractChangeColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractChangeEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractChangeEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractChangeColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractChangeColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractChangeEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractChangeEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractChange);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractChangeEntityData;
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
                    this.parentCollection.Remove((ContractChange) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{20}{19}- ContractChangeCode: {0}{19}- ContractChangeId: {1}{19}- ContractCode: {2}{19}- Voucher: {3}{19}- Money: {4}{19}- ChangeMoney: {5}{19}- NewMoney: {6}{19}- OriginalMoney: {7}{19}- TotalChangeMoney: {8}{19}- SupplierChangeMoney: {9}{19}- ConsultantAuditMoney: {10}{19}- ProjectAuditMoney: {11}{19}- ChangeReason: {12}{19}- Status: {13}{19}- ChangePerson: {14}{19}- ChangeDate: {15}{19}- ChangeType: {16}{19}- CheckPerson: {17}{19}- CheckDate: {18}{19}", new object[] { 
                this.ContractChangeCode, (this.ContractChangeId == null) ? string.Empty : this.ContractChangeId.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.Voucher == null) ? string.Empty : this.Voucher.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), !this.ChangeMoney.HasValue ? string.Empty : this.ChangeMoney.ToString(), !this.NewMoney.HasValue ? string.Empty : this.NewMoney.ToString(), !this.OriginalMoney.HasValue ? string.Empty : this.OriginalMoney.ToString(), !this.TotalChangeMoney.HasValue ? string.Empty : this.TotalChangeMoney.ToString(), !this.SupplierChangeMoney.HasValue ? string.Empty : this.SupplierChangeMoney.ToString(), !this.ConsultantAuditMoney.HasValue ? string.Empty : this.ConsultantAuditMoney.ToString(), !this.ProjectAuditMoney.HasValue ? string.Empty : this.ProjectAuditMoney.ToString(), (this.ChangeReason == null) ? string.Empty : this.ChangeReason.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), (this.ChangePerson == null) ? string.Empty : this.ChangePerson.ToString(), !this.ChangeDate.HasValue ? string.Empty : this.ChangeDate.ToString(), 
                (this.ChangeType == null) ? string.Empty : this.ChangeType.ToString(), (this.CheckPerson == null) ? string.Empty : this.CheckPerson.ToString(), !this.CheckDate.HasValue ? string.Empty : this.CheckDate.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [Description("变更日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? ChangeDate
        {
            get
            {
                return this.entityData.ChangeDate;
            }
            set
            {
                if (this.entityData.ChangeDate != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ChangeDate, this.entityData.ChangeDate);
                    this.entityData.ChangeDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ChangeDate, this.entityData.ChangeDate);
                    this.OnPropertyChanged("ChangeDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("本次变更金额")]
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
                    this.OnColumnChanging(ContractChangeColumn.ChangeMoney, this.entityData.ChangeMoney);
                    this.entityData.ChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ChangeMoney, this.entityData.ChangeMoney);
                    this.OnPropertyChanged("ChangeMoney");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("变更者"), TiannuoPM.Entities.Bindable]
        public virtual string ChangePerson
        {
            get
            {
                return this.entityData.ChangePerson;
            }
            set
            {
                if (this.entityData.ChangePerson != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ChangePerson, this.entityData.ChangePerson);
                    this.entityData.ChangePerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ChangePerson, this.entityData.ChangePerson);
                    this.OnPropertyChanged("ChangePerson");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description("变更原因及摘要"), TiannuoPM.Entities.Bindable]
        public virtual string ChangeReason
        {
            get
            {
                return this.entityData.ChangeReason;
            }
            set
            {
                if (this.entityData.ChangeReason != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ChangeReason, this.entityData.ChangeReason);
                    this.entityData.ChangeReason = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ChangeReason, this.entityData.ChangeReason);
                    this.OnPropertyChanged("ChangeReason");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("变更类型"), TiannuoPM.Entities.Bindable]
        public virtual string ChangeType
        {
            get
            {
                return this.entityData.ChangeType;
            }
            set
            {
                if (this.entityData.ChangeType != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ChangeType, this.entityData.ChangeType);
                    this.entityData.ChangeType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ChangeType, this.entityData.ChangeType);
                    this.OnPropertyChanged("ChangeType");
                }
            }
        }

        [Description("审核日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? CheckDate
        {
            get
            {
                return this.entityData.CheckDate;
            }
            set
            {
                if (this.entityData.CheckDate != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.CheckDate, this.entityData.CheckDate);
                    this.entityData.CheckDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.CheckDate, this.entityData.CheckDate);
                    this.OnPropertyChanged("CheckDate");
                }
            }
        }

        [Description("审核人"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string CheckPerson
        {
            get
            {
                return this.entityData.CheckPerson;
            }
            set
            {
                if (this.entityData.CheckPerson != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.CheckPerson, this.entityData.CheckPerson);
                    this.entityData.CheckPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.CheckPerson, this.entityData.CheckPerson);
                    this.OnPropertyChanged("CheckPerson");
                }
            }
        }

        [Description("顾问估算师审核金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? ConsultantAuditMoney
        {
            get
            {
                return this.entityData.ConsultantAuditMoney;
            }
            set
            {
                if (this.entityData.ConsultantAuditMoney != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ConsultantAuditMoney, this.entityData.ConsultantAuditMoney);
                    this.entityData.ConsultantAuditMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ConsultantAuditMoney, this.entityData.ConsultantAuditMoney);
                    this.OnPropertyChanged("ConsultantAuditMoney");
                }
            }
        }

        [Description("合同变更code(主键)"), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
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
                    this.OnColumnChanging(ContractChangeColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.entityData.ContractChangeCode = value;
                    this.EntityId.ContractChangeCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.OnPropertyChanged("ContractChangeCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("审批表编号")]
        public virtual string ContractChangeId
        {
            get
            {
                return this.entityData.ContractChangeId;
            }
            set
            {
                if (this.entityData.ContractChangeId != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ContractChangeId, this.entityData.ContractChangeId);
                    this.entityData.ContractChangeId = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ContractChangeId, this.entityData.ContractChangeId);
                    this.OnPropertyChanged("ContractChangeId");
                }
            }
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
                    this.OnColumnChanging(ContractChangeColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
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

        [TiannuoPM.Entities.Bindable]
        public TList<ContractCostChange> ContractCostChangeCollection
        {
            get
            {
                return this.entityData.ContractCostChangeCollection;
            }
            set
            {
                this.entityData.ContractCostChangeCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractNexus> ContractNexusCollection
        {
            get
            {
                return this.entityData.ContractNexusCollection;
            }
            set
            {
                this.entityData.ContractNexusCollection = value;
            }
        }

        [XmlIgnore]
        public ContractChangeKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractChangeKey(this);
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
                    this.entityTrackingKey = "ContractChange" + this.ContractChangeCode.ToString();
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

        [Description("变更前金额 money+changemoney=newmoney"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(ContractChangeColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Description("变更后金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(ContractChangeColumn.NewMoney, this.entityData.NewMoney);
                    this.entityData.NewMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.NewMoney, this.entityData.NewMoney);
                    this.OnPropertyChanged("NewMoney");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractChangeCode
        {
            get
            {
                return this.entityData.OriginalContractChangeCode;
            }
            set
            {
                this.entityData.OriginalContractChangeCode = value;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("原合同金额")]
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
                    this.OnColumnChanging(ContractChangeColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.entityData.OriginalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.OriginalMoney, this.entityData.OriginalMoney);
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
                this.parentCollection = value as TList<ContractChange>;
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("项目合约部审核金额")]
        public virtual decimal? ProjectAuditMoney
        {
            get
            {
                return this.entityData.ProjectAuditMoney;
            }
            set
            {
                if (this.entityData.ProjectAuditMoney != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.ProjectAuditMoney, this.entityData.ProjectAuditMoney);
                    this.entityData.ProjectAuditMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.ProjectAuditMoney, this.entityData.ProjectAuditMoney);
                    this.OnPropertyChanged("ProjectAuditMoney");
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

        [DataObjectField(false, false, true), Description("状态 0:// 已审 通过  1:// 申请 草稿 2: // 申请流程中 3作废"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractChangeColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("供应商本次变更申请金额")]
        public virtual decimal? SupplierChangeMoney
        {
            get
            {
                return this.entityData.SupplierChangeMoney;
            }
            set
            {
                if (this.entityData.SupplierChangeMoney != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.SupplierChangeMoney, this.entityData.SupplierChangeMoney);
                    this.entityData.SupplierChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.SupplierChangeMoney, this.entityData.SupplierChangeMoney);
                    this.OnPropertyChanged("SupplierChangeMoney");
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "ContractChangeCode", "ContractChangeId", "ContractCode", "Voucher", "Money", "ChangeMoney", "NewMoney", "OriginalMoney", "TotalChangeMoney", "SupplierChangeMoney", "ConsultantAuditMoney", "ProjectAuditMoney", "ChangeReason", "Status", "ChangePerson", "ChangeDate", 
                    "ChangeType", "CheckPerson", "CheckDate"
                 };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractChange";
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("累计已批变更")]
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
                    this.OnColumnChanging(ContractChangeColumn.TotalChangeMoney, this.entityData.TotalChangeMoney);
                    this.entityData.TotalChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.TotalChangeMoney, this.entityData.TotalChangeMoney);
                    this.OnPropertyChanged("TotalChangeMoney");
                }
            }
        }

        [Description("变更依据"), DataObjectField(false, false, true, 800), TiannuoPM.Entities.Bindable]
        public virtual string Voucher
        {
            get
            {
                return this.entityData.Voucher;
            }
            set
            {
                if (this.entityData.Voucher != value)
                {
                    this.OnColumnChanging(ContractChangeColumn.Voucher, this.entityData.Voucher);
                    this.entityData.Voucher = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractChangeColumn.Voucher, this.entityData.Voucher);
                    this.OnPropertyChanged("Voucher");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractChangeEntityData : ICloneable
        {
            public DateTime? ChangeDate = null;
            public decimal? ChangeMoney = null;
            public string ChangePerson = null;
            public string ChangeReason = null;
            public string ChangeType = null;
            public DateTime? CheckDate = null;
            public string CheckPerson = null;
            public decimal? ConsultantAuditMoney = null;
            public string ContractChangeCode;
            public string ContractChangeId = null;
            public string ContractCode = null;
            private TList<ContractCostChange> contractCostChangeContractChangeCode;
            private TList<ContractNexus> contractNexusContractChangeCode;
            public decimal? Money = null;
            public decimal? NewMoney = null;
            public string OriginalContractChangeCode;
            public decimal? OriginalMoney = null;
            public decimal? ProjectAuditMoney = null;
            public int? Status = null;
            public decimal? SupplierChangeMoney = null;
            public decimal? TotalChangeMoney = null;
            public string Voucher = null;

            public object Clone()
            {
                ContractChangeBase.ContractChangeEntityData data = new ContractChangeBase.ContractChangeEntityData();
                data.ContractChangeCode = this.ContractChangeCode;
                data.OriginalContractChangeCode = this.OriginalContractChangeCode;
                data.ContractChangeId = this.ContractChangeId;
                data.ContractCode = this.ContractCode;
                data.Voucher = this.Voucher;
                data.Money = this.Money;
                data.ChangeMoney = this.ChangeMoney;
                data.NewMoney = this.NewMoney;
                data.OriginalMoney = this.OriginalMoney;
                data.TotalChangeMoney = this.TotalChangeMoney;
                data.SupplierChangeMoney = this.SupplierChangeMoney;
                data.ConsultantAuditMoney = this.ConsultantAuditMoney;
                data.ProjectAuditMoney = this.ProjectAuditMoney;
                data.ChangeReason = this.ChangeReason;
                data.Status = this.Status;
                data.ChangePerson = this.ChangePerson;
                data.ChangeDate = this.ChangeDate;
                data.ChangeType = this.ChangeType;
                data.CheckPerson = this.CheckPerson;
                data.CheckDate = this.CheckDate;
                return data;
            }

            public TList<ContractCostChange> ContractCostChangeCollection
            {
                get
                {
                    if (this.contractCostChangeContractChangeCode == null)
                    {
                        this.contractCostChangeContractChangeCode = new TList<ContractCostChange>();
                    }
                    return this.contractCostChangeContractChangeCode;
                }
                set
                {
                    this.contractCostChangeContractChangeCode = value;
                }
            }

            public TList<ContractNexus> ContractNexusCollection
            {
                get
                {
                    if (this.contractNexusContractChangeCode == null)
                    {
                        this.contractNexusContractChangeCode = new TList<ContractNexus>();
                    }
                    return this.contractNexusContractChangeCode;
                }
                set
                {
                    this.contractNexusContractChangeCode = value;
                }
            }
        }
    }
}

