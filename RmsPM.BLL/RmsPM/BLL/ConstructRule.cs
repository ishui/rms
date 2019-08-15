namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class ConstructRule
    {
        public static void AddConstructProgressLastReportDate(string PBSUnitCode, DataTable tb)
        {
            try
            {
                tb.Columns.Add("IsEnd", typeof(int));
                foreach (DataRow row in tb.Rows)
                {
                    string visualProgress = ConvertRule.ToString(row["VisualProgress"]);
                    if (row["EndDate"] == DBNull.Value)
                    {
                        row["IsEnd"] = 0;
                        EntityData lastConstructProgressReportByVisualProgress = GetLastConstructProgressReportByVisualProgress(PBSUnitCode, visualProgress);
                        if (lastConstructProgressReportByVisualProgress.HasRecord())
                        {
                            row["EndDate"] = ConvertRule.ToDate(lastConstructProgressReportByVisualProgress.CurrentRow["ReportDate"]);
                        }
                        lastConstructProgressReportByVisualProgress.Dispose();
                    }
                    else
                    {
                        row["IsEnd"] = 1;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal CalcInvestByVisualProgress(decimal TotalInvest, string VisualProgress, int TotalFloorCount, int CurrFloorCount)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                QueryAgent agent = new QueryAgent();
                try
                {
                    string format = "select dbo.GetTotalCompleteInvestNew({0}, '{1}', {2}, {3})";
                    format = string.Format(format, new object[] { TotalInvest, VisualProgress, TotalFloorCount, CurrFloorCount });
                    num = ConvertRule.ToDecimal(agent.ExecuteScalar(format));
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal CalcPBSUnitCompleteInvest(string PBSUnitCode)
        {
            decimal num5;
            try
            {
                decimal num = 0M;
                EntityData data = PBSDAO.GetV_PBSUnitByCode(PBSUnitCode);
                if (data.HasRecord())
                {
                    decimal totalInvest = data.GetDecimal("PInvest");
                    string visualProgress = data.GetString("VisualProgress");
                    int totalFloorCount = PBSRule.GetPBSUnitFloorCount(PBSUnitCode);
                    int currFloorCount = PBSRule.GetPBSUnitCurrentFloorCount(PBSUnitCode);
                    num = CalcInvestByVisualProgress(totalInvest, visualProgress, totalFloorCount, currFloorCount);
                }
                data.Dispose();
                num5 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num5;
        }

        public static DataTable CreateConstructPlanTable()
        {
            DataTable table2;
            try
            {
                EntityData data = new EntityData("ConstructAnnualPlan");
                DataTable currentTable = data.CurrentTable;
                currentTable.Columns.Add(new DataColumn("PBSUnitName", typeof(string)));
                currentTable.Columns.Add(new DataColumn("TotalBuildArea", typeof(decimal)));
                currentTable.Columns.Add(new DataColumn("PTotalInvest", typeof(decimal)));
                data.Dispose();
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void DeleteConstructAnnualPlan(string AnnuelPlanCode)
        {
            try
            {
                if (AnnuelPlanCode != "")
                {
                    EntityData entity = ConstructDAO.GetConstructAnnualPlanByCode(AnnuelPlanCode);
                    if (entity.HasRecord())
                    {
                        string pBSUnitCode = entity.GetString("PBSUnitCode");
                        int iYear = entity.GetInt("IYear");
                        EntityData constructPlanStepByPBSUnitYear = ConstructDAO.GetConstructPlanStepByPBSUnitYear(pBSUnitCode, iYear);
                        ConstructDAO.DeleteConstructPlanStep(constructPlanStepByPBSUnitYear);
                        constructPlanStepByPBSUnitYear.Dispose();
                    }
                    ConstructDAO.DeleteConstructAnnualPlan(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteConstructAnnualPlan(string ProjectCode, int IYear)
        {
            try
            {
                if (ProjectCode != "")
                {
                    EntityData entity = ConstructDAO.GetConstructPlanStepByProjectYear(ProjectCode, IYear);
                    ConstructDAO.DeleteConstructPlanStep(entity);
                    entity.Dispose();
                    EntityData constructAnnualPlanByProjectYear = ConstructDAO.GetConstructAnnualPlanByProjectYear(ProjectCode, IYear);
                    ConstructDAO.DeleteConstructAnnualPlan(constructAnnualPlanByProjectYear);
                    constructAnnualPlanByProjectYear.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteConstructProgressReport(string ProgressCode)
        {
            try
            {
                EntityData entity = ConstructDAO.GetStandard_ConstructProgressByCode(ProgressCode);
                if (entity.HasRecord())
                {
                    string pBSUnitCode = entity.GetString("PBSUnitCode");
                    ConstructDAO.DeleteStandard_ConstructProgress(entity);
                    UpdatePBSUnitByConstructProgressReport(pBSUnitCode);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteRiskIndex(string IndexCode)
        {
            try
            {
                if (IndexCode != "")
                {
                    EntityData entity = ConstructDAO.GetRiskIndexByCode(IndexCode);
                    ConstructDAO.DeleteRiskIndex(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteRiskType(string TypeCode)
        {
            try
            {
                if (TypeCode != "")
                {
                    EntityData entity = ConstructDAO.GetRiskTypeByCode(TypeCode);
                    ConstructDAO.DeleteRiskType(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteVisualProgress(string code)
        {
            try
            {
                if (code != "")
                {
                    EntityData entity = ConstructDAO.GetVisualProgressByCode(code);
                    ConstructDAO.DeleteVisualProgress(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void FormatConstructProgressDate(DataTable tb)
        {
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    if ((row["StartDate"] == DBNull.Value) && (row["EndDate"] != DBNull.Value))
                    {
                        row["StartDate"] = row["EndDate"];
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable GenerateConstructPlanProgressTable(string PBSUnitCode, int year)
        {
            DataTable table4;
            try
            {
                EntityData data = new EntityData("ConstructProgressStep");
                data.CurrentTable.Columns.Add(new DataColumn("PStartDate", typeof(DateTime)));
                data.CurrentTable.Columns.Add(new DataColumn("PEndDate", typeof(DateTime)));
                data.CurrentTable.Columns.Add(new DataColumn("ProgressType", typeof(int)));
                data.CurrentTable.Columns.Add(new DataColumn("IsPoint", typeof(int)));
                data.CurrentTable.Columns.Add(new DataColumn("VisualProgressName", typeof(string)));
                EntityData validVisualProgress = GetValidVisualProgress();
                EntityData constructPlanStepByPBSUnitYear = ConstructDAO.GetConstructPlanStepByPBSUnitYear(PBSUnitCode, year);
                EntityData constructProgressStepByPBSUnit = ConstructDAO.GetConstructProgressStepByPBSUnit(PBSUnitCode);
                DataTable currentTable = data.CurrentTable;
                DataTable table2 = constructPlanStepByPBSUnitYear.CurrentTable;
                DataTable table3 = constructProgressStepByPBSUnit.CurrentTable;
                int num = 0;
                int count = validVisualProgress.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    validVisualProgress.SetCurrentRow(i);
                    string text = validVisualProgress.GetString("SystemID");
                    DataRow row = currentTable.NewRow();
                    row["VisualProgress"] = text;
                    row["VisualProgressName"] = validVisualProgress.GetString("VisualProgress");
                    row["ProgressType"] = validVisualProgress.GetInt("ProgressType");
                    row["IsPoint"] = validVisualProgress.GetInt("IsPoint");
                    DataRow[] rowArray = table3.Select("VisualProgress='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row["ProgressStepCode"] = ConvertRule.ToString(rowArray[0]["ProgressStepCode"]);
                        row["StartDate"] = ConvertRule.ToDate(rowArray[0]["StartDate"]);
                        row["EndDate"] = ConvertRule.ToDate(rowArray[0]["EndDate"]);
                    }
                    else
                    {
                        num--;
                        row["ProgressStepCode"] = num.ToString();
                    }
                    DataRow[] rowArray2 = table2.Select("VisualProgress='" + text + "'");
                    if (rowArray2.Length > 0)
                    {
                        row["PStartDate"] = ConvertRule.ToDate(rowArray2[0]["StartDate"]);
                        row["PEndDate"] = ConvertRule.ToDate(rowArray2[0]["EndDate"]);
                    }
                    currentTable.Rows.Add(row);
                }
                constructPlanStepByPBSUnitYear.Dispose();
                constructProgressStepByPBSUnit.Dispose();
                validVisualProgress.Dispose();
                data.Dispose();
                table4 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GenerateConstructPlanTable(string ProjectCode, int IYear)
        {
            DataTable table3;
            try
            {
                DataTable tbDst = CreateConstructPlanTable();
                EntityData data = PBSDAO.GetV_PBSUnitByProject(ProjectCode);
                EntityData constructAnnualPlanByProjectYear = ConstructDAO.GetConstructAnnualPlanByProjectYear(ProjectCode, IYear);
                DataTable tbSrc = constructAnnualPlanByProjectYear.CurrentTable;
                int num = 0;
                int count = data.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    data.SetCurrentRow(i);
                    string text = data.GetString("PBSUnitCode");
                    DataRow drDst = tbDst.NewRow();
                    drDst["PBSUnitCode"] = text;
                    DataRow[] rowArray = tbSrc.Select("PBSUnitCode='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        ConvertRule.DataRowCopy(rowArray[0], drDst, tbSrc, tbDst);
                    }
                    else
                    {
                        num++;
                        drDst["AnnualPlanCode"] = -num;
                    }
                    drDst["PBSUnitName"] = data.GetString("PBSUnitName");
                    drDst["TotalBuildArea"] = data.GetDecimal("TotalBuildArea");
                    drDst["PTotalInvest"] = data.GetDecimal("PInvest");
                    tbDst.Rows.Add(drDst);
                }
                constructAnnualPlanByProjectYear.Dispose();
                data.Dispose();
                table3 = tbDst;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static DataTable GenerateConstructPlanTable(string PBSUnitCode, int year, bool isCopy)
        {
            DataTable table3;
            try
            {
                EntityData data = new EntityData("ConstructPlanStep");
                data.CurrentTable.Columns.Add(new DataColumn("ProgressType", typeof(int)));
                data.CurrentTable.Columns.Add(new DataColumn("IsPoint", typeof(int)));
                data.CurrentTable.Columns.Add(new DataColumn("VisualProgressName", typeof(string)));
                EntityData validVisualProgress = GetValidVisualProgress();
                EntityData constructPlanStepByPBSUnitYear = ConstructDAO.GetConstructPlanStepByPBSUnitYear(PBSUnitCode, year);
                DataTable currentTable = data.CurrentTable;
                DataTable table2 = constructPlanStepByPBSUnitYear.CurrentTable;
                int num = 0;
                int count = validVisualProgress.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    validVisualProgress.SetCurrentRow(i);
                    string text = validVisualProgress.GetString("SystemID");
                    DataRow row = currentTable.NewRow();
                    row["VisualProgress"] = text;
                    row["VisualProgressName"] = validVisualProgress.GetString("VisualProgress");
                    row["ProgressType"] = validVisualProgress.GetInt("ProgressType");
                    row["IsPoint"] = validVisualProgress.GetInt("IsPoint");
                    DataRow[] rowArray = table2.Select("VisualProgress='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        if (isCopy)
                        {
                            num--;
                            row["ConstructPlanStepCode"] = num.ToString();
                        }
                        else
                        {
                            row["ConstructPlanStepCode"] = ConvertRule.ToString(rowArray[0]["ConstructPlanStepCode"]);
                        }
                        row["StartDate"] = ConvertRule.ToDate(rowArray[0]["StartDate"]);
                        row["EndDate"] = ConvertRule.ToDate(rowArray[0]["EndDate"]);
                    }
                    else
                    {
                        num--;
                        row["ConstructPlanStepCode"] = num.ToString();
                    }
                    currentTable.Rows.Add(row);
                }
                constructPlanStepByPBSUnitYear.Dispose();
                validVisualProgress.Dispose();
                data.Dispose();
                table3 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static DataTable GenerateConstructProgressRiskTable(string ProgressCode, bool IsDefault)
        {
            DataTable table3;
            try
            {
                EntityData data = new EntityData("ConstructProgressRisk");
                EntityData allRiskType = ConstructDAO.GetAllRiskType();
                EntityData constructProgressRiskByProgressCode = ConstructDAO.GetConstructProgressRiskByProgressCode(ProgressCode);
                DataTable currentTable = data.CurrentTable;
                DataTable table2 = constructProgressRiskByProgressCode.CurrentTable;
                currentTable.Columns.Add("RiskIndexName", typeof(string));
                int num = 0;
                int count = allRiskType.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allRiskType.SetCurrentRow(i);
                    string text = allRiskType.GetString("TypeName");
                    DataRow row = currentTable.NewRow();
                    row["RiskTypeName"] = text;
                    DataRow[] rowArray = table2.Select("RiskTypeName='" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        row["ProgressRiskCode"] = ConvertRule.ToString(rowArray[0]["ProgressRiskCode"]);
                        row["RiskIndexCode"] = rowArray[0]["RiskIndexCode"];
                    }
                    else
                    {
                        num--;
                        row["ProgressRiskCode"] = num.ToString();
                    }
                    if ((ConvertRule.ToString(row["RiskIndexCode"]) == "") && IsDefault)
                    {
                        EntityData defaultRiskIndex = ConstructDAO.GetDefaultRiskIndex();
                        if (defaultRiskIndex.HasRecord())
                        {
                            row["RiskIndexCode"] = defaultRiskIndex.GetString("IndexCode");
                            row["RiskIndexName"] = defaultRiskIndex.GetString("IndexName");
                        }
                        defaultRiskIndex.Dispose();
                    }
                    if (ConvertRule.ToString(row["RiskIndexName"]) == "")
                    {
                        row["RiskIndexName"] = GetRiskIndexName(row["RiskIndexCode"]);
                    }
                    currentTable.Rows.Add(row);
                }
                constructProgressRiskByProgressCode.Dispose();
                allRiskType.Dispose();
                data.Dispose();
                table3 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        public static string GetConstructPlanCurrYearByProject(string ProjectCode)
        {
            string text3;
            try
            {
                string queryString = string.Format("select top 1 IYear from ConstructAnnualPlan where ProjectCode = '{0}' order by 1 desc", ProjectCode);
                QueryAgent agent = new QueryAgent();
                string text2 = ConvertRule.ToString(agent.ExecuteScalar(queryString));
                agent.Dispose();
                text3 = text2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetConstructPlanVisualProgress(string PBSUnitCode, int IYear)
        {
            string text2;
            try
            {
                string text = "";
                EntityData constructAnnualPlanByPBSUnitYear = ConstructDAO.GetConstructAnnualPlanByPBSUnitYear(PBSUnitCode, IYear);
                if (constructAnnualPlanByPBSUnitYear.HasRecord())
                {
                    text = constructAnnualPlanByPBSUnitYear.GetString("VisualProgress");
                }
                constructAnnualPlanByPBSUnitYear.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetConstructProgressReportFirstJg(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                string visualProgressJgInStr = PBSRule.GetVisualProgressJgInStr();
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.VisualProgressIn, visualProgressJgInStr));
                builder.AddOrder("ReportDate", true);
                builder.AddOrder("ProgressCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructProgressReportFirstKg(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                string visualProgressNotStartInStr = PBSRule.GetVisualProgressNotStartInStr();
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.VisualProgressNotIn, visualProgressNotStartInStr));
                builder.AddOrder("ReportDate", true);
                builder.AddOrder("ProgressCode", true);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static object GetConstructProgressStepFirstJg(string PBSUnitCode)
        {
            object obj3;
            try
            {
                object obj2 = DBNull.Value;
                string visualProgressJgInStr = PBSRule.GetVisualProgressJgInStr();
                ConstructProgressStepStrategyBuilder builder = new ConstructProgressStepStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.VisualProgressIn, visualProgressJgInStr));
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.EndDateNotNull));
                builder.AddOrder("EndDate", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructProgressStep", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    obj2 = data.CurrentRow["EndDate"];
                }
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj3;
        }

        public static object GetConstructProgressStepFirstKg(string PBSUnitCode)
        {
            object obj3;
            try
            {
                object obj2 = DBNull.Value;
                string visualProgressNotStartInStr = PBSRule.GetVisualProgressNotStartInStr();
                ConstructProgressStepStrategyBuilder builder = new ConstructProgressStepStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.VisualProgressNotIn, visualProgressNotStartInStr));
                builder.AddStrategy(new Strategy(ConstructProgressStepStrategyName.StartDateNotNull));
                builder.AddOrder("StartDate", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructProgressStep", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    obj2 = data.CurrentRow["StartDate"];
                }
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj3;
        }

        public static EntityData GetLastConstructProgressReport(string PBSUnitCode)
        {
            EntityData lastConstructProgressReport;
            try
            {
                lastConstructProgressReport = GetLastConstructProgressReport(PBSUnitCode, 1);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return lastConstructProgressReport;
        }

        public static EntityData GetLastConstructProgressReport(string PBSUnitCode, int n)
        {
            EntityData data2;
            try
            {
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("ReportDate", false);
                builder.AddOrder("ProgressCode", false);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(n);
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetLastConstructProgressReport(string PBSUnitCode, string EndDate, int n)
        {
            EntityData data2;
            try
            {
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                ArrayList pas = new ArrayList();
                pas.Add("");
                pas.Add(EndDate);
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.ReportDateRange, pas));
                builder.AddOrder("ReportDate", false);
                builder.AddOrder("ProgressCode", false);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(n);
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetLastConstructProgressReportByVisualProgress(string PBSUnitCode, string VisualProgress)
        {
            EntityData data2;
            try
            {
                ConstructProgressStrategyBuilder builder = new ConstructProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddStrategy(new Strategy(ConstructProgressStrategyName.VisualProgress, VisualProgress));
                builder.AddOrder("ReportDate", false);
                builder.AddOrder("ProgressCode", false);
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                EntityData data = agent.FillEntityData("ConstructProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetRiskIndexName(object IndexCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((IndexCode != null) && (IndexCode.ToString() != ""))
                {
                    EntityData riskIndexByCode = ConstructDAO.GetRiskIndexByCode(IndexCode.ToString());
                    if (riskIndexByCode.HasRecord())
                    {
                        text = riskIndexByCode.GetString("IndexName");
                    }
                    riskIndexByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetRiskTypeName(object TypeCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((TypeCode != null) && (TypeCode.ToString() != ""))
                {
                    EntityData riskTypeByCode = ConstructDAO.GetRiskTypeByCode(TypeCode.ToString());
                    if (riskTypeByCode.HasRecord())
                    {
                        text = riskTypeByCode.GetString("TypeName");
                    }
                    riskTypeByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetValidVisualProgress()
        {
            EntityData data2;
            try
            {
                VisualProgressStrategyBuilder builder = new VisualProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(VisualProgressStrategyName.ProgressTypeNot, "-1"));
                builder.AddOrder("SortID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("VisualProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetVisualProgressName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData visualProgressByCode = ConstructDAO.GetVisualProgressByCode(code);
                if (visualProgressByCode.HasRecord())
                {
                    text = visualProgressByCode.GetString("VisualProgress");
                }
                visualProgressByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void InsertDefaultConstructAnnualPlan(string ProjectCode)
        {
            try
            {
                string val = GetConstructPlanCurrYearByProject(ProjectCode);
                if (val == "")
                {
                    val = DateTime.Today.Year.ToString();
                }
                int iYear = ConvertRule.ToInt(val);
                EntityData pBSUnitByProject = PBSDAO.GetPBSUnitByProject(ProjectCode);
                EntityData constructAnnualPlanByProjectYear = ConstructDAO.GetConstructAnnualPlanByProjectYear(ProjectCode, iYear);
                EntityData entity = new EntityData("ConstructAnnualPlan");
                foreach (DataRow row in pBSUnitByProject.CurrentTable.Rows)
                {
                    string text2 = row["PBSUnitCode"].ToString();
                    if (constructAnnualPlanByProjectYear.CurrentTable.Select("PBSUnitCode='" + text2 + "'").Length == 0)
                    {
                        DataRow row2 = entity.CurrentTable.NewRow();
                        row2["AnnualPlanCode"] = SystemManageDAO.GetNewSysCode("AnnualPlanCode");
                        row2["PBSUnitCode"] = text2;
                        row2["ProjectCode"] = ProjectCode;
                        row2["IYear"] = iYear;
                        entity.CurrentTable.Rows.Add(row2);
                    }
                }
                ConstructDAO.SubmitAllConstructAnnualPlan(entity);
                entity.Dispose();
                constructAnnualPlanByProjectYear.Dispose();
                pBSUnitByProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void NewYearConstructAnnualPlan(string ProjectCode, int IYear, string UserCode)
        {
            try
            {
                if ((ProjectCode != "") && (IYear > 0))
                {
                    DataRow drDst;
                    int num = IYear + 1;
                    EntityData constructPlanStepByProjectYear = ConstructDAO.GetConstructPlanStepByProjectYear(ProjectCode, IYear);
                    EntityData entity = new EntityData("ConstructPlanStep");
                    foreach (DataRow row in constructPlanStepByProjectYear.CurrentTable.Rows)
                    {
                        drDst = entity.CurrentTable.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, constructPlanStepByProjectYear.CurrentTable, entity.CurrentTable);
                        drDst["ConstructPlanStepCode"] = SystemManageDAO.GetNewSysCode("ConstructPlanStepCode");
                        drDst["IYear"] = num;
                        entity.CurrentTable.Rows.Add(drDst);
                    }
                    ConstructDAO.InsertConstructPlanStep(entity);
                    entity.Dispose();
                    constructPlanStepByProjectYear.Dispose();
                    EntityData data3 = PBSDAO.GetV_PBSUnitByProject(ProjectCode);
                    EntityData constructAnnualPlanByProjectYear = ConstructDAO.GetConstructAnnualPlanByProjectYear(ProjectCode, IYear);
                    entity = new EntityData("ConstructAnnualPlan");
                    foreach (DataRow row3 in data3.CurrentTable.Rows)
                    {
                        string pBSUnitCode = ConvertRule.ToString(row3["PBSUnitCode"]);
                        string code = ConvertRule.ToString(row3["VisualProgress"]);
                        decimal num2 = ConvertRule.ToDecimal(row3["TotalBuildArea"]);
                        drDst = entity.CurrentTable.NewRow();
                        drDst["AnnualPlanCode"] = SystemManageDAO.GetNewSysCode("AnnualPlanCode");
                        entity.CurrentTable.Rows.Add(drDst);
                        drDst["IYear"] = num;
                        drDst["PBSUnitCode"] = pBSUnitCode;
                        drDst["ProjectCode"] = ProjectCode;
                        drDst["PlanDate"] = DateTime.Now;
                        drDst["PlanPerson"] = UserCode;
                        decimal num3 = CalcPBSUnitCompleteInvest(pBSUnitCode);
                        drDst["InvestBefore"] = num3;
                        EntityData visualProgressByCode = ConstructDAO.GetVisualProgressByCode(code);
                        if (visualProgressByCode.HasRecord() && (visualProgressByCode.GetInt("ProgressType") >= 0))
                        {
                            drDst["LCFArea"] = num2;
                        }
                        visualProgressByCode.Dispose();
                        ConstructDAO.InsertConstructAnnualPlan(entity);
                    }
                    entity.Dispose();
                    constructAnnualPlanByProjectYear.Dispose();
                    data3.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructAnnualPlanInvest(string PBSUnitCode)
        {
            try
            {
                if (PBSUnitCode != "")
                {
                    EntityData pBSUnitByCode = PBSDAO.GetPBSUnitByCode(PBSUnitCode);
                    if (pBSUnitByCode.HasRecord())
                    {
                        decimal totalInvest = ConvertRule.ToDecimal(pBSUnitByCode.CurrentRow["PInvest"]);
                        EntityData entity = ConstructDAO.GetConstructAnnualPlanByPBSUnit(PBSUnitCode);
                        foreach (DataRow row2 in entity.CurrentTable.Rows)
                        {
                            string visualProgress = ConvertRule.ToString(row2["VisualProgress"]);
                            int currFloorCount = ConvertRule.ToInt(row2["CurrentFloor"]);
                            int totalFloorCount = PBSRule.GetPBSUnitFloorCount(PBSUnitCode);
                            decimal num4 = CalcInvestByVisualProgress(totalInvest, visualProgress, totalFloorCount, currFloorCount);
                            decimal num5 = ConvertRule.ToDecimal(row2["InvestBefore"]);
                            decimal num6 = num4 - num5;
                            row2["PInvest"] = num6;
                        }
                        ConstructDAO.UpdateConstructAnnualPlan(entity);
                        entity.Dispose();
                        pBSUnitByCode.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBSUnitByConstructProgressReport(string PBSUnitCode)
        {
            try
            {
                EntityData entity = PBSDAO.GetPBSUnitByCode(PBSUnitCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    string text = "";
                    int @int = 0;
                    EntityData lastConstructProgressReport = GetLastConstructProgressReport(PBSUnitCode);
                    if (lastConstructProgressReport.HasRecord())
                    {
                        text = lastConstructProgressReport.GetString("VisualProgress");
                        @int = lastConstructProgressReport.GetInt("CurrentLayer");
                    }
                    lastConstructProgressReport.Dispose();
                    if (text == "")
                    {
                        currentRow["VisualProgress"] = DBNull.Value;
                    }
                    else
                    {
                        currentRow["VisualProgress"] = text;
                    }
                    object constructProgressStepFirstKg = GetConstructProgressStepFirstKg(PBSUnitCode);
                    currentRow["StartDate"] = constructProgressStepFirstKg;
                    object constructProgressStepFirstJg = GetConstructProgressStepFirstJg(PBSUnitCode);
                    currentRow["EndDate"] = constructProgressStepFirstJg;
                    EntityData buildingByPBSUnitCode = ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
                    if (buildingByPBSUnitCode.HasRecord())
                    {
                        foreach (DataRow row2 in buildingByPBSUnitCode.CurrentTable.Rows)
                        {
                            row2["CurrentLayer"] = @int;
                            ProductDAO.UpdateBuilding(buildingByPBSUnitCode);
                        }
                    }
                    buildingByPBSUnitCode.Dispose();
                    PBSDAO.UpdatePBSUnit(entity);
                    lastConstructProgressReport.Dispose();
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

