namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewCostDynamicListString = SqlManager.GetSqlStruct("CostBudget", "SelectViewCostDynamicList").SqlString;
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudget", "SelectView").SqlString;

        public CostBudgetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudget", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetStrategyName) strategy.Name))
            {
                case CostBudgetStrategyName.CostBudgetCode:
                    strategy.RelationFieldName = "CostBudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case CostBudgetStrategyName.FirstCostBudgetCode:
                    strategy.RelationFieldName = "FirstCostBudgetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.VerID:
                    strategy.RelationFieldName = "VerID";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetStrategyName.TargetFlag:
                    strategy.RelationFieldName = "TargetFlag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case CostBudgetStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.CreateDateRange:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetStrategyName.CheckPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.CheckDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetStrategyName.CostBudgetSetName:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.CostBudgetSetNameLike:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.PBSTypeCode:
                    strategy.RelationFieldName = "PBSTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetStrategyName.SetType:
                    strategy.RelationFieldName = "SetType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public static string BuildAccessRangeString(string UserCode, string StationCodes)
        {
            return AccessRanggeQuery.BuildAccessRangeString("041101", UserCode, StationCodes, SystemClassDescription.GetItemTableName("CostBudget"), SystemClassDescription.GetItemKeyColumnName("CostBudget"), SystemClassDescription.GetItemTypeColumnName("CostBudget"), SystemClassDescription.GetItemCreateUserColumnName("CostBudget"));
        }

        public string BuildQueryViewCostDynamicListString()
        {
            return (this.QueryViewCostDynamicListString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetStrategyName name = (CostBudgetStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostBudgetStrategyName.False:
                        return "1=2";

                    case CostBudgetStrategyName.CostBudgetCode:
                        return text;

                    case CostBudgetStrategyName.ExceptCostBudgetCode:
                        return string.Format("CostBudgetCode <> '{0}'", strategy.GetParameter(0));

                    case CostBudgetStrategyName.GroupCodeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"));
                        }
                        return text;
                    }
                    case CostBudgetStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

