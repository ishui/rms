namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;

    public class V_WBSTaskStrategyBuilder : StandardQueryStringBuilder
    {
        public V_WBSTaskStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("V_WBSTask", "SelectAll").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((V_WBSTaskStrategyName) strategy.Name))
            {
                case V_WBSTaskStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_WBSTaskStrategyName.CodeLike:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_WBSTaskStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_WBSTaskStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_WBSTaskStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_WBSTaskStrategyName.TaskName:
                    strategy.RelationFieldName = "TaskName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case V_WBSTaskStrategyName.TaskNameLike:
                    strategy.RelationFieldName = "TaskName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case V_WBSTaskStrategyName.PlannedStartDate:
                    strategy.RelationFieldName = "PlannedStartDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_WBSTaskStrategyName.PlannedFinishDate:
                    strategy.RelationFieldName = "PlannedFinishDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_WBSTaskStrategyName.ActualStartDate:
                    strategy.RelationFieldName = "ActualStartDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_WBSTaskStrategyName.ActualFinishDate:
                    strategy.RelationFieldName = "ActualFinishDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case V_WBSTaskStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_WBSTaskStrategyName.ImportantLevel:
                    strategy.RelationFieldName = "ImportantLevel";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_WBSTaskStrategyName.Exceed:
                    strategy.RelationFieldName = "Exceed";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_WBSTaskStrategyName.RelaType:
                    strategy.RelationFieldName = "RelaType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_WBSTaskStrategyName.RelaCode:
                    strategy.RelationFieldName = "RelaCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case V_WBSTaskStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case V_WBSTaskStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
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
            V_WBSTaskStrategyName name = (V_WBSTaskStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case V_WBSTaskStrategyName.UserAccess:
                        return string.Format(" WBSCode in ( select WBSCode from TaskPerson where UserCode='{0}' and (Type='2' or Type = '0')  ) ", strategy.GetParameter(0));

                    case V_WBSTaskStrategyName.PlannedStartDate:
                    case V_WBSTaskStrategyName.PlannedFinishDate:
                    case V_WBSTaskStrategyName.ActualStartDate:
                    case V_WBSTaskStrategyName.ActualFinishDate:
                    case V_WBSTaskStrategyName.Status:
                        return text;

                    case V_WBSTaskStrategyName.PreStatusNot:
                        return string.Format(" PreStatus != '{0}' ", strategy.GetParameter(0));

                    case V_WBSTaskStrategyName.Master:
                        return string.Format(" WBSCode in ( select A.WBSCode  from TaskPerson A,SystemUser B where A.UserCode =B.UserCode and B.UserName like '%{0}%')", strategy.GetParameter(0));

                    case V_WBSTaskStrategyName.FullCode:
                        return string.Format(" FullCode like '%{0}%' ", strategy.GetParameter(0));

                    case V_WBSTaskStrategyName.RelatedUser:
                        return string.Format("WBSCode in ( select WBSCode from TaskPerson where UserCode='{0}') ", strategy.GetParameter(0));

                    case V_WBSTaskStrategyName.AccessRange:
                        return this.GetFullCode(strategy.GetParameter(1));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        private string GetFullCode(string strUserCode)
        {
            string text = "";
            string queryString = string.Format("select fullcode from task where  wbscode in ( select wbscode from taskperson where type in (0,1,2,4) and ((roletype='0' and usercode='{0}')  or (roletype='1' and usercode in (select stationcode from userrole where usercode ='{0}')))) order by len(fullcode) desc ", strUserCode);
            DataSet set = new QueryAgent().ExecSqlForDataSet(queryString);
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                DataTable table = set.Tables[0];
                ArrayList list = new ArrayList();
                while (table.Rows.Count > 0)
                {
                    list.Add(table.Rows[table.Rows.Count - 1]["FullCode"].ToString());
                    string text3 = table.Rows[table.Rows.Count - 1]["FullCode"].ToString();
                    int num = table.Rows.Count - 1;
                    for (int i = num; i >= 0; i--)
                    {
                        if (table.Rows[i]["FullCode"].ToString().IndexOf(text3) > -1)
                        {
                            table.Rows.Remove(table.Rows[i]);
                        }
                    }
                }
                foreach (string text5 in list)
                {
                    text = text + ((text.Length < 1) ? "(" : " or ");
                    text = text + " fullcode like '" + text5 + "%'";
                }
                if (text.Length > 0)
                {
                    text = text + ")";
                }
                return text;
            }
            return " WBSCode = ''";
        }
    }
}

