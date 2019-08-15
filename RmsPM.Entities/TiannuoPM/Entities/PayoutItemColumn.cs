namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum PayoutItemColumn
    {
        [EnumTextValue("AlloType"), ColumnEnum("AlloType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        AlloType = 7,
        [EnumTextValue("ExchangeRate"), ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        ExchangeRate = 11,
        [EnumTextValue("IsManualAlloc"), ColumnEnum("IsManualAlloc", typeof(int), DbType.Int32, false, false, true)]
        IsManualAlloc = 8,
        [ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MoneyType")]
        MoneyType = 10,
        [ColumnEnum("PaymentItemCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PaymentItemCode")]
        PaymentItemCode = 3,
        [ColumnEnum("PayoutCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PayoutCash")]
        PayoutCash = 9,
        [ColumnEnum("PayoutCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PayoutCode")]
        PayoutCode = 2,
        [EnumTextValue("PayoutExchangeRate"), ColumnEnum("PayoutExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        PayoutExchangeRate = 13,
        [EnumTextValue("PayoutItemCode"), ColumnEnum("PayoutItemCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        PayoutItemCode = 1,
        [ColumnEnum("PayoutMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PayoutMoney")]
        PayoutMoney = 4,
        [ColumnEnum("PayoutMoneyType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PayoutMoneyType")]
        PayoutMoneyType = 12,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800)]
        Remark = 6,
        [ColumnEnum("SubjectCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("SubjectCode")]
        SubjectCode = 5
    }
}

