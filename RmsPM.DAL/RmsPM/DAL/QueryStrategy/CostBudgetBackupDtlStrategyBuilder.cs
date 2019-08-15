namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class CostBudgetBackupDtlStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetBackupDtl", "SelectView").SqlString;

        public CostBudgetBackupDtlStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetBackupDtl", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetBackupDtlStrategyName) strategy.Name))
            {
                case CostBudgetBackupDtlStrategyName.CostBudgetBackupDtlCode:
                    strategy.RelationFieldName = "CostBudgetBackupDtlCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostBudgetBackupSetCode:
                    strategy.RelationFieldName = "CostBudgetBackupSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostBudgetBackupCode:
                    strategy.RelationFieldName = "CostBudgetBackupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostCode:
                    strategy.RelationFieldName = "CostCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostBudgetSetCode:
                    strategy.RelationFieldName = "CostBudgetSetCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostBudgetSetName:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.CostBudgetSetNameLike:
                    strategy.RelationFieldName = "CostBudgetSetName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetBackupDtlStrategyName.UnitCode:
                    strategy.RelationFieldName = "UnitCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.GroupCode:
                    strategy.RelationFieldName = "GroupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.PBSType:
                    strategy.RelationFieldName = "PBSType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.PBSCode:
                    strategy.RelationFieldName = "PBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupDtlStrategyName.VerID:
                    strategy.RelationFieldName = "VerID";
                    strategy.Type = StrategyType.IntegerEqual;
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
            CostBudgetBackupDtlStrategyName name = (CostBudgetBackupDtlStrategyName) strategy.Name;
            string text2 = "";
            string text3 = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            CostBudgetBackupDtlStrategyName name2 = name;
            if (name2 != CostBudgetBackupDtlStrategyName.False)
            {
                if (name2 != CostBudgetBackupDtlStrategyName.CostCodeIn)
                {
                    if (name2 != CostBudgetBackupDtlStrategyName.GroupCodeEx)
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

