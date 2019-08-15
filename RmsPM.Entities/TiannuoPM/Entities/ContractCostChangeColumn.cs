namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractCostChangeColumn
    {
        [EnumTextValue("Cash"), ColumnEnum("Cash", typeof(decimal), DbType.Decimal, false, false, true)]
        Cash = 5,
        [ColumnEnum("ChangeCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ChangeCash")]
        ChangeCash = 6,
        [EnumTextValue("ChangeMoney"), ColumnEnum("ChangeMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        ChangeMoney = 13,
        [EnumTextValue("ContractChangeCode"), ColumnEnum("ContractChangeCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractChangeCode = 4,
        [EnumTextValue("ContractCode"), ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractCode = 2,
        [EnumTextValue("ContractCostChangeCode"), ColumnEnum("ContractCostChangeCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        ContractCostChangeCode = 1,
        [EnumTextValue("ContractCostCode"), ColumnEnum("ContractCostCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractCostCode = 3,
        [EnumTextValue("CostBudgetSetCode"), ColumnEnum("CostBudgetSetCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CostBudgetSetCode = 0x12,
        [EnumTextValue("CostCode"), ColumnEnum("CostCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CostCode = 0x11,
        [ColumnEnum("Description", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Description")]
        Description = 0x13,
        [EnumTextValue("ExchangeRate"), ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        ExchangeRate = 11,
        [ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Money")]
        Money = 12,
        [EnumTextValue("MoneyType"), ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        MoneyType = 10,
        [EnumTextValue("NewCash"), ColumnEnum("NewCash", typeof(decimal), DbType.Decimal, false, false, true)]
        NewCash = 7,
        [ColumnEnum("NewMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("NewMoney")]
        NewMoney = 14,
        [ColumnEnum("OriginalCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OriginalCash")]
        OriginalCash = 8,
        [EnumTextValue("OriginalMoney"), ColumnEnum("OriginalMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        OriginalMoney = 15,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true)]
        Status = 20,
        [ColumnEnum("TotalChangeCash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalChangeCash")]
        TotalChangeCash = 9,
        [ColumnEnum("TotalChangeMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalChangeMoney")]
        TotalChangeMoney = 0x10
    }
}

