namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Xml;

    public class ConvertRule
    {
        public static string[] ArrayConcat(object[] arr1, object[] arr2)
        {
            string[] array = new string[arr1.Length + arr2.Length];
            arr1.CopyTo(array, 0);
            arr2.CopyTo(array, arr1.Length);
            return array;
        }

        public static string Concat(DataRow[] drs, string ColumnName, string sep)
        {
            string text2;
            try
            {
                string text = "";
                int length = drs.Length;
                for (int i = 0; i < length; i++)
                {
                    DataRow row = drs[i];
                    if (i > 0)
                    {
                        text = text + sep;
                    }
                    text = text + ToString(row[ColumnName]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string Concat(DataTable tb, string ColumnName, string sep)
        {
            string text2;
            try
            {
                text2 = Concat(tb, ColumnName, sep, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string Concat(DataTable tb, string ColumnName, string sep, string filter)
        {
            string text2;
            try
            {
                if (!tb.Columns.Contains(ColumnName))
                {
                    throw new ApplicationException(string.Format("表中未包含列{0}", ColumnName));
                }
                text2 = Concat(tb.Select(filter), ColumnName, sep);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void DataRowAddDecimal(DataRow drBase, DataRow drAdd, string ColumnName)
        {
            try
            {
                drBase[ColumnName] = ToDecimal(drBase[ColumnName]) + ToDecimal(drAdd[ColumnName]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataRowAddDecimal(DataRow drBase, DataRow drAdd, string[] ColumnNames)
        {
            try
            {
                foreach (string text in ColumnNames)
                {
                    DataRowAddDecimal(drBase, drAdd, text);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataRowCopy(DataRow drSrc, DataRow drDst, DataTable tbSrc, DataTable tbDst)
        {
            try
            {
                DataRowCopy(drSrc, drDst, tbSrc, tbDst, null);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataRowCopy(DataRow drSrc, DataRow drDst, DataTable tbSrc, DataTable tbDst, string[] ExceptField)
        {
            try
            {
                foreach (DataColumn column in tbDst.Columns)
                {
                    if (tbSrc.Columns.Contains(column.ColumnName) && (FindArray(ExceptField, column.ColumnName, true) < 0))
                    {
                        drDst[column.ColumnName] = drSrc[column.ColumnName];
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataTableAddColumn(DataTable tbSrc, DataTable tbDst)
        {
            try
            {
                foreach (DataColumn column in tbSrc.Columns)
                {
                    if (!tbDst.Columns.Contains(column.ColumnName))
                    {
                        tbDst.Columns.Add(column.ColumnName, column.DataType);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataTableCopyRow(DataTable tbSrc, DataTable tbDst)
        {
            try
            {
                foreach (DataRow row in tbSrc.Rows)
                {
                    DataRow drDst = tbDst.NewRow();
                    DataRowCopy(row, drDst, tbSrc, tbDst);
                    tbDst.Rows.Add(drDst);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DataTableCopyRow(DataView dvSrc, DataTable tbDst)
        {
            try
            {
                DataTable tbSrc = dvSrc.Table;
                foreach (DataRowView view in dvSrc)
                {
                    DataRow drSrc = view.Row;
                    DataRow drDst = tbDst.NewRow();
                    DataRowCopy(drSrc, drDst, tbSrc, tbDst);
                    tbDst.Rows.Add(drDst);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static int FindArray(string[] arr, string val)
        {
            return FindArray(arr, val, false);
        }

        public static int FindArray(string[] arr, string val, bool isEgnoreCase)
        {
            if (arr != null)
            {
                int length = arr.Length;
                for (int i = 0; i < length; i++)
                {
                    if (isEgnoreCase)
                    {
                        if (arr[i].ToUpper() == val.ToUpper())
                        {
                            return i;
                        }
                    }
                    else if (arr[i] == val)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int FindArrayLike(string[] arr, string val)
        {
            int length = arr.Length;
            int num2 = val.Length;
            for (int i = 0; i < length; i++)
            {
                string text = arr[i];
                if (text.Length > num2)
                {
                    text = text.Substring(0, num2);
                }
                if (text == val)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string FormatMM(object objMonth)
        {
            string text2;
            try
            {
                text2 = ToString(objMonth).PadLeft(2, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string FormatYYYYMM(object objYear, object objMonth)
        {
            string text4;
            try
            {
                string text = ToString(objYear);
                string text2 = ToString(objMonth);
                text4 = text.PadLeft(4, "0"[0]) + text2.PadLeft(2, "0"[0]);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        public static void GetAllTreeDataSource(DataTable dt, DataTable returndt, DataTable gradedt, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode)
        {
            GetAllTreeDataSource(dt, returndt, gradedt, "", CodeName, ParentCodeName, ParentCode, Code, LeftStr, Deep, PercentageValue, ConsiderDiathesisCode);
        }

        public static void GetAllTreeDataSource(DataTable dt, DataTable returndt, DataTable gradedt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode)
        {
            GetAllTreeDataSource(dt, returndt, gradedt, GradeMessageCode, CodeName, ParentCodeName, ParentCode, Code, LeftStr, Deep, PercentageValue, ConsiderDiathesisCode, "100001");
        }

        public static void GetAllTreeDataSource(DataTable dt, DataTable returndt, DataTable gradedt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode, string MainDefineCode)
        {
            if (Code == "")
            {
                returndt.Columns.Add("code", Type.GetType("System.String"));
                returndt.Columns.Add("treetop", Type.GetType("System.String"));
                returndt.Columns.Add("treebottom", Type.GetType("System.String"));
                returndt.Columns.Add("leftstr", Type.GetType("System.String"));
                returndt.Columns.Add("ChildCount", Type.GetType("System.String"));
                returndt.Columns.Add("deep", Type.GetType("System.String"));
                returndt.Columns.Add("freeflag", Type.GetType("System.String"));
                returndt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                returndt.Columns.Add("tempConsiderDiathesisCode", Type.GetType("System.String"));
                returndt.Columns.Add("issubtotal", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
                returndt.Clear();
                dt.Columns.Add("code", Type.GetType("System.String"));
                dt.Columns.Add("treetop", Type.GetType("System.String"));
                dt.Columns.Add("treebottom", Type.GetType("System.String"));
                dt.Columns.Add("leftstr", Type.GetType("System.String"));
                dt.Columns.Add("ChildCount", Type.GetType("System.String"));
                dt.Columns.Add("deep", Type.GetType("System.String"));
                dt.Columns.Add("freeflag", Type.GetType("System.String"));
                dt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                dt.Columns.Add("tempConsiderDiathesisCode", Type.GetType("System.String"));
                dt.Columns.Add("issubtotal", Type.GetType("System.String"));
                dt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                dt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                dt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
            }
            DataRow[] rowArray = dt.Select(ParentCodeName + "='" + ParentCode.ToString() + "'");
            int num = 1;
            Grade grade = new Grade();
            grade.GradeMessageCode = GradeMessageCode;
            DataTable grades = grade.GetGrades();
            GradeConsiderPercentage percentage = new GradeConsiderPercentage();
            DataTable lastConsiderPercentage = new DataTable();
            if (GradeMessageCode != "")
            {
                lastConsiderPercentage = percentage.GetLastConsiderPercentage(GradeMessageCode, MainDefineCode);
            }
            DataTable dtgradeConsiderDepartment = new GradeConsiderDepartment().GetGradeConsiderDepartments();
            ArrayList supplierDeparmentCode = BiddingGradeMainDefine.GetSupplierDeparmentCode(MainDefineCode);
            foreach (DataRow row in rowArray)
            {
                row["tempConsiderDiathesisCode"] = "";
                if (num == 1)
                {
                    row["freeflag"] = "1";
                    row["tempConsiderDiathesisCode"] = ConsiderDiathesisCode;
                }
                else
                {
                    row["freeflag"] = "0";
                }
                row["code"] = Code + ((num.ToString().Length < 2) ? ("0" + num.ToString()) : num.ToString());
                row["treetop"] = (Code == "") ? "block" : "none";
                row["leftstr"] = LeftStr + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                row["deep"] = Deep.ToString();
                row["tempPercentage"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row["Percentage"]) * 100M)).ToString("N0");
                row["issubtotal"] = "0";
                DataRow row2 = returndt.NewRow();
                row2.ItemArray = row.ItemArray;
                returndt.Rows.Add(row2);
                int num2 = GetTreeDataSource(dt, returndt, gradedt, dtgradeConsiderDepartment, GradeMessageCode, CodeName, ParentCodeName, row[CodeName].ToString(), row["code"].ToString(), row["leftstr"].ToString(), Deep + 1, (decimal) row["Percentage"], (string) row["ConsiderDiathesisCode"], MainDefineCode);
                row2["treebottom"] = (num2 == 0) ? "none" : "block";
                num2 += 2;
                row2["ChildCount"] = num2.ToString();
                row2["AgreementPoint"] = "0";
                row2["AgreementCode"] = "";
                row2["AgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'").Length != 0)
                {
                    row2["AgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["TechnicPoint"] = "0";
                row2["TechnicCode"] = "";
                row2["TechnicIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'").Length != 0)
                {
                    row2["TechnicIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemMajordomoPoint"] = "0";
                row2["ItemMajordomoCode"] = "";
                row2["ItemMajordomoIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'").Length != 0)
                {
                    row2["ItemMajordomoIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemAgreementPoint"] = "0";
                row2["ItemAgreementCode"] = "";
                row2["ItemAgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'").Length != 0)
                {
                    row2["ItemAgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemEngineeringPoint"] = "0";
                row2["ItemEngineeringCode"] = "";
                row2["ItemEngineeringIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'").Length != 0)
                {
                    row2["ItemEngineeringIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemDesignPoint"] = "0";
                row2["ItemDesignCode"] = "";
                row2["ItemDesignIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'").Length != 0)
                {
                    row2["ItemDesignIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ClientServicePoint"] = "0";
                row2["ClientServiceCode"] = "";
                row2["ClientServiceIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'").Length != 0)
                {
                    row2["ClientServiceIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["Flag"].ToString();
                }
                DataRow[] rowArray2 = gradedt.Select("ConsiderDiathesisCode='" + row["tempConsiderDiathesisCode"].ToString() + "' and GradeMessageCode='" + GradeMessageCode + "'");
                foreach (DataRow row3 in rowArray2)
                {
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[0].ToString())
                    {
                        row2["AgreementPoint"] = row3["GradeValue"];
                        row2["AgreementCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[1].ToString())
                    {
                        row2["TechnicPoint"] = row3["GradeValue"];
                        row2["TechnicCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[2].ToString())
                    {
                        row2["ItemMajordomoPoint"] = row3["GradeValue"];
                        row2["ItemMajordomoCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[3].ToString())
                    {
                        row2["ItemAgreementPoint"] = row3["GradeValue"];
                        row2["ItemAgreementCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[4].ToString())
                    {
                        row2["ItemEngineeringPoint"] = row3["GradeValue"];
                        row2["ItemEngineeringCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[5].ToString())
                    {
                        row2["ItemDesignPoint"] = row3["GradeValue"];
                        row2["ItemDesignCode"] = row3["GradeCode"];
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[6].ToString())
                    {
                        row2["ClientServicePoint"] = row3["GradeValue"];
                        row2["ClientServiceCode"] = row3["GradeCode"];
                    }
                }
                DataRow row4 = returndt.NewRow();
                row4.ItemArray = row.ItemArray;
                row4["code"] = row["code"].ToString() + "0" + num2.ToString();
                row4["tempConsiderDiathesisCode"] = row["ConsiderDiathesisCode"].ToString();
                row4["ConsiderDiathesisCode"] = "10" + num;
                row4["deep"] = "2";
                row4["leftstr"] = row["leftstr"].ToString() + row["leftstr"].ToString();
                row4["ConsiderDiathesis"] = "小计";
                row4["issubtotal"] = "1";
                row4["ChildCount"] = "0";
                row4["tempPercentage"] = row["tempPercentage"].ToString();
                row4["freeflag"] = "1";
                row4["treebottom"] = "none";
                row4["treetop"] = "none";
                row4["AgreementPoint"] = "0";
                row4["AgreementCode"] = "";
                row4["AgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'").Length != 0)
                {
                    row4["AgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["TechnicPoint"] = "0";
                row4["TechnicCode"] = "";
                row4["TechnicIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'").Length != 0)
                {
                    row4["TechnicIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["ItemMajordomoPoint"] = "0";
                row4["ItemMajordomoCode"] = "";
                row4["ItemMajordomoIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'").Length != 0)
                {
                    row4["ItemMajordomoIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["ItemAgreementPoint"] = "0";
                row4["ItemAgreementCode"] = "";
                row4["ItemAgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'").Length != 0)
                {
                    row4["ItemAgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["ItemEngineeringPoint"] = "0";
                row4["ItemEngineeringCode"] = "";
                row4["ItemEngineeringIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'").Length != 0)
                {
                    row4["ItemEngineeringIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["ItemDesignPoint"] = "0";
                row4["ItemDesignCode"] = "";
                row4["ItemDesignIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'").Length != 0)
                {
                    row4["ItemDesignIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["Flag"].ToString();
                }
                row4["ClientServicePoint"] = "0";
                row4["ClientServiceCode"] = "";
                row4["ClientServiceIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'").Length != 0)
                {
                    row4["ClientServiceIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["Flag"].ToString();
                }
                DataRow[] rowArray3 = returndt.Select("ParentCode='" + row["ConsiderDiathesisCode"].ToString() + "' or ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "'");
                string text = "";
                for (int i = 0; i < rowArray3.Length; i++)
                {
                    if (i != (rowArray3.Length - 1))
                    {
                        text = string.Concat(new object[] { text, "'", rowArray3[i]["ConsiderDiathesisCode"], "'," });
                    }
                    else
                    {
                        text = string.Concat(new object[] { text, "'", rowArray3[i]["ConsiderDiathesisCode"], "'" });
                    }
                }
                foreach (DataRow row5 in grades.Select("ConsiderDiathesisCode in (" + text + ")"))
                {
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[0].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["AgreementPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["AgreementPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["AgreementPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["AgreementPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[1].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["TechnicPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["TechnicPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["TechnicPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["TechnicPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[2].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["ItemMajordomoPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["ItemMajordomoPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["ItemMajordomoPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["ItemMajordomoPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[3].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["ItemAgreementPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["ItemAgreementPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["ItemAgreementPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["ItemAgreementPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[4].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["ItemEngineeringPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["ItemEngineeringPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["ItemEngineeringPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["ItemEngineeringPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[5].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["ItemDesignPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["ItemDesignPoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["ItemDesignPoint"] = Convert.ToString((int) (Convert.ToInt32(row4["ItemDesignPoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                    if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[6].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row4["ClientServicePoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row4["ClientServicePoint"]) + ((Convert.ToDecimal(row5["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row5["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row4["ClientServicePoint"] = Convert.ToString((int) (Convert.ToInt32(row4["ClientServicePoint"]) + Convert.ToInt32(row5["GradeValue"])));
                        }
                    }
                }
                returndt.Rows.Add(row4);
                num++;
            }
        }

        public static object GetArrayItem(object[] arr, int index)
        {
            object obj2 = null;
            if (arr.Length > index)
            {
                obj2 = arr[index];
            }
            return obj2;
        }

        public static string GetArrayLinkString(ArrayList ar)
        {
            string text = "";
            int count = ar.Count;
            for (int i = 0; i < count; i++)
            {
                if (text != "")
                {
                    text = text + ",";
                }
                text = text + ((string) ar[i]);
            }
            return text;
        }

        public static int GetDayOfWeek(DayOfWeek d)
        {
            int num2;
            try
            {
                int num = 0;
                switch (d.ToString())
                {
                    case "Monday":
                        num = 1;
                        break;

                    case "Tuesday":
                        num = 2;
                        break;

                    case "Wednesday ":
                        num = 3;
                        break;

                    case "Thursday":
                        num = 4;
                        break;

                    case "Friday":
                        num = 5;
                        break;

                    case "Saturday":
                        num = 6;
                        break;

                    case "Sunday":
                        num = 7;
                        break;
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static DataTable GetDistinct(DataRow[] drs, string ColumnName)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                if (drs.Length > 0)
                {
                    table = drs[0].Table.Clone();
                }
                else
                {
                    Type dataType = typeof(string);
                    if (drs.Length > 0)
                    {
                        dataType = drs[0].Table.Columns[ColumnName].DataType;
                    }
                    table.Columns.Add(ColumnName, dataType);
                }
                int length = drs.Length;
                for (int i = 0; i < length; i++)
                {
                    DataRow row = drs[i];
                    string text = ToString(row[ColumnName]);
                    if (table.Select(ColumnName + "='" + text + "'").Length == 0)
                    {
                        table.ImportRow(drs[i]);
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

        public static DataView GetDistinct(DataView dv, string ColumnName)
        {
            DataView view2;
            try
            {
                DataTable table = new DataTable();
                Type type = typeof(string);
                if (dv.Count > 0)
                {
                    type = dv[0].Row[ColumnName].GetType();
                }
                table.Columns.Add(ColumnName, type);
                int count = dv.Count;
                for (int i = 0; i < count; i++)
                {
                    string text = ToString(dv[i].Row[ColumnName]);
                    if (table.Select(ColumnName + "='" + text + "'").Length == 0)
                    {
                        DataRow row = table.NewRow();
                        row[ColumnName] = text;
                        table.Rows.Add(row);
                    }
                }
                DataView view = new DataView(table);
                view2 = view;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return view2;
        }

        public static DataTable GetDistinct(DataTable tb, string ColumnName, string filter)
        {
            DataTable distinct;
            try
            {
                if (!tb.Columns.Contains(ColumnName))
                {
                    throw new ApplicationException(string.Format("表中未包含列{0}", ColumnName));
                }
                distinct = GetDistinct(tb.Select(filter), ColumnName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return distinct;
        }

        public static string GetDistinctStr(DataRow[] drs, string ColumnName, string sep)
        {
            string text2;
            try
            {
                text2 = Concat(GetDistinct(drs, ColumnName), ColumnName, sep);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetDistinctStr(DataTable tb, string ColumnName, string filter, string sep)
        {
            string text2;
            try
            {
                if (!tb.Columns.Contains(ColumnName))
                {
                    throw new ApplicationException(string.Format("表中未包含列{0}", ColumnName));
                }
                text2 = GetDistinctStr(tb.Select(filter), ColumnName, sep);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetNextSno(HtmlInputHidden txtSno)
        {
            int num2;
            try
            {
                int num = 0;
                num = ToInt(txtSno.Value) + 1;
                txtSno.Value = num.ToString();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static void GetSHTreeDataSource(DataTable dt, DataTable returndt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode)
        {
            GetSHTreeDataSource(dt, returndt, GradeMessageCode, CodeName, ParentCodeName, ParentCode, Code, LeftStr, Deep, PercentageValue, ConsiderDiathesisCode, "100001");
        }

        public static void GetSHTreeDataSource(DataTable dt, DataTable returndt, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode, string MainDefineCode)
        {
            if (Code == "")
            {
                returndt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("UnitTotalPoint", Type.GetType("System.String"));
                returndt.Clear();
                dt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                dt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                dt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                dt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
                dt.Columns.Add("UnitTotalPoint", Type.GetType("System.String"));
            }
            DataRow[] rowArray = dt.Select(ParentCodeName + "='" + ParentCode.ToString() + "'");
            Grade grade = new Grade();
            grade.GradeMessageCode = GradeMessageCode;
            DataTable grades = grade.GetGrades();
            DataTable lastConsiderPercentage = new GradeConsiderPercentage().GetLastConsiderPercentage(GradeMessageCode, MainDefineCode);
            DataTable gradeConsiderDepartments = new GradeConsiderDepartment().GetGradeConsiderDepartments();
            ArrayList supplierDeparmentCode = BiddingGradeMainDefine.GetSupplierDeparmentCode(MainDefineCode);
            foreach (DataRow row in rowArray)
            {
                row["tempPercentage"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row["Percentage"]) * 100M)).ToString("N0");
                DataRow row2 = returndt.NewRow();
                row2.ItemArray = row.ItemArray;
                row2["AgreementPoint"] = 0;
                row2["AgreementCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[0].ToString(), GradeMessageCode);
                row2["AgreementIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'").Length != 0)
                {
                    row2["AgreementIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["TechnicPoint"] = 0;
                row2["TechnicCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[1].ToString(), GradeMessageCode);
                row2["TechnicIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'").Length != 0)
                {
                    row2["TechnicIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemMajordomoPoint"] = 0;
                row2["ItemMajordomoCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[2].ToString(), GradeMessageCode);
                row2["ItemMajordomoIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'").Length != 0)
                {
                    row2["ItemMajordomoIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemAgreementPoint"] = 0;
                row2["ItemAgreementCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[3].ToString(), GradeMessageCode);
                row2["ItemAgreementIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'").Length != 0)
                {
                    row2["ItemAgreementIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemEngineeringPoint"] = 0;
                row2["ItemEngineeringCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[4].ToString(), GradeMessageCode);
                row2["ItemEngineeringIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'").Length != 0)
                {
                    row2["ItemEngineeringIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemDesignPoint"] = 0;
                row2["ItemDesignCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[5].ToString(), GradeMessageCode);
                row2["ItemDesignIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'").Length != 0)
                {
                    row2["ItemDesignIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ClientServicePoint"] = 0;
                row2["ClientServiceCode"] = GradeList.GetGradeCode(row["ConsiderDiathesisCode"].ToString(), supplierDeparmentCode[6].ToString(), GradeMessageCode);
                row2["ClientServiceIsusing"] = 1;
                if (gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'").Length != 0)
                {
                    row2["ClientServiceIsusing"] = gradeConsiderDepartments.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["UnitTotalPoint"] = "0";
                DataRow[] rowArray2 = dt.Select("ParentCode='" + row["ConsiderDiathesisCode"].ToString() + "' or ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "'");
                string text = "";
                for (int i = 0; i < rowArray2.Length; i++)
                {
                    if (i != (rowArray2.Length - 1))
                    {
                        text = string.Concat(new object[] { text, "'", rowArray2[i]["ConsiderDiathesisCode"], "'," });
                    }
                    else
                    {
                        text = string.Concat(new object[] { text, "'", rowArray2[i]["ConsiderDiathesisCode"], "'" });
                    }
                }
                foreach (DataRow row3 in grades.Select("ConsiderDiathesisCode in (" + text + ")"))
                {
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[0].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["AgreementPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["AgreementPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["AgreementPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["AgreementPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[1].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["TechnicPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["TechnicPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["TechnicPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["TechnicPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[2].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["ItemMajordomoPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["ItemMajordomoPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["ItemMajordomoPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["ItemMajordomoPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[3].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["ItemAgreementPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["ItemAgreementPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["ItemAgreementPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["ItemAgreementPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[4].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["ItemEngineeringPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["ItemEngineeringPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["ItemEngineeringPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["ItemEngineeringPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[5].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["ItemDesignPoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["ItemDesignPoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["ItemDesignPoint"] = Convert.ToString((int) (Convert.ToInt32(row2["ItemDesignPoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                    if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[6].ToString())
                    {
                        if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'").Length != 0))
                        {
                            row2["ClientServicePoint"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row2["ClientServicePoint"]) + ((Convert.ToDecimal(row3["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + row3["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M))).ToString();
                        }
                        else
                        {
                            row2["ClientServicePoint"] = Convert.ToString((int) (Convert.ToInt32(row2["ClientServicePoint"]) + Convert.ToInt32(row3["GradeValue"])));
                        }
                    }
                }
                string decimalNoPointShowString = "0";
                string text3 = "0";
                string text4 = "0";
                string text5 = "0";
                string text6 = "0";
                string text7 = "0";
                string text8 = "0";
                DataTable gradeDepartments = new GradeDepartment().GetGradeDepartments();
                GradeDepartmentPercentage percentage2 = new GradeDepartmentPercentage();
                DataTable lastDepartmentPercentage = new DataTable();
                if (GradeMessageCode != "")
                {
                    lastDepartmentPercentage = percentage2.GetLastDepartmentPercentage(GradeMessageCode, MainDefineCode);
                }
                decimal num3 = 0M;
                foreach (DataRow row4 in lastDepartmentPercentage.Select())
                {
                    num3 += (decimal) row4["Percentage"];
                }
                if (gradeDepartments != null)
                {
                    foreach (DataRow row5 in gradeDepartments.Select("MainDefineCode='" + MainDefineCode + "'"))
                    {
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[0].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["Percentage"];
                            }
                            decimalNoPointShowString = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["AgreementPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (decimalNoPointShowString == "")
                            {
                                decimalNoPointShowString = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[1].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["Percentage"];
                            }
                            text3 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["TechnicPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text3 == "")
                            {
                                text3 = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[2].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["Percentage"];
                            }
                            text4 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["ItemMajordomoPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text4 == "")
                            {
                                text4 = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[3].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["Percentage"];
                            }
                            text5 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["ItemAgreementPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text5 == "")
                            {
                                text5 = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[4].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["Percentage"];
                            }
                            text6 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["ItemEngineeringPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text6 == "")
                            {
                                text6 = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[5].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["Percentage"];
                            }
                            text7 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["ItemDesignPoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text7 == "")
                            {
                                text7 = "0";
                            }
                        }
                        if (row5["DepartmentDefineCode"].ToString() == supplierDeparmentCode[6].ToString())
                        {
                            if (lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'").Length != 0)
                            {
                                if ((lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["AdjustCoefficient"] != null) && (Convert.ToString(lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["AdjustCoefficient"]) != ""))
                                {
                                    row5["AdjustCoefficient"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["AdjustCoefficient"];
                                }
                                row5["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["Percentage"];
                            }
                            text8 = MathRule.GetDecimalNoPointShowString((Convert.ToDecimal(row2["ClientServicePoint"]) * ((decimal) row5["AdjustCoefficient"])) * ((decimal) row5["Percentage"]));
                            if (text8 == "")
                            {
                                text8 = "0";
                            }
                        }
                    }
                }
                row2["UnitTotalPoint"] = Convert.ToString(Convert.ToInt32((int) ((((((Convert.ToInt32(decimalNoPointShowString) + Convert.ToInt32(text3)) + Convert.ToInt32(text4)) + Convert.ToInt32(text5)) + Convert.ToInt32(text6)) + Convert.ToInt32(text7)) + Convert.ToInt32(text8))));
                returndt.Rows.Add(row2);
            }
        }

        public static int GetTreeDataSource(DataTable dt, DataTable returndt, DataTable gradedt, DataTable dtgradeConsiderDepartment, string GradeMessageCode, string CodeName, string ParentCodeName, string ParentCode, string Code, string LeftStr, int Deep, decimal PercentageValue, string ConsiderDiathesisCode, string MainDefineCode)
        {
            if (Code == "")
            {
                returndt.Columns.Add("code", Type.GetType("System.String"));
                returndt.Columns.Add("treetop", Type.GetType("System.String"));
                returndt.Columns.Add("treebottom", Type.GetType("System.String"));
                returndt.Columns.Add("leftstr", Type.GetType("System.String"));
                returndt.Columns.Add("ChildCount", Type.GetType("System.String"));
                returndt.Columns.Add("deep", Type.GetType("System.String"));
                returndt.Columns.Add("freeflag", Type.GetType("System.String"));
                returndt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                returndt.Columns.Add("tempConsiderDiathesisCode", Type.GetType("System.String"));
                returndt.Columns.Add("issubtotal", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                returndt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                returndt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                returndt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
                returndt.Clear();
                dt.Columns.Add("code", Type.GetType("System.String"));
                dt.Columns.Add("treetop", Type.GetType("System.String"));
                dt.Columns.Add("treebottom", Type.GetType("System.String"));
                dt.Columns.Add("leftstr", Type.GetType("System.String"));
                dt.Columns.Add("ChildCount", Type.GetType("System.String"));
                dt.Columns.Add("deep", Type.GetType("System.String"));
                dt.Columns.Add("freeflag", Type.GetType("System.String"));
                dt.Columns.Add("tempPercentage", Type.GetType("System.String"));
                dt.Columns.Add("tempConsiderDiathesisCode", Type.GetType("System.String"));
                dt.Columns.Add("issubtotal", Type.GetType("System.String"));
                dt.Columns.Add("AgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("AgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("AgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("TechnicPoint", Type.GetType("System.String"));
                dt.Columns.Add("TechnicCode", Type.GetType("System.String"));
                dt.Columns.Add("TechnicIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemMajordomoIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemAgreementIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemEngineeringIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignPoint", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignCode", Type.GetType("System.String"));
                dt.Columns.Add("ItemDesignIsusing", Type.GetType("System.String"));
                dt.Columns.Add("ClientServicePoint", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceCode", Type.GetType("System.String"));
                dt.Columns.Add("ClientServiceIsusing", Type.GetType("System.String"));
            }
            DataRow[] rowArray = dt.Select(ParentCodeName + "='" + ParentCode.ToString() + "'");
            int num = 1;
            int num2 = 0;
            ArrayList supplierDeparmentCode = BiddingGradeMainDefine.GetSupplierDeparmentCode(MainDefineCode);
            foreach (DataRow row in rowArray)
            {
                row["tempConsiderDiathesisCode"] = "";
                if (num == 1)
                {
                    row["tempConsiderDiathesisCode"] = ConsiderDiathesisCode;
                }
                row["code"] = Code + ((num.ToString().Length < 2) ? ("0" + num.ToString()) : num.ToString());
                row["treetop"] = (Code == "") ? "block" : "none";
                row["leftstr"] = LeftStr + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                row["deep"] = Deep.ToString();
                row["tempPercentage"] = Convert.ToInt32((decimal) (Convert.ToDecimal(row["Percentage"]) * 100M)).ToString("N0");
                row["issubtotal"] = "0";
                DataRow row2 = returndt.NewRow();
                row2.ItemArray = row.ItemArray;
                returndt.Rows.Add(row2);
                int num3 = GetTreeDataSource(dt, returndt, gradedt, dtgradeConsiderDepartment, GradeMessageCode, CodeName, ParentCodeName, row[CodeName].ToString(), row["code"].ToString(), row["leftstr"].ToString(), Deep + 1, (decimal) row["Percentage"], (string) row["ConsiderDiathesisCode"], MainDefineCode);
                row2["treebottom"] = (num3 == 0) ? "none" : "block";
                if (num3 == 0)
                {
                    num2++;
                }
                else
                {
                    num2 += num3;
                }
                row2["ChildCount"] = num3.ToString();
                row2["freeflag"] = "0";
                row2["AgreementPoint"] = "0";
                row2["AgreementCode"] = "";
                row2["AgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'").Length != 0)
                {
                    row2["AgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[0].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["TechnicPoint"] = "0";
                row2["TechnicCode"] = "";
                row2["TechnicIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'").Length != 0)
                {
                    row2["TechnicIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[1].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemMajordomoPoint"] = "0";
                row2["ItemMajordomoCode"] = "";
                row2["ItemMajordomoIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'").Length != 0)
                {
                    row2["ItemMajordomoIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[2].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemAgreementPoint"] = "0";
                row2["ItemAgreementCode"] = "";
                row2["ItemAgreementIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'").Length != 0)
                {
                    row2["ItemAgreementIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[3].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemEngineeringPoint"] = "0";
                row2["ItemEngineeringCode"] = "";
                row2["ItemEngineeringIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'").Length != 0)
                {
                    row2["ItemEngineeringIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[4].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ItemDesignPoint"] = "0";
                row2["ItemDesignCode"] = "";
                row2["ItemDesignIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'").Length != 0)
                {
                    row2["ItemDesignIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[5].ToString() + "'")[0]["Flag"].ToString();
                }
                row2["ClientServicePoint"] = "0";
                row2["ClientServiceCode"] = "";
                row2["ClientServiceIsusing"] = 1;
                if (dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'").Length != 0)
                {
                    row2["ClientServiceIsusing"] = dtgradeConsiderDepartment.Select("ConsiderDiathesisCode='" + row["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + supplierDeparmentCode[6].ToString() + "'")[0]["Flag"].ToString();
                }
                if ((num == 1) && (row2["ChildCount"].ToString() == "0"))
                {
                    row2["freeflag"] = "1";
                    DataRow[] rowArray2 = gradedt.Select("ConsiderDiathesisCode='" + row["tempConsiderDiathesisCode"].ToString() + "' and GradeMessageCode='" + GradeMessageCode + "'");
                    foreach (DataRow row3 in rowArray2)
                    {
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[0].ToString())
                        {
                            row2["AgreementPoint"] = row3["GradeValue"];
                            row2["AgreementCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[1].ToString())
                        {
                            row2["TechnicPoint"] = row3["GradeValue"];
                            row2["TechnicCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[2].ToString())
                        {
                            row2["ItemMajordomoPoint"] = row3["GradeValue"];
                            row2["ItemMajordomoCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[3].ToString())
                        {
                            row2["ItemAgreementPoint"] = row3["GradeValue"];
                            row2["ItemAgreementCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[4].ToString())
                        {
                            row2["ItemEngineeringPoint"] = row3["GradeValue"];
                            row2["ItemEngineeringCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[5].ToString())
                        {
                            row2["ItemDesignPoint"] = row3["GradeValue"];
                            row2["ItemDesignCode"] = row3["GradeCode"];
                        }
                        if (row3["DepartmentDefineCode"].ToString() == supplierDeparmentCode[6].ToString())
                        {
                            row2["ClientServicePoint"] = row3["GradeValue"];
                            row2["ClientServiceCode"] = row3["GradeCode"];
                        }
                    }
                }
                num++;
            }
            return num2;
        }

        public XmlNode GetXmlDate(string OperationDate)
        {
            return this.GetXmlDate(OperationDate, "properties");
        }

        public XmlNode GetXmlDate(string OperationDate, string NodeName)
        {
            try
            {
                OperationDate = HttpContext.Current.Server.HtmlDecode(OperationDate);
                XmlDocument document = new XmlDocument();
                document.LoadXml(OperationDate);
                XmlNode node2 = document.DocumentElement.SelectSingleNode("application");
                if (node2 == null)
                {
                    throw new ApplicationException("没有找到定义的字符串:application");
                }
                XmlNode node3 = node2.SelectSingleNode(NodeName);
                if (node3 == null)
                {
                    throw new ApplicationException("没有找到定义的字符串:" + NodeName);
                }
                return node3;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsDateStartLessThanEnd(object StartDate, object EndDate)
        {
            bool flag2;
            try
            {
                bool flag = true;
                object obj2 = ToDate(StartDate);
                object obj3 = ToDate(EndDate);
                if ((obj2 == DBNull.Value) || (obj3 == DBNull.Value))
                {
                    return true;
                }
                DateTime time = (DateTime) obj2;
                DateTime time2 = (DateTime) obj3;
                flag = time <= time2;
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static string JoinArray(object[] arr, string separator)
        {
            string text = "";
            int length = arr.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > 0)
                {
                    text = text + separator;
                }
                text = text + arr[i].ToString();
            }
            return text;
        }

        public static bool ToBool(object val)
        {
            bool flag = false;
            try
            {
                if ((val == null) || (val == DBNull.Value))
                {
                    return flag;
                }
                try
                {
                    flag = bool.Parse(val.ToString());
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static object ToDate(object val)
        {
            object obj2 = DBNull.Value;
            if ((val == null) || (val.ToString() == ""))
            {
                return obj2;
            }
            try
            {
                return DateTime.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string ToDateString(object val, string format)
        {
            if ((val == null) || (val.ToString() == ""))
            {
                return "";
            }
            try
            {
                return DateTime.Parse(val.ToString()).ToString(format);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static decimal ToDecimal(object val)
        {
            if ((val == null) || (val.ToString() == ""))
            {
                return 0M;
            }
            try
            {
                return decimal.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static object ToDecimalObj(object val)
        {
            object obj2 = DBNull.Value;
            if ((val == null) || (val.ToString() == ""))
            {
                return obj2;
            }
            try
            {
                return decimal.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static double ToDouble(object val)
        {
            if ((val == null) || (val.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return double.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static object ToDoubleObj(object val)
        {
            object obj2 = DBNull.Value;
            if ((val == null) || (val.ToString() == ""))
            {
                return obj2;
            }
            try
            {
                return double.Parse(val.ToString());
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public static int ToInt(object val)
        {
            if ((val == null) || (val.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return int.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static object ToIntObj(object val)
        {
            object obj2 = DBNull.Value;
            if ((val == null) || (val.ToString() == ""))
            {
                return obj2;
            }
            try
            {
                return int.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static long ToLong(object val)
        {
            if ((val == null) || (val.ToString() == ""))
            {
                return 0;
            }
            try
            {
                return long.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static object ToLongObj(object val)
        {
            object obj2 = DBNull.Value;
            if ((val == null) || (val.ToString() == ""))
            {
                return obj2;
            }
            try
            {
                return long.Parse(val.ToString());
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string ToString(object val)
        {
            string text = "";
            try
            {
                if ((val == null) || (val == DBNull.Value))
                {
                    return text;
                }
                try
                {
                    text = val.ToString();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }
    }
}

