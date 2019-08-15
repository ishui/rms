namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum MaterialPurchasDtlColumn
    {
        [EnumTextValue("FinalPrice"), ColumnEnum("FinalPrice", typeof(decimal), DbType.Decimal, false, false, true)]
        FinalPrice = 9,
        [ColumnEnum("MaterialPurchasDtlID", typeof(int), DbType.Int32, true, true, false), EnumTextValue("MaterialPurchasDtlID")]
        MaterialPurchasDtlID = 1,
        [EnumTextValue("MaterialPurchasID"), ColumnEnum("MaterialPurchasID", typeof(int), DbType.Int32, false, false, true)]
        MaterialPurchasID = 2,
        [EnumTextValue("NeedDate"), ColumnEnum("NeedDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        NeedDate = 6,
        [ColumnEnum("Number", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Number")]
        Number = 5,
        [ColumnEnum("SearchPriceDtl", typeof(string), DbType.AnsiString, false, false, true, 0x1388), EnumTextValue("SearchPriceDtl")]
        SearchPriceDtl = 8,
        [EnumTextValue("SignDate"), ColumnEnum("SignDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        SignDate = 7,
        [EnumTextValue("TypeStandard"), ColumnEnum("TypeStandard", typeof(string), DbType.AnsiString, false, false, true, 50)]
        TypeStandard = 3,
        [ColumnEnum("Unit", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Unit")]
        Unit = 4
    }
}

