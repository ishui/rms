namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class PaymentBase : EntityBase, IEntityId<PaymentKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private Contract _contractCodeSource;
        private PaymentKey _entityId;
        private Project _projectCodeSource;
        private ISite _site;
        private PaymentEntityData backupData;
        private PaymentEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Payment> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event PaymentEventHandler ColumnChanged;

        [field: NonSerialized]
        public event PaymentEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public PaymentBase()
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new PaymentEntityData();
            this.backupData = null;
        }

        public PaymentBase(string paymentPaymentCode, string paymentPaymentTitle, string paymentPaymentID, string paymentVoucherID, int? paymentRecieptCount, string paymentProjectCode, string paymentApplyPerson, DateTime? paymentApplyDate, string paymentAccountant, DateTime? paymentAccountDate, string paymentPayer, DateTime? paymentPayDate, string paymentPurpose, decimal? paymentMoney, string paymentCheckPerson, DateTime? paymentCheckDate, string paymentCheckOpinion, int? paymentIsContract, string paymentContractCode, int? paymentStatus, string paymentWBSCode, int? paymentIsApportion, string paymentSupplyCode, string paymentUnitCode, string paymentSupplyName, string paymentRemark, decimal? paymentOldMoney, string paymentGroupCode, string paymentBankName, string paymentBankAccount, string paymentOtherAttachMent, int? paymentPayType, int? paymentIssue, string paymentIssueMode, DateTime? paymentFactPayDate, decimal? paymentTotalPayMoney, decimal? paymentAHMoney, decimal? paymentAPMoney, decimal? paymentUPMoney, decimal? paymentSupplierApplyMoney, decimal? paymentMaxPayMoney, decimal? paymentPlanPayMoney, decimal? paymentContractMoney, decimal? paymentAdjustedContractMoney, decimal? paymentPayoutMoney, decimal? paymentAHCash, decimal? paymentAPCash, decimal? paymentUPCash, decimal? paymentPayoutCash, decimal? paymentAHCash0, decimal? paymentAHCash1, decimal? paymentAHCash2, decimal? paymentAHCash3, decimal? paymentAHCash4, decimal? paymentAHCash5, decimal? paymentAHCash6, decimal? paymentAHCash7, decimal? paymentAHCash8, decimal? paymentAHCash9, string paymentPaymentName, decimal? paymentTotalViseChangeMoney, string paymentSumCode, string paymentPaymentCodition, string paymentCheckRemark)
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._contractCodeSource = null;
            this._site = null;
            this.entityData = new PaymentEntityData();
            this.backupData = null;
            this.PaymentCode = paymentPaymentCode;
            this.PaymentTitle = paymentPaymentTitle;
            this.PaymentID = paymentPaymentID;
            this.VoucherID = paymentVoucherID;
            this.RecieptCount = paymentRecieptCount;
            this.ProjectCode = paymentProjectCode;
            this.ApplyPerson = paymentApplyPerson;
            this.ApplyDate = paymentApplyDate;
            this.Accountant = paymentAccountant;
            this.AccountDate = paymentAccountDate;
            this.Payer = paymentPayer;
            this.PayDate = paymentPayDate;
            this.Purpose = paymentPurpose;
            this.Money = paymentMoney;
            this.CheckPerson = paymentCheckPerson;
            this.CheckDate = paymentCheckDate;
            this.CheckOpinion = paymentCheckOpinion;
            this.IsContract = paymentIsContract;
            this.ContractCode = paymentContractCode;
            this.Status = paymentStatus;
            this.WBSCode = paymentWBSCode;
            this.IsApportion = paymentIsApportion;
            this.SupplyCode = paymentSupplyCode;
            this.UnitCode = paymentUnitCode;
            this.SupplyName = paymentSupplyName;
            this.Remark = paymentRemark;
            this.OldMoney = paymentOldMoney;
            this.GroupCode = paymentGroupCode;
            this.BankName = paymentBankName;
            this.BankAccount = paymentBankAccount;
            this.OtherAttachMent = paymentOtherAttachMent;
            this.PayType = paymentPayType;
            this.Issue = paymentIssue;
            this.IssueMode = paymentIssueMode;
            this.FactPayDate = paymentFactPayDate;
            this.TotalPayMoney = paymentTotalPayMoney;
            this.AHMoney = paymentAHMoney;
            this.APMoney = paymentAPMoney;
            this.UPMoney = paymentUPMoney;
            this.SupplierApplyMoney = paymentSupplierApplyMoney;
            this.MaxPayMoney = paymentMaxPayMoney;
            this.PlanPayMoney = paymentPlanPayMoney;
            this.ContractMoney = paymentContractMoney;
            this.AdjustedContractMoney = paymentAdjustedContractMoney;
            this.PayoutMoney = paymentPayoutMoney;
            this.AHCash = paymentAHCash;
            this.APCash = paymentAPCash;
            this.UPCash = paymentUPCash;
            this.PayoutCash = paymentPayoutCash;
            this.AHCash0 = paymentAHCash0;
            this.AHCash1 = paymentAHCash1;
            this.AHCash2 = paymentAHCash2;
            this.AHCash3 = paymentAHCash3;
            this.AHCash4 = paymentAHCash4;
            this.AHCash5 = paymentAHCash5;
            this.AHCash6 = paymentAHCash6;
            this.AHCash7 = paymentAHCash7;
            this.AHCash8 = paymentAHCash8;
            this.AHCash9 = paymentAHCash9;
            this.PaymentName = paymentPaymentName;
            this.TotalViseChangeMoney = paymentTotalViseChangeMoney;
            this.SumCode = paymentSumCode;
            this.PaymentCodition = paymentPaymentCodition;
            this.CheckRemark = paymentCheckRemark;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "PaymentCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentTitle", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("VoucherID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ApplyPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Accountant", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Payer", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Purpose", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckOpinion", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("WBSCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SupplyCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UnitCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SupplyName", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("GroupCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BankName", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BankAccount", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("OtherAttachMent", 0x3e8));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("IssueMode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentName", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SumCode", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentCodition", 100));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckRemark", 0x3e8));
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

        public virtual Payment Copy()
        {
            Payment payment = new Payment();
            payment.PaymentCode = this.PaymentCode;
            payment.OriginalPaymentCode = this.OriginalPaymentCode;
            payment.PaymentTitle = this.PaymentTitle;
            payment.PaymentID = this.PaymentID;
            payment.VoucherID = this.VoucherID;
            payment.RecieptCount = this.RecieptCount;
            payment.ProjectCode = this.ProjectCode;
            payment.ApplyPerson = this.ApplyPerson;
            payment.ApplyDate = this.ApplyDate;
            payment.Accountant = this.Accountant;
            payment.AccountDate = this.AccountDate;
            payment.Payer = this.Payer;
            payment.PayDate = this.PayDate;
            payment.Purpose = this.Purpose;
            payment.Money = this.Money;
            payment.CheckPerson = this.CheckPerson;
            payment.CheckDate = this.CheckDate;
            payment.CheckOpinion = this.CheckOpinion;
            payment.IsContract = this.IsContract;
            payment.ContractCode = this.ContractCode;
            payment.Status = this.Status;
            payment.WBSCode = this.WBSCode;
            payment.IsApportion = this.IsApportion;
            payment.SupplyCode = this.SupplyCode;
            payment.UnitCode = this.UnitCode;
            payment.SupplyName = this.SupplyName;
            payment.Remark = this.Remark;
            payment.OldMoney = this.OldMoney;
            payment.GroupCode = this.GroupCode;
            payment.BankName = this.BankName;
            payment.BankAccount = this.BankAccount;
            payment.OtherAttachMent = this.OtherAttachMent;
            payment.PayType = this.PayType;
            payment.Issue = this.Issue;
            payment.IssueMode = this.IssueMode;
            payment.FactPayDate = this.FactPayDate;
            payment.TotalPayMoney = this.TotalPayMoney;
            payment.AHMoney = this.AHMoney;
            payment.APMoney = this.APMoney;
            payment.UPMoney = this.UPMoney;
            payment.SupplierApplyMoney = this.SupplierApplyMoney;
            payment.MaxPayMoney = this.MaxPayMoney;
            payment.PlanPayMoney = this.PlanPayMoney;
            payment.ContractMoney = this.ContractMoney;
            payment.AdjustedContractMoney = this.AdjustedContractMoney;
            payment.PayoutMoney = this.PayoutMoney;
            payment.AHCash = this.AHCash;
            payment.APCash = this.APCash;
            payment.UPCash = this.UPCash;
            payment.PayoutCash = this.PayoutCash;
            payment.AHCash0 = this.AHCash0;
            payment.AHCash1 = this.AHCash1;
            payment.AHCash2 = this.AHCash2;
            payment.AHCash3 = this.AHCash3;
            payment.AHCash4 = this.AHCash4;
            payment.AHCash5 = this.AHCash5;
            payment.AHCash6 = this.AHCash6;
            payment.AHCash7 = this.AHCash7;
            payment.AHCash8 = this.AHCash8;
            payment.AHCash9 = this.AHCash9;
            payment.PaymentName = this.PaymentName;
            payment.TotalViseChangeMoney = this.TotalViseChangeMoney;
            payment.SumCode = this.SumCode;
            payment.PaymentCodition = this.PaymentCodition;
            payment.CheckRemark = this.CheckRemark;
            payment.AcceptChanges();
            return payment;
        }

        public static Payment CreatePayment(string paymentPaymentCode, string paymentPaymentTitle, string paymentPaymentID, string paymentVoucherID, int? paymentRecieptCount, string paymentProjectCode, string paymentApplyPerson, DateTime? paymentApplyDate, string paymentAccountant, DateTime? paymentAccountDate, string paymentPayer, DateTime? paymentPayDate, string paymentPurpose, decimal? paymentMoney, string paymentCheckPerson, DateTime? paymentCheckDate, string paymentCheckOpinion, int? paymentIsContract, string paymentContractCode, int? paymentStatus, string paymentWBSCode, int? paymentIsApportion, string paymentSupplyCode, string paymentUnitCode, string paymentSupplyName, string paymentRemark, decimal? paymentOldMoney, string paymentGroupCode, string paymentBankName, string paymentBankAccount, string paymentOtherAttachMent, int? paymentPayType, int? paymentIssue, string paymentIssueMode, DateTime? paymentFactPayDate, decimal? paymentTotalPayMoney, decimal? paymentAHMoney, decimal? paymentAPMoney, decimal? paymentUPMoney, decimal? paymentSupplierApplyMoney, decimal? paymentMaxPayMoney, decimal? paymentPlanPayMoney, decimal? paymentContractMoney, decimal? paymentAdjustedContractMoney, decimal? paymentPayoutMoney, decimal? paymentAHCash, decimal? paymentAPCash, decimal? paymentUPCash, decimal? paymentPayoutCash, decimal? paymentAHCash0, decimal? paymentAHCash1, decimal? paymentAHCash2, decimal? paymentAHCash3, decimal? paymentAHCash4, decimal? paymentAHCash5, decimal? paymentAHCash6, decimal? paymentAHCash7, decimal? paymentAHCash8, decimal? paymentAHCash9, string paymentPaymentName, decimal? paymentTotalViseChangeMoney, string paymentSumCode, string paymentPaymentCodition, string paymentCheckRemark)
        {
            Payment payment = new Payment();
            payment.PaymentCode = paymentPaymentCode;
            payment.PaymentTitle = paymentPaymentTitle;
            payment.PaymentID = paymentPaymentID;
            payment.VoucherID = paymentVoucherID;
            payment.RecieptCount = paymentRecieptCount;
            payment.ProjectCode = paymentProjectCode;
            payment.ApplyPerson = paymentApplyPerson;
            payment.ApplyDate = paymentApplyDate;
            payment.Accountant = paymentAccountant;
            payment.AccountDate = paymentAccountDate;
            payment.Payer = paymentPayer;
            payment.PayDate = paymentPayDate;
            payment.Purpose = paymentPurpose;
            payment.Money = paymentMoney;
            payment.CheckPerson = paymentCheckPerson;
            payment.CheckDate = paymentCheckDate;
            payment.CheckOpinion = paymentCheckOpinion;
            payment.IsContract = paymentIsContract;
            payment.ContractCode = paymentContractCode;
            payment.Status = paymentStatus;
            payment.WBSCode = paymentWBSCode;
            payment.IsApportion = paymentIsApportion;
            payment.SupplyCode = paymentSupplyCode;
            payment.UnitCode = paymentUnitCode;
            payment.SupplyName = paymentSupplyName;
            payment.Remark = paymentRemark;
            payment.OldMoney = paymentOldMoney;
            payment.GroupCode = paymentGroupCode;
            payment.BankName = paymentBankName;
            payment.BankAccount = paymentBankAccount;
            payment.OtherAttachMent = paymentOtherAttachMent;
            payment.PayType = paymentPayType;
            payment.Issue = paymentIssue;
            payment.IssueMode = paymentIssueMode;
            payment.FactPayDate = paymentFactPayDate;
            payment.TotalPayMoney = paymentTotalPayMoney;
            payment.AHMoney = paymentAHMoney;
            payment.APMoney = paymentAPMoney;
            payment.UPMoney = paymentUPMoney;
            payment.SupplierApplyMoney = paymentSupplierApplyMoney;
            payment.MaxPayMoney = paymentMaxPayMoney;
            payment.PlanPayMoney = paymentPlanPayMoney;
            payment.ContractMoney = paymentContractMoney;
            payment.AdjustedContractMoney = paymentAdjustedContractMoney;
            payment.PayoutMoney = paymentPayoutMoney;
            payment.AHCash = paymentAHCash;
            payment.APCash = paymentAPCash;
            payment.UPCash = paymentUPCash;
            payment.PayoutCash = paymentPayoutCash;
            payment.AHCash0 = paymentAHCash0;
            payment.AHCash1 = paymentAHCash1;
            payment.AHCash2 = paymentAHCash2;
            payment.AHCash3 = paymentAHCash3;
            payment.AHCash4 = paymentAHCash4;
            payment.AHCash5 = paymentAHCash5;
            payment.AHCash6 = paymentAHCash6;
            payment.AHCash7 = paymentAHCash7;
            payment.AHCash8 = paymentAHCash8;
            payment.AHCash9 = paymentAHCash9;
            payment.PaymentName = paymentPaymentName;
            payment.TotalViseChangeMoney = paymentTotalViseChangeMoney;
            payment.SumCode = paymentSumCode;
            payment.PaymentCodition = paymentPaymentCodition;
            payment.CheckRemark = paymentCheckRemark;
            return payment;
        }

        public virtual Payment DeepCopy()
        {
            return EntityHelper.Clone<Payment>(this as Payment);
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

        public virtual bool Equals(PaymentBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(PaymentBase Object1, PaymentBase Object2)
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
            if (Object1.PaymentCode != Object2.PaymentCode)
            {
                flag = false;
            }
            if ((Object1.PaymentTitle != null) && (Object2.PaymentTitle != null))
            {
                if (Object1.PaymentTitle != Object2.PaymentTitle)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentTitle == null) ^ (Object2.PaymentTitle == null))
            {
                flag = false;
            }
            if ((Object1.PaymentID != null) && (Object2.PaymentID != null))
            {
                if (Object1.PaymentID != Object2.PaymentID)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentID == null) ^ (Object2.PaymentID == null))
            {
                flag = false;
            }
            if ((Object1.VoucherID != null) && (Object2.VoucherID != null))
            {
                if (Object1.VoucherID != Object2.VoucherID)
                {
                    flag = false;
                }
            }
            else if ((Object1.VoucherID == null) ^ (Object2.VoucherID == null))
            {
                flag = false;
            }
            if (Object1.RecieptCount.HasValue && Object2.RecieptCount.HasValue)
            {
                if (Object1.RecieptCount != Object2.RecieptCount)
                {
                    flag = false;
                }
            }
            else if (!Object1.RecieptCount.HasValue ^ !Object2.RecieptCount.HasValue)
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
            if ((Object1.ApplyPerson != null) && (Object2.ApplyPerson != null))
            {
                if (Object1.ApplyPerson != Object2.ApplyPerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.ApplyPerson == null) ^ (Object2.ApplyPerson == null))
            {
                flag = false;
            }
            if (Object1.ApplyDate.HasValue && Object2.ApplyDate.HasValue)
            {
                if (Object1.ApplyDate != Object2.ApplyDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.ApplyDate.HasValue ^ !Object2.ApplyDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.Accountant != null) && (Object2.Accountant != null))
            {
                if (Object1.Accountant != Object2.Accountant)
                {
                    flag = false;
                }
            }
            else if ((Object1.Accountant == null) ^ (Object2.Accountant == null))
            {
                flag = false;
            }
            if (Object1.AccountDate.HasValue && Object2.AccountDate.HasValue)
            {
                if (Object1.AccountDate != Object2.AccountDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.AccountDate.HasValue ^ !Object2.AccountDate.HasValue)
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
            if (Object1.PayDate.HasValue && Object2.PayDate.HasValue)
            {
                if (Object1.PayDate != Object2.PayDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PayDate.HasValue ^ !Object2.PayDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.Purpose != null) && (Object2.Purpose != null))
            {
                if (Object1.Purpose != Object2.Purpose)
                {
                    flag = false;
                }
            }
            else if ((Object1.Purpose == null) ^ (Object2.Purpose == null))
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
            if (Object1.IsContract.HasValue && Object2.IsContract.HasValue)
            {
                if (Object1.IsContract != Object2.IsContract)
                {
                    flag = false;
                }
            }
            else if (!Object1.IsContract.HasValue ^ !Object2.IsContract.HasValue)
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
            if ((Object1.WBSCode != null) && (Object2.WBSCode != null))
            {
                if (Object1.WBSCode != Object2.WBSCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.WBSCode == null) ^ (Object2.WBSCode == null))
            {
                flag = false;
            }
            if (Object1.IsApportion.HasValue && Object2.IsApportion.HasValue)
            {
                if (Object1.IsApportion != Object2.IsApportion)
                {
                    flag = false;
                }
            }
            else if (!Object1.IsApportion.HasValue ^ !Object2.IsApportion.HasValue)
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
            if ((Object1.UnitCode != null) && (Object2.UnitCode != null))
            {
                if (Object1.UnitCode != Object2.UnitCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.UnitCode == null) ^ (Object2.UnitCode == null))
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
            if (Object1.OldMoney.HasValue && Object2.OldMoney.HasValue)
            {
                if (Object1.OldMoney != Object2.OldMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.OldMoney.HasValue ^ !Object2.OldMoney.HasValue)
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
            if ((Object1.OtherAttachMent != null) && (Object2.OtherAttachMent != null))
            {
                if (Object1.OtherAttachMent != Object2.OtherAttachMent)
                {
                    flag = false;
                }
            }
            else if ((Object1.OtherAttachMent == null) ^ (Object2.OtherAttachMent == null))
            {
                flag = false;
            }
            if (Object1.PayType.HasValue && Object2.PayType.HasValue)
            {
                if (Object1.PayType != Object2.PayType)
                {
                    flag = false;
                }
            }
            else if (!Object1.PayType.HasValue ^ !Object2.PayType.HasValue)
            {
                flag = false;
            }
            if (Object1.Issue.HasValue && Object2.Issue.HasValue)
            {
                if (Object1.Issue != Object2.Issue)
                {
                    flag = false;
                }
            }
            else if (!Object1.Issue.HasValue ^ !Object2.Issue.HasValue)
            {
                flag = false;
            }
            if ((Object1.IssueMode != null) && (Object2.IssueMode != null))
            {
                if (Object1.IssueMode != Object2.IssueMode)
                {
                    flag = false;
                }
            }
            else if ((Object1.IssueMode == null) ^ (Object2.IssueMode == null))
            {
                flag = false;
            }
            if (Object1.FactPayDate.HasValue && Object2.FactPayDate.HasValue)
            {
                if (Object1.FactPayDate != Object2.FactPayDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.FactPayDate.HasValue ^ !Object2.FactPayDate.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalPayMoney.HasValue && Object2.TotalPayMoney.HasValue)
            {
                if (Object1.TotalPayMoney != Object2.TotalPayMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalPayMoney.HasValue ^ !Object2.TotalPayMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.AHMoney.HasValue && Object2.AHMoney.HasValue)
            {
                if (Object1.AHMoney != Object2.AHMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHMoney.HasValue ^ !Object2.AHMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.APMoney.HasValue && Object2.APMoney.HasValue)
            {
                if (Object1.APMoney != Object2.APMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.APMoney.HasValue ^ !Object2.APMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.UPMoney.HasValue && Object2.UPMoney.HasValue)
            {
                if (Object1.UPMoney != Object2.UPMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.UPMoney.HasValue ^ !Object2.UPMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.SupplierApplyMoney.HasValue && Object2.SupplierApplyMoney.HasValue)
            {
                if (Object1.SupplierApplyMoney != Object2.SupplierApplyMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.SupplierApplyMoney.HasValue ^ !Object2.SupplierApplyMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.MaxPayMoney.HasValue && Object2.MaxPayMoney.HasValue)
            {
                if (Object1.MaxPayMoney != Object2.MaxPayMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.MaxPayMoney.HasValue ^ !Object2.MaxPayMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.PlanPayMoney.HasValue && Object2.PlanPayMoney.HasValue)
            {
                if (Object1.PlanPayMoney != Object2.PlanPayMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlanPayMoney.HasValue ^ !Object2.PlanPayMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.ContractMoney.HasValue && Object2.ContractMoney.HasValue)
            {
                if (Object1.ContractMoney != Object2.ContractMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.ContractMoney.HasValue ^ !Object2.ContractMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.AdjustedContractMoney.HasValue && Object2.AdjustedContractMoney.HasValue)
            {
                if (Object1.AdjustedContractMoney != Object2.AdjustedContractMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.AdjustedContractMoney.HasValue ^ !Object2.AdjustedContractMoney.HasValue)
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
            if (Object1.AHCash.HasValue && Object2.AHCash.HasValue)
            {
                if (Object1.AHCash != Object2.AHCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash.HasValue ^ !Object2.AHCash.HasValue)
            {
                flag = false;
            }
            if (Object1.APCash.HasValue && Object2.APCash.HasValue)
            {
                if (Object1.APCash != Object2.APCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.APCash.HasValue ^ !Object2.APCash.HasValue)
            {
                flag = false;
            }
            if (Object1.UPCash.HasValue && Object2.UPCash.HasValue)
            {
                if (Object1.UPCash != Object2.UPCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.UPCash.HasValue ^ !Object2.UPCash.HasValue)
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
            if (Object1.AHCash0.HasValue && Object2.AHCash0.HasValue)
            {
                if (Object1.AHCash0 != Object2.AHCash0)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash0.HasValue ^ !Object2.AHCash0.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash1.HasValue && Object2.AHCash1.HasValue)
            {
                if (Object1.AHCash1 != Object2.AHCash1)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash1.HasValue ^ !Object2.AHCash1.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash2.HasValue && Object2.AHCash2.HasValue)
            {
                if (Object1.AHCash2 != Object2.AHCash2)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash2.HasValue ^ !Object2.AHCash2.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash3.HasValue && Object2.AHCash3.HasValue)
            {
                if (Object1.AHCash3 != Object2.AHCash3)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash3.HasValue ^ !Object2.AHCash3.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash4.HasValue && Object2.AHCash4.HasValue)
            {
                if (Object1.AHCash4 != Object2.AHCash4)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash4.HasValue ^ !Object2.AHCash4.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash5.HasValue && Object2.AHCash5.HasValue)
            {
                if (Object1.AHCash5 != Object2.AHCash5)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash5.HasValue ^ !Object2.AHCash5.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash6.HasValue && Object2.AHCash6.HasValue)
            {
                if (Object1.AHCash6 != Object2.AHCash6)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash6.HasValue ^ !Object2.AHCash6.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash7.HasValue && Object2.AHCash7.HasValue)
            {
                if (Object1.AHCash7 != Object2.AHCash7)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash7.HasValue ^ !Object2.AHCash7.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash8.HasValue && Object2.AHCash8.HasValue)
            {
                if (Object1.AHCash8 != Object2.AHCash8)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash8.HasValue ^ !Object2.AHCash8.HasValue)
            {
                flag = false;
            }
            if (Object1.AHCash9.HasValue && Object2.AHCash9.HasValue)
            {
                if (Object1.AHCash9 != Object2.AHCash9)
                {
                    flag = false;
                }
            }
            else if (!Object1.AHCash9.HasValue ^ !Object2.AHCash9.HasValue)
            {
                flag = false;
            }
            if ((Object1.PaymentName != null) && (Object2.PaymentName != null))
            {
                if (Object1.PaymentName != Object2.PaymentName)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentName == null) ^ (Object2.PaymentName == null))
            {
                flag = false;
            }
            if (Object1.TotalViseChangeMoney.HasValue && Object2.TotalViseChangeMoney.HasValue)
            {
                if (Object1.TotalViseChangeMoney != Object2.TotalViseChangeMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalViseChangeMoney.HasValue ^ !Object2.TotalViseChangeMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.SumCode != null) && (Object2.SumCode != null))
            {
                if (Object1.SumCode != Object2.SumCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SumCode == null) ^ (Object2.SumCode == null))
            {
                flag = false;
            }
            if ((Object1.PaymentCodition != null) && (Object2.PaymentCodition != null))
            {
                if (Object1.PaymentCodition != Object2.PaymentCodition)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentCodition == null) ^ (Object2.PaymentCodition == null))
            {
                flag = false;
            }
            if ((Object1.CheckRemark != null) && (Object2.CheckRemark != null))
            {
                if (Object1.CheckRemark != Object2.CheckRemark)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.CheckRemark == null) ^ (Object2.CheckRemark == null))
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

        public void OnColumnChanged(PaymentColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(PaymentColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                PaymentEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new PaymentEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(PaymentColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(PaymentColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                PaymentEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new PaymentEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Payment);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as PaymentEntityData;
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
                    this.parentCollection.Remove((Payment) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{65}{64}- PaymentCode: {0}{64}- PaymentTitle: {1}{64}- PaymentID: {2}{64}- VoucherID: {3}{64}- RecieptCount: {4}{64}- ProjectCode: {5}{64}- ApplyPerson: {6}{64}- ApplyDate: {7}{64}- Accountant: {8}{64}- AccountDate: {9}{64}- Payer: {10}{64}- PayDate: {11}{64}- Purpose: {12}{64}- Money: {13}{64}- CheckPerson: {14}{64}- CheckDate: {15}{64}- CheckOpinion: {16}{64}- IsContract: {17}{64}- ContractCode: {18}{64}- Status: {19}{64}- WBSCode: {20}{64}- IsApportion: {21}{64}- SupplyCode: {22}{64}- UnitCode: {23}{64}- SupplyName: {24}{64}- Remark: {25}{64}- OldMoney: {26}{64}- GroupCode: {27}{64}- BankName: {28}{64}- BankAccount: {29}{64}- OtherAttachMent: {30}{64}- PayType: {31}{64}- Issue: {32}{64}- IssueMode: {33}{64}- FactPayDate: {34}{64}- TotalPayMoney: {35}{64}- AHMoney: {36}{64}- APMoney: {37}{64}- UPMoney: {38}{64}- SupplierApplyMoney: {39}{64}- MaxPayMoney: {40}{64}- PlanPayMoney: {41}{64}- ContractMoney: {42}{64}- AdjustedContractMoney: {43}{64}- PayoutMoney: {44}{64}- AHCash: {45}{64}- APCash: {46}{64}- UPCash: {47}{64}- PayoutCash: {48}{64}- AHCash0: {49}{64}- AHCash1: {50}{64}- AHCash2: {51}{64}- AHCash3: {52}{64}- AHCash4: {53}{64}- AHCash5: {54}{64}- AHCash6: {55}{64}- AHCash7: {56}{64}- AHCash8: {57}{64}- AHCash9: {58}{64}- PaymentName: {59}{64}- TotalViseChangeMoney: {60}{64}- SumCode: {61}{64}- PaymentCodition: {62}{64}- CheckRemark: {63}{64}", new object[] { 
                this.PaymentCode, (this.PaymentTitle == null) ? string.Empty : this.PaymentTitle.ToString(), (this.PaymentID == null) ? string.Empty : this.PaymentID.ToString(), (this.VoucherID == null) ? string.Empty : this.VoucherID.ToString(), !this.RecieptCount.HasValue ? string.Empty : this.RecieptCount.ToString(), (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.ApplyPerson == null) ? string.Empty : this.ApplyPerson.ToString(), !this.ApplyDate.HasValue ? string.Empty : this.ApplyDate.ToString(), (this.Accountant == null) ? string.Empty : this.Accountant.ToString(), !this.AccountDate.HasValue ? string.Empty : this.AccountDate.ToString(), (this.Payer == null) ? string.Empty : this.Payer.ToString(), !this.PayDate.HasValue ? string.Empty : this.PayDate.ToString(), (this.Purpose == null) ? string.Empty : this.Purpose.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), (this.CheckPerson == null) ? string.Empty : this.CheckPerson.ToString(), !this.CheckDate.HasValue ? string.Empty : this.CheckDate.ToString(), 
                (this.CheckOpinion == null) ? string.Empty : this.CheckOpinion.ToString(), !this.IsContract.HasValue ? string.Empty : this.IsContract.ToString(), (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), (this.WBSCode == null) ? string.Empty : this.WBSCode.ToString(), !this.IsApportion.HasValue ? string.Empty : this.IsApportion.ToString(), (this.SupplyCode == null) ? string.Empty : this.SupplyCode.ToString(), (this.UnitCode == null) ? string.Empty : this.UnitCode.ToString(), (this.SupplyName == null) ? string.Empty : this.SupplyName.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), !this.OldMoney.HasValue ? string.Empty : this.OldMoney.ToString(), (this.GroupCode == null) ? string.Empty : this.GroupCode.ToString(), (this.BankName == null) ? string.Empty : this.BankName.ToString(), (this.BankAccount == null) ? string.Empty : this.BankAccount.ToString(), (this.OtherAttachMent == null) ? string.Empty : this.OtherAttachMent.ToString(), !this.PayType.HasValue ? string.Empty : this.PayType.ToString(), 
                !this.Issue.HasValue ? string.Empty : this.Issue.ToString(), (this.IssueMode == null) ? string.Empty : this.IssueMode.ToString(), !this.FactPayDate.HasValue ? string.Empty : this.FactPayDate.ToString(), !this.TotalPayMoney.HasValue ? string.Empty : this.TotalPayMoney.ToString(), !this.AHMoney.HasValue ? string.Empty : this.AHMoney.ToString(), !this.APMoney.HasValue ? string.Empty : this.APMoney.ToString(), !this.UPMoney.HasValue ? string.Empty : this.UPMoney.ToString(), !this.SupplierApplyMoney.HasValue ? string.Empty : this.SupplierApplyMoney.ToString(), !this.MaxPayMoney.HasValue ? string.Empty : this.MaxPayMoney.ToString(), !this.PlanPayMoney.HasValue ? string.Empty : this.PlanPayMoney.ToString(), !this.ContractMoney.HasValue ? string.Empty : this.ContractMoney.ToString(), !this.AdjustedContractMoney.HasValue ? string.Empty : this.AdjustedContractMoney.ToString(), !this.PayoutMoney.HasValue ? string.Empty : this.PayoutMoney.ToString(), !this.AHCash.HasValue ? string.Empty : this.AHCash.ToString(), !this.APCash.HasValue ? string.Empty : this.APCash.ToString(), !this.UPCash.HasValue ? string.Empty : this.UPCash.ToString(), 
                !this.PayoutCash.HasValue ? string.Empty : this.PayoutCash.ToString(), !this.AHCash0.HasValue ? string.Empty : this.AHCash0.ToString(), !this.AHCash1.HasValue ? string.Empty : this.AHCash1.ToString(), !this.AHCash2.HasValue ? string.Empty : this.AHCash2.ToString(), !this.AHCash3.HasValue ? string.Empty : this.AHCash3.ToString(), !this.AHCash4.HasValue ? string.Empty : this.AHCash4.ToString(), !this.AHCash5.HasValue ? string.Empty : this.AHCash5.ToString(), !this.AHCash6.HasValue ? string.Empty : this.AHCash6.ToString(), !this.AHCash7.HasValue ? string.Empty : this.AHCash7.ToString(), !this.AHCash8.HasValue ? string.Empty : this.AHCash8.ToString(), !this.AHCash9.HasValue ? string.Empty : this.AHCash9.ToString(), (this.PaymentName == null) ? string.Empty : this.PaymentName.ToString(), !this.TotalViseChangeMoney.HasValue ? string.Empty : this.TotalViseChangeMoney.ToString(), (this.SumCode == null) ? string.Empty : this.SumCode.ToString(), (this.PaymentCodition == null) ? string.Empty : this.PaymentCodition.ToString(), (this.CheckRemark == null) ? string.Empty : this.CheckRemark.ToString(), 
                Environment.NewLine, base.GetType()
             });
        }

        [TiannuoPM.Entities.Bindable, Description("结算人"), DataObjectField(false, false, true, 50)]
        public virtual string Accountant
        {
            get
            {
                return this.entityData.Accountant;
            }
            set
            {
                if (this.entityData.Accountant != value)
                {
                    this.OnColumnChanging(PaymentColumn.Accountant, this.entityData.Accountant);
                    this.entityData.Accountant = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Accountant, this.entityData.Accountant);
                    this.OnPropertyChanged("Accountant");
                }
            }
        }

        [DataObjectField(false, false, true), Description("结算日期"), TiannuoPM.Entities.Bindable]
        public virtual DateTime? AccountDate
        {
            get
            {
                return this.entityData.AccountDate;
            }
            set
            {
                if (this.entityData.AccountDate != value)
                {
                    this.OnColumnChanging(PaymentColumn.AccountDate, this.entityData.AccountDate);
                    this.entityData.AccountDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AccountDate, this.entityData.AccountDate);
                    this.OnPropertyChanged("AccountDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true)]
        public virtual decimal? AdjustedContractMoney
        {
            get
            {
                return this.entityData.AdjustedContractMoney;
            }
            set
            {
                if (this.entityData.AdjustedContractMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.AdjustedContractMoney, this.entityData.AdjustedContractMoney);
                    this.entityData.AdjustedContractMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AdjustedContractMoney, this.entityData.AdjustedContractMoney);
                    this.OnPropertyChanged("AdjustedContractMoney");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? AHCash
        {
            get
            {
                return this.entityData.AHCash;
            }
            set
            {
                if (this.entityData.AHCash != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash, this.entityData.AHCash);
                    this.entityData.AHCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash, this.entityData.AHCash);
                    this.OnPropertyChanged("AHCash");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? AHCash0
        {
            get
            {
                return this.entityData.AHCash0;
            }
            set
            {
                if (this.entityData.AHCash0 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash0, this.entityData.AHCash0);
                    this.entityData.AHCash0 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash0, this.entityData.AHCash0);
                    this.OnPropertyChanged("AHCash0");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? AHCash1
        {
            get
            {
                return this.entityData.AHCash1;
            }
            set
            {
                if (this.entityData.AHCash1 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash1, this.entityData.AHCash1);
                    this.entityData.AHCash1 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash1, this.entityData.AHCash1);
                    this.OnPropertyChanged("AHCash1");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? AHCash2
        {
            get
            {
                return this.entityData.AHCash2;
            }
            set
            {
                if (this.entityData.AHCash2 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash2, this.entityData.AHCash2);
                    this.entityData.AHCash2 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash2, this.entityData.AHCash2);
                    this.OnPropertyChanged("AHCash2");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? AHCash3
        {
            get
            {
                return this.entityData.AHCash3;
            }
            set
            {
                if (this.entityData.AHCash3 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash3, this.entityData.AHCash3);
                    this.entityData.AHCash3 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash3, this.entityData.AHCash3);
                    this.OnPropertyChanged("AHCash3");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? AHCash4
        {
            get
            {
                return this.entityData.AHCash4;
            }
            set
            {
                if (this.entityData.AHCash4 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash4, this.entityData.AHCash4);
                    this.entityData.AHCash4 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash4, this.entityData.AHCash4);
                    this.OnPropertyChanged("AHCash4");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? AHCash5
        {
            get
            {
                return this.entityData.AHCash5;
            }
            set
            {
                if (this.entityData.AHCash5 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash5, this.entityData.AHCash5);
                    this.entityData.AHCash5 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash5, this.entityData.AHCash5);
                    this.OnPropertyChanged("AHCash5");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? AHCash6
        {
            get
            {
                return this.entityData.AHCash6;
            }
            set
            {
                if (this.entityData.AHCash6 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash6, this.entityData.AHCash6);
                    this.entityData.AHCash6 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash6, this.entityData.AHCash6);
                    this.OnPropertyChanged("AHCash6");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual decimal? AHCash7
        {
            get
            {
                return this.entityData.AHCash7;
            }
            set
            {
                if (this.entityData.AHCash7 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash7, this.entityData.AHCash7);
                    this.entityData.AHCash7 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash7, this.entityData.AHCash7);
                    this.OnPropertyChanged("AHCash7");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true)]
        public virtual decimal? AHCash8
        {
            get
            {
                return this.entityData.AHCash8;
            }
            set
            {
                if (this.entityData.AHCash8 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash8, this.entityData.AHCash8);
                    this.entityData.AHCash8 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash8, this.entityData.AHCash8);
                    this.OnPropertyChanged("AHCash8");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual decimal? AHCash9
        {
            get
            {
                return this.entityData.AHCash9;
            }
            set
            {
                if (this.entityData.AHCash9 != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHCash9, this.entityData.AHCash9);
                    this.entityData.AHCash9 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHCash9, this.entityData.AHCash9);
                    this.OnPropertyChanged("AHCash9");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? AHMoney
        {
            get
            {
                return this.entityData.AHMoney;
            }
            set
            {
                if (this.entityData.AHMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.AHMoney, this.entityData.AHMoney);
                    this.entityData.AHMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.AHMoney, this.entityData.AHMoney);
                    this.OnPropertyChanged("AHMoney");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? APCash
        {
            get
            {
                return this.entityData.APCash;
            }
            set
            {
                if (this.entityData.APCash != value)
                {
                    this.OnColumnChanging(PaymentColumn.APCash, this.entityData.APCash);
                    this.entityData.APCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.APCash, this.entityData.APCash);
                    this.OnPropertyChanged("APCash");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? APMoney
        {
            get
            {
                return this.entityData.APMoney;
            }
            set
            {
                if (this.entityData.APMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.APMoney, this.entityData.APMoney);
                    this.entityData.APMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.APMoney, this.entityData.APMoney);
                    this.OnPropertyChanged("APMoney");
                }
            }
        }

        [Description("申请日期"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? ApplyDate
        {
            get
            {
                return this.entityData.ApplyDate;
            }
            set
            {
                if (this.entityData.ApplyDate != value)
                {
                    this.OnColumnChanging(PaymentColumn.ApplyDate, this.entityData.ApplyDate);
                    this.entityData.ApplyDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.ApplyDate, this.entityData.ApplyDate);
                    this.OnPropertyChanged("ApplyDate");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("申请人"), TiannuoPM.Entities.Bindable]
        public virtual string ApplyPerson
        {
            get
            {
                return this.entityData.ApplyPerson;
            }
            set
            {
                if (this.entityData.ApplyPerson != value)
                {
                    this.OnColumnChanging(PaymentColumn.ApplyPerson, this.entityData.ApplyPerson);
                    this.entityData.ApplyPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.ApplyPerson, this.entityData.ApplyPerson);
                    this.OnPropertyChanged("ApplyPerson");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("银行帐号")]
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
                    this.OnColumnChanging(PaymentColumn.BankAccount, this.entityData.BankAccount);
                    this.entityData.BankAccount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.BankAccount, this.entityData.BankAccount);
                    this.OnPropertyChanged("BankAccount");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 100), Description("开户银行")]
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
                    this.OnColumnChanging(PaymentColumn.BankName, this.entityData.BankName);
                    this.entityData.BankName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.BankName, this.entityData.BankName);
                    this.OnPropertyChanged("BankName");
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
                    this.OnColumnChanging(PaymentColumn.CheckDate, this.entityData.CheckDate);
                    this.entityData.CheckDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.CheckDate, this.entityData.CheckDate);
                    this.OnPropertyChanged("CheckDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("审核意见"), DataObjectField(false, false, true, 800)]
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
                    this.OnColumnChanging(PaymentColumn.CheckOpinion, this.entityData.CheckOpinion);
                    this.entityData.CheckOpinion = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.CheckOpinion, this.entityData.CheckOpinion);
                    this.OnPropertyChanged("CheckOpinion");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("审核人"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.CheckPerson, this.entityData.CheckPerson);
                    this.entityData.CheckPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.CheckPerson, this.entityData.CheckPerson);
                    this.OnPropertyChanged("CheckPerson");
                }
            }
        }

        [Description("x"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 0x3e8)]
        public virtual string CheckRemark
        {
            get
            {
                return this.entityData.CheckRemark;
            }
            set
            {
                if (this.entityData.CheckRemark != value)
                {
                    this.OnColumnChanging(PaymentColumn.CheckRemark, this.entityData.CheckRemark);
                    this.entityData.CheckRemark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.CheckRemark, this.entityData.CheckRemark);
                    this.OnPropertyChanged("CheckRemark");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(PaymentColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.ContractCode, this.entityData.ContractCode);
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

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("按合同应付的金额")]
        public virtual decimal? ContractMoney
        {
            get
            {
                return this.entityData.ContractMoney;
            }
            set
            {
                if (this.entityData.ContractMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.ContractMoney, this.entityData.ContractMoney);
                    this.entityData.ContractMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.ContractMoney, this.entityData.ContractMoney);
                    this.OnPropertyChanged("ContractMoney");
                }
            }
        }

        [XmlIgnore]
        public PaymentKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new PaymentKey(this);
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
                    this.entityTrackingKey = "Payment" + this.PaymentCode.ToString();
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

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual DateTime? FactPayDate
        {
            get
            {
                return this.entityData.FactPayDate;
            }
            set
            {
                if (this.entityData.FactPayDate != value)
                {
                    this.OnColumnChanging(PaymentColumn.FactPayDate, this.entityData.FactPayDate);
                    this.entityData.FactPayDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.FactPayDate, this.entityData.FactPayDate);
                    this.OnPropertyChanged("FactPayDate");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("请款单类型"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.GroupCode, this.entityData.GroupCode);
                    this.entityData.GroupCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.GroupCode, this.entityData.GroupCode);
                    this.OnPropertyChanged("GroupCode");
                }
            }
        }

        [Description("x"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual int? IsApportion
        {
            get
            {
                return this.entityData.IsApportion;
            }
            set
            {
                if (this.entityData.IsApportion != value)
                {
                    this.OnColumnChanging(PaymentColumn.IsApportion, this.entityData.IsApportion);
                    this.entityData.IsApportion = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.IsApportion, this.entityData.IsApportion);
                    this.OnPropertyChanged("IsApportion");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("是否合同请款")]
        public virtual int? IsContract
        {
            get
            {
                return this.entityData.IsContract;
            }
            set
            {
                if (this.entityData.IsContract != value)
                {
                    this.OnColumnChanging(PaymentColumn.IsContract, this.entityData.IsContract);
                    this.entityData.IsContract = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.IsContract, this.entityData.IsContract);
                    this.OnPropertyChanged("IsContract");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual int? Issue
        {
            get
            {
                return this.entityData.Issue;
            }
            set
            {
                if (this.entityData.Issue != value)
                {
                    this.OnColumnChanging(PaymentColumn.Issue, this.entityData.Issue);
                    this.entityData.Issue = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Issue, this.entityData.Issue);
                    this.OnPropertyChanged("Issue");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string IssueMode
        {
            get
            {
                return this.entityData.IssueMode;
            }
            set
            {
                if (this.entityData.IssueMode != value)
                {
                    this.OnColumnChanging(PaymentColumn.IssueMode, this.entityData.IssueMode);
                    this.entityData.IssueMode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.IssueMode, this.entityData.IssueMode);
                    this.OnPropertyChanged("IssueMode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? MaxPayMoney
        {
            get
            {
                return this.entityData.MaxPayMoney;
            }
            set
            {
                if (this.entityData.MaxPayMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.MaxPayMoney, this.entityData.MaxPayMoney);
                    this.entityData.MaxPayMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.MaxPayMoney, this.entityData.MaxPayMoney);
                    this.OnPropertyChanged("MaxPayMoney");
                }
            }
        }

        [Description("请款金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(PaymentColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? OldMoney
        {
            get
            {
                return this.entityData.OldMoney;
            }
            set
            {
                if (this.entityData.OldMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.OldMoney, this.entityData.OldMoney);
                    this.entityData.OldMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.OldMoney, this.entityData.OldMoney);
                    this.OnPropertyChanged("OldMoney");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalPaymentCode
        {
            get
            {
                return this.entityData.OriginalPaymentCode;
            }
            set
            {
                this.entityData.OriginalPaymentCode = value;
            }
        }

        [Description("其他附件"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 0x3e8)]
        public virtual string OtherAttachMent
        {
            get
            {
                return this.entityData.OtherAttachMent;
            }
            set
            {
                if (this.entityData.OtherAttachMent != value)
                {
                    this.OnColumnChanging(PaymentColumn.OtherAttachMent, this.entityData.OtherAttachMent);
                    this.entityData.OtherAttachMent = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.OtherAttachMent, this.entityData.OtherAttachMent);
                    this.OnPropertyChanged("OtherAttachMent");
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
                this.parentCollection = value as TList<Payment>;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("最后付款期限")]
        public virtual DateTime? PayDate
        {
            get
            {
                return this.entityData.PayDate;
            }
            set
            {
                if (this.entityData.PayDate != value)
                {
                    this.OnColumnChanging(PaymentColumn.PayDate, this.entityData.PayDate);
                    this.entityData.PayDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PayDate, this.entityData.PayDate);
                    this.OnPropertyChanged("PayDate");
                }
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
                    this.OnColumnChanging(PaymentColumn.Payer, this.entityData.Payer);
                    this.entityData.Payer = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Payer, this.entityData.Payer);
                    this.OnPropertyChanged("Payer");
                }
            }
        }

        [Description("请款单code"), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
        public virtual string PaymentCode
        {
            get
            {
                return this.entityData.PaymentCode;
            }
            set
            {
                if (this.entityData.PaymentCode != value)
                {
                    this.OnColumnChanging(PaymentColumn.PaymentCode, this.entityData.PaymentCode);
                    this.entityData.PaymentCode = value;
                    this.EntityId.PaymentCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PaymentCode, this.entityData.PaymentCode);
                    this.OnPropertyChanged("PaymentCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 100), Description("")]
        public virtual string PaymentCodition
        {
            get
            {
                return this.entityData.PaymentCodition;
            }
            set
            {
                if (this.entityData.PaymentCodition != value)
                {
                    this.OnColumnChanging(PaymentColumn.PaymentCodition, this.entityData.PaymentCodition);
                    this.entityData.PaymentCodition = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PaymentCodition, this.entityData.PaymentCodition);
                    this.OnPropertyChanged("PaymentCodition");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string PaymentID
        {
            get
            {
                return this.entityData.PaymentID;
            }
            set
            {
                if (this.entityData.PaymentID != value)
                {
                    this.OnColumnChanging(PaymentColumn.PaymentID, this.entityData.PaymentID);
                    this.entityData.PaymentID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PaymentID, this.entityData.PaymentID);
                    this.OnPropertyChanged("PaymentID");
                }
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

        [DataObjectField(false, false, true, 100), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string PaymentName
        {
            get
            {
                return this.entityData.PaymentName;
            }
            set
            {
                if (this.entityData.PaymentName != value)
                {
                    this.OnColumnChanging(PaymentColumn.PaymentName, this.entityData.PaymentName);
                    this.entityData.PaymentName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PaymentName, this.entityData.PaymentName);
                    this.OnPropertyChanged("PaymentName");
                }
            }
        }

        [Description("相关业务"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string PaymentTitle
        {
            get
            {
                return this.entityData.PaymentTitle;
            }
            set
            {
                if (this.entityData.PaymentTitle != value)
                {
                    this.OnColumnChanging(PaymentColumn.PaymentTitle, this.entityData.PaymentTitle);
                    this.entityData.PaymentTitle = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PaymentTitle, this.entityData.PaymentTitle);
                    this.OnPropertyChanged("PaymentTitle");
                }
            }
        }

        [Description("付款金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.PayoutCash, this.entityData.PayoutCash);
                    this.entityData.PayoutCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PayoutCash, this.entityData.PayoutCash);
                    this.OnPropertyChanged("PayoutCash");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("本币已付金额"), DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(PaymentColumn.PayoutMoney, this.entityData.PayoutMoney);
                    this.entityData.PayoutMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PayoutMoney, this.entityData.PayoutMoney);
                    this.OnPropertyChanged("PayoutMoney");
                }
            }
        }

        [DataObjectField(false, false, true), Description("支付方式 支付形式"), TiannuoPM.Entities.Bindable]
        public virtual int? PayType
        {
            get
            {
                return this.entityData.PayType;
            }
            set
            {
                if (this.entityData.PayType != value)
                {
                    this.OnColumnChanging(PaymentColumn.PayType, this.entityData.PayType);
                    this.entityData.PayType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PayType, this.entityData.PayType);
                    this.OnPropertyChanged("PayType");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? PlanPayMoney
        {
            get
            {
                return this.entityData.PlanPayMoney;
            }
            set
            {
                if (this.entityData.PlanPayMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.PlanPayMoney, this.entityData.PlanPayMoney);
                    this.entityData.PlanPayMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.PlanPayMoney, this.entityData.PlanPayMoney);
                    this.OnPropertyChanged("PlanPayMoney");
                }
            }
        }

        [Description("项目code"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Browsable(false), XmlIgnore]
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

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 800), Description("请款用途")]
        public virtual string Purpose
        {
            get
            {
                return this.entityData.Purpose;
            }
            set
            {
                if (this.entityData.Purpose != value)
                {
                    this.OnColumnChanging(PaymentColumn.Purpose, this.entityData.Purpose);
                    this.entityData.Purpose = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Purpose, this.entityData.Purpose);
                    this.OnPropertyChanged("Purpose");
                }
            }
        }

        [DataObjectField(false, false, true), Description("单据数量"), TiannuoPM.Entities.Bindable]
        public virtual int? RecieptCount
        {
            get
            {
                return this.entityData.RecieptCount;
            }
            set
            {
                if (this.entityData.RecieptCount != value)
                {
                    this.OnColumnChanging(PaymentColumn.RecieptCount, this.entityData.RecieptCount);
                    this.entityData.RecieptCount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.RecieptCount, this.entityData.RecieptCount);
                    this.OnPropertyChanged("RecieptCount");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 800), Description("备注")]
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
                    this.OnColumnChanging(PaymentColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
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

        [Description("审核状态"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 100)]
        public virtual string SumCode
        {
            get
            {
                return this.entityData.SumCode;
            }
            set
            {
                if (this.entityData.SumCode != value)
                {
                    this.OnColumnChanging(PaymentColumn.SumCode, this.entityData.SumCode);
                    this.entityData.SumCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.SumCode, this.entityData.SumCode);
                    this.OnPropertyChanged("SumCode");
                }
            }
        }

        [Description("承包商申请金额"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? SupplierApplyMoney
        {
            get
            {
                return this.entityData.SupplierApplyMoney;
            }
            set
            {
                if (this.entityData.SupplierApplyMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.SupplierApplyMoney, this.entityData.SupplierApplyMoney);
                    this.entityData.SupplierApplyMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.SupplierApplyMoney, this.entityData.SupplierApplyMoney);
                    this.OnPropertyChanged("SupplierApplyMoney");
                }
            }
        }

        [Description("受款单位编号"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(PaymentColumn.SupplyCode, this.entityData.SupplyCode);
                    this.entityData.SupplyCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.SupplyCode, this.entityData.SupplyCode);
                    this.OnPropertyChanged("SupplyCode");
                }
            }
        }

        [DataObjectField(false, false, true, 200), Description("受款单位"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(PaymentColumn.SupplyName, this.entityData.SupplyName);
                    this.entityData.SupplyName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.SupplyName, this.entityData.SupplyName);
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
                    "PaymentCode", "PaymentTitle", "PaymentID", "VoucherID", "RecieptCount", "ProjectCode", "ApplyPerson", "ApplyDate", "Accountant", "AccountDate", "Payer", "PayDate", "Purpose", "Money", "CheckPerson", "CheckDate", 
                    "CheckOpinion", "IsContract", "ContractCode", "Status", "WBSCode", "IsApportion", "SupplyCode", "UnitCode", "SupplyName", "Remark", "OldMoney", "GroupCode", "BankName", "BankAccount", "OtherAttachMent", "PayType", 
                    "Issue", "IssueMode", "FactPayDate", "TotalPayMoney", "AHMoney", "APMoney", "UPMoney", "SupplierApplyMoney", "MaxPayMoney", "PlanPayMoney", "ContractMoney", "AdjustedContractMoney", "PayoutMoney", "AHCash", "APCash", "UPCash", 
                    "PayoutCash", "AHCash0", "AHCash1", "AHCash2", "AHCash3", "AHCash4", "AHCash5", "AHCash6", "AHCash7", "AHCash8", "AHCash9", "PaymentName", "TotalViseChangeMoney", "SumCode", "PaymentCodition", "CheckRemark"
                 };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "Payment";
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("请款总额")]
        public virtual decimal? TotalPayMoney
        {
            get
            {
                return this.entityData.TotalPayMoney;
            }
            set
            {
                if (this.entityData.TotalPayMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.TotalPayMoney, this.entityData.TotalPayMoney);
                    this.entityData.TotalPayMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.TotalPayMoney, this.entityData.TotalPayMoney);
                    this.OnPropertyChanged("TotalPayMoney");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? TotalViseChangeMoney
        {
            get
            {
                return this.entityData.TotalViseChangeMoney;
            }
            set
            {
                if (this.entityData.TotalViseChangeMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.TotalViseChangeMoney, this.entityData.TotalViseChangeMoney);
                    this.entityData.TotalViseChangeMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.TotalViseChangeMoney, this.entityData.TotalViseChangeMoney);
                    this.OnPropertyChanged("TotalViseChangeMoney");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("请款部门code"), TiannuoPM.Entities.Bindable]
        public virtual string UnitCode
        {
            get
            {
                return this.entityData.UnitCode;
            }
            set
            {
                if (this.entityData.UnitCode != value)
                {
                    this.OnColumnChanging(PaymentColumn.UnitCode, this.entityData.UnitCode);
                    this.entityData.UnitCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.UnitCode, this.entityData.UnitCode);
                    this.OnPropertyChanged("UnitCode");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? UPCash
        {
            get
            {
                return this.entityData.UPCash;
            }
            set
            {
                if (this.entityData.UPCash != value)
                {
                    this.OnColumnChanging(PaymentColumn.UPCash, this.entityData.UPCash);
                    this.entityData.UPCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.UPCash, this.entityData.UPCash);
                    this.OnPropertyChanged("UPCash");
                }
            }
        }

        [Description("x"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? UPMoney
        {
            get
            {
                return this.entityData.UPMoney;
            }
            set
            {
                if (this.entityData.UPMoney != value)
                {
                    this.OnColumnChanging(PaymentColumn.UPMoney, this.entityData.UPMoney);
                    this.entityData.UPMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.UPMoney, this.entityData.UPMoney);
                    this.OnPropertyChanged("UPMoney");
                }
            }
        }

        [Description("x"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string VoucherID
        {
            get
            {
                return this.entityData.VoucherID;
            }
            set
            {
                if (this.entityData.VoucherID != value)
                {
                    this.OnColumnChanging(PaymentColumn.VoucherID, this.entityData.VoucherID);
                    this.entityData.VoucherID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.VoucherID, this.entityData.VoucherID);
                    this.OnPropertyChanged("VoucherID");
                }
            }
        }

        [Description("x"), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string WBSCode
        {
            get
            {
                return this.entityData.WBSCode;
            }
            set
            {
                if (this.entityData.WBSCode != value)
                {
                    this.OnColumnChanging(PaymentColumn.WBSCode, this.entityData.WBSCode);
                    this.entityData.WBSCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentColumn.WBSCode, this.entityData.WBSCode);
                    this.OnPropertyChanged("WBSCode");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class PaymentEntityData : ICloneable
        {
            public string Accountant = null;
            public DateTime? AccountDate = null;
            public decimal? AdjustedContractMoney = null;
            public decimal? AHCash = null;
            public decimal? AHCash0 = null;
            public decimal? AHCash1 = null;
            public decimal? AHCash2 = null;
            public decimal? AHCash3 = null;
            public decimal? AHCash4 = null;
            public decimal? AHCash5 = null;
            public decimal? AHCash6 = null;
            public decimal? AHCash7 = null;
            public decimal? AHCash8 = null;
            public decimal? AHCash9 = null;
            public decimal? AHMoney = null;
            public decimal? APCash = null;
            public decimal? APMoney = null;
            public DateTime? ApplyDate = null;
            public string ApplyPerson = null;
            public string BankAccount = null;
            public string BankName = null;
            public DateTime? CheckDate = null;
            public string CheckOpinion = null;
            public string CheckPerson = null;
            public string CheckRemark = null;
            public string ContractCode = null;
            public decimal? ContractMoney = null;
            public DateTime? FactPayDate = null;
            public string GroupCode = null;
            public int? IsApportion = null;
            public int? IsContract = null;
            public int? Issue = null;
            public string IssueMode = null;
            public decimal? MaxPayMoney = null;
            public decimal? Money = null;
            public decimal? OldMoney = null;
            public string OriginalPaymentCode;
            public string OtherAttachMent = null;
            public DateTime? PayDate = null;
            public string Payer = null;
            public string PaymentCode;
            public string PaymentCodition = null;
            public string PaymentID = null;
            private TList<PaymentItem> paymentItemPaymentCode;
            public string PaymentName = null;
            public string PaymentTitle = null;
            public decimal? PayoutCash = null;
            public decimal? PayoutMoney = null;
            public int? PayType = null;
            public decimal? PlanPayMoney = null;
            public string ProjectCode = null;
            public string Purpose = null;
            public int? RecieptCount = null;
            public string Remark = null;
            public int? Status = null;
            public string SumCode = null;
            public decimal? SupplierApplyMoney = null;
            public string SupplyCode = null;
            public string SupplyName = null;
            public decimal? TotalPayMoney = null;
            public decimal? TotalViseChangeMoney = null;
            public string UnitCode = null;
            public decimal? UPCash = null;
            public decimal? UPMoney = null;
            public string VoucherID = null;
            public string WBSCode = null;

            public object Clone()
            {
                PaymentBase.PaymentEntityData data = new PaymentBase.PaymentEntityData();
                data.PaymentCode = this.PaymentCode;
                data.OriginalPaymentCode = this.OriginalPaymentCode;
                data.PaymentTitle = this.PaymentTitle;
                data.PaymentID = this.PaymentID;
                data.VoucherID = this.VoucherID;
                data.RecieptCount = this.RecieptCount;
                data.ProjectCode = this.ProjectCode;
                data.ApplyPerson = this.ApplyPerson;
                data.ApplyDate = this.ApplyDate;
                data.Accountant = this.Accountant;
                data.AccountDate = this.AccountDate;
                data.Payer = this.Payer;
                data.PayDate = this.PayDate;
                data.Purpose = this.Purpose;
                data.Money = this.Money;
                data.CheckPerson = this.CheckPerson;
                data.CheckDate = this.CheckDate;
                data.CheckOpinion = this.CheckOpinion;
                data.IsContract = this.IsContract;
                data.ContractCode = this.ContractCode;
                data.Status = this.Status;
                data.WBSCode = this.WBSCode;
                data.IsApportion = this.IsApportion;
                data.SupplyCode = this.SupplyCode;
                data.UnitCode = this.UnitCode;
                data.SupplyName = this.SupplyName;
                data.Remark = this.Remark;
                data.OldMoney = this.OldMoney;
                data.GroupCode = this.GroupCode;
                data.BankName = this.BankName;
                data.BankAccount = this.BankAccount;
                data.OtherAttachMent = this.OtherAttachMent;
                data.PayType = this.PayType;
                data.Issue = this.Issue;
                data.IssueMode = this.IssueMode;
                data.FactPayDate = this.FactPayDate;
                data.TotalPayMoney = this.TotalPayMoney;
                data.AHMoney = this.AHMoney;
                data.APMoney = this.APMoney;
                data.UPMoney = this.UPMoney;
                data.SupplierApplyMoney = this.SupplierApplyMoney;
                data.MaxPayMoney = this.MaxPayMoney;
                data.PlanPayMoney = this.PlanPayMoney;
                data.ContractMoney = this.ContractMoney;
                data.AdjustedContractMoney = this.AdjustedContractMoney;
                data.PayoutMoney = this.PayoutMoney;
                data.AHCash = this.AHCash;
                data.APCash = this.APCash;
                data.UPCash = this.UPCash;
                data.PayoutCash = this.PayoutCash;
                data.AHCash0 = this.AHCash0;
                data.AHCash1 = this.AHCash1;
                data.AHCash2 = this.AHCash2;
                data.AHCash3 = this.AHCash3;
                data.AHCash4 = this.AHCash4;
                data.AHCash5 = this.AHCash5;
                data.AHCash6 = this.AHCash6;
                data.AHCash7 = this.AHCash7;
                data.AHCash8 = this.AHCash8;
                data.AHCash9 = this.AHCash9;
                data.PaymentName = this.PaymentName;
                data.TotalViseChangeMoney = this.TotalViseChangeMoney;
                data.SumCode = this.SumCode;
                data.PaymentCodition = this.PaymentCodition;
                data.CheckRemark = this.CheckRemark;
                return data;
            }

            public TList<PaymentItem> PaymentItemCollection
            {
                get
                {
                    if (this.paymentItemPaymentCode == null)
                    {
                        this.paymentItemPaymentCode = new TList<PaymentItem>();
                    }
                    return this.paymentItemPaymentCode;
                }
                set
                {
                    this.paymentItemPaymentCode = value;
                }
            }
        }
    }
}

