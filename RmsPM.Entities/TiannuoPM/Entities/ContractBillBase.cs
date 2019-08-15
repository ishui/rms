namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ContractBillBase : EntityBase, IEntityId<ContractBillKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private ContractBillKey _entityId;
        private ISite _site;
        private ContractBillEntityData backupData;
        private ContractBillEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractBill> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractBillEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractBillEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractBillBase()
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractBillEntityData();
            this.backupData = null;
        }

        public ContractBillBase(string contractBillProjectCode, string contractBillContractCode, string contractBillBillNo, decimal? contractBillBillMoney)
        {
            this.inTxn = false;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new ContractBillEntityData();
            this.backupData = null;
            this.ProjectCode = contractBillProjectCode;
            this.ContractCode = contractBillContractCode;
            this.BillNo = contractBillBillNo;
            this.BillMoney = contractBillBillMoney;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BillNo", 50));
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

        public virtual ContractBill Copy()
        {
            ContractBill bill = new ContractBill();
            bill.Code = this.Code;
            bill.ProjectCode = this.ProjectCode;
            bill.ContractCode = this.ContractCode;
            bill.BillNo = this.BillNo;
            bill.BillMoney = this.BillMoney;
            bill.AcceptChanges();
            return bill;
        }

        public static ContractBill CreateContractBill(string contractBillProjectCode, string contractBillContractCode, string contractBillBillNo, decimal? contractBillBillMoney)
        {
            ContractBill bill = new ContractBill();
            bill.ProjectCode = contractBillProjectCode;
            bill.ContractCode = contractBillContractCode;
            bill.BillNo = contractBillBillNo;
            bill.BillMoney = contractBillBillMoney;
            return bill;
        }

        public virtual ContractBill DeepCopy()
        {
            return EntityHelper.Clone<ContractBill>(this as ContractBill);
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

        public virtual bool Equals(ContractBillBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractBillBase Object1, ContractBillBase Object2)
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
            if (Object1.Code != Object2.Code)
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
            if ((Object1.BillNo != null) && (Object2.BillNo != null))
            {
                if (Object1.BillNo != Object2.BillNo)
                {
                    flag = false;
                }
            }
            else if ((Object1.BillNo == null) ^ (Object2.BillNo == null))
            {
                flag = false;
            }
            if (Object1.BillMoney.HasValue && Object2.BillMoney.HasValue)
            {
                if (Object1.BillMoney != Object2.BillMoney)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.BillMoney.HasValue ^ !Object2.BillMoney.HasValue)
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

        public void OnColumnChanged(ContractBillColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractBillColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractBillEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractBillEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractBillColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractBillColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractBillEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractBillEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractBill);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractBillEntityData;
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
                    this.parentCollection.Remove((ContractBill) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{6}{5}- Code: {0}{5}- ProjectCode: {1}{5}- ContractCode: {2}{5}- BillNo: {3}{5}- BillMoney: {4}{5}", new object[] { this.Code, (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.BillNo == null) ? string.Empty : this.BillNo.ToString(), !this.BillMoney.HasValue ? string.Empty : this.BillMoney.ToString(), Environment.NewLine, base.GetType() });
        }

        [TiannuoPM.Entities.Bindable, Description("发票金额"), DataObjectField(false, false, true)]
        public virtual decimal? BillMoney
        {
            get
            {
                return this.entityData.BillMoney;
            }
            set
            {
                if (this.entityData.BillMoney != value)
                {
                    this.OnColumnChanging(ContractBillColumn.BillMoney, this.entityData.BillMoney);
                    this.entityData.BillMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractBillColumn.BillMoney, this.entityData.BillMoney);
                    this.OnPropertyChanged("BillMoney");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("发票号")]
        public virtual string BillNo
        {
            get
            {
                return this.entityData.BillNo;
            }
            set
            {
                if (this.entityData.BillNo != value)
                {
                    this.OnColumnChanging(ContractBillColumn.BillNo, this.entityData.BillNo);
                    this.entityData.BillNo = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractBillColumn.BillNo, this.entityData.BillNo);
                    this.OnPropertyChanged("BillNo");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("发票code(主键)"), ReadOnly(false), DataObjectField(true, true, false)]
        public virtual int Code
        {
            get
            {
                return this.entityData.Code;
            }
            set
            {
                if (this.entityData.Code != value)
                {
                    this.OnColumnChanging(ContractBillColumn.Code, this.entityData.Code);
                    this.entityData.Code = value;
                    this.EntityId.Code = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractBillColumn.Code, this.entityData.Code);
                    this.OnPropertyChanged("Code");
                }
            }
        }

        [Description("合同code(外键)"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(ContractBillColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractBillColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
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

        [XmlIgnore]
        public ContractBillKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractBillKey(this);
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
                    this.entityTrackingKey = "ContractBill" + this.Code.ToString();
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

        [Browsable(false), XmlIgnore]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<ContractBill>;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("项目code(外键)"), DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(ContractBillColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractBillColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
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

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "Code", "ProjectCode", "ContractCode", "BillNo", "BillMoney" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractBill";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractBillEntityData : ICloneable
        {
            public decimal? BillMoney = null;
            public string BillNo = null;
            public int Code;
            public string ContractCode = null;
            public string ProjectCode = null;

            public object Clone()
            {
                ContractBillBase.ContractBillEntityData data = new ContractBillBase.ContractBillEntityData();
                data.Code = this.Code;
                data.ProjectCode = this.ProjectCode;
                data.ContractCode = this.ContractCode;
                data.BillNo = this.BillNo;
                data.BillMoney = this.BillMoney;
                return data;
            }
        }
    }
}

