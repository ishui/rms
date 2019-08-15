namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class ConstructProgRule
    {
        public static decimal CalcTaskPercentByConstructProg(string BuildingCode, string WBSCode)
        {
            decimal num4;
            try
            {
                decimal d = 0M;
                EntityData buildingFloorByBuildingCode = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                if (buildingFloorByBuildingCode.HasRecord())
                {
                    int count = buildingFloorByBuildingCode.CurrentTable.Rows.Count;
                    foreach (DataRow row in buildingFloorByBuildingCode.CurrentTable.Rows)
                    {
                        string buildingFloorCode = row["BuildingFloorCode"].ToString();
                        int @int = 0;
                        EntityData buildingFloorProgressByBuildingFloorWBSCode = ProductDAO.GetBuildingFloorProgressByBuildingFloorWBSCode(buildingFloorCode, WBSCode);
                        if (buildingFloorProgressByBuildingFloorWBSCode.HasRecord())
                        {
                            @int = buildingFloorProgressByBuildingFloorWBSCode.GetInt("CompletePercent");
                            d += Math.Round((decimal) (@int / count), 6);
                        }
                        buildingFloorProgressByBuildingFloorWBSCode.Dispose();
                    }
                }
                buildingFloorByBuildingCode.Dispose();
                d = Math.Round(d);
                if (d > 100M)
                {
                    d = 100M;
                }
                num4 = d;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num4;
        }

        public static string CanDeleteBuildingFloorByBuilding(string BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if (BuildingCode == "")
                {
                    return text;
                }
                EntityData buildingFloorProgressByBuildingCode = ProductDAO.GetBuildingFloorProgressByBuildingCode(BuildingCode);
                if (buildingFloorProgressByBuildingCode.HasRecord())
                {
                    text = "该楼栋已有工程进度，不能删除结构";
                }
                buildingFloorProgressByBuildingCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string CanDeleteBuildingFloorByCode(string BuildingFloorCode)
        {
            string text2;
            try
            {
                string text = "";
                if (BuildingFloorCode == "")
                {
                    return text;
                }
                EntityData buildingFloorProgressByCode = ProductDAO.GetBuildingFloorProgressByCode(BuildingFloorCode);
                if (buildingFloorProgressByCode.HasRecord())
                {
                    text = "该楼层已有工程进度，不能删除";
                }
                buildingFloorProgressByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void CollapseProjectProgressChartDataTable(string WBSCode, DataSet ds)
        {
            try
            {
                string wBSFullCode = WBSRule.GetWBSFullCode(WBSCode);
                if (wBSFullCode != "")
                {
                    DataTable table = ds.Tables["Task"];
                    for (int i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = table.Rows[i];
                        if (ConvertRule.ToString(row["FullCode"]).StartsWith(wBSFullCode + "-"))
                        {
                            table.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingFloor(string BuildingCode)
        {
            if (BuildingCode != "")
            {
                try
                {
                    string message = CanDeleteBuildingFloorByBuilding(BuildingCode);
                    if (message != "")
                    {
                        throw new Exception(message);
                    }
                    EntityData entity = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                    if (entity.HasRecord())
                    {
                        ProductDAO.DeleteBuildingFloor(entity);
                    }
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static void DeleteBuildingFloorProgress(string BuildingCode, string VisualProgressCode)
        {
            if (BuildingCode != "")
            {
                try
                {
                    EntityData entity = ProductDAO.GetBuildingFloorProgressByBuildingCodeVisualProgress(BuildingCode, VisualProgressCode);
                    if (entity.HasRecord())
                    {
                        ProductDAO.DeleteBuildingFloorProgress(entity);
                    }
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static void DeleteGroundWork(string GroundWorkCode)
        {
            if (GroundWorkCode != "")
            {
                try
                {
                    EntityData entity = ConstructDAO.GetGroundWorkByCode(GroundWorkCode);
                    if (entity.HasRecord())
                    {
                        ConstructDAO.DeleteGroundWork(entity);
                    }
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static void ExpandProjectProgressChartDataTable(string WBSCode, DataSet ds)
        {
            try
            {
                DataTable tb = GenerateProjectProgressChartTable(WBSCode);
                DateTime now = DateTime.Now;
                string startField = "ActualStartDate";
                string endField = "ActualFinishDate";
                string text3 = "PlannedStartDate";
                string text4 = "PlannedFinishDate";
                string tempEndField = "TempEndDate";
                string text6 = "TempEndDateB";
                FormatProjectProgressDate(tb, startField, endField);
                FormatProjectProgressEndDate(tb, startField, endField, tempEndField);
                FormatProjectProgressEndDateB(tb, text6);
                tb.Columns.Add(new DataColumn("TaskHint", typeof(string)));
                foreach (DataRow row in tb.Rows)
                {
                    string text7 = "工作名称：" + row["TaskName"].ToString() + "<br>状　　态：" + ComSource.GetTaskStatusName(ConvertRule.ToInt(row["Status"]).ToString()) + "<br>当前进度：" + ConvertRule.ToInt(row["CompletePercent"]).ToString() + "%<br>计划起止：" + ConvertRule.ToDateString(row[text3], "yyyy-MM-dd") + "..." + ConvertRule.ToDateString(row[text4], "yyyy-MM-dd") + "<br>实际起止：" + ConvertRule.ToDateString(row[startField], "yyyy-MM-dd") + "..." + ConvertRule.ToDateString(row[endField], "yyyy-MM-dd");
                    row["TaskHint"] = text7;
                }
                DataTable tbDst = ds.Tables["Task"];
                int num = -1;
                bool flag = false;
                foreach (DataRow row2 in tbDst.Rows)
                {
                    num++;
                    if (row2["WBSCode"].ToString() == WBSCode)
                    {
                        flag = true;
                        break;
                    }
                }
                for (int i = tb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drSrc = tb.Rows[i];
                    if (tbDst.Select("WBSCode='" + drSrc["WBSCode"].ToString() + "'").Length == 0)
                    {
                        DataRow drDst = tbDst.NewRow();
                        ConvertRule.DataRowCopy(drSrc, drDst, tb, tbDst);
                        if (flag)
                        {
                            tbDst.Rows.InsertAt(drDst, num + 1);
                        }
                        else
                        {
                            tbDst.Rows.Add(drDst);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FormatProjectProgressDate(DataTable tb, string StartField, string EndField)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    if ((row[StartField] == DBNull.Value) && (row[EndField] != DBNull.Value))
                    {
                        row[StartField] = row[EndField];
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FormatProjectProgressEndDate(DataTable tb, string StartField, string EndField, string TempEndField)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    row[TempEndField] = row[EndField];
                    if (((row[StartField] != DBNull.Value) && (row[EndField] == DBNull.Value)) && (((DateTime) row[StartField]) <= DateTime.Today))
                    {
                        row[TempEndField] = DateTime.Today;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FormatProjectProgressEndDateB(DataTable tb, string TempEndField)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    row[TempEndField] = row["ActualFinishDate"];
                    if ((row[TempEndField] == DBNull.Value) && (row["ActualStartDate"] != DBNull.Value))
                    {
                        row[TempEndField] = row["EarlyFinishDate"];
                        if ((row[TempEndField] == DBNull.Value) && ((row["PlannedStartDate"] != DBNull.Value) && (row["PlannedFinishDate"] != DBNull.Value)))
                        {
                            TimeSpan span = (TimeSpan) (((DateTime) row["PlannedFinishDate"]) - ((DateTime) row["PlannedStartDate"]));
                            if (span.Days >= 0)
                            {
                                row[TempEndField] = ((DateTime) row["ActualStartDate"]).AddDays((double) span.Days);
                            }
                        }
                        if ((row[TempEndField] == DBNull.Value) && (((DateTime) row["ActualStartDate"]) <= DateTime.Today))
                        {
                            row[TempEndField] = DateTime.Today;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GenerateBuildingFloorProgressChartTable(string BuildingCode, string VisualProgressCode, DataView dvTask)
        {
            DataTable table3;
            try
            {
                EntityData buildingFloorByBuildingCode = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                buildingFloorByBuildingCode.Dispose();
                DataTable currentTable = buildingFloorByBuildingCode.CurrentTable;
                currentTable.Columns.Add("Html", typeof(string));
                int dayOfWeek = ConvertRule.GetDayOfWeek(DateTime.Today.DayOfWeek);
                DateTime time = DateTime.Today.AddDays((double) (1 - dayOfWeek));
                DateTime time2 = DateTime.Today.AddDays((double) (7 - dayOfWeek));
                foreach (DataRow row in currentTable.Rows)
                {
                    string buildingFloorCode = ConvertRule.ToString(row["BuildingFloorCode"]);
                    string floorName = ConvertRule.ToString(row["FloorName"]);
                    string text3 = "";
                    EntityData data2 = ProductDAO.GetV_BuildingFloorProgressByBuildingFloorCode(buildingFloorCode);
                    data2.Dispose();
                    DataTable table2 = data2.CurrentTable;
                    foreach (DataRowView view in dvTask)
                    {
                        string text4 = ConvertRule.ToString(view["WBSCode"]);
                        string taskName = ConvertRule.ToString(view["TaskName"]);
                        int status = 0;
                        int statusEx = 0;
                        DataRow drProg = null;
                        DataRow[] rowArray = table2.Select("WBSCode='" + text4 + "'");
                        if (rowArray.Length > 0)
                        {
                            drProg = rowArray[0];
                            status = ConvertRule.ToInt(drProg["Status"]);
                            if ((status == 1) && (drProg["PEndDate"] != DBNull.Value))
                            {
                                DateTime time3 = (DateTime) drProg["PEndDate"];
                                if (time3 <= time2)
                                {
                                    statusEx = 1;
                                }
                            }
                        }
                        string text6 = GetBuildingFloorProgressHintHtml(drProg, floorName, taskName);
                        string progressHtmlByStatus = GetProgressHtmlByStatus(status, statusEx);
                        text3 = text3 + "<td class=\"status\" hint=\"" + text6 + "\" onMouseOver=\"init(myjoybox, joyboxTable, linktitle, hint);\" onMouseOut=\"mouseend();\" onclick=\"Modify('" + buildingFloorCode + "', '" + text4 + "', '" + VisualProgressCode + "')\">" + progressHtmlByStatus + "</td>";
                    }
                    row["Html"] = text3;
                }
                table3 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static DataTable GenerateBuildingFloorProgressChartTableB(string BuildingCode, string VisualProgressCode, ref DataTable tbLegend)
        {
            DataTable table2;
            try
            {
                tbLegend = new DataTable();
                tbLegend.Columns.Add("WBSCode");
                tbLegend.Columns.Add("TaskName");
                tbLegend.Columns.Add("ImageFileName");
                tbLegend.Columns.Add("hint");
                EntityData buildingFloorByBuildingCode = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                buildingFloorByBuildingCode.Dispose();
                DataTable currentTable = buildingFloorByBuildingCode.CurrentTable;
                currentTable.Columns.Add("WBSCode", typeof(string));
                currentTable.Columns.Add("TaskName", typeof(string));
                currentTable.Columns.Add("ImageFileName", typeof(string));
                currentTable.Columns.Add("hint", typeof(string));
                currentTable.Columns.Add("Html", typeof(string));
                foreach (DataRow row in currentTable.Rows)
                {
                    string buildingFloorCode = ConvertRule.ToString(row["BuildingFloorCode"]);
                    string floorName = ConvertRule.ToString(row["FloorName"]);
                    string wBSCode = "";
                    string taskName = "";
                    string text5 = "";
                    string text6 = "";
                    string text7 = "";
                    DataRow drProg = null;
                    wBSCode = GetCurrentBuildingFloorProgressTask(buildingFloorCode, VisualProgressCode);
                    if (wBSCode != "")
                    {
                        EntityData data2 = ProductDAO.GetV_BuildingFloorProgressByBuildingFloorWBSCode(buildingFloorCode, wBSCode);
                        data2.Dispose();
                        if (data2.HasRecord())
                        {
                            drProg = data2.CurrentRow;
                        }
                        EntityData taskByCode = WBSDAO.GetTaskByCode(wBSCode);
                        DataRow drTask = null;
                        if (taskByCode.HasRecord())
                        {
                            drTask = taskByCode.CurrentRow;
                            taskName = taskByCode.GetString("TaskName");
                            text5 = taskByCode.GetString("ImageFileName");
                        }
                        taskByCode.Dispose();
                        if (text5 == "")
                        {
                            text7 = taskName;
                        }
                        else
                        {
                            text7 = string.Format("<img src='../images/{0}'>", text5);
                        }
                        if (text5 != "")
                        {
                            DataRow row4;
                            DataRow[] rowArray = tbLegend.Select("WBSCode='" + wBSCode + "'");
                            if (rowArray.Length > 0)
                            {
                                row4 = rowArray[0];
                            }
                            else
                            {
                                row4 = tbLegend.NewRow();
                                row4["WBSCode"] = wBSCode;
                                row4["TaskName"] = taskName;
                                row4["ImageFileName"] = text5;
                                row4["hint"] = GetTaskHintHtml(drTask);
                                tbLegend.Rows.Add(row4);
                            }
                        }
                        text6 = GetBuildingFloorProgressHintHtml(drProg, floorName, taskName);
                    }
                    row["WBSCode"] = wBSCode;
                    row["TaskName"] = taskName;
                    row["ImageFileName"] = text5;
                    row["Html"] = text7;
                    row["hint"] = text6;
                }
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GenerateProjectProgressChartTable(string WBSCode)
        {
            DataTable table2;
            try
            {
                EntityData childTask = WBSDAO.GetChildTask(WBSCode);
                childTask.Dispose();
                DataTable currentTable = childTask.CurrentTable;
                currentTable.Columns.Add(new DataColumn("TempEndDate", typeof(DateTime)));
                currentTable.Columns.Add(new DataColumn("TempEndDateB", typeof(DateTime)));
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static int GetBuildingFloorIndex(object BuildingFloorCode)
        {
            int num2;
            try
            {
                int @int = 0;
                if ((BuildingFloorCode != null) && (BuildingFloorCode.ToString() != ""))
                {
                    EntityData buildingFloorByCode = ProductDAO.GetBuildingFloorByCode(BuildingFloorCode.ToString());
                    if (buildingFloorByCode.HasRecord())
                    {
                        @int = buildingFloorByCode.GetInt("FloorIndex");
                    }
                    buildingFloorByCode.Dispose();
                }
                num2 = @int;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetBuildingFloorName(object BuildingFloorCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingFloorCode != null) && (BuildingFloorCode.ToString() != ""))
                {
                    EntityData buildingFloorByCode = ProductDAO.GetBuildingFloorByCode(BuildingFloorCode.ToString());
                    if (buildingFloorByCode.HasRecord())
                    {
                        text = buildingFloorByCode.GetString("FloorName");
                    }
                    buildingFloorByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingFloorProgressHintHtml(DataRow drProg, string FloorName, string TaskName)
        {
            string text8;
            try
            {
                string text2 = "";
                string text3 = "";
                string text4 = "";
                string text5 = "";
                int num = 0;
                string text6 = "";
                string text7 = "0";
                if (drProg != null)
                {
                    num = ConvertRule.ToInt(drProg["Status"]);
                    text6 = ConvertRule.ToString(drProg["StatusName"]);
                    text7 = ConvertRule.ToInt(drProg["CompletePercent"]).ToString();
                    text2 = ConvertRule.ToDateString(drProg["PStartDate"], "yyyy-MM-dd");
                    text3 = ConvertRule.ToDateString(drProg["PEndDate"], "yyyy-MM-dd");
                    text4 = ConvertRule.ToDateString(drProg["StartDate"], "yyyy-MM-dd");
                    text5 = ConvertRule.ToDateString(drProg["EndDate"], "yyyy-MM-dd");
                }
                if (text6 == "")
                {
                    text6 = "未开始";
                }
                text8 = "楼　　层：" + FloorName + "<br>工作名称：" + TaskName + "<br>状　　态：" + text6 + "<br>完成百分比：" + text7 + "%<br>计划开始日期：" + text2 + "<br>计划结束日期：" + text3 + "<br>实际开始日期：" + text4 + "<br>实际结束日期：" + text5;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text8;
        }

        public static DataTable GetBuildingTaskVisualProgress(string[] arrBuildingCode, string ProjectCode)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("TaskName");
                table.Columns.Add("WBSCode");
                foreach (string text in arrBuildingCode)
                {
                    EntityData buildingTaskVisualProgress = GetBuildingTaskVisualProgress(text, ProjectCode);
                    foreach (DataRow row in buildingTaskVisualProgress.CurrentTable.Rows)
                    {
                        string text2 = ConvertRule.ToString(row["TaskName"]);
                        if (table.Select("TaskName='" + text2 + "'").Length == 0)
                        {
                            DataRow row2 = table.NewRow();
                            row2["TaskName"] = text2;
                            row2["WBSCode"] = "-1";
                            table.Rows.Add(row2);
                        }
                    }
                    buildingTaskVisualProgress.Dispose();
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static EntityData GetBuildingTaskVisualProgress(string BuildingCode, string ProjectCode)
        {
            EntityData data4;
            try
            {
                EntityData data = new EntityData("Task");
                DataTable tbDst = data.CurrentTable;
                data.Dispose();
                EntityData data2 = WBSDAO.GetTaskByRelaCode("B", BuildingCode, ProjectCode);
                foreach (DataRow row in data2.CurrentTable.Rows)
                {
                    EntityData childTask = WBSDAO.GetChildTask(ConvertRule.ToString(row["WBSCode"]));
                    foreach (DataRow row2 in childTask.CurrentTable.Rows)
                    {
                        DataRow drDst = tbDst.NewRow();
                        ConvertRule.DataRowCopy(row2, drDst, childTask.CurrentTable, tbDst);
                        tbDst.Rows.Add(drDst);
                    }
                    childTask.Dispose();
                }
                data2.Dispose();
                data4 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data4;
        }

        public static DataTable GetBuildingTaskVisualProgressChild(string VisualProgress)
        {
            DataTable table2;
            try
            {
                EntityData childTask = WBSDAO.GetChildTask(VisualProgress);
                childTask.Dispose();
                if (!childTask.HasRecord())
                {
                    childTask = WBSDAO.GetTaskByCode(VisualProgress);
                    childTask.CurrentTable.Columns.Add("ChildNodesCount", typeof(int));
                    childTask.CurrentTable.Columns.Add("Exceed", typeof(int));
                }
                childTask.Dispose();
                DataTable tbDst = childTask.CurrentTable;
                tbDst.Columns.Add("TaskNameHtml", typeof(string));
                tbDst.Columns.Add("TaskHintHtml", typeof(string));
                tbDst.Columns.Add("TempLevel", typeof(int));
                tbDst.Columns.Add("IsLeaf", typeof(int));
                tbDst.Columns.Add("ColSpan", typeof(int));
                tbDst.Columns.Add("RowSpan", typeof(int));
                for (int i = tbDst.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drTask = tbDst.Rows[i];
                    string wBSCode = ConvertRule.ToString(drTask["WBSCode"]);
                    string text2 = ConvertRule.ToString(drTask["TaskName"]);
                    int num3 = ConvertRule.ToInt(drTask["ChildNodesCount"]);
                    if (num3 > 0)
                    {
                        EntityData data2 = WBSDAO.GetChildTask(wBSCode);
                        data2.Dispose();
                        foreach (DataRow row2 in data2.CurrentTable.Rows)
                        {
                            DataRow drDst = tbDst.NewRow();
                            ConvertRule.DataRowCopy(row2, drDst, data2.CurrentTable, tbDst);
                            drDst["TaskNameHtml"] = text2 + "<br>" + ConvertRule.ToString(drDst["TaskName"]);
                            drDst["TempLevel"] = 2;
                            drDst["IsLeaf"] = 1;
                            drDst["ColSpan"] = 1;
                            drDst["RowSpan"] = 1;
                            drDst["TaskHintHtml"] = GetTaskHintHtml(drDst);
                            tbDst.Rows.InsertAt(drDst, i);
                        }
                        drTask["TempLevel"] = 1;
                        drTask["IsLeaf"] = 0;
                        drTask["ColSpan"] = num3;
                        drTask["RowSpan"] = 1;
                        drTask["TaskHintHtml"] = GetTaskHintHtml(drTask);
                    }
                    else
                    {
                        drTask["TaskNameHtml"] = drTask["TaskName"];
                        drTask["TempLevel"] = 1;
                        drTask["IsLeaf"] = 1;
                        drTask["ColSpan"] = 1;
                        drTask["RowSpan"] = 2;
                        drTask["TaskHintHtml"] = GetTaskHintHtml(drTask);
                    }
                }
                table2 = tbDst;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetCurrentBuildingFloorProgressTask(string BuildingFloorCode, string VisualProgressCode)
        {
            string text4;
            try
            {
                string text = "";
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingFloorCode, BuildingFloorCode));
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.VisualProgressCode, VisualProgressCode));
                ArrayList ar = new ArrayList();
                ar.Add("1");
                ar.Add("2");
                string arrayLinkString = ConvertRule.GetArrayLinkString(ar);
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.Status, arrayLinkString));
                builder.AddOrder("TaskSortID", false);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    return data.GetString("WBSCode");
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static int GetGanttChartHeight(int RowCount, int ChartRowHeight, int ChartTopHeight, int ChartBottomHeight)
        {
            int num3;
            try
            {
                int num2 = (GetGanttDataHeight(RowCount, ChartRowHeight) + ChartTopHeight) + ChartBottomHeight;
                num3 = num2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num3;
        }

        public static int GetGanttDataHeight(int RowCount, int ChartRowHeight)
        {
            int num3;
            try
            {
                int num = ChartRowHeight;
                if (num <= 0)
                {
                    num = 0x19;
                }
                int num2 = num * RowCount;
                num3 = num2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num3;
        }

        public static string GetGroundWorkChartColorByState(int state)
        {
            string text2;
            try
            {
                string text = "#ffffff";
                switch (state)
                {
                    case 0:
                        text = "#ffffff";
                        break;

                    case 1:
                        text = "#99CCFF";
                        break;

                    case 2:
                        text = "#FD7C7C";
                        break;

                    case 3:
                        text = "#FFCC66";
                        break;

                    case 4:
                        text = "#FF9900";
                        break;

                    case 5:
                        text = "#66CC00";
                        break;

                    case 6:
                        text = "#3C7C00";
                        break;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetGroundWorkChartLegend()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("State", typeof(int));
                table.Columns.Add("StateName");
                table.Columns.Add("Color");
                table.Columns.Add("Count", typeof(int));
                string[] textArray = new string[] { "未开始", "第一阶段(完成20%以下)", "第二阶段(完成20%以上)", "第三阶段(完成40%以上)", "第四阶段(完成60%以上)", "第五阶段(完成80%以上)", "已完成" };
                DataRow row = table.NewRow();
                row["State"] = 0;
                row["StateName"] = textArray[0];
                row["Color"] = GetGroundWorkChartColorByState(0);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 1;
                row["StateName"] = textArray[1];
                row["Color"] = GetGroundWorkChartColorByState(1);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 2;
                row["StateName"] = textArray[2];
                row["Color"] = GetGroundWorkChartColorByState(2);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 3;
                row["StateName"] = textArray[3];
                row["Color"] = GetGroundWorkChartColorByState(3);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 4;
                row["StateName"] = textArray[4];
                row["Color"] = GetGroundWorkChartColorByState(4);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 5;
                row["StateName"] = textArray[5];
                row["Color"] = GetGroundWorkChartColorByState(5);
                table.Rows.Add(row);
                row = table.NewRow();
                row["State"] = 6;
                row["StateName"] = textArray[6];
                row["Color"] = GetGroundWorkChartColorByState(6);
                table.Rows.Add(row);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static int GetGroundWorkChartState(DataRow dr)
        {
            int num4;
            try
            {
                int num = 0;
                int num2 = ConvertRule.ToInt(dr["Status"]);
                int num3 = ConvertRule.ToInt(dr["CompletePercent"]);
                switch (num2)
                {
                    case 0:
                        num = 0;
                        break;

                    case 4:
                        num = 6;
                        break;

                    default:
                        if (num3 >= 80)
                        {
                            num = 5;
                        }
                        else if (num3 >= 60)
                        {
                            num = 4;
                        }
                        else if (num3 >= 40)
                        {
                            num = 3;
                        }
                        else if (num3 >= 20)
                        {
                            num = 2;
                        }
                        else if (num3 > 0)
                        {
                            num = 1;
                        }
                        break;
                }
                num4 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num4;
        }

        public static string GetProgressHtmlByStatus(int Status, int StatusEx)
        {
            string text2;
            try
            {
                string text = "";
                switch (Status)
                {
                    case 0:
                        text = "<span class='status0'></span>";
                        goto Label_0048;

                    case 1:
                        if (StatusEx != 1)
                        {
                            break;
                        }
                        text = "<img src='../images/progress_3.gif' height='100%'>";
                        goto Label_0048;

                    case 2:
                        text = "<img src='../images/progress_2.gif' height='100%'>";
                        goto Label_0048;

                    default:
                        goto Label_0048;
                }
                text = "<img src='../images/progress_1.gif' height='100%'>";
            Label_0048:
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataSet GetProjectProgressChartDataTable(string WBSCode)
        {
            DataSet set = new DataSet();
            DataTable tb = GenerateProjectProgressChartTable(WBSCode);
            DateTime now = DateTime.Now;
            string startField = "ActualStartDate";
            string endField = "ActualFinishDate";
            string text3 = "PlannedStartDate";
            string text4 = "PlannedFinishDate";
            string tempEndField = "TempEndDate";
            string text6 = "TempEndDateB";
            FormatProjectProgressDate(tb, startField, endField);
            FormatProjectProgressEndDate(tb, startField, endField, tempEndField);
            FormatProjectProgressEndDateB(tb, text6);
            tb.Columns.Add(new DataColumn("TaskHint", typeof(string)));
            foreach (DataRow row in tb.Rows)
            {
                string text7 = "工作名称：" + row["TaskName"].ToString() + "<br>状　　态：" + ComSource.GetTaskStatusName(ConvertRule.ToInt(row["Status"]).ToString()) + "<br>当前进度：" + ConvertRule.ToInt(row["CompletePercent"]).ToString() + "%<br>计划起止：" + ConvertRule.ToDateString(row[text3], "yyyy-MM-dd") + "..." + ConvertRule.ToDateString(row[text4], "yyyy-MM-dd") + "<br>实际起止：" + ConvertRule.ToDateString(row[startField], "yyyy-MM-dd") + "..." + ConvertRule.ToDateString(row[endField], "yyyy-MM-dd");
                row["TaskHint"] = text7;
            }
            DataTable table = tb.Copy();
            set.Tables.Add(table);
            return set;
        }

        public static DataTable GetProjectProgressGanttByTask(DataTable tbTask)
        {
            DataTable table2;
            try
            {
                string text = "ActualStartDate";
                string text2 = "PlannedStartDate";
                string text3 = "PlannedFinishDate";
                string text4 = "TempEndDate";
                DataTable table = new DataTable("Gantt");
                table.Columns.AddRange(new DataColumn[] { new DataColumn("Series", typeof(string)), new DataColumn("Task", typeof(string)), new DataColumn("Start", typeof(DateTime)), new DataColumn("End", typeof(DateTime)), new DataColumn("ID", typeof(int)), new DataColumn("LinkTo", typeof(int)), new DataColumn("PercentComplete", typeof(double)), new DataColumn("Owner", typeof(string)), new DataColumn("Row", typeof(int)), new DataColumn("Column", typeof(int)), new DataColumn("WBSCode", typeof(string)), new DataColumn("Status", typeof(int)), new DataColumn("ParentCode", typeof(string)), new DataColumn("Deep", typeof(int)) });
                int num = 0;
                int num2 = -1;
                for (int i = tbTask.Rows.Count - 1; i >= 0; i--)
                {
                    num2++;
                    DataRow row = tbTask.Rows[i];
                    string text5 = ConvertRule.ToString(row["TaskName"]);
                    if (table.Select("Series='" + text5 + "'").Length > 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            string text6 = text5 + "(" + j.ToString() + ")";
                            if (table.Select("Series='" + text6 + "'").Length == 0)
                            {
                                text5 = text6;
                                break;
                            }
                        }
                    }
                    DataRow row2 = table.NewRow();
                    num++;
                    row2["Series"] = text5;
                    row2["Task"] = "实际";
                    row2["Start"] = row[text];
                    row2["End"] = row[text4];
                    row2["ID"] = num;
                    row2["LinkTo"] = -1;
                    row2["PercentComplete"] = row["CompletePercent"];
                    row2["owner"] = "";
                    row2["Row"] = num2;
                    row2["Column"] = num - 1;
                    row2["WBSCode"] = row["WBSCode"];
                    row2["Status"] = row["Status"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    table.Rows.Add(row2);
                    row2 = table.NewRow();
                    num++;
                    row2["Series"] = text5;
                    row2["Task"] = "计划";
                    row2["Start"] = row[text2];
                    row2["End"] = row[text3];
                    row2["ID"] = num;
                    row2["LinkTo"] = -1;
                    row2["PercentComplete"] = 0;
                    row2["owner"] = "";
                    row2["Row"] = num2;
                    row2["Column"] = num - 1;
                    row2["WBSCode"] = row["WBSCode"];
                    row2["Status"] = row["Status"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    table.Rows.Add(row2);
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetProjectProgressGanttByTaskA(DataTable tbTask)
        {
            DataTable table2;
            try
            {
                string text = "PlannedStartDate";
                string text2 = "PlannedFinishDate";
                DataTable table = new DataTable("Gantt");
                table.Columns.AddRange(new DataColumn[] { new DataColumn("Series", typeof(string)), new DataColumn("Task", typeof(string)), new DataColumn("Start", typeof(DateTime)), new DataColumn("End", typeof(DateTime)), new DataColumn("ID", typeof(int)), new DataColumn("LinkTo", typeof(int)), new DataColumn("PercentComplete", typeof(double)), new DataColumn("Owner", typeof(string)), new DataColumn("Row", typeof(int)), new DataColumn("Column", typeof(int)), new DataColumn("WBSCode", typeof(string)), new DataColumn("Status", typeof(int)), new DataColumn("ParentCode", typeof(string)), new DataColumn("Deep", typeof(int)) });
                int num = 0;
                int num2 = -1;
                for (int i = tbTask.Rows.Count - 1; i >= 0; i--)
                {
                    num2++;
                    DataRow row = tbTask.Rows[i];
                    string text3 = ConvertRule.ToString(row["TaskName"]);
                    if (table.Select("Series='" + text3 + "'").Length > 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            string text4 = text3 + "(" + j.ToString() + ")";
                            if (table.Select("Series='" + text4 + "'").Length == 0)
                            {
                                text3 = text4;
                                break;
                            }
                        }
                    }
                    num++;
                    DataRow row2 = table.NewRow();
                    num++;
                    row2["Series"] = text3;
                    row2["Task"] = "";
                    row2["Start"] = row[text];
                    row2["End"] = row[text2];
                    row2["ID"] = num;
                    row2["LinkTo"] = -1;
                    row2["PercentComplete"] = row["CompletePercent"];
                    row2["owner"] = "";
                    row2["Row"] = num2;
                    row2["Column"] = num - 1;
                    row2["WBSCode"] = row["WBSCode"];
                    row2["Status"] = row["Status"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    table.Rows.Add(row2);
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetProjectProgressGanttByTaskB(DataTable tbTask)
        {
            DataTable table2;
            try
            {
                string text = "ActualStartDate";
                string text3 = "PlannedStartDate";
                string text4 = "PlannedFinishDate";
                string text5 = "TempEndDateB";
                DataTable table = new DataTable("Gantt");
                table.Columns.AddRange(new DataColumn[] { new DataColumn("Series", typeof(string)), new DataColumn("Task", typeof(string)), new DataColumn("Start", typeof(DateTime)), new DataColumn("End", typeof(DateTime)), new DataColumn("ID", typeof(int)), new DataColumn("LinkTo", typeof(int)), new DataColumn("PercentComplete", typeof(double)), new DataColumn("Owner", typeof(string)), new DataColumn("Row", typeof(int)), new DataColumn("Column", typeof(int)), new DataColumn("WBSCode", typeof(string)), new DataColumn("Status", typeof(int)), new DataColumn("ParentCode", typeof(string)), new DataColumn("Deep", typeof(int)) });
                int num = 0;
                int num2 = -1;
                for (int i = tbTask.Rows.Count - 1; i >= 0; i--)
                {
                    num2++;
                    DataRow row = tbTask.Rows[i];
                    string text6 = ConvertRule.ToString(row["TaskName"]);
                    if (table.Select("Series='" + text6 + "'").Length > 0)
                    {
                        for (int j = 1; j <= 100; j++)
                        {
                            string text7 = text6 + "(" + j.ToString() + ")";
                            if (table.Select("Series='" + text7 + "'").Length == 0)
                            {
                                text6 = text7;
                                break;
                            }
                        }
                    }
                    num++;
                    DataRow row2 = table.NewRow();
                    row2["Series"] = text6;
                    row2["Task"] = "实际";
                    row2["Start"] = row[text];
                    row2["End"] = row[text5];
                    row2["ID"] = num;
                    row2["LinkTo"] = -1;
                    row2["PercentComplete"] = row["CompletePercent"];
                    row2["owner"] = "";
                    row2["Row"] = num2;
                    row2["Column"] = num - 1;
                    row2["WBSCode"] = row["WBSCode"];
                    row2["Status"] = row["Status"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    table.Rows.Add(row2);
                    row2 = table.NewRow();
                    num++;
                    row2["Series"] = text6;
                    row2["Task"] = "计划";
                    row2["Start"] = row[text3];
                    row2["End"] = row[text4];
                    row2["ID"] = num;
                    row2["LinkTo"] = -1;
                    row2["PercentComplete"] = 0;
                    row2["owner"] = "";
                    row2["Row"] = num2;
                    row2["Column"] = num - 1;
                    row2["WBSCode"] = row["WBSCode"];
                    row2["Status"] = row["Status"];
                    row2["ParentCode"] = row["ParentCode"];
                    row2["Deep"] = row["Deep"];
                    table.Rows.Add(row2);
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetProjectProgressGanttInfo(DataTable tbGantt)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("Info");
                table.Columns.Add("ChartWBSCode");
                table.Columns.Add("ChartCompletePercent");
                table.Columns.Add("ChartStatusName");
                string text = "";
                string text2 = "";
                string text3 = "";
                foreach (DataRow row in tbGantt.Rows)
                {
                    text = text + ConvertRule.ToString(row["WBSCode"]) + ";";
                    text2 = text2 + ConvertRule.ToDecimal(row["PercentComplete"]) + ";";
                    text3 = text3 + ComSource.GetTaskStatusName(ConvertRule.ToInt(row["Status"]).ToString()) + ";";
                }
                DataRow row2 = table.NewRow();
                row2["ChartWBSCode"] = text;
                row2["ChartCompletePercent"] = text2;
                row2["ChartStatusName"] = text3;
                table.Rows.Add(row2);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static EntityData GetRootTask(string ProjectCode)
        {
            EntityData data2;
            try
            {
                WBSStrategyBuilder builder = new WBSStrategyBuilder();
                builder.AddStrategy(new Strategy(WBSStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(WBSStrategyName.ParentCode, ""));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Task", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetRootTaskCode(string ProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ProjectCode == "")
                {
                    return text;
                }
                EntityData rootTask = GetRootTask(ProjectCode);
                if (rootTask.HasRecord())
                {
                    text = rootTask.GetString("WBSCode");
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTaskHintHtml(DataRow drTask)
        {
            string text3;
            try
            {
                if (drTask == null)
                {
                    return "";
                }
                string text2 = ConvertRule.ToString(drTask["TaskName"]);
                int num = ConvertRule.ToInt(drTask["Status"]);
                text3 = "工作名称：" + text2 + "<br>状　　态：" + ComSource.GetTaskStatusName(num.ToString()) + "<br>当前进度：" + ConvertRule.ToInt(drTask["CompletePercent"]).ToString() + "%<br>计划开始日期：" + ConvertRule.ToDateString(drTask["PlannedStartDate"], "yyyy-MM-dd") + "<br>计划结束日期：" + ConvertRule.ToDateString(drTask["PlannedFinishDate"], "yyyy-MM-dd") + "<br>实际开始日期：" + ConvertRule.ToDateString(drTask["ActualStartDate"], "yyyy-MM-dd") + "<br>实际结束日期：" + ConvertRule.ToDateString(drTask["ActualFinishDate"], "yyyy-MM-dd");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetTaskHintHtml(string WBSCode)
        {
            string text2;
            try
            {
                string taskHintHtml = "";
                if (WBSCode == "")
                {
                    return taskHintHtml;
                }
                EntityData taskByCode = WBSDAO.GetTaskByCode(WBSCode);
                if (taskByCode.HasRecord())
                {
                    taskHintHtml = GetTaskHintHtml(taskByCode.CurrentRow);
                }
                taskByCode.Dispose();
                text2 = taskHintHtml;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool IsGroundWorkTaskExists(string GroundWorkCode, string WBSCode, string ProjectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                GroundWorkStrategyBuilder builder = new GroundWorkStrategyBuilder();
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.WBSCode, WBSCode));
                builder.AddStrategy(new Strategy(GroundWorkStrategyName.GroundWorkCodeNot, GroundWorkCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("GroundWork", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    flag = true;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void MadeDefultBuildingFloor(DataTable tb, string BuildingCode)
        {
            try
            {
                int num = 0;
                EntityData buildingByCode = ProductDAO.GetBuildingByCode(BuildingCode);
                if (buildingByCode.HasRecord())
                {
                    int num4;
                    DataRow row;
                    DataRow row2;
                    string text = buildingByCode.GetString("ProjectCode");
                    int num2 = Math.Abs(buildingByCode.GetInt("IFloorCount"));
                    EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("楼栋工程结构底层");
                    for (num4 = dictionaryItemByName.CurrentTable.Rows.Count - 1; num4 >= 0; num4--)
                    {
                        row = dictionaryItemByName.CurrentTable.Rows[num4];
                        num++;
                        row2 = tb.NewRow();
                        row2["BuildingFloorCode"] = "-" + num.ToString();
                        row2["BuildingCode"] = BuildingCode;
                        row2["ProjectCode"] = text;
                        row2["FloorIndex"] = num;
                        row2["FloorName"] = ConvertRule.ToString(row["Name"]);
                        tb.Rows.Add(row2);
                    }
                    dictionaryItemByName.Dispose();
                    for (num4 = 1; num4 <= num2; num4++)
                    {
                        num++;
                        row2 = tb.NewRow();
                        row2["BuildingFloorCode"] = "-" + num.ToString();
                        row2["BuildingCode"] = BuildingCode;
                        row2["ProjectCode"] = text;
                        row2["FloorIndex"] = num;
                        row2["FloorName"] = num4.ToString();
                        tb.Rows.Add(row2);
                    }
                    dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName("楼栋工程结构顶层");
                    int count = dictionaryItemByName.CurrentTable.Rows.Count;
                    for (num4 = 0; num4 < count; num4++)
                    {
                        row = dictionaryItemByName.CurrentTable.Rows[num4];
                        num++;
                        row2 = tb.NewRow();
                        row2["BuildingFloorCode"] = "-" + num.ToString();
                        row2["BuildingCode"] = BuildingCode;
                        row2["ProjectCode"] = text;
                        row2["FloorIndex"] = num;
                        row2["FloorName"] = ConvertRule.ToString(row["Name"]);
                        tb.Rows.Add(row2);
                    }
                    dictionaryItemByName.Dispose();
                }
                buildingByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void ResetProjectProgressGantt(DataSet ds, string GanttType)
        {
            try
            {
                DataTable tbGantt;
                DataTable tbTask = ds.Tables["Task"];
                if (GanttType == "A")
                {
                    tbGantt = GetProjectProgressGanttByTaskA(tbTask);
                }
                else if (GanttType == "B")
                {
                    tbGantt = GetProjectProgressGanttByTaskB(tbTask);
                }
                else
                {
                    tbGantt = GetProjectProgressGanttByTask(tbTask);
                }
                DataTable table = GetProjectProgressGanttInfo(tbGantt);
                if (ds.Tables.Contains("Gantt"))
                {
                    ds.Tables.Remove("Gantt");
                }
                if (ds.Tables.Contains("Info"))
                {
                    ds.Tables.Remove("Info");
                }
                ds.Tables.Add(tbGantt);
                ds.Tables.Add(table);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTaskPercentByConstructProg(string BuildingCode, string WBSCode)
        {
            try
            {
                decimal completePercent = CalcTaskPercentByConstructProg(BuildingCode, WBSCode);
                WBSRule.UpdateTaskCompletePercent(WBSCode, completePercent);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

