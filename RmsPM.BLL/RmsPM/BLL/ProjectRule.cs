namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.BLL.RefSal;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class ProjectRule
    {
        public static DataTable GetAllProject()
        {
            DataTable currentTable;
            try
            {
                ProjectStrategyBuilder builder = new ProjectStrategyBuilder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Project", builder.BuildMainQueryString());
                agent.Dispose();
                currentTable = data.CurrentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return currentTable;
        }

        public static string GetAllSalProjectCode()
        {
            string text2;
            try
            {
                string text = "";
                EntityData allSalProjectCode = ProjectDAO.GetAllSalProjectCode();
                if (allSalProjectCode.HasRecord())
                {
                    if (text != "")
                    {
                        text = text + ",";
                    }
                    text = text + allSalProjectCode.GetString("SalProjectCode");
                }
                allSalProjectCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetContractModelName(string contractModelCode)
        {
            string text2;
            try
            {
                string text = "";
                if (contractModelCode == "")
                {
                    return text;
                }
                EntityData contractModelByCode = ContractDAO.GetContractModelByCode(contractModelCode);
                if (contractModelByCode.HasRecord())
                {
                    text = contractModelByCode.GetString("ContractModelName");
                }
                contractModelByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetDevelopUnitByProject(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("DevelopUnit");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectAdress(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("ProjectAddress");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetProjectArea(string projectCode)
        {
            decimal num2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildQuerySumBuildAreaString();
                QueryAgent agent = new QueryAgent();
                decimal num = (decimal) agent.ExecuteScalar(queryString);
                agent.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static EntityData GetProjectByUnit(string unitCode)
        {
            EntityData data2;
            try
            {
                ProjectStrategyBuilder builder = new ProjectStrategyBuilder();
                builder.AddStrategy(new Strategy(ProjectStrategyName.UnitCode, unitCode));
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Project", builder.BuildMainQueryString());
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetProjectCodeByName(string ProjectName)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select ProjectCode from Project where ProjectName like '%{0}%'", ProjectName));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectCodeBySalProjectCode(string SalProjectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (SalProjectCode == "")
                {
                    return text;
                }
                EntityData projectBySalProjectCode = ProjectDAO.GetProjectBySalProjectCode(SalProjectCode);
                if (projectBySalProjectCode.HasRecord())
                {
                    text = projectBySalProjectCode.GetString("ProjectCode");
                }
                projectBySalProjectCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectID(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("ProjectID");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectName(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("ProjectName");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectShortName(string ProjectCode)
        {
            string projectShortName;
            try
            {
                projectShortName = GetProjectShortName(ProjectCode, false);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return projectShortName;
        }

        public static string GetProjectShortName(string ProjectCode, bool WhenNullUseProjectName)
        {
            string text2;
            try
            {
                string text = "";
                if (ProjectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(ProjectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("ProjectShortName");
                    if ((text == "") && WhenNullUseProjectName)
                    {
                        text = projectByCode.GetString("ProjectName");
                    }
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalProjectCode(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("SalProjectCode");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSalProjectName(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                SalService service = new SalService();
                DataTable table = service.GetSalProjectByCode(projectCode).Tables[0];
                if (table.Rows.Count > 0)
                {
                    text = ConvertRule.ToString(table.Rows[0]["Proj_Name"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectSetCodeByProject(string projectCode)
        {
            string text2;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    text = projectByCode.GetString("SubjectSetCode");
                }
                projectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSupplierCodeByName(string SuplName)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select SupplierCode from Supplier where SupplierName = '{0}'", SuplName));
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        text = obj2.ToString();
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSupplierName(string supplierCode)
        {
            string text2;
            try
            {
                string text = "";
                if (supplierCode == "")
                {
                    return text;
                }
                EntityData supplierByCode = ProjectDAO.GetSupplierByCode(supplierCode);
                if (supplierByCode.HasRecord())
                {
                    text = supplierByCode.GetString("SupplierName");
                }
                supplierByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSupplierTypeName(string supplierTypeCode)
        {
            string systemGroupFullName;
            try
            {
                systemGroupFullName = SystemGroupRule.GetSystemGroupFullName(supplierTypeCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return systemGroupFullName;
        }

        public static string GetUnitByProject(string projectCode)
        {
            string text3;
            try
            {
                string text = "";
                if (projectCode == "")
                {
                    return text;
                }
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.RelaCode, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("UnitCode");
                }
                data.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static bool IsProjectNameExists(string name, string code)
        {
            bool flag2;
            try
            {
                bool flag = false;
                ProjectStrategyBuilder builder = new ProjectStrategyBuilder();
                builder.AddStrategy(new Strategy(ProjectStrategyName.ProjectName, name));
                builder.AddStrategy(new Strategy(ProjectStrategyName.ProjectCodeNot, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Project", queryString);
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

        public static bool IsUseShortNameByProjectCode(string projectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                if (projectCode == "")
                {
                    return flag;
                }
                EntityData projectByCode = ProjectDAO.GetProjectByCode(projectCode);
                if (projectByCode.HasRecord())
                {
                    flag = projectByCode.GetString("IsUseShortName") == "1";
                }
                projectByCode.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsUseShortNameByValue(string Value)
        {
            try
            {
                if (Value == "1")
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return false;
        }

        public static void UpdateUniSalProjectCode(string SalProjectCode, string ProjectCode)
        {
            try
            {
                if (SalProjectCode != "")
                {
                    ProjectStrategyBuilder builder = new ProjectStrategyBuilder();
                    builder.AddStrategy(new Strategy(ProjectStrategyName.SalProjectCode, SalProjectCode));
                    builder.AddStrategy(new Strategy(ProjectStrategyName.ProjectCodeNot, ProjectCode));
                    string queryString = builder.BuildMainQueryString();
                    QueryAgent agent = new QueryAgent();
                    EntityData data = agent.FillEntityData("Project", queryString);
                    agent.Dispose();
                    if (data.HasRecord())
                    {
                        foreach (DataRow row in data.CurrentTable.Rows)
                        {
                            string text2 = row["ProjectCode"].ToString();
                            agent.ExecuteScalar(string.Format("update project set SalProjectCode = '' where ProjectCode = '{0}'", text2));
                        }
                    }
                    data.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

