namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class LeaveStrategyBuilder : StandardQueryStringBuilder
    {
        public LeaveStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Leave", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((LeaveStrategyName) strategy.Name))
            {
                case LeaveStrategyName.LeaveCode:
                    strategy.RelationFieldName = "LeaveCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeavePerson:
                    strategy.RelationFieldName = "LeavePerson";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveUnit:
                    strategy.RelationFieldName = "LeaveUnit";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveCause:
                    strategy.RelationFieldName = "LeaveCause";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case LeaveStrategyName.LeaveTime:
                    strategy.RelationFieldName = "LeaveTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveStartTime:
                    strategy.RelationFieldName = "LeaveStartTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveEndTime:
                    strategy.RelationFieldName = "LeaveEndTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveFlag:
                    strategy.RelationFieldName = "LeaveFlag";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveType:
                    strategy.RelationFieldName = "LeaveType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.LeaveSumTime:
                    strategy.RelationFieldName = "LeaveSumTime";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case LeaveStrategyName.QueryDateStart:
                    strategy.RelationFieldName = "LeaveStartTime";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;

                case LeaveStrategyName.QueryDateEnd:
                    strategy.RelationFieldName = "LeaveEndTime";
                    strategy.Type = StrategyType.DateTimeRange;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            LeaveStrategyName name = (LeaveStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

