namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum PaymentColumn
    {
        [ColumnEnum("Accountant", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Accountant")]
        Accountant = 9,
        [EnumTextValue("AccountDate"), ColumnEnum("AccountDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        AccountDate = 10,
        [EnumTextValue("AdjustedContractMoney"), ColumnEnum("AdjustedContractMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        AdjustedContractMoney = 0x2c,
        [ColumnEnum("AHCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash")]
        AHCash = 0x2e,
        [ColumnEnum("AHCash0", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash0")]
        AHCash0 = 50,
        [ColumnEnum("AHCash1", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash1")]
        AHCash1 = 0x33,
        [ColumnEnum("AHCash2", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash2")]
        AHCash2 = 0x34,
        [ColumnEnum("AHCash3", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash3")]
        AHCash3 = 0x35,
        [EnumTextValue("AHCash4"), ColumnEnum("AHCash4", typeof(decimal), DbType.Decimal, false, false, true)]
        AHCash4 = 0x36,
        [ColumnEnum("AHCash5", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash5")]
        AHCash5 = 0x37,
        [EnumTextValue("AHCash6"), ColumnEnum("AHCash6", typeof(decimal), DbType.Decimal, false, false, true)]
        AHCash6 = 0x38,
        [ColumnEnum("AHCash7", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash7")]
        AHCash7 = 0x39,
        [ColumnEnum("AHCash8", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash8")]
        AHCash8 = 0x3a,
        [ColumnEnum("AHCash9", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHCash9")]
        AHCash9 = 0x3b,
        [ColumnEnum("AHMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AHMoney")]
        AHMoney = 0x25,
        [EnumTextValue("APCash"), ColumnEnum("APCash", typeof(decimal), DbType.Decimal, false, false, true)]
        APCash = 0x2f,
        [EnumTextValue("APMoney"), ColumnEnum("APMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        APMoney = 0x26,
        [ColumnEnum("ApplyDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("ApplyDate")]
        ApplyDate = 8,
        [ColumnEnum("ApplyPerson", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ApplyPerson")]
        ApplyPerson = 7,
        [EnumTextValue("BankAccount"), ColumnEnum("BankAccount", typeof(string), DbType.AnsiString, false, false, true, 50)]
        BankAccount = 30,
        [ColumnEnum("BankName", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("BankName")]
        BankName = 0x1d,
        [ColumnEnum("CheckDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("CheckDate")]
        CheckDate = 0x10,
        [ColumnEnum("CheckOpinion", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("CheckOpinion")]
        CheckOpinion = 0x11,
        [EnumTextValue("CheckPerson"), ColumnEnum("CheckPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CheckPerson = 15,
        [EnumTextValue("CheckRemark"), ColumnEnum("CheckRemark", typeof(string), DbType.AnsiString, false, false, true, 0x3e8)]
        CheckRemark = 0x40,
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 0x13,
        [ColumnEnum("ContractMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ContractMoney")]
        ContractMoney = 0x2b,
        [ColumnEnum("FactPayDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("FactPayDate")]
        FactPayDate = 0x23,
        [EnumTextValue("GroupCode"), ColumnEnum("GroupCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        GroupCode = 0x1c,
        [ColumnEnum("IsApportion", typeof(int), DbType.Int32, false, false, true), EnumTextValue("IsApportion")]
        IsApportion = 0x16,
        [EnumTextValue("IsContract"), ColumnEnum("IsContract", typeof(int), DbType.Int32, false, false, true)]
        IsContract = 0x12,
        [EnumTextValue("Issue"), ColumnEnum("Issue", typeof(int), DbType.Int32, false, false, true)]
        Issue = 0x21,
        [ColumnEnum("IssueMode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("IssueMode")]
        IssueMode = 0x22,
        [EnumTextValue("MaxPayMoney"), ColumnEnum("MaxPayMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        MaxPayMoney = 0x29,
        [ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Money")]
        Money = 14,
        [ColumnEnum("OldMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OldMoney")]
        OldMoney = 0x1b,
        [ColumnEnum("OtherAttachMent", typeof(string), DbType.AnsiString, false, false, true, 0x3e8), EnumTextValue("OtherAttachMent")]
        OtherAttachMent = 0x1f,
        [EnumTextValue("PayDate"), ColumnEnum("PayDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        PayDate = 12,
        [EnumTextValue("Payer"), ColumnEnum("Payer", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Payer = 11,
        [ColumnEnum("PaymentCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("PaymentCode")]
        PaymentCode = 1,
        [ColumnEnum("PaymentCodition", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("PaymentCodition")]
        PaymentCodition = 0x3f,
        [EnumTextValue("PaymentID"), ColumnEnum("PaymentID", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PaymentID = 3,
        [ColumnEnum("PaymentName", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("PaymentName")]
        PaymentName = 60,
        [EnumTextValue("PaymentTitle"), ColumnEnum("PaymentTitle", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PaymentTitle = 2,
        [ColumnEnum("PayoutCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PayoutCash")]
        PayoutCash = 0x31,
        [EnumTextValue("PayoutMoney"), ColumnEnum("PayoutMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        PayoutMoney = 0x2d,
        [ColumnEnum("PayType", typeof(int), DbType.Int32, false, false, true), EnumTextValue("PayType")]
        PayType = 0x20,
        [EnumTextValue("PlanPayMoney"), ColumnEnum("PlanPayMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        PlanPayMoney = 0x2a,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 6,
        [EnumTextValue("Purpose"), ColumnEnum("Purpose", typeof(string), DbType.AnsiString, false, false, true, 800)]
        Purpose = 13,
        [ColumnEnum("RecieptCount", typeof(int), DbType.Int32, false, false, true), EnumTextValue("RecieptCount")]
        RecieptCount = 5,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800)]
        Remark = 0x1a,
        [ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true), EnumTextValue("Status")]
        Status = 20,
        [ColumnEnum("SumCode", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("SumCode")]
        SumCode = 0x3e,
        [ColumnEnum("SupplierApplyMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("SupplierApplyMoney")]
        SupplierApplyMoney = 40,
        [EnumTextValue("SupplyCode"), ColumnEnum("SupplyCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        SupplyCode = 0x17,
        [EnumTextValue("SupplyName"), ColumnEnum("SupplyName", typeof(string), DbType.AnsiString, false, false, true, 200)]
        SupplyName = 0x19,
        [EnumTextValue("TotalPayMoney"), ColumnEnum("TotalPayMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        TotalPayMoney = 0x24,
        [ColumnEnum("TotalViseChangeMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalViseChangeMoney")]
        TotalViseChangeMoney = 0x3d,
        [ColumnEnum("UnitCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("UnitCode")]
        UnitCode = 0x18,
        [ColumnEnum("UPCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UPCash")]
        UPCash = 0x30,
        [ColumnEnum("UPMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UPMoney")]
        UPMoney = 0x27,
        [ColumnEnum("VoucherID", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("VoucherID")]
        VoucherID = 4,
        [EnumTextValue("WBSCode"), ColumnEnum("WBSCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        WBSCode = 0x15
    }
}

