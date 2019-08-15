namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum DictionaryItemColumn
    {
        [EnumTextValue("DictionaryItemCode"), ColumnEnum("DictionaryItemCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        DictionaryItemCode = 1,
        [EnumTextValue("DictionaryNameCode"), ColumnEnum("DictionaryNameCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        DictionaryNameCode = 3,
        [EnumTextValue("Name"), ColumnEnum("Name", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Name = 5,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 2,
        [EnumTextValue("SortID"), ColumnEnum("SortID", typeof(int), DbType.Int32, false, false, true)]
        SortID = 4
    }
}

