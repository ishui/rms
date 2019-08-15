namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.Check;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class SalBudgetRule
    {
        public static string[] BudgetFields = new string[] { "HouseCount", "HouseArea", "Price", "Money", "RcvMoney" };

        public static string CheckValidVal(string val, string FieldName)
        {
            string text = "";
            if ((val != null) && (val != ""))
            {
                if (FieldName.ToLower() == "housecount")
                {
                    if (!StringCheck.IsInt(val))
                    {
                        return string.Format("“{0}”不是有效的整数，请检查 ！ ", val);
                    }
                    return text;
                }
                if (!StringCheck.IsNumber(val))
                {
                    return string.Format("“{0}”不是有效的数值，请检查 ！ ", val);
                }
            }
            return text;
        }

        private static void FillSalBudgetDtlByYMInfra(DataTable tb, string ProjectCode, int y, int m, string ColName, DataTable tbPBSType, EntityData entityBudget)
        {
            try
            {
                string[] textArray = new string[] { "套数", "面积", "均价", "销售额", "回款额" };
                string[] textArray2 = new string[] { "", "平米", "元", "万元", "万元" };
                string[] textArray3 = new string[] { "int", "decimal", "decimal", "decimal", "decimal" };
                string[] textArray4 = new string[] { "HouseCount", "HouseArea", "Price", "Money", "RcvMoney" };
                int num = 0;
                for (int i = 0; i < textArray.Length; i++)
                {
                    num++;
                    string text = textArray[i];
                    string text2 = textArray4[i];
                    string text3 = textArray2[i];
                    string text4 = textArray3[i];
                    string text5 = text;
                    if (text3 != "")
                    {
                        text5 = text5 + "<br>(" + text3 + ")";
                    }
                    foreach (DataRow row2 in tbPBSType.Rows)
                    {
                        DataRow row;
                        string pBSTypeCode = ConvertRule.ToString(row2["PBSTypeCode"]);
                        DataRow[] rowArray = tb.Select("FieldName='" + text2 + "' and PBSTypeCode='" + pBSTypeCode + "'");
                        if (rowArray.Length == 0)
                        {
                            row = tb.NewRow();
                            row["sno"] = num;
                            row["ItemName"] = text;
                            row["FieldName"] = text2;
                            row["Unit"] = text3;
                            row["FieldType"] = text4;
                            row["ItemDesc"] = text5;
                            row["IsAct"] = 0;
                            row["IsActName"] = "计划";
                            row["PBSTypeCode"] = pBSTypeCode;
                            row["PBSTypeName"] = row2["PBSTypeName"];
                            row["PBSTypeSortID"] = row2["SortID"];
                            row["PBSTypeFullID"] = row2["FullID"];
                            row["Deep"] = row2["Deep"];
                            row["ParentCode"] = row2["ParentCode"];
                        }
                        else
                        {
                            row = rowArray[0];
                        }
                        string text7 = row["ItemName"].ToString();
                        string text8 = row["FieldType"].ToString();
                        DataRow row3 = null;
                        if (m == -1)
                        {
                            DataTable table = GetSalBudgetBeforePeriodSum(ProjectCode, y, pBSTypeCode);
                            if (table.Rows.Count > 0)
                            {
                                row3 = table.Rows[0];
                            }
                        }
                        else
                        {
                            DataRow[] rowArray2 = entityBudget.CurrentTable.Select("IMonth=" + m.ToString() + " and PBSTypeCode='" + pBSTypeCode + "'");
                            if (rowArray2.Length > 0)
                            {
                                row3 = rowArray2[0];
                            }
                        }
                        if (row3 != null)
                        {
                            if (text8 == "int")
                            {
                                row[ColName] = MathRule.GetIntShowString(row3[text2]);
                            }
                            else
                            {
                                row[ColName] = MathRule.GetDecimalShowString(row3[text2]);
                            }
                        }
                        if (rowArray.Length == 0)
                        {
                            tb.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string FormatSalBudgetFieldValue(object objVal, string FieldName)
        {
            string text2;
            try
            {
                string intShowString = "";
                if (FieldName.ToUpper() == "HOUSECOUNT")
                {
                    intShowString = MathRule.GetIntShowString(objVal);
                }
                else
                {
                    intShowString = MathRule.GetDecimalShowString(objVal);
                }
                text2 = intShowString;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GenerateSalBudgetDtlTableInfra(string ProjectCode, int IYear)
        {
            DataTable table4;
            try
            {
                int num;
                DataTable tb = new DataTable("SalBudget");
                tb.Columns.Add(new DataColumn("sno", typeof(int)));
                tb.Columns.Add(new DataColumn("ItemName", typeof(string)));
                tb.Columns.Add(new DataColumn("FieldName", typeof(string)));
                tb.Columns.Add(new DataColumn("Unit", typeof(string)));
                tb.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
                tb.Columns.Add(new DataColumn("FieldType", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeCode", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeName", typeof(string)));
                tb.Columns.Add(new DataColumn("PBSTypeSortID", typeof(int)));
                tb.Columns.Add(new DataColumn("PBSTypeFullID", typeof(string)));
                tb.Columns.Add(new DataColumn("Deep", typeof(int)));
                tb.Columns.Add(new DataColumn("ParentCode", typeof(string)));
                tb.Columns.Add(new DataColumn("IsAct", typeof(int)));
                tb.Columns.Add(new DataColumn("IsActName", typeof(string)));
                tb.Columns.Add(new DataColumn("y0", typeof(string)));
                for (num = 0; num <= 12; num++)
                {
                    tb.Columns.Add(new DataColumn("m" + num.ToString(), typeof(string)));
                }
                for (num = 1; num <= 2; num++)
                {
                    tb.Columns.Add(new DataColumn("y" + num.ToString(), typeof(string)));
                }
                DataTable tbPBSType = GetSalPBSType(true);
                EntityData entityBudget = SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear);
                FillSalBudgetDtlByYMInfra(tb, ProjectCode, IYear, -1, "y0", tbPBSType, entityBudget);
                for (int i = 0; i <= 12; i++)
                {
                    FillSalBudgetDtlByYMInfra(tb, ProjectCode, IYear, i, "m" + i.ToString(), tbPBSType, entityBudget);
                }
                for (num = 1; num <= 2; num++)
                {
                    EntityData salBudgetDtlByProjectYear = SalDAO.GetSalBudgetDtlByProjectYear(ProjectCode, IYear + num);
                    FillSalBudgetDtlByYMInfra(tb, ProjectCode, IYear + num, 0, "y" + num.ToString(), tbPBSType, salBudgetDtlByProjectYear);
                    salBudgetDtlByProjectYear.Dispose();
                }
                DataTable tbSrc = tb.Clone();
                foreach (DataRow row in tb.Rows)
                {
                    DataRow row2 = tbSrc.NewRow();
                    row2["sno"] = row["sno"];
                    row2["IsAct"] = 1;
                    row2["IsActName"] = "实际";
                    row2["ItemName"] = row["ItemName"];
                    row2["FieldName"] = row["FieldName"];
                    row2["Unit"] = row["Unit"];
                    row2["ItemDesc"] = row["ItemDesc"];
                    row2["FieldType"] = row["FieldType"];
                    row2["PBSTypeCode"] = row["PBSTypeCode"];
                    row2["PBSTypeName"] = row["PBSTypeName"];
                    row2["PBSTypeSortID"] = row["PBSTypeSortID"];
                    row2["PBSTypeFullID"] = row["PBSTypeFullID"];
                    row2["Deep"] = row["Deep"];
                    row2["ParentCode"] = row["ParentCode"];
                    tbSrc.Rows.Add(row2);
                }
                foreach (DataRow row2 in tbSrc.Rows)
                {
                    DataRow drDst = tb.NewRow();
                    ConvertRule.DataRowCopy(row2, drDst, tbSrc, tb);
                    tb.Rows.Add(drDst);
                }
                entityBudget.Dispose();
                table4 = tb;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        public static DataTable GetSalBudgetBeforePeriodSum(string ProjectCode, int IYear, string PBSTypeCode)
        {
            DataTable table2;
            try
            {
                SalBudgetDtlStrategyBuilder builder = new SalBudgetDtlStrategyBuilder();
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.ProjectCode, ProjectCode));
                ArrayList pas = new ArrayList();
                pas.Add("0");
                pas.Add((IYear - 1).ToString());
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IYearRange, pas));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.IMonth, "0"));
                builder.AddStrategy(new Strategy(SalBudgetDtlStrategyName.PBSTypeCode, PBSTypeCode));
                string queryString = builder.BuildQuerySumString();
                QueryAgent agent = new QueryAgent();
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

        public static DataTable GetSalPBSType(bool IsAddAll)
        {
            DataTable table2;
            try
            {
                EntityData pBSTypeByProject = PBSDAO.GetPBSTypeByProject("0");
                DataTable currentTable = pBSTypeByProject.CurrentTable;
                if (IsAddAll)
                {
                    DataRow row = currentTable.NewRow();
                    row["PBSTypeCode"] = "";
                    row["PBSTypeName"] = "所有";
                    row["Deep"] = 0;
                    row["ParentCode"] = "";
                    currentTable.Rows.InsertAt(row, 0);
                }
                pBSTypeByProject.Dispose();
                table2 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static void SaveSalBudgetOneDtl(string ProjectCode, string BudgetCode, EntityData entityDtl, int y, int m, string PBSTypeCode, string FieldName, string val)
        {
            try
            {
                DataRow row;
                DataRow[] rowArray = entityDtl.CurrentTable.Select("IYear=" + y.ToString() + " and IMonth=" + m.ToString() + " and PBSTypeCode='" + PBSTypeCode + "'");
                if (rowArray.Length == 0)
                {
                    row = entityDtl.CurrentTable.NewRow();
                    row["BudgetCode"] = BudgetCode;
                    row["SystemID"] = SystemManageDAO.GetNewSysCode("SalBudgetSystemID");
                    row["ProjectCode"] = ProjectCode;
                    row["IYear"] = y;
                    row["IMonth"] = m;
                    row["PBSTypeCode"] = PBSTypeCode;
                    entityDtl.CurrentTable.Rows.Add(row);
                }
                else
                {
                    row = rowArray[0];
                }
                row[FieldName] = ConvertRule.ToDecimalObj(val);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

