namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class PayoutBase : EntityBase, IEntityId<PayoutKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private PayoutKey _entityId;
        private Project _projectCodeSource;
        private ISite _site;
        private PayoutEntityData backupData;
        private PayoutEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Payout> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event PayoutEventHandler ColumnChanged;

        [field: NonSerialized]
        public event PayoutEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public PayoutBase()
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new PayoutEntityData();
            this.backupData = null;
        }

        public PayoutBase(string payoutPayoutCode, string payoutPayoutID, string payoutProjectCode, string payoutPaymentCodes, DateTime? payoutPayoutDate, string payoutPaymentType, string payoutPayer, string payoutSupplyCode, string payoutBillNo, string payoutInvoNo, string payoutBankName, string payoutBankAccount, string payoutSubjectCode, decimal? payoutMoney, int? payoutStatus, string payoutInputPerson, DateTime? payoutInputDate, string payoutCheckPerson, DateTime? payoutCheckDate, string payoutCheckOpinion, string payoutSupplyName, string payoutRemark, int? payoutReceiptCount, string payoutGroupCode, int? payoutIsApportioned, decimal? payoutCash, string payoutMoneyType, decimal? payoutExchangeRate, string payoutVoucherNo, string payoutSubjectSetCode)
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new PayoutEntityData();
            this.backupData = null;
            this.PayoutCode = payoutPayoutCode;
            this.PayoutID = payoutPayoutID;
            this.ProjectCode = payoutProjectCode;
            this.PaymentCodes = payoutPaymentCodes;
            this.PayoutDate = payoutPayoutDate;
            this.PaymentType = payoutPaymentType;
            this.Payer = payoutPayer;
            this.SupplyCode = payoutSupplyCode;
            this.BillNo = payoutBillNo;
            this.InvoNo = payoutInvoNo;
            this.BankName = payoutBankName;
            this.BankAccount = payoutBankAccount;
            this.SubjectCode = payoutSubjectCode;
            this.Money = payoutMoney;
            this.Status = payoutStatus;
            this.InputPerson = payoutInputPerson;
            this.InputDate = payoutInputDate;
            this.CheckPerson = payoutCheckPerson;
            this.CheckDate = payoutCheckDate;
            this.CheckOpinion = payoutCheckOpinion;
            this.SupplyName = payoutSupplyName;
            this.Remark = payoutRemark;
            this.ReceiptCount = payoutReceiptCount;
            this.GroupCode = payoutGroupCode;
            this.IsApportioned = payoutIsApportioned;
            this.Cash = payoutCash;
            this.MoneyType = payoutMoneyType;
            this.ExchangeRate = payoutExchangeRate;
            this.VoucherNo = payoutVoucherNo;
            this.SubjectSetCode = payoutSubjectSetCode;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "PayoutCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayoutCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PayoutID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentCodes", 500));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Payer", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SupplyCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BillNo", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InvoNo", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BankName", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BankAccount", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SubjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("InputPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckOpinion", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SupplyName", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("GroupCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("VoucherNo", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SubjectSetCode", 50));
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

        public virtual Payout Copy()
        {
            Payout payout = new Payout();
            payout.PayoutCode = this.PayoutCode;
            payout.OriginalPayoutCode = this.OriginalPayoutCode;
            payout.PayoutID = this.PayoutID;
            payout.ProjectCode = this.ProjectCode;
            payout.PaymentCodes = this.PaymentCodes;
            payout.PayoutDate = this.PayoutDate;
            payout.PaymentType = this.PaymentType;
            payout.Payer = this.Payer;
            payout.SupplyCode = this.SupplyCode;
            payout.BillNo = this.BillNo;
            payout.InvoNo = this.InvoNo;
            payout.BankName = this.BankName;
            payout.BankAccount = this.BankAccount;
            payout.SubjectCode = this.SubjectCode;
            payout.Money = this.Money;
            payout.Status = this.Status;
            payout.InputPerson = this.InputPerson;
            payout.InputDate = this.InputDate;
            payout.CheckPerson = this.CheckPerson;
            payout.CheckDate = this.CheckDate;
            payout.CheckOpinion = this.CheckOpinion;
            payout.SupplyName = this.SupplyName;
            payout.Remark = this.Remark;
            payout.ReceiptCount = this.ReceiptCount;
            payout.GroupCode = this.GroupCode;
            payout.IsApportioned = this.IsApportioned;
            payout.Cash = this.Cash;
            payout.MoneyType = this.MoneyType;
            payout.ExchangeRate = this.ExchangeRate;
            payout.VoucherNo = this.VoucherNo;
            payout.SubjectSetCode = this.SubjectSetCode;
            payout.AcceptChanges();
            return payout;
        }

        public static Payout CreatePayout(string payoutPayoutCode, string payoutPayoutID, string payoutProjectCode, string payoutPaymentCodes, DateTime? payoutPayoutDate, string payoutPaymentType, string payoutPayer, string payoutSupplyCode, string payoutBillNo, string payoutInvoNo, string payoutBankName, string payoutBankAccount, string payoutSubjectCode, decimal? payoutMoney, int? payoutStatus, string payoutInputPerson, DateTime? payoutInputDate, string payoutCheckPerson, DateTime? payoutCheckDate, string payoutCheckOpinion, string payoutSupplyName, string payoutRemark, int? payoutReceiptCount, string payoutGroupCode, int? payoutIsApportioned, decimal? payoutCash, string payoutMoneyType, decimal? payoutExchangeRate, string payoutVoucherNo, string payoutSubjectSetCode)
        {
            Payout payout = new Payout();
            payout.PayoutCode = payoutPayoutCode;
            payout.PayoutID = payoutPayoutID;
            payout.ProjectCode = payoutProjectCode;
            payout.PaymentCodes = payoutPaymentCodes;
            payout.PayoutDate = payoutPayoutDate;
            payout.PaymentType = payoutPaymentType;
            payout.Payer = payoutPayer;
            payout.SupplyCode = payoutSupplyCode;
            payout.BillNo = payoutBillNo;
            payout.InvoNo = payoutInvoNo;
            payout.BankName = payoutBankName;
            payout.BankAccount = payoutBankAccount;
            payout.SubjectCode = payoutSubjectCode;
            payout.Money = payoutMoney;
            payout.Status = payoutStatus;
            payout.InputPerson = payoutInputPerson;
            payout.InputDate = payoutInputDate;
            payout.CheckPerson = payoutCheckPerson;
            payout.CheckDate = payoutCheckDate;
            payout.CheckOpinion = payoutCheckOpinion;
            payout.SupplyName = payoutSupplyName;
            payout.Remark = payoutRemark;
            payout.ReceiptCount = payoutReceiptCount;
            payout.GroupCode = payoutGroupCode;
            payout.IsApportioned = payoutIsApportioned;
            payout.Cash = payoutCash;
            payout.MoneyType = payoutMoneyType;
            payout.ExchangeRate = payoutExchangeRate;
            payout.VoucherNo = payoutVoucherNo;
            payout.SubjectSetCode = payoutSubjectSetCode;
            return payout;
        }

        public virtual Payout DeepCopy()
        {
            return EntityHelper.Clone<Payout>(this as Payout);
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

        public virtual bool Equals(PayoutBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(PayoutBase Object1, PayoutBase Object2)
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
            if (Object1.PayoutCode != Object2.PayoutCode)
            {
                flag = false;
            }
            if ((Object1.PayoutID != null) && (Object2.PayoutID != null))
            {
                if (Object1.PayoutID != Object2.PayoutID)
                {
                    flag = false;
                }
            }
            else if ((Object1.PayoutID == null) ^ (Object2.PayoutID == null))
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
            if ((Object1.PaymentCodes != null) && (Object2.PaymentCodes != null))
            {
                if (Object1.PaymentCodes != Object2.PaymentCodes)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentCodes == null) ^ (Object2.PaymentCodes == null))
            {
                flag = false;
            }
            if (Object1.PayoutDate.HasValue && Object2.PayoutDate.HasValue)
            {
                if (Object1.PayoutDate != Object2.PayoutDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PayoutDate.HasValue ^ !Object2.PayoutDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.PaymentType != null) && (Object2.PaymentType != null))
            {
                if (Object1.PaymentType != Object2.PaymentType)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentType == null) ^ (Object2.PaymentType == null))
            {
                flag = false;
            }
            if ((Object1.Payer != null) && (Object2.Payer != null))
            {
                if (Object1.Payer != Object2.Payer)
                {
                    flag = false;
                }
            }
            else if ((Object1.Payer == null) ^ (Object2.Payer == null))
            {
                flag = false;
            }
            if ((Object1.SupplyCode != null) && (Object2.SupplyCode != null))
            {
                if (Object1.SupplyCode != Object2.SupplyCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SupplyCode == null) ^ (Object2.SupplyCode == null))
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
            if ((Object1.InvoNo != null) && (Object2.InvoNo != null))
            {
                if (Object1.InvoNo != Object2.InvoNo)
                {
                    flag = false;
                }
            }
            else if ((Object1.InvoNo == null) ^ (Object2.InvoNo == null))
            {
                flag = false;
            }
            if ((Object1.BankName != null) && (Object2.BankName != null))
            {
                if (Object1.BankName != Object2.BankName)
                {
                    flag = false;
                }
            }
            else if ((Object1.BankName == null) ^ (Object2.BankName == null))
            {
                flag = false;
            }
            if ((Object1.BankAccount != null) && (Object2.BankAccount != null))
            {
                if (Object1.BankAccount != Object2.BankAccount)
                {
                    flag = false;
                }
            }
            else if ((Object1.BankAccount == null) ^ (Object2.BankAccount == null))
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
            }
            else if (!Object1.CheckDate.HasValue ^ !Object2.CheckDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.CheckOpinion != null) && (Object2.CheckOpinion != null))
            {
                if (Object1.CheckOpinion != Object2.CheckOpinion)
                {
                    flag = false;
                }
            }
            else if ((Object1.CheckOpinion == null) ^ (Object2.CheckOpinion == null))
            {
                flag = false;
            }
            if ((Object1.SupplyName != null) && (Object2.SupplyName != null))
            {
                if (Object1.SupplyName != Object2.SupplyName)
                {
                    flag = false;
                }
            }
            else if ((Object1.SupplyName == null) ^ (Object2.SupplyName == null))
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
            if (Object1.ReceiptCount.HasValue && Object2.ReceiptCount.HasValue)
            {
                if (Object1.ReceiptCount != Object2.ReceiptCount)
                {
                    flag = false;
                }
            }
            else if (!Object1.ReceiptCount.HasValue ^ !Object2.ReceiptCount.HasValue)
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
            if (Object1.IsApportioned.HasValue && Object2.IsApportioned.HasValue)
            {
                if (Object1.IsApportioned != Object2.IsApportioned)
                {
                    flag = false;
                }
            }
            else if (!Object1.IsApportioned.HasValue ^ !Object2.IsApportioned.HasValue)
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
            if ((Object1.VoucherNo != null) && (Object2.VoucherNo != null))
            {
                if (Object1.VoucherNo != Object2.VoucherNo)
                {
                    flag = false;
                }
            }
            else if ((Object1.VoucherNo == null) ^ (Object2.VoucherNo == null))
            {
                flag = false;
            }
            if ((Object1.SubjectSetCode != null) && (Object2.SubjectSetCode != null))
            {
                if (Object1.SubjectSetCode != Object2.SubjectSetCode)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.SubjectSetCode == null) ^ (Object2.SubjectSetCode == null))
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

        public void OnColumnChanged(PayoutColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(PayoutColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                PayoutEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new PayoutEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(PayoutColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(PayoutColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                PayoutEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new PayoutEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Payout);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as PayoutEntityData;
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
                    this.parentCollection.Remove((Payout) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{31}{30}- PayoutCode: {0}{30}- PayoutID: {1}{30}- ProjectCode: {2}{30}- PaymentCodes: {3}{30}- PayoutDate: {4}{30}- PaymentType: {5}{30}- Payer: {6}{30}- SupplyCode: {7}{30}- BillNo: {8}{30}- InvoNo: {9}{30}- BankName: {10}{30}- BankAccount: {11}{30}- SubjectCode: {12}{30}- Money: {13}{30}- Status: {14}{30}- InputPerson: {15}{30}- InputDate: {16}{30}- CheckPerson: {17}{30}- CheckDate: {18}{30}- CheckOpinion: {19}{30}- SupplyName: {20}{30}- Remark: {21}{30}- ReceiptCount: {22}{30}- GroupCode: {23}{30}- IsApportioned: {24}{30}- Cash: {25}{30}- MoneyType: {26}{30}- ExchangeRate: {27}{30}- VoucherNo: {28}{30}- SubjectSetCode: {29}{30}", new object[] { 
                this.PayoutCode, (this.PayoutID == null) ? string.Empty : this.PayoutID.ToString(), (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.PaymentCodes == null) ? string.Empty : this.PaymentCodes.ToString(), !this.PayoutDate.HasValue ? string.Empty : this.PayoutDate.ToString(), (this.PaymentType == null) ? string.Empty : this.PaymentType.ToString(), (this.Payer == null) ? string.Empty : this.Payer.ToString(), (this.SupplyCode == null) ? string.Empty : this.SupplyCode.ToString(), (this.BillNo == null) ? string.Empty : this.BillNo.ToString(), (this.InvoNo == null) ? string.Empty : this.InvoNo.ToString(), (this.BankName == null) ? string.Empty : this.BankName.ToString(), (this.BankAccount == null) ? string.Empty : this.BankAccount.ToString(), (this.SubjectCode == null) ? string.Empty : this.SubjectCode.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), (this.InputPerson == null) ? string.Empty : this.InputPerson.ToString(), 
                !this.InputDate.HasValue ? string.Empty : this.InputDate.ToString(), (this.CheckPerson == null) ? string.Empty : this.CheckPerson.ToString(), !this.CheckDate.HasValue ? string.Empty : this.CheckDate.ToString(), (this.CheckOpinion == null) ? string.Empty : this.CheckOpinion.ToString(), (this.SupplyName == null) ? string.Empty : this.SupplyName.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), !this.ReceiptCount.HasValue ? string.Empty : this.ReceiptCount.ToString(), (this.GroupCode == null) ? string.Empty : this.GroupCode.ToString(), !this.IsApportioned.HasValue ? string.Empty : this.IsApportioned.ToString(), !this.Cash.HasValue ? string.Empty : this.Cash.ToString(), (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), (this.VoucherNo == null) ? string.Empty : this.VoucherNo.ToString(), (this.SubjectSetCode == null) ? string.Empty : this.SubjectSetCode.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [TiannuoPM.Entities.Bindable, Description("银行帐号"), DataObjectField(false, false, true, 50)]
        public virtual string BankAccount
        {
            get
            {
                return this.entityData.BankAccount;
            }
            set
            {
                if (this.entityData.BankAccount != value)
                {
                    this.OnColumnChanging(PayoutColumn.BankAccount, this.entityData.BankAccount);
                    this.entityData.BankAccount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.BankAccount, this.entityData.BankAccount);
                    this.OnPropertyChanged("BankAccount");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("银行"), DataObjectField(false, false, true, 100)]
        public virtual string BankName
        {
            get
            {
                return this.entityData.BankName;
            }
            set
            {
                if (this.entityData.BankName != value)
                {
                    this.OnColumnChanging(PayoutColumn.BankName, this.entityData.BankName);
                    this.entityData.BankName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.BankName, this.entityData.BankName);
                    this.OnPropertyChanged("BankName");
                }
            }
        }

        [Description("支票号"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(PayoutColumn.BillNo, this.entityData.BillNo);
                    this.entityData.BillNo = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.BillNo, this.entityData.BillNo);
                    this.OnPropertyChanged("BillNo");
                }
            }
        }

        [Description("原币金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.Cash, this.entityData.Cash);
                    this.entityData.Cash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.Cash, this.entityData.Cash);
                    this.OnPropertyChanged("Cash");
                }
            }
        }

        [DataObjectField(false, false, true), Description("审核日期"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.CheckDate, this.entityData.CheckDate);
                    this.entityData.CheckDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.CheckDate, this.entityData.CheckDate);
                    this.OnPropertyChanged("CheckDate");
                }
            }
        }

        [Description("审核意见"), DataObjectField(false, false, true, 800), TiannuoPM.Entities.Bindable]
        public virtual string CheckOpinion
        {
            get
            {
                return this.entityData.CheckOpinion;
            }
            set
            {
                if (this.entityData.CheckOpinion != value)
                {
                    this.OnColumnChanging(PayoutColumn.CheckOpinion, this.entityData.CheckOpinion);
                    this.entityData.CheckOpinion = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.CheckOpinion, this.entityData.CheckOpinion);
                    this.OnPropertyChanged("CheckOpinion");
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
                    this.OnColumnChanging(PayoutColumn.CheckPerson, this.entityData.CheckPerson);
                    this.entityData.CheckPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.CheckPerson, this.entityData.CheckPerson);
                    this.OnPropertyChanged("CheckPerson");
                }
            }
        }

        [XmlIgnore]
        public PayoutKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new PayoutKey(this);
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
                    this.entityTrackingKey = "Payout" + this.PayoutCode.ToString();
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

        [TiannuoPM.Entities.Bindable, Description("汇率"), DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(PayoutColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("付款单类型")]
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
                    this.OnColumnChanging(PayoutColumn.GroupCode, this.entityData.GroupCode);
                    this.entityData.GroupCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.GroupCode, this.entityData.GroupCode);
                    this.OnPropertyChanged("GroupCode");
                }
            }
        }

        [DataObjectField(false, false, true), Description("录入日期"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.InputDate, this.entityData.InputDate);
                    this.entityData.InputDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.InputDate, this.entityData.InputDate);
                    this.OnPropertyChanged("InputDate");
                }
            }
        }

        [Description("录入人"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.InputPerson, this.entityData.InputPerson);
                    this.entityData.InputPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.InputPerson, this.entityData.InputPerson);
                    this.OnPropertyChanged("InputPerson");
                }
            }
        }

        [Description("票据号"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string InvoNo
        {
            get
            {
                return this.entityData.InvoNo;
            }
            set
            {
                if (this.entityData.InvoNo != value)
                {
                    this.OnColumnChanging(PayoutColumn.InvoNo, this.entityData.InvoNo);
                    this.entityData.InvoNo = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.InvoNo, this.entityData.InvoNo);
                    this.OnPropertyChanged("InvoNo");
                }
            }
        }

        [DataObjectField(false, false, true), Description("x"), TiannuoPM.Entities.Bindable]
        public virtual int? IsApportioned
        {
            get
            {
                return this.entityData.IsApportioned;
            }
            set
            {
                if (this.entityData.IsApportioned != value)
                {
                    this.OnColumnChanging(PayoutColumn.IsApportioned, this.entityData.IsApportioned);
                    this.entityData.IsApportioned = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.IsApportioned, this.entityData.IsApportioned);
                    this.OnPropertyChanged("IsApportioned");
                }
            }
        }

        [Description("付款总额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Description("币种"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(PayoutColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalPayoutCode
        {
            get
            {
                return this.entityData.OriginalPayoutCode;
            }
            set
            {
                this.entityData.OriginalPayoutCode = value;
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
                this.parentCollection = value as TList<Payout>;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("受款人")]
        public virtual string Payer
        {
            get
            {
                return this.entityData.Payer;
            }
            set
            {
                if (this.entityData.Payer != value)
                {
                    this.OnColumnChanging(PayoutColumn.Payer, this.entityData.Payer);
                    this.entityData.Payer = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.Payer, this.entityData.Payer);
                    this.OnPropertyChanged("Payer");
                }
            }
        }

        [DataObjectField(false, false, true, 500), TiannuoPM.Entities.Bindable, Description("请款单code")]
        public virtual string PaymentCodes
        {
            get
            {
                return this.entityData.PaymentCodes;
            }
            set
            {
                if (this.entityData.PaymentCodes != value)
                {
                    this.OnColumnChanging(PayoutColumn.PaymentCodes, this.entityData.PaymentCodes);
                    this.entityData.PaymentCodes = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.PaymentCodes, this.entityData.PaymentCodes);
                    this.OnPropertyChanged("PaymentCodes");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("付款方式"), TiannuoPM.Entities.Bindable]
        public virtual string PaymentType
        {
            get
            {
                return this.entityData.PaymentType;
            }
            set
            {
                if (this.entityData.PaymentType != value)
                {
                    this.OnColumnChanging(PayoutColumn.PaymentType, this.entityData.PaymentType);
                    this.entityData.PaymentType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.PaymentType, this.entityData.PaymentType);
                    this.OnPropertyChanged("PaymentType");
                }
            }
        }

        [DataObjectField(true, false, false, 50), Description("付款code"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.PayoutCode, this.entityData.PayoutCode);
                    this.entityData.PayoutCode = value;
                    this.EntityId.PayoutCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.PayoutCode, this.entityData.PayoutCode);
                    this.OnPropertyChanged("PayoutCode");
                }
            }
        }

        [Description("最后付款日"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? PayoutDate
        {
            get
            {
                return this.entityData.PayoutDate;
            }
            set
            {
                if (this.entityData.PayoutDate != value)
                {
                    this.OnColumnChanging(PayoutColumn.PayoutDate, this.entityData.PayoutDate);
                    this.entityData.PayoutDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.PayoutDate, this.entityData.PayoutDate);
                    this.OnPropertyChanged("PayoutDate");
                }
            }
        }

        [Description("付款单号"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string PayoutID
        {
            get
            {
                return this.entityData.PayoutID;
            }
            set
            {
                if (this.entityData.PayoutID != value)
                {
                    this.OnColumnChanging(PayoutColumn.PayoutID, this.entityData.PayoutID);
                    this.entityData.PayoutID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.PayoutID, this.entityData.PayoutID);
                    this.OnPropertyChanged("PayoutID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<PayoutItem> PayoutItemCollection
        {
            get
            {
                return this.entityData.PayoutItemCollection;
            }
            set
            {
                this.entityData.PayoutItemCollection = value;
            }
        }

        [DataObjectField(false, false, true, 50), Description("项目code"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
        public virtual Project ProjectCodeSource
        {
            get
            {
                return this._projectCodeSource;
            }
            set
            {
                this._projectCodeSource = value;
            }
        }

        [Description("单据张数"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual int? ReceiptCount
        {
            get
            {
                return this.entityData.ReceiptCount;
            }
            set
            {
                if (this.entityData.ReceiptCount != value)
                {
                    this.OnColumnChanging(PayoutColumn.ReceiptCount, this.entityData.ReceiptCount);
                    this.entityData.ReceiptCount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.ReceiptCount, this.entityData.ReceiptCount);
                    this.OnPropertyChanged("ReceiptCount");
                }
            }
        }

        [Description("备注"), DataObjectField(false, false, true, 800), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
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

        [DataObjectField(false, false, true), Description("状态"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PayoutColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("银行科目")]
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
                    this.OnColumnChanging(PayoutColumn.SubjectCode, this.entityData.SubjectCode);
                    this.entityData.SubjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.SubjectCode, this.entityData.SubjectCode);
                    this.OnPropertyChanged("SubjectCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("帐套编号"), TiannuoPM.Entities.Bindable]
        public virtual string SubjectSetCode
        {
            get
            {
                return this.entityData.SubjectSetCode;
            }
            set
            {
                if (this.entityData.SubjectSetCode != value)
                {
                    this.OnColumnChanging(PayoutColumn.SubjectSetCode, this.entityData.SubjectSetCode);
                    this.entityData.SubjectSetCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.SubjectSetCode, this.entityData.SubjectSetCode);
                    this.OnPropertyChanged("SubjectSetCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("收款单位编号"), DataObjectField(false, false, true, 50)]
        public virtual string SupplyCode
        {
            get
            {
                return this.entityData.SupplyCode;
            }
            set
            {
                if (this.entityData.SupplyCode != value)
                {
                    this.OnColumnChanging(PayoutColumn.SupplyCode, this.entityData.SupplyCode);
                    this.entityData.SupplyCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.SupplyCode, this.entityData.SupplyCode);
                    this.OnPropertyChanged("SupplyCode");
                }
            }
        }

        [DataObjectField(false, false, true, 200), Description("收款单位名称"), TiannuoPM.Entities.Bindable]
        public virtual string SupplyName
        {
            get
            {
                return this.entityData.SupplyName;
            }
            set
            {
                if (this.entityData.SupplyName != value)
                {
                    this.OnColumnChanging(PayoutColumn.SupplyName, this.entityData.SupplyName);
                    this.entityData.SupplyName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.SupplyName, this.entityData.SupplyName);
                    this.OnPropertyChanged("SupplyName");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "PayoutCode", "PayoutID", "ProjectCode", "PaymentCodes", "PayoutDate", "PaymentType", "Payer", "SupplyCode", "BillNo", "InvoNo", "BankName", "BankAccount", "SubjectCode", "Money", "Status", "InputPerson", 
                    "InputDate", "CheckPerson", "CheckDate", "CheckOpinion", "SupplyName", "Remark", "ReceiptCount", "GroupCode", "IsApportioned", "Cash", "MoneyType", "ExchangeRate", "VoucherNo", "SubjectSetCode"
                 };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "Payout";
            }
        }

        [DataObjectField(false, false, true, 50), Description("财务系统凭证号"), TiannuoPM.Entities.Bindable]
        public virtual string VoucherNo
        {
            get
            {
                return this.entityData.VoucherNo;
            }
            set
            {
                if (this.entityData.VoucherNo != value)
                {
                    this.OnColumnChanging(PayoutColumn.VoucherNo, this.entityData.VoucherNo);
                    this.entityData.VoucherNo = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PayoutColumn.VoucherNo, this.entityData.VoucherNo);
                    this.OnPropertyChanged("VoucherNo");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class PayoutEntityData : ICloneable
        {
            public string BankAccount = null;
            public string BankName = null;
            public string BillNo = null;
            public decimal? Cash = null;
            public DateTime? CheckDate = null;
            public string CheckOpinion = null;
            public string CheckPerson = null;
            public decimal? ExchangeRate = null;
            public string GroupCode = null;
            public DateTime? InputDate = null;
            public string InputPerson = null;
            public string InvoNo = null;
            public int? IsApportioned = null;
            public decimal? Money = null;
            public string MoneyType = null;
            public string OriginalPayoutCode;
            public string Payer = null;
            public string PaymentCodes = null;
            public string PaymentType = null;
            public string PayoutCode;
            public DateTime? PayoutDate = null;
            public string PayoutID = null;
            private TList<PayoutItem> payoutItemPayoutCode;
            public string ProjectCode = null;
            public int? ReceiptCount = null;
            public string Remark = null;
            public int? Status = null;
            public string SubjectCode = null;
            public string SubjectSetCode = null;
            public string SupplyCode = null;
            public string SupplyName = null;
            public string VoucherNo = null;

            public object Clone()
            {
                PayoutBase.PayoutEntityData data = new PayoutBase.PayoutEntityData();
                data.PayoutCode = this.PayoutCode;
                data.OriginalPayoutCode = this.OriginalPayoutCode;
                data.PayoutID = this.PayoutID;
                data.ProjectCode = this.ProjectCode;
                data.PaymentCodes = this.PaymentCodes;
                data.PayoutDate = this.PayoutDate;
                data.PaymentType = this.PaymentType;
                data.Payer = this.Payer;
                data.SupplyCode = this.SupplyCode;
                data.BillNo = this.BillNo;
                data.InvoNo = this.InvoNo;
                data.BankName = this.BankName;
                data.BankAccount = this.BankAccount;
                data.SubjectCode = this.SubjectCode;
                data.Money = this.Money;
                data.Status = this.Status;
                data.InputPerson = this.InputPerson;
                data.InputDate = this.InputDate;
                data.CheckPerson = this.CheckPerson;
                data.CheckDate = this.CheckDate;
                data.CheckOpinion = this.CheckOpinion;
                data.SupplyName = this.SupplyName;
                data.Remark = this.Remark;
                data.ReceiptCount = this.ReceiptCount;
                data.GroupCode = this.GroupCode;
                data.IsApportioned = this.IsApportioned;
                data.Cash = this.Cash;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                data.VoucherNo = this.VoucherNo;
                data.SubjectSetCode = this.SubjectSetCode;
                return data;
            }

            public TList<PayoutItem> PayoutItemCollection
            {
                get
                {
                    if (this.payoutItemPayoutCode == null)
                    {
                        this.payoutItemPayoutCode = new TList<PayoutItem>();
                    }
                    return this.payoutItemPayoutCode;
                }
                set
                {
                    this.payoutItemPayoutCode = value;
                }
            }
        }
    }
}

