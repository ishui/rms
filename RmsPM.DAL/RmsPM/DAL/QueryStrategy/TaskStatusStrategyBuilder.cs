namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class TaskStatusStrategyBuilder : StandardQueryStringBuilder
    {
        public TaskStatusStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowAct", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((TaskActorStrategyName) strategy.Name))
            {
                case TaskActorStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case TaskActorStrategyName.ToTaskCode:
                    strategy.RelationFieldName = "ToTaskCode";
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
            TaskActorStrategyName name = (TaskActorStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case TaskActorStrategyName.Status:
                        return string.Format(" Status != '{0}' ", strategy.GetParameter(0));

                    case TaskActorStrategyName.Copy:
                        return string.Format(" Copy != '{0}' ", strategy.GetParameter(0));
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

