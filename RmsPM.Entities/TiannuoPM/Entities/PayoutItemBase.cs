namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class PayoutItemBase : EntityBase, IEntityId<PayoutItemKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private PayoutItemKey _entityId;
        private PaymentItem _paymentItemCodeSource;
        private Payout _payoutCodeSource;
        private ISite _site;
        private PayoutItemEntityData backupData;
        private PayoutItemEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<PayoutItem> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event PayoutItemEventHandler ColumnChanged;

        [field: NonSerialized]
        public event PayoutItemEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public PayoutItemBase()
        {
            this.inTxn = false;
            this._payoutCodeSource = null;
            this._paymentItemCodeSource = null;
            this._site = null;
            this.entityData = new PayoutItemEntityData();
            this.backupData = null;
        }

        public PayoutItemBase(string payoutItemPayoutItemCode, string payoutItemPayoutCode, string payoutItemPaymentItemCode, decimal? payoutItemPayoutMoney, string payoutItemSubjectCode, string payoutItemRemark, string payoutItemAlloType, int? payoutItemIsManualAlloc, decimal? payoutItemPayoutCash, string payoutItemMoneyType, decimal? payoutItemExchangeRate, string payoutItemPayoutMoneyType, decimal? payoutItemPayoutExchangeRate)
        {
            this.inTxn = false;
            this._payoutCodeSource = null;
            this._paymentItemCodeSource = null;
            this._site = null;
            this.entityData = new PayoutItemEntityData();
            this.backupData = null;
            this.PayoutItemCode = payoutItemPayoutItemCode;
            this.PayoutCode = payoutItemPayoutCode;
            this.PaymentItemCode = payoutItemPaymentItemCode;
            this.PayoutMoney = payoutItemPayoutMoney;
            this.SubjectCode = payoutItemSubjectCode;
            this.Remark = payoutItemRemark;
            this.AlloType = payoutItemAlloType;
            this.IsManualAlloc = payoutItemIsManualAlloc;
            this.PayoutCash = payoutItemPayoutCash;
            this.MoneyType = payoutItemMoneyType;
            this.ExchangeRate = payoutItemExchangeRate;
            this.PayoutMoneyType = payoutItemPayoutMoneyType;
            this.PayoutExchangeRate = payoutItemPayoutExchangeRate;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "PayoutItemCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayoutItemCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayoutCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentItemCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SubjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("AlloType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayoutMoneyType", 50));
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

        public virtual PayoutItem Copy()
        {
            PayoutItem item = new PayoutItem();
            item.PayoutItemCode = this.PayoutItemCode;
            item.OriginalPayoutItemCode = this.OriginalPayoutItemCode;
            item.PayoutCode = this.PayoutCode;
            item.PaymentItemCode = this.PaymentItemCode;
            item.PayoutMoney = this.PayoutMoney;
            item.SubjectCode = this.SubjectCode;
            item.Remark = this.Remark;
            item.AlloType = this.AlloType;
            item.IsManualAlloc = this.IsManualAlloc;
            item.PayoutCash = this.PayoutCash;
            item.MoneyType = this.MoneyType;
            item.ExchangeRate = this.ExchangeRate;
            item.PayoutMoneyType = this.PayoutMoneyType;
            item.PayoutExchangeRate = this.PayoutExchangeRate;
            item.AcceptChanges();
            return item;
        }

        public static PayoutItem CreatePayoutItem(string payoutItemPayoutItemCode, string payoutItemPayoutCode, string payoutItemPaymentItemCode, decimal? payoutItemPayoutMoney, string payoutItemSubjectCode, string payoutItemRemark, string payoutItemAlloType, int? payoutItemIsManualAlloc, decimal? payoutItemPayoutCash, string payoutItemMoneyType, decimal? payoutItemExchangeRate, string payoutItemPayoutMoneyType, decimal? payoutItemPayoutExchangeRate)
        {
            PayoutItem item = new PayoutItem();
            item.PayoutItemCode = payoutItemPayoutItemCode;
            item.PayoutCode = payoutItemPayoutCode;
            item.PaymentItemCode = payoutItemPaymentItemCode;
            item.PayoutMoney = payoutItemPayoutMoney;
            item.SubjectCode = payoutItemSubjectCode;
            item.Remark = payoutItemRemark;
            item.AlloType = payoutItemAlloType;
            item.IsManualAlloc = payoutItemIsManualAlloc;
            item.PayoutCash = payoutItemPayoutCash;
            item.MoneyType = payoutItemMoneyType;
            item.ExchangeRate = payoutItemExchangeRate;
            item.PayoutMoneyType = payoutItemPayoutMoneyType;
            item.PayoutExchangeRate = payoutItemPayoutExchangeRate;
            return item;
        }

        public virtual PayoutItem DeepCopy()
        {
            return EntityHelper.Clone<PayoutItem>(this as PayoutItem);
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

        public virtual bool Equals(PayoutItemBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(PayoutItemBase Object1, PayoutItemBase Object2)
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
            if (Object1.PayoutItemCode != Object2.PayoutItemCode)
            {
                flag = false;
            }
            if ((Object1.PayoutCode != null) && (Object2.PayoutCode != null))
            {
                if (Object1.PayoutCode != Object2.PayoutCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PayoutCode == null) ^ (Object2.PayoutCode == null))
            {
                flag = false;
            }
            if ((Object1.PaymentItemCode != null) && (Object2.PaymentItemCode != null))
            {
                if (Object1.PaymentItemCode != Object2.PaymentItemCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentItemCode == null) ^ (Object2.PaymentItemCode == null))
            {
                flag = false;
            }
            if (Object1.PayoutMoney.HasValue && Object2.PayoutMoney.HasValue)
            {
                if (Object1.PayoutMoney != Object2.PayoutMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.PayoutMoney.HasValue ^ !Object2.PayoutMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.SubjectCode != null) && (Object2.SubjectCode != null))
            {
                if (Object1.SubjectCode != Object2.SubjectCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SubjectCode == null) ^ (Object2.SubjectCode == null))
            {
                flag = false;
            }
            if ((Object1.Remark != null) && (Object2.Remark != null))
            {
                if (Object1.Remark != Object2.Remark)
                {
                    flag = false;
                }
            }
            else if ((Object1.Remark == null) ^ (Object2.Remark == null))
            {
                flag = false;
            }
            if ((Object1.AlloType != null) && (Object2.AlloType != null))
            {
                if (Object1.AlloType != Object2.AlloType)
                {
                    flag = false;
                }
            }
            else if ((Object1.AlloType == null) ^ (Object2.AlloType == null))
            {
                flag = false;
            }
            if (Object1.IsManualAlloc.HasValue && Object2.IsManualAlloc.HasValue)
            {
                if (Object1.IsManualAlloc != Object2.IsManualAlloc)
                {
                    flag = false;
                }
            }
            else if (!Object1.IsManualAlloc.HasValue ^ !Object2.IsManualAlloc.HasValue)
            {
                flag = false;
            }
            if (Object1.PayoutCash.HasValue && Object2.PayoutCash.HasValue)
            {
                if (Object1.PayoutCash != Object2.PayoutCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.PayoutCash.HasValue ^ !Object2.PayoutCash.HasValue)
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
            if ((Object1.PayoutMoneyType != null) && (Object2.PayoutMoneyType != null))
            {
                if (Object1.PayoutMoneyType != Object2.PayoutMoneyType)
                {
                    flag = false;
                }
            }
            else if ((Object1.PayoutMoneyType == null) ^ (Object2.PayoutMoneyType == null))
            {
                flag = false;
            }
            if (Object1.PayoutExchangeRate.HasValue && Object2.PayoutExchangeRate.HasValue)
            {
                if (Object1.PayoutExchangeRate != Object2.PayoutExchangeRate)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.PayoutExchangeRate.HasValue ^ !Object2.PayoutExchangeRate.HasValue)
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

        public void OnColumnChanged(PayoutItemColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(PayoutItemColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                PayoutItemEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new PayoutItemEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(PayoutItemColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(PayoutItemColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                PayoutItemEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new PayoutItemEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as PayoutItem);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as PayoutItemEntityData;
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
                    this.parentCollection.Remove((PayoutItem) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{14}{13}- PayoutItemCode: {0}{13}- PayoutCode: {1}{13}- PaymentItemCode: {2}{13}- PayoutMoney: {3}{13}- SubjectCode: {4}{13}- Remark: {5}{13}- AlloType: {6}{13}- IsManualAlloc: {7}{13}- PayoutCash: {8}{13}- MoneyType: {9}{13}- ExchangeRate: {10}{13}- PayoutMoneyType: {11}{13}- PayoutExchangeRate: {12}{13}", new object[] { this.PayoutItemCode, (this.PayoutCode == null) ? string.Empty : this.PayoutCode.ToString(), (this.PaymentItemCode == null) ? string.Empty : this.PaymentItemCode.ToString(), !this.PayoutMoney.HasValue ? string.Empty : this.PayoutMoney.ToString(), (this.SubjectCode == null) ? string.Empty : this.SubjectCode.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), (this.AlloType == null) ? string.Empty : this.AlloType.ToString(), !this.IsManualAlloc.HasValue ? string.Empty : this.IsManualAlloc.ToString(), !this.PayoutCash.HasValue ? string.Empty : this.PayoutCash.ToString(), (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), (this.PayoutMoneyType == null) ? string.Empty : this.PayoutMoneyType.ToString(), !this.PayoutExchangeRate.HasValue ? string.Empty : this.PayoutExchangeRate.ToString(), Environment.NewLine, base.GetType() });
        }

        [DataObjectField(false, false, true, 50), Description("x"), TiannuoPM.Entities.Bindable]
        public virtual string AlloType
        {
            get
            {
                return this.entityData.AlloType;
            }
            set
            {
                if (this.entityData.AlloType != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.AlloType, this.entityData.AlloType);
                    this.entityData.AlloType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.AlloType, this.entityData.AlloType);
                    this.OnPropertyChanged("AlloType");
                }
            }
        }

        [XmlIgnore]
        public PayoutItemKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new PayoutItemKey(this);
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
                    this.entityTrackingKey = "PayoutItem" + this.PayoutItemCode.ToString();
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
                    this.OnColumnChanging(PayoutItemColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("x")]
        public virtual int? IsManualAlloc
        {
            get
            {
                return this.entityData.IsManualAlloc;
            }
            set
            {
                if (this.entityData.IsManualAlloc != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.IsManualAlloc, this.entityData.IsManualAlloc);
                    this.entityData.IsManualAlloc = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.IsManualAlloc, this.entityData.IsManualAlloc);
                    this.OnPropertyChanged("IsManualAlloc");
                }
            }
        }

        [Description("币种"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutItemColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalPayoutItemCode
        {
            get
            {
                return this.entityData.OriginalPayoutItemCode;
            }
            set
            {
                this.entityData.OriginalPayoutItemCode = value;
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
                this.parentCollection = value as TList<PayoutItem>;
            }
        }

        [Description("请款明细code"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string PaymentItemCode
        {
            get
            {
                return this.entityData.PaymentItemCode;
            }
            set
            {
                if (this.entityData.PaymentItemCode != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PaymentItemCode, this.entityData.PaymentItemCode);
                    this.entityData.PaymentItemCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PaymentItemCode, this.entityData.PaymentItemCode);
                    this.OnPropertyChanged("PaymentItemCode");
                }
            }
        }

        [Browsable(false), XmlIgnore, TiannuoPM.Entities.Bindable]
        public virtual PaymentItem PaymentItemCodeSource
        {
            get
            {
                return this._paymentItemCodeSource;
            }
            set
            {
                this._paymentItemCodeSource = value;
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("原币金额")]
        public virtual decimal? PayoutCash
        {
            get
            {
                return this.entityData.PayoutCash;
            }
            set
            {
                if (this.entityData.PayoutCash != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutCash, this.entityData.PayoutCash);
                    this.entityData.PayoutCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutCash, this.entityData.PayoutCash);
                    this.OnPropertyChanged("PayoutCash");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("付款code")]
        public virtual string PayoutCode
        {
            get
            {
                return this.entityData.PayoutCode;
            }
            set
            {
                if (this.entityData.PayoutCode != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutCode, this.entityData.PayoutCode);
                    this.entityData.PayoutCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutCode, this.entityData.PayoutCode);
                    this.OnPropertyChanged("PayoutCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
        public virtual Payout PayoutCodeSource
        {
            get
            {
                return this._payoutCodeSource;
            }
            set
            {
                this._payoutCodeSource = value;
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("付款汇率")]
        public virtual decimal? PayoutExchangeRate
        {
            get
            {
                return this.entityData.PayoutExchangeRate;
            }
            set
            {
                if (this.entityData.PayoutExchangeRate != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutExchangeRate, this.entityData.PayoutExchangeRate);
                    this.entityData.PayoutExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutExchangeRate, this.entityData.PayoutExchangeRate);
                    this.OnPropertyChanged("PayoutExchangeRate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("付款明细code"), DataObjectField(true, false, false, 50)]
        public virtual string PayoutItemCode
        {
            get
            {
                return this.entityData.PayoutItemCode;
            }
            set
            {
                if (this.entityData.PayoutItemCode != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutItemCode, this.entityData.PayoutItemCode);
                    this.entityData.PayoutItemCode = value;
                    this.EntityId.PayoutItemCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutItemCode, this.entityData.PayoutItemCode);
                    this.OnPropertyChanged("PayoutItemCode");
                }
            }
        }

        [DataObjectField(false, false, true), Description("付款金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? PayoutMoney
        {
            get
            {
                return this.entityData.PayoutMoney;
            }
            set
            {
                if (this.entityData.PayoutMoney != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutMoney, this.entityData.PayoutMoney);
                    this.entityData.PayoutMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutMoney, this.entityData.PayoutMoney);
                    this.OnPropertyChanged("PayoutMoney");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("付款币种")]
        public virtual string PayoutMoneyType
        {
            get
            {
                return this.entityData.PayoutMoneyType;
            }
            set
            {
                if (this.entityData.PayoutMoneyType != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.PayoutMoneyType, this.entityData.PayoutMoneyType);
                    this.entityData.PayoutMoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.PayoutMoneyType, this.entityData.PayoutMoneyType);
                    this.OnPropertyChanged("PayoutMoneyType");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("备注"), DataObjectField(false, false, true, 800)]
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
                    this.OnColumnChanging(PayoutItemColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
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

        [Description("科目"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string SubjectCode
        {
            get
            {
                return this.entityData.SubjectCode;
            }
            set
            {
                if (this.entityData.SubjectCode != value)
                {
                    this.OnColumnChanging(PayoutItemColumn.SubjectCode, this.entityData.SubjectCode);
                    this.entityData.SubjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutItemColumn.SubjectCode, this.entityData.SubjectCode);
                    this.OnPropertyChanged("SubjectCode");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "PayoutItemCode", "PayoutCode", "PaymentItemCode", "PayoutMoney", "SubjectCode", "Remark", "AlloType", "IsManualAlloc", "PayoutCash", "MoneyType", "ExchangeRate", "PayoutMoneyType", "PayoutExchangeRate" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "PayoutItem";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class PayoutItemEntityData : ICloneable
        {
            public string AlloType = null;
            public decimal? ExchangeRate = null;
            public int? IsManualAlloc = null;
            public string MoneyType = null;
            public string OriginalPayoutItemCode;
            public string PaymentItemCode = null;
            public decimal? PayoutCash = null;
            public string PayoutCode = null;
            public decimal? PayoutExchangeRate = null;
            public string PayoutItemCode;
            public decimal? PayoutMoney = null;
            public string PayoutMoneyType = null;
            public string Remark = null;
            public string SubjectCode = null;

            public object Clone()
            {
                PayoutItemBase.PayoutItemEntityData data = new PayoutItemBase.PayoutItemEntityData();
                data.PayoutItemCode = this.PayoutItemCode;
                data.OriginalPayoutItemCode = this.OriginalPayoutItemCode;
                data.PayoutCode = this.PayoutCode;
                data.PaymentItemCode = this.PaymentItemCode;
                data.PayoutMoney = this.PayoutMoney;
                data.SubjectCode = this.SubjectCode;
                data.Remark = this.Remark;
                data.AlloType = this.AlloType;
                data.IsManualAlloc = this.IsManualAlloc;
                data.PayoutCash = this.PayoutCash;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                data.PayoutMoneyType = this.PayoutMoneyType;
                data.PayoutExchangeRate = this.PayoutExchangeRate;
                return data;
            }
        }
    }
}

