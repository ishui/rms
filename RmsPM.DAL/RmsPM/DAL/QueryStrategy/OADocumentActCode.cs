namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class OADocumentActCode : StandardQueryStringBuilder
    {
        public OADocumentActCode()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowAct", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((OADocumentWorkFlowActStrategyName) strategy.Name))
            {
                case OADocumentWorkFlowActStrategyName.ActCode:
                    strategy.RelationFieldName = "ActCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentWorkFlowActStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case OADocumentWorkFlowActStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
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
            OADocumentWorkFlowActStrategyName name = (OADocumentWorkFlowActStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

