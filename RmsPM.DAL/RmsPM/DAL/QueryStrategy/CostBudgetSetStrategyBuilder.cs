namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetSetStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetSet", "SelectView").SqlString;

        public CostBudgetSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetSet", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetSetStrategyName) strategy.Name))
            {
                case CostBudgetSetStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.CostBudgetSetName:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.CostBudgetSetNameLike:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetSetStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.CreatePerson:
                    strategy.RelationFieldName = "CreatePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.CreateDateRange:
                    strategy.RelationFieldName = "CreateDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetSetStrategyName.ModifyPerson:
                    strategy.RelationFieldName = "ModifyPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.ModifyDateRange:
                    strategy.RelationFieldName = "ModifyDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case CostBudgetSetStrategyName.PBSTypeCode:
                    strategy.RelationFieldName = "PBSTypeCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetSetStrategyName.SetType:
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
            return AccessRanggeQuery.BuildAccessRangeString("041101", UserCode, StationCodes, SystemClassDescription.GetItemTableName("CostBudgetSet"), SystemClassDescription.GetItemKeyColumnName("CostBudgetSet"), SystemClassDescription.GetItemTypeColumnName("CostBudgetSet"), SystemClassDescription.GetItemCreateUserColumnName("CostBudgetSet"));
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetSetStrategyName name = (CostBudgetSetStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostBudgetSetStrategyName.GroupCodeEx:
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
                    case CostBudgetSetStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));

                    case CostBudgetSetStrategyName.False:
                        return "1=2";

                    case CostBudgetSetStrategyName.NoTarget:
                        return "not Exists(select * from CostBudget b where isnull(b.TargetFlag, 0) = 1 and b.CostBudgetSetCode = CostBudgetSet.CostBudgetSetCode)";

                    case CostBudgetSetStrategyName.NoBudget:
                        return "not Exists(select * from CostBudget b where isnull(b.TargetFlag, 0) = 0 and b.CostBudgetSetCode = CostBudgetSet.CostBudgetSetCode)";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

