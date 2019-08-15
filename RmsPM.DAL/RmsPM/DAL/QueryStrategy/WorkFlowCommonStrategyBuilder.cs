namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowCommonStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryViewString = "";

        public WorkFlowCommonStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowCommon", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("WorkFlowCommon", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowCommonStrategyName) strategy.Name))
            {
                case WorkFlowCommonStrategyName.ProjectCode:
                    strategy.RelationFieldName = "WorkFlowCommon.ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCommonStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case WorkFlowCommonStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "WorkFlowCommon.ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCommonStrategyName.WorkFlowTitle:
                    strategy.RelationFieldName = "WorkFlowTitle";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WorkFlowCommonStrategyName.WorkFlowID:
                    strategy.RelationFieldName = "WorkFlowID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WorkFlowCommonStrategyName.Type:
                    strategy.RelationFieldName = "Type";
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
            WorkFlowCommonStrategyName name = (WorkFlowCommonStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                //switch (name)
                //{
                //    case WorkFlowCommonStrategyName.AccessRange:
                //        return "";
                //}
                return AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("WorkFlowCommon"), SystemClassDescription.GetItemKeyColumnName("WorkFlowCommon"), SystemClassDescription.GetItemTypeColumnName("WorkFlowCommon"), SystemClassDescription.GetItemCreateUserColumnName("WorkFlowCommon"));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

