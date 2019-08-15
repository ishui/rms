namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum PayoutColumn
    {
        [EnumTextValue("BankAccount"), ColumnEnum("BankAccount", typeof(string), DbType.AnsiString, false, false, true, 50)]
        BankAccount = 12,
        [ColumnEnum("BankName", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("BankName")]
        BankName = 11,
        [EnumTextValue("BillNo"), ColumnEnum("BillNo", typeof(string), DbType.AnsiString, false, false, true, 50)]
        BillNo = 9,
        [ColumnEnum("Cash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Cash")]
        Cash = 0x1a,
        [EnumTextValue("CheckDate"), ColumnEnum("CheckDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        CheckDate = 0x13,
        [ColumnEnum("CheckOpinion", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("CheckOpinion")]
        CheckOpinion = 20,
        [ColumnEnum("CheckPerson", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("CheckPerson")]
        CheckPerson = 0x12,
        [ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ExchangeRate")]
        ExchangeRate = 0x1c,
        [EnumTextValue("GroupCode"), ColumnEnum("GroupCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        GroupCode = 0x18,
        [EnumTextValue("InputDate"), ColumnEnum("InputDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        InputDate = 0x11,
        [ColumnEnum("InputPerson", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("InputPerson")]
        InputPerson = 0x10,
        [ColumnEnum("InvoNo", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("InvoNo")]
        InvoNo = 10,
        [EnumTextValue("IsApportioned"), ColumnEnum("IsApportioned", typeof(int), DbType.Int32, false, false, true)]
        IsApportioned = 0x19,
        [EnumTextValue("Money"), ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true)]
        Money = 14,
        [ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MoneyType")]
        MoneyType = 0x1b,
        [ColumnEnum("Payer", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Payer")]
        Payer = 7,
        [EnumTextValue("PaymentCodes"), ColumnEnum("PaymentCodes", typeof(string), DbType.AnsiString, false, false, true, 500)]
        PaymentCodes = 4,
        [EnumTextValue("PaymentType"), ColumnEnum("PaymentType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PaymentType = 6,
        [EnumTextValue("PayoutCode"), ColumnEnum("PayoutCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        PayoutCode = 1,
        [EnumTextValue("PayoutDate"), ColumnEnum("PayoutDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        PayoutDate = 5,
        [EnumTextValue("PayoutID"), ColumnEnum("PayoutID", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PayoutID = 2,
        [ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ProjectCode")]
        ProjectCode = 3,
        [EnumTextValue("ReceiptCount"), ColumnEnum("ReceiptCount", typeof(int), DbType.Int32, false, false, true)]
        ReceiptCount = 0x17,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800)]
        Remark = 0x16,
        [ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true), EnumTextValue("Status")]
        Status = 15,
        [EnumTextValue("SubjectCode"), ColumnEnum("SubjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        SubjectCode = 13,
        [EnumTextValue("SubjectSetCode"), ColumnEnum("SubjectSetCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        SubjectSetCode = 30,
        [EnumTextValue("SupplyCode"), ColumnEnum("SupplyCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        SupplyCode = 8,
        [ColumnEnum("SupplyName", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("SupplyName")]
        SupplyName = 0x15,
        [ColumnEnum("VoucherNo", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("VoucherNo")]
        VoucherNo = 0x1d
    }
}

