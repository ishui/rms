namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class ContractAccountBase : EntityBase, IEntityId<ContractAccountKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractAccountKey _entityId;
        private ISite _site;
        private ContractAccountEntityData backupData;
        private ContractAccountEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractAccount> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractAccountEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractAccountEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractAccountBase()
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractAccountEntityData();
            this.backupData = null;
        }

        public ContractAccountBase(string contractAccountContractAccountCode, string contractAccountContractAccountID, string contractAccountContractCode, string contractAccountReason, int? contractAccountStatus, DateTime? contractAccountCreateDate, string contractAccountCreatePerson, string contractAccountContractChangeCode)
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractAccountEntityData();
            this.backupData = null;
            this.ContractAccountCode = contractAccountContractAccountCode;
            this.ContractAccountID = contractAccountContractAccountID;
            this.ContractCode = contractAccountContractCode;
            this.Reason = contractAccountReason;
            this.Status = contractAccountStatus;
            this.CreateDate = contractAccountCreateDate;
            this.CreatePerson = contractAccountCreatePerson;
            this.ContractChangeCode = contractAccountContractChangeCode;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractAccountCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractAccountCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractAccountID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Reason", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CreatePerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractChangeCode", 50));
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

        public virtual ContractAccount Copy()
        {
            ContractAccount account = new ContractAccount();
            account.ContractAccountCode = this.ContractAccountCode;
            account.OriginalContractAccountCode = this.OriginalContractAccountCode;
            account.ContractAccountID = this.ContractAccountID;
            account.ContractCode = this.ContractCode;
            account.Reason = this.Reason;
            account.Status = this.Status;
            account.CreateDate = this.CreateDate;
            account.CreatePerson = this.CreatePerson;
            account.ContractChangeCode = this.ContractChangeCode;
            account.AcceptChanges();
            return account;
        }

        public static ContractAccount CreateContractAccount(string contractAccountContractAccountCode, string contractAccountContractAccountID, string contractAccountContractCode, string contractAccountReason, int? contractAccountStatus, DateTime? contractAccountCreateDate, string contractAccountCreatePerson, string contractAccountContractChangeCode)
        {
            ContractAccount account = new ContractAccount();
            account.ContractAccountCode = contractAccountContractAccountCode;
            account.ContractAccountID = contractAccountContractAccountID;
            account.ContractCode = contractAccountContractCode;
            account.Reason = contractAccountReason;
            account.Status = contractAccountStatus;
            account.CreateDate = contractAccountCreateDate;
            account.CreatePerson = contractAccountCreatePerson;
            account.ContractChangeCode = contractAccountContractChangeCode;
            return account;
        }

        public virtual ContractAccount DeepCopy()
        {
            return EntityHelper.Clone<ContractAccount>(this as ContractAccount);
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

        public virtual bool Equals(ContractAccountBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractAccountBase Object1, ContractAccountBase Object2)
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
            if (Object1.ContractAccountCode != Object2.ContractAccountCode)
            {
                flag = false;
            }
            if ((Object1.ContractAccountID != null) && (Object2.ContractAccountID != null))
            {
                if (Object1.ContractAccountID != Object2.ContractAccountID)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractAccountID == null) ^ (Object2.ContractAccountID == null))
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
            if ((Object1.Reason != null) && (Object2.Reason != null))
            {
                if (Object1.Reason != Object2.Reason)
                {
                    flag = false;
                }
            }
            else if ((Object1.Reason == null) ^ (Object2.Reason == null))
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
            if (Object1.CreateDate.HasValue && Object2.CreateDate.HasValue)
            {
                if (Object1.CreateDate != Object2.CreateDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.CreateDate.HasValue ^ !Object2.CreateDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.CreatePerson != null) && (Object2.CreatePerson != null))
            {
                if (Object1.CreatePerson != Object2.CreatePerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.CreatePerson == null) ^ (Object2.CreatePerson == null))
            {
                flag = false;
            }
            if ((Object1.ContractChangeCode != null) && (Object2.ContractChangeCode != null))
            {
                if (Object1.ContractChangeCode != Object2.ContractChangeCode)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.ContractChangeCode == null) ^ (Object2.ContractChangeCode == null))
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

        public void OnColumnChanged(ContractAccountColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractAccountColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractAccountEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractAccountEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractAccountColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractAccountColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractAccountEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractAccountEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractAccount);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractAccountEntityData;
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
                    this.parentCollection.Remove((ContractAccount) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{9}{8}- ContractAccountCode: {0}{8}- ContractAccountID: {1}{8}- ContractCode: {2}{8}- Reason: {3}{8}- Status: {4}{8}- CreateDate: {5}{8}- CreatePerson: {6}{8}- ContractChangeCode: {7}{8}", new object[] { this.ContractAccountCode, (this.ContractAccountID == null) ? string.Empty : this.ContractAccountID.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.Reason == null) ? string.Empty : this.Reason.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), !this.CreateDate.HasValue ? string.Empty : this.CreateDate.ToString(), (this.CreatePerson == null) ? string.Empty : this.CreatePerson.ToString(), (this.ContractChangeCode == null) ? string.Empty : this.ContractChangeCode.ToString(), Environment.NewLine, base.GetType() });
        }

        [Description("合同code(主键)"), DataObjectField(true, false, false, 50), TiannuoPM.Entities.Bindable]
        public virtual string ContractAccountCode
        {
            get
            {
                return this.entityData.ContractAccountCode;
            }
            set
            {
                if (this.entityData.ContractAccountCode != value)
                {
                    this.OnColumnChanging(ContractAccountColumn.ContractAccountCode, this.entityData.ContractAccountCode);
                    this.entityData.ContractAccountCode = value;
                    this.EntityId.ContractAccountCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.ContractAccountCode, this.entityData.ContractAccountCode);
                    this.OnPropertyChanged("ContractAccountCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("合同结算单号"), TiannuoPM.Entities.Bindable]
        public virtual string ContractAccountID
        {
            get
            {
                return this.entityData.ContractAccountID;
            }
            set
            {
                if (this.entityData.ContractAccountID != value)
                {
                    this.OnColumnChanging(ContractAccountColumn.ContractAccountID, this.entityData.ContractAccountID);
                    this.entityData.ContractAccountID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.ContractAccountID, this.entityData.ContractAccountID);
                    this.OnPropertyChanged("ContractAccountID");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("合同变更号"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractAccountColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.entityData.ContractChangeCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.OnPropertyChanged("ContractChangeCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("合同code(外键)")]
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
                    this.OnColumnChanging(ContractAccountColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [Browsable(false), TiannuoPM.Entities.Bindable, XmlIgnore]
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

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("创建日期")]
        public virtual DateTime? CreateDate
        {
            get
            {
                return this.entityData.CreateDate;
            }
            set
            {
                if (this.entityData.CreateDate != value)
                {
                    this.OnColumnChanging(ContractAccountColumn.CreateDate, this.entityData.CreateDate);
                    this.entityData.CreateDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.CreateDate, this.entityData.CreateDate);
                    this.OnPropertyChanged("CreateDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("创建者"), DataObjectField(false, false, true, 50)]
        public virtual string CreatePerson
        {
            get
            {
                return this.entityData.CreatePerson;
            }
            set
            {
                if (this.entityData.CreatePerson != value)
                {
                    this.OnColumnChanging(ContractAccountColumn.CreatePerson, this.entityData.CreatePerson);
                    this.entityData.CreatePerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.CreatePerson, this.entityData.CreatePerson);
                    this.OnPropertyChanged("CreatePerson");
                }
            }
        }

        [XmlIgnore]
        public ContractAccountKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractAccountKey(this);
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
                    this.entityTrackingKey = "ContractAccount" + this.ContractAccountCode.ToString();
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
        public virtual string OriginalContractAccountCode
        {
            get
            {
                return this.entityData.OriginalContractAccountCode;
            }
            set
            {
                this.entityData.OriginalContractAccountCode = value;
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
                this.parentCollection = value as TList<ContractAccount>;
            }
        }

        [DataObjectField(false, false, true, 800), Description("原因"), TiannuoPM.Entities.Bindable]
        public virtual string Reason
        {
            get
            {
                return this.entityData.Reason;
            }
            set
            {
                if (this.entityData.Reason != value)
                {
                    this.OnColumnChanging(ContractAccountColumn.Reason, this.entityData.Reason);
                    this.entityData.Reason = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.Reason, this.entityData.Reason);
                    this.OnPropertyChanged("Reason");
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

        [DataObjectField(false, false, true), Description("审核状态"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractAccountColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractAccountColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "ContractAccountCode", "ContractAccountID", "ContractCode", "Reason", "Status", "CreateDate", "CreatePerson", "ContractChangeCode" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractAccount";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractAccountEntityData : ICloneable
        {
            public string ContractAccountCode;
            public string ContractAccountID = null;
            public string ContractChangeCode = null;
            public string ContractCode = null;
            public DateTime? CreateDate = null;
            public string CreatePerson = null;
            public string OriginalContractAccountCode;
            public string Reason = null;
            public int? Status = null;

            public object Clone()
            {
                ContractAccountBase.ContractAccountEntityData data = new ContractAccountBase.ContractAccountEntityData();
                data.ContractAccountCode = this.ContractAccountCode;
                data.OriginalContractAccountCode = this.OriginalContractAccountCode;
                data.ContractAccountID = this.ContractAccountID;
                data.ContractCode = this.ContractCode;
                data.Reason = this.Reason;
                data.Status = this.Status;
                data.CreateDate = this.CreateDate;
                data.CreatePerson = this.CreatePerson;
                data.ContractChangeCode = this.ContractChangeCode;
                return data;
            }
        }
    }
}

