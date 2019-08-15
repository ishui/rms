namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractCostColumn
    {
        [ColumnEnum("Amount", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Amount")]
        Amount = 4,
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 2,
        [ColumnEnum("ContractCostCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ContractCostCode")]
        ContractCostCode = 1,
        [EnumTextValue("CostBudgetSetCode"), ColumnEnum("CostBudgetSetCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CostBudgetSetCode = 11,
        [ColumnEnum("CostCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("CostCode")]
        CostCode = 3,
        [ColumnEnum("Description", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Description")]
        Description = 12,
        [EnumTextValue("ExchangeRate"), ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        ExchangeRate = 10,
        [EnumTextValue("Money"), ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true)]
        Money = 5,
        [ColumnEnum("Moneycash", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Moneycash")]
        Moneycash = 7,
        [ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MoneyType")]
        MoneyType = 9,
        [ColumnEnum("OriginalMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OriginalMoney")]
        OriginalMoney = 13,
        [EnumTextValue("OriginalMoneycash"), ColumnEnum("OriginalMoneycash", typeof(decimal), DbType.Decimal, false, false, true)]
        OriginalMoneycash = 8,
        [ColumnEnum("UnitPrise", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UnitPrise")]
        UnitPrise = 6
    }
}

