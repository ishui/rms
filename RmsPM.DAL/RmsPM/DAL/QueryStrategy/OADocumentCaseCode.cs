namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentCaseCode : StandardQueryStringBuilder
    {
        public OADocumentCaseCode()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowCase", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADocumentWorkFlowCaseStrategyName) strategy.Name))
            {
                case OADocumentWorkFlowCaseStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentWorkFlowCaseStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentWorkFlowCaseStrategyName.ApplicationCode:
                    strategy.RelationFieldName = "ApplicationCode";
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
            OADocumentWorkFlowCaseStrategyName name = (OADocumentWorkFlowCaseStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

