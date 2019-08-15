namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractAccountColumn
    {
        [ColumnEnum("ContractAccountCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ContractAccountCode")]
        ContractAccountCode = 1,
        [EnumTextValue("ContractAccountID"), ColumnEnum("ContractAccountID", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractAccountID = 2,
        [EnumTextValue("ContractChangeCode"), ColumnEnum("ContractChangeCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ContractChangeCode = 8,
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 3,
        [ColumnEnum("CreateDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("CreateDate")]
        CreateDate = 6,
        [ColumnEnum("CreatePerson", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("CreatePerson")]
        CreatePerson = 7,
        [ColumnEnum("Reason", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Reason")]
        Reason = 4,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true)]
        Status = 5
    }
}

