namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowCasePropertyStrategyBuilder : StandardQueryStringBuilder
    {
        public WorkFlowCasePropertyStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowCaseProperty", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowCasePropertyStrategyName) strategy.Name))
            {
                case WorkFlowCasePropertyStrategyName.WorkFlowCasePropertyCode:
                    strategy.RelationFieldName = "WorkFlowCasePropertyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCasePropertyStrategyName.WorkFlowCaseCode:
                    strategy.RelationFieldName = "WorkFlowCaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCasePropertyStrategyName.WorkFlowProcedurePropertyCode:
                    strategy.RelationFieldName = "WorkFlowProcedurePropertyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCasePropertyStrategyName.WorkFlowProcedurePropertyValue:
                    strategy.RelationFieldName = "WorkFlowProcedurePropertyValue";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCasePropertyStrategyName.Remak:
                    strategy.RelationFieldName = "Remak";
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
            WorkFlowCasePropertyStrategyName name = (WorkFlowCasePropertyStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

