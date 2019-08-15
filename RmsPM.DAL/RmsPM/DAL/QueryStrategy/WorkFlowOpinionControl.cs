namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowOpinionControl : StandardQueryStringBuilder
    {
        public WorkFlowOpinionControl()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowOpinion", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowOpinionStrategyName) strategy.Name))
            {
                case WorkFlowOpinionStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowOpinionStrategyName.TaskID:
                    strategy.RelationFieldName = "TaskID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowOpinionStrategyName.TaskActorID:
                    strategy.RelationFieldName = "TaskActorID";
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
            WorkFlowOpinionStrategyName name = (WorkFlowOpinionStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

