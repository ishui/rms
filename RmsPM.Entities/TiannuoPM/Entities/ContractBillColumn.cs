namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractBillColumn
    {
        [EnumTextValue("BillMoney"), ColumnEnum("BillMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        BillMoney = 5,
        [ColumnEnum("BillNo", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("BillNo")]
        BillNo = 4,
        [ColumnEnum("Code", typeof(int), DbType.Int32, true, true, false), EnumTextValue("Code")]
        Code = 1,
        [EnumTextValue("ContractCode"), ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractCode = 3,
        [ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ProjectCode")]
        ProjectCode = 2
    }
}

