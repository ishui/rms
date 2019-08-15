namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractMaterialColumn
    {
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 2,
        [EnumTextValue("ContractMaterialCode"), ColumnEnum("ContractMaterialCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        ContractMaterialCode = 1,
        [ColumnEnum("MaterialCode", typeof(int), DbType.Int32, false, false, true), EnumTextValue("MaterialCode")]
        MaterialCode = 3,
        [EnumTextValue("Money"), ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true)]
        Money = 6,
        [ColumnEnum("Price", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Price")]
        Price = 5,
        [EnumTextValue("Qty"), ColumnEnum("Qty", typeof(decimal), DbType.Decimal, false, false, true)]
        Qty = 4
    }
}

