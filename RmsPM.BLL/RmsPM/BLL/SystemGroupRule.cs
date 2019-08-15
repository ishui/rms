namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SystemGroupRule
    {
        private static DataRow AccessRangeNewRow(DataTable tb, string AccessRangeType, string RelationCode, string OperationCode, string GroupCode, string RoleLevel, string ResourceCode)
        {
            DataRow row2;
            try
            {
                DataRow row = tb.NewRow();
                row["AccessRangeCode"] = SystemManageDAO.GetNewSysCode("AccessRangeCode");
                row["AccessRangeType"] = AccessRangeType;
                row["RelationCode"] = RelationCode;
                row["ResourceCode"] = ResourceCode;
                row["GroupCode"] = GroupCode;
                row["UnitCode"] = "";
                row["OperationCode"] = OperationCode;
                row["RoleLevel"] = ConvertRule.ToInt(RoleLevel);
                tb.Rows.Add(row);
                row2 = row;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return row2;
        }

        public static void AddSystemGroupAccessRelationImage(DataTable tb)
        {
            try
            {
                if (!tb.Columns.Contains("AccessRangeTypeImageName"))
                {
                    tb.Columns.Add("AccessRangeTypeImageName");
                }
                foreach (DataRow row in tb.Rows)
                {
                    SetSystemGroupAccessRelationImage(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void AddSystemGroupAccessRelationName(DataTable tb)
        {
            try
            {
                if (!tb.Columns.Contains("RelationName"))
                {
                    tb.Columns.Add("RelationName");
                }
                foreach (DataRow row in tb.Rows)
                {
                    SetSystemGroupAccessRelationName(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string CheckDeleteSystemGroup(string GroupCode)
        {
            string text13;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(GroupCode);
                if (systemGroupByCode.HasRecord())
                {
                    string classCode = systemGroupByCode.GetString("ClassCode");
                    string parentFullID = systemGroupByCode.GetString("FullID");
                    string itemInfoByClassCode = SystemClassDescription.GetItemInfoByClassCode(classCode);
                    if (itemInfoByClassCode != "")
                    {
                        EntityData systemGroupIncludeAllChildByParentFullID = SystemManageDAO.GetSystemGroupIncludeAllChildByParentFullID(parentFullID);
                        string text5 = "";
                        foreach (DataRow row in systemGroupIncludeAllChildByParentFullID.CurrentTable.Rows)
                        {
                            string text6 = row["GroupCode"].ToString();
                            if (text5 != "")
                            {
                                text5 = text5 + ",";
                            }
                            text5 = text5 + "'" + text6 + "'";
                        }
                        string[] textArray = itemInfoByClassCode.Split(",".ToCharArray());
                        foreach (string text7 in textArray)
                        {
                            if (text7 != "")
                            {
                                string[] textArray2 = text7.Split("|".ToCharArray());
                                string text8 = textArray2[0];
                                string text9 = textArray2[1];
                                string queryString = string.Format("select top 1 {1} from {0} where {1} in ({2})", text8, text9, text5);
                                QueryAgent agent = new QueryAgent();
                                try
                                {
                                    try
                                    {
                                        string code = ConvertRule.ToString(agent.ExecuteScalar(queryString));
                                        if (code != "")
                                        {
                                            EntityData data3 = SystemManageDAO.GetSystemGroupByCode(code);
                                            string text12 = data3.GetString("SortID") + " " + data3.GetString("GroupName");
                                            data3.Dispose();
                                            return string.Format("表 {0} 中已存在类别为 {1} 的记录，不能删除该类别", text8, text12);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                finally
                                {
                                    agent.Dispose();
                                }
                            }
                        }
                    }
                }
                systemGroupByCode.Dispose();
                text13 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text13;
        }

        public static void CopySystemGroup(string srcGroupCode, string dstGroupCode, string dstClassCode)
        {
            try
            {
                if (srcGroupCode == "")
                {
                    throw new Exception("未传入源结点号");
                }
                if (dstClassCode == "")
                {
                    throw new Exception("未传入大类代码");
                }
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(srcGroupCode);
                if (!systemGroupByCode.HasRecord())
                {
                    throw new Exception("源结点不存在");
                }
                DataRow drSrc = systemGroupByCode.CurrentRow;
                string parentFullID = ConvertRule.ToString(drSrc["FullID"]);
                DataRow drDst = null;
                if (dstGroupCode.Length > 0)
                {
                    EntityData data2 = SystemManageDAO.GetSystemGroupByCode(dstGroupCode);
                    if (data2.HasRecord())
                    {
                        drDst = data2.CurrentRow;
                    }
                    data2.Dispose();
                }
                EntityData entity = new EntityData("SystemGroup");
                DataTable tb = entity.CurrentTable;
                DataRow row3 = CopySystemGroupNewRow(tb, drSrc, drDst, dstClassCode);
                EntityData systemGroupAllChildByParentFullID = SystemManageDAO.GetSystemGroupAllChildByParentFullID(parentFullID);
                systemGroupAllChildByParentFullID.CurrentTable.Columns.Add("DstGroupCode");
                DataView view = new DataView(systemGroupAllChildByParentFullID.CurrentTable, "", "deep", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    DataRow row = view2.Row;
                    string text2 = row["ParentCode"].ToString();
                    DataRow[] rowArray = systemGroupAllChildByParentFullID.CurrentTable.Select("GroupCode='" + text2 + "'");
                    DataRow row5 = null;
                    if (rowArray.Length > 0)
                    {
                        string text3 = ConvertRule.ToString(rowArray[0]["DstGroupCode"]);
                        row5 = tb.Select("GroupCode='" + text3 + "'")[0];
                    }
                    else
                    {
                        row5 = row3;
                    }
                    DataRow row6 = CopySystemGroupNewRow(tb, row, row5, dstClassCode);
                    row["DstGroupCode"] = row6["GroupCode"];
                }
                systemGroupAllChildByParentFullID.Dispose();
                SystemManageDAO.SubmitAllSystemGroup(entity);
                entity.Dispose();
                systemGroupByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataRow CopySystemGroupNewRow(DataTable tb, DataRow drSrc, DataRow drDst, string dstClassCode)
        {
            DataRow row3;
            try
            {
                string text = "";
                int num = 0;
                string text2 = "";
                if (drDst != null)
                {
                    text = ConvertRule.ToString(drDst["GroupCode"]);
                    num = ConvertRule.ToInt(drDst["Deep"]);
                    text2 = ConvertRule.ToString(drDst["FullID"]);
                }
                DataRow row = tb.NewRow();
                int num2 = num + 1;
                string groupCode = SystemManageDAO.GetNewSysCode("SystemGroupCode");
                string text4 = text2;
                if (text4.Length > 0)
                {
                    text4 = text4 + "-" + groupCode;
                }
                else
                {
                    text4 = groupCode;
                }
                row["GroupCode"] = groupCode;
                row["GroupName"] = drSrc["GroupName"];
                row["SortID"] = drSrc["SortID"];
                row["ClassCode"] = dstClassCode;
                row["deep"] = num2;
                row["FullID"] = text4;
                row["ParentCode"] = text;
                tb.Rows.Add(row);
                EntityData entity = ResourceDAO.GetAccessRangeByGroupCode(drSrc[0].ToString());
                DataTable table = entity.Tables["AccessRange"];
                for (int i = table.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row2 = table.Rows[i];
                    AccessRangeNewRow(table, row2["AccessRangeType"].ToString(), row2["RelationCode"].ToString(), row2["OperationCode"].ToString(), groupCode, row2["RoleLevel"].ToString(), row2["ResourceCode"].ToString());
                }
                ResourceDAO.SubmitAllAccessRange(entity);
                row3 = row;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return row3;
        }

        public static void DeleteSystemGroup(string GroupCode)
        {
            Exception exception;
            try
            {
                if (GroupCode != "")
                {
                    string message = CheckDeleteSystemGroup(GroupCode);
                    if (message != "")
                    {
                        throw new Exception(message);
                    }
                    EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(GroupCode);
                    if (systemGroupByCode.HasRecord())
                    {
                        string keyvalues;
                        string parentFullID = systemGroupByCode.GetString("FullID");
                        DataTable table = new DataTable();
                        table.Columns.Add("AccessRangeCode");
                        EntityData systemGroupIncludeAllChildByParentFullID = SystemManageDAO.GetSystemGroupIncludeAllChildByParentFullID(parentFullID);
                        foreach (DataRow row in systemGroupIncludeAllChildByParentFullID.CurrentTable.Rows)
                        {
                            EntityData accessRangeByGroupCode = ResourceDAO.GetAccessRangeByGroupCode(row["GroupCode"].ToString());
                            foreach (DataRow row2 in accessRangeByGroupCode.CurrentTable.Rows)
                            {
                                keyvalues = row2["AccessRangeCode"].ToString();
                                DataRow row3 = table.NewRow();
                                row3["AccessRangeCode"] = keyvalues;
                                table.Rows.Add(row3);
                            }
                            accessRangeByGroupCode.Dispose();
                        }
                        using (StandardEntityDAO ydao = new StandardEntityDAO("SystemGroup"))
                        {
                            ydao.BeginTrans();
                            try
                            {
                                foreach (DataRow row3 in table.Rows)
                                {
                                    keyvalues = row3["AccessRangeCode"].ToString();
                                    ydao.EntityName = "AccessRange";
                                    EntityData entitydata = new EntityData("AccessRange");
                                    entitydata = ydao.SelectbyPrimaryKey(keyvalues);
                                    ydao.DeleteAllRow(entitydata);
                                    ydao.DeleteEntity(entitydata);
                                    entitydata.Dispose();
                                }
                                foreach (DataRow row3 in systemGroupIncludeAllChildByParentFullID.CurrentTable.Rows)
                                {
                                    string text3 = row3["GroupCode"].ToString();
                                    ydao.EntityName = "SystemGroup";
                                    EntityData data5 = new EntityData("SystemGroup");
                                    data5 = ydao.SelectbyPrimaryKey(text3);
                                    ydao.DeleteAllRow(data5);
                                    ydao.DeleteEntity(data5);
                                    data5.Dispose();
                                }
                                ydao.CommitTrans();
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
                        systemGroupIncludeAllChildByParentFullID.Dispose();
                    }
                    systemGroupByCode.Dispose();
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static DataTable GetSystemAccessDistinctRelation(DataTable tb)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("SystemID");
                table.Columns.Add("AccessRangeType");
                table.Columns.Add("RelationCode");
                int num = 0;
                foreach (DataRow row in tb.Rows)
                {
                    string text = ConvertRule.ToString(row["AccessRangeType"]);
                    string text2 = ConvertRule.ToString(row["RelationCode"]);
                    string filterExpression = string.Format("AccessRangeType='{0}' and RelationCode='{1}'", text, text2);
                    if (table.Select(filterExpression).Length == 0)
                    {
                        DataRow row2 = table.NewRow();
                        num++;
                        row2["SystemID"] = num;
                        row2["AccessRangeType"] = text;
                        row2["RelationCode"] = text2;
                        table.Rows.Add(row2);
                    }
                }
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetSystemGroupCodeByGroupName(string GroupName, string ClassCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupByGroupName = SystemManageDAO.GetSystemGroupByGroupName(GroupName, ClassCode);
                if (systemGroupByGroupName.HasRecord())
                {
                    text = systemGroupByGroupName.GetString("GroupCode");
                }
                systemGroupByGroupName.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSystemGroupCodeBySortID(string SortID, string ClassCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupBySortID = SystemManageDAO.GetSystemGroupBySortID(SortID, ClassCode);
                if (systemGroupBySortID.HasRecord())
                {
                    text = systemGroupBySortID.GetString("GroupCode");
                }
                systemGroupBySortID.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSystemGroupFullID(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(code);
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

        public static string GetSystemGroupFullName(string GroupCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSystemGroupFullName('{0}')", GroupCode));
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

        public static string GetSystemGroupFullNameEn(string GroupCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSystemGroupFullNameEn('{0}')", GroupCode));
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

        public static string GetSystemGroupFullNameEx(string GroupFullName, string ClassName)
        {
            string text2;
            try
            {
                string text = ClassName;
                if (GroupFullName != "")
                {
                    text = text + "->" + GroupFullName;
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSystemGroupFullSortID(string GroupCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    object obj2 = agent.ExecuteScalar(string.Format("select dbo.GetSystemGroupFullSortID('{0}')", GroupCode));
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

        public static string GetSystemGroupName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(code);
                if (systemGroupByCode.HasRecord())
                {
                    text = systemGroupByCode.GetString("GroupName");
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

        public static string GetSystemGroupNameByDeep(string GroupCode, int Deep)
        {
            string text3;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(GroupCode);
                if (systemGroupByCode.HasRecord())
                {
                    int @int = systemGroupByCode.GetInt("Deep");
                    string code = systemGroupByCode.GetString("ParentCode");
                    if (@int == Deep)
                    {
                        text = systemGroupByCode.GetString("GroupName");
                    }
                    else
                    {
                        while ((@int > Deep) && (code != ""))
                        {
                            EntityData data2 = SystemManageDAO.GetSystemGroupByCode(code);
                            if (data2.HasRecord())
                            {
                                @int = data2.GetInt("Deep");
                                code = data2.GetString("ParentCode");
                                if (@int == Deep)
                                {
                                    text = data2.GetString("GroupName");
                                    break;
                                }
                            }
                            data2.Dispose();
                        }
                    }
                }
                systemGroupByCode.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetSystemGroupNameEn(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(code);
                if (systemGroupByCode.HasRecord())
                {
                    text = systemGroupByCode.GetString("GroupNameEn");
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

        public static string GetSystemGroupSortIDByGroupCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(code);
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

        public static string GetSystemGroupSortIDByGroupNameAndClassCode(string pm_sGroupName, string pm_sClassCode)
        {
            string text3;
            try
            {
                string text = "";
                SystemGroupStrategyBuilder builder = new SystemGroupStrategyBuilder();
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.GroupName, pm_sGroupName));
                builder.AddStrategy(new Strategy(SystemGroupStrategyName.ClassCode, pm_sClassCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemGroup", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("SortID");
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

        public static bool IsSystemGroupLeafNode(string GroupCode)
        {
            bool flag2;
            try
            {
                EntityData systemGroupAllChildByParentFullID = SystemManageDAO.GetSystemGroupAllChildByParentFullID(GetSystemGroupFullID(GroupCode));
                bool flag = !systemGroupAllChildByParentFullID.HasRecord();
                systemGroupAllChildByParentFullID.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void MoveSystemGroup(string srcGroupCode, string dstGroupCode, string dstClassCode)
        {
            try
            {
                if (srcGroupCode == "")
                {
                    throw new Exception("未传入源结点号");
                }
                if (dstClassCode == "")
                {
                    throw new Exception("未传入大类代码");
                }
                EntityData systemGroupByCode = SystemManageDAO.GetSystemGroupByCode(srcGroupCode);
                if (!systemGroupByCode.HasRecord())
                {
                    throw new Exception("源结点不存在");
                }
                DataRow currentRow = systemGroupByCode.CurrentRow;
                string text = ConvertRule.ToString(currentRow["FullID"]);
                string text2 = ConvertRule.ToString(currentRow["ClassCode"]);
                string text3 = ConvertRule.ToString(currentRow["ParentCode"]);
                if (text2 != dstClassCode)
                {
                    throw new Exception("只能在同一大类下移动");
                }
                DataRow row2 = null;
                if (dstGroupCode.Length > 0)
                {
                    EntityData data2 = SystemManageDAO.GetSystemGroupByCode(dstGroupCode);
                    if (data2.HasRecord())
                    {
                        row2 = data2.CurrentRow;
                    }
                    data2.Dispose();
                }
                if (srcGroupCode == dstGroupCode)
                {
                    throw new Exception("无法移动：源结点和目标结点相同");
                }
                if (text3 == dstGroupCode)
                {
                    throw new Exception("无法移动：目标文件夹和源文件夹相同");
                }
                if ((row2 != null) && (ConvertRule.ToString(row2["FullID"]).IndexOf(text) == 0))
                {
                    throw new Exception("无法移动：目标文件夹是源文件夹的子文件");
                }
                EntityData entity = SystemManageDAO.GetSystemGroupIncludeAllChildByParentFullID(text);
                DataTable currentTable = entity.CurrentTable;
                DataView view = new DataView(currentTable, "", "deep", DataViewRowState.CurrentRows);
                foreach (DataRow row3 in currentTable.Rows)
                {
                    string text5 = ConvertRule.ToString(row3["GroupCode"]);
                    int num = ConvertRule.ToInt(row3["Deep"]);
                    string text6 = ConvertRule.ToString(row3["ParentCode"]);
                    DataRow drParent = null;
                    if (text5 == srcGroupCode)
                    {
                        drParent = row2;
                    }
                    else
                    {
                        DataRow[] rowArray = currentTable.Select("GroupCode='" + text6 + "'");
                        if (rowArray.Length > 0)
                        {
                            drParent = rowArray[0];
                        }
                        else
                        {
                            drParent = currentTable.Select("GroupCode='" + srcGroupCode + "'")[0];
                        }
                    }
                    SetSystemGroupParent(row3, drParent);
                }
                SystemManageDAO.SubmitAllSystemGroup(entity);
                entity.Dispose();
                systemGroupByCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetSystemAccessDistinctRelationOperationHtml(DataTable tbRela, DataTable tbOp)
        {
            try
            {
                tbRela.Columns.Add("OperationCodes");
                tbRela.Columns.Add("OperationNames");
                tbRela.Columns.Add("OperationHtml");
                foreach (DataRow row in tbRela.Rows)
                {
                    string text = ConvertRule.ToString(row["AccessRangeType"]);
                    string text2 = ConvertRule.ToString(row["RelationCode"]);
                    string filterExpression = string.Format("AccessRangeType='{0}' and RelationCode='{1}'", text, text2);
                    DataRow[] rowArray = tbOp.Select(filterExpression, "", DataViewRowState.CurrentRows);
                    string text4 = "";
                    string text5 = "";
                    string text6 = "";
                    foreach (DataRow row2 in rowArray)
                    {
                        string text7 = ConvertRule.ToString(row2["OperationCode"]);
                        string text8 = ConvertRule.ToString(row2["OperationName"]);
                        int num = ConvertRule.ToInt(row2["RoleLevel"]);
                        string text9 = ConvertRule.ToString(row2["RoleLevelName"]);
                        string text10 = "";
                        if (num != 0)
                        {
                            text10 = "<font color='red'>" + text9 + "</font>";
                        }
                        if (text4 != "")
                        {
                            text4 = text4 + ",";
                            text5 = text5 + ",";
                            text6 = text6 + ",";
                        }
                        text4 = text4 + text7;
                        text5 = text5 + text8;
                        text6 = text6 + text8 + text10;
                    }
                    row["OperationCodes"] = text4;
                    row["OperationNames"] = text5;
                    row["OperationHtml"] = text6;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetSystemGroupAccessRelationImage(DataRow dr)
        {
            try
            {
                int num = ConvertRule.ToInt(dr["AccessRangeType"]);
                string text = "";
                switch (num)
                {
                    case 0:
                        text = "user.gif";
                        break;

                    case 1:
                        text = "group.gif";
                        break;
                }
                dr["AccessRangeTypeImageName"] = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetSystemGroupAccessRelationName(DataRow dr)
        {
            try
            {
                int num = ConvertRule.ToInt(dr["AccessRangeType"]);
                string userCode = ConvertRule.ToString(dr["RelationCode"]);
                string userName = "";
                switch (num)
                {
                    case 0:
                        userName = SystemRule.GetUserName(userCode);
                        break;

                    case 1:
                        userName = SystemRule.GetStationNameEx(userCode);
                        break;
                }
                dr["RelationName"] = userName;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetSystemGroupParent(DataRow drNew, DataRow drParent)
        {
            try
            {
                string text = "";
                int num = 0;
                string text2 = "";
                if (drParent != null)
                {
                    text = ConvertRule.ToString(drParent["GroupCode"]);
                    num = ConvertRule.ToInt(drParent["Deep"]);
                    text2 = ConvertRule.ToString(drParent["FullID"]);
                }
                string text3 = drNew["GroupCode"].ToString();
                int num2 = num + 1;
                string text4 = text2;
                if (text4.Length > 0)
                {
                    text4 = text4 + "-" + text3;
                }
                else
                {
                    text4 = text3;
                }
                drNew["deep"] = num2;
                drNew["FullID"] = text4;
                drNew["ParentCode"] = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

