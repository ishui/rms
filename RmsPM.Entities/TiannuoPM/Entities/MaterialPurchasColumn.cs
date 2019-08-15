namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum MaterialPurchasColumn
    {
        [EnumTextValue("Description"), ColumnEnum("Description", typeof(string), DbType.AnsiString, false, false, true, 0x1388)]
        Description = 7,
        [ColumnEnum("FollowUserCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("FollowUserCode")]
        FollowUserCode = 8,
        [ColumnEnum("MaterialPurchasCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("MaterialPurchasCode")]
        MaterialPurchasCode = 2,
        [ColumnEnum("MaterialPurchasID", typeof(int), DbType.Int32, true, true, false), EnumTextValue("MaterialPurchasID")]
        MaterialPurchasID = 1,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 5,
        [ColumnEnum("PurchasDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("PurchasDate")]
        PurchasDate = 4,
        [ColumnEnum("PurchasUnitCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("PurchasUnitCode")]
        PurchasUnitCode = 3,
        [ColumnEnum("Status", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Status")]
        Status = 9,
        [ColumnEnum("Title", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Title")]
        Title = 6
    }
}

