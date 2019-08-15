namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ContractMaterialPlanColumn
    {
        [ColumnEnum("ContractCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractCode")]
        ContractCode = 3,
        [ColumnEnum("ContractMaterialCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ContractMaterialCode")]
        ContractMaterialCode = 2,
        [EnumTextValue("ContractMaterialPlanCode"), ColumnEnum("ContractMaterialPlanCode", typeof(string), DbType.AnsiString, true, false, false, 50)]
        ContractMaterialPlanCode = 1,
        [ColumnEnum("PlanDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("PlanDate")]
        PlanDate = 4,
        [ColumnEnum("PlanQty", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PlanQty")]
        PlanQty = 5
    }
}

