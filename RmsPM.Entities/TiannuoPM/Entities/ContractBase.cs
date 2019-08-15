namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ContractBase : EntityBase, IEntityId<ContractKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ContractKey _entityId;
        private Project _projectCodeSource;
        private ISite _site;
        private ContractEntityData backupData;
        private ContractEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Contract> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractBase()
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new ContractEntityData();
            this.backupData = null;
        }

        public ContractBase(string contractContractCode, string contractContractID, string contractProjectCode, string contractContractName, string contractType, string contractSupplierCode, string contractSupplier2Code, string contractContractPerson, DateTime? contractContractDate, decimal? contractTotalMoney, DateTime? contractCreateDate, string contractCreatePerson, string contractLastModifyPerson, DateTime? contractLastModifyDate, string contractRemark, int? contractStatus, string contractCheckPerson, string contractCheckOpinion, DateTime? contractCheckDate, string contractContractObject, string contractUnitCode, string contractThirdParty, decimal? contractBeforeAccountTotalMoney, decimal? contractOldSumMoney, decimal? contractOriginalMoney, int? contractMostly, string contractBiddingCode, decimal? contractBudgetMoney, decimal? contractAdjustMoney, string contractDevelopUnit, string contractCreateMode, string contractWorkTime, string contractMarkSegment, string contractGroupName, string contractBuilding, string contractPayMode, string contractQualityRequire, string contractContractArea, string contractContractDefaultValueCode, decimal? contractBaoHan, string contractPerformingCircs, int? contractAccountStatus, int? contractAuditingStatus, int? contractChangeStatus, int? contractChangeCount, DateTime? contractWorkStartDate, DateTime? contractWorkEndDate, decimal? contractPerCash0, decimal? contractPerCash1, decimal? contractPerCash2, decimal? contractPerCash3, decimal? contractPerCash4, decimal? contractPerCash5, decimal? contractPerCash6, decimal? contractPerCash7, decimal? contractPerCash8, decimal? contractPerCash9, int? contractStampDutyID, decimal? contractStampDuty, DateTime? contractAdIssueDate, string contractMoneyType, decimal? contractExchangeRate)
        {
            this.inTxn = false;
            this._projectCodeSource = null;
            this._site = null;
            this.entityData = new ContractEntityData();
            this.backupData = null;
            this.ContractCode = contractContractCode;
            this.ContractID = contractContractID;
            this.ProjectCode = contractProjectCode;
            this.ContractName = contractContractName;
            this.Type = contractType;
            this.SupplierCode = contractSupplierCode;
            this.Supplier2Code = contractSupplier2Code;
            this.ContractPerson = contractContractPerson;
            this.ContractDate = contractContractDate;
            this.TotalMoney = contractTotalMoney;
            this.CreateDate = contractCreateDate;
            this.CreatePerson = contractCreatePerson;
            this.LastModifyPerson = contractLastModifyPerson;
            this.LastModifyDate = contractLastModifyDate;
            this.Remark = contractRemark;
            this.Status = contractStatus;
            this.CheckPerson = contractCheckPerson;
            this.CheckOpinion = contractCheckOpinion;
            this.CheckDate = contractCheckDate;
            this.ContractObject = contractContractObject;
            this.UnitCode = contractUnitCode;
            this.ThirdParty = contractThirdParty;
            this.BeforeAccountTotalMoney = contractBeforeAccountTotalMoney;
            this.OldSumMoney = contractOldSumMoney;
            this.OriginalMoney = contractOriginalMoney;
            this.Mostly = contractMostly;
            this.BiddingCode = contractBiddingCode;
            this.BudgetMoney = contractBudgetMoney;
            this.AdjustMoney = contractAdjustMoney;
            this.DevelopUnit = contractDevelopUnit;
            this.CreateMode = contractCreateMode;
            this.WorkTime = contractWorkTime;
            this.MarkSegment = contractMarkSegment;
            this.GroupName = contractGroupName;
            this.Building = contractBuilding;
            this.PayMode = contractPayMode;
            this.QualityRequire = contractQualityRequire;
            this.ContractArea = contractContractArea;
            this.ContractDefaultValueCode = contractContractDefaultValueCode;
            this.BaoHan = contractBaoHan;
            this.PerformingCircs = contractPerformingCircs;
            this.AccountStatus = contractAccountStatus;
            this.AuditingStatus = contractAuditingStatus;
            this.ChangeStatus = contractChangeStatus;
            this.ChangeCount = contractChangeCount;
            this.WorkStartDate = contractWorkStartDate;
            this.WorkEndDate = contractWorkEndDate;
            this.PerCash0 = contractPerCash0;
            this.PerCash1 = contractPerCash1;
            this.PerCash2 = contractPerCash2;
            this.PerCash3 = contractPerCash3;
            this.PerCash4 = contractPerCash4;
            this.PerCash5 = contractPerCash5;
            this.PerCash6 = contractPerCash6;
            this.PerCash7 = contractPerCash7;
            this.PerCash8 = contractPerCash8;
            this.PerCash9 = contractPerCash9;
            this.StampDutyID = contractStampDutyID;
            this.StampDuty = contractStampDuty;
            this.AdIssueDate = contractAdIssueDate;
            this.MoneyType = contractMoneyType;
            this.ExchangeRate = contractExchangeRate;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractName", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Type", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SupplierCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Supplier2Code", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CreatePerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("LastModifyPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckPerson", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CheckOpinion", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractObject", 500));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UnitCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ThirdParty", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BiddingCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DevelopUnit", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CreateMode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("WorkTime", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MarkSegment", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("GroupName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Building", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractArea", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractDefaultValueCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PerformingCircs", 500));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
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

        public virtual Contract Copy()
        {
            Contract contract = new Contract();
            contract.ContractCode = this.ContractCode;
            contract.OriginalContractCode = this.OriginalContractCode;
            contract.ContractID = this.ContractID;
            contract.ProjectCode = this.ProjectCode;
            contract.ContractName = this.ContractName;
            contract.Type = this.Type;
            contract.SupplierCode = this.SupplierCode;
            contract.Supplier2Code = this.Supplier2Code;
            contract.ContractPerson = this.ContractPerson;
            contract.ContractDate = this.ContractDate;
            contract.TotalMoney = this.TotalMoney;
            contract.CreateDate = this.CreateDate;
            contract.CreatePerson = this.CreatePerson;
            contract.LastModifyPerson = this.LastModifyPerson;
            contract.LastModifyDate = this.LastModifyDate;
            contract.Remark = this.Remark;
            contract.Status = this.Status;
            contract.CheckPerson = this.CheckPerson;
            contract.CheckOpinion = this.CheckOpinion;
            contract.CheckDate = this.CheckDate;
            contract.ContractObject = this.ContractObject;
            contract.UnitCode = this.UnitCode;
            contract.ThirdParty = this.ThirdParty;
            contract.BeforeAccountTotalMoney = this.BeforeAccountTotalMoney;
            contract.OldSumMoney = this.OldSumMoney;
            contract.OriginalMoney = this.OriginalMoney;
            contract.Mostly = this.Mostly;
            contract.BiddingCode = this.BiddingCode;
            contract.BudgetMoney = this.BudgetMoney;
            contract.AdjustMoney = this.AdjustMoney;
            contract.DevelopUnit = this.DevelopUnit;
            contract.CreateMode = this.CreateMode;
            contract.WorkTime = this.WorkTime;
            contract.MarkSegment = this.MarkSegment;
            contract.GroupName = this.GroupName;
            contract.Building = this.Building;
            contract.PayMode = this.PayMode;
            contract.QualityRequire = this.QualityRequire;
            contract.ContractArea = this.ContractArea;
            contract.ContractDefaultValueCode = this.ContractDefaultValueCode;
            contract.BaoHan = this.BaoHan;
            contract.PerformingCircs = this.PerformingCircs;
            contract.AccountStatus = this.AccountStatus;
            contract.AuditingStatus = this.AuditingStatus;
            contract.ChangeStatus = this.ChangeStatus;
            contract.ChangeCount = this.ChangeCount;
            contract.WorkStartDate = this.WorkStartDate;
            contract.WorkEndDate = this.WorkEndDate;
            contract.PerCash0 = this.PerCash0;
            contract.PerCash1 = this.PerCash1;
            contract.PerCash2 = this.PerCash2;
            contract.PerCash3 = this.PerCash3;
            contract.PerCash4 = this.PerCash4;
            contract.PerCash5 = this.PerCash5;
            contract.PerCash6 = this.PerCash6;
            contract.PerCash7 = this.PerCash7;
            contract.PerCash8 = this.PerCash8;
            contract.PerCash9 = this.PerCash9;
            contract.StampDutyID = this.StampDutyID;
            contract.StampDuty = this.StampDuty;
            contract.AdIssueDate = this.AdIssueDate;
            contract.MoneyType = this.MoneyType;
            contract.ExchangeRate = this.ExchangeRate;
            contract.AcceptChanges();
            return contract;
        }

        public static Contract CreateContract(string contractContractCode, string contractContractID, string contractProjectCode, string contractContractName, string contractType, string contractSupplierCode, string contractSupplier2Code, string contractContractPerson, DateTime? contractContractDate, decimal? contractTotalMoney, DateTime? contractCreateDate, string contractCreatePerson, string contractLastModifyPerson, DateTime? contractLastModifyDate, string contractRemark, int? contractStatus, string contractCheckPerson, string contractCheckOpinion, DateTime? contractCheckDate, string contractContractObject, string contractUnitCode, string contractThirdParty, decimal? contractBeforeAccountTotalMoney, decimal? contractOldSumMoney, decimal? contractOriginalMoney, int? contractMostly, string contractBiddingCode, decimal? contractBudgetMoney, decimal? contractAdjustMoney, string contractDevelopUnit, string contractCreateMode, string contractWorkTime, string contractMarkSegment, string contractGroupName, string contractBuilding, string contractPayMode, string contractQualityRequire, string contractContractArea, string contractContractDefaultValueCode, decimal? contractBaoHan, string contractPerformingCircs, int? contractAccountStatus, int? contractAuditingStatus, int? contractChangeStatus, int? contractChangeCount, DateTime? contractWorkStartDate, DateTime? contractWorkEndDate, decimal? contractPerCash0, decimal? contractPerCash1, decimal? contractPerCash2, decimal? contractPerCash3, decimal? contractPerCash4, decimal? contractPerCash5, decimal? contractPerCash6, decimal? contractPerCash7, decimal? contractPerCash8, decimal? contractPerCash9, int? contractStampDutyID, decimal? contractStampDuty, DateTime? contractAdIssueDate, string contractMoneyType, decimal? contractExchangeRate)
        {
            Contract contract = new Contract();
            contract.ContractCode = contractContractCode;
            contract.ContractID = contractContractID;
            contract.ProjectCode = contractProjectCode;
            contract.ContractName = contractContractName;
            contract.Type = contractType;
            contract.SupplierCode = contractSupplierCode;
            contract.Supplier2Code = contractSupplier2Code;
            contract.ContractPerson = contractContractPerson;
            contract.ContractDate = contractContractDate;
            contract.TotalMoney = contractTotalMoney;
            contract.CreateDate = contractCreateDate;
            contract.CreatePerson = contractCreatePerson;
            contract.LastModifyPerson = contractLastModifyPerson;
            contract.LastModifyDate = contractLastModifyDate;
            contract.Remark = contractRemark;
            contract.Status = contractStatus;
            contract.CheckPerson = contractCheckPerson;
            contract.CheckOpinion = contractCheckOpinion;
            contract.CheckDate = contractCheckDate;
            contract.ContractObject = contractContractObject;
            contract.UnitCode = contractUnitCode;
            contract.ThirdParty = contractThirdParty;
            contract.BeforeAccountTotalMoney = contractBeforeAccountTotalMoney;
            contract.OldSumMoney = contractOldSumMoney;
            contract.OriginalMoney = contractOriginalMoney;
            contract.Mostly = contractMostly;
            contract.BiddingCode = contractBiddingCode;
            contract.BudgetMoney = contractBudgetMoney;
            contract.AdjustMoney = contractAdjustMoney;
            contract.DevelopUnit = contractDevelopUnit;
            contract.CreateMode = contractCreateMode;
            contract.WorkTime = contractWorkTime;
            contract.MarkSegment = contractMarkSegment;
            contract.GroupName = contractGroupName;
            contract.Building = contractBuilding;
            contract.PayMode = contractPayMode;
            contract.QualityRequire = contractQualityRequire;
            contract.ContractArea = contractContractArea;
            contract.ContractDefaultValueCode = contractContractDefaultValueCode;
            contract.BaoHan = contractBaoHan;
            contract.PerformingCircs = contractPerformingCircs;
            contract.AccountStatus = contractAccountStatus;
            contract.AuditingStatus = contractAuditingStatus;
            contract.ChangeStatus = contractChangeStatus;
            contract.ChangeCount = contractChangeCount;
            contract.WorkStartDate = contractWorkStartDate;
            contract.WorkEndDate = contractWorkEndDate;
            contract.PerCash0 = contractPerCash0;
            contract.PerCash1 = contractPerCash1;
            contract.PerCash2 = contractPerCash2;
            contract.PerCash3 = contractPerCash3;
            contract.PerCash4 = contractPerCash4;
            contract.PerCash5 = contractPerCash5;
            contract.PerCash6 = contractPerCash6;
            contract.PerCash7 = contractPerCash7;
            contract.PerCash8 = contractPerCash8;
            contract.PerCash9 = contractPerCash9;
            contract.StampDutyID = contractStampDutyID;
            contract.StampDuty = contractStampDuty;
            contract.AdIssueDate = contractAdIssueDate;
            contract.MoneyType = contractMoneyType;
            contract.ExchangeRate = contractExchangeRate;
            return contract;
        }

        public virtual Contract DeepCopy()
        {
            return EntityHelper.Clone<Contract>(this as Contract);
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

        public virtual bool Equals(ContractBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractBase Object1, ContractBase Object2)
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
            if (Object1.ContractCode != Object2.ContractCode)
            {
                flag = false;
            }
            if ((Object1.ContractID != null) && (Object2.ContractID != null))
            {
                if (Object1.ContractID != Object2.ContractID)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractID == null) ^ (Object2.ContractID == null))
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
            if ((Object1.ContractName != null) && (Object2.ContractName != null))
            {
                if (Object1.ContractName != Object2.ContractName)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractName == null) ^ (Object2.ContractName == null))
            {
                flag = false;
            }
            if ((Object1.Type != null) && (Object2.Type != null))
            {
                if (Object1.Type != Object2.Type)
                {
                    flag = false;
                }
            }
            else if ((Object1.Type == null) ^ (Object2.Type == null))
            {
                flag = false;
            }
            if ((Object1.SupplierCode != null) && (Object2.SupplierCode != null))
            {
                if (Object1.SupplierCode != Object2.SupplierCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SupplierCode == null) ^ (Object2.SupplierCode == null))
            {
                flag = false;
            }
            if ((Object1.Supplier2Code != null) && (Object2.Supplier2Code != null))
            {
                if (Object1.Supplier2Code != Object2.Supplier2Code)
                {
                    flag = false;
                }
            }
            else if ((Object1.Supplier2Code == null) ^ (Object2.Supplier2Code == null))
            {
                flag = false;
            }
            if ((Object1.ContractPerson != null) && (Object2.ContractPerson != null))
            {
                if (Object1.ContractPerson != Object2.ContractPerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractPerson == null) ^ (Object2.ContractPerson == null))
            {
                flag = false;
            }
            if (Object1.ContractDate.HasValue && Object2.ContractDate.HasValue)
            {
                if (Object1.ContractDate != Object2.ContractDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.ContractDate.HasValue ^ !Object2.ContractDate.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalMoney.HasValue && Object2.TotalMoney.HasValue)
            {
                if (Object1.TotalMoney != Object2.TotalMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalMoney.HasValue ^ !Object2.TotalMoney.HasValue)
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
            if ((Object1.LastModifyPerson != null) && (Object2.LastModifyPerson != null))
            {
                if (Object1.LastModifyPerson != Object2.LastModifyPerson)
                {
                    flag = false;
                }
            }
            else if ((Object1.LastModifyPerson == null) ^ (Object2.LastModifyPerson == null))
            {
                flag = false;
            }
            if (Object1.LastModifyDate.HasValue && Object2.LastModifyDate.HasValue)
            {
                if (Object1.LastModifyDate != Object2.LastModifyDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.LastModifyDate.HasValue ^ !Object2.LastModifyDate.HasValue)
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
            if ((Object1.ContractObject != null) && (Object2.ContractObject != null))
            {
                if (Object1.ContractObject != Object2.ContractObject)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractObject == null) ^ (Object2.ContractObject == null))
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
            if ((Object1.ThirdParty != null) && (Object2.ThirdParty != null))
            {
                if (Object1.ThirdParty != Object2.ThirdParty)
                {
                    flag = false;
                }
            }
            else if ((Object1.ThirdParty == null) ^ (Object2.ThirdParty == null))
            {
                flag = false;
            }
            if (Object1.BeforeAccountTotalMoney.HasValue && Object2.BeforeAccountTotalMoney.HasValue)
            {
                if (Object1.BeforeAccountTotalMoney != Object2.BeforeAccountTotalMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.BeforeAccountTotalMoney.HasValue ^ !Object2.BeforeAccountTotalMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.OldSumMoney.HasValue && Object2.OldSumMoney.HasValue)
            {
                if (Object1.OldSumMoney != Object2.OldSumMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.OldSumMoney.HasValue ^ !Object2.OldSumMoney.HasValue)
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
            if (Object1.Mostly.HasValue && Object2.Mostly.HasValue)
            {
                if (Object1.Mostly != Object2.Mostly)
                {
                    flag = false;
                }
            }
            else if (!Object1.Mostly.HasValue ^ !Object2.Mostly.HasValue)
            {
                flag = false;
            }
            if ((Object1.BiddingCode != null) && (Object2.BiddingCode != null))
            {
                if (Object1.BiddingCode != Object2.BiddingCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.BiddingCode == null) ^ (Object2.BiddingCode == null))
            {
                flag = false;
            }
            if (Object1.BudgetMoney.HasValue && Object2.BudgetMoney.HasValue)
            {
                if (Object1.BudgetMoney != Object2.BudgetMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.BudgetMoney.HasValue ^ !Object2.BudgetMoney.HasValue)
            {
                flag = false;
            }
            if (Object1.AdjustMoney.HasValue && Object2.AdjustMoney.HasValue)
            {
                if (Object1.AdjustMoney != Object2.AdjustMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.AdjustMoney.HasValue ^ !Object2.AdjustMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.DevelopUnit != null) && (Object2.DevelopUnit != null))
            {
                if (Object1.DevelopUnit != Object2.DevelopUnit)
                {
                    flag = false;
                }
            }
            else if ((Object1.DevelopUnit == null) ^ (Object2.DevelopUnit == null))
            {
                flag = false;
            }
            if ((Object1.CreateMode != null) && (Object2.CreateMode != null))
            {
                if (Object1.CreateMode != Object2.CreateMode)
                {
                    flag = false;
                }
            }
            else if ((Object1.CreateMode == null) ^ (Object2.CreateMode == null))
            {
                flag = false;
            }
            if ((Object1.WorkTime != null) && (Object2.WorkTime != null))
            {
                if (Object1.WorkTime != Object2.WorkTime)
                {
                    flag = false;
                }
            }
            else if ((Object1.WorkTime == null) ^ (Object2.WorkTime == null))
            {
                flag = false;
            }
            if ((Object1.MarkSegment != null) && (Object2.MarkSegment != null))
            {
                if (Object1.MarkSegment != Object2.MarkSegment)
                {
                    flag = false;
                }
            }
            else if ((Object1.MarkSegment == null) ^ (Object2.MarkSegment == null))
            {
                flag = false;
            }
            if ((Object1.GroupName != null) && (Object2.GroupName != null))
            {
                if (Object1.GroupName != Object2.GroupName)
                {
                    flag = false;
                }
            }
            else if ((Object1.GroupName == null) ^ (Object2.GroupName == null))
            {
                flag = false;
            }
            if ((Object1.Building != null) && (Object2.Building != null))
            {
                if (Object1.Building != Object2.Building)
                {
                    flag = false;
                }
            }
            else if ((Object1.Building == null) ^ (Object2.Building == null))
            {
                flag = false;
            }
            if ((Object1.PayMode != null) && (Object2.PayMode != null))
            {
                if (Object1.PayMode != Object2.PayMode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PayMode == null) ^ (Object2.PayMode == null))
            {
                flag = false;
            }
            if ((Object1.QualityRequire != null) && (Object2.QualityRequire != null))
            {
                if (Object1.QualityRequire != Object2.QualityRequire)
                {
                    flag = false;
                }
            }
            else if ((Object1.QualityRequire == null) ^ (Object2.QualityRequire == null))
            {
                flag = false;
            }
            if ((Object1.ContractArea != null) && (Object2.ContractArea != null))
            {
                if (Object1.ContractArea != Object2.ContractArea)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractArea == null) ^ (Object2.ContractArea == null))
            {
                flag = false;
            }
            if ((Object1.ContractDefaultValueCode != null) && (Object2.ContractDefaultValueCode != null))
            {
                if (Object1.ContractDefaultValueCode != Object2.ContractDefaultValueCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractDefaultValueCode == null) ^ (Object2.ContractDefaultValueCode == null))
            {
                flag = false;
            }
            if (Object1.BaoHan.HasValue && Object2.BaoHan.HasValue)
            {
                if (Object1.BaoHan != Object2.BaoHan)
                {
                    flag = false;
                }
            }
            else if (!Object1.BaoHan.HasValue ^ !Object2.BaoHan.HasValue)
            {
                flag = false;
            }
            if ((Object1.PerformingCircs != null) && (Object2.PerformingCircs != null))
            {
                if (Object1.PerformingCircs != Object2.PerformingCircs)
                {
                    flag = false;
                }
            }
            else if ((Object1.PerformingCircs == null) ^ (Object2.PerformingCircs == null))
            {
                flag = false;
            }
            if (Object1.AccountStatus.HasValue && Object2.AccountStatus.HasValue)
            {
                if (Object1.AccountStatus != Object2.AccountStatus)
                {
                    flag = false;
                }
            }
            else if (!Object1.AccountStatus.HasValue ^ !Object2.AccountStatus.HasValue)
            {
                flag = false;
            }
            if (Object1.AuditingStatus.HasValue && Object2.AuditingStatus.HasValue)
            {
                if (Object1.AuditingStatus != Object2.AuditingStatus)
                {
                    flag = false;
                }
            }
            else if (!Object1.AuditingStatus.HasValue ^ !Object2.AuditingStatus.HasValue)
            {
                flag = false;
            }
            if (Object1.ChangeStatus.HasValue && Object2.ChangeStatus.HasValue)
            {
                if (Object1.ChangeStatus != Object2.ChangeStatus)
                {
                    flag = false;
                }
            }
            else if (!Object1.ChangeStatus.HasValue ^ !Object2.ChangeStatus.HasValue)
            {
                flag = false;
            }
            if (Object1.ChangeCount.HasValue && Object2.ChangeCount.HasValue)
            {
                if (Object1.ChangeCount != Object2.ChangeCount)
                {
                    flag = false;
                }
            }
            else if (!Object1.ChangeCount.HasValue ^ !Object2.ChangeCount.HasValue)
            {
                flag = false;
            }
            if (Object1.WorkStartDate.HasValue && Object2.WorkStartDate.HasValue)
            {
                if (Object1.WorkStartDate != Object2.WorkStartDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.WorkStartDate.HasValue ^ !Object2.WorkStartDate.HasValue)
            {
                flag = false;
            }
            if (Object1.WorkEndDate.HasValue && Object2.WorkEndDate.HasValue)
            {
                if (Object1.WorkEndDate != Object2.WorkEndDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.WorkEndDate.HasValue ^ !Object2.WorkEndDate.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash0.HasValue && Object2.PerCash0.HasValue)
            {
                if (Object1.PerCash0 != Object2.PerCash0)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash0.HasValue ^ !Object2.PerCash0.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash1.HasValue && Object2.PerCash1.HasValue)
            {
                if (Object1.PerCash1 != Object2.PerCash1)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash1.HasValue ^ !Object2.PerCash1.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash2.HasValue && Object2.PerCash2.HasValue)
            {
                if (Object1.PerCash2 != Object2.PerCash2)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash2.HasValue ^ !Object2.PerCash2.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash3.HasValue && Object2.PerCash3.HasValue)
            {
                if (Object1.PerCash3 != Object2.PerCash3)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash3.HasValue ^ !Object2.PerCash3.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash4.HasValue && Object2.PerCash4.HasValue)
            {
                if (Object1.PerCash4 != Object2.PerCash4)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash4.HasValue ^ !Object2.PerCash4.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash5.HasValue && Object2.PerCash5.HasValue)
            {
                if (Object1.PerCash5 != Object2.PerCash5)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash5.HasValue ^ !Object2.PerCash5.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash6.HasValue && Object2.PerCash6.HasValue)
            {
                if (Object1.PerCash6 != Object2.PerCash6)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash6.HasValue ^ !Object2.PerCash6.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash7.HasValue && Object2.PerCash7.HasValue)
            {
                if (Object1.PerCash7 != Object2.PerCash7)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash7.HasValue ^ !Object2.PerCash7.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash8.HasValue && Object2.PerCash8.HasValue)
            {
                if (Object1.PerCash8 != Object2.PerCash8)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash8.HasValue ^ !Object2.PerCash8.HasValue)
            {
                flag = false;
            }
            if (Object1.PerCash9.HasValue && Object2.PerCash9.HasValue)
            {
                if (Object1.PerCash9 != Object2.PerCash9)
                {
                    flag = false;
                }
            }
            else if (!Object1.PerCash9.HasValue ^ !Object2.PerCash9.HasValue)
            {
                flag = false;
            }
            if (Object1.StampDutyID.HasValue && Object2.StampDutyID.HasValue)
            {
                if (Object1.StampDutyID != Object2.StampDutyID)
                {
                    flag = false;
                }
            }
            else if (!Object1.StampDutyID.HasValue ^ !Object2.StampDutyID.HasValue)
            {
                flag = false;
            }
            if (Object1.StampDuty.HasValue && Object2.StampDuty.HasValue)
            {
                if (Object1.StampDuty != Object2.StampDuty)
                {
                    flag = false;
                }
            }
            else if (!Object1.StampDuty.HasValue ^ !Object2.StampDuty.HasValue)
            {
                flag = false;
            }
            if (Object1.AdIssueDate.HasValue && Object2.AdIssueDate.HasValue)
            {
                if (Object1.AdIssueDate != Object2.AdIssueDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.AdIssueDate.HasValue ^ !Object2.AdIssueDate.HasValue)
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
                return flag;
            }
            if (!Object1.ExchangeRate.HasValue ^ !Object2.ExchangeRate.HasValue)
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

        public void OnColumnChanged(ContractColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Contract);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractEntityData;
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
                    this.parentCollection.Remove((Contract) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{63}{62}- ContractCode: {0}{62}- ContractID: {1}{62}- ProjectCode: {2}{62}- ContractName: {3}{62}- Type: {4}{62}- SupplierCode: {5}{62}- Supplier2Code: {6}{62}- ContractPerson: {7}{62}- ContractDate: {8}{62}- TotalMoney: {9}{62}- CreateDate: {10}{62}- CreatePerson: {11}{62}- LastModifyPerson: {12}{62}- LastModifyDate: {13}{62}- Remark: {14}{62}- Status: {15}{62}- CheckPerson: {16}{62}- CheckOpinion: {17}{62}- CheckDate: {18}{62}- ContractObject: {19}{62}- UnitCode: {20}{62}- ThirdParty: {21}{62}- BeforeAccountTotalMoney: {22}{62}- OldSumMoney: {23}{62}- OriginalMoney: {24}{62}- Mostly: {25}{62}- BiddingCode: {26}{62}- BudgetMoney: {27}{62}- AdjustMoney: {28}{62}- DevelopUnit: {29}{62}- CreateMode: {30}{62}- WorkTime: {31}{62}- MarkSegment: {32}{62}- GroupName: {33}{62}- Building: {34}{62}- PayMode: {35}{62}- QualityRequire: {36}{62}- ContractArea: {37}{62}- ContractDefaultValueCode: {38}{62}- BaoHan: {39}{62}- PerformingCircs: {40}{62}- AccountStatus: {41}{62}- AuditingStatus: {42}{62}- ChangeStatus: {43}{62}- ChangeCount: {44}{62}- WorkStartDate: {45}{62}- WorkEndDate: {46}{62}- PerCash0: {47}{62}- PerCash1: {48}{62}- PerCash2: {49}{62}- PerCash3: {50}{62}- PerCash4: {51}{62}- PerCash5: {52}{62}- PerCash6: {53}{62}- PerCash7: {54}{62}- PerCash8: {55}{62}- PerCash9: {56}{62}- StampDutyID: {57}{62}- StampDuty: {58}{62}- AdIssueDate: {59}{62}- MoneyType: {60}{62}- ExchangeRate: {61}{62}", new object[] { 
                this.ContractCode, (this.ContractID == null) ? string.Empty : this.ContractID.ToString(), (this.ProjectCode == null) ? string.Empty : this.ProjectCode.ToString(), (this.ContractName == null) ? string.Empty : this.ContractName.ToString(), (this.Type == null) ? string.Empty : this.Type.ToString(), (this.SupplierCode == null) ? string.Empty : this.SupplierCode.ToString(), (this.Supplier2Code == null) ? string.Empty : this.Supplier2Code.ToString(), (this.ContractPerson == null) ? string.Empty : this.ContractPerson.ToString(), !this.ContractDate.HasValue ? string.Empty : this.ContractDate.ToString(), !this.TotalMoney.HasValue ? string.Empty : this.TotalMoney.ToString(), !this.CreateDate.HasValue ? string.Empty : this.CreateDate.ToString(), (this.CreatePerson == null) ? string.Empty : this.CreatePerson.ToString(), (this.LastModifyPerson == null) ? string.Empty : this.LastModifyPerson.ToString(), !this.LastModifyDate.HasValue ? string.Empty : this.LastModifyDate.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), 
                (this.CheckPerson == null) ? string.Empty : this.CheckPerson.ToString(), (this.CheckOpinion == null) ? string.Empty : this.CheckOpinion.ToString(), !this.CheckDate.HasValue ? string.Empty : this.CheckDate.ToString(), (this.ContractObject == null) ? string.Empty : this.ContractObject.ToString(), (this.UnitCode == null) ? string.Empty : this.UnitCode.ToString(), (this.ThirdParty == null) ? string.Empty : this.ThirdParty.ToString(), !this.BeforeAccountTotalMoney.HasValue ? string.Empty : this.BeforeAccountTotalMoney.ToString(), !this.OldSumMoney.HasValue ? string.Empty : this.OldSumMoney.ToString(), !this.OriginalMoney.HasValue ? string.Empty : this.OriginalMoney.ToString(), !this.Mostly.HasValue ? string.Empty : this.Mostly.ToString(), (this.BiddingCode == null) ? string.Empty : this.BiddingCode.ToString(), !this.BudgetMoney.HasValue ? string.Empty : this.BudgetMoney.ToString(), !this.AdjustMoney.HasValue ? string.Empty : this.AdjustMoney.ToString(), (this.DevelopUnit == null) ? string.Empty : this.DevelopUnit.ToString(), (this.CreateMode == null) ? string.Empty : this.CreateMode.ToString(), (this.WorkTime == null) ? string.Empty : this.WorkTime.ToString(), 
                (this.MarkSegment == null) ? string.Empty : this.MarkSegment.ToString(), (this.GroupName == null) ? string.Empty : this.GroupName.ToString(), (this.Building == null) ? string.Empty : this.Building.ToString(), (this.PayMode == null) ? string.Empty : this.PayMode.ToString(), (this.QualityRequire == null) ? string.Empty : this.QualityRequire.ToString(), (this.ContractArea == null) ? string.Empty : this.ContractArea.ToString(), (this.ContractDefaultValueCode == null) ? string.Empty : this.ContractDefaultValueCode.ToString(), !this.BaoHan.HasValue ? string.Empty : this.BaoHan.ToString(), (this.PerformingCircs == null) ? string.Empty : this.PerformingCircs.ToString(), !this.AccountStatus.HasValue ? string.Empty : this.AccountStatus.ToString(), !this.AuditingStatus.HasValue ? string.Empty : this.AuditingStatus.ToString(), !this.ChangeStatus.HasValue ? string.Empty : this.ChangeStatus.ToString(), !this.ChangeCount.HasValue ? string.Empty : this.ChangeCount.ToString(), !this.WorkStartDate.HasValue ? string.Empty : this.WorkStartDate.ToString(), !this.WorkEndDate.HasValue ? string.Empty : this.WorkEndDate.ToString(), !this.PerCash0.HasValue ? string.Empty : this.PerCash0.ToString(), 
                !this.PerCash1.HasValue ? string.Empty : this.PerCash1.ToString(), !this.PerCash2.HasValue ? string.Empty : this.PerCash2.ToString(), !this.PerCash3.HasValue ? string.Empty : this.PerCash3.ToString(), !this.PerCash4.HasValue ? string.Empty : this.PerCash4.ToString(), !this.PerCash5.HasValue ? string.Empty : this.PerCash5.ToString(), !this.PerCash6.HasValue ? string.Empty : this.PerCash6.ToString(), !this.PerCash7.HasValue ? string.Empty : this.PerCash7.ToString(), !this.PerCash8.HasValue ? string.Empty : this.PerCash8.ToString(), !this.PerCash9.HasValue ? string.Empty : this.PerCash9.ToString(), !this.StampDutyID.HasValue ? string.Empty : this.StampDutyID.ToString(), !this.StampDuty.HasValue ? string.Empty : this.StampDuty.ToString(), !this.AdIssueDate.HasValue ? string.Empty : this.AdIssueDate.ToString(), (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [Description("结算状态"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual int? AccountStatus
        {
            get
            {
                return this.entityData.AccountStatus;
            }
            set
            {
                if (this.entityData.AccountStatus != value)
                {
                    this.OnColumnChanging(ContractColumn.AccountStatus, this.entityData.AccountStatus);
                    this.entityData.AccountStatus = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.AccountStatus, this.entityData.AccountStatus);
                    this.OnPropertyChanged("AccountStatus");
                }
            }
        }

        [Description("广告发布日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? AdIssueDate
        {
            get
            {
                return this.entityData.AdIssueDate;
            }
            set
            {
                if (this.entityData.AdIssueDate != value)
                {
                    this.OnColumnChanging(ContractColumn.AdIssueDate, this.entityData.AdIssueDate);
                    this.entityData.AdIssueDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.AdIssueDate, this.entityData.AdIssueDate);
                    this.OnPropertyChanged("AdIssueDate");
                }
            }
        }

        [Description("暂定金额/指定金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? AdjustMoney
        {
            get
            {
                return this.entityData.AdjustMoney;
            }
            set
            {
                if (this.entityData.AdjustMoney != value)
                {
                    this.OnColumnChanging(ContractColumn.AdjustMoney, this.entityData.AdjustMoney);
                    this.entityData.AdjustMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.AdjustMoney, this.entityData.AdjustMoney);
                    this.OnPropertyChanged("AdjustMoney");
                }
            }
        }

        [DataObjectField(false, false, true), Description("审核状态"), TiannuoPM.Entities.Bindable]
        public virtual int? AuditingStatus
        {
            get
            {
                return this.entityData.AuditingStatus;
            }
            set
            {
                if (this.entityData.AuditingStatus != value)
                {
                    this.OnColumnChanging(ContractColumn.AuditingStatus, this.entityData.AuditingStatus);
                    this.entityData.AuditingStatus = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.AuditingStatus, this.entityData.AuditingStatus);
                    this.OnPropertyChanged("AuditingStatus");
                }
            }
        }

        [Description("保函"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? BaoHan
        {
            get
            {
                return this.entityData.BaoHan;
            }
            set
            {
                if (this.entityData.BaoHan != value)
                {
                    this.OnColumnChanging(ContractColumn.BaoHan, this.entityData.BaoHan);
                    this.entityData.BaoHan = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.BaoHan, this.entityData.BaoHan);
                    this.OnPropertyChanged("BaoHan");
                }
            }
        }

        [Description("结算前金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? BeforeAccountTotalMoney
        {
            get
            {
                return this.entityData.BeforeAccountTotalMoney;
            }
            set
            {
                if (this.entityData.BeforeAccountTotalMoney != value)
                {
                    this.OnColumnChanging(ContractColumn.BeforeAccountTotalMoney, this.entityData.BeforeAccountTotalMoney);
                    this.entityData.BeforeAccountTotalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.BeforeAccountTotalMoney, this.entityData.BeforeAccountTotalMoney);
                    this.OnPropertyChanged("BeforeAccountTotalMoney");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("招标项目编号 Bidding.BiddingCode"), TiannuoPM.Entities.Bindable]
        public virtual string BiddingCode
        {
            get
            {
                return this.entityData.BiddingCode;
            }
            set
            {
                if (this.entityData.BiddingCode != value)
                {
                    this.OnColumnChanging(ContractColumn.BiddingCode, this.entityData.BiddingCode);
                    this.entityData.BiddingCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.BiddingCode, this.entityData.BiddingCode);
                    this.OnPropertyChanged("BiddingCode");
                }
            }
        }

        [DataObjectField(false, false, true), Description("原合同金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? BudgetMoney
        {
            get
            {
                return this.entityData.BudgetMoney;
            }
            set
            {
                if (this.entityData.BudgetMoney != value)
                {
                    this.OnColumnChanging(ContractColumn.BudgetMoney, this.entityData.BudgetMoney);
                    this.entityData.BudgetMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.BudgetMoney, this.entityData.BudgetMoney);
                    this.OnPropertyChanged("BudgetMoney");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("幢（楼栋号）"), DataObjectField(false, false, true, 50)]
        public virtual string Building
        {
            get
            {
                return this.entityData.Building;
            }
            set
            {
                if (this.entityData.Building != value)
                {
                    this.OnColumnChanging(ContractColumn.Building, this.entityData.Building);
                    this.entityData.Building = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Building, this.entityData.Building);
                    this.OnPropertyChanged("Building");
                }
            }
        }

        [DataObjectField(false, false, true), Description("累计变更次数"), TiannuoPM.Entities.Bindable]
        public virtual int? ChangeCount
        {
            get
            {
                return this.entityData.ChangeCount;
            }
            set
            {
                if (this.entityData.ChangeCount != value)
                {
                    this.OnColumnChanging(ContractColumn.ChangeCount, this.entityData.ChangeCount);
                    this.entityData.ChangeCount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ChangeCount, this.entityData.ChangeCount);
                    this.OnPropertyChanged("ChangeCount");
                }
            }
        }

        [Description("变更状态 0 无变更 1 变更申请 2 已变更 3 变更审核中"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual int? ChangeStatus
        {
            get
            {
                return this.entityData.ChangeStatus;
            }
            set
            {
                if (this.entityData.ChangeStatus != value)
                {
                    this.OnColumnChanging(ContractColumn.ChangeStatus, this.entityData.ChangeStatus);
                    this.entityData.ChangeStatus = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ChangeStatus, this.entityData.ChangeStatus);
                    this.OnPropertyChanged("ChangeStatus");
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
                    this.OnColumnChanging(ContractColumn.CheckDate, this.entityData.CheckDate);
                    this.entityData.CheckDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CheckDate, this.entityData.CheckDate);
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
                    this.OnColumnChanging(ContractColumn.CheckOpinion, this.entityData.CheckOpinion);
                    this.entityData.CheckOpinion = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CheckOpinion, this.entityData.CheckOpinion);
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
                    this.OnColumnChanging(ContractColumn.CheckPerson, this.entityData.CheckPerson);
                    this.entityData.CheckPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CheckPerson, this.entityData.CheckPerson);
                    this.OnPropertyChanged("CheckPerson");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractAccount> ContractAccountCollection
        {
            get
            {
                return this.entityData.ContractAccountCollection;
            }
            set
            {
                this.entityData.ContractAccountCollection = value;
            }
        }

        [Description("承包范围"), DataObjectField(false, false, true, 800), TiannuoPM.Entities.Bindable]
        public virtual string ContractArea
        {
            get
            {
                return this.entityData.ContractArea;
            }
            set
            {
                if (this.entityData.ContractArea != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractArea, this.entityData.ContractArea);
                    this.entityData.ContractArea = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractArea, this.entityData.ContractArea);
                    this.OnPropertyChanged("ContractArea");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractBill> ContractBillCollection
        {
            get
            {
                return this.entityData.ContractBillCollection;
            }
            set
            {
                this.entityData.ContractBillCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractChange> ContractChangeCollection
        {
            get
            {
                return this.entityData.ContractChangeCollection;
            }
            set
            {
                this.entityData.ContractChangeCollection = value;
            }
        }

        [Description("合同code(主键)"), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
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
                    this.OnColumnChanging(ContractColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    this.EntityId.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractCost> ContractCostCollection
        {
            get
            {
                return this.entityData.ContractCostCollection;
            }
            set
            {
                this.entityData.ContractCostCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractCostPlan> ContractCostPlanCollection
        {
            get
            {
                return this.entityData.ContractCostPlanCollection;
            }
            set
            {
                this.entityData.ContractCostPlanCollection = value;
            }
        }

        [Description("合同日期"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? ContractDate
        {
            get
            {
                return this.entityData.ContractDate;
            }
            set
            {
                if (this.entityData.ContractDate != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractDate, this.entityData.ContractDate);
                    this.entityData.ContractDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractDate, this.entityData.ContractDate);
                    this.OnPropertyChanged("ContractDate");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("中标通知书编号 BiddingMessage.BiddingMessageCode"), TiannuoPM.Entities.Bindable]
        public virtual string ContractDefaultValueCode
        {
            get
            {
                return this.entityData.ContractDefaultValueCode;
            }
            set
            {
                if (this.entityData.ContractDefaultValueCode != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractDefaultValueCode, this.entityData.ContractDefaultValueCode);
                    this.entityData.ContractDefaultValueCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractDefaultValueCode, this.entityData.ContractDefaultValueCode);
                    this.OnPropertyChanged("ContractDefaultValueCode");
                }
            }
        }

        [Description("合同编号"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ContractID
        {
            get
            {
                return this.entityData.ContractID;
            }
            set
            {
                if (this.entityData.ContractID != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractID, this.entityData.ContractID);
                    this.entityData.ContractID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractID, this.entityData.ContractID);
                    this.OnPropertyChanged("ContractID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<ContractMaterial> ContractMaterialCollection
        {
            get
            {
                return this.entityData.ContractMaterialCollection;
            }
            set
            {
                this.entityData.ContractMaterialCollection = value;
            }
        }

        [Description("合同名称"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 200)]
        public virtual string ContractName
        {
            get
            {
                return this.entityData.ContractName;
            }
            set
            {
                if (this.entityData.ContractName != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractName, this.entityData.ContractName);
                    this.entityData.ContractName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractName, this.entityData.ContractName);
                    this.OnPropertyChanged("ContractName");
                }
            }
        }

        [Description("合同概述"), DataObjectField(false, false, true, 500), TiannuoPM.Entities.Bindable]
        public virtual string ContractObject
        {
            get
            {
                return this.entityData.ContractObject;
            }
            set
            {
                if (this.entityData.ContractObject != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractObject, this.entityData.ContractObject);
                    this.entityData.ContractObject = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractObject, this.entityData.ContractObject);
                    this.OnPropertyChanged("ContractObject");
                }
            }
        }

        [Description("经办人"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ContractPerson
        {
            get
            {
                return this.entityData.ContractPerson;
            }
            set
            {
                if (this.entityData.ContractPerson != value)
                {
                    this.OnColumnChanging(ContractColumn.ContractPerson, this.entityData.ContractPerson);
                    this.entityData.ContractPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ContractPerson, this.entityData.ContractPerson);
                    this.OnPropertyChanged("ContractPerson");
                }
            }
        }

        [Description("创建日期"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(ContractColumn.CreateDate, this.entityData.CreateDate);
                    this.entityData.CreateDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CreateDate, this.entityData.CreateDate);
                    this.OnPropertyChanged("CreateDate");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("形成方式 招标 比价 委托"), TiannuoPM.Entities.Bindable]
        public virtual string CreateMode
        {
            get
            {
                return this.entityData.CreateMode;
            }
            set
            {
                if (this.entityData.CreateMode != value)
                {
                    this.OnColumnChanging(ContractColumn.CreateMode, this.entityData.CreateMode);
                    this.entityData.CreateMode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CreateMode, this.entityData.CreateMode);
                    this.OnPropertyChanged("CreateMode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("创建者"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractColumn.CreatePerson, this.entityData.CreatePerson);
                    this.entityData.CreatePerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.CreatePerson, this.entityData.CreatePerson);
                    this.OnPropertyChanged("CreatePerson");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("开发单位"), TiannuoPM.Entities.Bindable]
        public virtual string DevelopUnit
        {
            get
            {
                return this.entityData.DevelopUnit;
            }
            set
            {
                if (this.entityData.DevelopUnit != value)
                {
                    this.OnColumnChanging(ContractColumn.DevelopUnit, this.entityData.DevelopUnit);
                    this.entityData.DevelopUnit = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.DevelopUnit, this.entityData.DevelopUnit);
                    this.OnPropertyChanged("DevelopUnit");
                }
            }
        }

        [XmlIgnore]
        public ContractKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractKey(this);
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
                    this.entityTrackingKey = "Contract" + this.ContractCode.ToString();
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
                    this.OnColumnChanging(ContractColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [Description("组团"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string GroupName
        {
            get
            {
                return this.entityData.GroupName;
            }
            set
            {
                if (this.entityData.GroupName != value)
                {
                    this.OnColumnChanging(ContractColumn.GroupName, this.entityData.GroupName);
                    this.entityData.GroupName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.GroupName, this.entityData.GroupName);
                    this.OnPropertyChanged("GroupName");
                }
            }
        }

        [Description("最后修改日期"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? LastModifyDate
        {
            get
            {
                return this.entityData.LastModifyDate;
            }
            set
            {
                if (this.entityData.LastModifyDate != value)
                {
                    this.OnColumnChanging(ContractColumn.LastModifyDate, this.entityData.LastModifyDate);
                    this.entityData.LastModifyDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.LastModifyDate, this.entityData.LastModifyDate);
                    this.OnPropertyChanged("LastModifyDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("最后修改者"), DataObjectField(false, false, true, 50)]
        public virtual string LastModifyPerson
        {
            get
            {
                return this.entityData.LastModifyPerson;
            }
            set
            {
                if (this.entityData.LastModifyPerson != value)
                {
                    this.OnColumnChanging(ContractColumn.LastModifyPerson, this.entityData.LastModifyPerson);
                    this.entityData.LastModifyPerson = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.LastModifyPerson, this.entityData.LastModifyPerson);
                    this.OnPropertyChanged("LastModifyPerson");
                }
            }
        }

        [Description("期"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string MarkSegment
        {
            get
            {
                return this.entityData.MarkSegment;
            }
            set
            {
                if (this.entityData.MarkSegment != value)
                {
                    this.OnColumnChanging(ContractColumn.MarkSegment, this.entityData.MarkSegment);
                    this.entityData.MarkSegment = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.MarkSegment, this.entityData.MarkSegment);
                    this.OnPropertyChanged("MarkSegment");
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
                    this.OnColumnChanging(ContractColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [DataObjectField(false, false, true), Description("主要标段"), TiannuoPM.Entities.Bindable]
        public virtual int? Mostly
        {
            get
            {
                return this.entityData.Mostly;
            }
            set
            {
                if (this.entityData.Mostly != value)
                {
                    this.OnColumnChanging(ContractColumn.Mostly, this.entityData.Mostly);
                    this.entityData.Mostly = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Mostly, this.entityData.Mostly);
                    this.OnPropertyChanged("Mostly");
                }
            }
        }

        [DataObjectField(false, false, true), Description("原合同总额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? OldSumMoney
        {
            get
            {
                return this.entityData.OldSumMoney;
            }
            set
            {
                if (this.entityData.OldSumMoney != value)
                {
                    this.OnColumnChanging(ContractColumn.OldSumMoney, this.entityData.OldSumMoney);
                    this.entityData.OldSumMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.OldSumMoney, this.entityData.OldSumMoney);
                    this.OnPropertyChanged("OldSumMoney");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractCode
        {
            get
            {
                return this.entityData.OriginalContractCode;
            }
            set
            {
                this.entityData.OriginalContractCode = value;
            }
        }

        [Description("原合同金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.entityData.OriginalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.OriginalMoney, this.entityData.OriginalMoney);
                    this.OnPropertyChanged("OriginalMoney");
                }
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
                this.parentCollection = value as TList<Contract>;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<Payment> PaymentCollection
        {
            get
            {
                return this.entityData.PaymentCollection;
            }
            set
            {
                this.entityData.PaymentCollection = value;
            }
        }

        [Description("付款方式"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual string PayMode
        {
            get
            {
                return this.entityData.PayMode;
            }
            set
            {
                if (this.entityData.PayMode != value)
                {
                    this.OnColumnChanging(ContractColumn.PayMode, this.entityData.PayMode);
                    this.entityData.PayMode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PayMode, this.entityData.PayMode);
                    this.OnPropertyChanged("PayMode");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash0
        {
            get
            {
                return this.entityData.PerCash0;
            }
            set
            {
                if (this.entityData.PerCash0 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash0, this.entityData.PerCash0);
                    this.entityData.PerCash0 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash0, this.entityData.PerCash0);
                    this.OnPropertyChanged("PerCash0");
                }
            }
        }

        [DataObjectField(false, false, true), Description("预付款"), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash1
        {
            get
            {
                return this.entityData.PerCash1;
            }
            set
            {
                if (this.entityData.PerCash1 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash1, this.entityData.PerCash1);
                    this.entityData.PerCash1 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash1, this.entityData.PerCash1);
                    this.OnPropertyChanged("PerCash1");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("验收款"), DataObjectField(false, false, true)]
        public virtual decimal? PerCash2
        {
            get
            {
                return this.entityData.PerCash2;
            }
            set
            {
                if (this.entityData.PerCash2 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash2, this.entityData.PerCash2);
                    this.entityData.PerCash2 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash2, this.entityData.PerCash2);
                    this.OnPropertyChanged("PerCash2");
                }
            }
        }

        [Description("保修款"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? PerCash3
        {
            get
            {
                return this.entityData.PerCash3;
            }
            set
            {
                if (this.entityData.PerCash3 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash3, this.entityData.PerCash3);
                    this.entityData.PerCash3 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash3, this.entityData.PerCash3);
                    this.OnPropertyChanged("PerCash3");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash4
        {
            get
            {
                return this.entityData.PerCash4;
            }
            set
            {
                if (this.entityData.PerCash4 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash4, this.entityData.PerCash4);
                    this.entityData.PerCash4 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash4, this.entityData.PerCash4);
                    this.OnPropertyChanged("PerCash4");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash5
        {
            get
            {
                return this.entityData.PerCash5;
            }
            set
            {
                if (this.entityData.PerCash5 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash5, this.entityData.PerCash5);
                    this.entityData.PerCash5 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash5, this.entityData.PerCash5);
                    this.OnPropertyChanged("PerCash5");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash6
        {
            get
            {
                return this.entityData.PerCash6;
            }
            set
            {
                if (this.entityData.PerCash6 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash6, this.entityData.PerCash6);
                    this.entityData.PerCash6 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash6, this.entityData.PerCash6);
                    this.OnPropertyChanged("PerCash6");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash7
        {
            get
            {
                return this.entityData.PerCash7;
            }
            set
            {
                if (this.entityData.PerCash7 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash7, this.entityData.PerCash7);
                    this.entityData.PerCash7 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash7, this.entityData.PerCash7);
                    this.OnPropertyChanged("PerCash7");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash8
        {
            get
            {
                return this.entityData.PerCash8;
            }
            set
            {
                if (this.entityData.PerCash8 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash8, this.entityData.PerCash8);
                    this.entityData.PerCash8 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash8, this.entityData.PerCash8);
                    this.OnPropertyChanged("PerCash8");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? PerCash9
        {
            get
            {
                return this.entityData.PerCash9;
            }
            set
            {
                if (this.entityData.PerCash9 != value)
                {
                    this.OnColumnChanging(ContractColumn.PerCash9, this.entityData.PerCash9);
                    this.entityData.PerCash9 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerCash9, this.entityData.PerCash9);
                    this.OnPropertyChanged("PerCash9");
                }
            }
        }

        [DataObjectField(false, false, true, 500), Description("履行情况"), TiannuoPM.Entities.Bindable]
        public virtual string PerformingCircs
        {
            get
            {
                return this.entityData.PerformingCircs;
            }
            set
            {
                if (this.entityData.PerformingCircs != value)
                {
                    this.OnColumnChanging(ContractColumn.PerformingCircs, this.entityData.PerformingCircs);
                    this.entityData.PerformingCircs = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.PerformingCircs, this.entityData.PerformingCircs);
                    this.OnPropertyChanged("PerformingCircs");
                }
            }
        }

        [Description("项目code(外键)"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
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
                    this.OnColumnChanging(ContractColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ProjectCode, this.entityData.ProjectCode);
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

        [Description("质量要求及保修约定"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual string QualityRequire
        {
            get
            {
                return this.entityData.QualityRequire;
            }
            set
            {
                if (this.entityData.QualityRequire != value)
                {
                    this.OnColumnChanging(ContractColumn.QualityRequire, this.entityData.QualityRequire);
                    this.entityData.QualityRequire = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.QualityRequire, this.entityData.QualityRequire);
                    this.OnPropertyChanged("QualityRequire");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description("备注"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
            }
        }

        [Browsable(false), XmlIgnore, SoapIgnore]
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

        [DataObjectField(false, false, true), Description("印花税金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? StampDuty
        {
            get
            {
                return this.entityData.StampDuty;
            }
            set
            {
                if (this.entityData.StampDuty != value)
                {
                    this.OnColumnChanging(ContractColumn.StampDuty, this.entityData.StampDuty);
                    this.entityData.StampDuty = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.StampDuty, this.entityData.StampDuty);
                    this.OnPropertyChanged("StampDuty");
                }
            }
        }

        [DataObjectField(false, false, true), Description("印花税率编号"), TiannuoPM.Entities.Bindable]
        public virtual int? StampDutyID
        {
            get
            {
                return this.entityData.StampDutyID;
            }
            set
            {
                if (this.entityData.StampDutyID != value)
                {
                    this.OnColumnChanging(ContractColumn.StampDutyID, this.entityData.StampDutyID);
                    this.entityData.StampDutyID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.StampDutyID, this.entityData.StampDutyID);
                    this.OnPropertyChanged("StampDutyID");
                }
            }
        }

        [Description("状态  0 正常 已审  1 待审核 申请  2 合同结算完毕，结束 已结  3 申请不通过 作废  4 变更申请 变更 6 历史记录 历史   7 合同申请中 审核中 8 已评审 9 评审中"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
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
                    this.OnColumnChanging(ContractColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("第二供应商编号"), DataObjectField(false, false, true, 50)]
        public virtual string Supplier2Code
        {
            get
            {
                return this.entityData.Supplier2Code;
            }
            set
            {
                if (this.entityData.Supplier2Code != value)
                {
                    this.OnColumnChanging(ContractColumn.Supplier2Code, this.entityData.Supplier2Code);
                    this.entityData.Supplier2Code = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Supplier2Code, this.entityData.Supplier2Code);
                    this.OnPropertyChanged("Supplier2Code");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("供应商编号"), TiannuoPM.Entities.Bindable]
        public virtual string SupplierCode
        {
            get
            {
                return this.entityData.SupplierCode;
            }
            set
            {
                if (this.entityData.SupplierCode != value)
                {
                    this.OnColumnChanging(ContractColumn.SupplierCode, this.entityData.SupplierCode);
                    this.entityData.SupplierCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.SupplierCode, this.entityData.SupplierCode);
                    this.OnPropertyChanged("SupplierCode");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "ContractCode", "ContractID", "ProjectCode", "ContractName", "Type", "SupplierCode", "Supplier2Code", "ContractPerson", "ContractDate", "TotalMoney", "CreateDate", "CreatePerson", "LastModifyPerson", "LastModifyDate", "Remark", "Status", 
                    "CheckPerson", "CheckOpinion", "CheckDate", "ContractObject", "UnitCode", "ThirdParty", "BeforeAccountTotalMoney", "oldSumMoney", "OriginalMoney", "Mostly", "BiddingCode", "BudgetMoney", "AdjustMoney", "DevelopUnit", "CreateMode", "WorkTime", 
                    "MarkSegment", "GroupName", "Building", "PayMode", "QualityRequire", "ContractArea", "ContractDefaultValueCode", "BaoHan", "PerformingCircs", "AccountStatus", "AuditingStatus", "ChangeStatus", "ChangeCount", "WorkStartDate", "WorkEndDate", "PerCash0", 
                    "PerCash1", "PerCash2", "PerCash3", "PerCash4", "PerCash5", "PerCash6", "PerCash7", "PerCash8", "PerCash9", "StampDutyID", "StampDuty", "AdIssueDate", "MoneyType", "ExchangeRate"
                 };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "Contract";
            }
        }

        [Description("第三方"), DataObjectField(false, false, true, 200), TiannuoPM.Entities.Bindable]
        public virtual string ThirdParty
        {
            get
            {
                return this.entityData.ThirdParty;
            }
            set
            {
                if (this.entityData.ThirdParty != value)
                {
                    this.OnColumnChanging(ContractColumn.ThirdParty, this.entityData.ThirdParty);
                    this.entityData.ThirdParty = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.ThirdParty, this.entityData.ThirdParty);
                    this.OnPropertyChanged("ThirdParty");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("总金额"), DataObjectField(false, false, true)]
        public virtual decimal? TotalMoney
        {
            get
            {
                return this.entityData.TotalMoney;
            }
            set
            {
                if (this.entityData.TotalMoney != value)
                {
                    this.OnColumnChanging(ContractColumn.TotalMoney, this.entityData.TotalMoney);
                    this.entityData.TotalMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.TotalMoney, this.entityData.TotalMoney);
                    this.OnPropertyChanged("TotalMoney");
                }
            }
        }

        [Description("类型"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Type
        {
            get
            {
                return this.entityData.Type;
            }
            set
            {
                if (this.entityData.Type != value)
                {
                    this.OnColumnChanging(ContractColumn.Type, this.entityData.Type);
                    this.entityData.Type = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.Type, this.entityData.Type);
                    this.OnPropertyChanged("Type");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("部门"), TiannuoPM.Entities.Bindable]
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
                    this.OnColumnChanging(ContractColumn.UnitCode, this.entityData.UnitCode);
                    this.entityData.UnitCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.UnitCode, this.entityData.UnitCode);
                    this.OnPropertyChanged("UnitCode");
                }
            }
        }

        [Description("工作结束日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? WorkEndDate
        {
            get
            {
                return this.entityData.WorkEndDate;
            }
            set
            {
                if (this.entityData.WorkEndDate != value)
                {
                    this.OnColumnChanging(ContractColumn.WorkEndDate, this.entityData.WorkEndDate);
                    this.entityData.WorkEndDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.WorkEndDate, this.entityData.WorkEndDate);
                    this.OnPropertyChanged("WorkEndDate");
                }
            }
        }

        [Description("工作开始日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? WorkStartDate
        {
            get
            {
                return this.entityData.WorkStartDate;
            }
            set
            {
                if (this.entityData.WorkStartDate != value)
                {
                    this.OnColumnChanging(ContractColumn.WorkStartDate, this.entityData.WorkStartDate);
                    this.entityData.WorkStartDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.WorkStartDate, this.entityData.WorkStartDate);
                    this.OnPropertyChanged("WorkStartDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("工期"), DataObjectField(false, false, true, 50)]
        public virtual string WorkTime
        {
            get
            {
                return this.entityData.WorkTime;
            }
            set
            {
                if (this.entityData.WorkTime != value)
                {
                    this.OnColumnChanging(ContractColumn.WorkTime, this.entityData.WorkTime);
                    this.entityData.WorkTime = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractColumn.WorkTime, this.entityData.WorkTime);
                    this.OnPropertyChanged("WorkTime");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractEntityData : ICloneable
        {
            public int? AccountStatus = null;
            public DateTime? AdIssueDate = null;
            public decimal? AdjustMoney = null;
            public int? AuditingStatus = null;
            public decimal? BaoHan = null;
            public decimal? BeforeAccountTotalMoney = null;
            public string BiddingCode = null;
            public decimal? BudgetMoney = null;
            public string Building = null;
            public int? ChangeCount = null;
            public int? ChangeStatus = null;
            public DateTime? CheckDate = null;
            public string CheckOpinion = null;
            public string CheckPerson = null;
            private TList<ContractAccount> contractAccountContractCode;
            public string ContractArea = null;
            private TList<ContractBill> contractBillContractCode;
            private TList<ContractChange> contractChangeContractCode;
            public string ContractCode;
            private TList<ContractCost> contractCostContractCode;
            private TList<ContractCostPlan> contractCostPlanContractCode;
            public DateTime? ContractDate = null;
            public string ContractDefaultValueCode = null;
            public string ContractID = null;
            private TList<ContractMaterial> contractMaterialContractCode;
            public string ContractName = null;
            public string ContractObject = null;
            public string ContractPerson = null;
            public DateTime? CreateDate = null;
            public string CreateMode = null;
            public string CreatePerson = null;
            public string DevelopUnit = null;
            public decimal? ExchangeRate = null;
            public string GroupName = null;
            public DateTime? LastModifyDate = null;
            public string LastModifyPerson = null;
            public string MarkSegment = null;
            public string MoneyType = null;
            public int? Mostly = null;
            public decimal? OldSumMoney = null;
            public string OriginalContractCode;
            public decimal? OriginalMoney = null;
            private TList<Payment> paymentContractCode;
            public string PayMode = null;
            public decimal? PerCash0 = null;
            public decimal? PerCash1 = null;
            public decimal? PerCash2 = null;
            public decimal? PerCash3 = null;
            public decimal? PerCash4 = null;
            public decimal? PerCash5 = null;
            public decimal? PerCash6 = null;
            public decimal? PerCash7 = null;
            public decimal? PerCash8 = null;
            public decimal? PerCash9 = null;
            public string PerformingCircs = null;
            public string ProjectCode = null;
            public string QualityRequire = null;
            public string Remark = null;
            public decimal? StampDuty = null;
            public int? StampDutyID = null;
            public int? Status = null;
            public string Supplier2Code = null;
            public string SupplierCode = null;
            public string ThirdParty = null;
            public decimal? TotalMoney = null;
            public string Type = null;
            public string UnitCode = null;
            public DateTime? WorkEndDate = null;
            public DateTime? WorkStartDate = null;
            public string WorkTime = null;

            public object Clone()
            {
                ContractBase.ContractEntityData data = new ContractBase.ContractEntityData();
                data.ContractCode = this.ContractCode;
                data.OriginalContractCode = this.OriginalContractCode;
                data.ContractID = this.ContractID;
                data.ProjectCode = this.ProjectCode;
                data.ContractName = this.ContractName;
                data.Type = this.Type;
                data.SupplierCode = this.SupplierCode;
                data.Supplier2Code = this.Supplier2Code;
                data.ContractPerson = this.ContractPerson;
                data.ContractDate = this.ContractDate;
                data.TotalMoney = this.TotalMoney;
                data.CreateDate = this.CreateDate;
                data.CreatePerson = this.CreatePerson;
                data.LastModifyPerson = this.LastModifyPerson;
                data.LastModifyDate = this.LastModifyDate;
                data.Remark = this.Remark;
                data.Status = this.Status;
                data.CheckPerson = this.CheckPerson;
                data.CheckOpinion = this.CheckOpinion;
                data.CheckDate = this.CheckDate;
                data.ContractObject = this.ContractObject;
                data.UnitCode = this.UnitCode;
                data.ThirdParty = this.ThirdParty;
                data.BeforeAccountTotalMoney = this.BeforeAccountTotalMoney;
                data.OldSumMoney = this.OldSumMoney;
                data.OriginalMoney = this.OriginalMoney;
                data.Mostly = this.Mostly;
                data.BiddingCode = this.BiddingCode;
                data.BudgetMoney = this.BudgetMoney;
                data.AdjustMoney = this.AdjustMoney;
                data.DevelopUnit = this.DevelopUnit;
                data.CreateMode = this.CreateMode;
                data.WorkTime = this.WorkTime;
                data.MarkSegment = this.MarkSegment;
                data.GroupName = this.GroupName;
                data.Building = this.Building;
                data.PayMode = this.PayMode;
                data.QualityRequire = this.QualityRequire;
                data.ContractArea = this.ContractArea;
                data.ContractDefaultValueCode = this.ContractDefaultValueCode;
                data.BaoHan = this.BaoHan;
                data.PerformingCircs = this.PerformingCircs;
                data.AccountStatus = this.AccountStatus;
                data.AuditingStatus = this.AuditingStatus;
                data.ChangeStatus = this.ChangeStatus;
                data.ChangeCount = this.ChangeCount;
                data.WorkStartDate = this.WorkStartDate;
                data.WorkEndDate = this.WorkEndDate;
                data.PerCash0 = this.PerCash0;
                data.PerCash1 = this.PerCash1;
                data.PerCash2 = this.PerCash2;
                data.PerCash3 = this.PerCash3;
                data.PerCash4 = this.PerCash4;
                data.PerCash5 = this.PerCash5;
                data.PerCash6 = this.PerCash6;
                data.PerCash7 = this.PerCash7;
                data.PerCash8 = this.PerCash8;
                data.PerCash9 = this.PerCash9;
                data.StampDutyID = this.StampDutyID;
                data.StampDuty = this.StampDuty;
                data.AdIssueDate = this.AdIssueDate;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                return data;
            }

            public TList<ContractAccount> ContractAccountCollection
            {
                get
                {
                    if (this.contractAccountContractCode == null)
                    {
                        this.contractAccountContractCode = new TList<ContractAccount>();
                    }
                    return this.contractAccountContractCode;
                }
                set
                {
                    this.contractAccountContractCode = value;
                }
            }

            public TList<ContractBill> ContractBillCollection
            {
                get
                {
                    if (this.contractBillContractCode == null)
                    {
                        this.contractBillContractCode = new TList<ContractBill>();
                    }
                    return this.contractBillContractCode;
                }
                set
                {
                    this.contractBillContractCode = value;
                }
            }

            public TList<ContractChange> ContractChangeCollection
            {
                get
                {
                    if (this.contractChangeContractCode == null)
                    {
                        this.contractChangeContractCode = new TList<ContractChange>();
                    }
                    return this.contractChangeContractCode;
                }
                set
                {
                    this.contractChangeContractCode = value;
                }
            }

            public TList<ContractCost> ContractCostCollection
            {
                get
                {
                    if (this.contractCostContractCode == null)
                    {
                        this.contractCostContractCode = new TList<ContractCost>();
                    }
                    return this.contractCostContractCode;
                }
                set
                {
                    this.contractCostContractCode = value;
                }
            }

            public TList<ContractCostPlan> ContractCostPlanCollection
            {
                get
                {
                    if (this.contractCostPlanContractCode == null)
                    {
                        this.contractCostPlanContractCode = new TList<ContractCostPlan>();
                    }
                    return this.contractCostPlanContractCode;
                }
                set
                {
                    this.contractCostPlanContractCode = value;
                }
            }

            public TList<ContractMaterial> ContractMaterialCollection
            {
                get
                {
                    if (this.contractMaterialContractCode == null)
                    {
                        this.contractMaterialContractCode = new TList<ContractMaterial>();
                    }
                    return this.contractMaterialContractCode;
                }
                set
                {
                    this.contractMaterialContractCode = value;
                }
            }

            public TList<Payment> PaymentCollection
            {
                get
                {
                    if (this.paymentContractCode == null)
                    {
                        this.paymentContractCode = new TList<Payment>();
                    }
                    return this.paymentContractCode;
                }
                set
                {
                    this.paymentContractCode = value;
                }
            }
        }
    }
}

