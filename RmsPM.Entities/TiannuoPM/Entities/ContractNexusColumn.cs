namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractNexusColumn
    {
        [EnumTextValue("Code"), ColumnEnum("Code", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Code = 4,
        [ColumnEnum("ContractChangeCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractChangeCode")]
        ContractChangeCode = 3,
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 2,
        [ColumnEnum("ContractNexusCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ContractNexusCode")]
        ContractNexusCode = 1,
        [ColumnEnum("Date", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("Date")]
        Date = 9,
        [ColumnEnum("ID", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ID")]
        ID = 7,
        [EnumTextValue("Money"), ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true)]
        Money = 11,
        [ColumnEnum("Name", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Name")]
        Name = 6,
        [ColumnEnum("Path", typeof(string), DbType.AnsiString, false, false, true, 500), EnumTextValue("Path")]
        Path = 10,
        [ColumnEnum("Person", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Person")]
        Person = 8,
        [EnumTextValue("Type"), ColumnEnum("Type", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Type = 5
    }
}

