namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetMonthStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetMonth", "SelectView").SqlString;

        public CostBudgetMonthStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetMonth", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetMonthStrategyName) strategy.Name))
            {
                case CostBudgetMonthStrategyName.CostBudgetMonthCode:
                    strategy.RelationFieldName = "CostBudgetMonthCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.CostBudgetDtlCode:
                    strategy.RelationFieldName = "CostBudgetDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.CostBudgetCode:
                    strategy.RelationFieldName = "CostBudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.IYear:
                    strategy.RelationFieldName = "IYear";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetMonthStrategyName.IMonth:
                    strategy.RelationFieldName = "IMonth";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetMonthStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.CostBudgetMonthSetCode:
                    strategy.RelationFieldName = "CostBudgetMonthSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case CostBudgetMonthStrategyName.FirstCostBudgetMonthCode:
                    strategy.RelationFieldName = "FirstCostBudgetMonthCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.VerID:
                    strategy.RelationFieldName = "VerID";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetMonthStrategyName.TargetFlag:
                    strategy.RelationFieldName = "TargetFlag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetMonthStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetMonthStrategyName.CheckPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetMonthStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetMonthStrategyName name = (CostBudgetMonthStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                CostBudgetMonthStrategyName name2 = name;
                if (name2 != CostBudgetMonthStrategyName.False)
                {
                    if (name2 != CostBudgetMonthStrategyName.GroupCodeEx)
                    {
                        return text;
                    }
                }
                else
                {
                    return "1=2";
                }
                string systemGroupCode = strategy.GetParameter(0);
                string parameter = strategy.GetParameter(1);
                if (parameter == null)
                {
                    return text;
                }
                if (parameter != "0")
                {
                    if (parameter != "1")
                    {
                        if (parameter != "2")
                        {
                            return text;
                        }
                        return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));
                    }
                }
                else
                {
                    return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));
                }
                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

