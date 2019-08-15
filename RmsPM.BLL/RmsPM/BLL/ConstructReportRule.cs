namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class ConstructReportRule
    {
        public static DataTable FilterPubBuild(DataTable tb)
        {
            DataTable table2;
            try
            {
                DataTable tbDst = tb.Clone();
                DataRow[] rowArray = tb.Select("PBSTypeFullName like '公建%'", "BuildingName");
                foreach (DataRow row in rowArray)
                {
                    DataRow drDst = tbDst.NewRow();
                    ConvertRule.DataRowCopy(row, drDst, tb, tbDst);
                    tbDst.Rows.Add(drDst);
                }
                table2 = tbDst;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetSystemReportDtl(string ReportCode)
        {
            DataTable table2;
            try
            {
                DataTable table = null;
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select * from SystemReportDtl where ReportCode = '{0}' order by SortNo", ReportCode);
                    table = agent.ExecSqlForDataSet(queryString).Tables[0];
                }
                finally
                {
                    agent.Dispose();
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetSystemReportDtl(string ReportCode, int ShowFlag)
        {
            DataTable table2;
            try
            {
                DataTable table = null;
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select * from SystemReportDtl where ReportCode = '{0}' and isnull(ShowFlag, 0) = {1} order by SortNo", ReportCode, ShowFlag);
                    table = agent.ExecSqlForDataSet(queryString).Tables[0];
                }
                finally
                {
                    agent.Dispose();
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetSystemReportDtlString(DataTable tb)
        {
            string text2;
            try
            {
                string text = "";
                int num = -1;
                foreach (DataRow row in tb.Rows)
                {
                    num++;
                    if (num > 0)
                    {
                        text = text + ";";
                    }
                    text = text + ConvertRule.ToString(row["FieldName"]) + "=" + ConvertRule.ToString(row["FieldDispName"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void MakeConstructProgressApplyReport(string year, DataTable tb)
        {
            try
            {
                int month;
                for (month = 1; month <= 12; month++)
                {
                    tb.Columns.Add("VGMonth" + month.ToString());
                }
                int num2 = ConvertRule.ToInt(year);
                foreach (DataRow row in tb.Rows)
                {
                    string text = ConvertRule.ToString(row["BuildingCode"]);
                    EntityData data = ConstructDAO.GetV_ConstructProgressByPBSUnit(ConvertRule.ToString(row["PBSUnitCode"]));
                    for (month = 1; month <= 12; month++)
                    {
                        string text3 = "";
                        DateTime time = new DateTime(num2, month, 1);
                        string text4 = time.ToString("yyyy-MM-dd");
                        string text5 = time.AddMonths(1).ToString("yyyy-MM-dd");
                        DataView view = new DataView(data.CurrentTable, "ReportDate >= '" + text4 + "' and ReportDate < '" + text5 + "'", "ReportDate desc", DataViewRowState.CurrentRows);
                        if (view.Count > 0)
                        {
                            DataRow row2 = view[0].Row;
                            int num3 = ConvertRule.ToInt(row2["ProgressType"]);
                            text3 = ConvertRule.ToString(row2["VisualProgressName"]);
                            if (num3 == 1)
                            {
                                text3 = text3 + ConvertRule.ToInt(row2["CurrentLayer"]).ToString();
                            }
                        }
                        row["VGMonth" + month.ToString()] = text3;
                    }
                    data.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable RepRoomIn(string ProjectCode, int JGYear, string BeginDate, string EndDate)
        {
            DataTable table3;
            try
            {
                DataTable table;
                string format = "";
                if (BeginDate != "")
                {
                    format = format + " and {0} >= convert(DateTime, '" + BeginDate + "', 121)";
                }
                if (EndDate != "")
                {
                    format = format + " and {0} < convert(DateTime, '" + EndDate + "', 121) + 1";
                }
                string text2 = "select sum(isnull(r.BuildArea, 0))  from TempRoomOut a     , TempRoomStructure b       left join Room r on r.RoomCode = b.TempRoomCode where a.OutListCode = b.OutListCode   and a.CheckState = 1   and a.ProjectCode = '{0}'   and b.TempBuildingCode = '{1}'   and a.out_state = '{2}'";
                string text3 = "select sum(isnull(r.YuboArea, 0))  from TempRoomOut a     , TempRoomStructure b       left join Building r on r.BuildingCode = b.TempBuildingCode where a.OutListCode = b.OutListCode   and a.CheckState = 1   and a.ProjectCode = '{0}'   and b.TempBuildingCode = '{1}'   and a.out_state = '{2}'";
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select a.BuildingCode, a.ProjectCode, a.BuildingName, p.ProjectName, a.Remark, a.YuBoArea, t.PBSTypeName, t.PBSTypeFullName from Building a left join V_PBSType t on t.PBSTypeCode = a.PBSTypeCode, Project p, PBSUnit u where a.ProjectCode = p.ProjectCode and a.PBSUnitCode = u.PBSUnitCode and a.IsArea = 2";
                    if (ProjectCode != "")
                    {
                        string text5 = StrategyConvert.BuildInStr(ProjectCode);
                        queryString = queryString + " and a.ProjectCode in (" + text5 + ")";
                    }
                    string visualProgressJgInStr = PBSRule.GetVisualProgressJgInStr();
                    queryString = queryString + " and u.VisualProgress in (" + visualProgressJgInStr + ")";
                    if (JGYear > 0)
                    {
                        queryString = queryString + " and convert(varchar(4), u.EndDate, 112) = '" + JGYear.ToString() + "'";
                    }
                    queryString = queryString + " order by p.ProjectName, a.BuildingName";
                    table = agent.ExecSqlForDataSet(queryString).Tables[0];
                    table.Columns.Add("ChamberName", typeof(string));
                    table.Columns.Add("state", typeof(string));
                    table.Columns.Add("BeforeInvArea", typeof(decimal));
                    table.Columns.Add("InArea", typeof(decimal));
                    table.Columns.Add("OutArea", typeof(decimal));
                    table.Columns.Add("BackInArea", typeof(decimal));
                    table.Columns.Add("InvArea", typeof(decimal));
                    foreach (DataRow row in table.Rows)
                    {
                        string text7 = ConvertRule.ToString(row["BuildingCode"]);
                        string text8 = ConvertRule.ToString(row["ProjectCode"]);
                        string text9 = "";
                        string text10 = "";
                        queryString = "select ChamberName from Chamber where BuildingCode = '" + text7 + "' order by ChamberName";
                        DataTable table2 = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row2 in table2.Rows)
                        {
                            if (text9.Length > 0)
                            {
                                text9 = text9 + ",";
                            }
                            text9 = text9 + ConvertRule.ToString(row2["ChamberName"]);
                        }
                        row["ChamberName"] = text9;
                        if (text9.Length > 0)
                        {
                            decimal num = 0M;
                            decimal num2 = 0M;
                            decimal num3 = 0M;
                            if (BeginDate != "")
                            {
                                queryString = string.Format(text2, text8, text7, "入库") + " and a.Out_Date < convert(DateTime, '" + BeginDate + "', 121)";
                                num = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                                queryString = string.Format(text2, text8, text7, "出库") + " and a.Out_Date < convert(DateTime, '" + BeginDate + "', 121)";
                                num2 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                                queryString = string.Format(text2, text8, text7, "退库") + " and a.Out_Date < convert(DateTime, '" + BeginDate + "', 121)";
                                num3 = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            }
                            row["BeforeInvArea"] = (num + num3) - num2;
                            queryString = string.Format(text2, text8, text7, "入库") + string.Format(format, "a.Out_Date");
                            row["InArea"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            queryString = string.Format(text3, text8, text7, "预拨") + string.Format(format, "a.Out_Date");
                            row["YuboArea"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            queryString = string.Format(text2, text8, text7, "出库") + string.Format(format, "a.Out_Date");
                            row["OutArea"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            queryString = string.Format(text2, text8, text7, "退库") + string.Format(format, "a.Out_Date");
                            row["BackInArea"] = ConvertRule.ToDecimal(agent.ExecuteScalar(queryString));
                            row["InvArea"] = ((ConvertRule.ToDecimal(row["BeforeInvArea"]) + ConvertRule.ToDecimal(row["InArea"])) + ConvertRule.ToDecimal(row["BackInArea"])) - ConvertRule.ToDecimal(row["OutArea"]);
                            if (EndDate == "")
                            {
                                queryString = "select top 1 1 from room where BuildingCode = '" + text7 + "' and isnull(InvState, '') not in ('入库', '出库', '退库')";
                            }
                            else
                            {
                                queryString = "select top 1 1 from room r where BuildingCode = '" + text7 + "' and not exists( select a.*   from TempRoomOut a      , TempRoomStructure b  where a.OutListCode = b.OutListCode    and b.TempRoomCode = r.RoomCode    and a.CheckState = 1    and a.out_state in ('入库', '出库', '退库')    and a.Out_Date < convert(DateTime, '" + EndDate + "', 121) + 1)";
                            }
                            if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) == "")
                            {
                                text10 = "入库";
                            }
                            else
                            {
                                text10 = "未入库";
                            }
                        }
                        row["state"] = text10;
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                table3 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static void SplitPubBuildingName(DataTable tb)
        {
            try
            {
                tb.Columns.Add("BuildingNameH", typeof(string));
                tb.Columns.Add("BuildingNameP", typeof(string));
                foreach (DataRow row in tb.Rows)
                {
                    row["BuildingNameH"] = row["BuildingName"];
                    row["BuildingNameP"] = row["BuildingName"];
                }
                DataRow[] rowArray = tb.Select("PBSTypeFullName like '住宅%'", "BuildingName");
                foreach (DataRow row in rowArray)
                {
                    string text = ConvertRule.ToString(row["BuildingName"]);
                    DataRow[] rowArray2 = tb.Select(string.Format("BuildingName like '{0}%' and PBSTypeFullName not like '住宅%'", text));
                    foreach (DataRow row2 in rowArray2)
                    {
                        string text2 = ConvertRule.ToString(row2["BuildingName"]);
                        string text3 = text2.Substring(text.Length, text2.Length - text.Length);
                        row2["BuildingNameH"] = text;
                        row2["BuildingNameP"] = text3;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable UnionHouse(DataTable tb)
        {
            DataTable table2;
            try
            {
                DataTable tbDst = tb.Clone();
                DataRow[] rowArray = tb.Select("PBSTypeFullName like '住宅%'", "BuildingName");
                string[] columnNames = new string[] { 
                    "TotalInvest", "TotalCompleteInvest", "PCurrentYearInvest", "BeforInvestAccout", "PTotalConstructSize", "PHouseConstructSize", "PToBuildConstructSize", "POtherConstructSize", "PTotalCompleteSize", "PHouseCompleteSize", "PToBuildCompleteSize", "POtherCompleteSize", "HouseArea", "HouseSize", "ToBuildSize", "OtherSize", 
                    "RoomArea", "HouseRoomSize", "ToBuildRoomSize", "OtherRoomSize"
                 };
                foreach (DataRow row in rowArray)
                {
                    DataRow drDst = tbDst.NewRow();
                    ConvertRule.DataRowCopy(row, drDst, tb, tbDst);
                    string text = ConvertRule.ToString(drDst["BuildingName"]);
                    DataRow[] rowArray2 = tb.Select(string.Format("BuildingName like '{0}%' and PBSTypeFullName not like '住宅%'", text));
                    foreach (DataRow row3 in rowArray2)
                    {
                        ConvertRule.DataRowAddDecimal(drDst, row3, columnNames);
                    }
                    tbDst.Rows.Add(drDst);
                }
                table2 = tbDst;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }
    }
}

