namespace RmsPM.DAL.EntityDAO
{
    using System;
    using System.Data;
    using System.Text;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class SystemManageDAO
    {
        public static void DeleteAllRow(DataTable dt)
        {
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                dt.Rows[i].Delete();
            }
        }

        public static void DeleteDictionaryItem(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteDictionaryName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteFunctionStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePeriodDefine(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteProjectConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteRoleOperation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_DictionaryName(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_DictionaryName"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Role(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Role"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_SystemUser(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SystemUser"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteSysCode(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteSystemGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteSystemUser(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteSystemUserSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteUserRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllDictionaryItem()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllDictionaryName()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllFunctionStructure()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllPeriodDefine()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllProjectConfig()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRole()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRoleOperation()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllSysCode()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllSystemGroup()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllSystemUser()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllSystemUserSubjectSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllUserRole()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByName(string dictionaryName)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DictionaryItem");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryItem", "SelectByDictionaryName").GetSqlStringWithOrder(), "@Name", dictionaryName, entitydata, "DictionaryItem");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByNameCodeProject(string dictionaryNameCode, string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DictionaryItem");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@DictionaryNameCode" };
                    object[] values = new object[] { projectCode, dictionaryNameCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryItem", "SelectByDictionaryNameCodeProject").GetSqlStringWithOrder(), Params, values, entitydata, "DictionaryItem");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByNameItem(string dictionaryName, string dictionaryItem)
        {
            EntityData data2;
            try
            {
                DictionaryItemStrategyBuilder builder = new DictionaryItemStrategyBuilder();
                builder.AddStrategy(new Strategy(DictionaryItemStrategyName.DictionaryName, dictionaryName));
                builder.AddStrategy(new Strategy(DictionaryItemStrategyName.Name, dictionaryItem));
                string queryString = builder.BuildQueryViewString();
                data2 = new QueryAgent().FillEntityData("DictionaryItem", queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByNameProject(string dictionaryName, string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DictionaryItem");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@Name" };
                    object[] values = new object[] { projectCode, dictionaryName };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryItem", "SelectByDictionaryNameProject").GetSqlStringWithOrder(), Params, values, entitydata, "DictionaryItem");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryItemByProjectCode(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DictionaryItem");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    string[] Params = new string[] { "@ProjectCode" };
                    object[] values = new object[] { projectCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryItem", "SelectByProjectCode").SqlString, Params, values, entitydata, "DictionaryItem");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryNameByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetDictionaryNameByProjectCode(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("DictionaryName");
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryName", "SelectByProjectCode").SqlString, "@ProjectCode", projectCode, entitydata, "DictionaryName");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetFormatSysCode(string SysCodeName, string format)
        {
            return GetFormatSysCode(SysCodeName, format, 1);
        }

        public static string GetFormatSysCode(string SysCodeName, string format, int beginnum)
        {
            try
            {
                int startIndex = 0;
                int num2 = -1;
                int num3 = -1;
                int index = -1;
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                string text = "";
                string text2 = "";
                string text3 = "";
                string text4 = "[Number]";
                string newValue = "@@@@@";
                string text6 = "$$$$$";
                StringBuilder builder = new StringBuilder(format);
                while (num6 < 100)
                {
                    num6++;
                    Console.WriteLine(builder.ToString());
                    startIndex = builder.ToString().IndexOf('{');
                    if ((startIndex < 0) || (startIndex == (builder.Length - 1)))
                    {
                        break;
                    }
                    startIndex++;
                    index = startIndex - 1;
                    num5 = builder.ToString().IndexOf("}", startIndex);
                    num7 = builder.ToString().IndexOf("{", startIndex);
                    if (num5 <= 0)
                    {
                        num5 = startIndex;
                    }
                    switch (builder.ToString(startIndex, 1))
                    {
                        case "#":
                            num2 = index;
                            num3 = num5;
                            builder = builder.Remove(index, (num5 - index) + 1).Insert(index, text4);
                            break;

                        case "Y":
                        {
                            int num8 = (num5 - index) - 1;
                            builder = builder.Remove(index, (num5 - index) + 1).Insert(index, DateTime.Today.Year.ToString().Substring(4 - num8));
                            text = DateTime.Today.Year.ToString();
                            break;
                        }
                        case "M":
                            builder = builder.Remove(index, (num5 - index) + 1).Insert(index, DateTime.Today.Month.ToString().PadLeft(2, '0'));
                            text2 = DateTime.Today.Month.ToString().PadLeft(2, '0');
                            break;

                        case "D":
                            builder = builder.Remove(index, (num5 - index) + 1).Insert(index, DateTime.Today.Day.ToString().PadLeft(2, '0'));
                            text3 = DateTime.Today.Day.ToString().PadLeft(2, '0');
                            break;

                        default:
                            builder = builder.Replace("}", text6, num5, 1).Replace("{", newValue, index, 1);
                            break;
                    }
                }
                return builder.Replace(text4, getFormatSysCodeFromDatabase(SysCodeName, (num3 - num2) - 1, beginnum)).Replace(newValue, "{").Replace(text6, "}").ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string getFormatSysCodeFromDatabase(string name, int length, int beginnum)
        {
            int num = 0;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    EntityData entity = ydao.SelectbyPrimaryKey(name);
                    if (!entity.HasRecord())
                    {
                        DataRow newRecord = entity.GetNewRecord();
                        newRecord["CodeName"] = name;
                        newRecord["CodeValue"] = beginnum;
                        entity.AddNewRecord(newRecord);
                        InsertSysCode(entity);
                        num = beginnum;
                    }
                    else
                    {
                        int num2 = entity.GetInt("CodeValue") + 1;
                        entity.CurrentRow["CodeValue"] = num2;
                        UpdateSysCode(entity);
                        num = num2;
                    }
                    entity.Dispose();
                }
            }
            catch (Exception)
            {
                num = beginnum;
            }
            return num.ToString().PadLeft(length, '0');
        }

        public static EntityData GetFunctionStructureByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetFunctionStructureByParentCode(string parentCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("FunctionStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("FunctionStructure", "SelectByParentCode").SqlString, "@ParentCode", parentCode, entitydata, "FunctionStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetFunctionStructureParentCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData functionStructureByCode = GetFunctionStructureByCode(code);
                if (functionStructureByCode.HasRecord())
                {
                    text = functionStructureByCode.GetString("ParentCode");
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

        public static string GetNewSysCode(string name)
        {
            string text3;
            try
            {
                string text = "";
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    int num4;
                    EntityData entity = ydao.SelectbyPrimaryKey(name);
                    int num = 0x186a0;
                    if (!entity.HasRecord())
                    {
                        DataRow newRecord = entity.GetNewRecord();
                        newRecord["CodeName"] = name;
                        newRecord["CodeValue"] = num + 1;
                        entity.AddNewRecord(newRecord);
                        InsertSysCode(entity);
                        text = (num + 1).ToString();
                        goto Label_021D;
                    }
                    string text2 = entity.GetString("CodeRule");
                    int @int = entity.GetInt("CodeValue");
                    if (@int.ToString().Length <= 0)
                    {
                        goto Label_01E9;
                    }
                    if (text2.Length <= 0)
                    {
                        goto Label_01D6;
                    }
                    string[] textArray = text2.Split("+".ToCharArray());
                    if (textArray.Length <= 1)
                    {
                        goto Label_0166;
                    }
                    text = textArray[0];
                    if (textArray[1].Length <= 0)
                    {
                        goto Label_014F;
                    }
                    int num3 = 0;
                    goto Label_0123;
                Label_010F:
                    text = text + "0";
                    num3++;
                Label_0123:
                    num4 = @int + 1;
                    if (num3 <= ((textArray[1].Length - num4.ToString().Length) - 1))
                    {
                        goto Label_010F;
                    }
                Label_014F:
                    num4 = @int + 1;
                    text = text + num4.ToString();
                    goto Label_01FB;
                Label_0166:
                    if (text2.Length <= 0)
                    {
                        goto Label_01BE;
                    }
                    num3 = 0;
                    goto Label_0194;
                Label_0180:
                    text = text + "0";
                    num3++;
                Label_0194:
                    num4 = @int + 1;
                    if (num3 <= ((text2.Length - num4.ToString().Length) - 1))
                    {
                        goto Label_0180;
                    }
                Label_01BE:
                    num4 = @int + 1;
                    text = text + num4.ToString();
                    goto Label_01FB;
                Label_01D6:
                    num4 = @int + 1;
                    text = num4.ToString();
                    goto Label_01FB;
                Label_01E9:
                    num4 = @int + 1;
                    text = text = num4.ToString();
                Label_01FB:
                    entity.CurrentRow["CodeValue"] = @int + 1;
                    UpdateSysCode(entity);
                Label_021D:
                    entity.Dispose();
                    text3 = text;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static EntityData GetPeriodDefineByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPeriodDefineByProjectCode(string projectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PeriodDefine");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("PeriodDefine", "SelectByProjectCode").SqlString, "@ProjectCode", projectCode, entitydata, "PeriodDefine");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetProjectConfigByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoleByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoleByProjectCode(string projectCode)
        {
            EntityData data2;
            try
            {
                RoleStrategyBuilder builder = new RoleStrategyBuilder();
                builder.AddStrategy(new Strategy(RoleStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Role", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoleByProjectCodeEx(string projectCode)
        {
            EntityData data2;
            try
            {
                RoleStrategyBuilder builder = new RoleStrategyBuilder();
                builder.AddStrategy(new Strategy(RoleStrategyName.ProjectCodeEx, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Role", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoleOperationByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_DictionaryNameByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_DictionaryName");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_DictionaryName"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryName", "Select").GetSqlStringWithOrder(), "@DictionaryNameCode", code, entitydata, "DictionaryName");
                    ydao.FillEntity(SqlManager.GetSqlStruct("DictionaryItem", "SelectByDictionaryNameCode").GetSqlStringWithOrder(), "@DictionaryNameCode", code, entitydata, "DictionaryItem");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_RoleByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Role");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Role"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Role", "Select").SqlString, "@RoleCode", code, entitydata, "Role");
                    ydao.FillEntity(SqlManager.GetSqlStruct("RoleOperation", "SelectByRoleCode").SqlString, "@RoleCode", code, entitydata, "RoleOperation");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_SystemUserByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_SystemUser");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SystemUser"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("SystemUser", "Select").SqlString, "@UserCode", code, entitydata, "SystemUser");
                    ydao.FillEntity(SqlManager.GetSqlStruct("UserRole", "SelectByUserCode").SqlString, "@UserCode", code, entitydata, "UserRole");
                    ydao.FillEntity(SqlManager.GetSqlStruct("SystemUserSubjectSet", "SelectByUserCode").SqlString, "@UserCode", code, entitydata, "SystemUserSubjectSet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static DataSet GetSumContract()
        {
            SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup");
            string queryString = SqlManager.GetSqlStruct("SystemGroup", "SelectSumContract").SqlString;
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("V_PayoutItem", queryString);
            agent.Dispose();
            return data;
        }

        public static EntityData GetSysCodeByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetSysCodeByName(string name)
        {
            string text;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    text = ydao.SelectbyPrimaryKey(name).GetInt("CodeValue").ToString();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static EntityData GetSystemGroupAllChildByParentFullID(string ParentFullID)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.AllChild, ParentFullID));
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemGroupByClassCode(string ClassCode)
        {
            return GetSystemGroupByClassCode(ClassCode, "");
        }

        public static EntityData GetSystemGroupByClassCode(string ClassCode, string projectName)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.ClassCode, ClassCode));
                if (projectName != "")
                {
                }
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemGroupByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemGroupByGroupName(string GroupName, string ClassCode)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.ClassCode, ClassCode));
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.GroupName, GroupName));
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemGroupByParentCode(string ParentCode)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.ParentCode, ParentCode));
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemGroupBySortID(string SortID, string ClassCode)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.ClassCode, ClassCode));
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.SortID, SortID));
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetSystemGroupFullID(string code)
        {
            string text2;
            if (code == "")
            {
                return "";
            }
            try
            {
                string text = "";
                EntityData systemGroupByCode = GetSystemGroupByCode(code);
                if (systemGroupByCode.HasRecord())
                {
                    text = systemGroupByCode.GetString("FullID");
                }
                systemGroupByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetSystemGroupIncludeAllChildByParentFullID(string ParentFullID)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.IncludeAllChild, ParentFullID));
                string queryString = builder.BuildQueryViewString() + builder.GetDefaultOrder();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetSystemGroupSortID(string code)
        {
            string text2;
            if (code == "")
            {
                return "";
            }
            try
            {
                string text = "";
                EntityData systemGroupByCode = GetSystemGroupByCode(code);
                if (systemGroupByCode.HasRecord())
                {
                    text = systemGroupByCode.GetString("SortID");
                }
                systemGroupByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetSystemUserByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemUserBySortID(string SortID)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.SortID, SortID));
                string queryString = builder.BuildMainQueryString();
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

        public static EntityData GetSystemUserByUserID(string UserID)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.UserID, UserID));
                string queryString = builder.BuildMainQueryString();
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

        public static EntityData GetSystemUserByUserName(string UserName)
        {
            EntityData data2;
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.UserName, UserName));
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

        public static EntityData GetSystemUserSubjectSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemUserSubjectSetBySubjectSet(string SubjectSetCode)
        {
            EntityData data2;
            try
            {
                SystemUserSubjectSetStrategyBuilder builder = new SystemUserSubjectSetStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemUserSubjectSetStrategyName.SubjectSetCode, SubjectSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUserSubjectSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSystemUserSubjectSetByUser(string UserCode, string SubjectSetCode)
        {
            EntityData data2;
            try
            {
                SystemUserSubjectSetStrategyBuilder builder = new SystemUserSubjectSetStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemUserSubjectSetStrategyName.UserCode, UserCode));
                builder.AddStrategy(new Strategy(SystemUserSubjectSetStrategyName.SubjectSetCode, SubjectSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUserSubjectSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUnitByUserCode(string userCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Unit");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Unit"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Unit", "GetUnitByUserCode").SqlString, "@userCode", userCode, entitydata, "Unit");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserByUserName(string pm_sUserName)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("SystemUser");
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("SystemUser", "SelectByUserName").SqlString, "@UserName", pm_sUserName, entitydata, "SystemUser");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserRoleByCode(string userCode, string roleCode)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    data = ydao.SelectbyPrimaryKey(new object[] { userCode, roleCode });
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserRoleByStationCode(string stationCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("StationRole");
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("UserRole", "SelectByStationRole").SqlString, "@StationRole", stationCode, entitydata, "UserRole");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetUserRoleByUserCode(string userCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("UserRole");
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("UserRole", "SelectByUserCode").SqlString, "@UserCode", userCode, entitydata, "UserRole");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_SystemGroupByCode(string GroupCode)
        {
            EntityData data2;
            try
            {
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.GroupCode, GroupCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertDictionaryItem(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertDictionaryName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertFunctionStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPeriodDefine(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertProjectConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertRoleOperation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_DictionaryName(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_DictionaryName"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Role(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Role"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_SystemUser(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SystemUser"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSysCode(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSystemGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSystemUser(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertSystemUserSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertUserRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllFunctionStructure(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllPeriodDefine(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllProjectConfig(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllRoleOperation(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllStandard_DictionaryName(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_DictionaryName"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllStandard_Role(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Role"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllStandard_SystemUser(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SystemUser"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllSystemGroup(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllSystemUserSubjectSet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void SubmitAllUserRole(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
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

        public static void UpdateDictionaryItem(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryItem"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateDictionaryName(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("DictionaryName"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateFunctionStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("FunctionStructure"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePeriodDefine(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PeriodDefine"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateProjectConfig(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ProjectConfig"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Role"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRoleOperation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("RoleOperation"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_DictionaryName(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_DictionaryName"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Role(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Role"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_SystemUser(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_SystemUser"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSysCode(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SysCode"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSystemGroup(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemGroup"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSystemUser(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUser"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateSystemUserSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("SystemUserSubjectSet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateUserRole(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("UserRole"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

