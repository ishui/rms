namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TaskExecuteStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public TaskExecuteStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskExecute", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("TaskExecute", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TaskExecuteStrategyName) strategy.Name))
            {
                case TaskExecuteStrategyName.TaskExecuteCode:
                    strategy.RelationFieldName = "TaskExecuteCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteStrategyName.ExecutePerson:
                    strategy.RelationFieldName = "ExecutePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteStrategyName.Detail:
                    strategy.RelationFieldName = "Detail";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskExecuteStrategyName.ExecuteDate:
                    strategy.RelationFieldName = "ExecuteDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case TaskExecuteStrategyName.InputDate:
                    strategy.RelationFieldName = "InputDate";
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
            TaskExecuteStrategyName name = (TaskExecuteStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                TaskExecuteStrategyName name2 = name;
                if (name2 != TaskExecuteStrategyName.False)
                {
                    if (name2 != TaskExecuteStrategyName.AccessRange)
                    {
                        return "";
                    }
                }
                else
                {
                    return "1=2";
                }
                return string.Format("  exists(select wbscode from taskperson where type in (0,1,2,4,9) and ((roletype='0' and usercode='{0}')  or (roletype='1' and usercode in  (select stationcode from userrole where usercode ='{0}'))))", strategy.GetParameter(1));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

