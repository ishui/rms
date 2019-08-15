namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Configuration;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class WorkFlowActStrategyBuilder : StandardQueryStringBuilder
    {
        public WorkFlowActStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("WorkFlowAct", "SelectView").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WorkFlowActStrategyName) strategy.Name))
            {
                case WorkFlowActStrategyName.ActCode:
                    strategy.RelationFieldName = "ActCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowActStrategyName.CaseCode:
                    strategy.RelationFieldName = "CaseCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowActStrategyName.ProcedureCode:
                    strategy.RelationFieldName = "ProcedureCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowActStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.StringRange;
                    break;

                case WorkFlowActStrategyName.CaseStatus:
                    strategy.RelationFieldName = "CaseStatus";
                    strategy.Type = StrategyType.StringRange;
                    break;

                case WorkFlowActStrategyName.CopyState:
                    strategy.RelationFieldName = "Copy";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowActStrategyName.FromUserCode:
                    strategy.RelationFieldName = "FromUserCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WorkFlowActStrategyName.CurrentTaskCode:
                    strategy.RelationFieldName = "ToTaskCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildAccessTypeString(string operationCode, string userCode, string codes, string createUserColumnName)
        {
            string text = "";
            string functionStructureParentCode = SystemManageDAO.GetFunctionStructureParentCode(operationCode);
            string queryString = string.Format(" select accessRange.groupCode,SystemGroup.FullID,RoleLevel from accessRange left join SystemGroup on accessRange.GroupCode=SystemGroup.GroupCode where OperationCode = '{0}' and (( AccessRangeType=0 and relationCode = '{1}' ) or ( AccessRangeType=1 and relationCode in ( {2} ) )  ) ", new object[] { operationCode, userCode, codes });
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (!row.IsNull("FullID"))
                {
                    string text4 = row["FullID"].ToString();
                    int num = 0;
                    if (!row.IsNull("RoleLevel"))
                    {
                        num = (int) row["RoleLevel"];
                    }
                    if (num == 0)
                    {
                        text = text + string.Format("or (CaseCode in ( Select a2.CaseCode from  workflowCase a2 where a2.ProcedureCode in(select c1.ProcedureCode from WorkFlowProcedure c1 where isnull(c1.SysType, '') = '') and exists(select 1 from SystemGroup a1 where a1.GroupName in\t(dbo.GetProcedureNameByCaseCode(a2.CaseCode))\tand a1.ParentCode in (select a.GroupCode from SystemGroup a where a.GroupName =dbo.GetProjectNameByCaseCode(a2.CaseCode) and a.classcode='{1}') and a1.FullID like '{0}%'))\t)", text4, functionStructureParentCode);
                    }
                }
            }
            return text;
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            string text2;
            WorkFlowActStrategyName name = (WorkFlowActStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type != StrategyType.Other)
            {
                return StandardStrategyStringBuilder.BuildStrategyString(strategy);
            }
            switch (name)
            {
                case WorkFlowActStrategyName.ProcedureCodeIn:
                    return string.Format(" ProcedureCode in ({0})", strategy.GetParameter(0));

                case WorkFlowActStrategyName.Status:
                case WorkFlowActStrategyName.CaseStatus:
                case WorkFlowActStrategyName.CopyState:
                case WorkFlowActStrategyName.FromUserCode:
                case WorkFlowActStrategyName.CurrentTaskCode:
                    return text;

                case WorkFlowActStrategyName.ActUserCode:
                    return string.Format(" ActCode in (select a.actcode from workflowact a,workflowactuser b where a.actcode=b.actcode and (a.actusercode='{0}' or b.usercode='{0}'))", strategy.GetParameter(0));

                case WorkFlowActStrategyName.InActUser:
                    return string.Format(" exists ( select 1 from WorkFlowActUser where WorkFlowActUser.ActCode=V_WorkFlowAct.ActCode and WorkFlowActUser.UserCode='{0}' ) ", strategy.GetParameter(0));

                case WorkFlowActStrategyName.IsCaseActUser:
                    return string.Format(" CaseCode in (select CaseCode from V_WorkFlowAct where ActUserCode='{0}') and Copy=0 ", strategy.GetParameter(0));

                case WorkFlowActStrategyName.SendActUser:
                    return string.Format(" FromUserCode ='{0}' and Status='Begin'", strategy.GetParameter(0));

                case WorkFlowActStrategyName.StatusBegin:
                    return string.Format(" (Status='{0}' or (CopyFromActCode!='' and Status!='End'))", strategy.GetParameter(0));

                case WorkFlowActStrategyName.IsNotActUser:
                    return string.Format(" CaseCode not in (select CaseCode from V_WorkFlowAct where ActUserCode='{0}') and Copy=0 ", strategy.GetParameter(0));

                case WorkFlowActStrategyName.ActMeetOrder:
                    return string.Format(" ActCode not in (select ActCode from V_WorkFlowAct where copy='0' and TaskActorID != '' and substring(ActUnitCode,1,2) = 'no')", new object[0]);

                case WorkFlowActStrategyName.ProjectCode:
                    return string.Format(" CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='项目代码' and workflowprocedurepropertyvalue='{0}')", strategy.GetParameter(0));

                case WorkFlowActStrategyName.Title:
                    return string.Format(" CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='主题' and workflowprocedurepropertyvalue like '%{0}%')", strategy.GetParameter(0));

                case WorkFlowActStrategyName.CurrentTaskName:
                    return string.Format(" ToTaskName like '%{0}%'", strategy.GetParameter(0));

                case WorkFlowActStrategyName.SignDate:
                    return string.Format(" (SignDate>'{0}' and SignDate<'{1}')", strategy.GetParameter(0), strategy.GetParameter(1));

                case WorkFlowActStrategyName.FinishDate:
                    return string.Format(" (FinishDate>'{0}' and FinishDate<'{1}')", strategy.GetParameter(0), strategy.GetParameter(1));

                case WorkFlowActStrategyName.FromDate:
                    return string.Format(" (FromDate>'{0}' and FromDate<'{1}')", strategy.GetParameter(0), strategy.GetParameter(1));

                case WorkFlowActStrategyName.StatusBeginAndDealWith:
                    return string.Format(" (Status='Begin' or (CopyFromActCode!='' and Status!='End') or Status='DealWith') ", new object[0]);

                case WorkFlowActStrategyName.FlowNumber:
                    return string.Format(" ( CaseCode like '%{0}%' or CaseCode in (select workflowcasecode from workflowprocedureproperty a, workflowcaseproperty b where a.workflowprocedurepropertycode=b.workflowprocedurepropertycode and procedurepropertyname='流水号' and workflowprocedurepropertyvalue like '%{0}%'))", strategy.GetParameter(0));

                case WorkFlowActStrategyName.AccessRange:
                    text2 = ConfigurationSettings.AppSettings["FlowProjectGroup"];
                    if (text2 != "1")
                    {
                        text = " (( ProcedureCode in (select ProcedureCode from workflowprocedure where " + AccessRanggeQuery.BuildAccessRangeStringNoGroupCode(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("WorkFlowProcedure"), SystemClassDescription.GetItemKeyColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemTypeColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemCreateUserColumnName("WorkFlowProcedure")) + "))";
                        break;
                    }
                    text = " (( ProcedureCode in (select ProcedureCode from workflowprocedure where " + AccessRanggeQuery.BuildAccessRangeString(strategy.GetParameter(0), strategy.GetParameter(1), strategy.GetParameter(2), SystemClassDescription.GetItemTableName("WorkFlowProcedure"), SystemClassDescription.GetItemKeyColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemTypeColumnName("WorkFlowProcedure"), SystemClassDescription.GetItemCreateUserColumnName("WorkFlowProcedure")) + "))";
                    break;

                default:
                    return text;
            }
            if (strategy.GetParameter(3) != "")
            {
                text = text + string.Format(" or (CaseCode in (select CaseCode from V_WorkFlowAct where ActUserCode='{0}') and Copy=0 )", strategy.GetParameter(3));
            }
            if (text2 == "1")
            {
                string codes = AccessRanggeQuery.BuildStationCodeString(strategy.GetParameter(2));
                text = text + this.BuildAccessTypeString(strategy.GetParameter(0), strategy.GetParameter(1), codes, SystemClassDescription.GetItemCreateUserColumnName("WorkFlowProcedure"));
            }
            return (text + ") ");
        }
    }
}

