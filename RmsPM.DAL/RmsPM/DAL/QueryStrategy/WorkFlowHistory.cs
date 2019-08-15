namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using Rms.ORMap;

    public class WorkFlowHistory : StandardQueryStringBuilder
    {
        public WorkFlowHistory()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_WorkFlowHistory", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowHistoryStrategyName) strategy.Name))
            {
                case WorkFlowHistoryStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowHistoryStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowHistoryStrategyName.SourceUserCode:
                    strategy.RelationFieldName = "SourceUserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowHistoryStrategyName.Status:
                    strategy.RelationFieldName = "Status";
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
            WorkFlowHistoryStrategyName name = (WorkFlowHistoryStrategyName) strategy.Name;
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case WorkFlowHistoryStrategyName.ActUserCode:
                        return string.Format(" CaseCode in (select a.CaseCode from workflowact a,workflowcase b where a.casecode=b.casecode and ActUserCode='{0}')", strategy.GetParameter(0));

                    case WorkFlowHistoryStrategyName.ProcedureNameAndApplicationCodein:
                        return string.Format(" ProcedureName+ApplicationCode in ({0})", strategy.GetParameter(0));

                    case WorkFlowHistoryStrategyName.ProjectCode:
                        return string.Format(" CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='项目代码' and workflowprocedurepropertyvalue='{0}')", strategy.GetParameter(0));

                    case WorkFlowHistoryStrategyName.Title:
                        return string.Format(" CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='主题' and workflowprocedurepropertyvalue like '%{0}%')", strategy.GetParameter(0));

                    case WorkFlowHistoryStrategyName.CreateDate:
                        return string.Format(" (CreateDate>'{0}' and CreateDate<'{1}')", strategy.GetParameter(0), strategy.GetParameter(1));

                    case WorkFlowHistoryStrategyName.FinishDate:
                        return string.Format(" (FinishDate>'{0}' and FinishDate<'{1}')", strategy.GetParameter(0), strategy.GetParameter(1));

                    case WorkFlowHistoryStrategyName.FlowNumber:
                        return string.Format(" ( CaseCode like '%{0}%' or CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='流水号' and workflowprocedurepropertyvalue like '%{0}%'))", strategy.GetParameter(0));

                    case WorkFlowHistoryStrategyName.ProcedureCodeIn:
                        return string.Format(" ProcedureCode in ({0})", strategy.GetParameter(0));
                }
                return "";
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }
    }
}

