namespace RmsPM.BLL
{
    using System;
    using System.Data;

    public class GradeList : Grade
    {
        public static string GetGrade(string ConsiderDiathesisCode, string DepartmentCode, string GradeMessageCode)
        {
            GradeList list = new GradeList();
            list.GradeMessageCode = GradeMessageCode;
            list.ConsiderDiathesisCode = ConsiderDiathesisCode;
            list.DepartmentDefineCode = DepartmentCode;
            DataTable grades = list.GetGrades();
            if (grades.Rows.Count > 0)
            {
                return grades.Rows[0]["GradeValue"].ToString();
            }
            return "0";
        }

        public static string GetGradeCode(string ConsiderDiathesisCode, string DepartmentCode, string GradeMessageCode)
        {
            GradeList list = new GradeList();
            list.GradeMessageCode = GradeMessageCode;
            list.ConsiderDiathesisCode = ConsiderDiathesisCode;
            list.DepartmentDefineCode = DepartmentCode;
            DataTable grades = list.GetGrades();
            if (grades.Rows.Count > 0)
            {
                return grades.Rows[0]["GradeCode"].ToString();
            }
            return "0";
        }

        public static string GetGradePoint(string DepartmentCode)
        {
            return GetGradePoint(DepartmentCode, "");
        }

        public static string GetGradePoint(string DepartmentCode, string GradeMessageCode)
        {
            return GetGradePoint(DepartmentCode, GradeMessageCode, "100001");
        }

        public static string GetGradePoint(string DepartmentCode, string GradeMessageCode, string MainDefineCode)
        {
            GradeList list = new GradeList();
            if ((GradeMessageCode != "") && (GradeMessageCode != null))
            {
                list.GradeMessageCode = GradeMessageCode;
            }
            if ((DepartmentCode != "") && (DepartmentCode != null))
            {
                list.DepartmentDefineCode = DepartmentCode;
            }
            DataTable grades = list.GetGrades();
            DataTable lastConsiderPercentage = new GradeConsiderPercentage().GetLastConsiderPercentage(GradeMessageCode, MainDefineCode);
            decimal num = 0M;
            if (list != null)
            {
                for (int i = 0; i < grades.Rows.Count; i++)
                {
                    if ((lastConsiderPercentage != null) && (lastConsiderPercentage.Rows.Count != 0))
                    {
                        num += (Convert.ToDecimal(grades.Rows[i]["GradeValue"]) * Convert.ToDecimal(lastConsiderPercentage.Select("ConsiderDiathesisCode='" + grades.Rows[i]["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"])) * 10M;
                    }
                    else
                    {
                        num += Convert.ToDecimal(grades.Rows[i]["GradeValue"]);
                    }
                }
            }
            return num.ToString("N0");
        }

        public static string GetGradePoint(string DepartmentCode, string GradeMessageCode, DataTable dtcPoint, DataTable dtgradeConsiderPercentage)
        {
            decimal num = 0M;
            for (int i = 0; i < dtcPoint.Rows.Count; i++)
            {
                if ((dtgradeConsiderPercentage != null) && (dtgradeConsiderPercentage.Rows.Count != 0))
                {
                    num += (Convert.ToDecimal(dtcPoint.Rows[i]["GradeValue"]) * Convert.ToDecimal(dtgradeConsiderPercentage.Select("ConsiderDiathesisCode='" + dtcPoint.Rows[i]["ConsiderDiathesisCode"].ToString() + "' and DepartmentDefineCode='" + DepartmentCode + "'")[0]["Percentage"])) * 10M;
                }
                else
                {
                    num += Convert.ToDecimal(dtcPoint.Rows[i]["GradeValue"]);
                }
            }
            return num.ToString("N0");
        }

        public static string GetSumGradePoint(string GradeMessageCode)
        {
            try
            {
                string gradePoint = "0";
                string text2 = "0";
                string text3 = "0";
                string text4 = "0";
                string text5 = "0";
                string text6 = "0";
                string text7 = "0";
                string decimalNoPointShowString = "0";
                string text9 = "0";
                string text10 = "0";
                string text11 = "0";
                string text12 = "0";
                string text13 = "0";
                string text14 = "0";
                gradePoint = GetGradePoint("100001", GradeMessageCode);
                text2 = GetGradePoint("100002", GradeMessageCode);
                text3 = GetGradePoint("100003", GradeMessageCode);
                text4 = GetGradePoint("100004", GradeMessageCode);
                text5 = GetGradePoint("100005", GradeMessageCode);
                text6 = GetGradePoint("100006", GradeMessageCode);
                text7 = GetGradePoint("100007", GradeMessageCode);
                DataTable gradeDepartments = new GradeDepartment().GetGradeDepartments();
                DataTable lastDepartmentPercentage = new GradeDepartmentPercentage().GetLastDepartmentPercentage(GradeMessageCode, "100001");
                decimal num = 0M;
                foreach (DataRow row in lastDepartmentPercentage.Select())
                {
                    num += (decimal) row["Percentage"];
                }
                if (gradeDepartments != null)
                {
                    foreach (DataRow row2 in gradeDepartments.Select("MainDefineCode='100001'"))
                    {
                        switch (row2["DepartmentDefineCode"].ToString())
                        {
                            case "100001":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100001'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["Percentage"];
                                }
                                decimalNoPointShowString = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(gradePoint) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (decimalNoPointShowString == "")
                                {
                                    decimalNoPointShowString = "0";
                                }
                                break;

                            case "100002":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100002'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["Percentage"];
                                }
                                text9 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text2) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text9 == "")
                                {
                                    text9 = "0";
                                }
                                break;

                            case "100003":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100003'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["Percentage"];
                                }
                                text10 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text3) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text10 == "")
                                {
                                    text10 = "0";
                                }
                                break;

                            case "100004":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100004'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["Percentage"];
                                }
                                text11 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text4) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text11 == "")
                                {
                                    text11 = "0";
                                }
                                break;

                            case "100005":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100005'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["Percentage"];
                                }
                                text12 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text5) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text12 == "")
                                {
                                    text12 = "0";
                                }
                                break;

                            case "100006":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100006'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["Percentage"];
                                }
                                text13 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text6) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text13 == "")
                                {
                                    text13 = "0";
                                }
                                break;

                            case "100007":
                                if (lastDepartmentPercentage.Select("DepartmentDefineCode='100007'").Length != 0)
                                {
                                    row2["Percentage"] = (decimal) lastDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["Percentage"];
                                }
                                text14 = MathRule.GetDecimalNoPointShowString(((Convert.ToDecimal(text7) * ((decimal) row2["AdjustCoefficient"])) * ((decimal) row2["Percentage"])) / num);
                                if (text14 == "")
                                {
                                    text14 = "0";
                                }
                                break;
                        }
                    }
                }
                return MathRule.GetDecimalNoPointShowString((((((Convert.ToDecimal(decimalNoPointShowString) + Convert.ToDecimal(text9)) + Convert.ToDecimal(text10)) + Convert.ToDecimal(text11)) + Convert.ToDecimal(text12)) + Convert.ToDecimal(text13)) + Convert.ToDecimal(text14));
            }
            catch
            {
                return "";
            }
        }
    }
}

