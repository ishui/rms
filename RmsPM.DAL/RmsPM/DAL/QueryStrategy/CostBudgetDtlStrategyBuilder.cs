namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetDtlStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetDtl", "SelectView").SqlString;

        public CostBudgetDtlStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetDtl", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetDtlStrategyName) strategy.Name))
            {
                case CostBudgetDtlStrategyName.CostBudgetDtlCode:
                    strategy.RelationFieldName = "CostBudgetDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CostBudgetCode:
                    strategy.RelationFieldName = "CostBudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CostBudgetSetName:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CostBudgetSetNameLike:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetDtlStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case CostBudgetDtlStrategyName.FirstCostBudgetDtlCode:
                    strategy.RelationFieldName = "FirstCostBudgetDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.VerID:
                    strategy.RelationFieldName = "VerID";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetDtlStrategyName.TargetFlag:
                    strategy.RelationFieldName = "TargetFlag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetDtlStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetDtlStrategyName.CheckPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetDtlStrategyName.CheckDateRange:
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
            CostBudgetDtlStrategyName name = (CostBudgetDtlStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            CostBudgetDtlStrategyName name2 = name;
            if (name2 != CostBudgetDtlStrategyName.False)
            {
                if (name2 != CostBudgetDtlStrategyName.CostCodeIn)
                {
                    if (name2 != CostBudgetDtlStrategyName.GroupCodeEx)
                    {
                        return text3;
                    }
                    string systemGroupCode = strategy.GetParameter(0);
                    string parameter = strategy.GetParameter(1);
                    if (parameter == null)
                    {
                        return text3;
                    }
                    if (parameter != "0")
                    {
                        if (parameter != "1")
                        {
                            if (parameter != "2")
                            {
                                return text3;
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
                text2 = StrategyConvert.BuildInStr(strategy.GetParameter(0));
                if (text2 != "")
                {
                    text3 = string.Format(" CostCode in ({0}) ", text2);
                }
                return text3;
            }
            return "1=2";
        }
    }
}

