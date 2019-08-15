namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TaskGuidStrategyBuilder : StandardQueryStringBuilder
    {
        public TaskGuidStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("TaskGuid", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((GuidStrategyName) strategy.Name))
            {
                case GuidStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case GuidStrategyName.UserCode:
                    strategy.RelationFieldName = "UserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            GuidStrategyName name = (GuidStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case GuidStrategyName.AccessRange:
                //        return "";
                //}
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), "taskGuid", "taskGuidCode");
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

