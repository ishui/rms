namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class SystemRule
    {
        public const string m_ConfigCostBudgetOffineValidHours = "CostBudgetOffineValidHours";
        public const string m_ConfigVoucherIDModify = "VoucherIDModify";
        public const string m_CostApportionBuildingAreaField = "CostApportionBuildingAreaField";

        public static bool CheckUserAccountName(string userCode, string accountName)
        {
            bool flag2;
            try
            {
                DataRow[] rowArray;
                bool flag = true;
                EntityData allSystemUser = SystemManageDAO.GetAllSystemUser();
                if (userCode == "")
                {
                    rowArray = allSystemUser.CurrentTable.Select(string.Format("UserID='{0}'", accountName));
                }
                else
                {
                    rowArray = allSystemUser.CurrentTable.Select(string.Format("UserID='{0}' and UserCode<>'{1}' ", accountName, userCode));
                }
                if (rowArray.Length > 0)
                {
                    flag = false;
                }
                allSystemUser.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void DeleteUserRole(string UserCode, string RoleCode)
        {
            try
            {
                EntityData entity = SystemManageDAO.GetUserRoleByCode(UserCode, RoleCode);
                if (entity.HasRecord())
                {
                    SystemManageDAO.DeleteUserRole(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllCompanyUnit()
        {
            EntityData data2;
            try
            {
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.UnitType, "'公司'"));
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", builder.BuildMainQueryString());
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static int GetBudgetControlPower(string projectCode)
        {
            int num2;
            try
            {
                ProjectConfigStrategyBuilder builder = new ProjectConfigStrategyBuilder();
                builder.AddStrategy(new Strategy(ProjectConfigStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ProjectConfig", queryString);
                agent.Dispose();
                DataRow[] rowArray = data.CurrentTable.Select(string.Format(" ConfigName='BudgetControl'", new object[0]));
                int num = 2;
                if ((rowArray.Length > 0) && !rowArray[0].IsNull("ConfigData"))
                {
                    num = int.Parse((string) rowArray[0]["ConfigData"]);
                }
                data.Dispose();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetFunctionStructureName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData functionStructureByCode = SystemManageDAO.GetFunctionStructureByCode(code);
                if (functionStructureByCode.HasRecord())
                {
                    text = functionStructureByCode.GetString("FunctionStructureName");
                }
                functionStructureByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetFunctionStructureName(string code, string usercode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData functionStructureByCode = SystemManageDAO.GetFunctionStructureByCode(code);
                if (functionStructureByCode.HasRecord())
                {
                    text = functionStructureByCode.GetString("FunctionStructureName");
                }
                functionStructureByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetLastProjectCode(string userCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemUserByCode = SystemManageDAO.GetSystemUserByCode(userCode);
                if (systemUserByCode.HasRecord())
                {
                    text = systemUserByCode.GetString("LastProjectCode");
                }
                systemUserByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetNewUnitListByUserCode(string UserCode)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable();
                DataColumn column = new DataColumn();
                tb.Columns.Add("UnitCode");
                tb.Columns.Add("UnitName");
                EntityData userRoleByUserCode = SystemManageDAO.GetUserRoleByUserCode(UserCode);
                DataRow row = tb.NewRow();
                row["UnitCode"] = "";
                row["UnitName"] = "请选择";
                tb.Rows.Add(row);
                foreach (DataRow row2 in userRoleByUserCode.CurrentTable.Rows)
                {
                    EntityData stationByCode = OBSDAO.GetStationByCode(row2["StationCode"].ToString());
                    if (stationByCode.HasRecord())
                    {
                        DataRow row3 = tb.NewRow();
                        row3["UnitCode"] = stationByCode.GetString("UnitCode");
                        row3["UnitName"] = GetUnitName(stationByCode.GetString("UnitCode"));
                        tb.Rows.Add(row3);
                    }
                    stationByCode.Dispose();
                }
                userRoleByUserCode.Dispose();
                table2 = ConvertRule.GetDistinct(tb, "UnitCode", "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetProjectCodeByRole(string roleCode)
        {
            string text = "";
            try
            {
                EntityData entitydata = new EntityData("Role");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Role", "SelectProjectCodeByRole").SqlString, "@RoleCode", roleCode, entitydata, "Role");
                }
                if (entitydata.CurrentTable.Rows.Count > 0)
                {
                    text = entitydata.GetString("ProjectCode");
                }
                entitydata.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GetProjectConfigValue(string ConfigName)
        {
            string projectConfigValue;
            try
            {
                projectConfigValue = GetProjectConfigValue("", ConfigName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return projectConfigValue;
        }

        public static string GetProjectConfigValue(string ProjectCode, string ConfigName)
        {
            string text3;
            try
            {
                string text = "";
                ProjectConfigStrategyBuilder builder = new ProjectConfigStrategyBuilder();
                builder.AddStrategy(new Strategy(ProjectConfigStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ProjectConfigStrategyName.ConfigName, ConfigName));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("ProjectConfig", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("ConfigData");
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

        public static string GetProjectUnitCode(string projectCode)
        {
            string text3;
            try
            {
                string text = "";
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.RelaCode, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                if (data.HasRecord())
                {
                    text = data.GetString("UnitCode");
                }
                agent.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetRoleLevelName(int roleLevel)
        {
            switch (roleLevel)
            {
                case 0:
                    return "集团";

                case 1:
                    return "公司";

                case 2:
                    return "项目";

                case 3:
                    return "部门";

                case 4:
                    return "个人";
            }
            return "";
        }

        public static string GetRoleName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData roleByCode = SystemManageDAO.GetRoleByCode(code);
                if (roleByCode.HasRecord())
                {
                    text = roleByCode.GetString("RoleName");
                }
                roleByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetShortUserName(string userCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemUserByCode = SystemManageDAO.GetSystemUserByCode(userCode);
                if (systemUserByCode.HasRecord())
                {
                    text = systemUserByCode.GetString("ShortUserName");
                }
                systemUserByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetStationByUserCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData userRoleByUserCode = SystemManageDAO.GetUserRoleByUserCode(code);
                foreach (DataRow row in userRoleByUserCode.CurrentTable.Rows)
                {
                    text = text + row["StationCode"].ToString();
                }
                userRoleByUserCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetStationListByUserCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData userRoleByUserCode = SystemManageDAO.GetUserRoleByUserCode(code);
                foreach (DataRow row in userRoleByUserCode.CurrentTable.Rows)
                {
                    text = text + row["StationCode"].ToString() + ",";
                }
                userRoleByUserCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetStationName(string stationCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData stationByCode = OBSDAO.GetStationByCode(stationCode);
                if (stationByCode.HasRecord())
                {
                    text = stationByCode.GetString("StationName");
                }
                stationByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetStationNameEx(string stationCode)
        {
            string text4;
            try
            {
                string text = "";
                EntityData stationByCode = OBSDAO.GetStationByCode(stationCode);
                if (stationByCode.HasRecord())
                {
                    text = GetUnitFullName(stationByCode.GetString("UnitCode")) + "->" + stationByCode.GetString("StationName");
                }
                stationByCode.Dispose();
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static string GetStationNames(string stationCodes)
        {
            string text3;
            try
            {
                if (stationCodes.Length < 1)
                {
                    return "";
                }
                string text = "";
                string[] textArray = stationCodes.Split(new char[] { ',' });
                foreach (string text2 in textArray)
                {
                    text = text + "," + GetStationName(text2);
                }
                if (text.Length > 1)
                {
                    text = text.Substring(1);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static void GetStationUserGroup(DataView dv, string TypeField, string CodeField, string UserFlag, string StationFlag, ref string UserCodes, ref string StationCodes)
        {
            try
            {
                UserCodes = "";
                StationCodes = "";
                string[] arr = UserFlag.Split(",".ToCharArray());
                string[] textArray2 = StationFlag.Split(",".ToCharArray());
                foreach (DataRowView view in dv)
                {
                    DataRow row = view.Row;
                    string val = ConvertRule.ToString(row[TypeField]);
                    string text2 = ConvertRule.ToString(row[CodeField]);
                    if (ConvertRule.FindArray(arr, val) >= 0)
                    {
                        if (UserCodes != "")
                        {
                            UserCodes = UserCodes + ",";
                        }
                        UserCodes = UserCodes + text2;
                    }
                    else if (ConvertRule.FindArray(textArray2, val) >= 0)
                    {
                        if (StationCodes != "")
                        {
                            StationCodes = StationCodes + ",";
                        }
                        StationCodes = StationCodes + text2;
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetSupplierNameByCode(string supplierCode)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("Supplier"))
            {
                EntityData data = ydao.SelectbyPrimaryKey(supplierCode);
                if (data.HasRecord())
                {
                    return data.GetString("SupplierName");
                }
                return "";
            }
        }

        public static string GetSystemGroupFullNameByCode(string groupCode)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
            {
                EntityData data = ydao.SelectbyPrimaryKey(groupCode);
                if (data.HasRecord())
                {
                    string text = "";
                    string[] textArray = data.GetString("FullID").Split(new char[] { '-' });
                    foreach (string text3 in textArray)
                    {
                        data = ydao.SelectbyPrimaryKey(text3);
                        if (data.HasRecord())
                        {
                            text = text + " - " + data.GetString("GroupName");
                        }
                    }
                    return text;
                }
                return "";
            }
        }

        public static string GetSystemGroupNameByCode(string groupCode)
        {
            using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
            {
                EntityData data = ydao.SelectbyPrimaryKey(groupCode);
                if (data.HasRecord())
                {
                    return data.GetString("GroupName");
                }
                return "";
            }
        }

        public static string GetUnitByStationCode(string stationCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData stationByCode = OBSDAO.GetStationByCode(stationCode);
                if (stationByCode.HasRecord())
                {
                    text = stationByCode.GetString("UnitCode");
                }
                stationByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUnitFullCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData unitFullNameByUnitCode = OBSDAO.GetUnitFullNameByUnitCode(code);
                if (unitFullNameByUnitCode.HasRecord())
                {
                    text = unitFullNameByUnitCode.GetString("FullCode");
                }
                unitFullNameByUnitCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUnitFullName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData unitFullNameByUnitCode = OBSDAO.GetUnitFullNameByUnitCode(code);
                if (unitFullNameByUnitCode.HasRecord())
                {
                    text = unitFullNameByUnitCode.GetString("UnitFullName");
                }
                unitFullNameByUnitCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetUnitListByUserCode(string UserCode)
        {
            DataTable table2;
            try
            {
                DataTable tb = new DataTable();
                DataColumn column = new DataColumn();
                tb.Columns.Add("UnitCode");
                tb.Columns.Add("UnitName");
                EntityData userRoleByUserCode = SystemManageDAO.GetUserRoleByUserCode(UserCode);
                foreach (DataRow row in userRoleByUserCode.CurrentTable.Rows)
                {
                    EntityData stationByCode = OBSDAO.GetStationByCode(row["StationCode"].ToString());
                    if (stationByCode.HasRecord())
                    {
                        DataRow row2 = tb.NewRow();
                        row2["UnitCode"] = stationByCode.GetString("UnitCode");
                        row2["UnitName"] = GetUnitName(stationByCode.GetString("UnitCode"));
                        tb.Rows.Add(row2);
                    }
                    stationByCode.Dispose();
                }
                userRoleByUserCode.Dispose();
                table2 = ConvertRule.GetDistinct(tb, "UnitCode", "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetUnitName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData unitByCode = OBSDAO.GetUnitByCode(code);
                if (unitByCode.HasRecord())
                {
                    text = unitByCode.GetString("UnitName");
                }
                unitByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUnitParentSpecailUnitCode(string unitCode, string targetUnitType, ref string targetUnitName, ref string targetUnitFullCode)
        {
            string text7;
            try
            {
                string text = "";
                string text2 = unitCode;
                EntityData allUnit = OBSDAO.GetAllUnit();
                while (text2 != "")
                {
                    DataRow[] rowArray = allUnit.CurrentTable.Select(string.Format("UnitCode='{0}'", text2));
                    if (rowArray.Length > 0)
                    {
                        string text3 = rowArray[0]["UnitType"].ToString();
                        string text4 = rowArray[0]["ParentUnitCode"].ToString();
                        string text5 = rowArray[0]["UnitName"].ToString();
                        string text6 = rowArray[0]["FullCode"].ToString();
                        if (text3 == targetUnitType)
                        {
                            text = text2;
                            targetUnitName = text5;
                            targetUnitFullCode = text6;
                            break;
                        }
                        text2 = text4;
                    }
                    else
                    {
                        text2 = "";
                    }
                }
                allUnit.Dispose();
                text7 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text7;
        }

        public static string GetUnitU8Code(string UnitCode)
        {
            string text2;
            try
            {
                string text = "";
                if (UnitCode == "")
                {
                    return text;
                }
                EntityData unitByCode = OBSDAO.GetUnitByCode(UnitCode);
                if (unitByCode.HasRecord())
                {
                    text = unitByCode.GetString("U8Code");
                }
                unitByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetUnitUnderProject(string projectCode)
        {
            EntityData data2;
            try
            {
                string projectUnitCode = GetProjectUnitCode(projectCode);
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.ParentUnitCode, projectUnitCode));
                builder.AddOrder("FullCode", true);
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", builder.BuildMainQueryString());
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserByRole(string roleCode)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.RoleCode, roleCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserByStation(string stationCode)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.StationCode, stationCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetUserCodeByUserName(string pm_sUserName)
        {
            string text2;
            try
            {
                string text = "";
                EntityData userByUserName = SystemManageDAO.GetUserByUserName(pm_sUserName);
                if (userByUserName.HasRecord())
                {
                    text = userByUserName.GetString("UserCode");
                }
                userByUserName.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUserMailByCode(string userCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemUserByCode = SystemManageDAO.GetSystemUserByCode(userCode);
                if (systemUserByCode.HasRecord())
                {
                    text = systemUserByCode.GetString("MailBox");
                }
                systemUserByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUserMobileByCode(string userCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemUserByCode = SystemManageDAO.GetSystemUserByCode(userCode);
                if (systemUserByCode.HasRecord())
                {
                    text = systemUserByCode.GetString("Mobile");
                }
                systemUserByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUserName(string userCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemUserByCode = SystemManageDAO.GetSystemUserByCode(userCode);
                if (systemUserByCode.HasRecord())
                {
                    text = systemUserByCode.GetString("UserName");
                }
                systemUserByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUserNameByProjectCode(string userCode, string ProjectCode, EntityData ProjectData)
        {
            string text3;
            try
            {
                string shortUserName = "";
                bool flag = false;
                if (ProjectData != null)
                {
                    foreach (DataRow row in ProjectData.CurrentTable.Select())
                    {
                        if (row["ProjectCode"].ToString() == ProjectCode)
                        {
                            flag = ProjectRule.IsUseShortNameByValue(row["IsUseShortName"].ToString());
                        }
                    }
                }
                else
                {
                    flag = ProjectRule.IsUseShortNameByProjectCode(ProjectCode);
                }
                if (flag)
                {
                    shortUserName = GetShortUserName(userCode);
                    switch (shortUserName)
                    {
                        case null:
                        case "":
                            shortUserName = GetUserName(userCode);
                            break;
                    }
                }
                else
                {
                    shortUserName = GetUserName(userCode);
                }
                text3 = shortUserName;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetUserNameByProjectCode(string ProjectCode, string UserName, string UserShortName, EntityData ProjectData)
        {
            string text3;
            try
            {
                string text = "";
                bool flag = false;
                if (ProjectData != null)
                {
                    foreach (DataRow row in ProjectData.CurrentTable.Select())
                    {
                        if (row["ProjectCode"].ToString() == ProjectCode)
                        {
                            flag = ProjectRule.IsUseShortNameByValue(row["IsUseShortName"].ToString());
                        }
                    }
                }
                else
                {
                    flag = ProjectRule.IsUseShortNameByProjectCode(ProjectCode);
                }
                if (flag)
                {
                    text = UserShortName;
                    switch (text)
                    {
                        case null:
                        case "":
                            text = UserName;
                            break;
                    }
                }
                else
                {
                    text = UserName;
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetUserOrStationName(string code, int type)
        {
            if (type == 0)
            {
                return GetUserName(code);
            }
            return GetStationName(code);
        }

        public static EntityData GetUsersByProject(string projectCode)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUsersByUnit(string unitCode)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.UnitCode, unitCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUsersByUnitEx(string unitCode)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.UnitCodeEx, unitCode));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUsersNoStation()
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.NoStation));
                string queryString = builder.BuildMainQueryString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetUserStationNameHtml(string userCode)
        {
            string text7;
            try
            {
                EntityData stationByUserCode = OBSDAO.GetStationByUserCode(userCode);
                string text = "";
                int count = stationByUserCode.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    stationByUserCode.SetCurrentRow(i);
                    string text2 = stationByUserCode.GetString("StationCode");
                    string unitFullName = GetUnitFullName(stationByUserCode.GetString("UnitCode"));
                    string text4 = stationByUserCode.GetString("unitCode");
                    string text5 = stationByUserCode.GetString("StationName");
                    string text6 = "../Systems/StationInfo.aspx?StationCode=" + text2 + "&UnitCode=" + text4 + "";
                    string text8 = text;
                    text = text8 + "<a style=\"CURSOR: hand\" onclick=OpenMyStaionSetWindow(\"" + text6 + "\")>" + unitFullName + "->" + text5 + "</a><br>";
                }
                stationByUserCode.Dispose();
                text7 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text7;
        }

        public static void UpdateProjectConfigValue(string ConfigName, object ConfigData)
        {
            try
            {
                UpdateProjectConfigValue("", ConfigName, ConfigData);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateProjectConfigValue(string ProjectCode, string ConfigName, object ConfigData)
        {
            try
            {
                DataRow currentRow;
                ProjectConfigStrategyBuilder builder = new ProjectConfigStrategyBuilder();
                builder.AddStrategy(new Strategy(ProjectConfigStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ProjectConfigStrategyName.ConfigName, ConfigName));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData entity = agent.FillEntityData("ProjectConfig", queryString);
                agent.Dispose();
                if (entity.HasRecord())
                {
                    currentRow = entity.CurrentRow;
                }
                else
                {
                    currentRow = entity.CurrentTable.NewRow();
                    currentRow["ProjectConfigCode"] = SystemManageDAO.GetNewSysCode("ProjectConfigCode");
                    currentRow["ProjectCode"] = ProjectCode;
                    currentRow["ConfigName"] = ConfigName;
                    entity.CurrentTable.Rows.Add(currentRow);
                }
                currentRow["ConfigData"] = ConfigData;
                SystemManageDAO.SubmitAllProjectConfig(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateUserPwd(string UserCode, string NewPwd, string OwnName)
        {
            try
            {
                EntityData entity = SystemManageDAO.GetSystemUserByCode(UserCode);
                try
                {
                    if (entity.HasRecord())
                    {
                        DataRow currentRow = entity.CurrentRow;
                        if (NewPwd != null)
                        {
                            currentRow["Password"] = NewPwd;
                        }
                        if (OwnName != null)
                        {
                            currentRow["OwnName"] = OwnName;
                        }
                        SystemManageDAO.UpdateSystemUser(entity);
                    }
                }
                finally
                {
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

