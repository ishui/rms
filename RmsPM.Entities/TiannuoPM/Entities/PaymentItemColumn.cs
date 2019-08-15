namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum PaymentItemColumn
    {
        [EnumTextValue("AllocateCode"), ColumnEnum("AllocateCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        AllocateCode = 8,
        [ColumnEnum("AlloType", typeof(string), DbType.AnsiString, false, false, true, 1), EnumTextValue("AlloType")]
        AlloType = 10,
        [EnumTextValue("ContractCostCode"), ColumnEnum("ContractCostCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractCostCode = 15,
        [EnumTextValue("CostBudgetSetCode"), ColumnEnum("CostBudgetSetCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CostBudgetSetCode = 11,
        [EnumTextValue("CostCode"), ColumnEnum("CostCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        CostCode = 3,
        [ColumnEnum("Description", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Description")]
        Description = 14,
        [EnumTextValue("ExchangeRate"), ColumnEnum("ExchangeRate", typeof(decimal), DbType.Decimal, false, false, true)]
        ExchangeRate = 0x12,
        [EnumTextValue("ItemCash"), ColumnEnum("ItemCash", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemCash = 0x10,
        [ColumnEnum("ItemCash0", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash0")]
        ItemCash0 = 0x13,
        [ColumnEnum("ItemCash1", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash1")]
        ItemCash1 = 20,
        [ColumnEnum("ItemCash2", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash2")]
        ItemCash2 = 0x15,
        [EnumTextValue("ItemCash3"), ColumnEnum("ItemCash3", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemCash3 = 0x16,
        [EnumTextValue("ItemCash4"), ColumnEnum("ItemCash4", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemCash4 = 0x17,
        [EnumTextValue("ItemCash5"), ColumnEnum("ItemCash5", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemCash5 = 0x18,
        [ColumnEnum("ItemCash6", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash6")]
        ItemCash6 = 0x19,
        [EnumTextValue("ItemCash7"), ColumnEnum("ItemCash7", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemCash7 = 0x1a,
        [ColumnEnum("ItemCash8", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash8")]
        ItemCash8 = 0x1b,
        [ColumnEnum("ItemCash9", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("ItemCash9")]
        ItemCash9 = 0x1c,
        [EnumTextValue("ItemMoney"), ColumnEnum("ItemMoney", typeof(decimal), DbType.Decimal, false, false, true)]
        ItemMoney = 5,
        [ColumnEnum("MoneyType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MoneyType")]
        MoneyType = 0x11,
        [ColumnEnum("OldItemMoney", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("OldItemMoney")]
        OldItemMoney = 9,
        [EnumTextValue("PaymentCode"), ColumnEnum("PaymentCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PaymentCode = 2,
        [ColumnEnum("PaymentItemCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("PaymentItemCode")]
        PaymentItemCode = 1,
        [ColumnEnum("PaymentType", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PaymentType")]
        PaymentType = 4,
        [EnumTextValue("PBSCode"), ColumnEnum("PBSCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PBSCode = 13,
        [EnumTextValue("PBSType"), ColumnEnum("PBSType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PBSType = 12,
        [ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Remark")]
        Remark = 7,
        [ColumnEnum("Summary", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Summary")]
        Summary = 6
    }
}

