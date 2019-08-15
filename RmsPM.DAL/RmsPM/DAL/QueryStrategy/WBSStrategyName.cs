namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public enum WBSStrategyName
    {
        WBSCode,
        WBSCodeIn,
        CodeLike,
        ProjectCode,
        ParentCode,
        Deep,
        TaskName,
        TaskNameLike,
        UserAccess,
        PlannedStartDate,
        PlannedFinishDate,
        ActualStartDate,
        ActualFinishDate,
        Status,
        StatusNot,
        PreStatusNot,
        Master,
        FullCode,
        RelatedUser,
        AccessRange,
        ImportantLevel,
        Exceed,
        RelaType,
        RelaCode,
        Flag,
        SortID,
        AllChild
    }
}

