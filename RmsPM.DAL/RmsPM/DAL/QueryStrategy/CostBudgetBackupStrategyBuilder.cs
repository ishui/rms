namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class CostBudgetBackupStrategyBuilder : StandardQueryStringBuilder
    {
        private string QueryViewString = SqlManager.GetSqlStruct("CostBudgetBackup", "SelectView").SqlString;

        public CostBudgetBackupStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("CostBudgetBackup", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((CostBudgetBackupStrategyName) strategy.Name))
            {
                case CostBudgetBackupStrategyName.CostBudgetBackupCode:
                    strategy.RelationFieldName = "CostBudgetBackupCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupStrategyName.CostBudgetBackupName:
                    strategy.RelationFieldName = "CostBudgetBackupName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupStrategyName.CostBudgetBackupNameLike:
                    strategy.RelationFieldName = "CostBudgetBackupName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case CostBudgetBackupStrategyName.BackupPerson:
                    strategy.RelationFieldName = "BackupPerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case CostBudgetBackupStrategyName.BackupDateRange:
                    strategy.RelationFieldName = "BackupDate";
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
            CostBudgetBackupStrategyName name = (CostBudgetBackupStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                CostBudgetBackupStrategyName name2 = name;
                if (name2 != CostBudgetBackupStrategyName.False)
                {
                    if (name2 != CostBudgetBackupStrategyName.OnlyBackup)
                    {
                        return "";
                    }
                }
                else
                {
                    return "1=2";
                }
                return string.Format("CostBudgetBackupCode not like '{0}%'", CostBudgetDAO.m_OfflineBackupCodeStartWith);
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

