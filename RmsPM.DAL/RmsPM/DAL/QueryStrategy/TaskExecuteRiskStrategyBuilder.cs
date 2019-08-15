namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TaskExecuteRiskStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public TaskExecuteRiskStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskExecuteRisk", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("TaskExecuteRisk", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TaskExecuteRiskStrategyName) strategy.Name))
            {
                case TaskExecuteRiskStrategyName.TaskExecuteRiskCode:
                    strategy.RelationFieldName = "TaskExecuteRiskCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteRiskStrategyName.TaskExecuteCode:
                    strategy.RelationFieldName = "TaskExecuteCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteRiskStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteRiskStrategyName.RiskTypeName:
                    strategy.RelationFieldName = "RiskTypeName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteRiskStrategyName.RiskIndexCode:
                    strategy.RelationFieldName = "RiskIndexCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteRiskStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
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
            TaskExecuteRiskStrategyName name = (TaskExecuteRiskStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case TaskExecuteRiskStrategyName.False:
                        return "";
                }
                return "1=2";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

