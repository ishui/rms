namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum InspectSituationColumn
    {
        [ColumnEnum("InspectDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("InspectDate")]
        InspectDate = 4,
        [ColumnEnum("InspectSituationID", typeof(int), DbType.Int32, true, true, false), EnumTextValue("InspectSituationID")]
        InspectSituationID = 1,
        [EnumTextValue("InspectSituationNO"), ColumnEnum("InspectSituationNO", typeof(string), DbType.AnsiString, false, false, false, 200)]
        InspectSituationNO = 2,
        [EnumTextValue("InspectUser"), ColumnEnum("InspectUser", typeof(string), DbType.AnsiString, false, false, true, 50)]
        InspectUser = 7,
        [ColumnEnum("InspectUserIpecialty", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("InspectUserIpecialty")]
        InspectUserIpecialty = 6,
        [ColumnEnum("KeyPoint", typeof(string), DbType.AnsiString, false, false, true, 500), EnumTextValue("KeyPoint")]
        KeyPoint = 8,
        [EnumTextValue("ProjectCode"), ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectCode = 3,
        [ColumnEnum("Status", typeof(int), DbType.Int32, false, false, true), EnumTextValue("Status")]
        Status = 9,
        [EnumTextValue("Weather"), ColumnEnum("Weather", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Weather = 5
    }
}

