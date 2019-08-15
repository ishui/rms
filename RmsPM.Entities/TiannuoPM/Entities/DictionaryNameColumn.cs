namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum DictionaryNameColumn
    {
        [ColumnEnum("DictionaryNameCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("DictionaryNameCode")]
        DictionaryNameCode = 1,
        [EnumTextValue("NAME"), ColumnEnum("NAME", typeof(string), DbType.AnsiString, false, false, true, 50)]
        NAME = 2,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 3,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 400)]
        Remark = 4
    }
}

