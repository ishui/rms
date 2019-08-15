namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public enum CBSStrategyName
    {
        CostCode,
        ProjectCode,
        ParentCode,
        Deep,
        CostName,
        UserAccess,
        CostCodeStr,
        AllSubNodeIncludeSelf,
        AllSubNodeNotIncludeSelf,
        FirstChildNode,
        AllSubLeafNode,
        AllSubNotLeafNode,
        SortID,
        AccessRange,
        BudgetType,
        ParentCodeIn,
        FullCodeInLike
    }
}

