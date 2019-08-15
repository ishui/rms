namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractCostPlanColumn
    {
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 3,
        [ColumnEnum("ContractCostCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCostCode")]
        ContractCostCode = 2,
        [ColumnEnum("ContractCostPlanCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ContractCostPlanCode")]
        ContractCostPlanCode = 1,
        [ColumnEnum("Money", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("Money")]
        Money = 4,
        [EnumTextValue("PayConditionText"), ColumnEnum("PayConditionText", typeof(string), DbType.AnsiString, false, false, true, 200)]
        PayConditionText = 6,
        [ColumnEnum("PlanningPayDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("PlanningPayDate")]
        PlanningPayDate = 5
    }
}

