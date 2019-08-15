namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractChangeColumn
    {
        [ColumnEnum("ChangeDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("ChangeDate")]
        ChangeDate = 0x10,
        [ColumnEnum("ChangeMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ChangeMoney")]
        ChangeMoney = 6,
        [EnumTextValue("ChangePerson"), ColumnEnum("ChangePerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ChangePerson = 15,
        [EnumTextValue("ChangeReason"), ColumnEnum("ChangeReason", typeof(string), DbType.AnsiString, false, false, true, 800)]
        ChangeReason = 13,
        [EnumTextValue("ChangeType"), ColumnEnum("ChangeType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ChangeType = 0x11,
        [ColumnEnum("CheckDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("CheckDate")]
        CheckDate = 0x13,
        [EnumTextValue("CheckPerson"), ColumnEnum("CheckPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CheckPerson = 0x12,
        [EnumTextValue("ConsultantAuditMoney"), ColumnEnum("ConsultantAuditMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        ConsultantAuditMoney = 11,
        [ColumnEnum("ContractChangeCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ContractChangeCode")]
        ContractChangeCode = 1,
        [ColumnEnum("ContractChangeId", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractChangeId")]
        ContractChangeId = 2,
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 3,
        [EnumTextValue("Money"), ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true)]
        Money = 5,
        [EnumTextValue("NewMoney"), ColumnEnum("NewMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        NewMoney = 7,
        [ColumnEnum("OriginalMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OriginalMoney")]
        OriginalMoney = 8,
        [ColumnEnum("ProjectAuditMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ProjectAuditMoney")]
        ProjectAuditMoney = 12,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true)]
        Status = 14,
        [EnumTextValue("SupplierChangeMoney"), ColumnEnum("SupplierChangeMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        SupplierChangeMoney = 10,
        [EnumTextValue("TotalChangeMoney"), ColumnEnum("TotalChangeMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        TotalChangeMoney = 9,
        [ColumnEnum("Voucher", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Voucher")]
        Voucher = 4
    }
}

