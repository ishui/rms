namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowCaseStrategyBuilder : StandardQueryStringBuilder
    {
        public WorkFlowCaseStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowCase", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowCaseStrategyName) strategy.Name))
            {
                case WorkFlowCaseStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCaseStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCaseStrategyName.ApplicationCode:
                    strategy.RelationFieldName = "ApplicationCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowCaseStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringRange;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            WorkFlowCaseStrategyName name = (WorkFlowCaseStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                WorkFlowCaseStrategyName name2 = name;
                if (name2 != WorkFlowCaseStrategyName.ProcedureName)
                {
                    if (name2 != WorkFlowCaseStrategyName.ProcedureCodeIn)
                    {
                        return "";
                    }
                }
                else
                {
                    return string.Format(" exists ( select 1 from WorkFlowProcedure where procedureName='{0}'   ) ", strategy.GetParameter(0));
                }
                return string.Format(" ProcedureCode in ({0}) ", strategy.GetParameter(0));
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

