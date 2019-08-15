namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowProcedurePropertyStrategyBuilder : StandardQueryStringBuilder
    {
        public WorkFlowProcedurePropertyStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowProcedureProperty", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowProcedurePropertyStrategyName) strategy.Name))
            {
                case WorkFlowProcedurePropertyStrategyName.WorkFlowProcedurePropertyCode:
                    strategy.RelationFieldName = "WorkFlowProcedurePropertyCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedurePropertyStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedurePropertyStrategyName.ProcedurePropertyName:
                    strategy.RelationFieldName = "ProcedurePropertyName";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedurePropertyStrategyName.ProcedurePropertyType:
                    strategy.RelationFieldName = "ProcedurePropertyType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowProcedurePropertyStrategyName.Remak:
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
            WorkFlowProcedurePropertyStrategyName name = (WorkFlowProcedurePropertyStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

