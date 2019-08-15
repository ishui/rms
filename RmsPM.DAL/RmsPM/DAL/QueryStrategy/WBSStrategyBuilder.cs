namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class WBSStrategyBuilder : StandardQueryStringBuilder
    {
        public string QueryDeskTopExceedString = "";
        public string QueryDeskTopString = "";
        public string QueryViewString = "";

        public WBSStrategyBuilder()
        {
            base.QueryMainString = SqlManager.GetSqlStruct("Task", "SelectAll").SqlString;
            this.QueryViewString = SqlManager.GetSqlStruct("Task", "SelectView").SqlString;
            this.QueryDeskTopString = SqlManager.GetSqlStruct("Task", "SelectForDeskTop").SqlString;
            this.QueryDeskTopExceedString = SqlManager.GetSqlStruct("Task", "SelectForDeskTopExceed").SqlString;
            base.IsNeedWhere = true;
        }

        public override void AddStrategy(Strategy strategy)
        {
            switch (((WBSStrategyName) strategy.Name))
            {
                case WBSStrategyName.WBSCode:
                    strategy.RelationFieldName = "WBSCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WBSStrategyName.CodeLike:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WBSStrategyName.ProjectCode:
                    strategy.RelationFieldName = "ProjectCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WBSStrategyName.ParentCode:
                    strategy.RelationFieldName = "ParentCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WBSStrategyName.Deep:
                    strategy.RelationFieldName = "Deep";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case WBSStrategyName.TaskName:
                    strategy.RelationFieldName = "TaskName";
                    strategy.Type = StrategyType.StringLike;
                    break;

                case WBSStrategyName.TaskNameLike:
                    strategy.RelationFieldName = "TaskName";
                    strategy.Type = StrategyType.StringLike;
                    strategy.SetParameter(0, "%" + strategy.GetParameter(0) + "%");
                    break;

                case WBSStrategyName.PlannedStartDate:
                    strategy.RelationFieldName = "PlannedStartDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case WBSStrategyName.PlannedFinishDate:
                    strategy.RelationFieldName = "PlannedFinishDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case WBSStrategyName.ActualStartDate:
                    strategy.RelationFieldName = "ActualStartDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case WBSStrategyName.ActualFinishDate:
                    strategy.RelationFieldName = "ActualFinishDate";
                    strategy.Type = StrategyType.DateTimeRangeOnlyDate;
                    break;

                case WBSStrategyName.Status:
                    strategy.RelationFieldName = "Status";
                    strategy.Type = StrategyType.NumberIn;
                    break;

                case WBSStrategyName.ImportantLevel:
                    strategy.RelationFieldName = "ImportantLevel";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case WBSStrategyName.Exceed:
                    strategy.RelationFieldName = "Exceed";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case WBSStrategyName.RelaType:
                    strategy.RelationFieldName = "RelaType";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WBSStrategyName.RelaCode:
                    strategy.RelationFieldName = "RelaCode";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                case WBSStrategyName.Flag:
                    strategy.RelationFieldName = "Flag";
                    strategy.Type = StrategyType.IntegerEqual;
                    break;

                case WBSStrategyName.SortID:
                    strategy.RelationFieldName = "SortID";
                    strategy.Type = StrategyType.StringEqual;
                    break;

                default:
                    strategy.Type = StrategyType.Other;
                    break;
            }
            base.AddStrategy(strategy);
        }

        public string BuildQueryDeskTopExceedString()
        {
            return (this.QueryDeskTopExceedString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public string BuildQueryDeskTopString()
        {
            return (this.QueryDeskTopString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public string BuildQueryViewString()
        {
            return (this.QueryViewString + this.BuildStrategysString() + this.BuildOrderString());
        }

        public override string BuildSingleStrategyString(Strategy strategy)
        {
            WBSStrategyName name = (WBSStrategyName) strategy.Name;
            string text = "";
            if (strategy.Type == StrategyType.Other)
            {
                switch (name)
                {
                    case WBSStrategyName.UserAccess:
                        return string.Format(" WBSCode in ( select WBSCode from TaskPerson where UserCode='{0}' and Type in (0, 1, 2, 4, 9)  ) ", strategy.GetParameter(0));

                    case WBSStrategyName.PlannedStartDate:
                    case WBSStrategyName.PlannedFinishDate:
                    case WBSStrategyName.ActualStartDate:
                    case WBSStrategyName.ActualFinishDate:
                    case WBSStrategyName.Status:
                        return text;

                    case WBSStrategyName.StatusNot:
                        return string.Format(" Status not in  ({0})", strategy.GetParameter(0));

                    case WBSStrategyName.PreStatusNot:
                        return string.Format(" PreStatus != '{0}' ", strategy.GetParameter(0));

                    case WBSStrategyName.Master:
                        return string.Format(" WBSCode in ( select A.WBSCode  from TaskPerson A,SystemUser B where A.UserCode =B.UserCode and B.UserName like '%{0}%')", strategy.GetParameter(0));

                    case WBSStrategyName.FullCode:
                        return string.Format(" FullCode like '%{0}%' ", strategy.GetParameter(0));

                    case WBSStrategyName.RelatedUser:
                        return string.Format("WBSCode in ( select WBSCode from TaskPerson where UserCode='{0}') ", strategy.GetParameter(0));

                    case WBSStrategyName.AccessRange:
                        if (strategy.GetParameterCount() <= 2)
                        {
                            return GetFullCode(strategy.GetParameter(1));
                        }
                        return GetFullCode(strategy.GetParameter(1), strategy.GetParameter(2));

                    case WBSStrategyName.AllChild:
                        return string.Format(" FullCode like '{0}-%' ", strategy.GetParameter(0));

                    case WBSStrategyName.WBSCodeIn:
                        return string.Format(" WBSCode in ({0}) ", strategy.GetParameter(0));
                }
                return text;
            }
            return StandardStrategyStringBuilder.BuildStrategyString(strategy);
        }

        public static string BuildTreeNodeSearchString(string wbsCode, TreeNodeSearchType searchType)
        {
            string wBSFullCode = WBSDAO.GetWBSFullCode(wbsCode);
            switch (searchType)
            {
                case TreeNodeSearchType.AllSubNodeIncludeSelf:
                    return string.Format(" wbsCode in ( select wbsCode from Task where FullCode  like '{0}%'  ) ", wBSFullCode);

                case TreeNodeSearchType.AllSubNodeNotIncludeSelf:
                    return string.Format(" wbsCode in ( select wbsCode from Task where FullCode  FullCode like '{0}%' and FullCode <> {'0'}  ) ", wBSFullCode);

                case TreeNodeSearchType.FirstChildNode:
                    return string.Format(" wbsCode in ( select wbsCode from Task where ParentCode = '{0}'  ) ", wBSFullCode);

                case TreeNodeSearchType.AllSubLeafNode:
                    return string.Format("  wbsCode in ( select wbsCode from Task c where FullCode like '{0}%' and Not Exists ( select * from Task e where e.ParentCode = c.wbsCode  ) ) ", wBSFullCode);

                case TreeNodeSearchType.AllSubNotLeafNode:
                    return string.Format("  wbsCode in ( select wbsCode from Task c where FullCode like '{0}%' and Exists ( select * from Task e where e.ParentCode = c.wbsCode  ) ) ", wBSFullCode);

                case TreeNodeSearchType.OnlySelfNode:
                    return string.Format(" wbsCode = '{0}'  ) ", wbsCode);
            }
            return "";
        }

        private string CutRepeat(string strTmp)
        {
            if (strTmp.Length < 1)
            {
                return strTmp;
            }
            string text = "";
            string text2 = "";
            foreach (string text3 in strTmp.Split(new char[] { ',' }))
            {
                if (text3.Length >= 1)
                {
                    if (strTmp.IndexOf(',') == 0)
                    {
                        strTmp = strTmp.Substring(1);
                    }
                    if (strTmp.IndexOf(',') > 0)
                    {
                        text2 = strTmp.Substring(0, strTmp.IndexOf(','));
                        strTmp = strTmp.Substring(strTmp.IndexOf(',') + 1);
                        if (strTmp.IndexOf(text2) < 0)
                        {
                            text = text + "," + text2;
                        }
                    }
                    else if (text3 == strTmp)
                    {
                        text = text + "," + text3;
                    }
                }
            }
            return text.Substring(1);
        }

        private static string GetFullCode(string strUserCode)
        {
            return GetFullCode(strUserCode, "0,1,2,4,9");
        }

        private static string GetFullCode(string strUserCode, string strTaskPersons)
        {
            string text = "";
            if (strTaskPersons == "")
            {
                strTaskPersons = "0,1,2,4,9";
            }
            string queryString = string.Format("select fullcode from task where  wbscode in ( select wbscode from taskperson where type in (" + strTaskPersons + ") and ((roletype='0' and usercode='{0}')  or (roletype='1' and usercode in (select stationcode from userrole where usercode ='{0}')))) order by len(fullcode) desc ", strUserCode);
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

        public static string[] GetHasRightFullCodeArray(string strUserCode)
        {
            DataTable hasRightFullCodeTable = GetHasRightFullCodeTable(strUserCode);
            string[] textArray = new string[hasRightFullCodeTable.Rows.Count];
            int index = -1;
            foreach (DataRow row in hasRightFullCodeTable.Rows)
            {
                index++;
                textArray[index] = row["FullCode"].ToString();
            }
            return textArray;
        }

        public static string GetHasRightFullCodes(string strUserCode)
        {
            string text = "";
            DataTable hasRightFullCodeTable = GetHasRightFullCodeTable(strUserCode);
            foreach (DataRow row in hasRightFullCodeTable.Rows)
            {
                text = text + ((text.Length < 1) ? "" : ",");
                text = text + row["FullCode"].ToString();
            }
            return text;
        }

        private static DataTable GetHasRightFullCodeTable(string strUserCode)
        {
            return GetHasRightFullCodeTable(strUserCode, "0,1,2,4,9");
        }

        public static DataTable GetHasRightFullCodeTable(string strUserCode, string strTaskPersons)
        {
            if (strTaskPersons == "")
            {
                strTaskPersons = "0,1,2,4,9";
            }
            string queryString = string.Format("select fullcode from task where  wbscode in ( select wbscode from taskperson where type in (" + strTaskPersons + ") and ((roletype='0' and usercode='{0}')  or (roletype='1' and usercode in (select stationcode from userrole where usercode ='{0}')))) order by len(fullcode) desc ", strUserCode);
            QueryAgent agent = new QueryAgent();
            DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
            DataTable table2 = new DataTable();
            table2.Columns.Add("FullCode");
            foreach (DataRow row in table.Rows)
            {
                bool flag = false;
                string text2 = row["FullCode"].ToString();
                foreach (DataRow row2 in table2.Rows)
                {
                    string text3 = row2["FullCode"].ToString();
                    if (text2.IndexOf(text3) == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    DataRow row3 = table2.NewRow();
                    row3["FullCode"] = text2;
                    table2.Rows.Add(row3);
                }
            }
            return table2;
        }

        private string GetInStr(string wbsCode)
        {
            string queryString = " select  dbo.GetAccessWBSFullCode(" + wbsCode + ") ";
            QueryAgent agent = new QueryAgent();
            string strTmp = (string) agent.ExecuteScalar(queryString);
            if (strTmp.Length > 1)
            {
                strTmp = strTmp.Replace('-', ',');
                strTmp = this.CutRepeat(strTmp);
                return ("'" + strTmp.Replace(",", "','") + "'");
            }
            return "''";
        }

        private void GetShortRight()
        {
        }
    }
}

