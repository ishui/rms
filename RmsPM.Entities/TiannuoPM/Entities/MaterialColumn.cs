namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum MaterialColumn
    {
        [EnumTextValue("GroupCode"), ColumnEnum("GroupCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        GroupCode = 3,
        [EnumTextValue("InputDate"), ColumnEnum("InputDate", typeof(DateTime), DbType.DateTime, false, false, true)]
        InputDate = 8,
        [EnumTextValue("InputPerson"), ColumnEnum("InputPerson", typeof(string), DbType.AnsiString, false, false, true, 50)]
        InputPerson = 7,
        [ColumnEnum("MaterialCode", typeof(int), DbType.Int32, true, false, false), EnumTextValue("MaterialCode")]
        MaterialCode = 1,
        [ColumnEnum("MaterialName", typeof(string), DbType.AnsiString, false, false, true, 100), EnumTextValue("MaterialName")]
        MaterialName = 2,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800)]
        Remark = 9,
        [EnumTextValue("Spec"), ColumnEnum("Spec", typeof(string), DbType.AnsiString, false, false, true, 200)]
        Spec = 4,
        [ColumnEnum("StandardPrice", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("StandardPrice")]
        StandardPrice = 6,
        [EnumTextValue("Unit"), ColumnEnum("Unit", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Unit = 5
    }
}

