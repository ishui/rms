namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum SystemUserColumn
    {
        [EnumTextValue("Address"), ColumnEnum("Address", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Address = 12,
        [ColumnEnum("BirthDay", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("BirthDay")]
        BirthDay = 10,
        [EnumTextValue("Fax"), ColumnEnum("Fax", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Fax = 14,
        [EnumTextValue("LastProjectCode"), ColumnEnum("LastProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        LastProjectCode = 0x10,
        [EnumTextValue("MailBox"), ColumnEnum("MailBox", typeof(string), DbType.AnsiString, false, false, true, 0x7d0)]
        MailBox = 8,
        [ColumnEnum("Mobile", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Mobile")]
        Mobile = 13,
        [EnumTextValue("Note"), ColumnEnum("Note", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Note = 9,
        [EnumTextValue("OwnName"), ColumnEnum("OwnName", typeof(string), DbType.AnsiString, false, false, true, 50)]
        OwnName = 4,
        [ColumnEnum("PassWord", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PassWord")]
        PassWord = 5,
        [EnumTextValue("Phone"), ColumnEnum("Phone", typeof(string), DbType.AnsiString, false, false, true, 0x10)]
        Phone = 7,
        [EnumTextValue("PhoneHome"), ColumnEnum("PhoneHome", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PhoneHome = 11,
        [EnumTextValue("Sex"), ColumnEnum("Sex", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Sex = 6,
        [ColumnEnum("ShortUserName", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("ShortUserName")]
        ShortUserName = 0x12,
        [ColumnEnum("SortID", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("SortID")]
        SortID = 0x11,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true)]
        Status = 15,
        [ColumnEnum("UserCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("UserCode")]
        UserCode = 1,
        [ColumnEnum("UserID", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("UserID")]
        UserID = 2,
        [ColumnEnum("UserName", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("UserName")]
        UserName = 3
    }
}

