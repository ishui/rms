namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetBackupSetStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetBackupSet", "SelectView").SqlString;

        public CostBudgetBackupSetStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetBackupSet", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetBackupSetStrategyName) strategy.Name))
            {
                case CostBudgetBackupSetStrategyName.CostBudgetBackupSetCode:
                    strategy.RelationFieldName = "CostBudgetBackupSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.CostBudgetBackupCode:
                    strategy.RelationFieldName = "CostBudgetBackupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.CostBudgetSetName:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.CostBudgetSetNameLike:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetBackupSetStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.BackupPerson:
                    strategy.RelationFieldName = "BackupPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupSetStrategyName.BackupDateRange:
                    strategy.RelationFieldName = "BackupDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public static string BuildAccessRangeString(string UserCode, string StationCodes)
        {
            return AccessRanggeQuery.BuildAccessRangeString("041101", UserCode, StationCodes, SystemClassDescription.GetItemTableName("CostBudgetBackupSet"), SystemClassDescription.GetItemKeyColumnName("CostBudgetBackupSet"), SystemClassDescription.GetItemTypeColumnName("CostBudgetBackupSet"), SystemClassDescription.GetItemCreateUserColumnName("CostBudgetBackupSet"));
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + base.BuildStrategysString() + base.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            CostBudgetBackupSetStrategyName name = (CostBudgetBackupSetStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case CostBudgetBackupSetStrategyName.GroupCodeEx:
                    {
                        string systemGroupCode = strategy.GetParameter(0);
                        switch (strategy.GetParameter(1))
                        {
                            case "0":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNodeIncludeSelf, SystemClassDescription.GetItemTypeColumnName("CostBudgetBackupSet"));

                            case "1":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetBackupSet"));

                            case "2":
                                return SystemGroupStrategyBuilder.BuildTreeNodeSearchString(systemGroupCode, TreeNodeSearchType.AllSubNotLeafNode, SystemClassDescription.GetItemTypeColumnName("CostBudgetBackupSet"));
                        }
                        return text;
                    }
                    case CostBudgetBackupSetStrategyName.AccessRange:
                        return BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1));

                    case CostBudgetBackupSetStrategyName.False:
                        return "1=2";
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

