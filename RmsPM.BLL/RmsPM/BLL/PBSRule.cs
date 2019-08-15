namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class PBSRule
    {
        public static string CheckPBSUnitFloorCount(string PBSUnitCode, string InputFloorCount, string InputName)
        {
            string text2;
            try
            {
                string text = "";
                int num = ConvertRule.ToInt(InputFloorCount);
                if (num == 0)
                {
                    return text;
                }
                int pBSUnitFloorCount = GetPBSUnitFloorCount(PBSUnitCode);
                if (pBSUnitFloorCount >= 0)
                {
                    if (num > pBSUnitFloorCount)
                    {
                        return string.Format("{0}({1})不能超出总层数({2})", InputName, num, pBSUnitFloorCount);
                    }
                    if (num < 0)
                    {
                        return string.Format("{0}({1})不能为负数", InputName, num);
                    }
                }
                else
                {
                    if (Math.Abs(num) > Math.Abs(pBSUnitFloorCount))
                    {
                        return string.Format("{0}({1})不能超出总层数({2})", InputName, num, pBSUnitFloorCount);
                    }
                    if (num > 0)
                    {
                        return string.Format("{0}({1})必须为负数", InputName, num);
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void DeletePBSType(string PBSTypeCode)
        {
            try
            {
                if (PBSTypeCode != "")
                {
                    PBSDAO.DeletePBSType(PBSDAO.GetPBSTypeAllChildByParentCode(PBSTypeCode));
                    EntityData entity = PBSDAO.GetPBSTypeByCode(PBSTypeCode);
                    PBSDAO.DeletePBSType(entity);
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePBSUnit(string PBSUnitCode)
        {
            try
            {
                EntityData entity = ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    string text = row["BuildingCode"].ToString();
                    row["PBSUnitCode"] = DBNull.Value;
                }
                ProductDAO.UpdateBuilding(entity);
                entity.Dispose();
                entity = ConstructDAO.GetConstructAnnualPlanByPBSUnit(PBSUnitCode);
                if (entity.HasRecord())
                {
                    ConstructDAO.DeleteConstructAnnualPlan(entity);
                }
                entity.Dispose();
                entity = ConstructDAO.GetConstructPlanStepByPBSUnit(PBSUnitCode);
                if (entity.HasRecord())
                {
                    ConstructDAO.DeleteConstructPlanStep(entity);
                }
                entity.Dispose();
                entity = ConstructDAO.GetConstructProgressRiskByPBSUnitCode(PBSUnitCode);
                if (entity.HasRecord())
                {
                    ConstructDAO.DeleteConstructProgressRisk(entity);
                }
                entity.Dispose();
                entity = ConstructDAO.GetConstructProgressByPBSUnit(PBSUnitCode);
                if (entity.HasRecord())
                {
                    ConstructDAO.DeleteConstructProgress(entity);
                }
                entity.Dispose();
                entity = ConstructDAO.GetConstructProgressStepByPBSUnit(PBSUnitCode);
                if (entity.HasRecord())
                {
                    ConstructDAO.DeleteConstructProgressStep(entity);
                }
                entity.Dispose();
                entity = PBSDAO.GetPBSUnitByCode(PBSUnitCode);
                if (entity.HasRecord())
                {
                    PBSDAO.DeletePBSUnit(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal GetBuildingAreaByPBSUnit(string PBSUnitCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                EntityData buildingByPBSUnitCode = ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
                num = MathRule.SumColumn(buildingByPBSUnitCode.CurrentTable, "HouseArea");
                buildingByPBSUnitCode.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static DataTable GetBuildingNoPBSUnit(string ProjectCode)
        {
            DataTable table2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
                builder.AddStrategy(new Strategy(BuildingStrategyName.NullPBSUnit));
                builder.AddOrder("BuildingName", true);
                QueryAgent agent = new QueryAgent();
                string queryString = builder.BuildMainQueryString();
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                agent.Dispose();
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetFirstVisualProgress()
        {
            string text2;
            try
            {
                string text = "";
                EntityData firstVisualProgress = ConstructDAO.GetFirstVisualProgress("");
                if (firstVisualProgress.HasRecord())
                {
                    text = firstVisualProgress.GetString("SystemID");
                }
                firstVisualProgress.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetLastVisualProgress()
        {
            string text2;
            try
            {
                string text = "";
                EntityData lastVisualProgress = ConstructDAO.GetLastVisualProgress("");
                if (lastVisualProgress.HasRecord())
                {
                    text = lastVisualProgress.GetString("SystemID");
                }
                lastVisualProgress.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetModelName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData modelByCode = ProductDAO.GetModelByCode(code);
                if (modelByCode.HasRecord())
                {
                    text = modelByCode.GetString("ModelName");
                }
                modelByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSTypeFullID(object PBSTypeCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((PBSTypeCode != null) && (PBSTypeCode.ToString() != ""))
                {
                    EntityData pBSTypeByCode = PBSDAO.GetPBSTypeByCode(PBSTypeCode.ToString());
                    if (pBSTypeByCode.HasRecord())
                    {
                        text = pBSTypeByCode.GetString("FullID");
                    }
                    pBSTypeByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSTypeFullName(object PBSTypeCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((PBSTypeCode != null) && (PBSTypeCode.ToString() != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetPBSTypeFullName('{0}')", PBSTypeCode));
                        if ((obj2 != null) && (obj2 != DBNull.Value))
                        {
                            text = obj2.ToString();
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSTypeName(object PBSTypeCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((PBSTypeCode != null) && (PBSTypeCode.ToString() != ""))
                {
                    EntityData pBSTypeByCode = PBSDAO.GetPBSTypeByCode(PBSTypeCode.ToString());
                    if (pBSTypeByCode.HasRecord())
                    {
                        text = pBSTypeByCode.GetString("PBSTypeName");
                    }
                    pBSTypeByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetPBSUnitCurrentFloorCount(string PBSUnitCode)
        {
            int num2;
            try
            {
                int num = 0;
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("CurrentLayer", false);
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                string queryString = builder.BuildMainQueryString();
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (table.Rows.Count > 0)
                {
                    num = ConvertRule.ToInt(table.Rows[0]["CurrentLayer"]);
                }
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static int GetPBSUnitFloorCount(string PBSUnitCode)
        {
            int num2;
            try
            {
                int num = 0;
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("FloorCount", false);
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                string queryString = builder.BuildMainQueryString();
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (table.Rows.Count > 0)
                {
                    num = ConvertRule.ToInt(table.Rows[0]["IFloorCount"]);
                }
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetPBSUnitName(object PBSUnitCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((PBSUnitCode != null) && (PBSUnitCode.ToString() != ""))
                {
                    EntityData pBSUnitByCode = PBSDAO.GetPBSUnitByCode(PBSUnitCode.ToString());
                    if (pBSUnitByCode.HasRecord())
                    {
                        text = pBSUnitByCode.GetString("PBSUnitName");
                    }
                    pBSUnitByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetVisualProgressJg()
        {
            DataTable currentTable;
            try
            {
                EntityData visualProgressByProgressType = ConstructDAO.GetVisualProgressByProgressType(2);
                visualProgressByProgressType.Dispose();
                currentTable = visualProgressByProgressType.CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static string GetVisualProgressJgInStr()
        {
            string text = "";
            DataTable visualProgressJg = GetVisualProgressJg();
            foreach (DataRow row in visualProgressJg.Rows)
            {
                if (text != "")
                {
                    text = text + ",";
                }
                text = text + "'" + ConvertRule.ToString(row["SystemID"]) + "'";
            }
            return text;
        }

        public static DataTable GetVisualProgressNotStart()
        {
            DataTable currentTable;
            try
            {
                EntityData visualProgressByProgressType = ConstructDAO.GetVisualProgressByProgressType(-1);
                visualProgressByProgressType.Dispose();
                currentTable = visualProgressByProgressType.CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static string GetVisualProgressNotStartInStr()
        {
            string text = "";
            DataTable visualProgressNotStart = GetVisualProgressNotStart();
            foreach (DataRow row in visualProgressNotStart.Rows)
            {
                if (text != "")
                {
                    text = text + ",";
                }
                text = text + "'" + ConvertRule.ToString(row["SystemID"]) + "'";
            }
            return text;
        }

        public static bool IsPBSUnitNameExists(string name, string code, string ProjectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                PBSUnitStrategyBuilder builder = new PBSUnitStrategyBuilder();
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitName, name));
                builder.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitCodeNot, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("PBSUnit", queryString);
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

        public static bool IsVisualProgressJg(string code)
        {
            bool flag = false;
            if ((code != "") && (GetVisualProgressJg().Select("SystemID='" + code + "'").Length > 0))
            {
                flag = true;
            }
            return flag;
        }

        public static bool IsVisualProgressStart(string code)
        {
            bool flag = false;
            if ((code != "") && (GetVisualProgressNotStart().Select("SystemID='" + code + "'").Length > 0))
            {
                flag = true;
            }
            return flag;
        }

        public static void PBSTypeCopyByProject(string srcProjectCode, string dstProjectCode)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("PBSType"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        EntityData entitydata = new EntityData("PBSType");
                        ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectByProjectCode").SqlString, "@ProjectCode", dstProjectCode, entitydata, "PBSType");
                        ydao.DeleteAllRow(entitydata);
                        ydao.DeleteEntity(entitydata);
                        DataTable table = new DataTable();
                        table.Columns.Add("OldPBSTypeCode", typeof(string));
                        table.Columns.Add("NewPBSTypeCode", typeof(string));
                        EntityData data2 = new EntityData("PBSType");
                        ydao.FillEntity(SqlManager.GetSqlStruct("PBSType", "SelectByProjectCode").SqlString, "@ProjectCode", srcProjectCode, data2, "PBSType");
                        int num = 1;
                        for (DataRow[] rowArray = data2.CurrentTable.Select("deep=" + num.ToString()); rowArray.Length > 0; rowArray = data2.CurrentTable.Select("deep=" + num.ToString()))
                        {
                            foreach (DataRow row in rowArray)
                            {
                                DataRow row2 = entitydata.CurrentTable.NewRow();
                                string newSysCode = SystemManageDAO.GetNewSysCode("PBSTypeCode");
                                row2["PBSTypeCode"] = newSysCode;
                                row2["ProjectCode"] = dstProjectCode;
                                row2["PBSTypeName"] = ConvertRule.ToString(row["PBSTypeName"]);
                                row2["Description"] = ConvertRule.ToString(row["Description"]);
                                row2["Deep"] = ConvertRule.ToInt(row["Deep"]);
                                row2["SortID"] = ConvertRule.ToInt(row["SortID"]);
                                DataRow row3 = table.NewRow();
                                row3["OldPBSTypeCode"] = ConvertRule.ToString(row["PBSTypeCode"]);
                                row3["NewPBSTypeCode"] = newSysCode;
                                table.Rows.Add(row3);
                                string text2 = ConvertRule.ToString(row["ParentCode"]);
                                if (text2 != "")
                                {
                                    DataRow[] rowArray2 = table.Select("OldPBSTypeCode='" + text2 + "'");
                                    if (rowArray2.Length > 0)
                                    {
                                        text2 = ConvertRule.ToString(rowArray2[0]["NewPBSTypeCode"]);
                                    }
                                }
                                string text3 = "";
                                if (text2 != "")
                                {
                                    DataRow[] rowArray3 = entitydata.CurrentTable.Select("PBSTypeCode='" + text2 + "'");
                                    if (rowArray3.Length > 0)
                                    {
                                        text3 = ConvertRule.ToString(rowArray3[0]["FullID"]);
                                    }
                                }
                                string text4 = newSysCode;
                                if (text3 != "")
                                {
                                    text4 = text3 + "-" + text4;
                                }
                                row2["ParentCode"] = text2;
                                row2["FullID"] = text4;
                                entitydata.CurrentTable.Rows.Add(row2);
                            }
                            num++;
                        }
                        ydao.InsertEntity(entitydata);
                        ydao.CommitTrans();
                        data2.Dispose();
                        entitydata.Dispose();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        try
                        {
                            ydao.RollBackTrans();
                        }
                        catch
                        {
                        }
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void PBSTypeExport(string ProjectCode)
        {
            try
            {
                PBSTypeCopyByProject(ProjectCode, "0");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void PBSTypeImport(string ProjectCode)
        {
            try
            {
                PBSTypeCopyByProject("0", ProjectCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void PBSTypeImportAllProject()
        {
            try
            {
                EntityData allProject = ProjectDAO.GetAllProject();
                foreach (DataRow row in allProject.CurrentTable.Rows)
                {
                    string dstProjectCode = ConvertRule.ToString(row["ProjectCode"]);
                    PBSTypeCopyByProject("0", dstProjectCode);
                }
                allProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

