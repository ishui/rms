namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class FinanceRule
    {
        public static string GetBuildingSubjectSetU8Code(string BuildingCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (BuildingCode == "")
                {
                    return text;
                }
                EntityData buildingSubjectSetByBuilding = ProductDAO.GetBuildingSubjectSetByBuilding(BuildingCode, SubjectSetCode);
                if (buildingSubjectSetByBuilding.HasRecord())
                {
                    text = buildingSubjectSetByBuilding.GetString("U8Code");
                }
                buildingSubjectSetByBuilding.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetFinanceInterfaceAnalysisTypeTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("FinanceInterfaceAnalysisType");
                table.Columns.Add("FinanceInterfaceAnalysisTypeCode");
                table.Columns.Add("FinanceInterfaceAnalysisTypeName");
                table.Rows.Add(new object[] { "Unit", "部门财务编码" });
                table.Rows.Add(new object[] { "User", "人员财务编码" });
                table.Rows.Add(new object[] { "Building", "楼栋财务编码" });
                table.Rows.Add(new object[] { "Project", "项目财务编码" });
                table.Rows.Add(new object[] { "Supplier", "厂商财务编码" });
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetFinanceInterfaceSupplierCode(string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (SubjectSetCode == "")
                {
                    return text;
                }
                EntityData subjectSetByCode = SubjectDAO.GetSubjectSetByCode(SubjectSetCode);
                if (subjectSetByCode.HasRecord())
                {
                    text = subjectSetByCode.GetString("FinanceInterfaceSupplierCode");
                }
                subjectSetByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetFinanceSubjectSet(DataTable tbRelation)
        {
            EntityData data2;
            try
            {
                EntityData allSubjectSet = SubjectDAO.GetAllSubjectSet();
                allSubjectSet.CurrentTable.Columns.Add("U8Code");
                foreach (DataRow row in tbRelation.Rows)
                {
                    string text = ConvertRule.ToString(row["SubjectSetCode"]);
                    string text2 = ConvertRule.ToString(row["U8Code"]);
                    DataRow[] rowArray = allSubjectSet.CurrentTable.Select(string.Format("SubjectSetCode='{0}'", text));
                    if (rowArray.Length > 0)
                    {
                        rowArray[0]["U8Code"] = text2;
                    }
                }
                data2 = allSubjectSet;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetFinanceSubjectSetDesc(DataTable tbSet)
        {
            string text4;
            try
            {
                string text = "";
                foreach (DataRow row in tbSet.Rows)
                {
                    string subjectSetName = SubjectRule.GetSubjectSetName(ConvertRule.ToString(row["SubjectSetCode"]));
                    string text3 = ConvertRule.ToString(row["U8Code"]);
                    if (subjectSetName != "")
                    {
                        if (text != "")
                        {
                            text = text + "；";
                        }
                        text = text + subjectSetName + "：" + text3;
                    }
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static EntityData GetFinanceSubjectSetWithProject(DataTable tbRelation, string SubjectSetCode, string ProjectCode)
        {
            EntityData data3;
            try
            {
                EntityData allSubjectSet;
                EntityData data = new EntityData("SupplierSubjectSet");
                data.CurrentTable.Columns.Add("SubjectSetName");
                data.CurrentTable.Columns.Add("ProjectName");
                if (SubjectSetCode == "")
                {
                    allSubjectSet = SubjectDAO.GetAllSubjectSet();
                }
                else
                {
                    allSubjectSet = SubjectDAO.GetSubjectSetByCode(SubjectSetCode);
                }
                QueryAgent agent = new QueryAgent();
                try
                {
                    foreach (DataRow row in allSubjectSet.CurrentTable.Rows)
                    {
                        string text = ConvertRule.ToString(row["FinanceInterfaceSupplierCode"]);
                        string queryString = "select * from ( select a.SubjectSetCode + '_' + p.ProjectCode as SupplierSubjectSetCode, a.SubjectSetCode, a.SubjectSetName, p.ProjectCode, p.ProjectName from SubjectSet a, Project p where a.SubjectSetCode = p.SubjectSetCode union all select a.SubjectSetCode + '_' as SupplierSubjectSetCode, a.SubjectSetCode, a.SubjectSetName, '', '集团' from SubjectSet a) as a where 1 = 1 and SubjectSetCode = '" + ConvertRule.ToString(row["SubjectSetCode"]) + "'";
                        if (text.ToUpper() == "ByGroup".ToUpper())
                        {
                            queryString = queryString + " and ProjectCode = ''";
                        }
                        else if (ProjectCode != "")
                        {
                            queryString = queryString + " and ProjectCode = '" + ProjectCode + "'";
                        }
                        else
                        {
                            queryString = queryString + " and ProjectCode <> ''";
                        }
                        queryString = queryString + " order by SubjectSetName, ProjectName";
                        DataTable tbSrc = agent.ExecSqlForDataSet(queryString).Tables[0];
                        foreach (DataRow row2 in tbSrc.Rows)
                        {
                            DataRow drDst = data.CurrentTable.NewRow();
                            ConvertRule.DataRowCopy(row2, drDst, tbSrc, data.CurrentTable);
                            data.CurrentTable.Rows.Add(drDst);
                        }
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                foreach (DataRow row4 in tbRelation.Rows)
                {
                    string text3 = ConvertRule.ToString(row4["SubjectSetCode"]);
                    string text4 = ConvertRule.ToString(row4["ProjectCode"]);
                    string text5 = ConvertRule.ToString(row4["U8Code"]);
                    DataRow[] rowArray = data.CurrentTable.Select(string.Format("SubjectSetCode='{0}' and ProjectCode='{1}'", text3, text4));
                    if (rowArray.Length > 0)
                    {
                        rowArray[0]["U8Code"] = text5;
                    }
                }
                allSubjectSet.Dispose();
                data3 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data3;
        }

        public static string GetProjectSubjectSetU8Code(string ProjectCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (ProjectCode == "")
                {
                    return text;
                }
                EntityData projectSubjectSetByProject = ProjectDAO.GetProjectSubjectSetByProject(ProjectCode, SubjectSetCode);
                if (projectSubjectSetByProject.HasRecord())
                {
                    text = projectSubjectSetByProject.GetString("U8Code");
                }
                projectSubjectSetByProject.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetSupplierSubjectSetU8Code(string SupplierCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (SupplierCode == "")
                {
                    return text;
                }
                EntityData data = ProjectDAO.GetSupplierSubjectSetBySupplier(SupplierCode, "", SubjectSetCode);
                if (data.HasRecord())
                {
                    text = data.GetString("U8Code");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetSupplierSubjectSetU8Code(string SupplierCode, string ProjectCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (SupplierCode == "")
                {
                    return text;
                }
                EntityData data = ProjectDAO.GetSupplierSubjectSetBySupplier(SupplierCode, ProjectCode, SubjectSetCode);
                if (data.HasRecord())
                {
                    text = data.GetString("U8Code");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSupplierSubjectSetU8Code(string SupplierCode, string ProjectCode, string SubjectSetCode, string FinanceInterfaceSupplierCode)
        {
            string text;
            try
            {
                if (FinanceInterfaceSupplierCode.ToUpper() == "ByGroup".ToUpper())
                {
                    return GetSupplierSubjectSetU8Code(SupplierCode, SubjectSetCode);
                }
                text = GetSupplierSubjectSetU8Code(SupplierCode, ProjectCode, SubjectSetCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        public static string GetSystemUserSubjectSetU8Code(string UserCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (UserCode == "")
                {
                    return text;
                }
                EntityData systemUserSubjectSetByUser = SystemManageDAO.GetSystemUserSubjectSetByUser(UserCode, SubjectSetCode);
                if (systemUserSubjectSetByUser.HasRecord())
                {
                    text = systemUserSubjectSetByUser.GetString("U8Code");
                }
                systemUserSubjectSetByUser.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUFUnitSubjectSetU8Code(string UFUnitCode, string SubjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (UFUnitCode == "")
                {
                    return text;
                }
                EntityData unitSubjectSetByUnit = OBSDAO.GetUnitSubjectSetByUnit(UFUnitCode, SubjectSetCode);
                if (unitSubjectSetByUnit.HasRecord())
                {
                    text = unitSubjectSetByUnit.GetString("U8Code");
                }
                unitSubjectSetByUnit.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }
    }
}

