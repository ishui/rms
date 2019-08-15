namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum TroubleColumn
    {
        [EnumTextValue("ExecutionTime"), ColumnEnum("ExecutionTime", typeof(DateTime), DbType.DateTime, false, false, true)]
        ExecutionTime = 5,
        [ColumnEnum("InspectSituationID", typeof(int), DbType.Int32, false, false, false), EnumTextValue("InspectSituationID")]
        InspectSituationID = 2,
        [EnumTextValue("place"), ColumnEnum("place", typeof(string), DbType.AnsiString, false, false, true, 200)]
        Place = 6,
        [EnumTextValue("Remark"), ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 0xfa0)]
        Remark = 8,
        [ColumnEnum("Requirement", typeof(string), DbType.AnsiString, false, false, true, 0x7d0), EnumTextValue("Requirement")]
        Requirement = 3,
        [EnumTextValue("Status"), ColumnEnum("Status", typeof(int), DbType.Int32, false, false, false)]
        Status = 9,
        [EnumTextValue("Suggestion"), ColumnEnum("Suggestion", typeof(string), DbType.AnsiString, false, false, true, 0x7d0)]
        Suggestion = 4,
        [ColumnEnum("TroubleCompendium", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("TroubleCompendium")]
        TroubleCompendium = 7,
        [ColumnEnum("TroubleID", typeof(int), DbType.Int32, true, true, false), EnumTextValue("TroubleID")]
        TroubleID = 1
    }
}

