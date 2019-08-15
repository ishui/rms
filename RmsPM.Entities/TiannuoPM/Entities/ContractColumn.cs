namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractColumn
    {
        [EnumTextValue("AccountStatus"), ColumnEnum("AccountStatus", typeof(int), DbType.Int32, false, false, true)]
        AccountStatus = 0x2a,
        [ColumnEnum("AdIssueDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("AdIssueDate")]
        AdIssueDate = 60,
        [EnumTextValue("AdjustMoney"), ColumnEnum("AdjustMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        AdjustMoney = 0x1d,
        [ColumnEnum("AuditingStatus", typeof(int), DbType.Int32, false, false, true), EnumTextValue("AuditingStatus")]
        AuditingStatus = 0x2b,
        [EnumTextValue("BaoHan"), ColumnEnum("BaoHan", typeof(decimal), DbType.Decimal, false, false, true)]
        BaoHan = 40,
        [ColumnEnum("BeforeAccountTotalMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BeforeAccountTotalMoney")]
        BeforeAccountTotalMoney = 0x17,
        [ColumnEnum("BiddingCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("BiddingCode")]
        BiddingCode = 0x1b,
        [EnumTextValue("BudgetMoney"), ColumnEnum("BudgetMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        BudgetMoney = 0x1c,
        [EnumTextValue("Building"), ColumnEnum("Building", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Building = 0x23,
        [ColumnEnum("ChangeCount", typeof(int), DbType.Int32, false, false, true), EnumTextValue("ChangeCount")]
        ChangeCount = 0x2d,
        [EnumTextValue("ChangeStatus"), ColumnEnum("ChangeStatus", typeof(int), DbType.Int32, false, false, true)]
        ChangeStatus = 0x2c,
        [ColumnEnum("CheckDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("CheckDate")]
        CheckDate = 0x13,
        [EnumTextValue("CheckOpinion"), ColumnEnum("CheckOpinion", typeof(string), DbType.AnsiString, false, false, true, 800)]
        CheckOpinion = 0x12,
        [EnumTextValue("CheckPerson"), ColumnEnum("CheckPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CheckPerson = 0x11,
        [ColumnEnum("ContractArea", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("ContractArea")]
        ContractArea = 0x26,
        [EnumTextValue("ContractCode"), ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        ContractCode = 1,
        [ColumnEnum("ContractDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("ContractDate")]
        ContractDate = 9,
        [ColumnEnum("ContractDefaultValueCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractDefaultValueCode")]
        ContractDefaultValueCode = 0x27,
        [ColumnEnum("ContractID", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractID")]
        ContractID = 2,
        [ColumnEnum("ContractName", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("ContractName")]
        ContractName = 4,
        [ColumnEnum("ContractObject", typeof(string), DbType.AnsiString, false, false, true, 500), EnumTextValue("ContractObject")]
        ContractObject = 20,
        [EnumTextValue("ContractPerson"), ColumnEnum("ContractPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractPerson = 8,
        [EnumTextValue("CreateDate"), ColumnEnum("CreateDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        CreateDate = 11,
        [ColumnEnum("CreateMode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("CreateMode")]
        CreateMode = 0x1f,
        [EnumTextValue("CreatePerson"), ColumnEnum("CreatePerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CreatePerson = 12,
        [ColumnEnum("DevelopUnit", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("DevelopUnit")]
        DevelopUnit = 30,
        [EnumTextValue("ExchangeRate"), ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        ExchangeRate = 0x3e,
        [ColumnEnum("GroupName", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("GroupName")]
        GroupName = 0x22,
        [ColumnEnum("LastModifyDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("LastModifyDate")]
        LastModifyDate = 14,
        [EnumTextValue("LastModifyPerson"), ColumnEnum("LastModifyPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        LastModifyPerson = 13,
        [ColumnEnum("MarkSegment", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MarkSegment")]
        MarkSegment = 0x21,
        [EnumTextValue("MoneyType"), ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        MoneyType = 0x3d,
        [EnumTextValue("Mostly"), ColumnEnum("Mostly", typeof(int), DbType.Int32, false, false, true)]
        Mostly = 0x1a,
        [EnumTextValue("oldSumMoney"), ColumnEnum("oldSumMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        OldSumMoney = 0x18,
        [ColumnEnum("OriginalMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OriginalMoney")]
        OriginalMoney = 0x19,
        [EnumTextValue("PayMode"), ColumnEnum("PayMode", typeof(string), DbType.AnsiString, false, false, true)]
        PayMode = 0x24,
        [ColumnEnum("PerCash0", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PerCash0")]
        PerCash0 = 0x30,
        [ColumnEnum("PerCash1", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PerCash1")]
        PerCash1 = 0x31,
        [EnumTextValue("PerCash2"), ColumnEnum("PerCash2", typeof(decimal), DbType.Decimal, false, false, true)]
        PerCash2 = 50,
        [ColumnEnum("PerCash3", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PerCash3")]
        PerCash3 = 0x33,
        [EnumTextValue("PerCash4"), ColumnEnum("PerCash4", typeof(decimal), DbType.Decimal, false, false, true)]
        PerCash4 = 0x34,
        [ColumnEnum("PerCash5", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PerCash5")]
        PerCash5 = 0x35,
        [EnumTextValue("PerCash6"), ColumnEnum("PerCash6", typeof(decimal), DbType.Decimal, false, false, true)]
        PerCash6 = 0x36,
        [EnumTextValue("PerCash7"), ColumnEnum("PerCash7", typeof(decimal), DbType.Decimal, false, false, true)]
        PerCash7 = 0x37,
        [ColumnEnum("PerCash8", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PerCash8")]
        PerCash8 = 0x38,
        [EnumTextValue("PerCash9"), ColumnEnum("PerCash9", typeof(decimal), DbType.Decimal, false, false, true)]
        PerCash9 = 0x39,
        [ColumnEnum("PerformingCircs", typeof(string), DbType.AnsiString, false, false, true, 500), EnumTextValue("PerformingCircs")]
        PerformingCircs = 0x29,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 3,
        [ColumnEnum("QualityRequire", typeof(string), DbType.AnsiString, false, false, true), EnumTextValue("QualityRequire")]
        QualityRequire = 0x25,
        [ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Remark")]
        Remark = 15,
        [ColumnEnum("StampDuty", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("StampDuty")]
        StampDuty = 0x3b,
        [ColumnEnum("StampDutyID", typeof(int), DbType.Int32, false, false, true), EnumTextValue("StampDutyID")]
        StampDutyID = 0x3a,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true)]
        Status = 0x10,
        [EnumTextValue("Supplier2Code"), ColumnEnum("Supplier2Code", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Supplier2Code = 7,
        [EnumTextValue("SupplierCode"), ColumnEnum("SupplierCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        SupplierCode = 6,
        [ColumnEnum("ThirdParty", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("ThirdParty")]
        ThirdParty = 0x16,
        [ColumnEnum("TotalMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalMoney")]
        TotalMoney = 10,
        [ColumnEnum("Type", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Type")]
        Type = 5,
        [EnumTextValue("UnitCode"), ColumnEnum("UnitCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        UnitCode = 0x15,
        [EnumTextValue("WorkEndDate"), ColumnEnum("WorkEndDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        WorkEndDate = 0x2f,
        [ColumnEnum("WorkStartDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("WorkStartDate")]
        WorkStartDate = 0x2e,
        [EnumTextValue("WorkTime"), ColumnEnum("WorkTime", typeof(string), DbType.AnsiString, false, false, true, 50)]
        WorkTime = 0x20
    }
}

